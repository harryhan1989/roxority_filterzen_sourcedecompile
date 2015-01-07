using LoginClass;
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

namespace Web_Survey.Survey
{
    public class Survey_GetItem : Page, IRequiresSessionState
    {
        public int IID;
        public int SID;
        public string sItemHTML = "";
        public string sStyle = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;

            SqlDataReader reader = null; //∏≥≥ı÷µ
            int num2;
            goto Label_0037;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (this.IID != 0)
                    {
                        goto Label_0179;
                    }
                    num2 = 4;
                    goto Label_0002;

                case 1:
                    this.sStyle = reader[0].ToString();
                    num2 = 3;
                    goto Label_0002;

                case 2:
                    this.sItemHTML = reader[0].ToString();
                    num2 = 6;
                    goto Label_0002;

                case 3:
                    goto Label_00DC;

                case 4:
                    goto Label_025F;

                case 5:
                    num2 = 0;
                    goto Label_0002;

                case 6:
                    goto Label_02A3;

                case 7:
                    if (!reader.Read())
                    {
                        goto Label_00DC;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 8:
                    goto Label_0179;

                case 9:
                    if (!reader.Read())
                    {
                        goto Label_02A3;
                    }
                    num2 = 2;
                    goto Label_0002;

                case 10:
                    if (this.SID == 0)
                    {
                        goto Label_025F;
                    }
                    num2 = 5;
                    goto Label_0002;
            }
        Label_0037:
            long uID = 0;
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            this.IID = Convert.ToInt32(base.Request.QueryString["IID"]);
            this.SID = Convert.ToInt32(base.Request.QueryString["SID"]);
            num2 = 10;
            goto Label_0002;
        Label_00DC:
            reader.Close();
            //command.CommandText = "SELECT TOP 1 ItemHTML FROM ItemTable WHERE SID=" + this.SID.ToString() + " AND UID=" + uID.ToString() + " AND IID=" + this.IID.ToString();
            reader = new Survey_GetItem_Layer().GetItemTable(this.SID.ToString(), uID.ToString(), this.IID.ToString());
            num2 = 9;
            goto Label_0002;
        Label_0179:
            //command.CommandText = "SELECT TOP 1 ExpandContent FROM SurveyExpand WHERE SID=" + this.SID.ToString() + " AND UID=" + uID.ToString() + " AND ExpandType=9";
            reader = new Survey_GetItem_Layer().GetSurveyExpand(this.SID.ToString(), uID.ToString());
            num2 = 7;
            goto Label_0002;
        Label_025F:
            base.Response.End();
            num2 = 8;
            goto Label_0002;
        Label_02A3:
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
