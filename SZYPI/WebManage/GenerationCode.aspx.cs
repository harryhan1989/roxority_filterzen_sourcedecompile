using System;
using System.Web;
using WebUI;
using System.Drawing;

namespace WebManage
{
    public partial class GenerationCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    Response.BufferOutput = false;                                       //特别注意
                    Response.Cache.SetExpires(DateTime.Now.AddMilliseconds(-1));        //特别注意
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);           //特别注意
                    Response.AppendHeader("Pragma", "No-Cache");                      //特别注意
                    //先产生数字串
                    string Code = this.CreateRandomCode(4);
                    Session["CheckCode"] = Code;
                    //作图
                    CreateImage(Code);
                }
                catch (Exception ex)
                {
                    PageHelper.ShowExceptionMessage(ex.Message);
                }
            }
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="codeCount"></param>
        /// <returns></returns>
        public string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                int t = rand.Next(10);
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        /// <summary>
        /// 以图片方式输出验证码
        /// </summary>
        /// <param name="checkCode"></param>
        private void CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 15);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 27);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Coral, Color.Brown, Color.DarkCyan, Color.Purple };
            //定义字体
            string[] font = { "宋体", "宋体", "宋体", "宋体", "宋体" };
            Random rand = new Random();
            //随机输出噪点
            for (int i = 0; i < 50; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }
            //输出不同字体和颜色的验证码字符
            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(5);
                Font f = new System.Drawing.Font(font[findex], 14, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                int ii = 4;
                if ((i + 1) % 2 == 0)
                {
                    ii = 2;
                }
                g.DrawString(checkCode.Substring(i, 1), f, b, 3 + (i * 12), ii);
            }
            //画一个边框
            g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);
            //输出到浏览器
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }
    }
}
