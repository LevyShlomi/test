using SocketIO.ServiceModel.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketIO.ServiceModel
{
    public interface IServiceBinder
    {
        string InterfaceName { get; }
        List<OperationDescriptor> GetOperations();
        OperationDescriptor OperationByName(string name);
        object Invoke(string action, params object[] args);


    }
}