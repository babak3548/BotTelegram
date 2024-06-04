using System;
using System.Collections.Generic;
using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
namespace Channel
{
    public class CommandManager
    {
        public const byte orderByTopMember = 1;
        public const byte orderByStars = 2;
        public const byte orderByRecent = 3;
        /* 
        👎👍💵💰⚡🔆🔗🔧👥🔍🔥✨🌟😹🌀⭐⭐👻🎉💰💴💵📰🔭🔬🎯🏆🃏🎮🏁🍭🎼🎵🎬🎤⏰🔨📢🍗🎓🎆🎋🎁🎄🎅👻🎃🎑🎐🎉🎊🎈🎌🔮🎥📷📹☎📱💻💾💽📀💿📼📞📟📠📡📺📻🔊🔉⌛📢
        * 🔔⏰⌚🔓🔒🔏🔐🔑🔎🛁🔍🔋🔌🔅🔆🔦💡🛀🚿🚽🔧🔩🔨🚪🚬💵💴💰💉💊🔪🔫💣💷💶💳💸📲📧📥📤📭📬📪📫📯📨📩✉📈📊📑📃📄📝📦📮📉📜📋📅📆📇📁📂📕📐📏✏✒📎📌✂
        * 📖📚📒📔📓📙📘📗🔖📛🔬🔭📰🎨🎬🎤🎷🎺🎻🎹🎶🎵🎼🎧🎸👾🎮🃏🎴🀄🎲🎯🎳🏉🎱🎾⚾⚽🏀🏈⛳🚵🚴🏁🏇🏆🏂🎿🍺🍼🍶🍵☕🎣🏄🏊🍻🍸🍹🍷🍴🍕🍔🍟🍊🍏🍭🍅🍆🍍🍐🍒🍇🍑
        * 🍈🍌🍊🍏🍎🍉🌽🍍✈🚀🚓🚔🚑🚙🚘🚆🚄🚊🚉📍🚩🇬🇧♨🗿🎪🚾🚰♿🚻🆗⏩⏪ℹ↙↔↕🔄◀▶🔼🔽↘↖↗↗🔤🔡🔠➡⬅🔢#⃣🔣⬆⬇🔞📵🚯🚱🚳🚷✖➕➖➗♠♥♣♦💮💯✔◼🔳🔲🔱➰🔗
        * 🔘☑◻◾◽▪▫🔺⬜⬛🔸🔷🔶🔻🔵🔴⚪⚫🔹✅❎❇✳🚭😹👆👇👌👊✊✌👋🙏💪👏☝👬👭💏💑👯🙆🙅💁🙇🙍🙎👟👞👠👢👙🎽👗👚👕💼👜👛👓🎀🌂💄💓💗💔❤💋💍💎💘💭💬👣⭐🆔📢📣
        */
        public const string start = "start";//show /start 41143480
        public const byte startVal = 18;//show /start 41143480
        public const string byCategory = "براساس گروه 🎋";//🎆";//show
        public const byte byCategoryVal = 1;

        public const string showMore = "بیشتر ⏩";//no show
        public const byte showMoreVal = 2;

        public const string topMember = "پرطرفدارترینها 🏆";
        public const byte topMemberVal = 3;

        public const string TopStars = "محبوب ترینها 🔥";
        public const byte TopStarsVal = 4;

        public const string recent = "جدیدترینها ⚡";
        public const byte recentVal = 5;

        public const string addReview = "ثبت ستاره ⭐";
        public const byte addReviewVal = 6;

        public const string addChannel = "ثبت کانال ➕";//show - have step ✨
        public const byte addChannelVal = 7;

        public const string aboutMe = "درباره ما 👤";//show
        public const byte aboutMeVal = 8;

        public const string search = "جستجو 🔍";
        public const byte searchVal = 9;

        public const string mainMenu = "برگشت به منو 📘";
        public const byte mainMenuVal = 10;

        //شماره کد ستاره نباید تغییر کند چون با منهای 10 مقدار آنها به عنوان امتیاز ذخیره می شوند
        public const string star1 = "⭐";
        public const byte star1Val = 11;
        public const string star2 = "⭐⭐";
        public const byte star2Val = 12;
        public const string star3 = "⭐⭐⭐";
        public const byte star3Val = 13;
        public const string star4 = "⭐⭐⭐⭐";
        public const byte star4Val = 14;
        public const string star5 = "⭐⭐⭐⭐⭐";
        public const byte star5Val = 15;


