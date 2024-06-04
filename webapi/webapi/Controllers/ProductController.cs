using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Script.Serialization;
using PublicChatBot;
using Telegram.Bot;

namespace webapi.Controllers
{
    public class ProductController : ApiController
    {

        public IEnumerable<string> Get()
        {
            System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi.txt", "reqest time Get" + DateTime.Now + "\r\n");
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5
        public string Get(int id)
        {
            System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi.txt", "reqest time Get id" + DateTime.Now + "\r\n");
            return "value";
        }

     static   BotModel _botModel = null;

     public BotModel botModel
     {
         get {
             if (_botModel != null)
             {
                 return _botModel;
             }
             else
             {
                 _botModel = ManageRequest.LoadBotByName("main_orginal_book_list_bot");
                 return _botModel;
             }
         }
     }
        // POST: api/Product ([FromBody]string value)
        public void Post(Telegram.Bot.Types.Update update)
        {
            try
            {
                System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi.txt", "reqest time Post" + DateTime.Now + "\r\n");
                System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi.txt", "update.Id " + update.Id + "\r\n");
                System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi.txt", "load bot" + botModel.token + "\r\n");
                ManageRequest manage = new ManageRequest(botModel);
                System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi.txt", "load manage" + "\r\n");
                manage.executeARequest(botModel, update);
                System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi.txt", "executeARequestt" + "\r\n");

                //Telegram.Bot.Types.Update[] updateArray = null;
                //Api api = new Api(botModel.token);
                //updateArray = api.GetUpdates().Result;
                //manage.executeARequest(botModel, updateArray.FirstOrDefault());
            }
            catch (Exception e)
            {

                System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi.txt", "Exception " + e.Message + "\r\n");
            }

        }

        // PUT: api/Product/5
        public void Put(int id, [FromBody]string value)
        {
            System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi.txt", "reqest time Put" + DateTime.Now + "\r\n");

        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
            System.IO.File.AppendAllText(Setting.logFileName + "\\ProductApi.txt", "reqest time Delete" + DateTime.Now + "\r\n");

        }
        
    }


}
