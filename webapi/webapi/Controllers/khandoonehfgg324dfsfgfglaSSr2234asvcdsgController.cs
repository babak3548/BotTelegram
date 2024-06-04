using PublicChatBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Telegram.Bot;

namespace webapi.Controllers
{
    public class khandoonehfgg324dfsfgfglaSSr2234asvcdsgController : ApiController
    {
  
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5
        public string Get(int id)
        {
        
            return "value";
        }

        static BotModel _botModel = null;

        public BotModel botModel
        {
            get
            {
                if (_botModel != null)
                {
                    return _botModel;
                }
                else
                {
                    _botModel = ManageRequest.LoadBotByName("KhandoonehBot");
                    return _botModel;
                }
            }
        }
        // POST: api/Product ([FromBody]string value)
        public void Post(Telegram.Bot.Types.Update update)
        {
            try
            {
             ManageRequest manage = new ManageRequest(botModel);
             manage.executeARequest(botModel, update);

             //Telegram.Bot.Types.Update[] updateArray = null;
             //Api api = new Api(botModel.token);
             //updateArray = api.GetUpdates().Result;
             //foreach (var item in updateArray)
             //{
             //    manage.executeARequest(botModel, item);
             //}
               
            }
            catch (Exception e)
            {
                System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", "Exception " + e.Message + "\r\n");
            }

        }

        // PUT: api/Product/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {

            var x = "یه بار خواستم جلو دوست دخترم کلاس بذارم زنگ زدم خونمون \nگفتم مامیییی شام چی داریم؟\n.\n.\n.\n.\n.\n.\n.\n.\n.\nبابام گوشی رو گرفت \nگفت عن داریم پسرم ، عن !!!!\nمیخوری؟ :)))\nبابام اعصابش زیره درخــــــــــته آلبالو گمشده!!\nدانلود جوک بازار\nyon.ir/jokbazar";
        }
    }
}
