using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Request_TelegramModel
{
    public class Sticker
    {
        public string file_id;
        public int width;
        public int height;
        public PhotoSize thumb;
        public int file_size;
    }
}
