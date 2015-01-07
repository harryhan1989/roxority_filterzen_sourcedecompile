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
    public class Survey_DelPage : Page, IRequiresSessionState
    {
        public string sClientJs = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            int num;
            long num2;
            long num3;
            int num4;
            checkState.checkState state;
           SqlDataReader reader = null; //∏≥≥ı÷µ
            bool flag = false; //∏≥≥ı÷µ
            int num5;
            goto Label_0037;
        Label_0002:
            switch (num5)
            {
                case 0:
                    goto Label_0157;

                case 1:
                    if (!flag)
                    {
                        return;
                    }
                    num5 = 10;
                    goto Label_0002;

                case 2:
                    if (state.getState(num3, num2) < 1)
                    {
                        goto Label_0157;
                    }
                    num5 = 7;
                    goto Label_0002;

                case 3:
                    goto Label_0139;

                case 4:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_0344;
                    }
                    goto Label_0139;

                case 5:
                    if (!reader.Read())
                    {
                        goto Label_0344;
                    }
                    num5 = 6;
                    goto Label_0002;

                case 6:
                    num4 = Convert.ToInt16(reader[0]);
                    num5 = 9;
                    goto Label_0002;

                case 7:
                    base.Response.End();
                    num5 = 0;
                    goto Label_0002;

                case 8:
                    return;

                case 9:
                    if (num4 != 1)
                    {
                        flag = true;
                        num5 = 4;
                    }
                    else
                    {
                        num5 = 3;
                    }
                    goto Label_0002;

                case 10:
                    //command.CommandText = "UPDATE PageTable SET PageNo=PageNo-1 WHERE PageNo>" + num4.ToString() + " AND UID=" + num2.ToString() + " AND SID=" + num3.ToString();
                    new Survey_DelPage_Layer().UpdatePageTable( num4.ToString(),num2.ToString(),num3.ToString());
                    //command.CommandText = " DELETE FROM PageTable WHERE SID=" + num3.ToString() + " AND UID=" + num2.ToString() + " AND PID=" + num.ToString();
                    new Survey_DelPage_Layer().DeletePageTable(num3.ToString(),num2.ToString(),num.ToString());
                    //command.CommandText = "UPDATE ItemTable SET PageNo=PageNo-1 WHERE PageNo>=" + num4.ToString() + " AND SID=" + num3.ToString() + " AND UID=" + num2.ToString();
                    new Survey_DelPage_Layer().UpdateItemTable( num4.ToString(),num2.ToString(),num3.ToString());
                    num5 = 8;
                    goto Label_0002;
            }
        Label_0037:           
            num = 0;
            num2 = 0;
            num3 = 0;
            //new loginClass().checkLogin(out num2, "0");
            num2 = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num4 = 0;
            num3 = Convert.ToInt64(base.Request.QueryString["SID"]);
            num = Convert.ToInt32(base.Request.QueryString["PID"]);
            state = new checkState.checkState();
            this.sClientJs = "SID=" + num3.ToString() + ";\n";
            num5 = 2;
            goto Label_0002;
        Label_0139:
            this.sClientJs = this.sClientJs + "PageNo=1;\n";
            return;
        Label_0157:
            //command.CommandText = "SELECT TOP 1 PageNo FROM PageTable WHERE SID=" + num3.ToString() + " AND UID=" + num2.ToString() + " AND PID=" + num.ToString();
            reader = new Survey_DelPage_Layer().GetPageTable(num3.ToString(),num2.ToString(), num.ToString());
            flag = false;
            num5 = 5;
            goto Label_0002;
        Label_0344:
            reader.Close();
            num5 = 1;
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
