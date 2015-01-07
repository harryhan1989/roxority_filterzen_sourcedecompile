using System.Text.RegularExpressions;
using System.Text;

namespace WebManage.Common
{
    public class PageHelper:System.Web.UI.Page
    {
        /// <summary>
        /// TODO: 提取HTML中的纯文本
        /// </summary>
        /// <param name="strHtml">包含HTML的数据内容</param>
        /// <returns>去除HTML的数据内容</returns>
        public static string StripHTML(string strHtml)
        {
            string[] aryReg ={   
                      @"<script[^>]*?>.*?</script>",   
    
                      @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",   
                      @"([\r\n])[\s]+",   
                      @"&(quot|#34);",   
                      @"&(amp|#38);",   
                      @"&(lt|#60);",   
                      @"&(gt|#62);",     
                      @"&(nbsp|#160);",     
                      @"&(iexcl|#161);",   
                      @"&(cent|#162);",   
                      @"&(pound|#163);",   
                      @"&(copy|#169);",   
                      @"&#(\d+);",   
                      @"-->",   
                      @"<!--.*\n",
                    };

            string[] aryRep =   {   
                        "",   
                        "",   
                        "",   
                        "\"",   
                        "&",   
                        "<",   
                        ">",   
                        "   ",   
                        "\xa1",  //chr(161),   
                        "\xa2",  //chr(162),   
                        "\xa3",  //chr(163),   
                        "\xa9",  //chr(169),   
                        "",   
                        "\r\n",   
                        ""   
                      };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            return strOutput;
        }

        /// <summary>
        /// 验证字符串中是否包含特殊字符
        /// </summary>
        /// <param name="AuthStr"></param>
        /// <returns></returns>
        public static bool CheckString(string value)
        {
            const string str = @"~!%^&*();'\?><[]{}\\|:/=+—“”‘,";

            for (int i = 0; i < value.Length; i++)
            {
                if (str.IndexOf(value.Substring(i, 1)) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 验证字符串中是否包含特殊字符
        /// </summary>
        /// <param name="AuthStr"></param>
        /// <returns></returns>
        public static bool CheckIsDate(string value)
        {
            bool IsNumeric = Regex.IsMatch(value, @"^[-]?\d+[.]?\d*$");
            if (IsNumeric)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证是否是电话
        /// </summary>
        /// <param name="strPhone"></param>
        /// <returns></returns>
        public static bool CheckIsPhone(string strPhone)
        {
            bool IsPhone = Regex.IsMatch(strPhone, @"(\(\d{3}\)|\d{3}-)?\d{8}");

            if (IsPhone)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="info">要提示的消息</param>
        public void Alert(string info)
        {
            StringBuilder jsStr = new StringBuilder();
            jsStr.Append("<script language='javascript'>");
            jsStr.Append("alert('" + info + "');\r\n");
            jsStr.Append("</script>");


            if (!Page.ClientScript.IsClientScriptBlockRegistered("Alert"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", jsStr.ToString());
            }

        }

        /// <summary>
        /// 提示消息并跳转到指定页面
        /// </summary>
        /// <param name="info">要提示的消息</param>
        /// <param name="pageUrl">指定页面的URL</param>
        public void Alert(string info, string pageUrl)
        {
            StringBuilder jsStr = new StringBuilder();
            jsStr.Append("<script language='javascript'>");
            jsStr.Append("alert('" + info + "');\r\n");
            jsStr.Append(";window.location.href='" + pageUrl + "'");
            jsStr.Append("</script>");

            if (!Page.ClientScript.IsClientScriptBlockRegistered("Alert"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "Alert", jsStr.ToString());
            }
        }

        #region 截取标题长度
        /// <summary>
        /// 截取标题长度
        /// </summary>
        /// <param name="strInfo"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string SelectInfo(string strInfo, int length)
        {
            string selectInfo = string.Empty;
            if (strInfo.Length > length)
            {
                selectInfo = Common.PageHelper.StripHTML(strInfo).Substring(0, length).ToString().Trim();
            }
            else
            {
                selectInfo = Common.PageHelper.StripHTML(strInfo).ToString().Trim();
            }
            return selectInfo;
        }

        public string SelectInfoCut(string strInfo, int length)
        {
            string selectInfo = string.Empty;
            if (strInfo.Length > length)
            {
                selectInfo = Common.PageHelper.StripHTML(strInfo).Substring(0, length).ToString().Trim() + "...";
            }
            else
            {
                selectInfo = Common.PageHelper.StripHTML(strInfo).ToString().Trim();
            }
            return selectInfo;
        }
        #endregion
    }
}
