using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Request_TelegramModel
{
   public class Video
    {
       public string file_id;
       public int width;
       public int height;
       public int duration;
       public PhotoSize thumb;
       public string mime_type;
       public int file_size;
       public string caption;
    }
}
