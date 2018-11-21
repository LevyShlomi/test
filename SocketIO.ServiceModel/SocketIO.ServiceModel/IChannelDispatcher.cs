using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIO.ServiceModel
{
    public interface IChannelDispatcher 
    {
        void Open(Uri uri, string interfaceName);
        void Post(IMessage message);
        IMessage Send(IMessage message);
        void Close();
        void OnMessage(Action<IMessage> handler);
        IMessageEncoder GetMessageEncoder();

        event EventHandler Closed;
        event EventHandler Faulted;
    }
}