using AnarSoft.Utility.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataModelTelegram.Request_TelegramModel
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public int message_id;
        [DataMember]
        public User from;
        [DataMember]
        public int date;
        [DataMember]
        public Chat chat;//User or GroupChat	Conversation the message belongs to — user in case of a private message, GroupChat in case of a group
        [DataMember]
        public User forward_from;
        [DataMember]
        public string forward_date;
        [DataMember]
        public Message reply_to_message;//مسیج ریشه ای که رپلی شده
        [DataMember]
        public string text;
        [DataMember]
        public Audio audio;
        [DataMember]
        public Document document;
        [DataMember]
        public PhotoSize[] photo;//Optional. Message is a photo, available sizes of the photo
        [DataMember]
        public Sticker sticker;
        [DataMember]
        public Video video;
        [DataMember]
        public Contact contact;//Optional. Message is a shared contact, information about the contact
        [DataMember]
        public Location location;//Optional. Message is a shared location, information about the location
        [DataMember]
        public User new_chat_participant;//Optional. A new member was added to the group, information about them (this member may be bot itself)
        [DataMember]
        public User left_chat_participant;//Optional. A member was removed from the group, information about them (this member may be bot itself)
        [DataMember]
        public string new_chat_title;//Optional. A group title was changed to this value
        [DataMember]
        public PhotoSize[] new_chat_photo;//Optional. A group photo was change to this value
        [DataMember]
        public bool delete_chat_photo;//Optional. Informs that the group photo was deleted
        [DataMember]
        public bool group_chat_created;//Optional. Informs that the group has been created

        public string getString()
        {
            return JsonSerializer.Serialize(this);
        }
        public static Message GetMessage(string jsonString)
        {
            return JsonSerializer.DeSerialize<Message>(jsonString);
        }
    }



}
