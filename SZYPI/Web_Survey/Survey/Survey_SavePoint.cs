using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SavePoint : Page, IRequiresSessionState
    {
        public long IID;
        public long SID;
        public string sItemHTML = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            string str2;
            string[] strArray = null; //赋初值
            long num = 0; //赋初值
            string str3;
            string str4;
            short num2 = 0; //赋初值
            int num3;
            goto Label_004F;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num2 < strArray.Length)
                    {
                        num3 = 10;
                    }
                    else
                    {
                        num3 = 0x10;
                    }
                    goto Label_0002;

                case 1:
                    return;

                case 2:
                    if (str2 == null)
                    {
                        return;
                    }
                    num3 = 3;
                    goto Label_0002;

                case 3:
                    num3 = 13;
                    goto Label_0002;

                case 4:
                    goto Label_026E;

                case 5:
                    goto Label_01DA;

                case 6:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_021C;
                    }
                    return;

                case 7:
                    str3 = strArray[num2].Substring(strArray[num2].LastIndexOf(':') + 1, (strArray[num2].Length - strArray[num2].LastIndexOf(':')) - 1);
                    str4 = strArray[num2].Substring(0, strArray[num2].LastIndexOf(':'));
                    //command.CommandText = " UPDATE OptionTable SET Point='" + str3 + "' WHERE OID=" + str4 + " AND UID=" + num.ToString() + " AND SID=" + this.SID.ToString() + " AND IID=" + this.IID.ToString();
                    new Survey_SetPoint_Layer().UpdateOptionTable(str3, str4, num.ToString(), this.SID.ToString(), this.IID.ToString());
                    num3 = 4;
                    goto Label_0002;

                case 8:
                    num3 = 11;
                    goto Label_0002;

                case 9:
                    goto Label_021C;

                case 10:
                    if (!(strArray[num2] != ""))
                    {
                        goto Label_026E;
                    }
                    num3 = 7;
                    goto Label_0002;

                case 11:
                    if (this.IID != 0)
                    {
                        goto Label_01DA;
                    }
                    num3 = 14;
                    goto Label_0002;

                case 12:
                    return;

                case 13:
                    if (str2.Length > 2)
                    {
                        strArray = str2.Substring(0, str2.Length - 1).Split(new char[] { ';' });
                        this.IID = Convert.ToInt64(base.Request.Form["IID"]);
                        this.SID = Convert.ToInt64(base.Request.Form["SID"]);
                        num3 = 15;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0002;

                case 14:
                    goto Label_029F;

                case 15:
                    if (this.SID == 0)
                    {
                        goto Label_029F;
                    }
                    num3 = 8;
                    goto Label_0002;

                case 0x10:
                    num3 = 12;
                    goto Label_0002;
            }
        Label_004F:
            str2 = Convert.ToString(base.Request.Form["OptionPointStr"]);
            num = 0;

            num = ConvertHelper.ConvertLong(this.Session["UserID"]);
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "setpointstat.aspx", 2, "没有权限", "");
            str3 = "";
            num3 = 2;
            goto Label_0002;
        Label_01DA:
            str4 = "";
            num2 = 0;
            num3 = 6;
            goto Label_0002;
        Label_021C:
            num3 = 0;
            goto Label_0002;
        Label_026E:
            num2 = (short)(num2 + 1);
            num3 = 9;
            goto Label_0002;
        Label_029F:
            base.Response.Write("SID=" + this.SID.ToString() + "<BR>IID=" + this.IID.ToString());
            base.Response.End();
            num3 = 5;
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
