//using DataModelTelegram.Request_TelegramModel;
//using DataModelTelegram.Response_TelegramModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PublicChatBot
{
    /// <summary>
    /// هر سرویس برای یک نوع بوت باید از این کلاس ارث بری نمایید 
    /// </summary>
    public class ManageRequest : BaseService, IBot
    {

        public static List<BotModel> botModelList = new List<BotModel>();

        int sleepTime = 0;

        BotModel botModel;
       // DbContextEntity db = new DbContextEntity();
        //  private int serviceQuestion = 2;//سرویس آینده
        #region IBot
        public ManageRequest(BotModel botModel)
        {
            this.botModel = botModel;
        }

        public void enterRequest(BotModel botModel, Update[] updateArray)
        {
            try
            {
                Update lastRecord = null;
                foreach (Telegram.Bot.Types.Update update in updateArray)
                {
                    lastRecord = update;
                    ThreadStart childWork = new ThreadStart(() => executeARequest(botModel, update));
                    Thread th = new Thread(childWork);
                    th.Start();//th2
                
                }
                if (lastRecord != null)
                {
                    botModel.lastUpdateId = lastRecord.Id;
                }
            }
            catch (Exception e)
            {
                string msg = "خطا در پردازش ریکست های بوت "+e.Message + "enterRequest";
                Setting.log(msg, Setting.logEnterRequestCount, ref Setting.logEnterRequest);
            }

        }
        //شروع پردازش روبات
        //این متد می تواند توسط ورژن وبی هم صدا زده شود
        public void executeARequest(BotModel botModel, Telegram.Bot.Types.Update update)
        {
            short role = AuthenticateUser(botModel.botName, update.Message);
            ParseCommand(botModel, role, update.Message);
        }



        public void getUpdates()
        {
            try
            {
                Telegram.Bot.Types.Update[] updateArray=null ;
                Api api = new Api(botModel.token);
                updateArray = api.GetUpdates(botModel.lastUpdateId + 1, 100).Result;
                   if (updateArray == null || updateArray.Count() == 0) 
                {
                    if (sleepTime < 1000)
                    {
                        sleepTime += 10;
                    }
                    else
                    {
                        sleepTime = 1000;
                    }
                    Thread.Sleep(sleepTime);
                   // botModel.nextFetchFromTelegram = Setting.getDateTimeNowInMil() + 2000;
                }
                else
                {
                    sleepTime = 0;
                    enterRequest(botModel, updateArray);
                }
               
            }
            catch (Exception e)
            {
                string msg =  "خطا در پردازش ریکست های بوت " + " getUpdates "+e.Message;
                Setting.log(msg, Setting.logGetUpdatesCount, ref Setting.logGetUpdates);
            }
        }
        public string testcDb (){
            try
            {
                return dbContext.DbWriteble.Bots.FirstOrDefault().userName;

            }
            catch (Exception e)
            {
                return e.InnerException.Message + e.Message;

            }
         
        }
        public IEnumerable<BotModel> listBotsService()
        {
            if (botModelList.Count > 0)
            {
                return botModelList;
            }
            else
            {
                loadBots();
                if (botModelList.Count <= 0)
                {
                    int x = 2;
                    string msg =  "***خطا  خطر ناک در لود روبات ها :لیست روبات ها لود نشد***" ;
                    Setting.log(msg, 1, ref x);
                }
                return botModelList;
            }
        }

        private void loadBots()
        {
            try
            {
                foreach (var bot in dbContext.DbWriteble.Bots)
                {
                    BotModel _botModel = new BotModel();
                    _botModel.botName = bot.userName;
                    _botModel.token = bot.token;
                    _botModel.lastUpdateId = bot.lastUpdateId;
                    _botModel.description=bot.description;
                    _botModel.maxSendInDay=bot.sendCountDay;
                    _botModel.nextFetchFromTelegram = Setting.getDateTimeNowInMil();
                    _botModel.lastSentHours = Setting.getHours();
                    //مقادیر از دیتابیس خوانده شود؟؟؟
                    //عدد کمتر مساوی 10 به معنی صفر درصد می باشد عدد 20 به معنی پنجاه درصد می باشد
                    _botModel.shanseErsaleMohtavayeTekrari = bot.shanseErsaleMohtavayeTekrari;
                    botModelList.Add(_botModel);
                }
 
            }
            catch (Exception e)
            {
               
            }
        }

        #endregion IBot

        /*   public Message DeserializeMessage(string jsonMessage)
        {
            return Message.GetMessage(jsonMessage);
            // throw new Exception();
        }*/
        public short AuthenticateUser(string botUserName, Message message_req)
        {
             DbContextEntity _dbContext = new DbContextEntity();
            Bridge_BotUser b_u = _dbContext.DbWriteble.Bridge_BotUser.FirstOrDefault(b => b.fk_bot == botUserName &&
                b.fk_user == message_req.From.Id);
            if (b_u != null)
            {
                return b_u.role;
            }
            else return Roles.noReg;
        }

        public virtual void ParseCommand(BotModel botModel, short role, Message message_req)
        {
            string command = "";
            //int contentId = -1;

            UserService userService = new UserService(botModel);
            Command objCommand = new Command();
            ContentService contentService = new ContentService(botModel);
            ResponseCreator responseCreator = new ResponseCreator(botModel);
            DecideToBroadcast decideToBroadcast = new DecideToBroadcast(botModel);
            try
            {
                command = objCommand.AuthCommand(message_req.Text, role);
                command = objCommand.getCommandByEffectState(botModel.botName, message_req.Chat.Id, command);

                //اضافه کردن کاربر دریافت کننده به یک پابلیک چت
                if (Command.start == command)
                {
                   string pram= objCommand.getParamCommand(command, message_req.Text);

                   userService.addNormalUser(botModel, message_req, pram);
                }
                else if (Command.addContent == command)
                {
                    objCommand.addCommandStateUser(botModel.botName, message_req.Chat.Id, command, StateAddContent.request.ToString());
                    responseCreator.addContent(message_req.Chat.Id);
                }
                else if (Command.acceptContent == command)
                {
                    contentService.acceptContent(botModel.botName, message_req);
                    //  contentService.sendContentToRandomUsers(botUserName, message_req, contentId);
                }
                else if (Command.nextContent == command)
                {
                  contentService.sendContent( message_req.Chat.Id);
                
                }
                else if (Command.topRate == command)
                {
                    userService.getTopRate(message_req.Chat.Id);
                }
                else if (Command.myRate == command)
                {
                    userService.getMyRate(message_req.Chat.Id);
                }
                else if (Command.flowers == command)
                {
                    userService.getFlowers( message_req.Chat.Id);
                }
                else if (Command.shearWithFirend == command)
                {
                    responseCreator.shearWithFirend(message_req.Chat.Id);
                }
                else if (Command.plus == command)
                {
                    int lastSendContentId = objCommand.getContentIdCommandState(botModel.botName, message_req.Chat.Id);
                    contentService.plusRate( message_req.Chat.Id, lastSendContentId);
                    decideToBroadcast.DecideSendTime();
                }
                else if (Command.minus == command)
                {
                    int lastSendContentId = objCommand.getContentIdCommandState(botModel.botName, message_req.Chat.Id);
                    contentService.MinusRate( message_req.Chat.Id, lastSendContentId);
                }
                else if (Command.mainCommand == command)
                {
                    responseCreator.mainCommand(message_req.Chat.Id);
                }
                else if (Command.aboutMe == command)
                {
                    userService.aboutBot(message_req.Chat.Id);
            
                }
                else
                {
                    //دستور نا مشخصی رسیده برای هندل کردن مین کامند ارسال و یک عدد به لاگ این خطا اضافه گردد
                    responseCreator.mainCommand(message_req.Chat.Id);
                    string msg = "دستور نا مشخصی رسیده برای هندل کردن مین کامند ارسال و یک عدد به لاگ این خطا اضافه گردد "
                        + "ParseCommand" + message_req.Text;
                    Setting.log(msg, Setting.logNoDeterminCommandCount, ref Setting.logNoDeterminCommand);
                    // throw new Exception("دستور نا مشخصی");
                }
            }
            catch (Exception e)
            {
               // throw new Exception("خط در پارس  و اجرا دستور" + e.Message);
                string msg = "خط در پارس  و اجرا دستور" + e.Message + "ParseCommand" + message_req.Text;
                Setting.log(msg, Setting.logParsCommandCount, ref Setting.logParsCommand);
            }


        }









    }
}
