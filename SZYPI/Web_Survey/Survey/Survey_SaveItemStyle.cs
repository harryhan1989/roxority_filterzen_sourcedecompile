using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SaveItemStyle : Page, IRequiresSessionState
    {
        public long IID;
        public string sError = "";
        public string sItemHTML = "";

        protected void Page_Error(object sender, EventArgs e)
        {
        Label_0017:
            base.Server.GetLastError();
            int num = 2;
        Label_0002:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    HttpContext.Current.Response.Write("请输入合法的字符串【<a href=\"javascript:history.back(0);\">返回</a>】");
                    HttpContext.Current.Server.ClearError();
                    num = 0;
                    goto Label_0002;

                case 2:
                    if (!(HttpContext.Current.Server.GetLastError() is HttpRequestValidationException))
                    {
                        break;
                    }
                    num = 1;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
            string str;
            string str2;
            long num2;
            int num3;
            goto Label_001F;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (!Regex.IsMatch(this.sItemHTML, str, RegexOptions.IgnoreCase))
                    {
                        //command.CommandText = "UPDATE ItemTable SET ItemHTML = '" + this.sItemHTML + "' WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString() + " AND IID=" + this.IID.ToString();
                        new Survey_GetItem_Layer().UpdateItemTable(this.sItemHTML, num.ToString(), num2.ToString(), this.IID.ToString());
                        return;
                    }
                    num3 = 1;
                    goto Label_0002;

                case 1:
                    this.sError = Regex.IsMatch(this.sItemHTML, str, RegexOptions.IgnoreCase).ToString();
                    return;

                case 2:
                    if (!((num == 0) | (this.IID == 0)))
                    {
                        goto Label_010E;
                    }
                    num3 = 3;
                    goto Label_0002;

                case 3:
                    base.Response.End();
                    num3 = 4;
                    goto Label_0002;

                case 4:
                    goto Label_010E;
            }
        Label_001F:
            num = 0;
            str = "<applet>|<body>|<embed>|<frame>|<script>|<frameset>|<html>|<iframe>|<style>|<layer>|<link>|<ilayer>|<meta>|<object>";
            num2 = 0;
            //new loginClass().checkLogin(out num2, "0");
            num2 = ConvertHelper.ConvertLong(this.Session["UserID"]);
            this.IID = Convert.ToInt64(base.Request.Form["IID"]);
            num = Convert.ToInt64(base.Request.Form["SID"]);
            this.sItemHTML = Convert.ToString(base.Request.Form["Memo"]);
            num3 = 2;
            goto Label_0002;
        Label_010E:
            num3 = 0;
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
