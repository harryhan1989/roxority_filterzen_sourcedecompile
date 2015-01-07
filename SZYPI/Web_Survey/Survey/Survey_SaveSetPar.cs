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
    public class Survey_SaveSetPar : Page, IRequiresSessionState
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            long num;
            long num2;
            int num3;
            int num4;
            string str2;
            string str3;
            string str4;
            string str5;
            string str6;
            string str7;
            string str8;
            string str9;
            string str10;
            string str11;
            string str12;
            string str13= null; //赋初值
            string str14= null; //赋初值
            string str15= null; //赋初值
            string str16= null; //赋初值
            string str17 = null; //赋初值
            string str18 = null; //赋初值
            string str19 = null; //赋初值
            string str20 = null; //赋初值
            string str21 = null; //赋初值
            string str22 = null; //赋初值
            int num5 = 0; //赋初值
            string str23 = null; //赋初值
            short num6 = 0; //赋初值
            string str24 = null; //赋初值
            int num7 = 0; //赋初值
            string str25 = null; //赋初值
            string str26 = null; //赋初值
            string str27 = null; //赋初值
            string str28 = null; //赋初值
            string str29 = null; //赋初值
            string str30 = null; //赋初值
            string str31 = null; //赋初值
            string str33 = null; //赋初值
            string str34 = null; //分析结果查看权限
            int num8 = 0; //赋初值
            goto Label_0033;
        Label_0002:
            switch (num8)
            {
                case 0:
                    goto Label_0309;

                case 1:
                    if (!(str16 != "1"))
                    {
                        goto Label_0309;
                    }
                    num8 = 6;
                    goto Label_0002;

                case 2:
                    if (!(str16 != "1"))
                    {
                        goto Label_050D;
                    }
                    num8 = 9;
                    goto Label_0002;

                case 3:

                    num8 = 8;
                    goto Label_0002;

                case 4:
                case 8:
                    {
                        str19 = "Report:" + str19 + "|ReportAnswerResult:" + str17 + "|ReportStat:" + str18 + "|ReportDataList:" + str20 + "|ReportCardView:" + str21 + "|ReportPoint:" + str22 + "|ReportDefine:" + str23 + "|CR:" + str29 + "|XML:" + str30 + "|AXML:" + str31;
                        string str32 = "Email:" + str9 + "|IPToScreen:" + str3 + "|CheckCode:" + str7 + "|AnswerPSW:" + str13 + "|PSW:" + str10 + "|RecordIP:" + str14 + "|RecordTime:" + str15 + "|IP:" + str2 + "|Cookies:" + str4 + "|MemberLogin:" + str5 + "|AnswerArea:" + str6 + "|Quota:" + str25 + "|GUIDAndDep:" + str26 + "|TPaper:" + str27 + "|TToAll:" + str28 + "|NeedConfirm:" + str33 + "|ResultPublish:" + str34;

                 //       OleDbCommand command = new OleDbCommand(string.Concat(new object[] { 
                 //   "UPDATE SurveyTable SET Par='", str32, "',Report='", str19, "',MaxAnswerAmount=", num3, ",EndDate=", str12, ",ComplateMessage='", str24, "',ClassID=", num7.ToString(), ",EndPage=", num6, ",Active=", str16, 
                 //   ",SurveyPSW='", str11, "',ToURL='", str8, "',Point='", num4.ToString(), "',Lan=", num5.ToString(), " WHERE UID=", num2.ToString(), " AND SID=", num.ToString()
                 //}), connection);
                        new Survey_SaveSetPar_Layer().UpdateSurveyTable(str32, str19, num3.ToString(), str12, str24, num7.ToString(), num6.ToString(), str16, str11, str8, num4.ToString(), num5.ToString(), num2.ToString(), num.ToString());
                        //base.Response.Write(string.Format("<script language='javascript'>window.parent.document.getElementById(\"targetWin\").style.display=\"none\";</script>"));// ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("<script language='javascript'>window.parent.parent.alert(\"{0}\")</script>", "保存成功！"));                        
                        base.Response.Write(string.Format("<script language='javascript'>window.parent.parent.alert(\"{0}\");</script>", "保存成功！"));// ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("<script language='javascript'>window.parent.parent.alert(\"{0}\")</script>", "保存成功！"));
                        return;
                    }
                case 5:
                    if (!(str12 != ""))
                    {
                        str12 = "NULL";
                        num8 = 4;
                    }
                    else
                    {
                        num8 = 3;
                    }
                    goto Label_0002;

                case 6:
                    str16 = "0";
                    num8 = 0;
                    goto Label_0002;

                case 7:
                    goto Label_050D;

                case 9:
                    str16 = "0";
                    num8 = 7;
                    goto Label_0002;
            }
        Label_0033:
            num = Convert.ToInt64(base.Request.Form["SID"]);
            num2 = 0;
            num2 = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num3 = Convert.ToInt32(base.Request.Form["MaxAnswerAmount"]);
            num4 = Convert.ToInt32(base.Request.Form["SPoint"]);
            str2 = Convert.ToString(base.Request.Form["IP"]);
            str3 = Convert.ToString(base.Request.Form["IPToScreen"]);
            str4 = Convert.ToString(base.Request.Form["Cookies"]);
            str5 = Convert.ToString(base.Request.Form["MemberLogin"]);
            str6 = Convert.ToString(base.Request.Form["AnswerArea"]);
            str7 = Convert.ToString(base.Request.Form["CheckCode"]);
            str8 = Convert.ToString(base.Request.Form["ToURL"]);
            str9 = Convert.ToString(base.Request.Form["Email"]);
            str10 = Convert.ToString(base.Request.Form["PSW"]);
            str11 = Convert.ToString(base.Request.Form["SurveyPSW"]);
            str12 = Convert.ToString(base.Request.Form["EndDate"]);
            if (str12 == "")
            {
                str12 = null;
            }
            str13 = Convert.ToString(base.Request.Form["AnswerPSW"]);
            str14 = Convert.ToString(base.Request.Form["RecordIP"]);
            str15 = Convert.ToString(base.Request.Form["RecordTime"]);
            str16 = Convert.ToString(base.Request.Form["Active"]);

            str33 = base.Request.Form["NeedConfirm"];
            str34 = base.Request.Form["ResultPublish"];

            num8 = 1;
            goto Label_0002;
        Label_0309:
            str17 = Convert.ToString(base.Request.Form["ReportAnswerResult"]);
            str18 = Convert.ToString(base.Request.Form["ReportStat"]);
            str19 = Convert.ToString(base.Request.Form["Report"]);
            str20 = Convert.ToString(base.Request.Form["ReportDataList"]);
            str21 = Convert.ToString(base.Request.Form["ReportCardView"]);
            str22 = Convert.ToString(base.Request.Form["ReportPoint"]);
            num5 = Convert.ToInt32(base.Request.Form["Lan"]);
            str23 = Convert.ToString(base.Request.Form["ReportDefine"]);
            num6 = Convert.ToInt16(base.Request.Form["EndPage"]);
            str24 = Convert.ToString(base.Request.Form["ComplateMessage"]);
            num7 = Convert.ToInt32(base.Request.Form["ClassID"]);
            str25 = base.Request.Form["Quota"];
            str26 = base.Request.Form["GUIDAndDep"];
            str27 = base.Request.Form["TPaper"];
            str28 = base.Request.Form["TToAll"];
            str29 = base.Request.Form["CustomizeReport"];
            str30 = base.Request.Form["XML"];
            str31 = base.Request.Form["AnswerXML"];


            num8 = 2;
            goto Label_0002;
        Label_050D:
            num8 = 5;
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
