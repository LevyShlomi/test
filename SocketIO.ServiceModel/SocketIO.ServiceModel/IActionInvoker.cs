using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketIO.ServiceModel
{
    public interface IActionInvoker
    {
        System.Threading.Tasks.Task<IMessage> Invoke(IMessage message);
    }
}