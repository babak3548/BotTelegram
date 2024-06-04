using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicChatBot
{
    interface IBot
    {
        void enterRequest(BotModel botModel, Telegram.Bot.Types.Update[] jsonMessage);
     void  getUpdates();
     IEnumerable<BotModel> listBotsService();
    }
}
