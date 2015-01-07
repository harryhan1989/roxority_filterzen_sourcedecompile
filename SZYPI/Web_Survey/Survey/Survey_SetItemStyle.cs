using LoginClass;
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SetItemStyle : Page, IRequiresSessionState
    {
        public long IID;
        public string sClientJs = "";
        public long  SID;

        protected string getUserDir(long UID1)
        {
            string str = "";
            str = base.Server.MapPath("UserData/U" + UID1.ToString());
            str = "http://" + base.Request.ServerVariables["HTTP_HOST"] + base.Request.ServerVariables["URL"];
            return (str.Substring(0, str.LastIndexOf("/") + 1) + "UserData/U" + UID1.ToString() + "/");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        Label_0017:
            long uID = 0;
            //new loginClass().checkLogin(out uID, "0");
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            this.IID = Convert.ToInt64(base.Request.QueryString["IID"]);
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            this.sClientJs = "var SID=" + this.SID.ToString() + ";var IID=" + this.IID.ToString() + ";var blnEditHTML = false;";
            int num2 = 1;
        Label_0002:
            switch (num2)
            {
                case 0:
                    break;

                case 1:
                    if (Convert.ToString(this.Session["Limits3"]).IndexOf("HTMLEdit") < 0)
                    {
                        break;
                    }
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    this.sClientJs = this.sClientJs + "blnEditHTML=true;";
                    num2 = 0;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            this.sClientJs = this.sClientJs + "var sRoot='u" + uID.ToString() + "'\n";
            this.sClientJs = this.sClientJs + "sPath='" + this.getUserDir(uID) + "';\n";
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
