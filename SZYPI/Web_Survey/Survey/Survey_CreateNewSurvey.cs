using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLayer.Survey;
using System.Data.SqlClient;
using System.Data;
using Business.Helper;
using shareclass;

namespace Web_Survey.Survey
{
    public class Survey_CreateNewSurvey : Page, IRequiresSessionState
    {
        protected Button Button1;
        protected HtmlForm form1;
        //protected DropDownList Lan;
        protected DropDownList SurveyType;
        protected HtmlGenericControl Message;
        protected RegularExpressionValidator RegularExpressionValidator1;
        protected RequiredFieldValidator RequiredFieldValidator1;
        public string sClientJs = "";
        protected DropDownList SurveyClass;
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
            SqlDataReader reader = null; //赋初值
            long num2 = 0; //赋初值
        Label_002B:
            str = this.SurveyName.Text.ToString().Trim();
            int num = 0;
            DateTime now = DateTime.Now;
            int num3 = 5;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num > -1)
                    {
                        string str2 = "Email:|IPToScreen:|CheckCode:|AnswerPSW:|PSW:|RecordIP:1|RecordTime:1|IP:|Cookies:|MemberLogin:|AnswerArea:|Quota:|GUIDAndDep:|TPaper:|TToAll:|TTC:|NeedConfirm:";

                        //command.CommandText = string.Concat(new object[] { "INSERT INTO SurveyTable(SurveyName,CreateDate,UID,ClassID,Par,Lan) VALUES('", str, "','", now, "',", this.UID.ToString(), ",", this.SurveyClass.SelectedValue.ToString(), ",'", str2, "',", this.Lan.SelectedValue, ")" });
                        new Survey_CreateNewSurvey_Layer().InsertSurveyTable(str, now, this.UID.ToString(), this.SurveyClass.SelectedValue.ToString(), str2, "1",this.SurveyType.SelectedValue);
                        //command.CommandText = "SELECT   MAX(SID) FROM SurveyTable";
                        reader=new Survey_CreateNewSurvey_Layer().GetSurveyTable();
                        num2 = 0;
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    num3 = 0;
                    goto Label_0002;

                case 3:
                    goto Label_0226;

                case 4:
                    if (!reader.Read())
                    {
                        goto Label_0226;
                    }
                    num3 = 6;
                    goto Label_0002;

                case 5:
                    try
                    {
                        num = Convert.ToInt32(this.SurveyClass.SelectedValue);
                    }
                    catch
                    {
                        num = 0;
                    }
                    num3 = 7;
                    goto Label_0002;

                case 6:
                    num2 = Convert.ToInt64(reader[0]);
                    new NotClass(num2, this.UID).fromStyleLibCopy(1, num2, this.UID);
                    num3 = 3;
                    goto Label_0002;

                case 7:
                    if (str == "")
                    {
                        break;
                    }
                    num3 = 2;
                    goto Label_0002;

                default:
                    goto Label_002B;
            }
            return;
        Label_0226:
            reader.Close();
            //command.CommandText = "INSERT INTO PageTable(UID,SID,PageNo,PageContent) VALUES(" + this.UID.ToString() + "," + num2.ToString() + ",1,'') ";
            new Survey_CreateNewSurvey_Layer().InsertPageTable(num2.ToString(), this.UID.ToString(),"","1");
            //command.CommandText = " INSERT INTO HeadFoot(PageHead,UID,SID) VALUES('<div class=SurveyName>" + str + "</div>'," + this.UID.ToString() + "," + num2.ToString() + ")";
            new Survey_CreateNewSurvey_Layer().InsertHeadFoot("<div class=SurveyName>" + str + "</div>", this.UID.ToString(), num2.ToString());
            this.sClientJs = "SID=" + num2.ToString() + "\n";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Form.DefaultButton = this.Button1.UniqueID;
            UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "createsurvey.aspx", 0, "没有权限", "");
            //if (!class2.checkSurveyNum(Convert.ToString(this.Session["Limits3"]), this.UID))
            //{
            //    this.Message.InnerHtml = "超出了所允许的同时管理问卷数,你可以删除部门问卷以可创建新问卷";
            //    this.Button1.Enabled = false;
            //}
            //else
            //{
            //    connection.Dispose();
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
