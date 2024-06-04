using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using System.Web.Configuration;
 
namespace Channel
{
    internal class ResponseCreator 
    {
   
        Telegram.Bot.Api api;
        ReplyKeyboardMarkup replyKeyMrk;
         static string Token
        {
            get
            {
           // return "110092488:AAEDGQ9FBbdHDQXZcr8XljGiCMfsosTUFAo";
               return "164899142:AAHnK81CDbtG0BhxhxrBnFPlNy9AB17h9qs";//to do
              //  return WebConfigurationManager.AppSettings["ChannelBotToken"].ToString();
            }
        }
        public ResponseCreator()
        {
          
            api = new Telegram.Bot.Api(Token);
            replyKeyMrk = new ReplyKeyboardMarkup();
            replyKeyMrk.ResizeKeyboard = true;
        }


        // void ForwardMessage(int to_chatId, int from_chatId, int MessageId)
        //{
        //    var x =  api.ForwardMessage(to_chatId, from_chatId, MessageId);
        //}

        //  void sendChatAction(int to_chatId)
        //{
        //    api.SendChatAction(to_chatId, ChatAction.Typing);
        //}
        // void sendTextMessage(int chatId, string message)
        //{
        //    api.SendTextMessage(chatId, message, false, 0, getMainCommandReplay());
        //}
        void sendTextMessage(int chatId, string message, ReplyMarkup replyMarkup)
        {
            // var x=  api.SendTextMessage(chatId, message, false, 0, replyMarkup);
            api.SendTextMessage(chatId, message, false, 0, replyMarkup);
        }
        //internal void invaiteToFirend(int chatId)
        //{
        //    api.SendTextMessage(chatId, Setting.send_url_to_your_friends + Setting.urlTelegramMe + botModel.botName + "?start=" + chatId);
        //}

        //internal void mainCommand(int chatId)
        //{
        //    api.SendTextMessage(chatId, Setting.main_command_message, false, 0, getMainCommandReplay());
        //}

        // void addContent(int chatId)
        //{
        //    api.SendTextMessage(chatId, Setting.waiting_send_content);
        //}

      /*  internal ReplyMarkup getPlusMinesReplay(string contentId)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup();

            string[][] keyboard = new string[][] { new string[] { Command.plus + " " + contentId, Command.minus + " " + contentId } };
            replyKeyboardMarkup.Keyboard = keyboard;
            return replyKeyboardMarkup;
        }*/

         ReplyKeyboardMarkup getMainCommandReplay()
        {
           

            string[][] keyboard = new string[][] 
            { 
              new string[] { CommandManager.byCategory , CommandManager.topMember } ,
              new string[] { CommandManager.recent, CommandManager.TopStars},
              new string[] { CommandManager.addChannel , CommandManager.addReview},
              new string[] { CommandManager.aboutMe,CommandManager.search },
              new string[] {CommandManager.getLinkRate}
            };
            replyKeyMrk.Keyboard = keyboard;
            return replyKeyMrk;
        }



        internal void sendCategories(int chatId)
        {
            sendTextMessage(chatId, Setting.select_group, getReplyKeyCat());
        }

        private ReplyKeyboardMarkup getReplyKeyCat()
        {
            //         var _catMng= new CategoryManager();
            //         List<Group> groups = _catMng.GetGroups();
            //         string[][] keyboard = new string[(groups.Count/2)+1][];
            //         int i = 0;
            //         int j = 0;
            //         while ( i < groups.Count)
            //{
            //             keyboard[j] = new string[] { groups.ElementAt(i).Name, groups.ElementAt(i + 1).Name };
            //            i += 2;
            //            j++;
            //}
            //         keyboard[j] = new string[] { CommandManager.mainMenu };
            //         ReplyKeyboardMarkup replyKeyMrk= new ReplyKeyboardMarkup();
            //         replyKeyMrk.Keyboard = keyboard;
            //         return replyKeyMrk;

////            Id Name
////1   سرگرمی 😹
////2   ورزشی 🏊
////3   خبری 📡
////4   آموزشی 💡
////5   سلامت 🔬
////6   گردشگری 🚀
////7   فرهنگی مذهبی 👣
////8   تجاری 🎊
////9   سایر
            var _catMng = new CategoryManager();
            List<Group> groups = _catMng.GetGroups();
            string[][] keyboard = new string[][]
{
              new string[] { groups.ElementAt(2).Name, groups.ElementAt(0).Name } ,
              new string[] { groups.ElementAt(6).Name, groups.ElementAt(1).Name},
              new string[] { groups.ElementAt(4).Name, groups.ElementAt(3).Name},
              new string[] { groups.ElementAt(7).Name,groups.ElementAt(5).Name },
              new string[] {  CommandManager.mainMenu,groups.ElementAt(8).Name }
};
            replyKeyMrk.Keyboard = keyboard;
            return replyKeyMrk;
        }



        internal void SucessMessage(int chatId)
        {
            sendTextMessage(chatId, Setting.sucess_message, getMainCommandReplay());
        }

