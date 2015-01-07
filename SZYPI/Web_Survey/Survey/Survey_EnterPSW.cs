using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Web_Survey.Survey
{
    public class Survey_EnterPSW : Page, IRequiresSessionState
    {
        protected Button Button1;
        protected HtmlForm form1;
        protected Label Label1;
        protected TextBox PSW;

        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = Convert.ToString(base.Request.QueryString["URL"]);
            string str2 = Convert.ToString(base.Request.QueryString["e"]);
            base.Form.DefaultButton = this.Button1.UniqueID;
            this.Button1.PostBackUrl = str;
            this.Label1.Text = str2;
            this.PSW.Focus();
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
