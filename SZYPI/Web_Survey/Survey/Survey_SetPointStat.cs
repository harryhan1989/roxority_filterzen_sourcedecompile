using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using BusinessLayer.Survey;
using System.Data.SqlClient;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SetPointStat : Page, IRequiresSessionState
    {
        public string sClientJs = "";
        public string sList = "";
        public string sModifyResult = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
            long num2;
            string str = null; //赋初值
            string str2 = null; //赋初值
            string str3 = null; //赋初值
            string str4 = null; //赋初值
            string str5 = null; //赋初值
            string str6 = null; //赋初值
            string str7 = null; //赋初值
            int num3 = 0; //赋初值
            int num4 = 0; //赋初值
            SqlDataReader reader = null; //赋初值
            int num5;
            goto Label_0086;
        Label_0005:
            switch (num5)
            {
                case 0:
                    goto Label_0816;

                case 1:
                case 11:
                    this.sList = this.sList + "<td>" + reader["MinValue"].ToString() + "</td><td>并且</td>";
                    num5 = 10;
                    goto Label_0005;

                case 2:
                    if (reader.Read())
                    {
                        this.sList = this.sList + "<tr bgcolor=#FFFFFF><td>当得分</td>";
                        num5 = 5;
                    }
                    else
                    {
                        num5 = 0x19;
                    }
                    goto Label_0005;

                case 3:
                    goto Label_0964;

                case 4:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_0964;
                    }
                    goto Label_034E;

                case 5:
                    if (Convert.ToInt16(reader["Relation1"]) != 0)
                    {
                        this.sList = this.sList + "<td>大于</td>";
                        num5 = 11;
                    }
                    else
                    {
                        num5 = 0x18;
                    }
                    goto Label_0005;

                case 6:
                    goto Label_066B;

                case 7:
                    base.Response.End();
                    num5 = 15;
                    goto Label_0005;

                case 8:
                case 0x16:
                    num5 = 2;
                    goto Label_0005;

                case 9:
                    if (!(str3 == "Yes"))
                    {
                        num5 = 0x1c;
                    }
                    else
                    {
                        num5 = 0x10;
                    }
                    goto Label_0005;

                case 10:
                    if (Convert.ToInt16(reader["Relation2"]) != 2)
                    {
                        this.sList = this.sList + "<td>小于</td>";
                        num5 = 0x12;
                    }
                    else
                    {
                        num5 = 13;
                    }
                    goto Label_0005;

                case 12:
                    //command.CommandText = "SELECT TOP 1 * FROM ReviewPoint  WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString() + " AND ID=" + str5;
                    reader = new Survey_SetPointStat_Layer().GetReviewPoint(num.ToString(), num2.ToString(), str5);
                    num5 = 0x1d;
                    goto Label_0005;

                case 13:
                    this.sList = this.sList + "<td>小于等于</td>";
                    num5 = 0x15;
                    goto Label_0005;

                case 14:
                    if (num > 0)
                    {
                        goto Label_069D;
                    }
                    num5 = 7;
                    goto Label_0005;

                case 15:
                    goto Label_069D;

                case 0x10:
                    //command.CommandText = "INSERT INTO ReviewPoint(Relation1,Relation2,MaxValue,MinValue,Result,SID,UID) VALUES(" + str + "," + str2 + "," + num3.ToString() + "," + num4.ToString() + ",'" + str6 + "'," + num.ToString() + "," + num2.ToString() + ")";
                    new Survey_SetPointStat_Layer().InsertReviewPoint(str, str2, num3.ToString(), num4.ToString(), str6, num.ToString(), num2.ToString());
                    num5 = 4;
                    goto Label_0005;

                case 0x11:
                    if (!(str3 == "update"))
                    {
                        goto Label_0964;
                    }
                    num5 = 0x1b;
                    goto Label_0005;

                case 0x12:
                case 0x15:
                    {
                        string sList = this.sList;
                        this.sList = sList + "<td>" + reader["MaxValue"].ToString() + "</td><td>" + reader["Result"].ToString() + "</td>";
                        string str10 = this.sList;
                        this.sList = str10 + "<td onclick='modify(" + reader["ID"].ToString() + ")' onmousemove=this.className='BTMove' onmouseout=this.className='BTNormal' class='BTNormal'><img src='images/edit2.gif' alt='修改'></td><td onclick='del(" + reader["ID"].ToString() + ")' onmousemove=this.className='BTMove' onmouseout=this.className='BTNormal' class='BTNormal'><img src='images/del.gif' alt='删除'></td></tr>";
                        num5 = 0x16;
                        goto Label_0005;
                    }
                case 0x13:
                    goto Label_045A;

                case 20:
                    this.sClientJs = this.sClientJs + "intModifyID=" + reader["ID"].ToString() + ";\n";
                    this.sClientJs = this.sClientJs + "var intRelation1=" + reader["Relation1"].ToString() + ";\n";
                    this.sClientJs = this.sClientJs + "var intRelation2=" + reader["Relation2"].ToString() + ";\n";
                    this.sClientJs = this.sClientJs + "var intMaxValue=" + reader["MaxValue"].ToString() + ";\n";
                    this.sClientJs = this.sClientJs + "var intMinValue=" + reader["MinValue"].ToString() + ";\n";
                    this.sModifyResult = reader["Result"].ToString();
                    num5 = 0;
                    goto Label_0005;

                case 0x17:
                    //command.CommandText = "DELETE FROM ReviewPoint WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString() + " AND ID=" + str5;
                    new Survey_SetPointStat_Layer().DeleteReviewPoint(num.ToString(), num2.ToString(), str5);
                    num5 = 6;
                    goto Label_0005;

                case 0x18:
                    goto Label_034E;

                case 0x19:
                    return;

                case 0x1a:
                    if (!(str4 == "modify"))
                    {
                        goto Label_045A;
                    }
                    num5 = 12;
                    goto Label_0005;

                case 0x1b:
                    //command.CommandText = "UPDATE ReviewPoint SET Relation1=" + str + ",Relation2=" + str2 + ",MaxValue=" + num3.ToString() + ",MinValue=" + num4.ToString() + ",Result='" + str6 + "'  WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString() + " AND ID=" + str7.ToString();
                    new Survey_SetPointStat_Layer().UpdateReviewPoint(str, str2, num3.ToString(), num4.ToString(), str6, num.ToString(), num2.ToString(), str7.ToString());
                    num5 = 3;
                    goto Label_0005;

                case 0x1c:
                    if (!(str4 == "del"))
                    {
                        goto Label_066B;
                    }
                    num5 = 0x17;
                    goto Label_0005;

                case 0x1d:
                    if (!reader.Read())
                    {
                        goto Label_0816;
                    }
                    num5 = 20;
                    goto Label_0005;
            }
        Label_0086:
            num = 0;
            num2 = 0;
            num2=ConvertHelper.ConvertLong(this.Session["UserID"]);
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "ex:pointtable", 2, "没有权限", "");
            num = Convert.ToInt32(base.Request.QueryString["SID"]);
            this.sClientJs = "var SID=" + num.ToString() + ";\n";
            this.sClientJs = this.sClientJs + "var intModifyID = 0;\n";
            num5 = 14;
            goto Label_0005;
        Label_034E:
            this.sList = this.sList + "<td>大于等于</td>";
            num5 = 1;
            goto Label_0005;
        Label_045A:
            num5 = 0x11;
            goto Label_0005;
        Label_066B:
            num5 = 0x1a;
            goto Label_0005;
        Label_069D:
            str = Convert.ToString(base.Request.Form["Relation1"]);
            str2 = Convert.ToString(base.Request.Form["Relation2"]);
            str3 = Convert.ToString(base.Request.Form["Flag"]);
            str4 = Convert.ToString(base.Request.QueryString["A"]);
            str5 = Convert.ToString(base.Request.QueryString["ID"]);
            str6 = Convert.ToString(base.Request.Form["Result"]);
            str7 = Convert.ToString(base.Request.Form["ModifyID"]);
            num3 = Convert.ToInt32(base.Request.Form["MaxValue"]);
            num4 = Convert.ToInt32(base.Request.Form["MinValue"]);
            num5 = 9;
            goto Label_0005;
        Label_0816:
            reader.Close();
            num5 = 0x13;
            goto Label_0005;
        Label_0964:
            //command.CommandText = "SELECT * FROM ReviewPoint WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString();
            reader = new Survey_SetPointStat_Layer().GetReviewPoint1(num.ToString(), num2.ToString());
            num5 = 8;
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

