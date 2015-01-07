using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using EntityModel.survey;
using Business.Helper;
using DBAccess.Entity;
using BLL;

namespace Web_Survey.Survey
{
    public class Survey_ss : Page, IRequiresSessionState
    {
        protected HtmlForm form1;

        protected void checkAnswerAmount(long intMaxAnswerAmount, long SID)
        {
            SqlDataReader reader = null; //赋初值
            int num = 1;
        Label_000D:
            switch (num)
            {
                case 0:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num = 6;
                    goto Label_000D;

                case 2:
                    return;

                case 3:
                    break;

                case 4:
                    if (Convert.ToInt32(reader[0]) < intMaxAnswerAmount)
                    {
                        break;
                    }
                    num = 7;
                    goto Label_000D;

                case 5:
                    //objComm.CommandText = "SELECT COUNT(1) FROM Z" + SID.ToString();
                    reader = new Survey_ss_Layer().GetAnswerInfo(SID.ToString());
                    num = 0;
                    goto Label_000D;

                case 6:
                    num = 4;
                    goto Label_000D;

                case 7:
                    reader.Dispose();
                    base.Response.Redirect("Error.aspx?ec=15&SID=" + SID);
                    num = 3;
                    goto Label_000D;

                default:
                    if (intMaxAnswerAmount == 0)
                    {
                        return;
                    }
                    num = 5;
                    goto Label_000D;
            }
            reader.Dispose();
            num = 2;
            goto Label_000D;
        }

        //protected bool checkGUAnswered(int GUID, OleDbConnection objConn, string SID)
        //{
        //Label_0017:
        //    bool flag = false;
        //    OleDbCommand command = new OleDbCommand("SELECT TOP 1 ID FROM AnsweredSurvey WHERE GUID=" + GUID.ToString() + " AND SID=" + SID, objConn);
        //    OleDbDataReader reader = command.ExecuteReader();
        //    int num = 2;
        //Label_0002:
        //    switch (num)
        //    {
        //        case 0:
        //            flag = true;
        //            num = 1;
        //            goto Label_0002;

        //        case 1:
        //            break;

        //        case 2:
        //            if (!reader.Read())
        //            {
        //                break;
        //            }
        //            num = 0;
        //            goto Label_0002;

        //        default:
        //            goto Label_0017;
        //    }
        //    reader.Dispose();
        //    command.Dispose();
        //    return flag;
        //}

        //protected bool checkGUPoint(OleDbCommand objComm, int GUID, int intSPoint)
        //{
        //    bool flag;
        //Label_001F:
        //    flag = false;
        //    objComm.CommandText = string.Concat(new object[] { "SELECT TOP 1 SPoint+", intSPoint, " FROM GUTable WHERE GUID=", GUID.ToString() });
        //    OleDbDataReader reader = objComm.ExecuteReader();
        //    int num = 4;
        //Label_0002:
        //    switch (num)
        //    {
        //        case 0:
        //            if (Convert.ToInt32(reader[0]) < 0)
        //            {
        //                break;
        //            }
        //            if ((1 != 0) && (0 != 0))
        //            {
        //            }
        //            num = 1;
        //            goto Label_0002;

        //        case 1:
        //            flag = true;
        //            num = 2;
        //            goto Label_0002;

        //        case 2:
        //            break;

        //        case 3:
        //            num = 0;
        //            goto Label_0002;

        //        case 4:
        //            if (!reader.Read())
        //            {
        //                break;
        //            }
        //            num = 3;
        //            goto Label_0002;

        //        default:
        //            goto Label_001F;
        //    }
        //    reader.Close();
        //    return flag;
        //}

        protected bool checkQuota(string sQuotaCondition, int intQuotaAmount)
        {
            bool flag;
            int num;
            SqlDataReader reader;
            int num2;
            goto Label_0023;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (!reader.Read())
                    {
                        goto Label_0065;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    num = Convert.ToInt32(reader[0]);
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    goto Label_0065;

                case 3:
                    return flag;

                case 4:
                    flag = true;
                    num2 = 3;
                    goto Label_0002;

                case 5:
                    if (num >= intQuotaAmount)
                    {
                        return flag;
                    }
                    num2 = 4;
                    goto Label_0002;
            }
        Label_0023:
            flag = false;
            num = 0;
            reader = new Survey_ss_Layer().ExcuteSql(sQuotaCondition);
            num2 = 0;
            goto Label_0002;
        Label_0065:
            reader.Close();
            num2 = 5;
            goto Label_0002;
        }

        //protected bool checkQuotaInCondition(string sInput)
        //{
        //    bool flag = false; //赋初值
        //    string[] strArray = null; //赋初值
        //    string[][] strArray2 = null; //赋初值
        //    string str = null; //赋初值
        //    string[] strArray3 = null; //赋初值
        //    bool flag2 = false; //赋初值
        //    int num = 0; //赋初值
        //    int num2 = 0; //赋初值
        //    string str2 = null; //赋初值
        //    string str3 = null; //赋初值
        //    string str4 = null; //赋初值
        //    string str5 = null; //赋初值
        //    string str6 = null; //赋初值
        //    int num3 = 0; //赋初值
        //    string[] strArray4 = null; //赋初值
        //    int num4 = 0; //赋初值
        //    string[] strArray5 = null; //赋初值
        //    int num5 = 0; //赋初值
        //    string[] strArray6 = null; //赋初值
        //    int num6 = 0; //赋初值
        //    string[] strArray7 = null; //赋初值
        //    int num7 = 0; //赋初值
        //    int num8 = 0; //赋初值
        //    goto Label_015E;
        //Label_0005:
        //    switch (num8)
        //    {
        //        case 0:
        //            if (Convert.ToInt32(str) > Convert.ToInt32(strArray2[num][2]))
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 0x1c;
        //            goto Label_0005;

        //        case 1:
        //            if (Convert.ToInt32(str) >= Convert.ToInt32(strArray2[num][2]))
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 0x2e;
        //            goto Label_0005;

        //        case 2:
        //            flag = false;
        //            num8 = 30;
        //            goto Label_0005;

        //        case 3:
        //            if (Convert.ToInt32(str) < Convert.ToInt32(strArray2[num][2]))
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 13;
        //            goto Label_0005;

        //        case 4:
        //            switch (num3)
        //            {
        //                case 0:
        //                    num8 = 15;
        //                    goto Label_0005;

        //                case 1:
        //                    num8 = 0x24;
        //                    goto Label_0005;

        //                case 2:
        //                    num8 = 0;
        //                    goto Label_0005;

        //                case 3:
        //                    num8 = 1;
        //                    goto Label_0005;

        //                case 4:
        //                    num8 = 3;
        //                    goto Label_0005;

        //                case 5:
        //                    num8 = 80;
        //                    goto Label_0005;

        //                case 6:
        //                    num8 = 0x18;
        //                    goto Label_0005;

        //                case 7:
        //                    num8 = 0x1d;
        //                    goto Label_0005;

        //                case 8:
        //                    strArray3 = str.Split(new char[] { ',' });
        //                    flag2 = false;
        //                    strArray4 = strArray3;
        //                    num4 = 0;
        //                    num8 = 0x31;
        //                    goto Label_0005;

        //                case 9:
        //                    strArray3 = str.Split(new char[] { ',' });
        //                    flag2 = true;
        //                    strArray5 = strArray3;
        //                    num5 = 0;
        //                    num8 = 12;
        //                    goto Label_0005;

        //                case 10:
        //                    strArray3 = str.Split(new char[] { ',' });
        //                    flag2 = true;
        //                    strArray6 = strArray3;
        //                    num6 = 0;
        //                    num8 = 0x43;
        //                    goto Label_0005;

        //                case 11:
        //                    strArray3 = str.Split(new char[] { ',' });
        //                    flag2 = true;
        //                    num2 = 0;
        //                    strArray7 = strArray3;
        //                    num7 = 0;
        //                    num8 = 0x20;
        //                    goto Label_0005;
        //            }
        //            num8 = 0x3e;
        //            goto Label_0005;

        //        case 5:
        //            if (flag2)
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 10;
        //            goto Label_0005;

        //        case 6:
        //        case 14:
        //            num8 = 0x13;
        //            goto Label_0005;

        //        case 7:
        //            if (strArray2[num][2].IndexOf("|" + str5 + "|") < 0)
        //            {
        //                goto Label_0482;
        //            }
        //            num8 = 0x22;
        //            goto Label_0005;

        //        case 8:
        //        case 0x19:
        //        case 30:
        //        case 0x2a:
        //        case 0x2d:
        //        case 0x30:
        //        case 0x37:
        //        case 0x39:
        //        case 0x3a:
        //        case 0x47:
        //        case 0x49:
        //        case 0x4d:
        //        case 0x51:
        //            goto Label_094D;

        //        case 9:
        //            if (strArray2[num][2].ToString().IndexOf("|" + str2 + "|") < 0)
        //            {
        //                num4++;
        //                num8 = 0x1f;
        //            }
        //            else
        //            {
        //                num8 = 11;
        //            }
        //            goto Label_0005;

        //        case 10:
        //            flag = false;
        //            num8 = 0x4d;
        //            goto Label_0005;

        //        case 11:
        //            flag2 = true;
        //            num8 = 0x25;
        //            goto Label_0005;

        //        case 12:
        //        case 0x41:
        //            num8 = 0x11;
        //            goto Label_0005;

        //        case 13:
        //            flag = false;
        //            num8 = 0x37;
        //            goto Label_0005;

        //        case 15:
        //            if (str == strArray2[num][2])
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 0x17;
        //            goto Label_0005;

        //        case 0x10:
        //            {
        //                Dictionary<string, int> dictionary1 = new Dictionary<string, int>(12);
        //                dictionary1.Add("0", 0);
        //                dictionary1.Add("1", 1);
        //                dictionary1.Add("2", 2);
        //                dictionary1.Add("3", 3);
        //                dictionary1.Add("4", 4);
        //                dictionary1.Add("5", 5);
        //                dictionary1.Add("6", 6);
        //                dictionary1.Add("7", 7);
        //                dictionary1.Add("8", 8);
        //                dictionary1.Add("11", 9);
        //                dictionary1.Add("9", 10);
        //                dictionary1.Add("10", 11);
        //                ModelSurvey_ss.dictionary1 = dictionary1;
        //                num8 = 0x4c;
        //                goto Label_0005;
        //            }
        //        case 0x11:
        //            if (num5 < strArray5.Length)
        //            {
        //                str3 = strArray5[num5];
        //                num8 = 0x3f;
        //            }
        //            else
        //            {
        //                num8 = 0x40;
        //            }
        //            goto Label_0005;

        //        case 0x12:
        //        case 0x20:
        //            num8 = 0x52;
        //            goto Label_0005;

        //        case 0x13:
        //            if (num2 == (strArray2[num][2].Split(new char[] { '|' }).Length - 2))
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 0x15;
        //            goto Label_0005;

        //        case 20:
        //        case 40:
        //            num8 = 0x2b;
        //            goto Label_0005;

        //        case 0x15:
        //            flag = false;
        //            num8 = 0x49;
        //            goto Label_0005;

        //        case 0x16:
        //            num++;
        //            num8 = 40;
        //            goto Label_0005;

        //        case 0x17:
        //            flag = false;
        //            num8 = 0x51;
        //            goto Label_0005;

        //        case 0x18:
        //            if (str.ToString().IndexOf(strArray2[num][2]) >= 0)
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 0x4a;
        //            goto Label_0005;

        //        case 0x1a:
        //            if (strArray2[num][2].ToString().IndexOf("|" + str4 + "|") < 0)
        //            {
        //                num6++;
        //                num8 = 0x42;
        //            }
        //            else
        //            {
        //                num8 = 0x4e;
        //            }
        //            goto Label_0005;

        //        case 0x1b:
        //        case 0x40:
        //            num8 = 5;
        //            goto Label_0005;

        //        case 0x1c:
        //            flag = false;
        //            num8 = 0x47;
        //            goto Label_0005;

        //        case 0x1d:
        //            if (str.ToString().IndexOf(strArray2[num][2]) < 0)
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 0x33;
        //            goto Label_0005;

        //        case 0x1f:
        //        case 0x31:
        //            num8 = 0x53;
        //            goto Label_0005;

        //        case 0x21:
        //            num = 0;
        //            num8 = 20;
        //            goto Label_0005;

