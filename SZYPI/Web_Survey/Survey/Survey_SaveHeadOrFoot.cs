using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SaveHeadOrFoot : Page, IRequiresSessionState
    {
        public string sContent = "";
        public short t;

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
            long num2;
            bool flag;
            string str;
            int index = 0; //∏≥≥ı÷µ
            SqlDataReader reader = null; //∏≥≥ı÷µ
            int num5;
            goto Label_005B;
        Label_0002:
            switch (num5)
            {
                case 0:
                    if (!Regex.IsMatch(this.sContent, str, RegexOptions.IgnoreCase))
                    {
                        num5 = 9;
                    }
                    else
                    {
                        num5 = 1;
                    }
                    goto Label_0002;

                case 1:
                    Regex.IsMatch(this.sContent, str, RegexOptions.IgnoreCase).ToString();
                    base.Response.End();
                    return;

                case 2:
                    this.sContent = this.sContent.Substring(index + 8);
                    num5 = 0x13;
                    goto Label_0002;

                case 3:
                case 6:
                case 12:
                case 0x10:
                    //command.ExecuteNonQuery();
                    return;

                case 4:
                    if (!reader.Read())
                    {
                        goto Label_01A4;
                    }
                    num5 = 0x12;
                    goto Label_0002;

                case 5:
                    if (this.t != 0)
                    {
                        //command.CommandText = "INSERT INTO HeadFoot(PageFoot,SID,UID) VALUES('" + this.sContent + "'," + num.ToString() + "," + num2.ToString() + ")";
                        new Survey_SaveHeadOrFoot_Layer().InsertHeadFoot1(this.sContent, num.ToString(), num2.ToString());
                        num5 = 12;
                    }
                    else
                    {
                        num5 = 13;
                    }
                    goto Label_0002;

                case 7:
                    if (!flag)
                    {
                        num5 = 5;
                    }
                    else
                    {
                        num5 = 0x11;
                    }
                    goto Label_0002;

                case 8:
                    goto Label_01A4;

                case 9:
                    if (!(this.sContent != ""))
                    {
                        goto Label_046D;
                    }
                    num5 = 15;
                    goto Label_0002;

                case 10:
                    //command.CommandText = "UPDATE HeadFoot SET PageHead='" + this.sContent + "' WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString();
                    new Survey_SaveHeadOrFoot_Layer().UpdateHeadFoot(this.sContent, num.ToString(), num2.ToString());
                    num5 = 6;
                    goto Label_0002;

                case 11:
                    if (index <= 0)
                    {
                        goto Label_046D;
                    }
                    num5 = 2;
                    goto Label_0002;

                case 13:
                    //command.CommandText = "INSERT INTO HeadFoot(PageHead,SID,UID) VALUES('" + this.sContent + "'," + num.ToString() + "," + num2.ToString() + ")";
                    new Survey_SaveHeadOrFoot_Layer().InsertHeadFoot(this.sContent, num.ToString(), num2.ToString());
                    num5 = 0x10;
                    goto Label_0002;

                case 14:
                    if (this.t != 0)
                    {
                        //command.CommandText = "UPDATE HeadFoot SET PageFoot='" + this.sContent + "' WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString();
                        new Survey_SaveHeadOrFoot_Layer().UpdateHeadFoot1(this.sContent, num.ToString(), num2.ToString());
                        num5 = 3;
                    }
                    else
                    {
                        num5 = 10;
                    }
                    goto Label_0002;

                case 15:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    index = this.sContent.IndexOf("</STYLE>");
                    base.Response.Write(index + "<BR>" + this.sContent.Length.ToString());
                    num5 = 11;
                    goto Label_0002;

                case 0x11:
                    num5 = 14;
                    goto Label_0002;

                case 0x12:
                    flag = true;
                    num5 = 8;
                    goto Label_0002;

                case 0x13:
                    goto Label_046D;
            }
        Label_005B:
            num = 0;
            num2 = 0;
            num2 = ConvertHelper.ConvertLong(this.Session["UserID"]);
            flag = false;
            str = "<applet>|<body>|<embed>|<frame>|<script>|<frameset>|<html>|<style>|<iframe>|<layer>|<link>|<ilayer>|<meta>|<object>";
            num = Convert.ToInt64(base.Request.Form["SID"]);
            this.t = Convert.ToInt16(base.Request.Form["t"]);
            this.sContent = Convert.ToString(base.Request.Form["Memo"]);
            base.Response.Write(this.sContent);
            num5 = 0;
            goto Label_0002;
        Label_01A4:
            reader.Close();
            num5 = 7;
            goto Label_0002;
        Label_046D:
            //command.CommandText = "` HeadFoot WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString();
            reader = new Survey_SaveHeadOrFoot_Layer().GetHeadFoot(num.ToString(), num2.ToString());
            num5 = 4;
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
