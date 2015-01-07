using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.OleDb;
using System.Configuration;
using LoginClass;
using BusinessLayer.Survey;
using System.Data.SqlClient;
using Business.Helper;

//对声明的局部变量赋初值
namespace Web_Survey.Survey
{
    public class Survey_AddPage : Page, IRequiresSessionState
    {
        public string sMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            long num;
            short num2;
            long num3;
            short num4;
            checkState.checkState state;
            SqlDataReader reader = null; //赋初值
            int num5;
            goto Label_002F;
        Label_0002:
            switch (num5)
            {
                case 0:
                    if (num2 != -1)
                    {
                        //command.CommandText = "UPDATE PageTable SET PageNo=PageNo+1 WHERE PageNo>=" + num2.ToString() + " AND UID=" + num3.ToString() + " AND SID=" + num.ToString();
                        new Survey_AddPage_Layer().UpdatePageTable(num2.ToString(), num3.ToString(), num.ToString());
                        //command.CommandText = " INSERT INTO PageTable(PageNo,SID,UID) VALUES(" + num2.ToString() + "," + num.ToString() + "," + num3.ToString() + ")";
                        new Survey_AddPage_Layer().InsertPageTable(num2.ToString(), num3.ToString(), num.ToString());
                        //command.CommandText = " UPDATE ItemTable SET PageNo=PageNo+1 WHERE UID=" + num3.ToString() + " AND SID=" + num.ToString() + " AND PageNo>=" + num2.ToString();
                        new Survey_AddPage_Layer().UpdateItemTable(num3.ToString(), num.ToString(), num2.ToString());
                        num5 = 7;
                    }
                    else
                    {
                        num5 = 4;
                    }
                    goto Label_0002;

                case 1:
                    if (!reader.Read())
                    {
                        goto Label_0127;
                    }
                    num5 = 2;
                    goto Label_0002;

                case 2:
                    num4 = Convert.ToInt16(reader[0]);
                    num5 = 3;
                    goto Label_0002;

                case 3:
                    goto Label_0127;

                case 4:
                    //command.CommandText = "SELECT TOP 1 PageNo FROM PageTable WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " ORDER BY PageNo DESC";
                    reader = new Survey_AddPage_Layer().GetPageTable(num.ToString(), num3.ToString());
                    num5 = 1;
                    goto Label_0002;

                case 5:
                    if (state.getState( num, num3) < 1)
                    {
                        num5 = 0;
                    }
                    else
                    {
                        num5 = 6;
                    }
                    goto Label_0002;

                case 6:
                    this.sMessage = "Error";
                    return;

                case 7:
                case 8:
                    return;
            }
        Label_002F:
            num = 0;
            num2 = 0;
            num3 = 0;
            //loginClass class2 = new loginClass();
            //class2.checkLogin(out num3, "0");
            num3=ConvertHelper.ConvertLong(this.Session["UserID"]);
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "addpage.aspx", 2, "没有权限", "");
            num = Convert.ToInt64(base.Request.QueryString["SID"]);
            num2 = Convert.ToInt16(base.Request.QueryString["FrontPage"]);
            num4 = 0;
            state = new checkState.checkState();
            num5 = 5;
            goto Label_0002;
        Label_0127:
            if ((1 != 0) && (0 != 0))
            {
            }
            reader.Close();
            num4 = (short)(num4 + 1);
            //command.CommandText = "INSERT INTO PageTable(PageNo,SID,UID) VALUES(" + num4.ToString() + "," + num.ToString() + "," + num3.ToString() + ")";
            new Survey_AddPage_Layer().InsertPageTable(num4.ToString(), num3.ToString(), num.ToString());
            num5 = 8;
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

