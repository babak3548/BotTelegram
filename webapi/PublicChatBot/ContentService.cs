
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Telegram.Bot.Types;
using AnarSoft.Utility.Utilities;

namespace PublicChatBot
{
    class ContentService : BaseService
    {
        BotModel botModel;
        ResponseCreator responseCreator;
        public ContentService(BotModel botModel)
        {
            this.botModel = botModel;
            responseCreator = new ResponseCreator(botModel);
        }


        internal void plusRate(int chatId, int contentId)
        {
            EffectRate(chatId, contentId, 1);
        }
        internal void MinusRate(int chatId, int contentId)
        {
            EffectRate(chatId, contentId, -1);
        }
        private void EffectRate(int chatId, int contentId, int rate)
        {
            content _content;
            if (contentId <= 0) { sendContent(chatId); }
            else
            {
                _content = dbContext.contents.FirstOrDefault(c => c.id == contentId);
                if (rate > 0)
                {
                    _content.ratePlus = _content.ratePlus + rate;
                }
                else
                {
                    _content.rateMinus = _content.rateMinus + rate;
                }
                if (_content.ratePlus + _content.rateMinus < -3)
                {
                    _content.isBroadcast = (int)EnumIsBroadcast.no;
                }
                dbContext.SaveChanges();
                sendContent(chatId);
            }
        }

        internal void acceptContent(string botUserName, Message message_req)
        {
            Bridge_BotUser bridge_BotUser = dbContext.Bridge_BotUser.FirstOrDefault(u => u.fk_user == message_req.Chat.Id);
            if (bridge_BotUser.role == 7 || bridge_BotUser.role == 8)
            {
                responseCreator.sendTextMessage(message_req.Chat.Id ,Setting.you_cant_add_content);
                return;
            }

            if (determinIsContent(message_req))
            {
                content _content = new content();
                _content.messageId = message_req.MessageId;
                _content.fk_bot = botUserName;
                _content.fk_user = message_req.Chat.Id;
                _content.ratePlus = 0;
                _content.isBroadcast = (int)EnumIsBroadcast.noDetermin;
                _content.date = DateTime.Now;
                dbContext.contents.Add(_content);
                dbContext.SaveChanges();
            }

            responseCreator.mainCommand(message_req.Chat.Id);

        }
        private bool determinIsContent(Message message_req)
        {
            Command command = new Command();
            if (message_req.Type.ContentParameter != MessageType.TextMessage.ContentParameter)
            {
                return true;
            }
            else
            {
                if (command.isCommand(message_req.Text))
                {
                    return false;
                }
                else
                {
                    if (message_req.Text.Length > 40)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        internal void sendContent(int chatId)
        {
            Command objCommand = new Command();
            content _content;
            int lastWatchedContentId = 1;

            Bridge_BotUser bridge_BotUser = dbContext.Bridge_BotUser.FirstOrDefault(b => b.fk_bot == botModel.botName
                && b.fk_user == chatId);
       
            if (bridge_BotUser.role == 8)
            {
                responseCreator.sendTextMessage(chatId, Setting.you_cant_use_robot);
                return;
            }
            lastWatchedContentId = bridge_BotUser.last_contentId;

            _content = dbContext.contents
               .FirstOrDefault(c => c.id > lastWatchedContentId && c.isBroadcast == (int)EnumIsBroadcast.noDetermin
                && c.fk_bot == botModel.botName);
            if (_content == null)//اگر محتوای ارسال شده هم نبود درخواست یه کانتنت جدید از کاربر را می کند
            {
                responseCreator.sendTextMessage(chatId, Setting.please_send_me_new_conten, responseCreator.getMainCommandReplay());
            }
            else
            {
                bridge_BotUser.last_contentId = _content.id;
                responseCreator.ForwardMessage(chatId, _content.fk_user, _content.messageId);

                List<Bridge_BotUser> bridge_BotUsers = dbContext.Bridge_BotUser.Where(b => b.fk_bot == botModel.botName)
                  .OrderBy(x => Guid.NewGuid()).Take(5).ToList();
                foreach (var bu in bridge_BotUsers)
                {
                    responseCreator.ForwardMessage(bu.fk_user, _content.fk_user, _content.messageId);
                }

                objCommand.addContentIdToCommandState(botModel.botName, chatId, _content.id);
                dbContext.SaveChanges();
            }
        }
    }

    public enum EnumIsBroadcast
    {
        noDetermin = 0, yse = 1, no = 2
    }
}


/* try
 {
     Random rand = new Random();
     int randomContentId = rand.Next(1, botModel.shanseErsaleMohtavayeTekrari);
     if (randomContentId > 10)
     {
         var BroadcastList = dbContext.DbWriteble.contents
           .Where(c => c.isBroadcast == (int)EnumIsBroadcast.yse && c.fk_bot == botModel.botName);
         if (BroadcastList.Count() > 0)
         {
             _content = BroadcastList.OrderBy(r => Guid.NewGuid()).First();
         }

     }

 }
 catch (Exception e) { _content = null; }
 */