using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketIO.ServiceModel
{
    public interface IMessage
    {
        Dictionary<string,string> MessageHeaders { get;  }
        object Data { get; set; }

        string Action { get; set; }
        string MessageId { get; set; }
        string ReplyAction { get; set; }
        bool IsResponse { get; set; }

       
    }
}