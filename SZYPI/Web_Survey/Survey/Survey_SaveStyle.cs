using LoginClass;
using shareclass;
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
    public class Survey_SaveStyle : Page, IRequiresSessionState
    {
        public string sMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            long num;
            long num2;
            string str2;
            string str3;
            int num3 = 0; //赋初值
            int num4;
            goto Label_003B;
        Label_0002:
            switch (num4)
            {
                case 0:
                    base.Response.End();
                    num4 = 1;
                    goto Label_0002;

                case 1:
                    goto Label_0203;

                case 2:
                    //command.CommandText = "INSERT INTO SurveyExpand(SID,UID,ExpandContent,ExpandType) VALUES(" + num.ToString() + "," + num2.ToString() + ",'" + str3 + "',9)";
                    new Survey_SetStyle_Layer().InsertSurveyExpand(num.ToString(), num2.ToString(), str3,"9");
                    num4 = 10;
                    goto Label_0002;

                case 3:
                    if (num3 != 0)
                    {
                        goto Label_02CD;
                    }
                    num4 = 2;
                    goto Label_0002;

                case 4:
                    if (!(str2 != ""))
                    {
                        goto Label_0314;
                    }
                    num4 = 9;
                    goto Label_0002;

                case 5:
                    if (str2 != null)
                    {
                        goto Label_016B;
                    }
                    num4 = 6;
                    goto Label_0002;

                case 6:
                    str2 = "";
                    num4 = 11;
                    goto Label_0002;

                case 7:
                    if (num != 0)
                    {
                        goto Label_0203;
                    }
                    num4 = 0;
                    goto Label_0002;

                case 8:
                    goto Label_0314;

                case 9:
                    {
                        NotClass class3 = new NotClass(num, num2);
                        //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "rs:newstyle", 2, "样式入库:没有权限", "Limits.aspx");
                        this.sMessage = class3.saveStyleToLib(num2, str2, str3);
                        num4 = 8;
                        goto Label_0002;
                    }
                case 10:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_02CD;
                    }
                    goto Label_016B;

                case 11:
                    goto Label_016B;
            }
        Label_003B:
            num = 0;
            num2 = 0;
            str2 = "";
            str3 = "";
            num2 = ConvertHelper.ConvertLong(this.Session["UserID"]);
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "setstyle.aspx", 2, "定义全局样式:没有权限", "Limits.aspx");
            num = Convert.ToInt32(base.Request.Form["sid"]);
            num4 = 7;
            goto Label_0002;
        Label_016B:
            //command = new OleDbCommand("UPDATE SurveyExpand SET ExpandContent='" + str3 + "' WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString() + " AND ExpandType=9", connection);
            num3 = new Survey_SetStyle_Layer().UpdateSurveyExpand(str3, num.ToString(), num2.ToString());
            num4 = 3;
            goto Label_0002;
        Label_0203:
            str3 = base.Request.Form["Result"];
            str2 = base.Request.Form["StyleLibName"];
            num4 = 5;
            goto Label_0002;
        Label_02CD:
            num4 = 4;
            goto Label_0002;
        Label_0314:
            return;
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

