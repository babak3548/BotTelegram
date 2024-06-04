using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicChatBot
{
  public  class UsersOnline : List<UserOnLine>
    {
      string botName;
      public UsersOnline(string botName)
        {
            this.botName = botName;
        }
      /// <summary>
      /// یه کاربر  به  لیست اضافه کن و اگر کاربر ثبت نشده بود انو ثبت کن
      /// </summary>
      /// <param name="UserId"></param>
      void addUser(int UserId){}
      void regUser(Telegram.Bot.Types.User user){}
      void delIdelUser(Telegram.Bot.Types.User user) { }
      void fetchFromDbUser(int UserId) { }

      void getThisListUser(int UserId) { }
      public void FetchUserAndState(Telegram.Bot.Types.User user, string command) {
          getThisListUser(1);
          fetchFromDbUser(1);
          regUser(new Telegram.Bot.Types.User());
          addUser(1);
          fetchFromDbUser(1);
      }


    }
}
