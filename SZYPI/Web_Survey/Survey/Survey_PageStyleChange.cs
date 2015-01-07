using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_PageStyleChange : Page, IRequiresSessionState
    {
        public string sClientJs = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            long uID = 0;
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            string str = base.Request.QueryString["SID"].ToString();
            string s = base.Request.QueryString["P"].ToString();
            s = base.Server.HtmlDecode(s);
            this.sClientJs = "EditSurveyHTML.aspx?SID=" + str.ToString();
            //OleDbCommand command = new OleDbCommand("UPDATE SurveyTable SET TempPage='" + s + "' WHERE UID=" + uID.ToString() + " AND SID=" + str, connection);
            new Survey_PageStyleChange_Layer().UpdateSurveyTable(s, uID.ToString(), str);
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

