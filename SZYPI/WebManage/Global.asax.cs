using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebManage
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Nandasoft.FrameworkConfig.ConnectionInfo.DataSource = System.Configuration.ConfigurationManager.AppSettings["dbServer"];
            Nandasoft.FrameworkConfig.ConnectionInfo.Database = System.Configuration.ConfigurationManager.AppSettings["databaseName"];
            Nandasoft.FrameworkConfig.ConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["user"];
            Nandasoft.FrameworkConfig.ConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["password"];
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.Url.ToString().Contains("'"))
            {
                HttpContext.Current.Response.End();
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}