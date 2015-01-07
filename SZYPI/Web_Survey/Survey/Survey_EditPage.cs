using LoginClass;
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_EditPage : Page, IRequiresSessionState
    {
        public int PID;
        public string sClientJs = "";
        public long SID;
        public string sStyle = "";
        public long UID;

        protected void Page_Load(object sender, EventArgs e)
        {
        Label_0017:
   
            string str = "";
            UID=ConvertHelper.ConvertLong(this.Session["UserID"]);
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            this.PID = Convert.ToInt32(base.Request.QueryString["PID"]);
            int num = 1;
        Label_0002:
            switch (num)
            {
                case 0:
                    base.Response.End();
                    num = 2;
                    goto Label_0002;

                case 1:
                    if (this.SID > 0)
                    {
                        break;
                    }
                    num = 0;
                    goto Label_0002;

                case 2:
                    break;

                default:
                    goto Label_0017;
            }
            str = base.Server.MapPath("UserData/U" + this.UID.ToString());
            str = "http://" + base.Request.ServerVariables["HTTP_HOST"] + base.Request.ServerVariables["URL"];
            str = str.Substring(0, str.LastIndexOf("/") + 1) + "UserData/U" + this.UID.ToString() + "/";
            this.sClientJs = this.sClientJs + "var sPath='" + str + "';\n";
            this.sClientJs = "var SID=" + this.SID.ToString() + "\n";
            string sClientJs = this.sClientJs;
            this.sClientJs = sClientJs + "var sRoot='u" + this.UID.ToString() + "'\nvar sPath='" + str + "';var PID=" + this.PID.ToString();
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
