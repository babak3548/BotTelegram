using AnarSoft.Utility.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataModelTelegram.Response_TelegramModel
{
    [DataContract]
    public class GetUpdates_R
    {
        [DataMember]
        public int offset;//Integer	Optional	Identifier of the first update to be returned. Must be greater by one than the highest among the identifiers of previously received updates. By default, updates starting with the earliest unconfirmed update are returned. An update is considered confirmed as soon as getUpdates is called with an offset higher than its update_id.
        [DataMember]
        public int limit;//Integer	Optional	Limits the number of updates to be retrieved. Values between 1—100 are accepted. Defaults to 100
        [DataMember]
        public int timeout;//Integer	Optional	Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling
        public string getString()
        {
            return JsonSerializer.Serialize(this);
        }
        public static SendMessage_R GetMessagePacket(string jsonString)
        {
            return JsonSerializer.DeSerialize<SendMessage_R>(jsonString);
        }
    }
}
