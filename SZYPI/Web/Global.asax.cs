using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using EntityModel.WebEntity;
using BusinessLayer.Web.Logic;
using Business.Helper;

namespace Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["daycount"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();

            List<TrafficStatisticsEntity> traEntity = TrafficStatisticsLogic.GetTrafficCount();

            Application["count"] = traEntity[0].TotalCount;

            Application["count"] = ConvertHelper.ConvertLong(Application["count"]) + 1;

            if (DateTime.Now.Hour==0 && DateTime.Now.Minute==0)
            {
                Application["daycount"] = 0;
            }
            else
            {
                Application["daycount"] = ConvertHelper.ConvertLong(Application["daycount"]) + 1;
            }

            Application.UnLock();

            if (Session["flage"] != null && ConvertHelper.ConvertBoolean(Session["flage"]) == false)
            {
                Session["flage"] = true;
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();

            Application["count"] = ConvertHelper.ConvertLong(Application["count"]) - 1;

            Application["daycount"] = ConvertHelper.ConvertLong(Application["daycount"]) - 1;

            Application.UnLock();

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}