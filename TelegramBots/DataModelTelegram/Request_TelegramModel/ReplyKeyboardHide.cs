﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModelTelegram.Request_TelegramModel
{
    /// <summary>
    /// Upon receiving a message with this object, Telegram clients will hide the current custom keyboard and display the default 
    /// letter-keyboard. By default, custom keyboards are displayed until a new keyboard is sent by a bot. An exception is made 
    /// for one-time keyboards that are hidden immediately after the user presses a button (see ReplyKeyboardMarkup).
    /// </summary>
   public class ReplyKeyboardHide
    {
       /// <summary>
        /// Requests clients to hide the custom keyboard
       /// </summary>
       public bool hide_keyboard;
       /// <summary>
       /// Optional. Use this parameter if you want to hide keyboard for specific users only. Targets: 1) users that are @mentioned in the
       /// text of the Message object; 2) if the bot's message is a reply (has reply_to_message_id), sender of the original message.
       /// Example: A user votes in a poll, bot returns confirmation message in reply to the vote and hides keyboard for that user,
       /// while still showing the keyboard with poll options to users who haven't voted yet.
       /// </summary>
       public bool selective;

    }
}