        internal void ShowOrderCommand(int chatId)
        {
             string[][] keyboard = new string[][] {
                  new string[]{ CommandManager.TopStars,CommandManager.topMember} ,
                  new string[]{CommandManager.mainMenu,CommandManager.recent},
             };
            replyKeyMrk.Keyboard=keyboard;
           
            sendTextMessage(chatId, Setting.select_options, replyKeyMrk);
        }

 

        internal void sendChannels(List<Channel> channels,int chatId)
        {
                  string[][] keyboard = new string[][] {
                  new string[]{ CommandManager.showMore,CommandManager.mainMenu} 
             };
            replyKeyMrk.Keyboard=keyboard;

            string msg = createStringChannels(channels);
            if (channels.Count < ChannelManager.PageSize)
            {
                msg= string.Format("{0}{1} 🆔@ChannelsStoreBot 📣{2}", msg ,Setting.no_more_result, (ChannelManager.channelsCount+1000).GetMember());
                sendTextMessage(chatId, msg, getMainCommandReplay());
            }
            else
            {
                sendTextMessage(chatId, string.Format("{0} 🆔@ChannelsStoreBot 📣{1}", msg, (ChannelManager.channelsCount+1000).GetMember()), replyKeyMrk);
            }
           
        }
        //internal void sendSearchChannels(List<Channel> channels, int chatId)
        //{
        //    string msg = "";
        //    if (channels != null && channels.Count > 0)
        //        msg = createStringChannels(channels);
        //    else
        //        msg = Setting.No_search_result;

        //           sendTextMessage(chatId, msg, getMainCommandReplay());
        //}
        internal void sendWait(int chatId)
        {
            api.SendChatAction(chatId, ChatAction.Typing);
         }
        private static string createStringChannels(List<Channel> channels)
        {
            try
            {
                string msg = "";
                string stars = "";
                double ave = 0;
                foreach (var channel in channels)
                {
                    stars = "";
                    ave = 0;
                    ave = channel.User_Channel.Count > 0 ? channel.User_Channel.Average(uc => uc.Star) : 0;
                    for (int i = 0; i < Math.Ceiling(ave); i++)
                    {
                        stars += "⭐";
                    }

                    msg += string.Format("\r\n@{0} 👥{1}{2}{3}{4}{5}\r\n ........................................................"
                        , channel.Link, channel.Member.GetMember(),channel.ShowOrder>0? " ✨" : "", string.IsNullOrWhiteSpace(channel.Name) ? "" : ("\r\n" + channel.Name)
                        , string.IsNullOrWhiteSpace(channel.Description) ? "" : "\r\n" + (channel.Description)
                        , stars != "" ? "\r\n" + stars : "", channel.User_Channel.Count > 0 ? channel.User_Channel.Count.ToString() : "");
                }
                return msg;
            }
            catch (Exception )
            {
                throw new Exception("خطا در قسمت ایجاد استرینق کانال");
              
            }

        }

        internal void ReqChannelName(int chatId)
        {
            sendTextMessage(chatId, Setting.enter_name_channel, new ReplyKeyboardHide());
        }

        internal void ReqChannelLink(int chatId)
        {
            string[][] keyboard = new string[][] { };
            replyKeyMrk.Keyboard = keyboard;
            sendTextMessage(chatId, Setting.enter_link, replyKeyMrk);
        }

        internal void SendAboutMe(int chatId)
        {
            sendTextMessage(chatId, Setting.aboutMe, getMainCommandReplay());
           // api.SendTextMessage()
        }

        internal void ShowOptionRate(int chatId)
        {
            //پیغام شما به این کانال چچند ستار ه می دهید
            //کلید بر اساس ستاره
            string[][] keyboard = new string[][] {
                  new string[]{ CommandManager.star1,CommandManager.star2} ,
                  new string[]{ CommandManager.star3,CommandManager.star4},
                  new string[]{CommandManager.mainMenu, CommandManager.star5}
             };
            replyKeyMrk.Keyboard=keyboard;
            sendTextMessage(chatId, Setting.now_select_rate_channel, replyKeyMrk);
        }

        internal void SendNoExistChannel(int chatId)
        {
            sendTextMessage(chatId, Setting.No_exist_channel, getMainCommandReplay());
        }



        internal void ChannelDesReq(int chatId)
        {
            sendTextMessage(chatId, Setting.Send_descroption, new ReplyKeyboardHide());
        }

        internal void ShowMessageWithMainCommand(string text,int chatId)
        {
            sendTextMessage(chatId, text, getMainCommandReplay());
        }

        internal void FailMessage(int chatId)
        {
            sendTextMessage(chatId, Setting.fail_message, getMainCommandReplay());
        }



        internal void ReqSearchCal(int chatId)
        {
            sendTextMessage(chatId, Setting.enter_search_value, new ReplyKeyboardHide());
        }

        internal void SendRateLink(int chatId,Channel channel)
        {
            string rateLink =string.Format( Setting.save_star, channel.Link)+ "\r\n" +Setting.channelLink+ @"?start=" + channel.Link;
            sendTextMessage(chatId, rateLink, getMainCommandReplay());
          //  "@" + channel.Link + "\r\n" + channel.Description + "\r\nrate:"
        }

        
    }
}
