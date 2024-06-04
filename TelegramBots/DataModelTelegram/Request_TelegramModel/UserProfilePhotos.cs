using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Request_TelegramModel
{
    /// <summary>
    /// This object represent a user's profile pictures.
    /// </summary>
   public class UserProfilePhotos
    {
       public int total_count;
       /// <summary>
       /// Requested profile pictures (in up to 4 sizes each)
       /// </summary>
       public PhotoSize[] photos; 
    }
}
