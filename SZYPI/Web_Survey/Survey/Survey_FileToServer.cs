
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_FileToServer : Page, IRequiresSessionState
    {
        protected Button BT;
        protected FileUpload f0;
        protected FileUpload f1;
        protected FileUpload f2;
        protected FileUpload f3;
        protected FileUpload f4;
        protected HtmlForm form1;
        protected Label Label1;
        public long UID;

        public void Button1_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = null; //赋初值
            string str;
            string str2;
            string str3;
            string str4;
            short num;
            int num2;
            goto Label_0043;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (num < 5)
                    {
                        file = base.Request.Files[num];
                        str2 = file.FileName.ToLower();
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 8;
                    }
                    goto Label_0002;

                case 1:
                    goto Label_0166;

                case 2:
                    if ((1 == 0) || (0 == 0))
                    {
                        string filename = base.Server.MapPath("FileToServer.aspx");
                        filename = filename.Substring(0, filename.LastIndexOf(@"\")) + @"\UserData\U" + this.UID.ToString() + @"\" + str2;
                        file.SaveAs(filename);
                        object obj2 = str4;
                        str4 = string.Concat(new object[] { obj2, str2, "[", file.ContentLength / 0x3e8, "kb]<BR>" });
                        num2 = 5;
                        goto Label_0002;
                    }
                    goto Label_0166;

                case 3:
                    if (str2 == "")
                    {
                        goto Label_02BF;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 4:
                    str = str2.Substring(str2.LastIndexOf('.') + 1, (str2.Length - str2.LastIndexOf('.')) - 1);
                    str2 = str2.Substring(str2.LastIndexOf('\\') + 1, (str2.Length - str2.LastIndexOf('\\')) - 1);
                    num2 = 11;
                    goto Label_0002;

                case 5:
                    goto Label_020C;

                case 6:
                case 9:
                    num2 = 0;
                    goto Label_0002;

                case 7:
                    if (str2.Length <= 4)
                    {
                        goto Label_02BF;
                    }
                    num2 = 4;
                    goto Label_0002;

                case 8:
                case 13:
                    goto Label_02BF;

                case 10:
                    if (file.ContentLength <= 0)
                    {
                        goto Label_020C;
                    }
                    num2 = 2;
                    goto Label_0002;

                case 11:
                    if (str3.IndexOf(str) > 0)
                    {
                        num2 = 10;
                    }
                    else
                    {
                        num2 = 12;
                    }
                    goto Label_0002;

                case 12:
                    str4 = str4 + "上传的文件中有不允许的文件类型:" + str + "<BR>";
                    num2 = 13;
                    goto Label_0002;
            }
        Label_0043:
            str = "";
            str2 = "";
            str3 = ".jpg.jpeg.gif.png.pdf.doc.ppt.rar.zip.swf.avi.rm.rmvb.mp3.mid.wma.wmv.bmp.txt.zip.mpg.mpeg";
            str4 = "上传结果:<BR>";
            num = 0;
            num2 = 9;
            goto Label_0002;
        Label_0166:
            num2 = 7;
            goto Label_0002;
        Label_020C:
            num = (short)(num + 1);
            num2 = 6;
            goto Label_0002;
        Label_02BF:
            this.Label1.Text = str4;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "filemanage.aspx", 2, "没有权限", "");
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
