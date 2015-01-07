using GetJs;
using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_QueryState : Page, IRequiresSessionState
    {
        public string sClientJs = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            long uID = 0;
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            long num2 = Convert.ToInt64(base.Request.QueryString["SID"]);
            this.sClientJs = "\nsSource='QS';";
            GetJs.GetJs js = new GetJs.GetJs(num2, uID);
            this.sClientJs = "var SID=" + num2.ToString() + ";" + js.getItemAndOptionArr() + "\nvar sSource='QS';";
        }

        protected HttpApplication ApplicationInstance
        {
            get
            {
                return this.Context.ApplicationInstance;
            }
        }

        protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile)this.Context.Profile;
            }
        }
    }
}

