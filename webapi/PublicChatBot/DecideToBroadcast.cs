using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnarSoft.Utility.Utilities;
using System.Threading;

namespace PublicChatBot
{
    public class DecideToBroadcast : BaseService
    {
        static bool flag = true;
        BotModel botModel;
        UserService userService;
        ResponseCreator responseCreator;
        public static bool getFlagDecide()
        {
            return flag;
        }
        internal DecideToBroadcast(BotModel botModel)
        {
            this.botModel = botModel;
            userService = new UserService(botModel);
            responseCreator = new ResponseCreator(botModel);
        }
        /// <summary>
        /// بررسی می کند در صورتی که زمان مناسب ارسال باشد چندین کانتنت برتر را انتخاب و ارسال می کند
        /// </summary>
        internal void DecideSendTime()
        {
            if (flag)
            {
                flag = false;
                //اگر زمان آخرین ارسال ست نشده بود این زمان را ست می کند
                if (botModel.lastSentHours == 0)
                {
                    setlastSentTime();
                }
                //یعنی هر یک ساعت یک ارسال و در بازه زمانی سیزده ساعت بیداری 
                if (9 < Setting.getHoursInDay() && Setting.getHoursInDay() < 24 && botModel.lastSentHours < Setting.getHours())
                {
                    setlastSentTime();
                    int howCountTopToSend = botModel.maxSendInDay / 14; //عدد سیزده به خاطر سیزده ساعت ارسال در روز می باشد
                    manageSentTopContent(howCountTopToSend);
                }
                flag = true;
            }
        }
        /// <summary>
        /// لیست محتوا ها را براساس رنکینگ امتیازی آنها می چیند
        /// </summary>
        /// <returns></returns>
        private IQueryable<content> getTopRankingContent()
        {
            int countAccept = countUserToDecide();
            return dbContext.contents.Where(c => c.fk_bot == botModel.botName && c.isBroadcast == (int)EnumIsBroadcast.noDetermin &&
                    (c.ratePlus - c.rateMinus) > countAccept).OrderByDescending(c => (c.ratePlus + c.rateMinus) / (c.ratePlus - c.rateMinus));

        }

        /// <summary>
        /// تعداد کاربری که به یک محتوا باید رای داده باشند تا محتوای مورد نظر به سیستم انتخاب بهترین ها وارد شود
        /// </summary>
        /// <returns></returns>
        private int countUserToDecide()
        {
            //Setting.persentAcceptUser
            int countUser = userService.getCountUser();
            if (countUser < 40)
            {
                return 1;//حدا قل سه نفر
            }
            else if (countUser < 500)
            {
                return 1;//حدا قل 4 نفر
            }
            else if (countUser < 1000)
            {
                return 2; //حدا قل 10 نفر
            }
            else if (countUser < 5000)
            {
                return 2; //حدا قل 5 نفر
            }
            else if (countUser < 10000)
            {
                return 4;
            }
            else if (countUser < 100000)
            {
                return 5;
            }
            else
            {
                return 7;
            }
        }


        private void setlastSentTime()
        {
            botModel.lastSentHours = Setting.getHours();
        }
        private void manageSentTopContent(int howCountTopToSend)
        {
            List<SendContent> sendContentListObject = new List<SendContent>();
            int counter = 0;
            //
            foreach (content _content in getTopRankingContent())
            {
                content _c = new content();
                _c = _content;
                if (veto(_content) && counter < howCountTopToSend)
                {
                    sendContentListObject.Add(new SendContent { userId = _content.fk_user, messageId = _content.messageId });
                    _content.isBroadcast = (byte)EnumIsBroadcast.yse;
                }
                else if (_content.ratePlus+_content.rateMinus >= -1 && counter < 50)//
                {
                    // nothing یه فرصت دوباره داده می شود
                }
                else
                {
                    _content.isBroadcast = (byte)EnumIsBroadcast.no;
                }
                counter++;
            }
            dbContext.SaveChanges();
            sendContentToAll(sendContentListObject);
        }

        private static bool veto(content _content)
        {
            return (_content.ratePlus + _content.rateMinus) > 0;
        }


        private void sendContentToAll(List<SendContent> sendContentListObject)
        {
            Bot bot = dbContext.Bots.FirstOrDefault(b => b.userName == botModel.botName);
            UserService userService = new UserService(botModel);
            IQueryable<Bridge_BotUser> IQ_bridges = userService.getBridge_BotUser_Bot(botModel.botName);
            int countUser = IQ_bridges.Count();
            countUser = countUser <= 0 ? 1 : countUser;
            int countSend = countUser * sendContentListObject.Count;
            int sleepTime = (int)((60 * (60-2) * 1000) / countSend);//منهای دو به خاطر زمان تلف شده در پردازش در نظر گرفته شده
            if (sleepTime < (1000/25))//هزار بر بیست و پنج به خاطر قانون تلگرام در ارسال هر یک ثانیه هست
            {
                sleepTime = (1000 / 25);
            }
            foreach (var sc in sendContentListObject)
            {
                foreach (var bridge_BotUser in IQ_bridges)
                {
                    responseCreator.ForwardMessage(bridge_BotUser.fk_user, sc.userId, sc.messageId);
                    Thread.Sleep(sleepTime); 
                }
            }
        }


    }
}
