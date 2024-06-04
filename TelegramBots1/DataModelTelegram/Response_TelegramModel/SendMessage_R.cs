using AnarSoft.Utility.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataModelTelegram.Response_TelegramModel
{
    [DataContract]
    public class SendMessage_R
    {
        [DataMember]
        public int chat_id;	//Yes	Unique identifier for the message recipient — User or GroupChat id
        [DataMember]
        public string text;	//Yes	Text of the message to be sent
        [DataMember]
        public bool disable_web_page_preview;	//	Optional	Disables link previews for links in this message
        [DataMember]
        public int reply_to_message_id;//Integer	Optional	If the message is a reply, ID of the original message
        [DataMember]
        public string reply_markup;//ReplyKeyboardMarkup or ReplyKeyboardHide or ForceReply	Optional	Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.
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
