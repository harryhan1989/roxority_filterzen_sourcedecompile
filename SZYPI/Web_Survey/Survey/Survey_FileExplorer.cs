using LoginClass;
using System;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_FileExplorer : Page, IRequiresSessionState
    {
        public string sClientJs = "";
        public long UID;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            string[] strArray = null; //赋初值
            short num = 0; //赋初值
            string[] strArray2 = null; //赋初值
            short num2 = 0; //赋初值
            int num3;
            goto Label_003F;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (str == null)
                    {
                        goto Label_00C0;
                    }
                    num3 = 5;
                    goto Label_0002;

                case 1:
                    this.sClientJs = string.Join(";", strArray2);
                    this.sClientJs = "\nsFileStr='" + this.sClientJs + "'\nvar Root='u" + this.UID.ToString() + "'\n";
                    return;

                case 2:
                    goto Label_00C0;

                case 3:
                case 11:
                    goto Label_0103;

                case 4:
                    File.Delete(base.Server.MapPath(@"userdata\u" + this.UID.ToString() + @"\" + strArray[num]));
                    num3 = 7;
                    goto Label_0002;

                case 5:
                    strArray = str.Split(new char[] { ';' });
                    num = 0;
                    num3 = 3;
                    goto Label_0002;

                case 6:
                    if (num2 < strArray2.Length)
                    {
                        strArray2[num2] = strArray2[num2].Substring(strArray2[num2].LastIndexOf('\\') + 1, (strArray2[num2].Length - strArray2[num2].LastIndexOf('\\')) - 1);
                        num2 = (short)(num2 + 1);
                        num3 = 10;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0002;

                case 7:
                    goto Label_01FF;

                case 8:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_015A;
                    }
                    goto Label_0103;

                case 9:
                    if (num < strArray.Length)
                    {
                        num3 = 12;
                    }
                    else
                    {
                        num3 = 2;
                    }
                    goto Label_0002;

                case 10:
                    goto Label_015A;

                case 12:
                    if (!(strArray[num] != ""))
                    {
                        goto Label_01FF;
                    }
                    num3 = 4;
                    goto Label_0002;
            }
        Label_003F:
            UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "filemanage.aspx", 2, "没有权限", "");
            str = Convert.ToString(base.Request.Form["SelectResult"]);
            num3 = 0;
            goto Label_0002;
        Label_00C0:
            strArray2 = Directory.GetFiles(base.Server.MapPath("userdata/u" + this.UID.ToString() + "/"));
            num2 = 0;
            num3 = 8;
            goto Label_0002;
        Label_0103:
            num3 = 9;
            goto Label_0002;
        Label_015A:
            num3 = 6;
            goto Label_0002;
        Label_01FF:
            num = (short)(num + 1);
            num3 = 11;
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
