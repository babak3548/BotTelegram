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

        BotModel botModel;
        UserService userService;
        ResponseCreator responseCreator;
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
            //اگر زمان آخرین ارسال ست نشده بود این زمان را ست می کند
            if (botModel.lastSentHours == 0)
            {
                setlastSentTime();
            }
            //یعنی هر یک ساعت یک ارسال و در بازه زمانی سیزده ساعت بیداری 
            if (10 < Setting.getHoursInDay() && Setting.getHoursInDay() < 24 && botModel.lastSentHours < Setting.getHours())
            {
                setlastSentTime();
                int howCountTopToSend = botModel.maxSendInDay / 13; //عدد سیزده به خاطر سیزده ساعت ارسال در روز می باشد
                manageSentTopContent(howCountTopToSend);
               
            }

        }
        /// <summary>
        /// لیست محتوا ها را براساس رنکینگ امتیازی آنها می چیند
        /// </summary>
        /// <returns></returns>
        private IQueryable<content> getTopRankingContent()
        {
            int countAccept = countUserToDecide();
            return dbContext.DbWriteble.contents.Where(c => c.fk_bot == botModel.botName && c.isBroadcast == (int)EnumIsBroadcast.noDetermin &&
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
                else if (veto(_content) && counter < 4 * howCountTopToSend)
                {
                    // nothing یه فرصت دوباره داده می شود
                }
                else
                {
                    _content.isBroadcast = (byte)EnumIsBroadcast.no;
                }
                counter++;
            }
            dbContext.DbWriteble.SaveChanges();
            sendContentToAll(sendContentListObject);
        }

        private static bool veto(content _content)
        {
            return (_content.ratePlus + _content.rateMinus) > 0;
        }


        private void sendContentToAll(List<SendContent> sendContentListObject)
        {
            Bot bot = dbContext.DbWriteble.Bots.FirstOrDefault(b => b.userName == botModel.botName);
            UserService userService = new UserService(botModel);
            IQueryable<Bridge_BotUser> IQ_bridges = userService.getBridge_BotUser_Bot(botModel.botName);
            int countUser = IQ_bridges.Count();
            countUser = countUser <= 0 ? 1 : countUser;
            int sleepTime = (int)((50 * 60 * 1000) / countUser);
            foreach (var bridge_BotUser in IQ_bridges)
            {
                foreach (var sc in sendContentListObject)
                {
                    responseCreator.ForwardMessage(bridge_BotUser.fk_user, sc.userId, sc.messageId);
                }
                //یه فاصله بین ارسال دو تا یوز می دهد
                Thread.Sleep(sleepTime);
            }
        }


    }
}