        //        case 0x22:
        //            num2++;
        //            num8 = 6;
        //            goto Label_0005;

        //        case 0x23:
        //            if (ModelSurvey_ss.dictionary1 != null)
        //            {
        //                goto Label_07D6;
        //            }
        //            num8 = 0x10;
        //            goto Label_0005;

        //        case 0x24:
        //            if (str != strArray2[num][2])
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 50;
        //            goto Label_0005;

        //        case 0x25:
        //        case 0x4b:
        //            num8 = 0x29;
        //            goto Label_0005;

        //        case 0x26:
        //            if (num6 < strArray6.Length)
        //            {
        //                str4 = strArray6[num6];
        //                num8 = 0x1a;
        //            }
        //            else
        //            {
        //                num8 = 0x3b;
        //            }
        //            goto Label_0005;

        //        case 0x27:
        //        case 0x38:
        //            num8 = 0x2c;
        //            goto Label_0005;

        //        case 0x29:
        //            if (flag2)
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 0x2f;
        //            goto Label_0005;

        //        case 0x2b:
        //            if (num < strArray.Length)
        //            {
        //                str = base.Request.Form["F" + strArray2[num][0]];
        //                num8 = 0x3d;
        //            }
        //            else
        //            {
        //                num8 = 0x44;
        //            }
        //            goto Label_0005;

        //        case 0x2c:
        //            if (num < strArray.Length)
        //            {
        //                strArray2[num] = strArray[num].Split(new char[] { ':' });
        //                num++;
        //                num8 = 0x27;
        //            }
        //            else
        //            {
        //                num8 = 0x21;
        //            }
        //            goto Label_0005;

        //        case 0x2e:
        //            if ((1 == 0) || (0 == 0))
        //            {
        //                flag = false;
        //                num8 = 0x19;
        //                goto Label_0005;
        //            }
        //            goto Label_0482;

        //        case 0x2f:
        //            flag = false;
        //            num8 = 0x2a;
        //            goto Label_0005;

        //        case 50:
        //            flag = false;
        //            num8 = 0x2d;
        //            goto Label_0005;

        //        case 0x33:
        //            flag = false;
        //            num8 = 8;
        //            goto Label_0005;

        //        case 0x34:
        //            flag = false;
        //            num8 = 0x3a;
        //            goto Label_0005;

        //        case 0x35:
        //            num8 = 0x23;
        //            goto Label_0005;

        //        case 0x36:
        //            flag2 = false;
        //            num8 = 0x1b;
        //            goto Label_0005;

        //        case 0x3b:
        //        case 0x48:
        //            num8 = 70;
        //            goto Label_0005;

        //        case 60:
        //            num8 = 4;
        //            goto Label_0005;

        //        case 0x3d:
        //            str6 = strArray2[num][1];
        //            if (str6 == null)
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 0x35;
        //            goto Label_0005;

        //        case 0x3e:
        //            num8 = 0x39;
        //            goto Label_0005;

        //        case 0x3f:
        //            if (strArray2[num][2].ToString().IndexOf("|" + str3 + "|") >= 0)
        //            {
        //                num5++;
        //                num8 = 0x41;
        //            }
        //            else
        //            {
        //                num8 = 0x36;
        //            }
        //            goto Label_0005;

        //        case 0x42:
        //        case 0x43:
        //            num8 = 0x26;
        //            goto Label_0005;

        //        case 0x44:
        //            return flag;

        //        case 0x45:
        //            if (!ModelSurvey_ss.dictionary1.TryGetValue(str6, out num3))
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 60;
        //            goto Label_0005;

        //        case 70:
        //            if (flag2)
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 2;
        //            goto Label_0005;

        //        case 0x4a:
        //            flag = false;
        //            num8 = 0x30;
        //            goto Label_0005;

        //        case 0x4c:
        //            goto Label_07D6;

        //        case 0x4e:
        //            flag2 = false;
        //            num8 = 0x48;
        //            goto Label_0005;

        //        case 0x4f:
        //            if (!flag)
        //            {
        //                return flag;
        //            }
        //            num8 = 0x16;
        //            goto Label_0005;

        //        case 80:
        //            if (Convert.ToInt32(str) <= Convert.ToInt32(strArray2[num][2]))
        //            {
        //                goto Label_094D;
        //            }
        //            num8 = 0x34;
        //            goto Label_0005;

        //        case 0x52:
        //            if (num7 < strArray7.Length)
        //            {
        //                str5 = strArray7[num7];
        //                num8 = 7;
        //            }
        //            else
        //            {
        //                num8 = 14;
        //            }
        //            goto Label_0005;

        //        case 0x53:
        //            if (num4 < strArray4.Length)
        //            {
        //                str2 = strArray4[num4];
        //                num8 = 9;
        //            }
        //            else
        //            {
        //                num8 = 0x4b;
        //            }
        //            goto Label_0005;
        //    }
        //Label_015E:
        //    flag = true;
        //    strArray = sInput.Split(new char[] { '*' });
        //    strArray2 = new string[strArray.Length][];
        //    str = "";
        //    strArray3 = null;
        //    flag2 = false;
        //    num = 0;
        //    num2 = 0;
        //    num = 0;
        //    num8 = 0x38;
        //    goto Label_0005;
        //Label_0482:
        //    num7++;
        //    num8 = 0x12;
        //    goto Label_0005;
        //Label_07D6:
        //    num8 = 0x45;
        //    goto Label_0005;
        //Label_094D:
        //    num8 = 0x4f;
        //    goto Label_0005;
        //}

        protected void checkRepeatAnswerItem(string sCheckStr, long SID)
        {
            string[] strArray = null; //赋初值
            string str = null; //赋初值
            string str2 = null; //赋初值
            int num = 0; //赋初值
            SqlDataReader reader = null; //赋初值
            int num2 = 0; //赋初值
            int num3 = 6;
        Label_000D:
            switch (num3)
            {
                case 0:
                case 15:
                case 0x12:
                    goto Label_0303;

                case 1:
                case 14:
                    num3 = 0x11;
                    goto Label_000D;

                case 2:
                    str = str.Substring(0, str.Length - 3);
                    num3 = 0x10;
                    goto Label_000D;

                case 3:
                    if (!(str != ""))
                    {
                        break;
                    }
                    num3 = 2;
                    goto Label_000D;

                case 4:
                    switch (num2)
                    {
                        case 1:
                        case 8:
                        case 9:
                        case 10:
                            {
                                string str3 = str;
                                str = str3 + str2 + "='" + base.Request.Form[str2] + "'  OR ";
                                num3 = 0x12;
                                goto Label_000D;
                            }
                        case 2:
                        case 4:
                        case 5:
                        case 6:
                            {
                                string str4 = str;
                                str = str4 + str2 + "=" + base.Request.Form[str2] + "  OR ";
                                num3 = 15;
                                goto Label_000D;
                            }
                        case 3:
                        case 7:
                            goto Label_0303;
                    }
                    num3 = 5;
                    goto Label_000D;

                case 5:
                    num3 = 0;
                    goto Label_000D;

                case 7:
                    if (sCheckStr != null)
                    {
                        strArray = sCheckStr.Split(new char[] { '|' });
                        str = "";
                        str2 = "";
                        num = 0;
                        num3 = 1;
                    }
                    else
                    {
                        num3 = 10;
                    }
                    goto Label_000D;

                case 8:
                    reader.Dispose();
                    base.Response.Redirect("Error.aspx?EC=35&SID=" + SID.ToString(), true);
                    num3 = 11;
                    goto Label_000D;

                case 9:
                    num3 = 3;
                    goto Label_000D;

                case 10:
                    return;

                case 11:
                    goto Label_0354;

                case 12:
                    if (!reader.Read())
                    {
                        goto Label_0354;
                    }
                    num3 = 8;
                    goto Label_000D;

                case 13:
                    num3 = 7;
                    goto Label_000D;

                case 0x10:
                    break;

                case 0x11:
                    if (num < strArray.Length)
                    {
                        str2 = "F" + strArray[num].Substring(0, strArray[num].IndexOf(':'));
                        num2 = Convert.ToInt32(strArray[num].Substring(strArray[num].IndexOf(':') + 1));
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 9;
                    }
                    goto Label_000D;

                default:
                    if (sCheckStr == "")
                    {
                        return;
                    }
                    num3 = 13;
                    goto Label_000D;
            }
            //objComm.CommandText = "SELECT TOP 1 ID FROM Z" + SID.ToString() + " WHERE " + str;
            reader = new Survey_ss_Layer().GetAnswerInfo1(SID.ToString(), str);
            num3 = 12;
            goto Label_000D;
        Label_0303:
            num++;
            num3 = 14;
            goto Label_000D;
        Label_0354:
            reader.Dispose();
        }

