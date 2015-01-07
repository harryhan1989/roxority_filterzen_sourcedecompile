using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;
using WebUI;

namespace Web_Survey.Survey
{
    public class Survey_Default3 : BasePage, IRequiresSessionState
    {
        public string sClientJs = "var SurveyListPageNo = '';\n";

        protected void Page_Load(object sender, EventArgs e)
        {
        Label_0017:
            long uID = 0;
            long num2 = 0;
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num2 = Convert.ToInt64(base.Request.QueryString["SID"]);
            string sClientJs = this.sClientJs;
            this.sClientJs = sClientJs + "SurveyListPageNo='" + Convert.ToString(base.Request.QueryString["PageNo"]) + "';\nvar SID=" + num2.ToString() + ";\n";

            //SqlDataReader reader = new OleDbCommand("SELECT TOP 1 SID FROM SurveyTable WHERE SID=" + num2.ToString() + " AND UID=" + uID.ToString()).ExecuteReader();
            SqlDataReader reader = new Survey_Default3_Layer().GetSurveyTableSID(num2.ToString(), uID.ToString());
            int num3 = 1;
        Label_0002:
            switch (num3)
            {
                case 0:
                    reader.Close();
                    base.Response.Write("未找到问卷");
                    base.Response.End();
                    num3 = 2;
                    goto Label_0002;

                case 1:
                    if (reader.Read())
                    {
                        break;
                    }
                    num3 = 0;
                    goto Label_0002;

                case 2:
                    break;

                default:
                    goto Label_0017;
            }
            reader.Close();
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

