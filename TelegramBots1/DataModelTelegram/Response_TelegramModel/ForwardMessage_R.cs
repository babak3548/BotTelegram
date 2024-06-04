using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Response_TelegramModel
{
    public class ForwardMessage_R
    {
        public int chat_id;//Integer	Yes	Unique identifier for the message recipient — User or GroupChat id
        public int from_chat_id;//Integer	Yes	Unique identifier for the chat where the original message was sent — User or GroupChat id
        public int message_id;//Integer	Yes	Unique message identifier
    }
}
