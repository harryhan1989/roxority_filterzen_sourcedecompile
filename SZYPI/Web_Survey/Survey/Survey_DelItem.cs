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
    public class Survey_DelItem : Page, IRequiresSessionState
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
            checkState.checkState state;
            SqlDataReader reader = null; //¸³³õÖµ
            int num6;
            goto Label_0023;
        Label_0002:
            switch (num6)
            {
                case 0:
                    goto Label_0142;

                case 1:
                    reader.Close();
                    //command.CommandText = "UPDATE ItemTable SET Sort=Sort-1 WHERE Sort>" + num5.ToString() + " AND UID=" + num3.ToString() + " AND SID=" + num.ToString() + " AND PageNo=" + num4.ToString();
                    new Survey_DelItem_Layer().UpdateItemTable(num5.ToString(), num3.ToString(), num.ToString(), num4.ToString());
                    //command.CommandText = " DELETE FROM ItemTable WHERE (IID=" + num2.ToString() + " AND  UID=" + num3.ToString() + " AND SID=" + num.ToString() + ") OR (ParentID=" + num2.ToString() + " AND UID=" + num3.ToString() + " AND SID=" + num.ToString() + ")";
                    new Survey_DelItem_Layer().DeleteItemTable(num2.ToString(), num3.ToString(), num.ToString(), num2.ToString());
                    //command.CommandText = " DELETE FROM OptionTable WHERE IID=" + num2.ToString() + " AND  UID=" + num3.ToString() + " AND SID=" + num.ToString();
                    new Survey_DelItem_Layer().DeleteOptionTable(num2.ToString(), num3.ToString(), num.ToString());
                    return;

                case 2:
                    if (!reader.Read())
                    {
                        return;
                    }
                    num6 = 3;
                    goto Label_0002;

                case 3:
                    num4 = Convert.ToInt16(reader[0]);
                    num5 = Convert.ToInt16(reader[1]);
                    num6 = 1;
                    goto Label_0002;

                case 4:
                    base.Response.End();
                    num6 = 0;
                    goto Label_0002;

                case 5:
                    if (state.getState(num, num3) < 1)
                    {
                        goto Label_0142;
                    }
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num6 = 4;
                    goto Label_0002;
            }
        Label_0023:
            num = 0;
            num2 = 0;
            num3 = 0;
            num3 = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num4 = 0;
            num5 = 0;
            num = Convert.ToInt64(base.Request.QueryString["SID"]);
            num2 = Convert.ToInt64(base.Request.QueryString["IID"]);
            this.sClientJs = "SID=" + num.ToString() + ";\n";
            state = new checkState.checkState();
            num6 = 5;
            goto Label_0002;
        Label_0142:
            //command.CommandText = "SELECT TOP 1 PageNo,Sort FROM ItemTable WHERE SID=" + num.ToString() + " AND UID=" + num3.ToString() + " AND IID=" + num2.ToString();
            reader = new Survey_DelItem_Layer().GetItemTable(num.ToString(), num3.ToString(), num2.ToString());
            num6 = 2;
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
