using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Response_TelegramModel
{
   public  class SendChatAction_R
    {
    public int chat_id	;//Integer	Yes	Unique identifier for the message recipient — User or GroupChat id
    public string action	;//String	Yes	Type of action to broadcast. Choose one, depending on what the user is about to receive: typing for text messages, upload_photo for photos, record_video or upload_video for videos, record_audio or upload_audio for audio files, upload_document for general files, find_location for location data.
    }
}
