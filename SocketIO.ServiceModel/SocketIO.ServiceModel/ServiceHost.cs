using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIO.ServiceModel
{
    public class ServiceHost 
    {
        List<ServiceEndpoint> endpoints;
        IServiceFactory factory;
        public ServiceHost(IServiceFactory factory)
        {
            endpoints = new List<ServiceEndpoint>();
            this.factory = factory;
        }

        public ServiceEndpoint AddServiceEndpoint<T>(Uri uri,T singleton)
        {
            var ep = factory.CreateServiceEndpoint<T>(uri, singleton);
            this.endpoints.Add(ep);

            return ep;
        }

        public void Open()
        {
            foreach (var ep in endpoints)
            {
                ep.Open();
            }
        }
    }

   

}