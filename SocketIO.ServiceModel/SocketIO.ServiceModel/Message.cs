using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketIO.ServiceModel
{
    public class Message : IMessage
    {
        public Dictionary<string, string> MessageHeaders { get; private set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public object Data { get; set; }


        private void SetProperty(string v, string value)
        {
            this.MessageHeaders[v] = value;
        }

        private string GetProperty(string v)
        {
            string ret;
            MessageHeaders.TryGetValue(v, out ret);
            return ret;
        }


        public string Action {  get { return GetProperty("action"); } set { SetProperty("action",value); } }


        public bool IsResponse
        {
            get
            {
                return string.Compare("res", GetProperty("type") ?? string.Empty, true) == 0;
            }

            set
            {
                SetProperty("type", value ? "res" : "req");
            }
        }

        
        public string MessageId { get { return GetProperty("id"); } set { SetProperty("id", value); } }

        public string ReplyAction { get { return GetProperty("reply"); } set { SetProperty("reply", value); } }
    }
}