        public const string getLinkRate = "لینک ثبت ستاره 🔗";
        public const byte getLinkRateVal = 16;

        UsersManager _users;
        ChannelManager _channelManger;
        ResponseCreator _rc;
        CategoryManager _categoryManger;


        public CommandManager()
        {
            _users = new UsersManager();
            _rc = new ResponseCreator();
            _channelManger = new ChannelManager();
            _categoryManger = new CategoryManager();
        }
        private byte authCommand(string value)
        {
            value = value.Replace("/", "");
            if (value.StartsWith(start)) return startVal;
            else if (value.StartsWith(byCategory)) return byCategoryVal;
            else if (value.StartsWith(showMore)) return showMoreVal;
            else if (value.StartsWith(topMember)) return topMemberVal;
            else if (value.StartsWith(TopStars)) return TopStarsVal;
            else if (value.StartsWith(recent)) return recentVal;
            else if (value.StartsWith(addReview)) return addReviewVal;
            else if (value.StartsWith(addChannel)) return addChannelVal;
            else if (value.StartsWith(aboutMe)) return aboutMeVal;
            else if (value.StartsWith(search)) return searchVal;
            else if (value.StartsWith(mainMenu)) return mainMenuVal;
            else if (value==star1) return star1Val;
            else if (value==star2) return star2Val;
            else if (value==star3) return star3Val;
            else if (value==star4) return star4Val;
            else if (value==star5) return star5Val;
            else if (value.StartsWith(getLinkRate)) return getLinkRateVal;
            else return 255;
        }

