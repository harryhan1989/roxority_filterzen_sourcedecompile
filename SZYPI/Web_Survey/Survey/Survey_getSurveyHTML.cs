using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using BusinessLayer.Survey;
using Business.Helper;
using LoginClass;

namespace Web_Survey.Survey
{
    public class Survey_getSurveyHTML : Page, IRequiresSessionState
    {
        public string sSurvey = "";

        private string getSurveyContent(DataTable dtPageTable, DataTable dtItemTable)
        {
            StringBuilder builder;
            StringBuilder builder2;
            short num;
            short num2;
            int num3;
            goto Label_003F;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num < dtPageTable.Rows.Count)
                    {
                        builder.Append(string.Concat(new object[] { "<div id='page_", Convert.ToString((int)(num + 1)), "' class='PageBox'><div>", dtPageTable.Rows[num][1], "</div>" }));
                        num2 = 0;
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 11;
                    }
                    goto Label_0002;

                case 1:
                case 4:
                    num3 = 3;
                    goto Label_0002;

                case 2:
                    if ((num + 1) != Convert.ToInt32(dtItemTable.Rows[num2]["PageNo"]))
                    {
                        goto Label_008D;
                    }
                    num3 = 9;
                    goto Label_0002;

                case 3:
                    if (num2 < dtItemTable.Rows.Count)
                    {
                        num3 = 2;
                    }
                    else
                    {
                        num3 = 7;
                    }
                    goto Label_0002;

                case 5:
                    if (Convert.ToInt32(dtItemTable.Rows[num2]["ParentID"]) != 0)
                    {
                        goto Label_008D;
                    }
                    num3 = 12;
                    goto Label_0002;

                case 6:
                case 8:
                    num3 = 0;
                    goto Label_0002;

                case 7:
                    builder.Append(builder2.ToString() + "</div>\r\n");
                    builder2.Length = 0;
                    num = (short)(num + 1);
                    num3 = 6;
                    goto Label_0002;

                case 9:
                    num3 = 5;
                    goto Label_0002;

                case 10:
                    goto Label_008D;

                case 11:
                    return builder.ToString();

