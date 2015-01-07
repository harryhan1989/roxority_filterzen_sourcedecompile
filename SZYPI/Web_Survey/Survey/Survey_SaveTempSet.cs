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
    public class Survey_SaveTempSet : Page, IRequiresSessionState
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long num = 0;
            long uID = 0;
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            string str = "";
            num = Convert.ToInt64(base.Request.Form["SID"]);
            str = Convert.ToString(base.Request.Form["TempList"]);
            //command.CommandText = "UPDATE SurveyTable SET TempPage='" + str + "' WHERE SID=" + num.ToString() + " AND UID=" + uID.ToString();
            new Survey_ApplyTemp_Layer().UpdateSurveyTable_Save(str, num.ToString(), uID.ToString());

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
