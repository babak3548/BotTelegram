using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PublicChatBot
{
 public   class Setting
    {
        public static int logGetUpdates = 0;
        public const int logGetUpdatesCount = 1;

        public static int logEnterRequest = 0;
        public const int logEnterRequestCount = 1;

        public static int logParsCommand = 0;
        public const int logParsCommandCount = 1;

        public static int logNoDeterminCommand = 0;
        public const int logNoDeterminCommandCount = 10;

        public static int rateShear = 1000;
        static readonly object _object = new object();
        static readonly object _object1 = new object();
     public static int maxCommandStateListCount=1000;


     internal static void log(string msg, int maxValue, ref int value)
     {
         lock (_object) {
             if (value > maxValue)
             {
                 System.IO.File.AppendAllText(DateTime.Now.ToString("yyyy-MM-dd") +Setting.logFileName, DateTime.Now.ToString() + " : " + msg + "error falt:" + value + "\r\n");
                 value = 0;
             }
             else
             {
                 value++;
             }
         }

     }

     internal static bool logStatusSystem(string msg, int maxValue,  int value)
     {
         lock (_object1) {
             if (value > maxValue)
             {
                 System.IO.File.AppendAllText(DateTime.Now.ToString("yyyy-MM-dd")+Setting.logFileName1, DateTime.Now.ToString() + " : " + msg + "error falt:" + value + "\r\n");
                 return true;
             }
             else
             {
                 return false;
             }
         }
     }

     internal static Int64 getDateTimeNowInMil()
     {
         return Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
             //(int) (DateTime.Now - (new DateTime(1, 1, 1, 0, 0, 0))).TotalMilliseconds;//???
     }


     internal static int getHours()
     {
         return (int) (DateTime.Now - (new DateTime(1970, 1, 1, 0, 0, 0))).TotalHours;
     }
     internal static int getHoursInDay()
     {
         //DateTime.Now.TimeOfDay.Hours
         return DateTime.Now.Hour;//???
     }
        public static string urlWebMethod = @"https://api.telegram.org/bot";
        public static string urlTelegramMe = @"https://telegram.me/";
        public static string logFileName = "log.txt";
        public static string logFileName1 = "log_status.txt";
        public static int x { get { return 1; } }
        public static string you_allready_added 
        { get {
            return "ثبت نام شما با موفقیت انجام شد";
          //  return "you all ready added";
        } } 
     public static string you_add_to_public_chat { 
         get {
             return "ثبت نام شما انجام شد";
             //return "you add to public cha";
         } }
     public static string incorrect_Command { get {
         return "دستور اشتباه می باشد";
      //   return "incorrect Command";
     } }
     public static string change_role_user { get {
         return "رول کاربر مورد نظر را تغییر دهید";
        // return "change_role_user"; 
     } }
     public static string remove_user { get {
         return "کاربر حذف شد";
         //return "remove user"; 
     } }
     public static string main_command_message { get {
         return "تشکر برای ادامه یک دستور وارد کنید";
         //return "for continue send me a command";
     } }
     public static string waiting_send_content { get {
         return "منتظر ورود ...";
         //return "waiting for content"; 
     } }
     public static string send_url_to_your_friends { get {
         return "آدرس زیر را به دوستان یا گروهتان بفرستین و اونها با کلیک بر روی آدرس وارد پابلیک چت می شن" + "\r\n";
             //"برای اشتراک گذاری، این آدرس را به دوستان تان ارسال فرمایید و 1000 امتیاز دریافت نمایید" + "\r\n";
         //return "send url to your friends (its increase your rate 1000 plus) \r\n";
     } }
     public static string your_rate { get {
         return "امتیاز شما";
         //return "your rate "; 
     } }
     public static string please_send_me_new_conten { get {
         return "با استفاده از کلید add مطالب خود را به اشتراک بگذارید";
         //   return "sorry no new cotent please add new content";
     } }
     public static string flowers { get {
         return "تعداد اعضاء";
         //return "flowers";
     } }
     public static string aboutMe { get {
         return "من یه روبات هستم که بهترین محتواهای شما را انتخاب و به همه دوستان ارسال می کنم ";
         //return "I am a robat";
     } }  
    

    }
}
