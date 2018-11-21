using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketIO.ServiceModel
{
    public class ServiceFactory : IServiceFactory
    {
        public ServiceEndpoint CreateServiceEndpoint<T>(Uri uri, T singleton)
        {
            IMessageEncoder encoder = new JsonMessageEncoder();

            IChannelDispatcher dispathcer = new ChannelDispatcher(encoder);

            IServiceBinder binder = new ServiceBinder<T>(singleton);

            ITaskScheduler scheduler = new TaskScheduler();

            ServiceEndpoint ret = new ServiceEndpoint(dispathcer, binder, scheduler, uri);

            return ret;
        }
    }
}