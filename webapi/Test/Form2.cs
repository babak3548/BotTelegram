using Channel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Test
{
    /*
     👎👍💵💰⚡🔆🔗🔧👥🔍🔥✨🌟😹🌀⭐⭐👻🎉💰💴💵📰🔭🔬🎯🏆🃏🎮🏁🍭🎼🎵🎬🎤⏰🔨📢🍗🎓🎆🎋🎁🎄🎅👻🎃🎑🎐🎉🎊🎈🎌🔮🎥📷📹☎📱💻💾💽📀💿📼📞📟📠📡📺📻🔊🔉⌛📢
     * 🔔⏰⌚🔓🔒🔏🔐🔑🔎🛁🔍🔋🔌🔅🔆🔦💡🛀🚿🚽🔧🔩🔨🚪🚬💵💴💰💉💊🔪🔫💣💷💶💳💸📲📧📥📤📭📬📪📫📯📨📩✉📈📊📑📃📄📝📦📮📉📜📋📅📆📇📁📂📕📐📏✏✒📎📌✂
     * 📖📚📒📔📓📙📘📗🔖📛🔬🔭📰🎨🎬🎤🎷🎺🎻🎹🎶🎵🎼🎧🎸👾🎮🃏🎴🀄🎲🎯🎳🏉🎱🎾⚾⚽🏀🏈⛳🚵🚴🏁🏇🏆🏂🎿🍺🍼🍶🍵☕🎣🏄🏊🍻🍸🍹🍷🍴🍕🍔🍟🍊🍏🍭🍅🍆🍍🍐🍒🍇🍑
     * 🍈🍌🍊🍏🍎🍉🌽🍍✈🚀🚓🚔🚑🚙🚘🚆🚄🚊🚉📍🚩🇬🇧♨🗿🎪🚾🚰♿🚻🆗⏩⏪ℹ↙↔↕🔄◀▶🔼🔽↘↖↗↗🔤🔡🔠➡⬅🔢#⃣🔣⬆⬇🔞📵🚯🚱🚳🚷✖➕➖➗♠♥♣♦💮💯✔◼🔳🔲🔱➰🔗
     * 🔘☑◻◾◽▪▫🔺⬜⬛🔸🔷🔶🔻🔵🔴⚪⚫🔹✅❎❇✳🚭😹👆👇👌👊✊✌👋🙏💪👏☝👬👭💏💑👯🙆🙅💁🙇🙍🙎👟👞👠👢👙🎽👗👚👕💼👜👛👓🎀🌂💄💓💗💔❤💋💍💎💘💭💬👣⭐
     */
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startService();
        }
        BotModel botModel = new BotModel();
        private void startService()
        {
            button1.Text = "runing";
            botModel.botName = "channelStore";
            //botModel.token = "164899142:AAHnK81CDbtG0BhxhxrBnFPlNy9AB17h9qs";
            botModel.token = "110092488:AAEDGQ9FBbdHDQXZcr8XljGiCMfsosTUFAo";
                ThreadStart childWork = new ThreadStart(() => runBot(botModel));
                Thread th = new Thread(childWork);
                th.Start();//th1

            
        }
        int sleepTime = 0;
        public  void runBot(BotModel botModel)
        {
            while (true)
            {
                try
                {
                    Telegram.Bot.Types.Update[] updateArray = null;
                    Api api = new Api(botModel.token);
                    updateArray = api.GetUpdates(botModel.lastUpdateId + 1, 100).Result;
                    if (updateArray == null || updateArray.Count() == 0)
                    {
                        if (sleepTime < 1000)
                        {
                            sleepTime += 10;
                        }
                        else
                        {
                            sleepTime = 1000;
                        }
                       // Thread.Sleep(sleepTime);
                        // botModel.nextFetchFromTelegram = Setting.getDateTimeNowInMil() + 2000;
                    }
                    else
                    {
                        sleepTime = 0;
                        enterRequest( updateArray);
                    }

                }
                catch (Exception e)
                {
                    string msg = "خطا در پردازش ریکست های بوت " + " getUpdates " + e.Message;
                   // Setting.log(msg, Setting.logGetUpdatesCount, ref Setting.logGetUpdates);
                }

            }
        }

        public void enterRequest( Update[] updateArray)
        {
            try
            {
                Update lastRecord = null;
                foreach (Telegram.Bot.Types.Update update in updateArray)
                {
                    lastRecord = update;
                    ThreadStart childWork = new ThreadStart(() => executeARequest(update));
                    Thread th = new Thread(childWork);
                    th.Start();//th2

                }
                if (lastRecord != null)
                {
                    botModel.lastUpdateId = lastRecord.Id;
                }
            }
            catch (Exception e)
            {
                string msg = "خطا در پردازش ریکست های بوت " + e.Message + "enterRequest";
     
            }

        }
        CommandManager _commandManager = new CommandManager();   
        public void executeARequest(Telegram.Bot.Types.Update update)
        {
            _commandManager.ExcuteCommand(update.Message);
        }



        private void button3_Click(object sender, EventArgs e)
        {
            string urlAddress = "https://web.telegram.org/#/im?p=@delneveshte1224";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
        }

      
        

 
    }

    public class BotModel
    {
        public string botName;
        public string token;
        public int lastUpdateId;
        public string description;
        public int maxSendInDay;

        public int lastSentHours;
        public Int64 nextFetchFromTelegram;



        public int shanseErsaleMohtavayeTekrari;
    }



 
}
