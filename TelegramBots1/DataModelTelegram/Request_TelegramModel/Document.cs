using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Request_TelegramModel
{
    public class Document
    {
        public string file_id;
        public PhotoSize thumb;
        public string file_name;
        public string mime_type;
        public int file_size;
    }
}
