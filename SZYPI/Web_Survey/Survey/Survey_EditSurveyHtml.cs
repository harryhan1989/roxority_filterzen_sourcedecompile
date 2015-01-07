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
    public class Survey_EditSurveyHtml : Page, IRequiresSessionState
    {
        public int PID;
        public string sClientJs = "var blnEditHTML = false;";
        public long SID;
        public static string sPageStyle = "";
        public string sPath = "";
        public string sStyle = "";

        protected string getPageStyle(SqlDataReader dr)
        {
            string str;
        Label_001B:
            str = "var arrPage = new Array();\n";
            int num = 0;
            //objComm.CommandText = "SELECT P_ID,PageFileName,StyleName,PageType,PageImage FROM  PageStyle WHERE PageType=0 ORDER BY ID DESC";
            dr = new Survey_EditSurveyHtml_Layer().GetPageStyle("0");
            if ((1 != 0) && (0 != 0))
            {
            }
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 1:
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    if (dr.Read())
                    {
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, "arrPage[", num.ToString(), "] = [", dr["P_ID"], ",'", dr["PageFileName"], "','", dr["StyleName"], "',", dr["PageType"], ",'']\n" });
                        num++;
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 3:
                    dr.Close();
                    return str;
            }
            goto Label_001B;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            SqlDataReader reader = null; //¸³³õÖµ
            int num2;
            goto Label_002F;
        Label_0002:
            switch (num2)
            {
                case 0:
                    goto Label_0288;

                case 1:
                    goto Label_00AB;

                case 2:
                    if (!(sPageStyle == ""))
                    {
                        goto Label_02F5;
                    }
                    num2 = 4;
                    goto Label_0002;

                case 3:
                    base.Response.End();
                    num2 = 1;
                    goto Label_0002;

                case 4:
                    sPageStyle = this.getPageStyle(reader);
                    num2 = 8;
                    goto Label_0002;

                case 5:
                    this.sClientJs = this.sClientJs + "blnEditHTML=true;";
                    num2 = 0;
                    goto Label_0002;

                case 6:
                    if (this.SID > 0)
                    {
                        goto Label_00AB;
                    }
                    num2 = 3;
                    goto Label_0002;

                case 7:
                    if (Convert.ToString(this.Session["Limits3"]).IndexOf("HTMLEdit") < 0)
                    {
                        goto Label_0288;
                    }
                    num2 = 5;
                    goto Label_0002;

                case 8:
                    goto Label_02F5;
            }
        Label_002F:
            long uID;
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num2 = 7;
            goto Label_0002;
        Label_00AB:
            this.sClientJs = this.sClientJs + "var SID=" + this.SID.ToString() + "\n";
            this.sClientJs = this.sClientJs + "var sRoot='u" + uID.ToString() + "'\n";

            //this.sPath = base.Server.MapPath("UserData/U" + uID.ToString());
            this.sPath = base.Server.MapPath("UserData/U" + 1);

            this.sPath = "http://" + base.Request.ServerVariables["HTTP_HOST"] + base.Request.ServerVariables["URL"];
            //this.sPath = this.sPath.Substring(0, this.sPath.LastIndexOf("/") + 1) + "UserData/U" + uID.ToString() + "/";
            this.sPath = this.sPath.Substring(0, this.sPath.LastIndexOf("/") + 1) + "UserData/U" + 1 + "/";

            this.sClientJs = this.sClientJs + "var sPath='" + this.sPath + "';\n";
            this.sPath = this.sPath + "s" + this.SID.ToString() + ".aspx";
            reader = null;
            num2 = 2;
            goto Label_0002;
        Label_0288:
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            this.PID = Convert.ToInt32(base.Request.QueryString["PID"]);
            num2 = 6;
            goto Label_0002;
        Label_02F5:
            this.sClientJs = this.sClientJs + sPageStyle;
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
