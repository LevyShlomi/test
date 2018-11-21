using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketIO.ServiceModel
{
    public interface IMessageEncoder
    {
        IMessage DecodeMessage(object payload);

        object EncodeMessage(IMessage message);

        IMessage CreateMessage();
    }
}