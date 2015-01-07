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
    public class Survey_SavePageContent : Page, IRequiresSessionState
    {
        public int PID;
        public string sContent = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            int num;
            int index = 0; //∏≥≥ı÷µ
        Label_0027:
            num = 0;
            long uID = 0;
            uID=ConvertHelper.ConvertLong(this.Session["UserID"]);
            string pattern = "<applet>|<body>|<embed>|<frame>|<script>|<frameset>|<html>|<iframe>|<style>|<layer>|<link>|<ilayer>|<meta>|<object>";
            num = Convert.ToInt32(base.Request.Form["SID"]);
            this.PID = Convert.ToInt32(base.Request.Form["PID"]);
            this.sContent = Convert.ToString(base.Request.Form["Memo"]);
            int num4 = 4;
        Label_0002:
            switch (num4)
            {
                case 0:
                    this.sContent = this.sContent.Substring(index + 8);
                    num4 = 1;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    if (index <= 0)
                    {
                        break;
                    }
                    num4 = 0;
                    goto Label_0002;

                case 3:
                    index = this.sContent.IndexOf("</STYLE>");
                    num4 = 2;
                    goto Label_0002;

                case 4:
                    if (!Regex.IsMatch(this.sContent, pattern, RegexOptions.IgnoreCase))
                    {
                        num4 = 6;
                    }
                    else
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num4 = 5;
                    }
                    goto Label_0002;

                case 5:
                    Regex.IsMatch(this.sContent, pattern, RegexOptions.IgnoreCase).ToString();
                    base.Response.End();
                    return;

                case 6:
                    if (!(this.sContent != ""))
                    {
                        break;
                    }
                    num4 = 3;
                    goto Label_0002;

                default:
                    goto Label_0027;
            }
 
            //command.CommandText = "UPDATE PageTable SET PageContent='" + this.sContent + "' WHERE SID=" + num.ToString() + " AND UID=" + uID.ToString() + " AND PID=" + this.PID.ToString();
            new Survey_EditPage_Layer().UpdatePageTable(this.sContent, num.ToString(), uID.ToString(), this.PID.ToString());

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
