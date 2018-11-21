using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIO.ServiceModel
{
    public class ChannelDispatcher : IChannelDispatcher
    {
        private Socket socket;
        public event EventHandler Closed;
        public event EventHandler Faulted;
        IMessageEncoder encoder;
        Action<IMessage> handler;
        private string id;
        public ChannelDispatcher(IMessageEncoder e)
        {
            encoder = e;
            id = Guid.NewGuid().ToString();
        }
        public void Close()
        {
            socket.Close();
            Closed?.Invoke(this, EventArgs.Empty);
        }

        public IMessageEncoder GetMessageEncoder()
        {
            return encoder;
        }

        public void OnMessage(Action<IMessage> handler)
        {
            this.handler = handler;
        }

        public void Open(Uri uri, string interfaceName)
        {
            this.socket = IO.Socket(uri);
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                var message = encoder.CreateMessage();
                message.IsResponse = false;
                message.Action = "register";
                message.MessageId = Guid.NewGuid().ToString();
                message.Data = new
                {
                    id = this.id,
                    time = DateTime.UtcNow,
                    contract = interfaceName
                };
                socket.Emit("register", encoder.EncodeMessage(message));
            })
            .On("svcAction", (data) =>
            {
                var token = JToken.FromObject(data);
                var message = encoder.DecodeMessage(token);
                if (handler != null)
                    handler(message);
            })
            .On(Socket.EVENT_ERROR, (data) =>
            {
                System.Diagnostics.Trace.WriteLine(data.ToString());
            });
        }

        public void Post(IMessage message)
        {
            var eventName = message.IsResponse ? "svcReply" : "svcAction";
            socket.Emit(eventName, encoder.EncodeMessage(message));
        }

        public IMessage Send(IMessage message)
        {
            throw new NotImplementedException();
        }
        
    }
}