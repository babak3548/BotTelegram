using AnarSoft.Utility.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataModelTelegram.Request_TelegramModel
{
    [DataContract]
    public class ListUpdate
    {
        [DataMember]
        public bool ok;
        [DataMember]
        public List<Update> result;

        public string getString()
        {
            return JsonSerializer.Serialize(this);
        }
        public static ListUpdate GetMessage(string jsonString)
        {
            return JsonSerializer.DeSerialize<ListUpdate>(jsonString);
        }
    }


}
