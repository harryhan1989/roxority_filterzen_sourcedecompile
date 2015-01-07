using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BLL;

namespace WebUI
{
    public partial class head : System.Web.UI.Page
    {
        /// <summary>
        /// “≥√Ê‘ÿ»Î
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {                
                lblPersonName.Text = SessionState.UserName;
                Hidden1.Value = "../../Index.aspx";
            }
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            SessionState.OutLogon();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Logon", "window.parent.parent.self.location = '../../Login.aspx';", true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnHomePage_Click(object sender, EventArgs e)
        {
            SessionState.OutLogon();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Logon", "window.parent.parent.self.location = '../../Web/Web/Index.aspx';", true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkToHomePage()
        {
            SessionState.OutLogon();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Logon", "window.parent.parent.self.location = '../../Web/Web/Index.aspx';", true);
        }
    }
}
