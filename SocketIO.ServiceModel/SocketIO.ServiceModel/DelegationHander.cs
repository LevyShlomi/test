using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIO.ServiceModel
{
    public class DelegationHandler //: IDelegationHander
    {
        IServiceBinder serviceBinder;
        public DelegationHandler(IServiceBinder service)
        {
            serviceBinder = service;
        }
        public Task<IMessage> APICall(IMessage message, IActionInvoker invoker)
        {
            return Task.Run(async () => await invoker.Invoke(message));
        }

        protected virtual async Task<IMessage> InvokeAsync(IMessage message, IActionInvoker invoker)
        {
            return await invoker.Invoke(message);
        }
    }

}