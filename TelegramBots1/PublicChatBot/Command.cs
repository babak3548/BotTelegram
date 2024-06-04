using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicChatBot
{

    public class Command
    {
        // 👎👍👥👤 ⏩💰💵 ➕🔗🔧🔆⚡❄👍👎❤💔
        public static string start = "start";//show /start 41143480
        public static string addContent = "add ➕";//show - have step
        public static string acceptContent = "accept content";//no show
        public static string nextContent = "next ⏩";//no show
        public static string topRate = "top rate 💰";//show  
        public static string myRate = "my rate 💵";//show
        public static string flowers = "flowers";//show
        public static string shearWithFirend = "share ❄";//show
        public static string aboutMe = "about me 👤";//show
        public static string mainCommand = "main command";//main

        public static string plus = "plus 👍";//show
        public static string minus = "minus 👎";//show

        static List<CommandState> commandStateList = new List<CommandState>();
        internal string AuthCommand(string value, short userRole)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            value.Replace("/", "");
            if (userRole == Roles.noReg) return start;
            else if (value == addContent) return addContent;
            else if (value == acceptContent) return acceptContent;
            else if (value == nextContent) return nextContent;
            else if (value == topRate) return topRate;
            else if (value == myRate) return myRate;
            else if (value == flowers) return flowers;
            else if (value == shearWithFirend) return shearWithFirend;
            else if (value.StartsWith(plus)) return plus;
            else if (value.StartsWith(minus)) return minus;
            else if (value == aboutMe) return aboutMe;
            else if (value == shearWithFirend) return mainCommand;
            else return "";
        }

        
        internal void addCommandStateUser(string botName, int chatIdUser, string command, string state)
        {
            CommandState commandState = getCommandStateUser(botName, chatIdUser);

            if (commandState != null)
            {
                commandState.command = command;
                commandState.state = state;
            }
            else
            {
                commandState = new CommandState(botName, chatIdUser, command, state);
            }
            commandStateList.Add(commandState);

            if ( Setting.logStatusSystem("تعداد کامند استست لیست زیاد شده است",
                Setting.maxCommandStateListCount, commandStateList.Count))
            {
                //مدت زمانی که استیت کامند منتظر دستور بعدیش می ماند
                commandStateList.RemoveAll(c => c.startTime + 180 * 1000 < Setting.getDateTimeNowInMil() );
               Setting.maxCommandStateListCount=  Setting.maxCommandStateListCount + 100;
            }

        }
        internal static CommandState getCommandStateUser(string botName, int chatIdUser)
        {
            CommandState commandState = commandStateList.FirstOrDefault(c => c.botName == botName && c.chatIdUser == chatIdUser);
            return commandState;
        }
        internal string getCommandByEffectState(string botName, int chatIdUser, string command)
        {
            CommandState commandState = getCommandStateUser(botName, chatIdUser);
            //زمانی استیت تاثیر گذار است که کامند خالی باشد
            if (!string.IsNullOrWhiteSpace(command) )
            {
                return command;
            }

            if (commandState == null)
            {
                return command;
            }
            if (commandState.command == addContent && commandState.state == StateAddContent.request.ToString())
            {
                commandStateList.Remove(commandState);
                return acceptContent;
            }
            else
            {
                return command;
            }
        }

        internal void addContentIdToCommandState(string botName, int chatIdUser, int contentId)
        {
            addCommandStateUser(botName,chatIdUser,acceptContent,"");
            CommandState commandState = getCommandStateUser(botName, chatIdUser);
            commandState.contentId=contentId;
        }
        internal int getContentIdCommandState(string botName, int chatIdUser) 
        {
            CommandState commandState = getCommandStateUser(botName, chatIdUser);
            if (commandState!=null)
            {
                commandStateList.Remove(commandState);
                return commandState.contentId; 
            }
            else
            {
                return -1;
            }
        }


        internal string getParamCommand(string command,string commandParam)
        {
            return commandParam.Replace("/", "").Replace(command, "").Trim();
        }

        internal bool isCommand(string value)
        {
            string result= AuthCommand(value, 1);
            if (string.IsNullOrWhiteSpace(result))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }


    public enum StateAddContent { request = 1 }

    public class CommandState
    {
        public string botName;
        public int chatIdUser;
        public string command;
        public string state;
        public int contentId;
        public Int64 startTime;
        public CommandState(string botName, int chatIdUser, string command, string state)
        {
            this.botName = botName;
            this.chatIdUser = chatIdUser;
            this.command = command;
            this.state = state;
            this.startTime = Setting.getDateTimeNowInMil();
        }

    }
}
