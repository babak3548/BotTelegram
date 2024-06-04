using Channel;
using PublicChatBot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Services;

namespace webapi.Controllers
{
    public class homeController : Controller
    {
        // GET: home

        public ActionResult Index()
        {
            System.IO.File.AppendAllText(PublicChatBot.Setting.logFileName + "\\home.txt", "reqest time Index" + DateTime.Now + "\r\n");
            //Telegram.Bot.Types.Message m = new Telegram.Bot.Types.Message();
            return View();
        }
        public ActionResult test(string text)
        {
            string reg = @"[a-z]+[0-9].*?(?=\s)";
            text = Regex.Replace(text, reg, "$$$$$");
            return View();
        }
        public ActionResult testDb()
        {
            CommandManager _commandManager = new CommandManager();
            ViewBag.res = _commandManager.TestSql();
           
            return View();
        }
        public ActionResult StateBot()
        {
           
            ViewData["commandStateList"] = Command.CountCommandStateList();
            ViewData["Setting_logFileName"] = PublicChatBot.Setting.logFileName;
            ViewData["GetFlagDecide"] = DecideToBroadcast.getFlagDecide();
            return View();
        }//disablePlusCommandTest
        public ActionResult TagglePath(int command)
        {
            ManageRequest.disablePlusCommandTest = command;
            return View();
        }
    }
}
