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
using BusinessLayer.Survey;
using System.Data.SqlClient;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SetPar : Page, IRequiresSessionState
    {
        public StringBuilder sbClient = new StringBuilder("var sHiddenItem='';\nvar sURLVar = '';");
        public string sClientJs = "";
        public string sComplateMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
            DataSet set;
            SqlDataReader reader;
            int num3 = 0; //赋初值
            int num4 = 0; //赋初值
            int num5 = 0; //赋初值
            int num6;
            goto Label_0086;
        Label_0005:
            switch (num6)
            {
                case 0:
                case 0x1b:
                    this.sComplateMessage = reader["ComplateMessage"].ToString();
                    num6 = 20;
                    goto Label_0005;

                case 1:
                    if (num4 < set.Tables["PageStyle"].Rows.Count)
                    {
                        this.sbClient.Append("arrPageStyle[" + num4.ToString() + "] = new Array();\n");
                        this.sbClient.Append("arrPageStyle[" + num4.ToString() + "][0] = " + set.Tables["PageStyle"].Rows[num4]["P_ID"].ToString() + ";\n");
                        this.sbClient.Append("arrPageStyle[" + num4.ToString() + "][1] = '" + set.Tables["PageStyle"].Rows[num4]["PageFileName"].ToString() + "';\n");
                        this.sbClient.Append("arrPageStyle[" + num4.ToString() + "][2] = '" + set.Tables["PageStyle"].Rows[num4]["StyleName"].ToString() + "';\n");
                        this.sbClient.Append("arrPageStyle[" + num4.ToString() + "][3] = '" + set.Tables["PageStyle"].Rows[num4]["PageType"].ToString() + "';\n");
                        num4++;
                        num6 = 10;
                    }
                    else
                    {
                        num6 = 0x17;
                    }
                    goto Label_0005;

                case 2:
                case 0x12:
                    num6 = 13;
                    goto Label_0005;

                case 3:
                    if (!(reader["ClassID"].ToString() != ""))
                    {
                        this.sbClient.Append("var intClassID = -1;\n");
                        num6 = 0;
                    }
                    else
                    {
                        num6 = 0x18;
                    }
                    goto Label_0005;

                case 4:
                    if (Convert.ToString(";" + Convert.ToString(this.Session["Limits3"]) + ";").IndexOf(";clientreport.aspx;") < 0)
                    {
                        this.sbClient.Append("var blnSetClientReport=false;");
                        num6 = 0x13;
                    }
                    else
                    {
                        num6 = 12;
                    }
                    goto Label_0005;

                case 5:
                case 0x13:
                    this.sbClient.Append("var sReport = '" + reader["Report"].ToString() + "';\n");
                    this.sbClient.Append("var sEndDate = '" + reader["EndDate"].ToString() + "';\n");
                    this.sbClient.Append("var sToURL = '" + reader["ToURL"].ToString() + "';\n");
                    this.sbClient.Append("var sPar = '" + reader["Par"].ToString() + "';\n");
                    this.sbClient.Append("var intEndPage = " + reader["EndPage"].ToString() + ";\n");
                    this.sbClient.Append("var sActive = '" + reader["Active"].ToString() + "';\n");
                    this.sbClient.Append("var sSurveyPSW = '" + reader["SurveyPSW"].ToString() + "';\n");
                    this.sbClient.Append("var intMaxAnswerAmount = '" + reader["MaxAnswerAmount"].ToString() + "';\n");
                    this.sbClient.Append("var intSPoint = '" + reader["Point"].ToString() + "';\n");
                    this.sbClient.Append("var intLan='" + reader["Lan"] + "';");
                    num3 = 0;
                    num6 = 6;
                    goto Label_0005;

                case 6:
                case 15:
                    num6 = 7;
                    goto Label_0005;

                case 7:
                    if (num3 < set.Tables["SurveyExpand"].Rows.Count)
                    {
                        num6 = 9;
                    }
                    else
                    {
                        num6 = 0x1c;
                    }
                    goto Label_0005;

                case 8:
                    if (!reader.Read())
                    {
                        reader.Close();
                        base.Response.End();
                        num6 = 0x16;
                    }
                    else
                    {
                        num6 = 11;
                    }
                    goto Label_0005;

                case 9:
                    if (!(set.Tables["SurveyExpand"].Rows[num3]["ExpandType"].ToString() == "0"))
                    {
                        num6 = 0x1d;
                    }
                    else
                    {
                        num6 = 0x19;
                    }
                    goto Label_0005;

                case 10:
                case 0x11:
                    num6 = 1;
                    goto Label_0005;

                case 11:
                    num6 = 4;
                    goto Label_0005;

                case 12:
                    this.sbClient.Append("var blnSetClientReport=true;");
                    num6 = 5;
                    goto Label_0005;

                case 13:
                    if (num5 < set.Tables["SurveyClass"].Rows.Count)
                    {
                        this.sbClient.Append("arrSurveyClass[" + num5.ToString() + "] = new Array();\n");
                        this.sbClient.Append("arrSurveyClass[" + num5.ToString() + "][0] = " + set.Tables["SurveyClass"].Rows[num5]["CID"].ToString() + ";\n");
                        this.sbClient.Append("arrSurveyClass[" + num5.ToString() + "][1] = '" + set.Tables["SurveyClass"].Rows[num5]["SurveyClassName"].ToString() + "';\n");
                        num5++;
                        num6 = 2;
                    }
                    else
                    {
                        num6 = 0x10;
                    }
                    goto Label_0005;

                case 14:
                case 0x1a:
                    goto Label_08B7;

                case 0x10:
                    this.sClientJs = this.sbClient.ToString();
                    set.Dispose();
                    return;

                case 20:
                case 0x16:
                    this.sbClient.Append("var arrPageStyle = new Array();\n");
                    num4 = 0;
                    num6 = 0x11;
                    goto Label_0005;

                case 0x15:
                    this.sbClient.Append("sURLVar='" + set.Tables["SurveyExpand"].Rows[num3]["ExpandContent"].ToString() + "';\n");
                    num6 = 0x1a;
                    goto Label_0005;

                case 0x17:
                    this.sbClient.Append("var arrSurveyClass = new Array();\n");
                    num5 = 0;
                    num6 = 0x12;
                    goto Label_0005;

                case 0x18:
                    this.sbClient.Append("var intClassID = " + reader["ClassID"].ToString() + ";\n");
                    num6 = 0x1b;
                    goto Label_0005;

                case 0x19:
                    this.sbClient.Append("sHiddenItem='" + set.Tables["SurveyExpand"].Rows[num3]["ExpandContent"].ToString() + "';\n");
                    num6 = 14;
                    goto Label_0005;

                case 0x1c:
                    num6 = 3;
                    goto Label_0005;

                case 0x1d:
                    if (!(set.Tables["SurveyExpand"].Rows[num3]["ExpandType"].ToString() == "1"))
                    {
                        goto Label_08B7;
                    }
                    num6 = 0x15;
                    goto Label_0005;
            }
        Label_0086:
            num = 0;
            long num2 = Convert.ToInt64(base.Request.QueryString["SID"]);
            //class2.checkLogin(out num, "0");
            num = ConvertHelper.ConvertLong(this.Session["UserID"]);
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "setupoption.aspx", 0, "没有权限", "");
            languageClass class3 = new languageClass();
            this.sbClient.Append("var SID=" + num2.ToString() + ";");
            class3.getLanList();
            this.sbClient.Append("var sLan='" + class3._sLanList + "';");
            set = new DataSet();
            //command.CommandText = "SELECT P_ID,PageFileName,StyleName,PageType FROM PageStyle WHERE PageType>=1  ORDER BY Sort";
            //adapter.SelectCommand = command;
            //adapter.Fill(set, "PageStyle");
            DataTable PageStyle = new Survey_SetPar_Layer().GetPageStyle();
            PageStyle.TableName = "PageStyle";

            //command.CommandText = "SELECT CID,SurveyClassName FROM SurveyClass ORDER BY Sort";
            //adapter.Fill(set, "SurveyClass");
            DataTable SurveyClass = new Survey_SetPar_Layer().GetSurveyClass();
            SurveyClass.TableName = "SurveyClass";

            //command.CommandText = "SELECT ExpandContent,ExpandType FROM SurveyExpand WHERE UID=" + num.ToString() + " AND SID=" + num2.ToString();
            //adapter.Fill(set, "SurveyExpand");
            DataTable SurveyExpand = new Survey_SetPar_Layer().GetSurveyExpand(num.ToString(), num2.ToString());
            SurveyExpand.TableName = "SurveyExpand";
            set.Tables.Add(PageStyle);
            set.Tables.Add(SurveyClass);
            set.Tables.Add(SurveyExpand);

            //command.CommandText = "SELECT * FROM SurveyTable WHERE SID=" + num2.ToString() + " AND UID=" + num.ToString();
            //reader = command.ExecuteReader();
            reader = new Survey_SetPar_Layer().GetSurveyTable(num.ToString(), num2.ToString());


            num6 = 8;
            goto Label_0005;
        Label_08B7:
            num3++;
            num6 = 15;
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

