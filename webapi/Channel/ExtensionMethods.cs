using Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
   public static class ExtensionMethods
    {
       public static string GetLinkId(this string value)
       {
          return value.Replace("@", "").Replace(Setting.TelegramJoinChatUrl, "")
                        .Replace("http://telegram.me/", "").Replace("https://telegram.me/", "")
                        .Replace("Telegram.me/", "").Replace("TELEGRAM.ME/", "")
                        .Replace("telegram.me/", "").Replace("Telegram.me/", "")
                        .Replace("telegram.me", "").Replace("Telegram.me", "")
                        .Replace("Https://", "").Replace("Http://", "")
                        .Replace("Https://", "").Replace("Httos://", "")
                        .Replace("Http://", "").Replace("Https://", "")
                        .Replace("http://", "").Replace("https://", "")
                       .ToLower();
       }

        public static string GetName(this string value) {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                    return value;
                if (value.Contains("<span"))
                    value = value.Remove(value.IndexOf("<span"), value.IndexOf(">")- value.IndexOf("<span") + 1);
                if (value.Contains("span>"))
                    value = value.Remove(value.IndexOf("span>"), value.IndexOf(">")- value.IndexOf("span>") + 1);
                return value.TrimStart(':').TrimEnd(':').Replace("ى", "ی").Replace("NULL","");
            }
            catch (Exception ex)
            {
                return value;
            }
                //<span title="couple_with_heart" class="emoji  emoji-spritesheet-0" style="background-position: -414px -72px;">:couple_with_heart:
        }
        public static string GetDescription(this string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                    return value;
                return value.TrimStart(':').TrimEnd(':').Replace("ى", "ی").Replace("NULL", "");
            }
            catch (Exception ex)
            {
                return value;
            }
            //<span title="couple_with_heart" class="emoji  emoji-spritesheet-0" style="background-position: -414px -72px;">:couple_with_heart:
        }
        public static string GetMember(this int? value)
        {
            if (value == null)
                return "unknown";
            if (value < 1000)
                return "<1k";
      
                return  "~"+(int)value/1000+"k" ;

        }

    }
}
