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
    public class Survey_getStyle : Page, IRequiresSessionState
    {
        public string sContent = "";
        public string sStyle = "";
        public string sStyleName = "PageHead";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            long num;
            long num2;
            string str2;
            short num3;
            SqlDataReader reader;
            int num4;
            goto Label_0053;
        Label_0002:
            switch (num4)
            {
                case 0:
                    this.sContent = reader[num3].ToString();
                    num4 = 3;
                    goto Label_0002;

                case 1:
                    this.sContent = "<div class='SurveyName'>" + str2 + "</div>";
                    num4 = 13;
                    goto Label_0002;

                case 2:
                    this.sContent = "<div class='PageHead'>" + this.sContent + "</div>";
                    num4 = 15;
                    goto Label_0002;

                case 3:
                    goto Label_0374;

                case 4:
                    if (num3 != 0)
                    {
                        goto Label_026F;
                    }
                    num4 = 1;
                    goto Label_0002;

                case 5:
                case 15:
                    return;

                case 6:
                    this.sStyle = reader[0].ToString();
                    num4 = 0x11;
                    goto Label_0002;

                case 7:
                    if (!reader.Read())
                    {
                        goto Label_0345;
                    }
                    num4 = 14;
                    goto Label_0002;

                case 8:
                    if (num3 != 0)
                    {
                        this.sStyleName = "PageFoot";
                        this.sContent = "<div class='PageFoot'>" + this.sContent + "</div>";
                        num4 = 5;
                    }
                    else
                    {
                        num4 = 2;
                    }
                    goto Label_0002;

                case 9:
                    if (!reader.Read())
                    {
                        goto Label_01B4;
                    }
                    num4 = 6;
                    goto Label_0002;

                case 10:
                    if (!reader.Read())
                    {
                        goto Label_0374;
                    }
                    num4 = 0;
                    goto Label_0002;

                case 11:
                    if (!(this.sContent == ""))
                    {
                        goto Label_026F;
                    }
                    num4 = 12;
                    goto Label_0002;

                case 12:
                    //command.CommandText = "SELECT TOP 1 SurveyName FROM SurveyTable WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString();
                    reader = new Survey_getStyle_Layer().GetSurveyTable(num.ToString(), num2.ToString());
                    num4 = 7;
                    goto Label_0002;

                case 13:
                    goto Label_026F;

                case 14:
                    str2 = reader[0].ToString();
                    num4 = 0x10;
                    goto Label_0002;

                case 0x10:
                    goto Label_0345;

                case 0x11:
                    goto Label_01B4;
            }
        Label_0053:
            num = 0;
            num2 = 0;
            num2 = ConvertHelper.ConvertLong(this.Session["UserID"]);
            str2 = "";
            num3 = 0;
            num = Convert.ToInt32(base.Request.QueryString["SID"]);
            num3 = Convert.ToInt16(base.Request.QueryString["t"]);
            //command.CommandText = "SELECT TOP 1 ExpandContent FROM SurveyExpand WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString() + " AND ExpandType=9";

            reader = new Survey_getStyle_Layer().GetSurveyExpand(num.ToString(), num2.ToString());

            num4 = 9;
            goto Label_0002;
        Label_01B4:
            reader.Close();
            //command.CommandText = "SELECT TOP 1 PageHead,PageFoot FROM HeadFoot WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString();
            reader = new Survey_getStyle_Layer().GetHeadFoot(num.ToString(), num2.ToString());
            if ((1 != 0) && (0 != 0))
            {
            }
            num4 = 10;
            goto Label_0002;
        Label_026F:
            num4 = 8;
            goto Label_0002;
        Label_0345:
            reader.Close();
            num4 = 4;
            goto Label_0002;
        Label_0374:
            reader.Close();
            num4 = 11;
            goto Label_0002;
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

