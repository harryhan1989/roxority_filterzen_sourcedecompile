using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SortOutPage : Page, IRequiresSessionState
    {
        public string sClientJs = "var sMessage='';\n";
        public long UID;

        protected string getItemAndPageList(SqlDataReader dr, long SID, long UID1)
        {
            string str;
        Label_002B:
            str = "";
            int num = 0;
            //objComm.CommandText = "SELECT IID,ItemName,PageNo FROM ItemTable WHERE UID=" + UID1.ToString() + " AND SID=" + SID.ToString() + " AND ParentID=0 ORDER BY PageNo";
            dr = new Survey_SortOutPage_Layer().GetItemTable(UID1.ToString(), SID.ToString());
            int num2 = 7;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 7:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num2 = 6;
                    goto Label_0002;

                case 1:
                    dr.Close();
                    return str;

                case 2:
                case 4:
                    num2 = 5;
                    goto Label_0002;

                case 3:
                    dr.Close();
                    num = 0;
                    //objComm.CommandText = "SELECT PID,PageNo FROM PageTable WHERE UID=" + UID1.ToString() + " AND SID=" + SID.ToString() + " ORDER BY PageNo";
                    dr = new Survey_SortOutPage_Layer().GetPageTable(UID1.ToString(), SID.ToString());
                    num2 = 2;
                    goto Label_0002;

                case 5:
                    if (dr.Read())
                    {
                        object obj5 = str + "arrPage[" + num.ToString() + "] = new Array();\n";
                        object obj6 = string.Concat(new object[] { obj5, "arrPage[", num.ToString(), "][0] =", dr[0], ";\n" });
                        str = string.Concat(new object[] { obj6, "arrPage[", num.ToString(), "][1] =", dr[1], ";\n" });
                        num++;
                        num2 = 4;
                    }
                    else
                    {
                        num2 = 1;
                    }
                    goto Label_0002;

                case 6:
                    if (dr.Read())
                    {
                        object obj2 = str + "arrItem[" + num.ToString() + "] = new Array();\n";
                        object obj3 = string.Concat(new object[] { obj2, "arrItem[", num.ToString(), "][0] =", dr[0], ";\n" });
                        object obj4 = string.Concat(new object[] { obj3, "arrItem[", num.ToString(), "][1] ='", dr[1], "';\n" });
                        str = string.Concat(new object[] { obj4, "arrItem[", num.ToString(), "][2] =", dr[2], ";\n" });
                        num++;
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;
            }
            goto Label_002B;
        }

        protected int getMaxPage(SqlDataReader dr, long SID, long UID1)
        {
        Label_0017:
            if ((1 != 0) && (0 != 0))
            {
            }
            int num = 1;
            //objComm.CommandText = "SELECT Max(PageNo) FROM PageTable WHERE UID=" + UID1.ToString() + " AND SID=" + SID.ToString();
            dr = new Survey_SortOutPage_Layer().GetPageTable1(UID1.ToString(), SID.ToString());
            int num2 = 2;
        Label_0002:
            switch (num2)
            {
                case 0:
                    num = Convert.ToInt32(dr[0]);
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    if (!dr.Read())
                    {
                        break;
                    }
                    num2 = 0;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            dr.Close();
            dr.Dispose();
            return num;
        }

        protected string moveItemToPage(SqlDataReader dr, long SID, long UID1, string sItem, string sToPage)
        {
            //objComm.CommandText = "UPDATE ItemTable SET PageNo=" + sToPage + " WHERE SID=" + SID.ToString() + " AND UID=" + UID1.ToString() + " AND IID IN(" + sItem + ")";
            new Survey_SortOutPage_Layer().UpdateItemTable(sToPage, SID.ToString(), UID1.ToString(), sItem);
            return "题目移动完成";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            SqlDataReader reader = null; //赋初值
            string str6 = null; //赋初值
            int num3;
            goto Label_0043;
        Label_0002:
            switch (num3)
            {
                case 0:
                case 1:
                case 2:
                case 11:
                    this.sClientJs = this.sClientJs + this.getItemAndPageList( reader, num, this.UID);

                    return;

                case 3:
                    if (str6 == "Form1")
                    {
                        str4 = base.Request.Form["ItemList"].ToString();
                        str5 = base.Request.Form["PageList"].ToString();
                        this.sClientJs = this.sClientJs + "sMessage='" + this.moveItemToPage( reader, num, this.UID, str4, str5) + "';\n";
                        this.sClientJs = this.sClientJs + "action='Card1';\n";
                        num3 = 11;
                    }
                    else
                    {
                        num3 = 5;
                    }
                    goto Label_0002;

                case 4:
                    if (str6 == "Form2")
                    {
                        str2 = base.Request.Form["DelPageNo"].ToString();
                        str3 = base.Request.Form["DelPageID"].ToString();
                        this.sClientJs = this.sClientJs + "sMessage='" + this.saveDelPage( reader, num, this.UID, str3, str2) + "';\n";
                        this.sClientJs = this.sClientJs + "action='Card2';\n";
                        num3 = 0;
                    }
                    else
                    {
                        num3 = 13;
                    }
                    goto Label_0002;

                case 5:
                    num3 = 4;
                    goto Label_0002;

                case 6:
                    num3 = 9;
                    goto Label_0002;

                case 7:
                    try
                    {
                        str = base.Request.Form["Flag"].ToString();
                    }
                    catch
                    {
                    }
                    reader = null;
                    num3 = 10;
                    goto Label_0002;

                case 8:
                    if (str6 == "Form3")
                    {
                        int intAddAmount = Convert.ToInt32(base.Request.Form["NewPage"]);
                        this.sClientJs = this.sClientJs + "sMessage='" + this.saveAddPage( reader, num, this.UID, intAddAmount) + "';\n";
                        this.sClientJs = this.sClientJs + "action='Card3';\n";
                        num3 = 2;
                    }
                    else
                    {
                        num3 = 6;
                    }
                    goto Label_0002;

                case 9:
                    goto Label_029C;

                case 10:
                    str6 = str;
                    if (str6 == null)
                    {
                        goto Label_029C;
                    }
                    num3 = 12;
                    goto Label_0002;

                case 12:
                    num3 = 3;
                    goto Label_0002;

                case 13:
                    num3 = 8;
                    goto Label_0002;
            }
        Label_0043:
            num = 0;
            str = "";
            //new loginClass().checkLogin(out this.UID, "0");
            UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num = Convert.ToInt64(base.Request.QueryString["SID"]);
            str2 = "";
            str3 = "";
            str4 = "";
            str5 = "";
            num3 = 7;
            goto Label_0002;
        Label_029C:
            this.sClientJs = this.sClientJs + "action='Card1';\n";
            num3 = 1;
            goto Label_0002;
        }

        protected string saveAddPage(SqlDataReader dr, long SID, long UID1, int intAddAmount)
        {
            int num;
            int num2;
            int num3;
            goto Label_001B;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num2 < intAddAmount)
                    {
                        goto Label_0057;
                    }
                    num3 = 1;
                    goto Label_0002;

                case 1:
                    if ((1 == 0) || (0 == 0))
                    {
                        return "增加页完成";
                    }
                    goto Label_0057;

                case 2:
                case 3:
                    num3 = 0;
                    goto Label_0002;
            }
        Label_001B:
            num = this.getMaxPage(dr, SID, UID1);
            num2 = 0;
            num3 = 3;
            goto Label_0002;
        Label_0057: ;
            //objComm.CommandText = " INSERT INTO PageTable(UID,SID,PageNo) VALUES(" + UID1.ToString() + "," + SID.ToString() + "," + Convert.ToString((int)((num + num2) + 1)) + ")";
            new Survey_SortOutPage_Layer().InsertPageTable(UID1.ToString(), SID.ToString(), Convert.ToString((int)((num + num2) + 1)));
            num2++;
            num3 = 2;
            goto Label_0002;
        }

        protected string saveDelPage(SqlDataReader dr, long SID, long UID1, string sDelPageID, string sDelPageNo)
        {
            string str;
        Label_001B:
            str = "";
            int num = 3;
        Label_0002:
            switch (num)
            {
                case 0:
                    if (!(sDelPageNo == ""))
                    {
                        //objComm.CommandText = " DELETE FROM PageTable WHERE UID=" + UID1.ToString() + " AND SID=" + SID.ToString() + " AND PID=" + sDelPageID;
                        new Survey_SortOutPage_Layer().DeletePageTable(UID1.ToString(), SID.ToString(), sDelPageID);
                        //objComm.CommandText = " UPDATE PageTable SET PageNo=PageNo-1 WHERE UID=" + UID1.ToString() + " AND SID=" + SID.ToString() + " AND PageNo>" + sDelPageNo;
                        new Survey_SortOutPage_Layer().UpdatePageTable(UID1.ToString(), SID.ToString(), sDelPageNo);
                        //objComm.CommandText = " UPDATE ItemTable SET PageNo=PageNo-1 WHERE  UID=" + UID1.ToString() + " AND SID=" + SID.ToString() + " AND PageNo>=" + sDelPageNo;
                        new Survey_SortOutPage_Layer().UpdateItemTable(UID1.ToString(), SID.ToString(), sDelPageNo);
                        //objComm.CommandText = " UPDATE ItemTable SET Logic='' WHERE SID=" + SID.ToString() + " AND UID=" + UID1.ToString();
                        new Survey_SortOutPage_Layer().UpdateItemTable1(SID.ToString(), UID1.ToString());
                        return "删除完成";
                    }
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num = 2;
                    goto Label_0002;

                case 1:
                    num = 0;
                    goto Label_0002;

                case 2:
                    break;

                case 3:
                    if (sDelPageID == "")
                    {
                        break;
                    }
                    num = 1;
                    goto Label_0002;

                default:
                    goto Label_001B;
            }
            return "没有选择删除页";
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
