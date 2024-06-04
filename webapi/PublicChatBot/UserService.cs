//using DataModelTelegram.Request_TelegramModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types;
using AnarSoft.Utility.Utilities;

namespace PublicChatBot
{
    public class UserService : BaseService
    {
        BotModel botModel;
        ResponseCreator responseCreator;
        public UserService(BotModel botModel)
        {
            this.botModel = botModel;
            responseCreator = new ResponseCreator(botModel);
        }
        internal void addNormalUser(BotModel botModel, Message message_req, string userShear)
        {

            this.botModel = botModel;
            byte role = Roles.normal;

            _addAndChnageRoleUser(botModel.botName, message_req, role, userShear);
            responseCreator.mainCommand(message_req.Chat.Id);
        }

        private string _addAndChnageRoleUser(string botUserName, Message message_req, byte role, string userShear)
        {
            User user = _getUser(message_req.From.Id);
            if (user != null)
            {
                Bridge_BotUser b_botUser = _getB_botUser(botUserName, user.id);
                if (b_botUser != null)
                {
                    return _changeRole(b_botUser, role);//>>> 
                }
                else
                {
                    b_botUser = new Bridge_BotUser();
                    _addBridge_UserBot(botUserName, user, b_botUser, role, userShear);
                    return Setting.you_add_to_public_chat;
                }
            }
            else
            {
                user = new User();
                _addUser(message_req, user);
                Bridge_BotUser b_botUser = new Bridge_BotUser();
                _addBridge_UserBot(botUserName, user, b_botUser, role, userShear);
                return Setting.you_add_to_public_chat;
            }
        }

        private string _changeRole(Bridge_BotUser b_botUser, byte role)
        {
            if (b_botUser.role == role)
            { return Setting.you_allready_added; }
            else
            {
                if (b_botUser.role >= Roles.participant)
                {
                    b_botUser.role = role;
                    dbContext.SaveChanges();
                    return Setting.change_role_user;
                }
                else
                {
                    return Setting.incorrect_Command;
                }
            }
        }

        private void _addUser(Message message_req, User user)
        {
            user.id = message_req.From.Id;
            user.name = message_req.From.FirstName;
            user.lastName = message_req.From.LastName;
            user.date = DateTime.Now;
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        private void _addBridge_UserBot(string botUserName, User user, Bridge_BotUser b_botUser, byte role, string userShear)
        {
            b_botUser.fk_bot = botUserName;
            b_botUser.fk_user = user.id;
            b_botUser.role = role;
            content _content = dbContext.contents.FirstOrDefault(c => c.fk_bot == botModel.botName
                && c.isBroadcast == (int)EnumIsBroadcast.noDetermin);
            if (_content != null)
            {
                b_botUser.last_contentId = _content.id;
            }
            else
            {
                b_botUser.last_contentId = 0;
            }

            dbContext.Bridge_BotUser.Add(b_botUser);
            int res = dbContext.SaveChanges();
            int paramUserShearId = userShear.ToInteger(0);
            Bridge_BotUser bridge_BotUser = dbContext.Bridge_BotUser.FirstOrDefault(b => b.fk_bot == botModel.botName
              && b.fk_user == paramUserShearId);
            if (bridge_BotUser != null && res > 0)
            {
                bridge_BotUser.rate = bridge_BotUser.rate + Setting.rateShear;
                dbContext.SaveChanges();
            }

        }
        private User _getUser(int user_id)
        {
            return dbContext.Users.FirstOrDefault(u => u.id == user_id);
        }

        private Bridge_BotUser _getB_botUser(string botUserName, int user_id)
        {
            return dbContext.Bridge_BotUser.FirstOrDefault(b => b.fk_bot == botUserName && b.fk_user == user_id);
        }
        /*
                internal string addParticipantUser(string botUserName, Message message_req)
                {
                    byte role = Roles.participant;
                    return _addAndChnageRoleUser(botUserName, message_req, role);
                }

                internal string deleteParticipantUser(string botUserName, Message message_req)
                {
                    Bridge_BotUser b_botUser = _getB_botUser(botUserName, message_req.From.Id);
                    dbContext.DbWriteble.Bridge_BotUser.Remove(b_botUser);
                    dbContext.DbWriteble.SaveChanges();
                    return Setting.remove_user;
                }
                */
        internal IQueryable<Bridge_BotUser> getBridge_BotUser_Bot(string botUserName)
        {
            return dbContext.Bridge_BotUser.Where(b => b.fk_bot == botUserName);
        }

        internal void getTopRate(int chatId)
        {
            string message = "";
            IQueryable<User> iQuerybleUser = dbContext.Users.OrderByDescending(u => u.contents
                .Where(b => b.fk_bot == botModel.botName).Sum(b => b.ratePlus + b.rateMinus) + u.Bridge_BotUser.FirstOrDefault(b => b.fk_bot == botModel.botName).rate).Take(10);

            foreach (User user in iQuerybleUser)
            {
                message = message + "\r\n" + user.name + " " + user.lastName + " : "
                    + getRateUser(user.id);
            }
            responseCreator.sendTextMessage(chatId, message, responseCreator.getMainCommandReplay());
        }

        internal void getMyRate(int chatId)
        {
            int rate = getRateUser(chatId);
            string message = Setting.your_rate + " : " + rate;
            responseCreator.sendTextMessage(chatId, message, responseCreator.getMainCommandReplay());
        }

        private int getRateUser(int chatId)
        {
            int rate = 0;
            var iEContentUser = dbContext.contents.Where(b => b.fk_bot == botModel.botName && b.fk_user == chatId);
            if (iEContentUser.Count() > 0)
            {
                rate = iEContentUser.Sum(b => b.ratePlus + b.rateMinus);
            }
            var bridge_BotUser = dbContext.Bridge_BotUser.FirstOrDefault(b => b.fk_bot == botModel.botName && b.fk_user == chatId);
            if (bridge_BotUser != null)
            {
                rate = rate + bridge_BotUser.rate;
            }
            return rate;

        }

        internal void getFlowers(int chatId)
        {
            int countUser = dbContext.Bridge_BotUser.Count(u => u.fk_bot == botModel.botName);
            string message = Setting.flowers + " : " + countUser;
            responseCreator.sendTextMessage(chatId, message, responseCreator.getMainCommandReplay());
        }

        internal int getCountUser()
        {
            return dbContext.Bridge_BotUser.Count(b => b.fk_bot == botModel.botName);
        }

        internal void aboutBot(int ChatId)
        {
            Bot bot = dbContext.Bots.FirstOrDefault(b => b.userName == botModel.botName);
            responseCreator.sendTextMessage(ChatId, bot.description);//Setting.aboutMe
            //throw new NotImplementedException();
        }
    }
}
