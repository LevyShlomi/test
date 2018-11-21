using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketIO.ServiceModel.Reflection;
using Newtonsoft.Json.Linq;

namespace SocketIO.ServiceModel
{
    public class ServiceBinder<T> : IServiceBinder
    {
        private Dictionary<string, OperationDescriptor> operations;
        private T singleton;

        public List<OperationDescriptor> GetOperations()
        {
            return operations.Values.ToList();
        }
        public OperationDescriptor OperationByName(string name)
        {
            OperationDescriptor ret;
            operations.TryGetValue(name, out ret);
            return ret;
        }
        public string InterfaceName { get; private set; }
        public ServiceBinder(T instance)
        {
            this.operations = typeof(T).GetMethods().Select(m => m.ToOperationDescriptor()).ToDictionary(o => o.Name, StringComparer.OrdinalIgnoreCase);
            this.InterfaceName = typeof(T)
                .GetCustomAttributes(typeof(System.ServiceModel.ServiceContractAttribute), true)
                .Cast<System.ServiceModel.ServiceContractAttribute>()
                .FirstOrDefault()?.Name ?? "";
            this.singleton = instance;
        }
        public object Invoke(string action, params object[] args)
        {
            OperationDescriptor op;
            if (!operations.TryGetValue(action, out op))
                throw new ArgumentException($"There is operation for action {action}");

            var parameters = args.Where(o => o != null).Select((obj, i) => JToken.FromObject(obj).ToObject(op.ParameterTypes[i]));
            return op.Invoke(singleton, parameters.ToArray());
        }
    }

    

}