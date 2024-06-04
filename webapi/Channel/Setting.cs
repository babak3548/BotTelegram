using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Channel
{
   public class Setting
    {
     
       public static string urlWebMethod = @"https://api.telegram.org/bot";
       public static string urlTelegramMe = @"https://telegram.me/";
       public static string TelegramJoinChatUrl = @"https://telegram.me/joinchat/";
      public static string channelLink { get { return "https://telegram.me/ChannelsStoreBot"; } }
       // public static string channelLink { get { return "https://telegram.me/Vargas_bot"; } }
       public static string logFileName
       {
           get
           {
               return WebConfigurationManager.AppSettings["logPath"].ToString();
           }
       }

        public static string No_search_result = "کانالی مطابق با کلمه مورد جستجوی شما یافت نشد";

        public static string send_url_to_your_friends = "در این ربات لیست کانال های بر اساس گروه بندی قابل دسترس هستند ";
        public static string waiting_send_content="منتظر بمانید";
        public static string main_command_message="برای ادامه یکی از دستورهای زیر اجرا فرمایید";
        public static string this_channel_already_added="این کانال از قبل اضافه شده است";


        public static string select_group = "یکی از گروهای زیرا انتخاب نمایید";

        public static string sucess_message = "در خواست شما کامل شد";
        public static string select_options="یکی از گزینه های زیرا انتخاب نمایید";

        public static string enter_name_channel = "نام کانال را وارد نمایید";
        public static string enter_search_value = "کلمه مورد جستجو را وارد نمایید";
        //enter_search_value

        public static string enter_link = "لینک کانال را به صورت مثال وارد نمایید"+"\r\n"+"@sample";

        public static string aboutMe = "کانال یاب ،جستجوگر حرفه ای کانال های فارسی با دسته بندی دقیق محتوایی، کاملترین مرجع انواع کانالهای  " +
            "غیر تبلیغاتی است  " + "\r\n" + channelLink;
          //  "در این ربات لیست کانالها، بر اساس گروه بندی و امتیاز کاربران در دسترس می باشد  "+
           // "همچنین می توانید کانال های دلخواه خود را به ربات اضافه نمایید";

        public static string now_select_rate_channel = "حالا ستاره مورد نظر را برای کانال، انتخاب کنید";
        public static string No_exist_channel="کانالی با این لینک وجود ندارد";
        public static string Send_descroption="توضیحاتی در مورد کانال ارسال نمایید";
      

        public static string fail_message = "عملیات ناموفق بود";

        public static string to_Continue_select_Command = "برای ادامه یکی از دستورات زیرا انتخاب نمایید";

        public static string no_more_result = "\r\n"+"نتایج یافت شده ارسال شد";
        public static string you_add_rate_after="شما قبلا به این کانال امتیاز داده اید";

        public static string register_complete = "کانال با موفقیت ثبت شد بعد از بررسی توسط آدمین در لیست کانال ها قرار خواهد گرفت";

       // public static string get_link_rate = "با لینک زیر می توانید از کاربران خود درخواست نمایید به کانال شما امتیاز اضافه نمایند";
        public static string save_star = "به {0} امتیاز دهید،برای اینکار وارد لینک شوید و دکمه استارت را بزنید و به کانال امتیاز دهید  ";
        public static string help = "پرطرفدارترین ها: بر اساس تعداد اعضا کاربران کانالها را مرتب می کند"+
            "محبوب ترینها: بر اساس تعداد ستاره که کاربران به کانال اختصاص داده اند کانال ها را مرتب می کند"+
            "جدیدترینها: آخرین کانالها را لیست می کند";

    }
}
