using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketIO.ServiceModel
{
    public abstract class MessageEncoder : IMessageEncoder
    {
        public virtual IMessage CreateMessage()
        {
            return new Message();
        }

        public abstract IMessage DecodeMessage(object payload);

        public abstract object EncodeMessage(IMessage message);
    }
}