using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.IO;

namespace webapi
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }
                protected void Application_BeginRequest()
        {
             //System.Web.HttpContext.Current.Request.Path
            // Debug.Write("call from: " + HttpContext.Current.Request.Path);
           // saveLog("url :"+Request.Url.ToString()+"\n* IsFile:"+Request.Url.IsFile.ToString()+"\n * Type :"+Request.RequestType+
            //    "\n *Request.Files.Count: " + Request.Files.Count);
           /* if (!Request.Url.Host.StartsWith("www") && !Request.Url.IsLoopback)
            {
                UriBuilder builder = new UriBuilder(Request.Url);
                builder.Host = "www." + Request.Url.Host;
                Response.Redirect(builder.ToString(), true);
            }*/
        }

                private void saveLog(string log)
                {
                    string  logPath = Path.Combine(Server.MapPath("~/Images/"), "logGlobal.txt");
                    System.IO.File.AppendAllText(logPath, log);
                }
       //     [Conditional("Release")]
                protected void Application_Error(object sender, EventArgs e)
                { 
                
                }
    }
}