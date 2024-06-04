using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Response_TelegramModel
{
    public class SendSticker_R
    {
        public int chat_id;// Integer	Yes	Unique identifier for the message recipient — User or GroupChat id
        public string sticker;// InputFile or String	Yes	Sticker to send. You can either pass a file_id as String to resend a sticker that is already on the Telegram servers, or upload a new sticker using multipart/form-data.
        public int reply_to_message_id;//Integer	Optional	If the message is a reply, ID of the original message
        public string reply_markup;// ReplyKeyboardMarkup or ReplyKeyboardHide or ForceReply	Optional	Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.
    }
}
