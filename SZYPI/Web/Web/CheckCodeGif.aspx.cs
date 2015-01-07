using System;
using System.Drawing;
using Business.Helper;

namespace Web.Web
{
    public partial class CheckCodeGif : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            string keyCode = PageHelper.RndNum(4);
            Session["CheckCode"] = keyCode;
            CreateCheckCodeImage(keyCode);
        }

        private void CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || Equals(checkCode.Trim(), String.Empty))
                return;
            using (var image = new Bitmap((int)Math.Ceiling((checkCode.Length * 10.5)),20))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    try
                    {
                        //生成随机生成器
                        var random = new Random();
                        //清空图片背景色
                        g.Clear(Color.White);
                        var pen = new Pen(Color.Silver);
                        //画图片的背景噪音线
                        for (int i = 0; i < 15; i++)
                        {
                            int x1 = random.Next(image.Width);
                            int x2 = random.Next(image.Width);
                            int y1 = random.Next(image.Height);
                            int y2 = random.Next(image.Height);

                            g.DrawLine(pen, x1, y1, x2, y2);
                        }

                        var font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                        using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true))
                        {
                            g.DrawString(checkCode, font, brush, 2, 2);
                        }

                        //画图片的前景噪音点
                        for (int i = 0; i < 40; i++)
                        {
                            int x = random.Next(image.Width);
                            int y = random.Next(image.Height);

                            image.SetPixel(x, y, Color.FromArgb(random.Next()));
                        }

                        //画图片的边框线
                        //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                        using (var ms = new System.IO.MemoryStream())
                        {
                            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                            Response.ClearContent();
                            Response.ContentType = "image/Gif";
                            Response.BinaryWrite(ms.ToArray());
                        }
                    }
                    finally
                    {
                        g.Dispose();
                        image.Dispose();
                    }
                }
            }
        }

    }
}