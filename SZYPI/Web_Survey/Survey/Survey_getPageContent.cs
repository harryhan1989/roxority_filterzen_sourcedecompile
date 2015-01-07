using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using BusinessLayer.Survey;
using System.Data.SqlClient;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_getPageContent : Page, IRequiresSessionState
    {
        public string sContent = "";
        public string sStyle = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            SqlDataReader reader;
            int num4;
            goto Label_0023;
        Label_0002:
            switch (num4)
            {
                case 0:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (!reader.Read())
                    {
                        goto Label_015D;
                    }
                    num4 = 5;
                    goto Label_0002;

                case 1:
                    goto Label_020F;

                case 2:
                    if (!reader.Read())
                    {
                        goto Label_020F;
                    }
                    num4 = 3;
                    goto Label_0002;

                case 3:
                    this.sContent = reader[0].ToString();
                    num4 = 1;
                    goto Label_0002;

                case 4:
                    goto Label_015D;

                case 5:
                    this.sStyle = reader[0].ToString();
                    num4 = 4;
                    goto Label_0002;
            }
        Label_0023:
            long num = 0;
            long uID = 0;
            int num3 = 0;
            uID=ConvertHelper.ConvertLong(this.Session["UserID"]);
            num = Convert.ToInt32(base.Request.QueryString["SID"]);
            num3 = Convert.ToInt32(base.Request.QueryString["PID"]);
            //command.CommandText = "SELECT TOP 1 ExpandContent FROM SurveyExpand WHERE SID=" + num.ToString() + " AND UID=" + uID.ToString() + " AND ExpandType=1";

            reader = new Survey_EditPage_Layer().GetSurveyExpand(num.ToString(), uID.ToString());
            num4 = 0;
            goto Label_0002;
        Label_015D:
            reader.Close();
            //command.CommandText = "SELECT TOP 1 PageContent FROM PageTable WHERE SID=" + num.ToString() + " AND UID=" + uID.ToString() + " AND PID=" + num3.ToString();
            reader = new Survey_EditPage_Layer().GetPageTable(num.ToString(), uID.ToString(), num3.ToString());
            num4 = 2;
            goto Label_0002;
        Label_020F:
            reader.Close();
            this.sContent = "<span class='PageContent'>" + this.sContent + "</span>";
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
