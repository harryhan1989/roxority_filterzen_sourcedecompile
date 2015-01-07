using LoginClass;
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_EditHeadAndFoot : Page, IRequiresSessionState
    {
        public string sClientJs = "";
        public long SID;
        public string sStyle = "";
        public short t;

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
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            this.t = Convert.ToInt16(base.Request.QueryString["t"]);
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (this.SID != 0)
                    {
                        break;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    base.Response.End();
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    break;

                default:
                    goto Label_0017;
            }
            this.sClientJs = "var SID=" + this.SID.ToString() + "\n";
            this.sClientJs = this.sClientJs + "var t=" + this.t.ToString() + "\n";
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
