//using LoginClass;
using BusinessLayer.ShareClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using System.Data;
using Business.Helper;
using shareclass;
using WebUI;

namespace Web_Survey.Survey
{
    public class Survey_CreateSurvey : BasePage, IRequiresSessionState
    {
        protected Button Button1;
        protected HtmlForm form1;
        //protected DropDownList Lan;
        //public loginClass lc = new loginClass();
        //protected SqlDataSource OQSS14;
        protected DropDownList SurveyType;
        protected RequiredFieldValidator RequiredFieldValidator1;
        public string sClientJs = "var blnCreated = false;\nvar SID = 0;\n";
        protected HiddenField StyleLibID;
        protected DropDownList SurveyClass;
        protected TextBox SurveyContent;
        protected TextBox SurveyName;
        public long UID;

        protected override void OnInit(EventArgs e)
        {
            setDropDownListBySurveyClass();
            base.OnInit(e);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str;
            int num;
            DateTime now;
            SqlDataReader reader = null; //赋初值
            long num2 = 0; //赋初值
            int num3;
            goto Label_0037;
        Label_0002:
            switch (num3)
            {
                case 0:
                    goto Label_0082;

                case 1:
                    try
                    {
                        num = Convert.ToInt32(this.SurveyClass.SelectedValue);
                    }
                    catch
                    {
                        num = 0;
                    }
                    num3 = 2;
                    goto Label_0002;

                case 2:
                    if (str == "")
                    {
                        return;
                    }
                    num3 = 4;
                    goto Label_0002;

                case 3:
                    goto Label_039A;

                case 4:
                    num3 = 5;
                    goto Label_0002;

                case 5:
                    if (num > -1)
                    {
                        string str2 = "Email:|IPToScreen:|CheckCode:|AnswerPSW:|PSW:|RecordIP:1|RecordTime:1|IP:|Cookies:|MemberLogin:|AnswerArea:|Quota:|GUIDAndDep:|TPaper:|TToAll:|TTC:|NeedConfirm:";
                        //command.CommandText = string.Concat(new object[] { "INSERT INTO SurveyTable(SurveyName,CreateDate,UID,ClassID,Par,Lan) VALUES('", str, "','", now, "',", this.UID.ToString(), ",", num.ToString(), ",'", str2, "',", this.Lan.SelectedValue, ")" });
                        new CreateSurvey_Layer().InsertSurveyTable(str, now, this.UID.ToString(), num.ToString(), str2, "1",this.SurveyType.SelectedValue);
                        //command.CommandText = " SELECT TOP 1 SID FROM SurveyTable ORDER BY SID DESC";
                        reader = new CreateSurvey_Layer().GetSIDBySurveyTable();
                        num2 = 0;
                        num3 = 9;
                    }
                    else
                    {
                        num3 = 6;
                    }
                    goto Label_0002;

                case 6:
                    return;

                case 7:
                    new NotClass( num2, this.UID).fromStyleLibCopy(Convert.ToInt32(this.StyleLibID.Value), num2, this.UID);
                    num3 = 3;
                    goto Label_0002;

                case 8:
                    num2 = Convert.ToInt64(reader[0]);
                    num3 = 0;
                    goto Label_0002;

                case 9:
                    if (!reader.Read())
                    {
                        goto Label_0082;
                    }
                    num3 = 8;
                    goto Label_0002;

                case 10:
                    if (Convert.ToInt32(this.StyleLibID.Value) <= 0)
                    {
                        goto Label_039A;
                    }
                    num3 = 7;
                    goto Label_0002;
            }
        Label_0037:
            str = this.SurveyName.Text.ToString().Trim();
            num = 0;
            now = DateTime.Now;
            num3 = 1;
            goto Label_0002;
        Label_0082:
            reader.Close();
            //command.CommandText = "INSERT INTO PageTable(UID,SID,PageNo,PageContent) VALUES(" + this.UID.ToString() + "," + num2.ToString() + ",1,'" + this.SurveyContent.Text.Trim() + "') ";
            new CreateSurvey_Layer().InsertPageTable(this.UID.ToString(), num2.ToString(), "1", this.SurveyContent.Text.Trim());
            //command.CommandText = " INSERT INTO HeadFoot(PageHead,UID,SID) VALUES('<div class=SurveyName>" + str + "</div>'," + this.UID.ToString() + "," + num2.ToString() + ")";
            new CreateSurvey_Layer().InsertHeadFoot("<div class=SurveyName>" + str + "</div>", this.UID.ToString(), num2.ToString());
            


            num3 = 10;
            goto Label_0002;
        Label_039A:
            this.sClientJs = "blnCreated=true;\nSID=" + num2.ToString() + ";\n";
        }

        public string createRan(short intMaxAmount)
        {
            Random random = new Random();
            return random.Next(intMaxAmount).ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Form.DefaultButton = this.Button1.UniqueID;
            this.SurveyName.Focus();
            //this.lc.checkLogin(out this.UID, "0");

            UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            //this.lc.checkLimits(Convert.ToString(this.Session["Limits3"]), "createsurvey.aspx", 0, "没有权限", "");

            //if (!this.lc.checkSurveyNum(Convert.ToString(this.Session["Limits3"]), this.UID))
            //{
            //    base.Response.Write("超出了所允许的同时管理问卷数,你可以删除部门问卷以可创建新问卷");
            //    base.Response.End();
            //}
        }

        public void setDropDownListBySurveyClass()
        {
            DataTable dtSurveyClass = new CreateSurvey_Layer().GetSurveyClass();
            DropDownList ddl = Page.FindControl("SurveyClass") as DropDownList;
            ddl.DataSource = dtSurveyClass;
            ddl.DataTextField = ConvertHelper.ConvertString(dtSurveyClass.Columns["SurveyClassName"]);
            ddl.DataValueField = ConvertHelper.ConvertString(dtSurveyClass.Columns["CID"]);
            ddl.DataBind();
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

