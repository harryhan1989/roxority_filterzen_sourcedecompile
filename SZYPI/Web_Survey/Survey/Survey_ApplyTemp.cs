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

//对函数变量赋初值
namespace Web_Survey.Survey
{
    public class Survey_ApplyTemp : Page, IRequiresSessionState
    {
        public string sClientJs = "var arrPageStyle = new Array();\n";
        public long SID;
        public static string sPageList = "";

        public string getPageStyleList()
        {
            string str;
        Label_001B:
            str = "";
            //objComm.CommandText = "SELECT StyleName,PageFileName,PageImage FROM PageStyle WHERE PageType=0 ORDER BY Sort DESC";
            SqlDataReader reader = new Survey_ApplyTemp_Layer().GetPageStyle();
            int num = 0;
            int num2 = 2;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 2:
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    if (reader.Read())
                    {
                        string str2 = str;
                        str = str2 + "arrPageStyle[" + num.ToString() + "] = ['" + reader["StyleName"].ToString() + "','" + reader["PageFileName"].ToString() + "','" + reader["PageImage"].ToString() + "'];\n";
                        num++;
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 3:
                    reader.Close();
                    return str;
            }
            goto Label_001B;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
            SqlDataReader reader = null; //赋初值
            int num2;
            goto Label_0027;
        Label_0002:
            switch (num2)
            {
                case 0:
                    sPageList = this.getPageStyleList();
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    goto Label_0135;

                case 2:
                    this.sClientJs = this.sClientJs + "var sCurrPageStyle='" + reader[0].ToString() + "';\n";
                    num2 = 4;
                    goto Label_0002;

                case 3:
                    if (!reader.Read())
                    {
                        this.sClientJs = this.sClientJs + "var sCurrPageStyle='';\n";
                        num2 = 6;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 4:
                case 6:

                    return;

                case 5:
                    if (!(sPageList == ""))
                    {
                        goto Label_0135;
                    }
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num2 = 0;
                    goto Label_0002;
            }
        Label_0027:
            num = 0;
            num = ConvertHelper.ConvertLong(this.Session["UserID"]);
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            this.sClientJs = this.sClientJs + "var SID=" + this.SID.ToString() + ";\n";
            num2 = 5;
            goto Label_0002;
        Label_0135:
            this.sClientJs = this.sClientJs + sPageList;
            //command.CommandText = "SELECT TOP 1 TempPage FROM SurveyTable WHERE SID=" + this.SID.ToString() + " AND UID=" + num.ToString();
            reader = new Survey_ApplyTemp_Layer().GetSurveyTable(this.SID.ToString(), num.ToString());
            num2 = 3;
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

