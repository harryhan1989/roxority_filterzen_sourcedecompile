using checkState;
using LoginClass;
using System;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Configuration;
using BusinessLayer.Survey;
using System.Data.SqlClient;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_MovePage : Page, IRequiresSessionState
    {
        public string sClientJs = "";

        protected void Page_Load(object sender, EventArgs e)
        {
                long num;
                int num2;
                long num3;
                short num4;
                short num5;
                checkState.checkState state;
                SqlDataReader reader = null; //∏≥≥ı÷µ
                int num6;
                goto Label_0033;
            Label_0002:
                switch (num6)
                {
                    case 0:
                        //command.CommandText = "UPDATE PageTable SET PageNo=PageNo-1 WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PageNo = " + Convert.ToString((int)(num4 + 1));
                        new Survey_MovePage_Layer().UpdatePageTable(num.ToString(), num3.ToString(), Convert.ToString((int)(num4 + 1)));
                        //command.CommandText = " UPDATE PageTable SET PageNo=PageNo+1 WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PID=" + num2.ToString();
                        new Survey_MovePage_Layer().UpdatePageTable1(num.ToString(), num3.ToString(), num2.ToString());
                        //command.CommandText = " UPDATE ItemTable SET PageNo=0 WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PageNo=" + Convert.ToString((int)(num4 + 1));
                        new Survey_MovePage_Layer().UpdateItemTable(num.ToString(), num3.ToString(), Convert.ToString((int)(num4 + 1)));
                        //command.CommandText = " UPDATE ItemTable SET PageNo=PageNo+1 WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PageNo=" + num4.ToString();
                        new Survey_MovePage_Layer().UpdateItemTable1(num.ToString(), num3.ToString(), num4.ToString());
                        //command.CommandText = " UPDATE ItemTable SET PageNo=" + num4.ToString() + " WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PageNo=0";
                        new Survey_MovePage_Layer().UpdateItemTable2(num4.ToString(), num.ToString(), num3.ToString());

                        num6 = 2;
                        goto Label_0002;

                    case 1:
                        base.Response.End();
                        num6 = 9;
                        goto Label_0002;

                    case 2:
                    case 5:
                        this.sClientJs = "PID=" + num2.ToString() + "\nd=" + num5.ToString();
                        return;

                    case 3:
                        if (state.getState(num, num3) < 1)
                        {
                            goto Label_04F2;
                        }
                        num6 = 1;
                        goto Label_0002;

                    case 4:
                        if (!reader.Read())
                        {
                            goto Label_05A3;
                        }
                        num6 = 6;
                        goto Label_0002;

                    case 6:
                        num4 = Convert.ToInt16(reader[0]);
                        num6 = 8;
                        goto Label_0002;

                    case 7:
                        if (num5 != 1)
                        {
                            //command.CommandText = "UPDATE PageTable SET PageNo=PageNo+1 WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PageNo = " + Convert.ToString((int)(num4 - 1));
                            new Survey_MovePage_Layer().UpdatePageTable2(num.ToString(), num3.ToString(), Convert.ToString((int)(num4 - 1)));
                            //command.CommandText = " UPDATE PageTable SET PageNo=PageNo-1 WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PID=" + num2.ToString();
                            new Survey_MovePage_Layer().UpdatePageTable3(num.ToString(), num3.ToString(), num2.ToString());
                            //command.CommandText = " UPDATE ItemTable SET PageNo=0 WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PageNo=" + Convert.ToString((int)(num4 - 1));
                            new Survey_MovePage_Layer().UpdateItemTable(num.ToString(), num3.ToString(), Convert.ToString((int)(num4 - 1)));
                            //command.CommandText = " UPDATE ItemTable SET PageNo=PageNo-1 WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PageNo=" + num4.ToString();
                            new Survey_MovePage_Layer().UpdateItemTable3(num.ToString(), num3.ToString(), num4.ToString());
                            //command.CommandText = " UPDATE ItemTable SET PageNo=" + num4.ToString() + " WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PageNo=0";
                            new Survey_MovePage_Layer().UpdateItemTable2(num4.ToString(), num.ToString(), num3.ToString());
                            num6 = 5;
                        }
                        else
                        {
                            num6 = 0;
                        }
                        goto Label_0002;

                    case 8:
                        goto Label_05A3;

                    case 9:
                        goto Label_04F2;
                }
            Label_0033:
                num = 0;
                num2 = 0;
                num3 = 0;
                num3 = ConvertHelper.ConvertLong(this.Session["UserID"]);
                num4 = 0;
                num5 = 0;
                num = Convert.ToInt64(base.Request.QueryString["SID"]);
                num2 = Convert.ToInt32(base.Request.QueryString["PID"]);

                state = new checkState.checkState();
                num6 = 3;
                goto Label_0002;
            Label_04F2:
                num5 = Convert.ToInt16(base.Request.QueryString["d"]);
                //command.CommandText = "SELECT TOP 1 PageNo FROM PageTable WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND PID=" + num2.ToString();
                reader = new Survey_MovePage_Layer().GetPageTable(num.ToString(), num3.ToString(), num2.ToString());
                num6 = 4;
                goto Label_0002;
            Label_05A3:
                reader.Close();
                num6 = 7;
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
