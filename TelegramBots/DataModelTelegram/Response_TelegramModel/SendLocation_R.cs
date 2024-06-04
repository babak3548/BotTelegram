using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Response_TelegramModel
{
    public class SendLocation_R
    {
        public int chat_id;//  Integer	Yes	Unique identifier for the message recipient — User or GroupChat id
        public int latitude;//  Float number	Yes	Latitude of location
        public int longitude;//  Float number	Yes	Longitude of location
        public int reply_to_message_id;//  Integer	Optional	If the message is a reply, ID of the original message
        public int reply_markup;//  ReplyKeyboardMarkup or ReplyKeyboardHide or ForceReply	Optional	Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.
    }                        
}
