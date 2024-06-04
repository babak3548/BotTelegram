using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel
{
    public class ChannelManager
    {
        public const byte PageSize = 8;

        CategoryManager _categoryManager;

        static List<Channel> _channels;
        public class RateState
        {
            public const byte NameReqState = 1;
            public const byte NameGetState = 2;
            public const byte RateGet = 3;
        }

        public class AddState
        {
            public const byte LinkReq = 1;
            public const byte CatReq = 2;
            public const byte DesReq = 3;
            public const byte DesGet = 4;
        }

        public class ShowOrder
        {
            public const byte zero = 0;
            public const byte first = 1;
            public const byte second = 2;
            public const byte thrid = 3;
        }
        public class TableStateAdd
        {
            public const byte userAdd = 0;
            public const byte accept = 1;
            public const byte blackList = 3;
            public const byte under100 = 6;

        }
       static List<Channel> channels
        {
            get
            {
                if (_channels == null)
                {
                    ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
                    _channels = new List<Channel>();
                    _channels = _channelBotEntities.Channels.Where(c => c.StateAdd == TableStateAdd.accept).ToList();
                }
                return _channels;
            }
        }
        static int _channelsCount=0;
        public static int? channelsCount
        {
            get
            {
                if (_channelsCount == 0)
                {
                    _channelsCount=channels.Count;
                }
                return _channelsCount;
            }
        }
        public ChannelManager()
        {

            _categoryManager = new CategoryManager();
        }
        public List<Channel> Search(string valuee,int skip)
        {
            var arr = valuee.Trim().Split(' ').Where(a=>a!="" && a.Length > 1 && a != "و" && a != "از"  ).ToArray();
            IEnumerable<Channel> ich ;
            if (arr.Length == 1)
            {
                ich = channels.Where(ch => ch.Link.Contains(arr[0]) || (ch.Description != null && ch.Description.Contains(arr[0]))
                 || (ch.Name != null && ch.Name.Contains(arr[0])));
            }
            else if (arr.Length>=2)
                ich = channels.Where(ch => ch.Link.Contains(arr[0])|| ch.Link.Contains(arr[1])
                || (ch.Description != null && (ch.Description.Contains(arr[0])|| ch.Description.Contains(arr[1])))
                 || (ch.Name != null && (ch.Name.Contains(arr[0])|| ch.Name.Contains(arr[1]))));
            else {
                ich =new List<Channel>();
            }
         return   ich.OrderByDescending(ch => ch.ShowOrder).ThenByDescending(ch => ch.Member)
                  .Skip(skip).Take(PageSize).ToList();


        }
        public List<Channel> ShowMore(int? groupId, int skip, byte orderBy)
        {
            if (groupId == null || groupId == 0)
            {
                if (orderBy == CommandManager.orderByStars)
                {
                    //.OrderByDescending(ch => ch.DateTimeAdd).Take(50)
                    return channels.OrderByDescending(ch => ch.ShowOrder).ThenByDescending(ch => ch.User_Channel.Sum(uc => uc.Star))
                        .Skip(skip).Take(PageSize).ToList();
                }
                else if (orderBy == CommandManager.orderByRecent)
                {
                    return channels.OrderByDescending(ch => ch.DateTimeAdd).Take(50).OrderByDescending(ch => ch.ShowOrder)
                        .ThenByDescending(ch => ch.Member).Skip(skip).Take(PageSize).ToList();
                }
                else
                {
                    return channels.OrderByDescending(ch => ch.ShowOrder).ThenByDescending(ch => ch.Member)
.Skip(skip).Take(PageSize).ToList();

                }
            }
            else
            {
                if (orderBy == CommandManager.orderByStars)
                {
                    return channels.Where(ch => ch.GroupId == groupId)
                        .OrderByDescending(ch => ch.ShowOrder).ThenByDescending(ch => ch.User_Channel.Sum(uc => uc.Star))
                        .Skip(skip).Take(PageSize).ToList();
                }
                else if (orderBy == CommandManager.orderByRecent)
                {
                    return channels.Where(ch => ch.GroupId == groupId)
.OrderByDescending(ch => ch.DateTimeAdd).Take(50).OrderByDescending(ch => ch.ShowOrder).ThenByDescending(ch => ch.Member)
.Skip(skip).Take(PageSize).ToList();
                }
                else
                {
                    return channels.Where(ch => ch.GroupId == groupId)
        .OrderByDescending(ch => ch.ShowOrder).ThenByDescending(ch => ch.Member)
        .Skip(skip).Take(PageSize).ToList();

  
                }
            }


        }

        public Channel CreateChannel(string link, int chatIdAdder)
        {
            try
            {
                ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
                Channel channel = new Channel();
                channel.ChatIdAdder = chatIdAdder;
                channel.Link = link;
                channel.ShowOrder = ShowOrder.zero;
                channel.StateAdd = TableStateAdd.userAdd;
                channel.DateTimeAdd = DateTime.Now;
                channel.GroupId = 9;
                _channelBotEntities.Channels.Add(channel);
                _channelBotEntities.SaveChanges();
                return channel;
            }
            catch (Exception ex)
            {
                //to do log
                return null;
            }
        }
        public bool AddDescription(int channelId, string description)
        {
            try
            {
                ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
                var channel = _channelBotEntities.Channels.FirstOrDefault(ch => ch.Id == channelId);
                if (channel == null)
                    throw new Exception("کانالی با آی دی ارسالی موجود نیست" + " channelId: " + channelId);

                channel.Description = description.Replace("ى", "ی"); 

                _channelBotEntities.SaveChanges();

                //_channels = null;//باعث لود مجدد کانال ها می شود
                return true;

            }
            catch (Exception ex)
            {
                //to do
                return false;
            }
        }

        private void addChannelToList(Channel channel)
        {

            _channels.Add(channel);
        }

        internal Channel GetChannelByLink(string link)
        {
            link = link.GetLinkId();//jabama
            return channels.FirstOrDefault(ch => ch.Link == link);
        }
        internal Channel GetChannelByLinkDb(string link)
        {
            link = link.GetLinkId();//jabama
            ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
            return _channelBotEntities.Channels.FirstOrDefault(ch => ch.Link == link);
        }
        internal Channel GetChannelById(int id)
        {
            return channels.FirstOrDefault(ch => ch.Id == id);
        }
        internal User_Channel GetUser_Channel(int chatId, int channelId)
        {
            return channels.FirstOrDefault(c => c.Id == channelId).User_Channel.FirstOrDefault(uc => uc.ChatId == chatId);
        }

        internal void AddRateChannel(Channel channel, int ChatId, byte star)
        {
            ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
            var u_ch = new User_Channel();
            u_ch.ChannelId = channel.Id;
            u_ch.ChatId = ChatId;
            u_ch.Star = star;
            _channelBotEntities.User_Channel.Add(u_ch);
            _channelBotEntities.SaveChanges();
            // _channels = null;//باعث لود مجدد کانال ها می شود
        }

        internal bool SetCatChannel(int id, string catName)
        {
            ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
            var channel = _channelBotEntities.Channels.FirstOrDefault(ch => ch.Id == id);
            var group = _categoryManager.GetCategoryByName(catName);
            if (channel == null || group == null)
                throw new Exception("خطای اضافه کردن گروه به کانال");
            channel.GroupId = group.Id;
            _channelBotEntities.SaveChanges();
            return true;
        }

        internal void ChangeSate(int id, byte accept)
        {
            ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
            var channel = _channelBotEntities.Channels.FirstOrDefault(ch => ch.Id == id);
            if (channel != null)
                channel.StateAdd = accept;
            _channelBotEntities.SaveChanges();
        }
    }

    public class ChannelModel
    {
        public string Link { get; set; }
        public string Descrption { get; set; }

    }

}
