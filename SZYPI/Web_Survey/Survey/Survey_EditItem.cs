using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using WebUI;

namespace Web_Survey.Survey
{
    public class Survey_EditItem : Page, IRequiresSessionState
    {
        public string sClientJs = "";
        public string sPageList = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //int num;
        Label_001B:
            //num = 0;
            //class2.checkLogin(out num, "0");

            long sID = 0;
            int num3 = 0;
            sID = Convert.ToInt64(base.Request.QueryString["SID"]);
            num3 = Convert.ToInt32(base.Request.QueryString["PageNo"]);
            //this.sClientJs = "enCreateNum = " + class2.checkItemNum(objComm,Convert.ToString(this.Session["Limits3"]), sID) + ";";
            //objComm.CommandText = "SELECT PageNo FROM PageTable WHERE SID=" + sID + " ORDER BY PageNo";
            SqlDataReader reader = new Survey_EditItem_Layer().GetPageTable(sID);

            int num4 = 2;
        Label_0002:
            switch (num4)
            {
                case 0:
                    {
                        string sClientJs = this.sClientJs;
                        this.sClientJs = sClientJs + "intPageNo=" + num3.ToString() + ";SID=" + sID.ToString() + ";";
                        return;
                    }
                case 1:
                    if (reader.Read())
                    {
                        if (num3 != 0)
                        {
                            if (num3.ToString() == reader["PageNo"].ToString())
                            {
                                this.sPageList = sPageList + "<option value='" + reader["PageNo"].ToString() + "'>第" + reader["PageNo"].ToString() + "页</option>";
                            }
                            else
                            {
                                this.sPageList = sPageList + "<option value='" + reader["PageNo"].ToString() + "'>第" + reader["PageNo"].ToString() + "页</option>";
                            }
                        }
                        else
                        {
                            this.sPageList = sPageList + "<option value='" + reader["PageNo"].ToString() + "'>第" + reader["PageNo"].ToString() + "页</option>";
                        }
                        num4 = 3;
                    }
                    else
                    {
                        num4 = 0;
                    }
                    goto Label_0002;

                case 2:
                case 3:
                    num4 = 1;
                    goto Label_0002;
            }
            goto Label_001B;
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

