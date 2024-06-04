using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Request_TelegramModel
{
    /// <summary>
    /// This object represents an incoming update.
    /// </summary>
   public class Update
    {
       /// <summary>
        /// The update‘s unique identifier. Update identifiers start from a certain positive number and increase sequentially. 
        /// This ID becomes especially handy if you’re using Webhooks, since it allows you to ignore repeated updates or to 
        /// restore the correct update sequence, should they get out of order.
       /// </summary>
       public int update_id;
       /// <summary>
       /// Optional. New incoming message of any kind — text, photo, sticker, etc.
       /// </summary>
       public Message message;
    }
}
