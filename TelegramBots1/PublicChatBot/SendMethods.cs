using DataModelTelegram.Response_TelegramModel;
using DataModelTelegram.Request_TelegramModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace PublicChatBot
{
    public class SendMethods :BaseService
    {
        private string sendAction1(string sendMessageJson, string token, string method)
        {

            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                result = client.UploadString(Setting.urlWebMethod + token + "/" + method, "POST", sendMessageJson);
            }
            return result;
        }
        private string sendAction(string sendMessageJson, string token, string method)
        {
            var webAddr = Setting.urlWebMethod+ token + "/" + method;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = sendMessageJson;

                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }

         async void testApiAsync()
        {
            var Bot = new Telegram.Bot.Api("91812379:AAGbpv-IJQG6TMwtS-UpP6gwzr7UwzPt7rU");
            var me = await Bot.GetMe();
            System.Console.WriteLine("Hello my name is " + me.FirstName);
        }
         public async void getUpdates(string userNameBot)
        {
            testApiAsync();
           string token= dbContext.DbWriteble.Bots.FirstOrDefault(b=>b.userName==userNameBot).token;
           Telegram.Bot.Api api = new Telegram.Bot.Api(token);
            try
            {
                var x = await api.GetUpdates(); 
            }
            catch (Exception e)
            {
                
                throw;
            }
   
           // return x.Zip;
           // GetUpdates_R getUptate = new GetUpdates_R();
           //// string x = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
           // Bot bot = DbContextEntity.DbReadOnly.Bots.FirstOrDefault(b => b.userName == userNameBot);
           // getUptate.offset = bot.offsetLast;
           // string result = sendAction1(getUptate.getString(), bot.token, "getUpdates");
            //
            //return result;
        }
        public void sendToUserMsg(string sendMessageJson, string token)
        {
            sendAction(sendMessageJson, token, "sendmessage");
        }

    }
}
