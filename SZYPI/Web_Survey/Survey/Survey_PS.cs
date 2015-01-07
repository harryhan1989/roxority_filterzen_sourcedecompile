using LoginClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_PS : Page, IRequiresSessionState
    {
        public string sSurvey = "";

        protected string getItemList(DataTable dt)
        {
            StringBuilder builder;
        Label_001B:
            builder = new StringBuilder();
            short num = 0;
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 3:
                    num2 = 2;
                    goto Label_0002;

                case 1:
                    return builder.ToString();

                case 2:
                    if (num < dt.Rows.Count)
                    {
                        builder.Append(string.Concat(new object[] { 
                        "_I[", num.ToString(), "] =[", dt.Rows[num]["IID"], ",'", dt.Rows[num]["ItemName"].ToString().Replace("'", "'"), "',", dt.Rows[num]["PageNo"], ",'", dt.Rows[num]["DataFormatCheck"], "',", dt.Rows[num]["ItemType"], ",'", dt.Rows[num]["Logic"], "',", dt.Rows[num]["ParentID"], 
                        ",", dt.Rows[num]["OptionAmount"], ",", dt.Rows[num]["ChildID"], ",'", dt.Rows[num]["OptionImgModel"], "','", dt.Rows[num]["MultiReject"], "'];\n"
                     }));
                        num = (short)(num + 1);
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 1;
                    }
                    goto Label_0002;
            }
            goto Label_001B;
        }

        protected string getOptionList(DataTable dt)
        {
            StringBuilder builder;
        Label_001B:
            builder = new StringBuilder();
            int num = 0;
            int num2 = 3;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 3:
                    num2 = 2;
                    goto Label_0002;

                case 1:
                    return builder.ToString();

                case 2:
                    if (num < dt.Rows.Count)
                    {
                        builder.Append(string.Concat(new object[] { "_O[", num.ToString(), "] = [", dt.Rows[num]["OID"], ",", dt.Rows[num]["Point"], ",", dt.Rows[num]["IID"], ",", dt.Rows[num]["ParentNode"], ",'", dt.Rows[num]["OptionName"].ToString().Replace("'", "'"), "','", dt.Rows[num]["IsMatrixRowColumn"], "'];\n" }));
                        num++;
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 1;
                    }
                    goto Label_0002;
            }
            goto Label_001B;
        }

        protected string getSurveyContent(DataTable dt_Page, DataTable dt_Item)
        {
            StringBuilder builder;
            string str;
            int num;
            short num2 = 0; //赋初值
            int num3;
            goto Label_003F;
        Label_0002:
            switch (num3)
            {
                case 0:
                case 9:
                    num3 = 12;
                    goto Label_0002;

                case 1:
                case 6:
                    num3 = 3;
                    goto Label_0002;

                case 2:
                    goto Label_0086;

                case 3:
                    if (num2 < dt_Item.Rows.Count)
                    {
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 11;
                    }
                    goto Label_0002;

                case 4:
                    if ((num + 1) != Convert.ToInt32(dt_Item.Rows[num2]["PageNo"]))
                    {
                        goto Label_0086;
                    }
                    num3 = 10;
                    goto Label_0002;

                case 5:
                    str = str + dt_Item.Rows[num2]["ItemHTML"].ToString() + "<DIV class='ItemPitch'></DIV>";
                    num3 = 2;
                    goto Label_0002;

                case 7:
                    if (Convert.ToInt32(dt_Item.Rows[num2]["ParentID"]) != 0)
                    {
                        goto Label_0086;
                    }
                    num3 = 5;
                    goto Label_0002;

                case 8:
                    return builder.ToString();

                case 10:
                    num3 = 7;
                    goto Label_0002;

                case 11:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    builder.Append(str + "</div>");
                    num++;
                    num3 = 9;
                    goto Label_0002;

                case 12:
                    if (num < dt_Page.Rows.Count)
                    {
                        builder.Append(string.Concat(new object[] { "<div id=page_", Convert.ToString((int)(num + 1)), " style='width:100%;display:none;vertical-align: top' class='PageBox'><div>", dt_Page.Rows[num][1], "</div>" }));
                        str = "";
                        num2 = 0;
                        num3 = 6;
                    }
                    else
                    {
                        num3 = 8;
                    }
                    goto Label_0002;
            }
        Label_003F:
            builder = new StringBuilder();
            str = "";
            num = 0;
            num3 = 0;
            goto Label_0002;
        Label_0086:
            num2 = (short)(num2 + 1);
            num3 = 1;
            goto Label_0002;
        }

        protected string[] getSurveyExpand(string SID, SqlDataReader dr)
        {
        Label_001B:
            ;
            string[] strArray = new string[] { "", "", "", "", "", "", "", "", "", "" };
            //objComm.CommandText = "SELECT ExpandContent,ExpandType FROM SurveyExpand WHERE SID=" + SID + " AND ExpandType IN(0,1,8,9)";
            dr = new Survey_PS_Layer().GetSurveyExpand(SID);
            if ((1 != 0) && (0 != 0))
            {
            }
            int num = 3;
        Label_0002:
            switch (num)
            {
                case 0:
                    dr.Close();
                    return strArray;

                case 1:
                case 3:
                    num = 2;
                    goto Label_0002;

                case 2:
                    if (dr.Read())
                    {
                        strArray[Convert.ToInt32(dr[1])] = dr[0].ToString();
                        num = 1;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_0002;
            }
            goto Label_001B;
        }

        protected string getSurveyTemplate(string SID)
        {
            string str;
        Label_0017:
            str = "005.htm.htm";
            //objComm.CommandText = "SELECT TOP 1 Temppage FROM SurveyTable WHERE SID=" + SID.ToString();
            SqlDataReader reader = new Survey_PS_Layer().GetSurveyTable(SID.ToString());
            int num = 1;
        Label_0002:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num = 2;
                    goto Label_0002;

                case 2:
                    str = reader[0].ToString();
                    num = 0;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            reader.Dispose();
            return str;
        }

        protected string getURLBindVar(string sInput)
        {
            string[] strArray = null; //赋初值
            int num = 0; //赋初值
            int num2 = 0;
        Label_000D:
            switch (num2)
            {
                case 1:
                    if (num < strArray.Length)
                    {
                        try
                        {
                            num2 = 2;
                        Label_0076:
                            switch (num2)
                            {
                                case 0:
                                    break;

                                case 1:
                                    {
                                        string str = sInput;
                                        sInput = str + strArray[num].Substring(strArray[num].IndexOf(":") + 1) + "<$A$>" + base.Request.QueryString[strArray[num].Substring(0, strArray[num].IndexOf(":"))] + "<$B$>";
                                        num2 = 0;
                                        goto Label_0076;
                                    }
                                default:
                                    if (base.Request.QueryString[strArray[num].Substring(0, strArray[num].IndexOf(":"))].ToString().Trim() != "")
                                    {
                                        num2 = 1;
                                        goto Label_0076;
                                    }
                                    break;
                            }
                            num2 = 3;
                            goto Label_0076; //添加到这里作循环
                        }
                        catch
                        {

                        }
                        
                    }
                    num2 = 3;
                    goto Label_000D;

                case 2:
                case 4:
                    num2 = 1;
                    goto Label_000D;

                case 3:
                    return sInput;

                case 5:
                    if ((1 == 0) || (0 == 0))
                    {
                        return "";
                    }
                    break;

                default:
                    if (!(sInput == ""))
                    {
                        strArray = sInput.Split(new char[] { ';' });
                        sInput = "";
                        num = 0;
                        num2 = 4;
                    }
                    else
                    {
                        num2 = 5;
                    }
                    goto Label_000D;
            }
            num++;
            num2 = 2;
            goto Label_000D;
        }

        private string OpenFile(string sFile, string sSurveyName, string sClientJs1)
        {
            StreamReader reader = new StreamReader(sFile, Encoding.GetEncoding("UTF-8"));
            string str = reader.ReadToEnd();
            reader.Close();
            str = str.ToLower();
            string pattern = "<title>([^<]*)</title>";
            string str3 = "";
            str3 =  Regex.Match(str.Replace("\n", ""), pattern,RegexOptions.IgnoreCase).Groups[1].Value;
            return str.Replace("<title>" + str3 + "</title>", "<title>" + sSurveyName + "</title>").Replace("<head>", "<head><link href='../css/AdvObj.css' rel='stylesheet' type='text/css'>" + sClientJs1 + "").Replace("<body>", "<body>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            long num;
            string str3;
            string str6;
            string str7;
            string str8;
            StringBuilder builder = null; //赋初值
            SqlDataReader reader = null; //赋初值
            DataSet set = null; //赋初值
            string[] strArray = null; //赋初值
            string str11 = null; //赋初值
            int num6;
            goto Label_0057;
        Label_0002:
            switch (num6)
            {
                case 0:
                    if (set.Tables["SurveyTable"].Rows.Count != 0)
                    {
                        goto Label_05CC;
                    }
                    num6 = 0x10;
                    goto Label_0002;

                case 1:
                    if (File.Exists(str))
                    {
                        goto Label_0323;
                    }
                    num6 = 10;
                    goto Label_0002;

                case 2:
                    goto Label_022B;

                case 3:
                    builder.Append("blnCheckCode=true;\n");
                    num6 = 2;
                    goto Label_0002;

                case 4:
                    if (!(str3 == ""))
                    {
                        goto Label_06FE;
                    }
                    num6 = 14;
                    goto Label_0002;

                case 5:
                    goto Label_05CC;

                case 6:
                    if (set.Tables["HeadFoot"].Rows.Count <= 0)
                    {
                        goto Label_079E;
                    }
                    num6 = 15;
                    goto Label_0002;

                case 7:
                    goto Label_01EF;

                case 8:
                    if (set.Tables["SurveyTable"].Rows.Count <= 0)
                    {
                        goto Label_04D6;
                    }
                    num6 = 7;
                    goto Label_0002;

                case 9:
                    if (str7.IndexOf("|CheckCode:1|") < 0)
                    {
                        goto Label_022B;
                    }
                    num6 = 3;
                    goto Label_0002;

                case 10:
                    base.Response.Write("非法输入");
                    reader.Dispose();
                    base.Response.End();
                    num6 = 11;
                    goto Label_0002;

                case 11:
                    goto Label_0323;

                case 12:
                    try
                    {
                        str3 = base.Request.QueryString["PS"].ToString();
                    }
                    catch
                    {
                    }
                    reader = null;
                    set = new DataSet();
                    num6 = 4;
                    goto Label_0002;

                case 13:
                    goto Label_06FE;

                case 14:
                    if ((1 == 0) || (0 == 0))
                    {
                        str3 = this.getSurveyTemplate(num.ToString());
                        num6 = 13;
                        goto Label_0002;
                    }
                    goto Label_01EF;

                case 15:
                    str6 = set.Tables["HeadFoot"].Rows[0][0].ToString();
                    str8 = set.Tables["HeadFoot"].Rows[0][1].ToString();
                    num6 = 0x12;
                    goto Label_0002;

                case 0x10:
                    set.Dispose();
                    base.Response.Write("未找到问卷");
                    base.Response.End();
                    num6 = 5;
                    goto Label_0002;

                case 0x11:
                    goto Label_04D6;

                case 0x12:
                    goto Label_079E;
            }
        Label_0057:
            str = "TempLate/";
            num = 0;
            long uID = 0;
            string str2 = "<script src=Js/Client.js type='text/javascript' language='javascript'></script><script src=Js/Client_IntroductionAnswer.js type='text/javascript' language='javascript'></script><script src=Js/Client_ProgressiveAsk.js type='text/javascript' language='javascript'></script>";
            num = Convert.ToInt64(base.Request.QueryString["SID"]);
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            if (uID != 0)
            {

            }
            else
            { 
            
            }
            languageClass class3 = new languageClass();
            str3 = "";
            string sSurveyName = "";
            string str5 = "intTargetSID=" + num.ToString() + ";";
            str6 = "";
            str7 = "";
            str8 = "";
            builder = new StringBuilder();
            string str9 = "";
            string str10 = "<table  style='text-align:center;width:100%'><tr><td  style='text-align:center;width:100%'><input type='button' name='beforepagebt' id='beforepagebt' value='上一页' onClick='beforepage()'  style='visibility:hidden' disabled class='BeforePage'><input type='button' name='closepagebt' id='closepagebt' value='关闭' onClick='javascript:self.close()'  style='display:none' class='BeforePage'><input type='submit' name='submitbt' id='submitbt' value=' 提 交 '  style='visibility:hidden' disabled class='SubmitBT'><input type='button' name='nextpagebt' id='nextpagebt' value='下一页' onClick='nextpage()'  style='visibility:hidden' disabled class='NextPage'><input type=hidden name=Point id=Point></td></tr></table>";
            num6 = 12;
            goto Label_0002;
        Label_01EF:
            sSurveyName = set.Tables["SurveyTable"].Rows[0][0].ToString();
            num6 = 0x11;
            goto Label_0002;
        Label_022B:
            builder.Append("\nintpageamount = " + set.Tables["PageTable"].Rows.Count.ToString() + ";\n");
            str9 = "<style>" + strArray[9] + "</style>";
            num6 = 8;
            goto Label_0002;
        Label_0323: ;
            //command.CommandText = "SELECT ItemHTML,I.IID,ItemName,PageNo,DataFormatCheck,ItemType,Logic,ParentID,OptionAmount,OptionImgModel,ChildID,MultiReject FROM ItemTable I LEFT JOIN ItemTableExpand I1 ON I.IID=I1.IID WHERE I.UID=" + uID.ToString() + " AND I.SID=" + num.ToString() + "  ORDER BY PageNo,Sort";
            //adapter.Fill(set, "ItemTable");
            DataTable ItemTable = new Survey_PS_Layer().GetItemTable(uID.ToString(), num.ToString());
            ItemTable.TableName = "ItemTable";

            //command.CommandText = "SELECT PageNo,PageContent FROM PageTable WHERE UID=" + uID.ToString() + " AND SID=" + num.ToString() + "  ORDER BY PageNo";
            //adapter.Fill(set, "PageTable");
            DataTable PageTable = new Survey_PS_Layer().GetPageTable(uID.ToString(), num.ToString());
            PageTable.TableName = "PageTable";

            //command.CommandText = "SELECT TOP 1 PageHead,PageFoot FROM HeadFoot WHERE UID=" + uID.ToString() + " AND SID=" + num.ToString();
            //adapter.Fill(set, "HeadFoot");
            DataTable HeadFoot = new Survey_PS_Layer().GetHeadFoot(uID.ToString(), num.ToString());
            HeadFoot.TableName = "HeadFoot";

            //command.CommandText = "SELECT TOP 1 SurveyName,TempPage,State,Active,SID,Par,Lan FROM SurveyTable WHERE UID=" + uID.ToString() + " AND SID=" + num.ToString();
            //adapter.Fill(set, "SurveyTable");
            DataTable SurveyTable = new Survey_PS_Layer().GetSurveyTable1(uID.ToString(), num.ToString());
            SurveyTable.TableName = "SurveyTable";

            //command.CommandText = "SELECT  OID,Point,IID,ParentNode,OptionName,IsMatrixRowColumn FROM OptionTable WHERE SID=" + num.ToString() + " AND UID=" + uID.ToString();
            //adapter.Fill(set, "OptionTable");
            DataTable OptionTable = new Survey_PS_Layer().GetOptionTable(uID.ToString(), num.ToString());
            OptionTable.TableName = "OptionTable";

            set.Tables.Add(ItemTable);
            set.Tables.Add(PageTable);
            set.Tables.Add(HeadFoot);
            set.Tables.Add(SurveyTable);
            set.Tables.Add(OptionTable);

            class3.getLanguage();
            num6 = 0;
            goto Label_0002;
        Label_04D6:
            builder.Append(this.getItemList(set.Tables["ItemTable"]));
            builder.Append(this.getOptionList(set.Tables["OptionTable"]));
            str11 = str11 + this.getSurveyContent(set.Tables["PageTable"], set.Tables["ItemTable"]);
            str7.IndexOf("CheckCode:1").ToString();
            str7.IndexOf("PSW:1").ToString();
            str5 = str2 + "<script language='javascript' type='text/javascript'>try{window.parent.complateActionWin();}catch(e){}" + builder.ToString() + "</script>";
            num6 = 6;
            goto Label_0002;
        Label_05CC:
            builder.Append("var sLanguage='" + class3._arrLanguage[Convert.ToInt32(set.Tables["SurveyTable"].Rows[0]["Lan"]), 0].Replace("'", "\"") + "';");
            strArray = this.getSurveyExpand( num.ToString(), reader);
            builder.Append("sHiddenItem='" + strArray[0] + "';sURLVar='" + strArray[1] + "';sProgressiveAsk='" + strArray[8] + "';");
            str7 = set.Tables["SurveyTable"].Rows[0]["Par"].ToString();
            str11 = "";
            num6 = 9;
            goto Label_0002;
        Label_06FE:
            str = base.Server.MapPath(str + str3);
            num6 = 1;
            goto Label_0002;
        Label_079E:
            str3 = this.OpenFile(str, sSurveyName, str5).Replace("surveystyle/", "template/surveystyle/");
            this.sSurvey = str3.Replace("这里是表单代码", str9 + str6 + "<form action='Template/lastpage/005.htm.htm' name='surveyform' id='surveyform' onsubmit='return checkform()'>" + str11 + str10 + "</form>" + str8);
        }


        public void InitChoose()
        {
            string  sid = ConvertHelper.ConvertString((base.Request.QueryString["SID"]));
            string FindAnswerGUID = ConvertHelper.ConvertString(Request.QueryString["AnswerGUID"]);
            if (FindAnswerGUID != "")
            {
                DataTable dt_GetAnswerDetail = new Survey_PS_Layer().GetAnswerDetail(FindAnswerGUID, sid);
                if (dt_GetAnswerDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_GetAnswerDetail.Rows)
                    {
                        string getAnswer = ConvertHelper.ConvertString(dr["Answer"]);
                        string IID = ConvertHelper.ConvertString(dr["IID"]);
                        if ( getAnswer!= null)
                        {
                            string[] Answers = getAnswer.Split(',');
                            foreach (string Answer in Answers)
                            {
                                string[] AnswersItem = Answer.Split('|');
                                int i = 0;
                                foreach (string AnswerItem in AnswersItem)
                                {
                                    if (i == 0)
                                    {
                                        base.Response.Write(string.Format(" <script language='javascript'>SetChecked('{0}','{1}');</script> ", "F" + IID, AnswerItem));
                                    }
                                    else if (i == 1)
                                    {
                                        base.Response.Write(string.Format(" <script language='javascript'>SetText('{0}','{1}');</script> ", "F" + IID + "_Input", AnswerItem));
                                    }
                                    i++;
                                }
                            }
                        }
                    }
                    base.Response.Write(string.Format(" <script language='javascript'>SetDisplay();</script> "));
                }
            }
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