        protected void doCheckCode(string sCheckStr, string SID)
        {
            string str = null; //赋初值
            HttpCookie cookie = null; //赋初值
            int num = 8;
        Label_000D:
            switch (num)
            {
                case 0:
                    base.Response.Redirect("Error.aspx?ec=16&SID=" + SID);
                    num = 2;
                    goto Label_000D;

                case 1:
                    break;

                case 2:
                    goto Label_0194;

                case 3:
                    return;

                case 4:
                    base.Response.Redirect("Error.aspx?ec=5&SID=" + SID);
                    num = 1;
                    goto Label_000D;

                case 5:
                    goto Label_0139;

                case 6:
                    if (!(cookie.Value != str))
                    {
                        goto Label_0194;
                    }
                    num = 0;
                    goto Label_000D;

                case 7:
                    base.Response.Redirect("Error.aspx?ec=8&SID=" + SID);
                    num = 5;
                    goto Label_000D;

                case 9:
                    //if (str != null)
                    //{
                        break;
                    //}
                    //num = 4;
                    goto Label_000D;

                case 10:
                    if (cookie != null)
                    {
                        goto Label_0139;
                    }
                    num = 7;
                    goto Label_000D;

                default:
                    if (sCheckStr.IndexOf("|CheckCode:1|") >= 0)
                    {
                        str = Convert.ToString(base.Request.Form["CheckCode"]);
                        num = 9;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_000D;
            }
            cookie = HttpContext.Current.Request.Cookies["CheckCode"];
            num = 10;
            goto Label_000D;
        Label_0139:
            num = 6;
            goto Label_000D;
        Label_0194:
            base.Response.Cookies["CheckCode"].Expires = DateTime.Now.AddDays(-1.0);
            cookie = null;
        }

        protected void doCheckIP(string sCheckStr, long SID, string sSubmitIP)
        {
            SqlDataReader reader = null; //赋初值
            int num = 2;
        Label_0011:
            switch (num)
            {
                case 0:
                    base.Response.Redirect("Error.aspx?ec=13&SID=" + SID);
                    num = 1;
                    goto Label_0011;

                case 1:
                    goto Label_00E6;

                case 2:
                    if ((1 == 0) || (0 == 0))
                    {
                        break;
                    }
                    goto Label_0011;

                case 3:
                    return;

                case 4:
                    if (!reader.Read())
                    {
                        goto Label_00E6;
                    }
                    num = 0;
                    goto Label_0011;
            }
            if (sCheckStr.IndexOf("|IP:1|") >= 0)
            {
                num = 3;
            }
            else
            {
                //objComm.CommandText = "SELECT TOP 1 ID FROM Z" + SID.ToString() + " WHERE IP='" + sSubmitIP + "'";
                reader = new Survey_ss_Layer().GetAnswerInfoIP(sSubmitIP, SID.ToString());
                num = 4;
            }
            goto Label_0011;
        Label_00E6:
            reader.Dispose();
        }

        protected void doCookies(string sCheckStr,long SID)
        {
            HttpCookie cookie = null; //赋初值
            int num = 6;
        Label_000D:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    num = 3;
                    goto Label_000D;

                case 2:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (cookie == null)
                    {
                        base.Response.Cookies["Answered"].Value = SID.ToString() + ",";
                        base.Response.Cookies["Answered"].Expires = DateTime.Now.AddYears(0x3e8);
                        return;
                    }
                    num = 1;
                    goto Label_000D;

                case 3:
                    if (("," + cookie.Value).ToString().IndexOf("," + SID.ToString() + ",") < 0)
                    {
                        break;
                    }
                    num = 4;
                    goto Label_000D;

                case 4:
                    base.Response.Redirect("Error.aspx?ec=44&SID=" + SID);
                    num = 0;
                    goto Label_000D;

                case 5:
                    return;

                default:
                    if (sCheckStr.IndexOf("|Cookies:1|") >= 0)
                    {
                        cookie = HttpContext.Current.Request.Cookies["Answered"];
                        num = 2;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_000D;
            }
            base.Response.Cookies["Answered"].Value = cookie.Value + SID.ToString() + ",";
            base.Response.Cookies["Answered"].Expires = DateTime.Now.AddYears(0x3e8);
        }

        //protected bool doQuota(int SID, string[] arrQuota)
        //{
        //    bool flag;
        //    bool flag2;
        //    string[] strArray = null; //赋初值
        //    int num;
        //    int num2;
        //    goto Label_0047;
        //Label_0002:
        //    switch (num2)
        //    {
        //        case 0:
        //        case 11:
        //            num2 = 9;
        //            goto Label_0002;

        //        case 1:
        //            return flag;

        //        case 2:
        //            return false;

        //        case 3:
        //            if (arrQuota == null)
        //            {
        //                goto Label_00DD;
        //            }
        //            num2 = 5;
        //            goto Label_0002;

        //        case 4:
        //            goto Label_00DD;

        //        case 5:
        //            if ((1 == 0) || (0 == 0))
        //            {
        //                num2 = 8;
        //                goto Label_0002;
        //            }
        //            goto Label_0079;

        //        case 6:
        //            if (!(strArray[3] == "True"))
        //            {
        //                goto Label_0079;
        //            }
        //            num2 = 2;
        //            goto Label_0002;

        //        case 7:
        //            command.CommandText = "UPDATE Quota SET Active=1 WHERE SID=" + SID.ToString() + " AND QID=" + strArray[0];
        //            command.ExecuteNonQuery();
        //            return false;

        //        case 8:
        //            if (arrQuota.Length != 0)
        //            {
        //                num = 0;
        //                num2 = 0;
        //            }
        //            else
        //            {
        //                num2 = 4;
        //            }
        //            goto Label_0002;

        //        case 9:
        //            if (num < arrQuota.Length)
        //            {
        //                strArray = arrQuota[num].Split(new char[] { '@' });
        //                flag2 = this.checkQuotaInCondition(strArray[1]);
        //                num2 = 12;
        //            }
        //            else
        //            {
        //                num2 = 1;
        //            }
        //            goto Label_0002;

        //        case 10:
        //            if (flag)
        //            {
        //                goto Label_00DF;
        //            }
        //            num2 = 7;
        //            goto Label_0002;

        //        case 12:
        //            if (flag2)
        //            {
        //                num2 = 6;
        //            }
        //            else
        //            {
        //                num2 = 13;
        //            }
        //            goto Label_0002;

        //        case 13:
        //            flag = true;
        //            num2 = 14;
        //            goto Label_0002;

        //        case 14:
        //            goto Label_00DF;
        //    }
        //Label_0047:
        //    command = new OleDbCommand("", objConn);
        //    flag = false;
        //    flag2 = false;
        //    num = 0;
        //    num2 = 3;
        //    goto Label_0002;
        //Label_0079:
        //    flag = this.checkQuota(objConn, strArray[4], Convert.ToInt32(strArray[2]));
        //    num2 = 10;
        //    goto Label_0002;
        //Label_00DD:
        //    return true;
        //Label_00DF:
        //    num++;
        //    num2 = 11;
        //    goto Label_0002;
        //}

        protected string getClientFolder(string SID)
        {
            string str;
        Label_0017:
            str = "";
            //SqlDataReader reader = new OleDbCommand("SELECT TOP 1 UID FROM SurveyTable WHERE SID=" + SID, objConn).ExecuteReader();
            SqlDataReader reader = new Survey_ss_Layer().GetSurveyTableUID(SID);
            int num = 2;
        Label_0002:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    str = reader["UID"].ToString();
                    num = 0;
                    goto Label_0002;

                case 2:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num = 1;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            if ((1 != 0) && (0 != 0))
            {
            }
            reader.Close();
            string str2 = base.Server.MapPath("ss.aspx");
            return (str2.Substring(0, str2.LastIndexOf('\\')) + @"\UserData\U" + str + @"\ClientFile\");
        }

        protected string getFormatFileName(string SID, string sOldFileName)
        {
            string str;
        Label_0017:
            str = "";
            int num = 2;
        Label_0002:
            switch (num)
            {
                case 0:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    break;

                case 1:
                    str = sOldFileName.Substring(sOldFileName.LastIndexOf('.') + 1);
                    sOldFileName = sOldFileName.Substring(0, 0x19) + "." + str;
                    num = 0;
                    goto Label_0002;

                case 2:
                    if (sOldFileName.Length < 30)
                    {
                        break;
                    }
                    num = 1;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            return string.Concat(new object[] { SID, "_", DateTime.Now.ToFileTime(), "_", sOldFileName });
        }

        protected string[] getItem15Form(string IID, string[,] arrItem)
        {
            int num;
            string[] strArray;
            int num2;
            goto Label_0027;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 6:
                    num2 = 2;
                    goto Label_0002;

                case 1:
                    string[] strArray2;
                    string[] strArray3;
                    (strArray2 = strArray)[0] = strArray2[0] + "F" + arrItem[num, 0] + ",";
                    (strArray3 = strArray)[1] = strArray3[1] + "'" + base.Request.Form["F" + arrItem[num, 0]] + "',";
                    num2 = 5;
                    goto Label_0002;

                case 2:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (num < arrItem.GetLength(0))
                    {
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    goto Label_0002;

                case 3:
                    if (!(arrItem[num, 3] == IID.ToString()))
                    {
                        goto Label_0042;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 4:
                    return strArray;

                case 5:
                    goto Label_0042;
            }
        Label_0027:
            num = 0;
            strArray = new string[2];
            num = 0;
            num2 = 6;
            goto Label_0002;
        Label_0042:
            num++;
            num2 = 0;
            goto Label_0002;
        }

        protected string[] getItem16Form(string IID, string[,] arrItem, string[,] arrOption)
        {
            int num;
            int num2;
            string[] strArray;
            string str;
            string[] strArray2 = null; //赋初值
            int num3;
            goto Label_004F;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num2 < strArray2.Length)
                    {
                        string[] strArray3 = strArray2; //赋初值
                        string[] strArray5 = strArray2; //赋初值
                        string str2 = strArray3[0];
                        (strArray3 = strArray)[0] = str2 + "F" + arrItem[num, 0] + "_" + strArray2[num2] + ",";
                        (strArray5 = strArray)[1] = strArray5[1] + "'" + base.Request.Form["F" + arrItem[num, 0] + "_" + strArray2[num2]] + "',";
                        num2++;
                        num3 = 11;
                    }
                    else
                    {
                        num3 = 5;
                    }
                    goto Label_0002;

                case 1:
                    if (num < arrItem.GetLength(0))
                    {
                        num3 = 0x10;
                    }
                    else
                    {
                        num3 = 6;
                    }
                    goto Label_0002;

                case 2:
                case 11:
                    num3 = 0;
                    goto Label_0002;

                case 3:
                    num2 = 0;
                    num3 = 2;
                    goto Label_0002;

                case 4:
                case 7:
                    num3 = 1;
                    goto Label_0002;

                case 5:
                    goto Label_0183;

                case 6:
                    return strArray;

                case 8:
                    strArray2 = str.Substring(0, str.Length - 1).Split(new char[] { '|' });
                    num = 0;
                    num3 = 7;
                    goto Label_0002;

                case 9:
                case 10:
                    num3 = 12;
                    goto Label_0002;

                case 12:
                    if (num2 < arrOption.GetLength(0))
                    {
                        num3 = 13;
                    }
                    else
                    {
                        num3 = 8;
                    }
                    goto Label_0002;

                case 13:
                    if (!(arrOption[num2, 1] == IID.ToString()))
                    {
                        goto Label_025E;
                    }
                    num3 = 14;
                    goto Label_0002;

                case 14:
                    str = str + arrOption[num2, 0] + "|";
                    num3 = 15;
                    goto Label_0002;

                case 15:
                    goto Label_025E;

                case 0x10:
                    if (!(arrItem[num, 3] == IID.ToString()))
                    {
                        goto Label_0183;
                    }
                    num3 = 3;
                    goto Label_0002;
            }
        Label_004F:
            num = 0;
            num2 = 0;
            strArray = new string[2];
            str = "";
            num2 = 0;
            num3 = 10;
            goto Label_0002;
        Label_0183:
            num++;
            num3 = 4;
            goto Label_0002;
        Label_025E:
            num2++;
            num3 = 9;
            goto Label_0002;
        }

        protected string[] getItem18Form(string IID, string[,] arrItem, string[,] arrOption)
        {
            int num;
            int num2;
            string[] strArray;
            string str;
            string[] strArray2 = null; //赋初值
            int num3;
            goto Label_0057;
        Label_0002:
            switch (num3)
            {
                case 0:
                    num3 = 12;
                    goto Label_0002;

                case 1:
                case 0x12:
                    num3 = 5;
                    goto Label_0002;

                case 2:
                    strArray2 = str.Substring(0, str.Length - 1).Split(new char[] { '|' });
                    num = 0;
                    num3 = 14;
                    goto Label_0002;

                case 3:
                    if (!(arrOption[num2, 1] == IID.ToString()))
                    {
                        goto Label_02D8;
                    }
                    num3 = 0;
                    goto Label_0002;

                case 4:
                    num2 = 0;
                    num3 = 1;
                    goto Label_0002;

                case 5:
                    if (num2 < strArray2.Length)
                    {
                        string[] strArray3 = strArray2; //赋初值
                        string[] strArray5 = strArray2;//赋初值
                        string str2 = strArray3[0];
                        (strArray3 = strArray)[0] = str2 + "F" + arrItem[num, 0] + "_" + strArray2[num2] + ",";
                        (strArray5 = strArray)[1] = strArray5[1] + "'" + base.Request.Form["F" + arrItem[num, 0] + "_" + strArray2[num2]] + "',";
                        num2++;
                        num3 = 0x12;
                    }
                    else
                    {
                        num3 = 6;
                    }
                    goto Label_0002;

                case 6:
                    goto Label_0188;

                case 7:
                case 8:
                    num3 = 0x11;
                    goto Label_0002;

                case 9:
                    if (num < arrItem.GetLength(0))
                    {
                        num3 = 0x10;
                    }
                    else
                    {
                        num3 = 15;
                    }
                    goto Label_0002;

                case 10:
                    goto Label_02D8;

                case 11:
                    str = str + arrOption[num2, 0] + "|";
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num3 = 10;
                    goto Label_0002;

                case 12:
                    if (!(arrOption[num2, 2] == "True"))
                    {
                        goto Label_02D8;
                    }
                    num3 = 11;
                    goto Label_0002;

                case 13:
                case 14:
                    num3 = 9;
                    goto Label_0002;

                case 15:
                    return strArray;

                case 0x10:
                    if (!(arrItem[num, 3] == IID.ToString()))
                    {
                        goto Label_0188;
                    }
                    num3 = 4;
                    goto Label_0002;

                case 0x11:
                    if (num2 < arrOption.GetLength(0))
                    {
                        num3 = 3;
                    }
                    else
                    {
                        num3 = 2;
                    }
                    goto Label_0002;
            }
        Label_0057:
            num = 0;
            num2 = 0;
            strArray = new string[2];
            str = "";
            num2 = 0;
            num3 = 8;
            goto Label_0002;
        Label_0188:
            num++;
            num3 = 13;
            goto Label_0002;
        Label_02D8:
            num2++;
            num3 = 7;
            goto Label_0002;
        }

        protected string[] getItem19Form(string IID, string[,] arrItem)

        {
            int num;
            string str;
            string[] strArray;
            int num2;
            goto Label_002D;
        Label_0002:
            if ((1 != 0) && (0 != 0))
            {
            }
            switch (num2)
            {
                case 0:
                case 3:
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    if (num < arrItem.GetLength(0))
                    {
                        num2 = 5;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    goto Label_0002;

                case 2:
                    goto Label_004E;

                case 4:
                    return strArray;

                case 5:
                    if (!(arrItem[num, 3] == IID.ToString()))
                    {
                        goto Label_004E;
                    }
                    num2 = 6;
                    goto Label_0002;

                case 6:
                    {
                        string[] strArray2=strArray; //赋初值
                        string[] strArray4=strArray; //赋初值
                        str = "F" + arrItem[num, 0];
                        string str2 = strArray2[0];
                        (strArray2 = strArray)[0] = str2 + str + "," + str + "_Input,";
                        string str3 = strArray4[1];
                        (strArray4 = strArray)[1] = str3 + base.Request.Form[str] + ",'" + base.Request.Form[str + "_Input"] + "',";
                        num2 = 2;
                        goto Label_0002;
                    }
            }
        Label_002D:
            num = 0;
            str = "";
            strArray = new string[2];
            num = 0;
            num2 = 0;
            goto Label_0002;
        Label_004E:
            num++;
            num2 = 3;
            goto Label_0002;
        }          

        protected string[] getItem20Form(string IID, string[,] arrItem)
        {
            int num;
            string str;
            string[] strArray;
            int num2;
            goto Label_0027;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (num < arrItem.GetLength(0))
                    {
                        num2 = 2;
                    }
                    else
                    {
                        num2 = 5;
                    }
                    goto Label_0002;

                case 1:
                case 3:
                    num2 = 0;
                    goto Label_0002;

                case 2:
                    if (!(arrItem[num, 3] == IID.ToString()))
                    {
                        goto Label_0048;
                    }
                    num2 = 6;
                    goto Label_0002;

                case 4:
                    goto Label_0048;

                case 5:
                    return strArray;

                case 6:
                    {
                        string[] strArray2 =strArray; //赋初值
                        string[] strArray4 =strArray; //赋初值
                        str = "F" + arrItem[num, 0];
                        string str2 = strArray2[0];
                        (strArray2 = strArray)[0] = str2 + str + "," + str + "_Input,";
                        string str3 = strArray4[1];
                        (strArray4 = strArray)[1] = str3 + "'" + base.Request.Form[str] + "','" + base.Request.Form[str + "_Input"] + "',";
                        num2 = 4;
                        goto Label_0002;
                    }
            }
        Label_0027:
            num = 0;
            str = "";
            strArray = new string[2];
            num = 0;
            num2 = 3;
            goto Label_0002;
        Label_0048:
            num++;
            num2 = 1;
            goto Label_0002;
        }

        protected string[] getItem21Form(string IID, string[,] arrItem)
        {
            int num;
            string str;
            string[] strArray;
            int num2;
            goto Label_0027;
        Label_0002:
            switch (num2)
            {
                case 0:
                    goto Label_004F;

                case 1:
                    string[] strArray2;
                    string[] strArray3;
                    str = "F" + arrItem[num, 0];
                    (strArray2 = strArray)[0] = strArray2[0] + str + ",";
                    (strArray3 = strArray)[1] = strArray3[1] + "'" + base.Request.Form[str] + "',";
                    num2 = 0;
                    goto Label_0002;

                case 2:
                    return strArray;

                case 3:
                    if (num < arrItem.GetLength(0))
                    {
                        num2 = 5;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 4:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_00FC;
                    }
                    goto Label_004F;

                case 5:
                    if (!(arrItem[num, 3] == IID.ToString()))
                    {
                        goto Label_004F;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 6:
                    goto Label_00FC;
            }
        Label_0027:
            num = 0;
            str = "";
            strArray = new string[2];
            num = 0;
            num2 = 4;
            goto Label_0002;
        Label_004F:
            num++;
            num2 = 6;
            goto Label_0002;
        Label_00FC:
            num2 = 3;
            goto Label_0002;
        }

        protected string[] getItem7Form(string IID, string[,] arrItem)
        {
            int num;
            string[] strArray;
            int num2;
            goto Label_0027;
        Label_0002:
            switch (num2)
            {
                case 0:
                    string[] strArray2;
                    string[] strArray3;
                    (strArray2 = strArray)[0] = strArray2[0] + "F" + arrItem[num, 0] + ",";
                    (strArray3 = strArray)[1] = strArray3[1] + base.Request.Form["F" + arrItem[num, 0]] + ",";
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    goto Label_0042;

                case 2:
                    return strArray;

                case 3:
                    if (num < arrItem.GetLength(0))
                    {
                        num2 = 5;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 4:
                case 6:
                    num2 = 3;
                    goto Label_0002;

                case 5:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (!(arrItem[num, 3] == IID.ToString()))
                    {
                        goto Label_0042;
                    }
                    num2 = 0;
                    goto Label_0002;
            }
        Label_0027:
            num = 0;
            strArray = new string[2];
            num = 0;
            num2 = 4;
            goto Label_0002;
        Label_0042:
            num++;
            num2 = 6;
            goto Label_0002;
        }

        protected string getItemStr(SqlDataReader dr, long SID)
        {
        Label_001B:
            StringBuilder builder = new StringBuilder();
            //objComm.CommandText = "  SELECT IID,ItemType,OptionAmount,ParentID,DataFormatCheck FROM ItemTable WHERE SID=" + SID.ToString() + " ORDER BY PageNo,Sort";
            dr = new Survey_ss_Layer().GetItemTable(SID.ToString());
            int num = 2;
        Label_0002:
            switch (num)
            {
                case 0:
                    if (dr.Read())
                    {
                        builder.Append(string.Concat(new object[] { dr["IID"].ToString(), "-", dr["ItemType"], "-", dr["OptionAmount"], "-", dr["ParentID"], "-", dr["DataFormatCheck"], "/" }));
                        num = 3;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_0002;

                case 1:
                    dr.Dispose();
                    return builder.ToString();

                case 2:
                case 3:
                    num = 0;
                    goto Label_0002;
            }
            goto Label_001B;
        }

        protected string getOptionStr(SqlDataReader dr, long SID)
        {
            StringBuilder builder;
        Label_001B:
            builder = new StringBuilder();
            //objComm.CommandText = "  SELECT OID,IID FROM OptionTable WHERE SID=" + SID.ToString();
            dr = new Survey_ss_Layer().GetOptionTable(SID.ToString());
            int num = 0;
        Label_0002:
            switch (num)
            {
                case 0:
                case 1:
                    num = 3;
                    goto Label_0002;

                case 2:
                    dr.Dispose();
                    return builder.ToString();

                case 3:
                    if (!dr.Read())
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num = 2;
                    }
                    else
                    {
                        builder.Append(string.Concat(new object[] { dr["OID"].ToString(), "-", dr["IID"], "/" }));
                        num = 1;
                    }
                    goto Label_0002;
            }
            goto Label_001B;
        }

        //protected string[] getQuota(int SID)
        //{
        //    string str;
        //    SqlDataReader reader;
        //    int num;
        //    goto Label_0027;
        //Label_0002:
        //    switch (num)
        //    {
        //        case 0:
        //            if (!(str != ""))
        //            {
        //                goto Label_016A;
        //            }
        //            num = 6;
        //            goto Label_0002;

        //        case 1:
        //            goto Label_016C;

        //        case 2:
        //            if ((1 == 0) || (0 == 0))
        //            {
        //                goto Label_016C;
        //            }
        //            goto Label_016A;

        //        case 3:
        //            reader.Close();
        //            num = 0;
        //            goto Label_0002;

        //        case 4:
        //            return str.Split(new char[] { '^' });

        //        case 5:
        //            if (reader.Read())
        //            {
        //                object obj2 = str;
        //                str = string.Concat(new object[] { obj2, reader["QID"], "@", reader["QuotaCondition"], "@", reader["QuotaAmount"], "@", reader["Active"], "@", reader["SQLStr"], "^" });
        //                num = 2;
        //            }
        //            else
        //            {
        //                num = 3;
        //            }
        //            goto Label_0002;

        //        case 6:
        //            str = str.Substring(0, str.Length - 1);
        //            num = 4;
        //            goto Label_0002;
        //    }
        //Label_0027:
        //    str = "";
        //    new StringBuilder();
        //    OleDbCommand command = new OleDbCommand("", objConn);
        //    command.CommandText = "SELECT QID,SID,QuotaCondition,QuotaAmount,Active,SQLStr FROM Quota WHERE SID=" + SID.ToString();
        //    reader = command.ExecuteReader();
        //    num = 1;
        //    goto Label_0002;
        //Label_016A:
        //    return null;
        //Label_016C:
        //    num = 5;
        //    goto Label_0002;
        //}

        //protected OleDbCommand getSQL(string[,] DTItemTable, string[,] DTOptionTable, string SID1, string sPar, string sSubmitIP, string GUID, string dNowTime, int intAnswerTime, OleDbConnection objConn, string sUploadTarget, int intPoint)
        //{
        //    string str;
        //    int num;
        //    int num2;
        //    int num3;
        //    int num4;
        //    string str2;
        //    string str3;
        //    string str4;
        //    StringBuilder builder;
        //    bool flag;
        //    OleDbCommand command;
        //    string[] strArray;
        //    HttpFileCollection files = null; //赋初值
        //    HttpPostedFile file = null; //赋初值
        //    string str5 = null; //赋初值
        //    string str6 = null; //赋初值
        //    string str7 = null; //赋初值
        //    int num5 = 0; //赋初值
        //    int num6 = 0; //赋初值

        //    str = "";
        //    num = 0;
        //    num2 = 0;
        //    num3 = 0;
        //    num4 = 0;
        //    str2 = "";
        //    str3 = "";
        //    str4 = "";
        //    builder = new StringBuilder();
        //    flag = false;
        //    command = new OleDbCommand();
        //    strArray = new string[2];
        //    num2 = 0;


        //    goto Label_014E;
        //Label_0005:
        //    switch (num6)
        //    {
        //        case 0:
        //            if (!(sUploadTarget == "DB"))
        //            {
        //                builder.Append("F" + str + "_FileName,");
        //                str3 = str3 + "@F" + str + "_FileName,";
        //                num6 = 0x25;
        //            }
        //            else
        //            {
        //                num6 = 0x2a;
        //            }
        //            goto Label_0005;

        //        case 1:
        //            if (!(DTItemTable[num2, 3] == "0"))
        //            {
        //                goto Label_0954;
        //            }
        //            num6 = 0x2f;
        //            goto Label_0005;

        //        case 2:
        //            if (!(sUploadTarget == "DB"))
        //            {
        //                file.SaveAs(str6 + str5);
        //                num6 = 0x2b;
        //            }
        //            else
        //            {
        //                num6 = 3;
        //            }
        //            goto Label_0005;

        //        case 3:
        //            {
        //                byte[] buffer = new byte[file.ContentLength];
        //                file.InputStream.Read(buffer, 0, file.ContentLength);
        //                command.Parameters.AddWithValue("@F" + DTItemTable[num2, 0].ToString(), buffer);
        //                num6 = 6;
        //                goto Label_0005;
        //            }
        //        case 4:
        //        case 0x1f:
        //            num6 = 0x1c;
        //            goto Label_0005;

        //        case 5:
        //        case 12:
        //        case 0x10:
        //        case 0x1b:
        //        case 0x1d:
        //        case 30:
        //        case 0x20:
        //        case 0x24:
        //        case 0x27:
        //        case 0x31:
        //        case 50:
        //        case 0x3b:
        //        case 60:
        //        case 0x47:
        //        case 0x49:
        //        case 0x4e:
        //            goto Label_0954;

        //        case 6:
        //        case 0x2b:
        //            command.Parameters.AddWithValue("@F" + DTItemTable[num2, 0].ToString() + "_FileName", str5);
        //            num6 = 0x34;
        //            goto Label_0005;

        //        case 7:
        //            if (num4 < Convert.ToInt16(DTItemTable[num2, 2]))
        //            {
        //                str4 = str4 + Convert.ToString(base.Request.Form["F" + str + "_" + num4.ToString()]) + "{$$}";
        //                num4++;
        //                num6 = 0x1a;
        //            }
        //            else
        //            {
        //                num6 = 0x3e;
        //            }
        //            goto Label_0005;

        //        case 8:
        //            if (num3 < 1)
        //            {
        //                goto Label_08D4;
        //            }
        //            num6 = 0x43;
        //            goto Label_0005;

        //        case 9:
        //        case 0x12:
        //            num6 = 0x44;
        //            goto Label_0005;

        //        case 10:
        //        case 0x1a:
        //            num6 = 7;
        //            goto Label_0005;

        //        case 11:
        //            goto Label_0ACF;

        //        case 13:
        //            str4 = str4.Replace("'", "''");
        //            num6 = 0x4b;
        //            goto Label_0005;

        //        case 14:
        //            objConn.Dispose();
        //            base.Response.Redirect("Error.aspx?ec=33&SID=" + SID1);
        //            base.Response.End();
        //            num6 = 0x18;
        //            goto Label_0005;

        //        case 15:
        //            try
        //            {
        //                str4 = Convert.ToString(base.Request.Form["F" + str + "_input"]);
        //            }
        //            catch
        //            {
        //                str4 = "";
        //            }
        //            builder.Append("F" + str + "_input,");
        //            str3 = str3 + "'" + str4 + "',";
        //            num6 = 0x47;
        //            goto Label_0005;

        //        case 0x11:
        //            goto Label_08D4;

        //        case 0x13:
        //            if (Convert.ToInt32(str7) >= (file.ContentLength / 0x400))
        //            {
        //                goto Label_0317;
        //            }
        //            num6 = 14;
        //            goto Label_0005;

        //        case 20:
        //            num6 = 0x4f;
        //            goto Label_0005;

        //        case 0x15:
        //        case 0x38:
        //            str2 = "INSERT INTO Z" + SID1 + " (" + builder.ToString() + ") VALUES(" + str3 + ")";
        //            command.CommandText = str2;
        //            num6 = 0x33;
        //            goto Label_0005;

        //        case 0x16:
        //            num6 = 0x27;
        //            goto Label_0005;

        //        case 0x17:
        //            num6 = 8;
        //            goto Label_0005;

        //        case 0x18:
        //            goto Label_0317;

        //        case 0x19:
        //            {
        //                str4 = str4 + num3.ToString() + ":" + Convert.ToString(base.Request.Form["F" + str + "_" + DTOptionTable[num4, 0] + "_" + num3.ToString()]) + ";";
        //                num3++;
        //                num6 = 0x30;
        //                goto Label_0005;
        //            }
        //        case 0x1c:
        //            if (num2 < DTItemTable.GetLength(0))
        //            {
        //                num6 = 0x35;
        //            }
        //            else
        //            {
        //                num6 = 0x4a;
        //            }
        //            goto Label_0005;

        //        case 0x21:
        //            goto Label_053C;

        //        case 0x22:
        //            str4 = "";
        //            num6 = 11;
        //            goto Label_0005;

        //        case 0x23:
        //            if (str4.IndexOf('\'') < 0)
        //            {
        //                goto Label_0E86;
        //            }
        //            num6 = 13;
        //            goto Label_0005;

        //        case 0x25:
        //        case 0x3f:
        //            flag = true;
        //            num6 = 30;
        //            goto Label_0005;

        //        case 0x26:
        //            builder.Append("IP,[GUID],SubmitTime,Point,AnswerTime");
        //            num6 = 0x2e;
        //            goto Label_0005;

        //        case 40:
        //        case 0x45:
        //            num6 = 0x36;
        //            goto Label_0005;

        //        case 0x29:
        //            if (str4 != null)
        //            {
        //                goto Label_0ACF;
        //            }
        //            num6 = 0x22;
        //            goto Label_0005;

        //        case 0x2a:
        //            {
        //                builder.Append("F" + str + ",F" + str + "_FileName,");
        //                string str9 = str3;
        //                str3 = str9 + "@F" + str + ",@F" + str + "_FileName,";
        //                num6 = 0x3f;
        //                goto Label_0005;
        //            }
        //        case 0x2c:
        //            switch (num5)
        //            {
        //                case 1:
        //                case 3:
        //                    str4 = Convert.ToString(base.Request.Form["F" + str]);
        //                    num6 = 0x23;
        //                    goto Label_0005;

        //                case 2:
        //                    str4 = Convert.ToString(base.Request.Form["F" + str]);
        //                    num6 = 0x39;
        //                    goto Label_0005;

        //                case 4:
        //                case 5:
        //                case 6:
        //                case 11:
        //                    str4 = Convert.ToString(base.Request.Form["F" + str]);
        //                    num6 = 0x41;
        //                    goto Label_0005;

        //                case 7:
        //                    strArray = this.getItem7Form(str, DTItemTable);
        //                    builder.Append(strArray[0]);
        //                    str3 = str3 + strArray[1];
        //                    num6 = 0x10;
        //                    goto Label_0005;

        //                case 8:
        //                case 9:
        //                case 10:
        //                    str4 = Convert.ToString(base.Request.Form["F" + str]);
        //                    num6 = 0x29;
        //                    goto Label_0005;

        //                case 12:
        //                    num3 = 0;
        //                    num4 = 0;
        //                    num6 = 0x4d;
        //                    goto Label_0005;

        //                case 13:
        //                    num4 = 0;
        //                    num6 = 10;
        //                    goto Label_0005;

        //                case 14:
        //                    goto Label_0954;

        //                case 15:
        //                    strArray = this.getItem15Form(str, DTItemTable);
        //                    builder.Append(strArray[0]);
        //                    str3 = str3 + strArray[1];
        //                    num6 = 50;
        //                    goto Label_0005;

        //                case 0x10:
        //                    strArray = this.getItem16Form(str, DTItemTable, DTOptionTable);
        //                    builder.Append(strArray[0]);
        //                    str3 = str3 + strArray[1];
        //                    num6 = 0x31;
        //                    goto Label_0005;

        //                case 0x11:
        //                    num6 = 0;
        //                    goto Label_0005;

        //                case 0x12:
        //                    strArray = this.getItem18Form(str, DTItemTable, DTOptionTable);
        //                    builder.Append(strArray[0]);
        //                    str3 = str3 + strArray[1];
        //                    num6 = 5;
        //                    goto Label_0005;

        //                case 0x13:
        //                    strArray = this.getItem19Form(str, DTItemTable);
        //                    builder.Append(strArray[0]);
        //                    str3 = str3 + strArray[1];
        //                    num6 = 0x20;
        //                    goto Label_0005;

        //                case 20:
        //                    strArray = this.getItem20Form(str, DTItemTable);
        //                    builder.Append(strArray[0]);
        //                    str3 = str3 + strArray[1];
        //                    num6 = 0x3b;
        //                    goto Label_0005;

        //                case 0x15:
        //                    strArray = this.getItem21Form(str, DTItemTable);
        //                    builder.Append(strArray[0]);
        //                    str3 = str3 + strArray[1];
        //                    num6 = 12;
        //                    goto Label_0005;
        //            }
        //            num6 = 0x16;
        //            goto Label_0005;

        //        case 0x2d:
        //            if (num != 9)
        //            {
        //                goto Label_0954;
        //            }
        //            num6 = 15;
        //            goto Label_0005;

        //        case 0x2e:
        //            if (sPar.IndexOf("|RecordTime:1|") < 0)
        //            {
        //                string str11 = str3;
        //                str3 = str11 + "'" + sSubmitIP + "','" + GUID + "',null," + intPoint.ToString() + "," + intAnswerTime.ToString();
        //                num6 = 0x15;
        //            }
        //            else
        //            {
        //                num6 = 0x40;
        //            }
        //            goto Label_0005;

        //        case 0x2f:
        //            str = DTItemTable[num2, 0];
        //            num = Convert.ToInt16(DTItemTable[num2, 1]);
        //            str4 = "";
        //            num5 = num;
        //            num6 = 0x2c;
        //            goto Label_0005;

        //        case 0x30:
        //            goto Label_096B;

        //        case 0x33:
        //            if (!flag)
        //            {
        //                return command;
        //            }
        //            num6 = 0x42;
        //            goto Label_0005;

        //        case 0x34:
        //            goto Label_043E;

        //        case 0x35:
        //            if (Convert.ToInt32(DTItemTable[num2, 1]) != 0x11)
        //            {
        //                goto Label_043E;
        //            }
        //            num6 = 0x4c;
        //            goto Label_0005;

        //        case 0x36:
        //            if (num != 5)
        //            {
        //                goto Label_0954;
        //            }
        //            num6 = 0x3d;
        //            goto Label_0005;

        //        case 0x37:
        //            if (!(DTOptionTable[num4, 1] == str))
        //            {
        //                goto Label_096B;
        //            }
        //            num6 = 0x19;
        //            goto Label_0005;

        //        case 0x39:
        //            if (!(str4 == ""))
        //            {
        //                builder.Append("F" + str + ",");
        //                str3 = str3 + str4 + ",";
        //                num6 = 0x1b;
        //            }
        //            else
        //            {
        //                num6 = 0x48;
        //            }
        //            goto Label_0005;

        //        case 0x3a:
        //        case 0x4d:
        //            num6 = 70;
        //            goto Label_0005;

        //        case 0x3d:
        //            try
        //            {
        //                str4 = base.Request.Form["f" + str + "_input"].ToString();
        //            }
        //            catch
        //            {
        //                str4 = "";
        //            }
        //            builder.Append("F" + str + "_input,");
        //            str3 = str3 + "'" + str4 + "',";
        //            num6 = 0x24;
        //            goto Label_0005;

        //        case 0x3e:
        //            builder.Append("F" + str + ",");
        //            str3 = str3 + "'" + str4 + "',";
        //            num6 = 0x4e;
        //            goto Label_0005;

        //        case 0x40:
        //            {
        //                string str10 = str3;
        //                str3 = str10 + "'" + sSubmitIP + "','" + GUID + "','" + dNowTime.ToString() + "'," + intPoint.ToString() + "," + intAnswerTime.ToString();
        //                num6 = 0x38;
        //                goto Label_0005;
        //            }
        //        case 0x41:
        //            if (str4 == null)
        //            {
        //                goto Label_053C;
        //            }
        //            num6 = 20;
        //            goto Label_0005;

        //        case 0x42:
        //            files = HttpContext.Current.Request.Files;
        //            str5 = "";
        //            str6 = this.getClientFolder(SID1);
        //            str7 = "";
        //            num2 = 0;
        //            num6 = 4;
        //            goto Label_0005;

        //        case 0x43:
        //            str4 = str4.Substring(0, str4.Length - 1);
        //            num6 = 0x11;
        //            goto Label_0005;

        //        case 0x44:
        //            if (num2 < DTItemTable.GetLength(0))
        //            {
        //                num6 = 1;
        //            }
        //            else
        //            {
        //                num6 = 0x26;
        //            }
        //            goto Label_0005;

        //        case 70:
        //            if (num4 < DTOptionTable.GetLength(0))
        //            {
        //                num6 = 0x37;
        //            }
        //            else
        //            {
        //                num6 = 0x17;
        //            }
        //            goto Label_0005;

        //        case 0x48:
        //            str4 = "0";
        //            num6 = 0x49;
        //            goto Label_0005;

        //        case 0x4a:
        //            return command;

        //        case 0x4b:
        //            goto Label_0E86;

        //        case 0x4c:
        //            file = files["F" + DTItemTable[num2, 0].ToString()];
        //            str7 = DTItemTable[num2, 4].ToString().Split(new char[] { '|' })[1];
        //            str7 = str7.Substring("MaxFileLen".Length);
        //            num6 = 0x13;
        //            goto Label_0005;

        //        case 0x4f:
        //            if (!(str4 == ""))
        //            {
        //                builder.Append("F" + str + ",");
        //                str3 = str3 + str4 + ",";
        //                num6 = 0x45;
        //            }
        //            else
        //            {
        //                num6 = 0x21;
        //            }
        //            goto Label_0005;
        //    }
        //Label_014E:
        //    str = "";
        //    num = 0;
        //    num2 = 0;
        //    num3 = 0;
        //    num4 = 0;
        //    str2 = "";
        //    str3 = "";
        //    str4 = "";
        //    builder = new StringBuilder();
        //    flag = false;
        //    command = new OleDbCommand();
        //    strArray = new string[2];
        //    num2 = 0;
        //    num6 = 0x12;
        //    goto Label_0005;
        //Label_0317:
        //    str5 = this.getFormatFileName(SID1.ToString(), file.FileName.Substring(file.FileName.LastIndexOf(@"\") + 1));
        //    num6 = 2;
        //    goto Label_0005;
        //Label_043E:
        //    num2++;
        //    num6 = 0x1f;
        //    goto Label_0005;
        //Label_053C:
        //    str4 = "0";
        //    num6 = 40;
        //    goto Label_0005;
        //Label_08D4:
        //    builder.Append("F" + str + ",");
        //    str3 = str3 + "'" + str4 + "',";
        //    num6 = 60;
        //    goto Label_0005;
        //Label_0954:
        //    num2++;
        //    num6 = 9;
        //    goto Label_0005;
        //Label_096B:
        //    num4++;
        //    num6 = 0x3a;
        //    goto Label_0005;
        //Label_0ACF:
        //    builder.Append("F" + str + ",");
        //    str3 = str3 + "'" + str4 + "',";
        //    num6 = 0x2d;
        //    goto Label_0005;
        //Label_0E86:
        //    builder.Append("F" + str + ",");
        //    str3 = str3 + "'" + str4 + "',";
        //    num6 = 0x1d;
        //    goto Label_0005;
        //}

        protected bool doCheckIsNeedConfirm(string sCheckStr)
        {
            bool IsNeedConfirm = false;

            if (sCheckStr.IndexOf("|NeedConfirm:1|") >= 0)
            {
                IsNeedConfirm = true;
            }

            return IsNeedConfirm;
        }

        public void AnswerSurvey(string[,] DTItemTable, string[,] DTOptionTable, string SID1, string sPar, string sSubmitIP, string GUID, string dNowTime, int intAnswerTime,string sUploadTarget, int intPoint)
        {

            long UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            bool Anonymity = false;
            int point = 0;
            int totalPoint = 0;
            string TabelName;
            string UserGuid=null; //会员GUID
            int integral = 0; //积分
            string AnswerUserKind = "0";
            string prefix = new Survey_ss_Layer().prefix;
            List<SqlTransEntity> sqlparameters = new List<SqlTransEntity>();

            if (this.Session["UserIDClient"] != null)
            {
                UID = ConvertHelper.ConvertLong(this.Session["UserIDClient"]);
            }
            else
            {
                if (UID == 0)
                {
                    Anonymity = true;
                    AnswerUserKind = ((int)CommonEnum.AnsweruserKind.AnonymityUser).ToString(); //匿名用户
                }
                else
                {
                    AnswerUserKind = ((int)CommonEnum.AnsweruserKind.ManagerUser).ToString(); //后台管理用户
                }
            }

            if (!doCheckIsNeedConfirm(sPar)) //答题判断校验
            {
                if (UID == 0)
                {
                    Anonymity = true;
                    AnswerUserKind = ((int)CommonEnum.AnsweruserKind.AnonymityUser).ToString(); //匿名用户
                }
                else  //添加会员积分
                {
                    if (this.Session["UserIDClient"] != null)
                    {
                        AnswerUserKind = ((int)CommonEnum.AnsweruserKind.HiyuanUser).ToString(); //会员用户

                        Anonymity = false;
                        UserGuid = ConvertHelper.ConvertString(new Survey_ss_Layer().GetUserGUID(UID.ToString()).Rows[0]["id"]);
                        integral = ConvertHelper.ConvertInt(new Survey_ss_Layer().GetSIntegral(SID1).Rows[0]["Point"]);

                        SqlDataReader dr = new Survey_ss_Layer().GetHuiYuan_Point(UserGuid);


                        TabelName = " " + prefix + "HuiYuan_Point ";
                        SqlTransEntity sqlTransEntity2 = new SqlTransEntity();

                        if (!dr.Read())
                        {
                            sqlTransEntity2.SqlCommandText = "Insert into" + TabelName + "(HuiYuanGuid,TotalPoint,RemainPoint,Status) values(@HuiYuanGuid,@integral,@integral,1)";
                        }
                        else
                        {
                            sqlTransEntity2.SqlCommandText = "Update" + TabelName + "set TotalPoint=TotalPoint+@integral,RemainPoint=RemainPoint+@integral where HuiYuanGuid=@HuiYuanGuid";
                        }

                        SqlParameter[] parameters2 = new SqlParameter[2];
                        parameters2[0] = new SqlParameter("@integral", integral);
                        parameters2[1] = new SqlParameter("@HuiYuanGuid", UserGuid);
                        sqlTransEntity2.SqlParameters = parameters2;
                        sqlparameters.Add(sqlTransEntity2);
                    }
                    else
                    {
                        AnswerUserKind = ((int)CommonEnum.AnsweruserKind.ManagerUser).ToString(); //后台管理用户
                    }
                }
            }

            string CreateGUID = Guid.NewGuid().ToString();
            
            for (int i = 0; i < DTItemTable.GetLength(0); i++)
            {
                if (DTItemTable[i, 1] == "1")
                {

                }
                else
                {
                    string value= GetValue(DTItemTable[i, 0], DTItemTable[i, 1],SID1,out point);

                    TabelName = " " + prefix + "AnswerDetail ";
                    SqlTransEntity sqlTransEntity= new SqlTransEntity();
                    sqlTransEntity.SqlCommandText = "insert into " + TabelName + "(SID,IID,Answer,Point,AnswerGUID) values(@SID,@IID,@Answer,@Point,@AnswerGUID)";
                    SqlParameter[] parameter = new SqlParameter[5];
                    parameter[0] = new SqlParameter("@SID", SID1);
                    parameter[1] = new SqlParameter("@IID", DTItemTable[i, 0]);
                    parameter[2] = new SqlParameter("@Answer", value);
                    parameter[3] = new SqlParameter("@Point", point);
                    parameter[4] = new SqlParameter("@AnswerGUID", CreateGUID);
                    sqlTransEntity.SqlParameters = parameter;
                    sqlparameters.Add(sqlTransEntity);

                    totalPoint = totalPoint + point;
                }
            }

            TabelName = " " + prefix + "AnswerInfo ";

            DateTime dtStartTime; //答卷的开始时间
            if (Request.Form["AnswerTime"] != null)
            {
                dtStartTime = ConvertHelper.ConvertDateTime(Request.Form["AnswerTime"]);
            }
            else
            {
                dtStartTime = DateTime.Now.AddSeconds(-20); //如果没有获得到答卷初始时间，则默认添加答卷时间为当前时间钱20秒
            }


            SqlTransEntity sqlTransEntity1 = new SqlTransEntity();
            if (sPar.IndexOf("NeedConfirm:1") > 0)
            {
                sqlTransEntity1.SqlCommandText = "insert into " + TabelName + "(Anonymity,UID,IP,SubmittIime,Point,SID,AnswerGUID,AnswerUserKind,BeginTime) values(@Anonymity,@UID,@IP,@SubmittIime,@Point,@SID,@AnswerGUID,@AnswerUserKind,@BeginTime)";
            }
            else
            { 
                sqlTransEntity1.SqlCommandText = "insert into " + TabelName + "(Anonymity,UID,IP,SubmittIime,Point,SID,AnswerGUID,AnswerUserKind,ApprovalStaus,BeginTime) values(@Anonymity,@UID,@IP,@SubmittIime,@Point,@SID,@AnswerGUID,@AnswerUserKind,@ApprovalStaus,@BeginTime)";               
            }
            SqlParameter[] parameters1 = new SqlParameter[10];
            parameters1[0] = new SqlParameter("@Anonymity", Anonymity);
            parameters1[1] = new SqlParameter("@UID", UID);
            parameters1[2] = new SqlParameter("@IP", sSubmitIP);
            parameters1[3] = new SqlParameter("@SubmittIime", DateTime.Now);
            parameters1[4] = new SqlParameter("@Point", totalPoint);
            parameters1[5] = new SqlParameter("@SID", SID1);
            parameters1[6] = new SqlParameter("@AnswerGUID", CreateGUID);
            parameters1[7] = new SqlParameter("@AnswerUserKind", AnswerUserKind);
            parameters1[8] = new SqlParameter("@ApprovalStaus", 1);
            parameters1[9] = new SqlParameter("@BeginTime", dtStartTime);
            sqlTransEntity1.SqlParameters = parameters1;
            sqlparameters.Add(sqlTransEntity1);

            new Survey_ss_Layer().ExecProcedureByTrans(sqlparameters);
        }

        protected string GetValue(string item,string itemType,string SID,out int point)
        {
            string itemValue="";
            point = 0;
            switch (itemType)
            { 
                case "4":
                    itemValue = base.Request.Form["f" + item];
                    if (itemValue != null && itemValue != "")
                    {
                        string[] arrayOption = itemValue.Split(',');
                        foreach (string str in arrayOption)
                        {
                            DataTable dtGetOptionTablePoint= new Survey_ss_Layer().GetOptionTablePoint(SID, str);
                            if (dtGetOptionTablePoint.Rows.Count == 1)
                            {
                                point = point+ConvertHelper.ConvertInt(dtGetOptionTablePoint.Rows[0]["Point"]);
                            }
                        }
                    }
                    break;

                case "5":
                    itemValue = base.Request.Form["f" + item] + "|" + base.Request.Form["f" + item + "_input"];
                    if (itemValue != null && itemValue != "")
                    {
                        string[] arrayOption = itemValue.Split('|')[0].Split(',');
                        foreach (string str in arrayOption)
                        {
                            DataTable dtGetOptionTablePoint = new Survey_ss_Layer().GetOptionTablePoint(SID, str);
                            if (dtGetOptionTablePoint.Rows.Count == 1)
                            {
                                point = point + ConvertHelper.ConvertInt(dtGetOptionTablePoint.Rows[0]["Point"]);
                            }
                        }
                    }
                    break;

                case "6":
                    itemValue = base.Request.Form["f" + item];
                    if (itemValue != null && itemValue != "")
                    {
                        string[] arrayOption = itemValue.Split(',');
                        foreach (string str in arrayOption)
                        {
                            DataTable dtGetOptionTablePoint = new Survey_ss_Layer().GetOptionTablePoint(SID, str);
                            if (dtGetOptionTablePoint.Rows.Count == 1)
                            {
                                point = point + ConvertHelper.ConvertInt(dtGetOptionTablePoint.Rows[0]["Point"]);
                            }
                        }
                    }
                    break;

                case "8":
                    itemValue = base.Request.Form["f" + item];
                    if (itemValue != null && itemValue != "")
                    {
                        string[] arrayOption = itemValue.Split(',');
                        foreach (string str in arrayOption)
                        {
                            DataTable dtGetOptionTablePoint = new Survey_ss_Layer().GetOptionTablePoint(SID, str);
                            if (dtGetOptionTablePoint.Rows.Count == 1)
                            {
                                point = point + ConvertHelper.ConvertInt(dtGetOptionTablePoint.Rows[0]["Point"]);
                            }
                        }
                    }
                    break;

                case "9":
                    itemValue = base.Request.Form["f" + item] + "|" + base.Request.Form["f" + item+"_input"];
                    if (itemValue != null && itemValue != "")
                    {
                        string[] arrayOption = itemValue.Split('|')[0].Split(',');
                        foreach (string str in arrayOption)
                        {
                            DataTable dtGetOptionTablePoint = new Survey_ss_Layer().GetOptionTablePoint(SID, str);
                            if (dtGetOptionTablePoint.Rows.Count == 1)
                            {
                                point = point + ConvertHelper.ConvertInt(dtGetOptionTablePoint.Rows[0]["Point"]);
                            }
                        }
                    }

                    break;

                case "10":
                    itemValue = base.Request.Form["f" + item];
                    if (itemValue != null && itemValue != "")
                    {
                        string[] arrayOption = itemValue.Split(',');
                        foreach (string str in arrayOption)
                        {
                            DataTable dtGetOptionTablePoint = new Survey_ss_Layer().GetOptionTablePoint(SID, str);
                            if (dtGetOptionTablePoint.Rows.Count == 1)
                            {
                                point = point + ConvertHelper.ConvertInt(dtGetOptionTablePoint.Rows[0]["Point"]);
                            }
                        }
                    }
                    break;
            }

            return itemValue;
        }

        public static string GetSQLDateTimeText(DateTime date)
        {
            if (date.ToString("yyyy-MM-dd") == "0001-01-01")
            {
                return "null";
            }
            if ((1 != 0) && (0 != 0))
            {
            }
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        protected string[] getSurveyExpand(long SID)
        {
            string[] strArray;
        Label_0027:
            strArray = new string[5];
            //objComm.CommandText = "SELECT ExpandType,ExpandContent FROM SurveyExpand WHERE SID=" + SID.ToString() + " AND (ExpandType IN (2,3,4))";
            SqlDataReader dr = new Survey_ss_Layer().GetSurveyExpand(SID.ToString());
            int num = 4;
        Label_0002:
            switch (num)
            {
                case 0:
                    if (!(strArray[3] == ""))
                    {
                        break;
                    }
                    num = 1;
                    goto Label_0002;

                case 1:
                    strArray[3] = this.getItemStr(dr, SID);
                    strArray[4] = this.getOptionStr(dr, SID);
                    num = 6;
                    goto Label_0002;

                case 2:
                    dr.Dispose();
                    num = 0;
                    goto Label_0002;

                case 3:
                case 4:
                    num = 5;
                    goto Label_0002;

                case 5:
                    if (dr.Read())
                    {
                        strArray[Convert.ToInt32(dr["ExpandType"])] = dr["ExpandContent"].ToString();
                        num = 3;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_0002;

                case 6:
                    break;

                default:
                    goto Label_0027;
            }
            return strArray;
        }

        protected int getTextParentType(string[,] dt, int intCurrIID)
        {
            int num;
        Label_0043:
            num = 0;
            int num2 = 0;
            num2 = 0;
            int num3 = 7;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (!(dt[num2, 0].ToString() == num.ToString()))
                    {
                        num2++;
                        num3 = 5;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0002;

                case 1:
                    num = Convert.ToInt32(dt[num2, 1]);
                    num3 = 4;
                    goto Label_0002;

                case 2:
                    num = Convert.ToInt32(dt[num2, 3]);
                    num3 = 12;
                    goto Label_0002;

                case 3:
                    if (num2 < dt.Length)
                    {
                        num3 = 0;
                    }
                    else
                    {
                        num3 = 9;
                    }
                    goto Label_0002;

                case 4:
                    return num;

                case 5:
                case 11:
                    num3 = 3;
                    goto Label_0002;

                case 6:
                    if (num2 < dt.Length)
                    {
                        num3 = 10;
                    }
                    else
                    {
                        num3 = 13;
                    }
                    goto Label_0002;

                case 7:
                case 8:
                    num3 = 6;
                    goto Label_0002;

                case 9:
                    return num;

                case 10:
                    if (!(dt[num2, 0] == intCurrIID.ToString()))
                    {
                        num2++;
                        num3 = 8;
                    }
                    else
                    {
                        num3 = 2;
                    }
                    goto Label_0002;

                case 12:
                case 13:
                    num2 = 0;
                    num3 = 11;
                    goto Label_0002;
            }
            goto Label_0043;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            string str2;
            string str3;
            string userHostAddress;
            string str5;
            int num;
            string[] strArray;
            string[,] strArray2;
            string[,] strArray3;
            DateTime now;
            int num2;
            int num3;
            int num4;
            DataSet set = null; //赋初值
            int num6 = 0; //赋初值
            int num7;
            goto Label_009E;
        Label_0005:
            switch (num7)
            {
                case 0:
                case 0x22:
                    //command = this.getSQL(strArray2, strArray3, str, str2, userHostAddress, num.ToString(), GetSQLDateTimeText(now), num2, connection, str5, num4);
                    this.AnswerSurvey(strArray2, strArray3, str, str2, userHostAddress, num.ToString(), GetSQLDateTimeText(now), num2, str5, num4);
                    num7 = 0x17;
                    goto Label_0005;

                case 1:
                    base.Response.Redirect("Error.aspx?ec=4&SID=" + str);
                    base.Response.End();
                    num7 = 0x13;
                    goto Label_0005;

                case 2:
                    switch (num6)
                    {
                        case 1:
                            base.Response.Redirect("Template/lastpage/" + set.Tables["SurveyTable"].Rows[0]["PageFileName"].ToString());
                            return;

                        case 2:
                            base.Response.Redirect(set.Tables["SurveyTable"].Rows[0]["PageFileName"].ToString() + "?SID=" + str.ToString());
                            return;

                        case 3:
                            base.Response.Redirect("CAnswerPoint.aspx?SID=" + str.ToString());
                            return;

                        case 4:
                            base.Response.Redirect(set.Tables["SurveyTable"].Rows[0]["ToURL"].ToString());
                            return;

                        case 5:
                            base.Response.Redirect("userdata/u" + set.Tables["SurveyTable"].Rows[0]["UID"].ToString() + "/s" + str.ToString() + ".aspx");
                            return;
                    }
                    num7 = 20;
                    goto Label_0005;

                case 3:
                    if (set.Tables["SurveyTable"].Rows.Count != 0)
                    {
                        goto Label_0309;
                    }
                    num7 = 0x21;
                    goto Label_0005;

                case 4:
                    num = 0;
                    num7 = 0x11;
                    goto Label_0005;

                case 5:
                    if (str2.IndexOf("|RecordIP:1|") >= 0)
                    {
                        goto Label_0501;
                    }
                    num7 = 0x10;
                    goto Label_0005;

                case 6:
                    base.Response.Redirect("Error.aspx?ec=7&SID=" + str);
                    num7 = 0x19;
                    goto Label_0005;

                case 7:
                    base.Response.Redirect("Error.aspx?ec=17&SID=" + str);
                    base.Response.End();
                    num7 = 10;
                    goto Label_0005;

                case 8:
                    base.Response.Write("非法访问本页");
                    base.Response.End();
                    num7 = 0x15;
                    goto Label_0005;

                case 9:
                    if (str2.IndexOf("|AnswerPSW:1|") < 0)
                    {
                        goto Label_055B;
                    }
                    num7 = 0x1d;
                    goto Label_0005;

                case 10:
                    goto Label_0465;

                case 11:
                    if (!(set.Tables["SurveyTable"].Rows[0]["Active"].ToString() == "False"))
                    {
                        goto Label_060B;
                    }
                    num7 = 1;
                    goto Label_0005;

                case 12:
                    if (str2.IndexOf("|TPaper:1|") >= 0)
                    {
                        num4 = this.SurveyScoring(Convert.ToInt64(str), strArray2);
                        num7 = 0x22;
                    }
                    else
                    {
                        num7 = 0x1f;
                    }
                    goto Label_0005;

                case 13:
                    if (!(set.Tables["SurveyTable"].Rows[0]["State"].ToString() == "False"))
                    {
                        goto Label_0465;
                    }
                    num7 = 7;
                    goto Label_0005;

                case 14:
                    if (str2.IndexOf("|Email:1|") < 0)
                    {
                        num6 = Convert.ToInt16(set.Tables["SurveyTable"].Rows[0]["PageType"]);
                        num7 = 2;
                    }
                    else
                    {
                        num7 = 0x1c;
                    }
                    goto Label_0005;

                case 15:
                    if (num3 != 0)
                    {
                        goto Label_055B;
                    }
                    num7 = 0x16;
                    goto Label_0005;

                case 0x10:
                    userHostAddress = "";
                    num7 = 0x1b;
                    goto Label_0005;

                case 0x11:
                    goto Label_06D7;

                case 0x12:
                    goto Label_0309;

                case 0x13:
                    goto Label_060B;

                case 20:
                    return;

                case 0x15:
                    goto Label_0738;

                case 0x16:
                    base.Response.Redirect("Error.aspx?ec=11&SID=" + str);
                    num7 = 0x23;
                    goto Label_0005;

                case 0x17:
                    try
                    {
                        //num3 = command.ExecuteNonQuery();
                        //command.CommandText = string.Concat(new object[] { " UPDATE SurveyTable SET LastUpdate='", DateTime.Now, "',AnswerAmount=AnswerAmount+1 WHERE SID=", str });
                        new Survey_ss_Layer().UpdateSurveyTable(DateTime.Now, str);
                    }
                    catch (Exception)
                    {
                        base.Response.Write("SQL Error!");
                        base.Response.End();
                    }
                    num7 = 14;
                    goto Label_0005;

                case 0x18:
                    if (str3 != null)
                    {
                        goto Label_0179;
                    }
                    num7 = 6;
                    goto Label_0005;

                case 0x19:
                    goto Label_0179;

                case 0x1a:
                    if (str2.IndexOf("|MemberLogin:1|") < 0)
                    {
                        goto Label_06D7;
                    }
                    num7 = 4;
                    goto Label_0005;

                case 0x1b:
                    goto Label_0501;

                case 0x1c:
                    base.Response.Redirect("SendAnswerEmail.aspx?SID=" + str.ToString());
                    return;

                case 0x1d:
                    str3 = Convert.ToString(base.Request.Form["AnswerPSW"]);
                    num7 = 0x18;
                    goto Label_0005;

                case 30:
                    try
                    {
                        strArray2 = this.StrToArray(strArray[3], 5);
                    }
                    catch (Exception)
                    {
                        base.Response.Write("arrItem Error");
                        base.Response.End();
                    }
                    try
                    {
                        strArray3 = this.StrToArray(strArray[4], 3);
                    }
                    catch
                    {
                        base.Response.Write("arrOption Error");
                        base.Response.End();
                    }
                    num7 = 3;
                    goto Label_0005;

                case 0x1f:
                    num4 = Convert.ToInt32(base.Request.Form["Point"]);
                    num7 = 0;
                    goto Label_0005;

                case 0x20:
                    if (!(base.Request.HttpMethod.ToString() != "POST"))
                    {
                        goto Label_0738;
                    }
                    num7 = 8;
                    goto Label_0005;

                case 0x21:
                    base.Response.Redirect("Error.aspx?EC=17&SID=" + str);
                    base.Response.End();
                    num7 = 0x12;
                    goto Label_0005;

                case 0x23:
                    goto Label_055B;
            }
        Label_009E:
            str = "0";
            str2 = "";
            str3 = "";
            userHostAddress = this.Page.Request.UserHostAddress;
            str5 = new Survey_ss_Layer().GetUploadTargetConfig();
            num = 0;
            strArray = new string[5];
            strArray2 = null;
            strArray3 = null;
            now = DateTime.Now;
            num2 = 0;
            num3 = 0;
            num4 = 0;
            num7 = 0x20;
            goto Label_0005;
        Label_0179: ;
            //command.CommandText = "UPDATE AnswerPSW SET Used=1 WHERE SID=" + str.ToString() + " AND Used=0 AND PSW='" + str3 + "'";
            //num3 = command.ExecuteNonQuery();
            num7 = 15;
            goto Label_0005;
        Label_0309:
            str2 = "|" + set.Tables["SurveyTable"].Rows[0]["Par"].ToString() + "|";
            num7 = 13;
            goto Label_0005;
        Label_0465:
            num7 = 11;
            goto Label_0005;
        Label_0501:
            num7 = 0x1a;
            goto Label_0005;
        Label_055B:
            this.checkRepeatAnswerItem(strArray[2], Convert.ToInt32(str));
            num7 = 12;
            goto Label_0005;
        Label_060B:
            this.checkAnswerAmount( Convert.ToInt32(set.Tables["SurveyTable"].Rows[0]["MaxAnswerAmount"]), Convert.ToInt32(str));
            this.doCheckCode(str2, str);
            this.doCookies(str2, Convert.ToInt64(str));
            this.doCheckIP(str2,Convert.ToInt64(str), userHostAddress);
            num7 = 5;
            goto Label_0005;
        Label_06D7:
            num7 = 9;
            goto Label_0005;
        Label_0738:
            str = Convert.ToString(base.Request.Form["SID"]);
            DateTime time2 = Convert.ToDateTime(base.Request.Form["AnswerTime"]);
            num2 = Convert.ToInt32(now.Subtract(time2).TotalSeconds);

            //command.CommandText = "SELECT TOP 1 UID,Active,State,Par,MaxAnswerAmount,PageFileName,PageType,ToURL,Point,AdminSetAnswerAmount,AdminSetAnsweredAmount FROM SurveyTable S LEFT JOIN PageStyle P ON S.EndPage=P.P_ID WHERE  SID=" + str + " AND State=1";
            set = new DataSet();
            //adapter.Fill(set, "SurveyTable");
            DataTable SurveyTable = new Survey_ss_Layer().GetSurveyTable(str);
            SurveyTable.TableName = "SurveyTable";
            set.Tables.Add(SurveyTable);

            strArray = this.getSurveyExpand(Convert.ToInt64(str));
            num7 = 30;
            goto Label_0005;
        }

        //protected bool saveGUAnswer(OleDbConnection objConn, int GUID, string SID1, int intSPoint)
        //{
        //    OleDbCommand objComm = new OleDbCommand("", objConn);
        //    if (!this.checkGUPoint(objComm, GUID, intSPoint))
        //    {
        //        return false;
        //    }
        //    if ((1 != 0) && (0 != 0))
        //    {
        //    }
        //    objComm.CommandText = string.Concat(new object[] { "INSERT INTO AnsweredSurvey(SID,[GUID],SPoint) VALUES(", SID1, ",", GUID.ToString(), ",", intSPoint.ToString(), ") UPDATE  GUTable SET SPoint=SPoint+", intSPoint.ToString(), ",ComplateSurveyAmount=ComplateSurveyAmount+1 WHERE GUID=", GUID });
        //    objComm.ExecuteNonQuery();
        //    objComm.CommandText = " UPDATE SurveyTable SET AdminSetAnsweredAmount=AdminSetAnsweredAmount+1 WHERE SID=" + SID1.ToString();
        //    objComm.ExecuteNonQuery();
        //    objComm.Dispose();
        //    return true;
        //}

        protected string[,] StrToArray(string sInput, int intFieldAmount)
        {
            string[] strArray = null; //赋初值
            string[] strArray2 = null; //赋初值
            int num = 0; //赋初值
            string[,] strArray3 = null; //赋初值
            int num2 = 0; //赋初值
            int num3 = 6;
        Label_000D:
            switch (num3)
            {
                case 0:
                case 10:
                    num3 = 7;
                    goto Label_000D;

                case 1:
                    num2++;
                    num3 = 10;
                    goto Label_000D;

                case 2:
                    return strArray3;

                case 3:
                    sInput = sInput.Substring(0, sInput.Length - 1);
                    num3 = 4;
                    goto Label_000D;

                case 4:
                    strArray = sInput.Split(new char[] { '/' });
                    strArray2 = new string[intFieldAmount];
                    num = 0;
                    strArray3 = new string[strArray.Length, intFieldAmount];
                    num2 = 0;
                    num3 = 0;
                    goto Label_000D;

                case 5:
                    if (num < intFieldAmount)
                    {
                        strArray3[num2, num] = strArray2[num];
                        num++;
                        num3 = 9;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_000D;

                case 7:
                    if (num2 < strArray.Length)
                    {
                        strArray2 = strArray[num2].Split(new char[] { '-' });
                        num = 0;
                        num3 = 8;
                    }
                    else
                    {
                        num3 = 2;
                    }
                    goto Label_000D;

                case 8:
                case 9:
                    num3 = 5;
                    goto Label_000D;
            }
            if (!(sInput != ""))
            {
                return null;
            }
            num3 = 3;
            goto Label_000D;
        }

        protected int SurveyScoring(long SID, string[,] arrItem)
        {
            string str;
            int num;
            SqlDataReader reader;
            Hashtable hashtable = null; //赋初值
            int num2 = 0; //赋初值
            string[] strArray = null; //赋初值
            string[] strArray2 = null; //赋初值
            string str2 = null; //赋初值
            string str3 = null; //赋初值
            int num3 = 0; //赋初值
            int num4 = 0; //赋初值
            string[] strArray3 = null; //赋初值
            string[] strArray4 = null; //赋初值
            string str4 = null; //赋初值
            int num5 = 0; //赋初值
            int num6;
            goto Label_00FE;
        Label_0005:
            switch (num6)
            {
                case 0:
                case 2:
                case 4:
                case 0x18:
                case 0x1a:
                case 0x20:
                case 0x21:
                case 0x2a:
                case 0x2e:
                case 50:
                case 0x36:
                    goto Label_08CB;

                case 1:
                    if (!(arrItem[num4, 3] == strArray2[0]))
                    {
                        goto Label_0861;
                    }
                    num6 = 0x24;
                    goto Label_0005;

                case 3:
                case 0x30:
                    num6 = 0x2f;
                    goto Label_0005;

                case 5:
                    return num;

                case 6:
                    strArray3 = strArray2[1].Split(new char[] { ';' });
                    strArray4 = strArray2[2].Split(new char[] { ';' });
                    num4 = 0;
                    num6 = 0x11;
                    goto Label_0005;

                case 7:
                    if (!(str == ""))
                    {
                        hashtable = new Hashtable();
                        num2 = 0;
                        num6 = 0x1d;
                    }
                    else
                    {
                        num6 = 5;
                    }
                    goto Label_0005;

                case 8:
                    num6 = 0x36;
                    goto Label_0005;

                case 9:
                    if (Convert.ToString(";" + strArray2[1] + ";").IndexOf(";" + str2 + ";") < 0)
                    {
                        num += Convert.ToInt32(strArray2[3]);
                        num6 = 0x20;
                    }
                    else
                    {
                        num6 = 6;
                    }
                    goto Label_0005;

                case 10:
                    str = reader[0].ToString();
                    num6 = 0x3b;
                    goto Label_0005;

                case 11:
                    num += Convert.ToInt32(strArray4[num4]);
                    num6 = 0x21;
                    goto Label_0005;

                case 12:
                    {
                        Dictionary<string, int> dictionary1 = new Dictionary<string, int>(10);
                        dictionary1.Add("1", 0);
                        dictionary1.Add("2", 1);
                        dictionary1.Add("4", 2);
                        dictionary1.Add("5", 3);
                        dictionary1.Add("6", 4);
                        dictionary1.Add("11", 5);
                        dictionary1.Add("8", 6);
                        dictionary1.Add("9", 7);
                        dictionary1.Add("10", 8);
                        dictionary1.Add("21", 9);
                        ModelSurvey_ss.dictionary2 = dictionary1;
                        num6 = 0x31;
                        goto Label_0005;
                    }
                case 13:
                    strArray3 = strArray2[1].Split(new char[] { ';' });
                    strArray4 = strArray2[2].Split(new char[] { ';' });
                    num4 = 0;
                    num6 = 0x22;
                    goto Label_0005;

                case 14:
                    goto Label_0861;

                case 15:
                case 0x22:
                    num6 = 0x16;
                    goto Label_0005;

                case 0x10:
                case 0x12:
                    num6 = 0x3a;
                    goto Label_0005;

                case 0x11:
                case 0x1c:
                    num6 = 0x26;
                    goto Label_0005;

                case 0x13:
                    if (!ModelSurvey_ss.dictionary2.TryGetValue(str4, out num5))
                    {
                        goto Label_08CB;
                    }
                    num6 = 0x2c;
                    goto Label_0005;

                case 20:
                    num += Convert.ToInt32(strArray2[3]);
                    num6 = 0;
                    goto Label_0005;

                case 0x15:
                    {
                        strArray = Regex.Split(str, "\r\n", RegexOptions.IgnoreCase);
                        strArray2 = new string[4];
                        int length = strArray.Length;
                        str2 = "";
                        str3 = "";
                        num3 = 0;
                        num6 = 0x30;
                        goto Label_0005;
                    }
                case 0x16:
                    if (num4 < (strArray3.Length - 1))
                    {
                        num6 = 30;
                    }
                    else
                    {
                        num6 = 8;
                    }
                    goto Label_0005;

                case 0x17:
                case 0x1d:
                    num6 = 0x38;
                    goto Label_0005;

                case 0x19:
                    num6 = 0x37;
                    goto Label_0005;

                case 0x1b:
                    switch (num5)
                    {
                        case 0:
                        case 1:
                            str2 = base.Request.Form["F" + strArray2[0]];
                            base.Response.Write(str2 + ":" + strArray2[1] + "<HR>");
                            num6 = 0x25;
                            goto Label_0005;

                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            str2 = base.Request.Form["F" + strArray2[0]];
                            num6 = 9;
                            goto Label_0005;

                        case 6:
                        case 7:
                        case 8:
                            str2 = base.Request.Form["F" + strArray2[0]];
                            num6 = 0x2b;
                            goto Label_0005;

                        case 9:
                            str3 = "";
                            num4 = 0;
                            num6 = 0x10;
                            goto Label_0005;
                    }
                    num6 = 0x27;
                    goto Label_0005;

                case 30:
                    if (!(strArray3[num4] == (str2 + ",")))
                    {
                        num4++;
                        num6 = 15;
                    }
                    else
                    {
                        num6 = 0x23;
                    }
                    goto Label_0005;

                case 0x1f:
                    str4 = arrItem[Convert.ToInt32(hashtable[strArray2[0]]), 1];
                    if (str4 == null)
                    {
                        goto Label_08CB;
                    }
                    num6 = 0x19;
                    goto Label_0005;

                case 0x23:
                    num += Convert.ToInt32(strArray4[num4]);
                    num6 = 0x2a;
                    goto Label_0005;

                case 0x24:
                    str3 = str3 + base.Request.Form["F" + arrItem[num4, 0]] + ";";
                    num6 = 14;
                    goto Label_0005;

                case 0x25:
                    if (!(str2 != strArray2[1]))
                    {
                        num += Convert.ToInt32(strArray2[2]);
                        num6 = 0x2e;
                    }
                    else
                    {
                        num6 = 20;
                    }
                    goto Label_0005;

                case 0x26:
                    if (num4 < (strArray3.Length - 1))
                    {
                        num6 = 40;
                    }
                    else
                    {
                        num6 = 0x39;
                    }
                    goto Label_0005;

                case 0x27:
                    num6 = 0x18;
                    goto Label_0005;

                case 40:
                    if (!(strArray3[num4] == str2))
                    {
                        num4++;
                        num6 = 0x1c;
                    }
                    else
                    {
                        num6 = 11;
                    }
                    goto Label_0005;

                case 0x29:
                    num6 = 0x35;
                    goto Label_0005;

                case 0x2b:
                    if (Convert.ToString(";" + strArray2[1] + ";").IndexOf(";" + str2 + ",;") < 0)
                    {
                        num += Convert.ToInt32(strArray2[3]);
                        num6 = 4;
                    }
                    else
                    {
                        num6 = 13;
                    }
                    goto Label_0005;

                case 0x2c:
                    num6 = 0x1b;
                    goto Label_0005;

                case 0x2d:
                    num += Convert.ToInt32(strArray2[2]);
                    num6 = 0x1a;
                    goto Label_0005;

                case 0x2f:
                    if (num3 < (strArray.Length - 1))
                    {
                        strArray2 = strArray[num3].Split(new char[] { '|' });
                        num4 = 0;
                        strArray3 = new string[1];
                        strArray4 = new string[1];
                        num6 = 0x1f;
                    }
                    else
                    {
                        num6 = 0x33;
                    }
                    goto Label_0005;

                case 0x31:
                    goto Label_0294;

                case 0x33:
                    return num;

                case 0x34:
                    if (!reader.Read())
                    {
                        goto Label_07DC;
                    }
                    num6 = 10;
                    goto Label_0005;

                case 0x35:
                    if (!(strArray2[1] == str3))
                    {
                        num += Convert.ToInt32(strArray2[3]);
                        num6 = 2;
                    }
                    else
                    {
                        num6 = 0x2d;
                    }
                    goto Label_0005;

                case 0x37:
                    if (ModelSurvey_ss.dictionary2 != null)
                    {
                        goto Label_0294;
                    }
                    num6 = 12;
                    goto Label_0005;

                case 0x38:
                    if (num2 < arrItem.GetLength(0))
                    {
                        hashtable.Add(arrItem[num2, 0], num2);
                        num2++;
                        num6 = 0x17;
                    }
                    else
                    {
                        num6 = 0x15;
                    }
                    goto Label_0005;

                case 0x39:
                    num6 = 50;
                    goto Label_0005;

                case 0x3a:
                    if (num4 < arrItem.GetLength(0))
                    {
                        num6 = 1;
                    }
                    else
                    {
                        num6 = 0x29;
                    }
                    goto Label_0005;

                case 0x3b:
                    goto Label_07DC;
            }
        Label_00FE:
            str = "";
            num = 0;
            //reader = new OleDbCommand("SELECT TOP 1 ExpandContent FROM SurveyExpand WHERE SID=" + SID.ToString() + " AND ExpandType=6", objConn).ExecuteReader();
            reader = new Survey_ss_Layer().GetSurveyExpand1(SID.ToString());
            num6 = 0x34;
            goto Label_0005;
        Label_0294:
            num6 = 0x13;
            goto Label_0005;
        Label_07DC:
            reader.Close();
            num6 = 7;
            goto Label_0005;
        Label_0861:
            num4++;
            num6 = 0x12;
            goto Label_0005;
        Label_08CB:
            num3++;
            num6 = 3;
            goto Label_0005;
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