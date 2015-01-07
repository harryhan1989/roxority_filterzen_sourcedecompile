using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;
using WebUI;
using Web_SubCode.CreateItem;

namespace Web_Survey.Survey
{
    public class Survey_getSurvey : Page, IRequiresSessionState
    {
        public string sClientJs = "";
        public string sPageFoot = "";
        public string sPageHead = "";
        public string sStyle = ".SurveyName{}.PageFoot{}.MatrixInputCSS{}.ExpandContentStyle{}.PageBox{}.PageContent{}.ItemBox{}.ItemBar{}.ItemName{}.SubItemName{}.ItemContent{}.OptionName{}.InputCSS{}.OtherInputCSS{}.CheckBoxCSS{}.RadioCSS{}.ListMulitCSS{}.TextAreaCSS{}.SelectCSS{}.MatrixSelectCSS{}.PercentInputCSS{}.NumInputCSS{}.MatrixInputCSS{}.NextPage{}.SubmitBT{}.CheckCode{}.OneTimePSW{}.ItemPitch{}";
        public string sSurveySrc = "";
        public long UID;

        public string getItemList(string SID1, string UID1)
        {
            string str;
            StringBuilder builder;
            StringBuilder builder2;
            StringBuilder builder3;
            DataSet set;
            int num = 0; //赋初值
            int num2 = 0; //赋初值
            int num3 = 0; //赋初值
            string str2;
            int num4;
            goto Label_0053;
        Label_0002:
            switch (num4)
            {
                case 0:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num2 = 0;
                    num4 = 15;
                    goto Label_0002;

                case 1:
                case 7:
                    num4 = 0x11;
                    goto Label_0002;

                case 2:
                    base.Response.Write("未找到问卷");
                    base.Response.End();
                    num4 = 10;
                    goto Label_0002;

                case 3:
                    this.sPageHead = set.Tables["HeadFoot"].Rows[0]["PageHead"].ToString();
                    this.sPageFoot = set.Tables["HeadFoot"].Rows[0]["PageFoot"].ToString();
                    num4 = 0x10;
                    goto Label_0002;

                case 4:
                    if (set.Tables["SurveyTable"].Rows.Count != 0)
                    {
                        goto Label_0511;
                    }
                    num4 = 2;
                    goto Label_0002;

                case 5:
                case 15:
                    num4 = 9;
                    goto Label_0002;

                case 6:
                    if (set.Tables["HeadFoot"].Rows.Count <= 0)
                    {
                        this.sPageHead = "<div class=SurveyName>" + set.Tables["SurveyTable"].Rows[0]["SurveyName"].ToString() + "</div>";
                        num4 = 13;
                    }
                    else
                    {
                        num4 = 3;
                    }
                    goto Label_0002;

                case 8:
                    if (set.Tables["StyleTable"].Rows.Count < 1)
                    {
                        goto Label_027A;
                    }
                    num4 = 12;
                    goto Label_0002;

                case 9:
                    if (num2 < set.Tables["ItemTable"].Rows.Count)
                    {
                        str2 = set.Tables["ItemTable"].Rows[num2]["IID"].ToString();
                        builder.Append("arrItem[" + num3.ToString() + "] = [" + str2 + "," + set.Tables["ItemTable"].Rows[num2]["PageNo"].ToString() + "," + set.Tables["ItemTable"].Rows[num2]["ItemType"].ToString() + "];\n");
                        builder3.Append("<div id='Posi_I_" + num3.ToString() + "'   class='ItemContentLine'>");
                        builder3.Append("<div style='height:22px'><div id=ItemNameTitle" + str2 + " class='ItemNameTitleHidden'>" + set.Tables["ItemTable"].Rows[num2]["ItemName"].ToString() + "</div>");
                        builder3.Append("<div id='I_Option" + str2 + "'  class='I_Option'></div></div>");
                        builder3.Append("<div id=I" + str2 + " class='ItemContent'>" + set.Tables["ItemTable"].Rows[num2]["ItemHTML"].ToString() + "</div></div>");
                        num3++;
                        num2++;
                        num4 = 5;
                    }
                    else
                    {
                        num4 = 11;
                    }
                    goto Label_0002;

                case 10:
                    goto Label_0511;

                case 11:
                    str = builder2.ToString() + "<div id=ItemTempBox>" + builder3.ToString() + "</div>";
                    this.sClientJs = builder.ToString();
                    return str;

                case 12:
                    this.sStyle = this.sStyle + set.Tables["StyleTable"].Rows[0][0].ToString();
                    num4 = 14;
                    goto Label_0002;

                case 13:
                case 0x10:
                    num = 0;
                    num2 = 0;
                    num3 = 0;
                    builder.Append("var SID = " + SID1 + ";\nvar arrPageNo = new Array();\nvar arrItem = new Array();\n");
                    builder.Append("var arrPage2 = new Array();\nvar arrItem2 = new Array();\n");
                    num4 = 8;
                    goto Label_0002;

                case 14:
                    goto Label_027A;

                case 0x11:
                    if (num < set.Tables["PageTable"].Rows.Count)
                    {
                        builder.Append("arrPage2[" + num.ToString() + "] = " + set.Tables["PageTable"].Rows[num]["PID"].ToString() + ";\n");
                        builder.Append("arrPageNo[" + num.ToString() + "] = " + set.Tables["PageTable"].Rows[num]["PID"].ToString() + ";\n");
                        builder2.Append("<div id='Posi_P_" + num.ToString() + "'  class='PageContentLine'><div style='height:22px'><div id=PageNo" + set.Tables["PageTable"].Rows[num]["PID"].ToString() + " class='PageNo'>页号</div>");
                        builder2.Append("<div id='P_Option" + set.Tables["PageTable"].Rows[num]["PID"].ToString() + "' class='P_Option'></div></div>");
                        builder2.Append("<div id='PageBox" + set.Tables["PageTable"].Rows[num]["PID"].ToString() + "' style='clear:both'><div id='PageContent" + set.Tables["PageTable"].Rows[num]["PID"].ToString() + "' class='PageContent'>" + set.Tables["PageTable"].Rows[num]["PageContent"].ToString() + "</div><div id=PageItem" + set.Tables["PageTable"].Rows[num]["PID"].ToString() + "></div></div></div>");
                        num++;
                        num4 = 7;
                    }
                    else
                    {
                        num4 = 0;
                    }
                    goto Label_0002;
            }
        Label_0053:
            str = "";
            builder = new StringBuilder();
            builder2 = new StringBuilder();
            builder3 = new StringBuilder();
            set = new DataSet();
            //SqlDataAdapter adapter = new SqlDataAdapter("SELECT IID,ItemName,ItemType,ParentID,ItemHTML,OrderModel,OptionAmount,ItemContent,DataFormatCheck,OtherProperty,PageNo,Sort FROM ItemTable WHERE UID=" + UID1 + " AND SID=" + SID1 + " AND ParentID=0 ORDER BY PageNo,Sort", connection);
            //adapter.Fill(set, "ItemTable");
            DataTable ItemTable = new Survey_getSurvey_Layer().GetItemTable(UID1, SID1, 0);
            ItemTable.TableName = "ItemTable";
            //command.CommandText = "SELECT PID,PageNo,PageContent FROM PageTable WHERE UID=" + UID1 + " AND SID=" + SID1 + " ORDER BY PageNo";
            //adapter.SelectCommand = command;
            //adapter.Fill(set, "PageTable");
            DataTable PageTable = new Survey_getSurvey_Layer().GetPageTable(UID1, SID1);
            PageTable.TableName = "PageTable";
            //command.CommandText = "SELECT ExpandContent FROM SurveyExpand WHERE UID=" + UID1 + " AND SID=" + SID1 + " AND ExpandType=9";
            //adapter.SelectCommand = command;
            //adapter.Fill(set, "StyleTable");
            DataTable StyleTable = new Survey_getSurvey_Layer().GetSurveyExpand(UID1, SID1, 9);
            StyleTable.TableName = "StyleTable";
            //command.CommandText = "SELECT TOP 1 SurveyName,State FROM SurveyTable WHERE UID=" + UID1 + " AND SID=" + SID1;
            //adapter.SelectCommand = command;
            //adapter.Fill(set, "SurveyTable");
            DataTable SurveyTable = new Survey_getSurvey_Layer().GetSurveyTable(UID1, SID1);
            SurveyTable.TableName="SurveyTable";
            //command.CommandText = "SELECT TOP 1 PageHead,PageFoot FROM HeadFoot WHERE UID=" + UID1 + " AND SID=" + SID1;
            //adapter.SelectCommand = command;
            //adapter.Fill(set, "HeadFoot");
            DataTable HeadFoot = new Survey_getSurvey_Layer().GetHeadFoot(UID1, SID1);
            HeadFoot.TableName = "HeadFoot";

            set.Tables.Add(ItemTable);
            set.Tables.Add(PageTable);
            set.Tables.Add(StyleTable);
            set.Tables.Add(SurveyTable);
            set.Tables.Add(HeadFoot);

            num4 = 4;
            goto Label_0002;
        Label_027A:
            str2 = "";
            num = 0;
            num4 = 1;
            goto Label_0002;
        Label_0511:
            builder.Append("var sState = '" + set.Tables["SurveyTable"].Rows[0]["State"] + "';\n");
            Convert.ToBoolean(set.Tables["SurveyTable"].Rows[0]["State"]);
            num4 = 6;
            goto Label_0002;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            int num = 0;
            try
            {
                num = Convert.ToInt32(base.Request.QueryString[0]);
            }
            catch
            {
                base.Response.Write("非法的输入");
                base.Response.End();
            }
            new SerialNumber().SetSerialNumber(num.ToString());
            this.sSurveySrc = this.getItemList(num.ToString(), this.UID.ToString());
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
