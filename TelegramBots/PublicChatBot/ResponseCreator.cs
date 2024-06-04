using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
namespace PublicChatBot
{
    internal class ResponseCreator : BaseService
    {
        BotModel botModel;
        Telegram.Bot.Api api;
        internal ResponseCreator(BotModel botModel)
        {
            this.botModel = botModel;
            api = new Telegram.Bot.Api(botModel.token);
        }


        internal void ForwardMessage(int to_chatId, int from_chatId, int MessageId)
        {
            api.ForwardMessage(to_chatId, from_chatId, MessageId);
        }
        internal void copyMessage(int to_chatId, Message message, string contentId)
        {
            if (message.Type == MessageType.AudioMessage)
            {
                api.SendAudio(message.Chat.Id, message.Audio.FileId, 0, getMainCommandReplay());
            }
            else if (message.Type == MessageType.ContactMessage)
            {
                throw new Exception("خطا در کپی مسیج: نو مسیج تشخیص داده نمی شود");
            }
            else if (message.Type == MessageType.DocumentMessage)
            {
                api.SendDocument(message.Chat.Id, message.Document.FileId, 0, getMainCommandReplay());
            }
            else if (message.Type == MessageType.LocationMessage)
            {
                api.SendLocation(message.Chat.Id, message.Location.Latitude, message.Location.Longitude,
                    0, getMainCommandReplay());
            }
            else if (message.Type == MessageType.PhotoMessage)
            {
                //  api.SendPhoto(message.Chat.Id, message.Photo[0].FileId,message., 0, getPlusMinesReplay(contentId));
            }
            else if (message.Type == MessageType.StickerMessage)
            {

            }
            else if (message.Type == MessageType.TextMessage)
            {

            }
            else if (message.Type == MessageType.VideoMessage)
            {

            }
            else
            {
                throw new Exception("خطا در کپی مسیج: نو مسیج تشخیص داده نمی شود");
            }

        }

        internal void sendChatAction(int to_chatId)
        {
            api.SendChatAction(to_chatId, ChatAction.Typing);
        }
        internal void sendTextMessage(int chatId, string message)
        {
            api.SendTextMessage(chatId, message, false, 0, getMainCommandReplay());
        }
        internal void sendTextMessage(int chatId, string message, ReplyMarkup replyMarkup)
        {
            api.SendTextMessage(chatId, message, false, 0, replyMarkup);
        }
        internal void shearWithFirend(int chatId)
        {
            api.SendTextMessage(chatId, Setting.send_url_to_your_friends + Setting.urlTelegramMe + botModel.botName + "?start=" + chatId);
        }

        internal void mainCommand(int chatId)
        {
            api.SendTextMessage(chatId, Setting.main_command_message, false, 0, getMainCommandReplay());
        }

        internal void addContent(int chatId)
        {
            api.SendTextMessage(chatId, Setting.waiting_send_content);
        }

      /*  internal ReplyMarkup getPlusMinesReplay(string contentId)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup();

            string[][] keyboard = new string[][] { new string[] { Command.plus + " " + contentId, Command.minus + " " + contentId } };
            replyKeyboardMarkup.Keyboard = keyboard;
            return replyKeyboardMarkup;
        }*/

        internal ReplyKeyboardMarkup getMainCommandReplay()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup();

            string[][] keyboard = new string[][] 
            { 
              new string[] { Command.plus , Command.minus } ,
              new string[] { Command.nextContent, Command.addContent },
              new string[] { Command.topRate , Command.myRate},
              new string[] { Command.shearWithFirend ,Command.aboutMe}
            };
            replyKeyboardMarkup.Keyboard = keyboard;
            return replyKeyboardMarkup;
        }


    }
}
