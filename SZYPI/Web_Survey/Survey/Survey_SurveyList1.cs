using LoginClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Management;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SurveyList1 : Page, IRequiresSessionState
    {
        public string sClientJs = "var UID=0;\nvar arrSurvey = new Array();\nvar SID=0;\nvar intCurrPageNo=0;\nvar intPageSize=0;\nvar intRecordAmount=0;\n";

        public string GetCpuID()
        {
            try
            {
                ManagementObjectCollection instances = new ManagementClass("Win32_Processor").GetInstances();
                string str = null;
                ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator();
                try
                {
                    int num = 3;
                Label_002B:
                    switch (num)
                    {
                        case 0:
                            if (enumerator.MoveNext())
                            {
                                break;
                            }
                            num = 1;
                            goto Label_002B;

                        case 1:
                        case 4:
                            num = 2;
                            goto Label_002B;

                        case 2:
                            goto Label_00F5;

                        default:
                            num = 0;
                            goto Label_002B;
                    }
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    str = current.Properties["ProcessorId"].Value.ToString();
                    num = 4;
                    goto Label_002B;
                }
                finally
                {
                    int num2 = 2;
                Label_00BA:
                    switch (num2)
                    {
                        case 0:
                            enumerator.Dispose();
                            num2 = 1;
                            goto Label_00BA;

                        case 1:
                            break;

                        default:
                            if (enumerator != null)
                            {
                                num2 = 0;
                                goto Label_00BA;
                            }
                            break;
                    }
                }
            Label_00F5:
                return str;
            }
            catch
            {
                return "";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
            long num2;
            int num3;
            int num4;
            int num5;
            int num6;
            string str;
            string str2;
            DataSet set;
            int num7;
            goto Label_0057;
        Label_0002:
            switch (num7)
            {
                case 0:
                    num4 = 1;
                    num7 = 3;
                    goto Label_0002;

                case 1:
                    goto Label_01DD;

                case 2:
                    goto Label_037C;

                case 3:
                    goto Label_029F;

                case 4:
                    if (num3 < set.Tables["SurveyTable"].Rows.Count)
                    {
                        this.sClientJs = this.sClientJs + "arrSurvey[" + num3.ToString() + "] = new Array();\n";
                        string str4 = this.sClientJs;
                        this.sClientJs = str4 + "arrSurvey[" + num3.ToString() + "]=[" + set.Tables["SurveyTable"].Rows[num3]["SID"].ToString() + ",'" + set.Tables["SurveyTable"].Rows[num3]["SurveyName"].ToString() + "','" + set.Tables["SurveyTable"].Rows[num3]["State"].ToString() + "','" + set.Tables["SurveyTable"].Rows[num3]["Active"].ToString() + "'," + set.Tables["SurveyTable"].Rows[num3]["AnswerAmount"].ToString() + "," + set.Tables["SurveyTable"].Rows[num3]["MaxAnswerAmount"].ToString() + ",'" + set.Tables["SurveyTable"].Rows[num3]["LastUpdate"].ToString() + "','" + Convert.ToDateTime(set.Tables["SurveyTable"].Rows[num3]["CreateDate"]).ToShortDateString() + "']\n";
                        num3++;
                        num7 = 0x12;
                    }
                    else
                    {
                        num7 = 8;
                    }
                    goto Label_0002;

                case 5:
                    if (num4 > 0)
                    {
                        goto Label_029F;
                    }
                    num7 = 0;
                    goto Label_0002;

                case 6:
                    num4 = num6;
                    num7 = 1;
                    goto Label_0002;

                case 7:
                    if (!(str == "a"))
                    {
                        goto Label_037C;
                    }
                    num7 = 13;
                    goto Label_0002;

                case 8:
                    return;

                case 9:
                    if (!(set.Tables["COUNT"].ToString() == "0"))
                    {
                        num6 = (int)Math.Abs(Math.Floor((double)(((double)-Convert.ToInt32(set.Tables["COUNT"].Rows[0][0])) / ((double)num5))));
                        num7 = 15;
                    }
                    else
                    {
                        num7 = 0x11;
                    }
                    goto Label_0002;

                case 10:
                    try
                    {
                        goto Label_0801;
                    Label_07E8:
                        switch (num7)
                        {
                            case 0:
                                if (!(str2 != ""))
                                {
                                    goto Label_085B;
                                }
                                num7 = 1;
                                goto Label_07E8;

                            case 1:
                                this.reName(str2, num2, num);
                                num7 = 3;
                                goto Label_07E8;

                            case 2:
                                goto Label_0267;

                            case 3:
                                goto Label_085B;
                        }
                    Label_0801:
                        str2 = base.Request.QueryString["NewName"].ToString();
                        num7 = 0;
                        goto Label_07E8;
                    Label_085B:
                        num7 = 2;
                        goto Label_07E8;
                    }
                    catch
                    {
                        goto Label_0267;
                    }
                    goto Label_0871;

                case 11:
                case 0x12:
                    num7 = 4;
                    goto Label_0002;

                case 12:
                    goto Label_07B4;

                case 13:
                    goto Label_0871;

                case 14:
                    //command.CommandText = "DELETE FROM SurveyTable WHERE SID=" + num2.ToString() + " AND UID=" + num.ToString();
                    new Survey_SurveyList1_Layer().DelSurveyTable(num2.ToString(), num.ToString());
                    //command.CommandText = " DELETE FROM ItemTable WHERE SID=" + num2.ToString() + " AND UID=" + num.ToString();
                    new Survey_SurveyList1_Layer().DelItemTable(num2.ToString(), num.ToString());
                    //command.CommandText = " DELETE FROM PageTable WHERE SID=" + num2.ToString() + " AND UID=" + num.ToString();
                    new Survey_SurveyList1_Layer().DelPageTable(num2.ToString(), num.ToString());
                    //command.CommandText = " DELETE FROM OptionTable WHERE SID=" + num2.ToString() + " AND UID=" + num.ToString();
                    new Survey_SurveyList1_Layer().DelOptionTable(num2.ToString(), num.ToString());
                    //command.CommandText = " DELETE FROM HeadFoot WHERE SID=" + num2.ToString() + " AND UID=" + num.ToString();
                    new Survey_SurveyList1_Layer().DelHeadFoot(num2.ToString(), num.ToString());
                    //command.CommandText = " DELETE FROM SurveyExpand WHERE SID=" + num2.ToString() + " AND UID=" + num.ToString();
                    new Survey_SurveyList1_Layer().DelSurveyExpand(num2.ToString(), num.ToString());
                    num7 = 12;
                    goto Label_0002;

                case 15:
                    if (num4 <= num6)
                    {
                        goto Label_01DD;
                    }
                    num7 = 6;
                    goto Label_0002;

                case 0x10:
                    if (!(str == "d"))
                    {
                        goto Label_07B4;
                    }
                    num7 = 14;
                    goto Label_0002;

                case 0x11:
                    return;
            }
        Label_0057:
            num = 0;
            num2 = 0;
            num = ConvertHelper.ConvertLong(this.Session["UserID"]);
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "surveylist.aspx", 0, "没有权限", "Limits.aspx");
            num3 = 0;
            num4 = 1;
            num5 = 10;
            num6 = 1;
            str = Convert.ToString(base.Request.QueryString["A"]);
            num2 = Convert.ToInt64(base.Request.QueryString["SID"]);
            num4 = Convert.ToInt32(base.Request.QueryString["PageNo"]);
            str2 = "";
            string sClientJs = this.sClientJs;
            this.sClientJs = sClientJs + "intPageSize=" + num5.ToString() + ";\nSID = " + num2.ToString() + ";\nUID=" + num.ToString() + ";\nintCurrPageNo=" + num4.ToString() + ";\n";
            set = new DataSet();


            num7 = 10;
            goto Label_0002;
        Label_01DD:
            num7 = 5;
            goto Label_0002;
        Label_0267:
            num7 = 0x10;
            goto Label_0002;
        Label_029F: ;
            //command.CommandText = "SELECT TOP " + Convert.ToString((int)(num4 * num5)).ToString() + " * FROM SurveyTable WHERE UID=" + num.ToString() + " ORDER BY SID DESC";
            //adapter.Fill(set, (num4 - 1) * num5, num5, "SurveyTable");
            DataTable SurveyTable = new DataTable();
            SurveyTable = new Survey_SurveyList1_Layer().GetSurveyTable((int)(num4 * num5), num.ToString());
            SurveyTable.TableName = "SurveyTable";
            set.Tables.Add(SurveyTable);

            num3 = 0;
            num7 = 11;
            goto Label_0002;
        Label_037C:
            //command.CommandText = "SELECT COUNT(1) FROM SurveyTable WHERE UID=" + num.ToString();
            //adapter.Fill(set, "COUNT");
            DataTable COUNT = new DataTable();
            COUNT = new Survey_SurveyList1_Layer().GetCount(num.ToString());
            COUNT.TableName = "COUNT";
            set.Tables.Add(COUNT);

            this.sClientJs = this.sClientJs + "intRecordAmount=" + set.Tables["COUNT"].Rows[0][0].ToString() + ";\n";
            num7 = 9;
            goto Label_0002;
        Label_07B4:
            num7 = 7;
            goto Label_0002;
        Label_0871:
            //command.CommandText = "UPDATE SurveyTable SET Active=ABS(Active-1) WHERE SID=" + num2.ToString() + " AND UID=" + num.ToString();
            new Survey_SurveyList1_Layer().UpdateSurveyTable(num2.ToString(), num.ToString());
            num7 = 2;
            goto Label_0002;
        }

        public void reName(string sNewName, long SID,long UID1)
        {
            sNewName = new Regex("[%-+=]").Replace(sNewName, "a");
            //OleDbCommand command = new OleDbCommand("UPDATE SurveyTable SET SurveyName='" + sNewName + "' WHERE SID=" + SID.ToString() + " AND UID=" + UID1.ToString(), objConn);
            new Survey_SurveyList1_Layer().UpdateSurveyTable1(sNewName, SID.ToString(), UID1.ToString());
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
