using LoginClass;
using System;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SetPoint : Page, IRequiresSessionState
    {
        public long IID;
        public string sClientJs = "";
        public long SID;
        public string sOptionList = "";
        public long UID;

        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet set = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2;
            goto Label_002F;
        Label_0002:
            switch (num2)
            {
                case 0:
                    set.Clear();
                    return;

                case 1:
                    if (this.IID != 0)
                    {
                        goto Label_0300;
                    }
                    num2 = 4;
                    goto Label_0002;

                case 2:
                case 7:
                    num2 = 3;
                    goto Label_0002;

                case 3:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        goto Label_00B9;
                    }
                    num2 = 0;
                    goto Label_0002;

                case 4:
                    goto Label_0407;

                case 5:
                    goto Label_0300;

                case 6:
                    if ((1 == 0) || (0 == 0))
                    {
                        num2 = 1;
                        goto Label_0002;
                    }
                    goto Label_00B9;

                case 8:
                    if (this.SID == 0)
                    {
                        goto Label_0407;
                    }
                    num2 = 6;
                    goto Label_0002;
            }
        Label_002F:
            this.IID = Convert.ToInt64(base.Request.QueryString["IID"]);
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num2 = 8;
            goto Label_0002;
        Label_00B9:
            this.sClientJs = this.sClientJs + "arrOption[" + num.ToString() + "] = new Array()\n";
            string sClientJs = this.sClientJs;
            this.sClientJs = sClientJs + "arrOption[" + num.ToString() + "][0] = " + set.Tables[0].Rows[num][0].ToString() + "\n";
            string str2 = this.sClientJs;
            this.sClientJs = str2 + "arrOption[" + num.ToString() + "][1] = '" + set.Tables[0].Rows[num][1].ToString() + "'\n";
            string str3 = this.sClientJs;
            this.sClientJs = str3 + "arrOption[" + num.ToString() + "][2] = '" + set.Tables[0].Rows[num][2].ToString() + "'\n";
            string sOptionList = this.sOptionList;
            this.sOptionList = sOptionList + " <tr><td>" + set.Tables[0].Rows[num][1].ToString() + "</td><td><input type='text' id='F" + set.Tables[0].Rows[num][0].ToString() + "' value='" + set.Tables[0].Rows[num][2].ToString() + "'/></td></tr> ";
            num++;
            num2 = 2;
            goto Label_0002;
        Label_0300:
            set = new DataSet();
            //new OleDbDataAdapter("SELECT OID,OptionName,Point FROM OptionTable WHERE SID=" + this.SID.ToString() + " AND UID=" + this.UID.ToString() + " AND IID=" + this.IID.ToString(), connection).Fill(set, "OptionTable");
            DataTable OptionTable = new Survey_SetPoint_Layer().GetOptionTable(this.SID.ToString(), this.UID.ToString(), this.IID.ToString());
            OptionTable.TableName = "OptionTable";
            set.Tables.Add(OptionTable);

            num = 0;
            num2 = 7;
            goto Label_0002;
        Label_0407:
            base.Response.End();
            num2 = 5;
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
