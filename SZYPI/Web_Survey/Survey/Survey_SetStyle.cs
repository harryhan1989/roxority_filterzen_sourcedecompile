using LoginClass;
using shareclass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using BusinessLayer.Survey;
using System.Data.SqlClient;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SetStyle : Page, IRequiresSessionState
    {
        protected HtmlInputHidden body_1;
        protected HtmlInputHidden CheckBoxCSS_1;
        protected HtmlInputHidden CheckCode_1;
        protected HtmlInputHidden ExpandContentStyle_1;
        protected HtmlInputHidden InputCSS_1;
        protected HtmlInputHidden ItemBar_1;
        protected HtmlInputHidden ItemBox_1;
        protected HtmlInputHidden ItemContent_1;
        protected HtmlInputHidden ItemName_1;
        protected HtmlInputHidden ItemPitch_1;
        protected HtmlInputHidden ListMulitCSS_1;
        protected HtmlInputHidden MatrixInputCSS_1;
        protected HtmlInputHidden MatrixSelectCSS_1;
        protected HtmlInputHidden NextPage_1;
        protected HtmlInputHidden NumInputCSS_1;
        protected HtmlInputHidden OneTimePSW_1;
        protected HtmlInputHidden OptionName_1;
        protected HtmlInputHidden OtherInputCSS_1;
        protected HtmlInputHidden PageBox_1;
        protected HtmlInputHidden PageContent_1;
        protected HtmlInputHidden PageFoot_1;
        protected HtmlInputHidden PageHead_1;
        protected HtmlInputHidden PercentInputCSS_1;
        protected HtmlInputHidden RadioCSS_1;
        protected HtmlInputHidden Result;
        public string sClientJs = "";
        protected HtmlInputHidden SelectCSS_1;
        protected HtmlInputHidden sid1;
        public long SID;
        protected HtmlInputHidden SubmitBT_1;
        protected HtmlInputHidden SurveyName_1;
        protected HtmlInputHidden TextAreaCSS_1;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            string str2;
            long num;
            long num2 = 0; //∏≥≥ı÷µ
            SqlDataReader reader = null; //∏≥≥ı÷µ
            int num3;
            goto Label_0033;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (!reader.Read())
                    {
                        goto Label_022D;
                    }
                    num3 = 3;
                    goto Label_0002;

                case 1:
                    goto Label_0186;

                case 2:
                    new NotClass(this.SID, num).fromStyleLibCopy(num2, this.SID, num);

                    this.sClientJs = "ID=1;\r\n";
                    num3 = 1;
                    goto Label_0002;

                case 3:
                    str2 = reader[0].ToString();
                    num3 = 8;
                    goto Label_0002;

                case 4:
                    try
                    {
                        num2 = Convert.ToInt64(base.Request.QueryString["ID"]);
                    }
                    catch
                    {
                    }
                    num3 = 5;
                    goto Label_0002;

                case 5:
                    if (num2 <= 0)
                    {
                        goto Label_0186;
                    }
                    num3 = 2;
                    goto Label_0002;

                case 6:
                    if (this.SID != 0)
                    {
                        goto Label_0134;
                    }
                    num3 = 7;
                    goto Label_0002;

                case 7:
                    base.Response.End();
                    num3 = 9;
                    goto Label_0002;

                case 8:
                    goto Label_022D;

                case 9:
                    goto Label_0134;
            }
        Label_0033:
            str2 = "";
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            num = 0;
            num = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num3 = 6;
            goto Label_0002;
        Label_0134:
            num2 = 0;
            num3 = 4;
            goto Label_0002;
        Label_0186: ;
            //command = new OleDbCommand("SELECT TOP 1 ExpandContent FROM SurveyExpand WHERE SID=" + this.SID.ToString() + " AND UID=" + num.ToString() + " AND ExpandType=9", connection);
            reader = new Survey_SetStyle_Layer().GetSurveyExpand(this.SID.ToString(), num.ToString());
            num3 = 0;
            goto Label_0002;
        Label_022D:
            reader.Dispose();
            this.Result.Value = str2;
            (Page.FindControl("sid") as HtmlInputHidden).Value = this.SID.ToString();
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
