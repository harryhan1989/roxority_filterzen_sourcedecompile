using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_ItemBatchSort : Page, IRequiresSessionState
    {
        public string sClientJs = "var arrItem = new Array();";

        protected string getPageItem(long UID, long SID, int PID)
        {
            StringBuilder builder;
        Label_001B:
            builder = new StringBuilder();
            //SqlDataReader reader = new OleDbCommand("SELECT IID,ItemName,ItemType,Sort FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND PageNo=(SELECT TOP 1 PageNo FROM PageTable WHERE PID=" + PID.ToString() + ") ORDER BY Sort", objConn).ExecuteReader();
            SqlDataReader reader = new Survey_ItemBatchSort_Layer().GetItemTable(SID.ToString(), UID.ToString(), PID.ToString());
            int num = 0;
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 1:
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    if (reader.Read())
                    {
                        builder.Append(string.Concat(new object[] { "arrItem[", num.ToString(), "] = [", reader["IID"], ",'", reader["ItemName"].ToString(), "',", reader["ItemType"], ",", reader["Sort"], "];\n" }));
                        num++;
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 3:
                    reader.Dispose();
                    return builder.ToString();
            }
            goto Label_001B;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            long uID = 0;
            long sID = 0;
            int pID = 0;
            //new loginClass().checkLogin(out uID, "0");
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            sID = Convert.ToInt64(base.Request.QueryString["SID"]);
            pID = Convert.ToInt32(base.Request.QueryString["PID"]);
            this.sClientJs = this.sClientJs + this.getPageItem( uID, sID, pID);
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
