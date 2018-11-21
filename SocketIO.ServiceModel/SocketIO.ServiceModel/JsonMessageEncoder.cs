using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketIO.ServiceModel
{
    public class JsonMessageEncoder : MessageEncoder
    {
        public override IMessage DecodeMessage(object payload)
        {
            var m = CreateMessage();
            JObject obj = payload as JObject;
            var header = obj["h"];
            if(header != null)
            {
                foreach (var item in header)
                {
                    JProperty pop = item as JProperty;
                    if (pop != null)
                        m.MessageHeaders[pop.Name] = pop.Value.ToObject<string>();
                }
            }
            var data = obj["d"];
            m.Data = data;
            return m;
        }

        public override object EncodeMessage(IMessage message)
        {
            JObject json = new JObject();
            var header = new JObject();
            json["h"] = header;
            foreach (var kp in message.MessageHeaders)
            {
                header[kp.Key] = kp.Value;
            }
            if(message.Data != null)
                json["d"] = JToken.FromObject(message.Data);
            return json;
        } 
    }
}