        public void ExcuteCommand(Message message)
        {
            byte oldCommandCode = 0;
            int EntityId1 = 0;
            byte StateInstraction = 0;
            try
            {
                _rc.sendWait(message.Chat.Id);
                message.Text = message.Text.ToLower();
                _users.CheckRegUser(message);
                byte commandCode = authCommand(message.Text);
                UserModel user = _users.GetUser(message.Chat.Id);

                oldCommandCode = user.CommandCode;
                EntityId1 = user.EntityIdSkip;
                StateInstraction = user.StateInstraction;
                if (commandCode == byCategoryVal)//category
                {
                    user.CommandCode = byCategoryVal;
                    var groups = _categoryManger.GetGroups();
                    _rc.sendCategories(message.Chat.Id);

                }
                else if (commandCode == startVal)//showMore
                {
                    string text = message.Text.Replace("/", "").Replace(start, "").Trim();
                    if (text.Length > 3)
                    {
                        resetUserState(user);
                        user.CommandCode = addReviewVal;//تبدیل کامند 
                        reqRate(message, user, text);
                    }
                    else
                    {
                        _rc.ShowMessageWithMainCommand(Setting.to_Continue_select_Command, message.Chat.Id);
                    }

                }
                else if (commandCode == showMoreVal)//showMore
                {
                    List<Channel> channels;
                    if (!string.IsNullOrWhiteSpace( user.searchVal))
                        channels = _channelManger.Search(user.searchVal, user.EntityIdSkip);
                    else
                        channels = _channelManger.ShowMore(user.GroupEntityId, user.EntityIdSkip, user.Order);

                    _rc.sendChannels(channels, message.Chat.Id);
                    setLastSkip(user, channels);
                    
                    if (channels.Count < ChannelManager.PageSize)
                        resetUserState(user);
                }
                else if (commandCode == topMemberVal)
                {
                    user.Order = orderByTopMember;
                    var channels = _channelManger.ShowMore(user.GroupEntityId, 0, user.Order);
                    _rc.sendChannels(channels, message.Chat.Id);
                    setLastSkip(user, channels);
                    if (channels.Count < ChannelManager.PageSize)
                        resetUserState(user);

                }
                else if (commandCode == TopStarsVal)//inviteFirend
                {
                    user.Order = orderByStars;
                    var channels = _channelManger.ShowMore(user.GroupEntityId, 0, user.Order);
                    _rc.sendChannels(channels, message.Chat.Id);
                    setLastSkip(user, channels);
                    if (channels.Count < ChannelManager.PageSize)
                        resetUserState(user);
                }
                else if (commandCode == recentVal)//aboutMe
                {
                    user.Order = recentVal;
                    var channels = _channelManger.ShowMore(user.GroupEntityId, 0, user.Order);
                    _rc.sendChannels(channels, message.Chat.Id);
                    setLastSkip(user, channels);
                    if (channels.Count < ChannelManager.PageSize)
                        resetUserState(user);

                }
                else if (commandCode == addReviewVal)
                {
                    resetUserState(user);
                    user.CommandCode = addReviewVal;
                    user.StateInstraction = ChannelManager.RateState.NameReqState;
                    _rc.ReqChannelLink(message.Chat.Id);
                }
                else if (commandCode == addChannelVal)
                {
                    resetUserState(user);
                    user.CommandCode = addChannelVal;
                    user.StateInstraction = ChannelManager.AddState.LinkReq;
                    _rc.ReqChannelLink(message.Chat.Id);
                }
                else if (commandCode == aboutMeVal)
                {
                    resetUserState(user);
                    _rc.SendAboutMe(message.Chat.Id);
                }
                else if (commandCode == searchVal)
                {
                    resetUserState(user);
                    user.CommandCode = searchVal;
                    _rc.ReqSearchCal(message.Chat.Id);
                }

                else if (commandCode == getLinkRateVal)
                {
                    resetUserState(user);
                    user.CommandCode = getLinkRateVal;
                    _rc.ReqChannelLink(message.Chat.Id);
                }
                else if (commandCode == mainMenuVal)
                {
                    resetUserState(user);

                    _rc.ShowMessageWithMainCommand(Setting.to_Continue_select_Command, message.Chat.Id);
                }
                else if (commandCode == 255 && user.CommandCode == byCategoryVal)
                {
                    var group = _categoryManger.GetCategoryByName(message.Text);
                    if (group == null)
                        throw new Exception("نال بودن گروه خطا به ندرت رخ می دهد");
                    user.GroupEntityId = group.Id;
                    _rc.ShowOrderCommand(message.Chat.Id);
                }
                else if (commandCode == 255 && user.CommandCode == addReviewVal && user.StateInstraction == ChannelManager.RateState.NameReqState)
                {
                    string text = message.Text;
                    reqRate(message, user, text);
                    return;
                }
                else if ((commandCode == star1Val | commandCode == star2Val | commandCode == star3Val
                    | commandCode == star4Val | commandCode == star5Val)
                    && user.CommandCode == addReviewVal && user.StateInstraction == ChannelManager.RateState.NameGetState)
                {
                    var channel = _channelManger.GetChannelById(user.EntityIdSkip);
                    if (channel == null)
                        throw new Exception("در مرحله دریافت تعداد ستاره کاربر خطای اتفاق افتاده");
                    var user_channel = _channelManger.GetUser_Channel(user.ChatId, channel.Id);
                    if (user_channel != null)
                    {
                        _rc.ShowMessageWithMainCommand(Setting.you_add_rate_after, user.ChatId);
                        return;
                    }

                    _channelManger.AddRateChannel(channel, message.Chat.Id, (byte)(commandCode - 10));
                    user.StateInstraction = ChannelManager.RateState.RateGet;
                    _rc.SucessMessage(message.Chat.Id);

                }
                else if (commandCode == 255 && user.CommandCode == addChannelVal && user.StateInstraction == ChannelManager.AddState.LinkReq)
                {
                    var channel = _channelManger.GetChannelByLinkDb(message.Text.GetLinkId());
                    if (channel == null)
                    {
                        var createChannel = _channelManger.CreateChannel(message.Text.GetLinkId(), message.Chat.Id);
                        user.StateInstraction = ChannelManager.AddState.CatReq;
                        user.EntityIdSkip = createChannel.Id;
                        _rc.sendCategories(message.Chat.Id);

                    }
                    else if (channel != null)// && (string.IsNullOrWhiteSpace(channel.Description)||channel.StateAdd == ChannelManager.TableStateAdd.under100)
                    {
                        user.StateInstraction = ChannelManager.AddState.CatReq;
                        user.EntityIdSkip = channel.Id;
                        _channelManger.ChangeSate(channel.Id, ChannelManager.TableStateAdd.accept);
                        _rc.sendCategories(message.Chat.Id);
                    }
                    else
                    {
                        _rc.ShowMessageWithMainCommand(Setting.this_channel_already_added, message.Chat.Id);
                    }
                }
                else if (commandCode == 255 && user.CommandCode == addChannelVal && user.StateInstraction == ChannelManager.AddState.CatReq)
                {
                    var result = _channelManger.SetCatChannel(user.EntityIdSkip, message.Text);
                    if (result)
                    {
                        _rc.ChannelDesReq(message.Chat.Id);
                        user.StateInstraction = ChannelManager.AddState.DesReq;
                    }
                    else
                        _rc.FailMessage(message.Chat.Id);
                }
                else if (commandCode == 255 && user.CommandCode == addChannelVal && user.StateInstraction == ChannelManager.AddState.DesReq)
                {
                    bool result = _channelManger.AddDescription(user.EntityIdSkip, message.Text);

                    if (result)
                    {
                        _rc.ShowMessageWithMainCommand(Setting.register_complete, message.Chat.Id);
                        user.StateInstraction = ChannelManager.AddState.DesGet;
                    }
                    else
                        _rc.FailMessage(message.Chat.Id);
                }
                else if (commandCode == 255 && user.CommandCode == searchVal)
                {
                    user.searchVal = message.Text;
                    var channels = _channelManger.Search(message.Text,0);
                    setLastSkip(user, channels);//getLinkRateVal
                    _rc.sendChannels(channels, message.Chat.Id);
                }
                else if (commandCode == 255 && user.CommandCode == getLinkRateVal)
                {

                    var channel = _channelManger.GetChannelByLink(message.Text.GetLinkId());
                    if (channel == null)
                    {
                        _rc.SendNoExistChannel(message.Chat.Id);
                        return;
                    }
                    _rc.SendRateLink(message.Chat.Id, channel);
                }
                else
                {
                    _rc.ShowMessageWithMainCommand(Setting.to_Continue_select_Command, message.Chat.Id);
                    //  throw new Exception("دستوری به سیستم رسیده که برای اجرای آن پیش بینی انجام نشده" );
                }
            }
            catch (Exception ex)
            {
                //string innerMsg = ex.InnerException != null ? ex.InnerException.Message : "";
                //System.IO.File.AppendAllText(Setting.logFileName + "\\ChannelLog" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", ex.Message + "innerMsg: " + innerMsg + "\r\n message Text : " +
                //    message.Text + "\r\n user commandCode: " + oldCommandCode + " StateInstraction:  " + StateInstraction +
                //    " EntityId1: " + EntityId1 + " ChatId: " + message.Chat.Id + " time: " + DateTime.Now.ToString());
            }
        }

