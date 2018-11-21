using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocketIO.ServiceModel.Reflection
{
    public static class ReflectionExtensions
    {
        public static OperationDescriptor ToOperationDescriptor(this MethodInfo prop)
        {
            var d = prop.GetCustomAttribute<System.ServiceModel.OperationContractAttribute>(true);
            if(d != null)
            {
                var ret = new OperationDescriptor(d);
                ret.MethodInfo = prop;
                ret.ParameterTypes = prop.GetParameters().Select(p => p.ParameterType).ToArray(); 
                ret.ReturnParamType = prop.ReturnParameter?.ParameterType;
                //ret.MethodDelegate = new Func<object[],object>(args => prop.Invoke(//prop.CreateDelegate(typeof(Delegate));
                return ret;
            }
            return null;
        }
    }

    public class OperationDescriptor
    {
        public OperationDescriptor()
        {

        }
        public OperationDescriptor(System.ServiceModel.OperationContractAttribute op)
        {
            this.Name = op.Name;
            this.IsOneway = op.IsOneWay;
            this.Action = op.Action ?? Name;
            this.ReplyAction = op.ReplyAction ?? $"{Action}Reply";
        }

        public string Action { get; private set; }
        public bool IsOneway { get; private set; }
        public MethodInfo MethodInfo { get; internal set; }
        public string Name { get; set; }
        public Type[] ParameterTypes { get; internal set; }
        public string ReplyAction { get; private set; }
        public Type ReturnParamType { get; internal set; }
        //public Delegate MethodDelegate { get; internal set; }

        public object Invoke(object instance, object[] args)
        {
            return this.MethodInfo.Invoke(instance, args);
        }
    }

}
