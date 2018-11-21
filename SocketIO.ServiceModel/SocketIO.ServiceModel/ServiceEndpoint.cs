using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIO.ServiceModel
{
    public class ServiceEndpoint : IActionInvoker
    {
        IChannelDispatcher channelDispatcher;
        IServiceBinder binder;
        ITaskScheduler scheduler;

        public Uri Uri { get; private set; }

        public ServiceEndpoint(IChannelDispatcher channelDispatcher, IServiceBinder binder, ITaskScheduler scheduler, Uri uri)
        {
            this.channelDispatcher = channelDispatcher;
            this.binder = binder;
            this.scheduler = scheduler;
            this.Uri = uri;
            channelDispatcher.OnMessage(this.OnMessage);

        }

        private void OnMessage(IMessage message)
        {
            if (!message.IsResponse)
            {
                IActionInvoker invoker = this;

                invoker.Invoke(message).ContinueWith((t) => channelDispatcher.Post(t.Result));

            }
        }

        private IMessage InvokeAction(IMessage message)
        {
            var action = message.Action;

            var result = binder.Invoke(action, message.Data);

            var replyMessage = channelDispatcher.GetMessageEncoder().CreateMessage();

            var reply = message.ReplyAction;
            if (string.IsNullOrEmpty(reply))
                return replyMessage;


            replyMessage.MessageId = message.MessageId;
            replyMessage.IsResponse = true;
            replyMessage.Action = reply;
            replyMessage.Data = result;

            return replyMessage;
        }

        public void Open()
        {
            channelDispatcher.Open(this.Uri, binder.InterfaceName);

        }

        Task<IMessage> IActionInvoker.Invoke(IMessage message)
        {
            return scheduler.Start(() => InvokeAction(message));
        }


    }
}