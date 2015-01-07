using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_volumeAddPage : Page, IRequiresSessionState
    {
        protected Button Button1;
        protected HtmlForm form1;
        protected ListBox ListBox1;
        public string sClientJs = "var sMessage='';\n";
        public long UID;

        protected void Button1_Click(object sender, EventArgs e)
        {
            long num;
            int num2;
            SqlDataReader reader;
            int num3 = 0; //∏≥≥ı÷µ
            int num4;
            goto Label_0027;
        Label_0002:
            switch (num4)
            {
                case 0:
                    goto Label_0157;

                case 1:
                    this.sClientJs = this.sClientJs + "sMessage='ok';\n";
                    return;

                case 2:
                    if (!reader.Read())
                    {
                        goto Label_0157;
                    }
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num4 = 6;
                    goto Label_0002;

                case 3:
                    if (num3 < Convert.ToInt32(this.ListBox1.SelectedValue))
                    {
                        //command.CommandText = " INSERT INTO PageTable(UID,SID,PageNo) VALUES(" + this.UID.ToString() + "," + num.ToString() + "," + Convert.ToString((int)((num2 + num3) + 1)) + ")";
                        new Survey_volumeAddPage_Layer().InsertPageTable(Convert.ToString((int)((num2 + num3) + 1)), this.UID.ToString(), num.ToString());
                        num3++;
                        num4 = 4;
                    }
                    else
                    {
                        num4 = 1;
                    }
                    goto Label_0002;

                case 4:
                case 5:
                    num4 = 3;
                    goto Label_0002;

                case 6:
                    num2 = Convert.ToInt32(reader[0]);
                    num4 = 0;
                    goto Label_0002;
            }
        Label_0027:
            num = 0;
            num2 = 1;
            num = Convert.ToInt64(base.Request.QueryString["SID"]);
            //command = new OleDbCommand("SELECT Max(PageNo) FROM PageTable WHERE UID=" + this.UID.ToString() + " AND SID=" + num.ToString(), connection);
            reader = new Survey_volumeAddPage_Layer().GetPageTable(this.UID.ToString(), num.ToString());
            num4 = 2;
            goto Label_0002;
        Label_0157:
            reader.Close();
            reader.Dispose();
            num3 = 0;
            num4 = 5;
            goto Label_0002;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //new loginClass().checkLogin(out this.UID, "0");
            UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
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