        private void reqRate(Message message, UserModel user, string link)
        {
            var channel = _channelManger.GetChannelByLink(link);
            if (channel == null)
            {
                _rc.SendNoExistChannel(message.Chat.Id);
                return;
            }

            var user_channel = _channelManger.GetUser_Channel(user.ChatId, channel.Id);
            if (user_channel != null)
            {
                _rc.ShowMessageWithMainCommand(Setting.you_add_rate_after, user.ChatId);
                return;
            }

            user.EntityIdSkip = channel.Id;
            user.StateInstraction = ChannelManager.RateState.NameGetState;
            _rc.ShowOptionRate(message.Chat.Id);
            //return;
        }

        internal static void resetUserState(UserModel user)
        {
            user.CommandCode = mainMenuVal;//jabama
            user.EntityIdSkip = 0;//jabama
            user.GroupEntityId = 0;//jabama
            user.Order = 0;
            user.StateInstraction = 0;
            user.searchVal = "";
        }

        private static void setLastSkip(UserModel user, List<Channel> channels)
        {
            user.EntityIdSkip += channels.Count;
        }



        public void AddlinkFromTextFile()
        {
            int i = 0;
            ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
            // string contents = System.IO.File.ReadAllText("links.txt",Encoding.UTF8);
            var contents1 = System.IO.File.ReadAllLines("links.txt", Encoding.UTF8);
            foreach (var line in contents1)
            {
                Channel channel = new Channel();
                try
                {
                    var cols = line.Replace("\t", "").Split(',');

                    string groupName = cols[2];
                    channel.GroupId = _channelBotEntities.Groups.FirstOrDefault(g => g.Name.Contains(groupName) | groupName.Contains(g.Name)).Id;
                    channel.Link = cols[0].GetLinkId();
                    channel.Description = cols[1];
                    channel.StateAdd = ChannelManager.TableStateAdd.userAdd;
                    channel.ShowOrder = ChannelManager.ShowOrder.first;
                    channel.ChatIdAdder = 47653753;
                    channel.DateTimeAdd = DateTime.Now;
                    _channelBotEntities.Channels.Add(channel);
                    i++;
                    _channelBotEntities.SaveChanges();
                }
                catch (Exception ex)
                {
                    _channelBotEntities.Channels.Remove(channel);
                    i--;
                    //throw;
                }
            }

        }

        public string TestSql()
        {
            ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
            return _channelBotEntities.Groups.FirstOrDefault().Name;
        }
    }
}