                case 12:
                    builder2.Append(dtItemTable.Rows[num2][0].ToString() + "<div class='ItemPitch'></div>\r\n");
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num3 = 10;
                    goto Label_0002;
            }
        Label_003F:
            builder = new StringBuilder();
            builder2 = new StringBuilder();
            num = 0;
            num2 = 0;
            num = 0;
            num3 = 8;
            goto Label_0002;
        Label_008D:
            num2 = (short)(num2 + 1);
            num3 = 1;
            goto Label_0002;
        }

        private string OpenFile(string sFile, string sSurveyName1, long SID1)
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            StreamReader reader = new StreamReader(sFile, Encoding.GetEncoding("UTF-8"));
            string str = reader.ReadToEnd();
            reader.Close();
            str = str.ToLower();
            str.Replace("surveystyle/", "SurveyStyle/");
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            string str2;
            string str4;
            string str5;
            DataSet set;
            string str7 = null; //赋初值
            int num3;
            goto Label_003B;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (set.Tables["StyleTable"].Rows.Count <= 0)
                    {
                        goto Label_047A;
                    }
                    num3 = 10;
                    goto Label_0002;

                case 1:
                    base.Response.Write("非法输入");
                    base.Response.End();
                    num3 = 11;
                    goto Label_0002;

                case 2:
                    str2 = "001.htm.htm";
                    num3 = 7;
                    goto Label_0002;

                case 3:
                    if (set.Tables["HeadFoot"].Rows.Count <= 0)
                    {
                        goto Label_0536;
                    }
                    num3 = 9;
                    goto Label_0002;

                case 4:
                    if (File.Exists(str))
                    {
                        goto Label_0375;
                    }
                    num3 = 1;
                    goto Label_0002;

                case 5:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_047A;
                    }
                    goto Label_0375;

                case 6:
                    if (!(str2 == ""))
                    {
                        goto Label_03BE;
                    }
                    num3 = 2;
                    goto Label_0002;

                case 7:
                    goto Label_03BE;

                case 8:
                    goto Label_0536;

                case 9:
                    str4 = set.Tables["HeadFoot"].Rows[0][0].ToString();
                    str5 = set.Tables["HeadFoot"].Rows[0][1].ToString();
                    num3 = 8;
                    goto Label_0002;

                case 10:
                    str7 = "<style>" + set.Tables["StyleTable"].Rows[0][0].ToString() + "</style>";
                    num3 = 5;
                    goto Label_0002;

                case 11:
                    goto Label_0375;
            }
        Label_003B:
            str = "TempLate/";
            long sID = 0;
            long uID = 0;
            uID=ConvertHelper.ConvertLong(this.Session["UserID"]);
            languageClass class3 = new languageClass();
            str2 = "";
            string str3 = "";
            str4 = "";
            str5 = "";
            sID = Convert.ToInt64(base.Request.QueryString["SID"]);
            //OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["AccessDB"].ToString().Replace("aaaaaa", "1aaaaaaaaaaaaaaaaaaw"));
            //connection.Open();
            //OleDbCommand objComm = new OleDbCommand("", connection);
            class3.getLanguage();
            string[] strArray = class3._arrLanguage[class3.getLan(sID), 1].Split(new char[] { '|' });
            string str6 = "<table  style='text-align:center;width:100%'><tr><td  style='text-align:center;width:100%'><input type='button' name='beforepagebt' id='beforepagebt' value='上一页' onClick='beforepage()'  style='visibility:hidden' disabled class='BeforePage'><input type='button' name='closepagebt' id='closepagebt' value='关闭' onClick='javascript:self.close()'  style='display:none' class='BeforePage'><input type='submit' name='submitbt' id='submitbt'  value='" + strArray[1] + "'  disabled class='SubmitBT'><input type='button' name='nextpagebt' id='nextpagebt' value='" + strArray[2] + "' onClick='nextpage()'  disabled  class='NextPage'><span id='obj'></span></td></tr></table>";
            //OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT ItemHTML,IID,ItemName,PageNo,DataFormatCheck,ItemType,Logic,ParentID,OptionAmount FROM ItemTable WHERE UID=" + uID.ToString() + " AND SID=" + sID.ToString() + "  ORDER BY PageNo,Sort", connection);
            set = new DataSet();
            //adapter.Fill(set, "ItemTable");
            DataTable ItemTable = new Survey_getSurveyHTML_Layer().GetItemTable(uID.ToString(), sID.ToString());
            ItemTable.TableName = "ItemTable";

            //new OleDbDataAdapter("SELECT PageNo,PageContent FROM PageTable WHERE UID=" + uID.ToString() + " AND SID=" + sID.ToString() + "  ORDER BY PageNo", connection).Fill(set, "PageTable");
            DataTable PageTable = new Survey_getSurveyHTML_Layer().GetPageTable(uID.ToString(), sID.ToString());
            PageTable.TableName = "PageTable";
            
            //new OleDbDataAdapter("SELECT TOP 1 ExpandContent FROM SurveyExpand WHERE UID=" + uID.ToString() + " AND SID=" + sID.ToString() + " AND ExpandType=9", connection).Fill(set, "StyleTable");
            DataTable StyleTable = new Survey_getSurveyHTML_Layer().GetSurveyExpand(uID.ToString(), sID.ToString(),"9");
            StyleTable.TableName = "StyleTable";
            
            //new OleDbDataAdapter("SELECT TOP 1 PageHead,PageFoot FROM HeadFoot WHERE UID=" + uID.ToString() + " AND SID=" + sID.ToString(), connection).Fill(set, "HeadFoot");
            DataTable HeadFoot = new Survey_getSurveyHTML_Layer().GetHeadFoot(uID.ToString(), sID.ToString());
            HeadFoot.TableName = "HeadFoot";
            
            //new OleDbDataAdapter("SELECT TOP 1 SurveyName,TempPage FROM SurveyTable WHERE UID=" + uID.ToString() + " AND SID=" + sID.ToString(), connection).Fill(set, "SurveyTable");
            DataTable SurveyTable = new Survey_getSurveyHTML_Layer().GetSurveyTable(uID.ToString(), sID.ToString());
            SurveyTable.TableName = "SurveyTable";

            set.Tables.Add(ItemTable);
            set.Tables.Add(PageTable);
            set.Tables.Add(StyleTable);
            set.Tables.Add(HeadFoot);
            set.Tables.Add(SurveyTable);



            str2 = set.Tables["SurveyTable"].Rows[0][1].ToString();
            num3 = 6;
            goto Label_0002;
        Label_0375:
            str7 = "";
            num3 = 0;
            goto Label_0002;
        Label_03BE:
            str = base.Server.MapPath(str + str2);
            num3 = 4;
            goto Label_0002;
        Label_047A:
            int num1 = set.Tables["SurveyTable"].Rows.Count;
            str7 = str7 + this.getSurveyContent(set.Tables["PageTable"], set.Tables["ItemTable"]);
            num3 = 3;
            goto Label_0002;
        Label_0536:
            str2 = this.OpenFile(str, str3, sID).Replace("surveystyle/", "template/surveystyle/");
            this.sSurvey = str2.Replace("这里是表单代码", str4 + "<form action='../../SS.aspx' name='SurveyForm' id='SurveyForm' method='post'  enctype=\"multipart/form-data\" onsubmit='return checkform()'>" + str7 + str6 + "</form>" + str5);
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
