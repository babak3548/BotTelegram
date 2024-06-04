using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Response_TelegramModel
{
    public class GetUserProfilePhotos_R
    {
        public int user_id;//Integer	Yes	Unique identifier of the target user
        public int offset;//Integer	Optional	Sequential number of the first photo to be returned. By default, all photos are returned.
        public int limit;//Integer	Optional	Limits the number of photos to be retrieved. Values between 1—100 are accepted. Defaults to 100.
    }
}
