using checkState;
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
    public class Survey_MoveItem : Page, IRequiresSessionState
    {
        public string sClientJs = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            long num;
            long num2;
            long num3;
            short num4;
            short num5;
            short num6;
            checkState.checkState state;
            SqlDataReader reader = null; //∏≥≥ı÷µ
            int num7;
            goto Label_0033;
        Label_0002:
            switch (num7)
            {
                case 0:
                    if (state.getState( num, num3) < 1)
                    {
                        goto Label_0139;
                    }
                    num7 = 6;
                    goto Label_0002;

                case 1:
                case 8:
                    this.sClientJs = "IID=" + num2.ToString() + "\nd=" + num6.ToString();
                    return;

                case 2:
                    reader.Close();
                    num7 = 9;
                    goto Label_0002;

                case 3:
                    num5 = Convert.ToInt16(reader[0]);
                    num4 = Convert.ToInt16(reader[1]);
                    num7 = 2;
                    goto Label_0002;

                case 4:
                    goto Label_0139;

                case 5:
                    if (!reader.Read())
                    {
                        return;
                    }
                    num7 = 3;
                    goto Label_0002;

                case 6:
                    base.Response.End();
                    num7 = 4;
                    goto Label_0002;

                case 7:
                    //command.CommandText = "UPDATE ItemTable SET Sort=Sort-1 WHERE PageNo=" + num5.ToString() + " AND UID=" + num3.ToString() + " AND SID=" + num.ToString() + " AND Sort=" + Convert.ToString((int)(num4 + 1));
                    new Survey_MoveItem_Layer().UpdateItemTable(num5.ToString(), num3.ToString(), num.ToString(), Convert.ToString((int)(num4 + 1)));
                    //command.CommandText = " UPDATE ItemTable SET Sort=Sort+1 WHERE  UID=" + num3.ToString() + " AND SID=" + num.ToString() + " AND IID=" + num2.ToString();
                    new Survey_MoveItem_Layer().UpdateItemTable2(num3.ToString(), num.ToString(), num2.ToString());
                    num7 = 8;
                    goto Label_0002;

                case 9:
                    if (num6 != 1)
                    {
                        //command.CommandText = "UPDATE ItemTable SET Sort=Sort+1 WHERE PageNo=" + num5.ToString() + " AND UID=" + num3.ToString() + " AND SID=" + num.ToString() + " AND Sort=" + Convert.ToString((int)(num4 - 1));
                        new Survey_MoveItem_Layer().UpdateItemTable1(num5.ToString(), num3.ToString(), num.ToString(), Convert.ToString((int)(num4 - 1)));
                        //command.CommandText = " UPDATE ItemTable SET Sort=Sort-1 WHERE  UID=" + num3.ToString() + " AND SID=" + num.ToString() + " AND IID=" + num2.ToString();
                        new Survey_MoveItem_Layer().UpdateItemTable3(num3.ToString(), num.ToString(), num2.ToString());
                        num7 = 1;
                    }
                    else
                    {
                        num7 = 7;
                    }
                    goto Label_0002;
            }
        Label_0033:
            num = 0;
            num2 = 0;
            num3 = 0;
            num3 = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num4 = 0;
            num5 = 0;
            num6 = 0;
            num = Convert.ToInt64(base.Request.QueryString["SID"]);
            num2 = Convert.ToInt64(base.Request.QueryString["IID"]);
            state = new checkState.checkState();
            num7 = 0;
            goto Label_0002;
        Label_0139:
            num6 = Convert.ToInt16(base.Request.QueryString["d"]);
            //command.CommandText = "SELECT TOP 1 PageNo,Sort FROM ItemTable WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND IID=" + num2.ToString();
            reader = new Survey_MoveItem_Layer().GetItemTable(num.ToString(), num3.ToString(), num2.ToString());
            num7 = 5;
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
