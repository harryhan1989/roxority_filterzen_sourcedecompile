using LoginClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Text;
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
    public class Survey_StatIndex : Page, IRequiresSessionState
    {
        public string sClientJs = "";
        public long SID;
        public string sItemLsit = "";
        protected HtmlGenericControl SL;
        protected HtmlGenericControl SurveyName;

        protected string getSurveyList(long SID, long UID, int intTop)
        {
            StringBuilder builder;
        Label_0027:
            builder = new StringBuilder();
            //builder.Append("<select name='Survey' style=\"width:100px\" onchange=\"location.href ='statindex.aspx?sid='+this.options[this.selectedIndex].value\"><option value='0'>选择问卷</option>");
            builder.Append("<select name='Survey' id='selectSurvey' style=\"width:100px\" onclick=\"initFace();x_open('请选择查看问卷', 'SelectGrid.aspx',700,400)\">");
            //OleDbDataReader reader = new OleDbCommand("SELECT TOP " + intTop.ToString() + " SID,SurveyName FROM SurveyTable WHERE  UID=" + UID.ToString() + " AND State=1 ORDER BY SID DESC", objConn).ExecuteReader();
            SqlDataReader reader = new Survey_StatIndex_Layer().GetSurveyTable(intTop, UID.ToString());
            int num = 6;
        Label_0002:
            switch (num)
            {
                case 0:
                case 4:
                case 6:
                    num = 2;
                    goto Label_0002;

                case 1:
                    if (!(reader[0].ToString() != SID.ToString()))
                    {
                        builder.Append(string.Concat(new object[] { "<option value='", reader[0], "' selected>", reader[1], "</option>" }));
                        num = 4;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_0002;

                case 2:
                    if (reader.Read())
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_0002;

                case 3:
                    reader.Dispose();
                    builder.Append("</select>");
                    return builder.ToString();

                case 5:
                    //builder.Append(string.Concat(new object[] { "<option value='", reader[0], "'>", reader[1], "</option>" }));
                    num = 0;
                    goto Label_0002;
            }
            goto Label_0027;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
            StringBuilder builder;
            DataSet set;
            int num2 = 0; //赋初值
            int num3;
            goto Label_0027;
        Label_0002:
            switch (num3)
            {
                case 0:
                case 6:
                    num3 = 4;
                    goto Label_0002;

                case 1:
                    if (set.Tables["SurveyTable"].Rows.Count > 0)
                    {
                        goto Label_0376;
                    }
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num3 = 5;
                    goto Label_0002;

                case 2:
                    goto Label_0376;

                case 3:
                    this.sItemLsit = this.sItemLsit + "</table>";
                    this.sClientJs = builder.ToString();
                    return;

                case 4:
                    if (num2 < set.Tables["ItemTable"].Rows.Count)
                    {
                        builder.Append("arrItem[" + num2.ToString() + "] = new Array();");
                        builder.Append(string.Concat(new object[] { "arrItem[", num2.ToString(), "][0] = ", set.Tables["ItemTable"].Rows[num2]["IID"], ";" }));
                        builder.Append(string.Concat(new object[] { "arrItem[", num2.ToString(), "][1] = '", set.Tables["ItemTable"].Rows[num2]["ItemName"], "';" }));
                        builder.Append(string.Concat(new object[] { "arrItem[", num2.ToString(), "][2] = ", set.Tables["ItemTable"].Rows[num2]["ItemType"], ";" }));
                        num2++;
                        num3 = 6;
                    }
                    else
                    {
                        num3 = 3;
                    }
                    goto Label_0002;

                case 5:
                    base.Response.Write("未找到问卷");
                    base.Response.End();
                    num3 = 2;
                    goto Label_0002;
            }
        Label_0027:
            num = 0;
            builder = new StringBuilder();
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            builder.Append("var SID=" + this.SID.ToString() + ";");
            num = ConvertHelper.ConvertLong(this.Session["UserID"]);
            set = new DataSet();
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "da:index", 0, "没有权限", "");
            //OleDbCommand selectCommand = new OleDbCommand("SELECT TOP 1000 * FROM ItemTable WHERE SID=" + this.SID.ToString() + " AND UID=" + num.ToString() + " AND ParentID=0 ORDER BY PageNo,Sort", connection);
            DataTable ItemTable = new DataTable();
            ItemTable = new Survey_StatIndex_Layer().GetItemTable(this.SID.ToString(), num.ToString());
            ItemTable.TableName = "ItemTable";
            set.Tables.Add(ItemTable);
            
            this.SL.InnerHtml = this.getSurveyList(this.SID, num, 20);
            //selectCommand.CommandText = "SELECT TOP 1 SurveyName FROM SurveyTable WHERE UID=" + num.ToString() + " AND SID=" + this.SID.ToString() + " AND State=1";
            DataTable SurveyTable = new DataTable();
            SurveyTable = new Survey_StatIndex_Layer().GetSurveyTable1(this.SID.ToString(), num.ToString());
            SurveyTable.TableName = "SurveyTable";
            set.Tables.Add(SurveyTable);

            num3 = 1;
            goto Label_0002;
        Label_0376:
            this.SurveyName.InnerHtml = set.Tables["SurveyTable"].Rows[0][0].ToString();
            '"'.ToString();
            this.sItemLsit = "<table style='width:130px'  border='0' cellpadding='2' cellspacing='0'>";
            builder.Append("var arrItem = new Array();");
            num2 = 0;
            num3 = 0;
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
