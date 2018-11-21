using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketIO.ServiceModel
{
    public interface IServiceFactory
    {
        ServiceEndpoint CreateServiceEndpoint<T>(Uri uri, T singleton);
    }
}