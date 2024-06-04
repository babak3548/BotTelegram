using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Channel
{
  public  class UsersManager
    {
      
      private static List<UserModel> _users;
      private  List<UserModel> users { get {
          if (_users == null)
          {
              ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
              _users = new List<UserModel>();
              _users= _channelBotEntities.Users.Select(u=>new UserModel{ChatId=u.ChatId}).ToList();
          }
          return _users;
      }
      }
      public void CheckRegUser(Message message)
      {
          var um = users.FirstOrDefault(u => u.ChatId == message.Chat.Id);
        if (um== null)
        {
            ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
            _channelBotEntities.Users.Add(new User { ChatId = message.Chat.Id, UserName = message.From.Username });
            _channelBotEntities.SaveChanges();
            _users = null;
        }

      }
      public UserModel  GetUser(int chatId){
         return users.FirstOrDefault(u => u.ChatId == chatId);
      }

      public void AddUserStateCommand(int chatId,byte commandCode , int entityId,byte stateInstraction )
       {
           UserModel user = users.FirstOrDefault(u => u.ChatId == chatId);
           user.ChatId = chatId;
           user.CommandCode = commandCode;
           user.EntityIdSkip = entityId;
           user.StateInstraction = stateInstraction;
         

        
      }
    }
}
