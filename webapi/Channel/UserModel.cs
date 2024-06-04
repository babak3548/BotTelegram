using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel
{
   public class UserModel
    {
       public UserModel()
       {
            
       }
       public UserModel(int chatId,byte commandCode , int entityId,byte stateInstraction )
       {
            ChatId = chatId;
            CommandCode= commandCode;
            EntityIdSkip = entityId;
            StateInstraction = stateInstraction;
       }
        public int ChatId { get; set; }
        public  byte CommandCode{ get; set; }
        public int EntityIdSkip { get; set; }
        public int GroupEntityId{ get; set; }
        public  byte StateInstraction{ get; set; }
        public byte Order { get; set; }
        public string searchVal { get; set; }
      
    }
}
