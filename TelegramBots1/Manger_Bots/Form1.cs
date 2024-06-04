using NetTelegramBotApi;
using NetTelegramBotApi.Requests;
using PublicChatBot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Manger_Bots
{
    public partial class Form1 : Form
    {
        static bool start = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PublicChatBot.SendMethods sendMethods = new PublicChatBot.SendMethods();
            //  rtGetUpdate.Text = 
            sendMethods.getUpdates("key_bot");

        }

        private void bSendMessage_Click(object sender, EventArgs e)
        {
            // PublicChatBot.SendMethods sendMethods = new PublicChatBot.SendMethods();
            //   sendMethods.sendToUserMsg(rtSendMessage.Text, "91812379:AAGbpv-IJQG6TMwtS-UpP6gwzr7UwzPt7rU");
            // TelegramBot bot = new TelegramBot("91812379:AAGbpv-IJQG6TMwtS-UpP6gwzr7UwzPt7rU");
            Telegram.Bot.Api api = new Telegram.Bot.Api("98587826:AAEbkPyPvS3zIcfLdB6-pr2jioOLGc4_-5A");
            //  api.SendTextMessage(48124657, "testtt");
            //botModel.lastUpdateId + 1, 100


            ThreadRun(api);

          //ThreadStart childWork = new ThreadStart(() => ThreadRun(api));
          //  Thread th = new Thread(childWork);
           // th.Start();
            //👎👍👥👤 ⏩💰💵 ➕🔗🔧🔆⚡❄👍👎❤💔
        }
        public  void m() { }
        public  void ThreadRun(Telegram.Bot.Api api)
        {
            while (true)
            {
                Telegram.Bot.Types.Update[] updateArray = api.GetUpdates().Result;
                foreach (var item in updateArray)
                {
                    rtGetUpdate.Text = rtGetUpdate.Text + item.Message.Text + "\r\n";
                    Application.DoEvents();
                }
                Thread.Sleep(1000);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (start == false)
                {
                    start = true;
                    btnStart.Text = "stop";
                    startService();
                }
                else
                {
                    start = false;
                    btnStart.Text = "start";
                }

            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("logUnhandelError.txt", "خطای خطرناک و کنترول نشده" + ex.Message);
            }




        }

     //  static bool inRun = true;
        private void startService()
        {
            ManageRequest manage = new ManageRequest( null);
                foreach (BotModel botModel in manage.listBotsService())
                {
                    ThreadStart childWork = new ThreadStart(() => runBot(botModel));
                    Thread th = new Thread(childWork);
                    th.Start();//th1
                    
                }
        }

        public static void runBot(BotModel botModel)
        {
            ManageRequest manage = new ManageRequest(botModel);
            while (start)
            {
                manage.getUpdates();
            
            }
        }

        private void testDbConn_Click(object sender, EventArgs e)
        {
            //var x = DateTime.Now.ToString("yyyy-MM-dd");
                //DirectCast((Datetime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds, Int64);
                //(int)(DateTime.Now)).TotalMilliseconds;
            ManageRequest manage = new ManageRequest(null);
            rtGetUpdate.Text = manage.testcDb();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            start = false;
        }
    }
}
