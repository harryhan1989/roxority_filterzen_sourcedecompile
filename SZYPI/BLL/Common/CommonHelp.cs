using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Web;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using Nandasoft;
using System.Text.RegularExpressions;

namespace BLL
{
    public class CommonHelp
    {
        /// <summary>
        /// 执行2005数据库分页存储过程
        /// </summary>
        /// <param name="sqlStr">执行的SQL语句，开头不能包含 SELECT</param>
        /// <param name="orderStr">排序字段(示例：field1 DESC,filed2 ASC)ASC可省略</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize"></param>
        /// <param name="All"></param>
        /// <returns></returns>
        public static DataSet DataPagination(string sqlStr, string orderStr, int pageIndex, int pageSize)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
            paramList.Add(new System.Data.SqlClient.SqlParameter("@sqlStr", sqlStr));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@orderStr", orderStr));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@pageIndex", pageIndex));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@pageSize", pageSize));
            DataSet ds = new DataSet();
            NDDBAccess.Fill(ds, "UP_Comm_Qry_DataPagination", paramList, CommandType.StoredProcedure);
            return ds;
        }

        /// <summary>
        /// 显示下拉框选定值的公用方法
        /// </summary>
        /// <param name="drp"></param>
        /// <param name="drpCurValue"></param>
        public static void BinddrpCurValue(DropDownList drp, string drpCurValue)
        {
            for (int i = 0; i < drp.Items.Count; i++)
            {
                if (drp.Items[i].Value.ToLower() == drpCurValue.ToLower())
                {
                    drp.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 显示单选框选定值的公用方法
        /// </summary>
        /// <param name="rdo"></param>
        /// <param name="rdoCurValue"></param>
        public static void BindrdoCurValue(RadioButtonList rdo, string rdoCurValue)
        {
            for (int i = 0; i < rdo.Items.Count; i++)
            {
                if (rdo.Items[i].Value.ToLower() == rdoCurValue.ToLower())
                {
                    rdo.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 显示下拉框选定值的公用方法
        /// </summary>
        /// <param name="chklst"></param>
        /// <param name="rdoCurValue"></param>
        public static void BindchklstCurValue(CheckBoxList chklst, string rdoCurValue)
        {
            if (string.IsNullOrEmpty(rdoCurValue))
            {
                return;
            }
            string[] Value = rdoCurValue.Split(',');

            for (int j = 0; j < Value.Length; j++)
            {
                for (int i = 0; i < chklst.Items.Count; i++)
                {
                    if (chklst.Items[i].Value == Value[j])
                    {
                        chklst.Items[i].Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// 获得配置文件配置值(整型)
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static int GetConfigValueInt(string Key)
        {
            try
            {
                string Value = ConfigurationManager.AppSettings[Key];
                return NDConvert.ToInt32(Value);
            }
            catch
            {
                throw new Exception("未找到" + Key + "配置信息,或配置值错误！");
            }
        }

        /// <summary>
        /// 获得配置文件配置值(字符型)
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetConfigValueStr(string Key)
        {
            try
            {
                string Value = ConfigurationManager.AppSettings[Key];
                return Value;
            }
            catch
            {
                throw new Exception("未找到" + Key + "配置信息,或配置值错误！");
            }
        }

        /// <summary>
        /// 验证字符串中是否包含特殊字符
        /// </summary>
        /// <param name="AuthStr"></param>
        /// <returns></returns>
        public static bool CheckString(string value)
        {
            const string STR = @"~!%^&*();'\?><[]{}\\|:/=+—“”‘,";

            for (int i = 0; i < value.Length; i++)
            {
                if (STR.IndexOf(value.Substring(i, 1)) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否是链接格式
        /// </summary>
        /// <param name="strHttp"></param>
        /// <returns></returns>
        public static bool CheckHttp(string strHttp)
        {
            bool IsHttp = Regex.IsMatch(strHttp, @"^((http|ftp)://)?(((([\d]+\.)+){3}[\d]+(/[\w./]+)?)|([a-z]\w*((\.\w+)+){2,})([/][\w.~]*)*)\.gif$");

            if (IsHttp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得列合计数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static int StatColumn(DataTable dt, string ColumnName)
        {
            int Sum = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Sum += NDConvert.ToInt32(dt.Rows[i][ColumnName].ToString());
            }
            return Sum;
        }

        /// <summary>
        /// 获得图片byte[]
        /// </summary>
        /// <returns></returns>
        public static byte[] GetImageByte(Stream fileStream)
        {
            Byte[] byteFile = new Byte[fileStream.Length];
            fileStream.Read(byteFile, 0, byteFile.Length);
            fileStream.Close();
            return byteFile;
        }

        /// <summary>
        /// 电话号码检测
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool PhoneNumberCheck(string str)
        {
            Regex regex1 = new Regex("13[4-9][0-9]{8}|15[0-9][0-9]{8}");
            Regex regex2 = new Regex("13[0-3][0-9]{8}");
            Regex regex3 = new Regex("0[0-9]{9,11}");
            if (regex1.Match(str).Success == true || regex2.Match(str).Success == true || regex3.Match(str).Success == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据短信内容长度确定短信发送的次数
        /// </summary>
        /// <param name="NumberList"></param>
        /// <param name="NoteContent"></param>
        /// <returns></returns>
        public static int GetSendQuantity(string NumberList, string NoteContent)
        {
            Regex regex1 = new Regex("13[4-9][0-9]{8}|15[8-9][0-9]{8}"); //移动
            Regex regex2 = new Regex("13[0-3][0-9]{8}"); //联通
            Regex regex3 = new Regex("0[0-9]{9,11}"); //小灵通

            int i = 0;
            string[] s;
            s = NumberList.Split(';');
            if (s.Length > 0)
            {
                foreach (string str in s)
                {
                    if (str != "")
                    {
                        if (NoteContent != "")
                        {
                            if (regex1.Match(str).Success == true)
                            {
                                i += (int)(System.Text.Encoding.Default.GetBytes(NoteContent).Length / 140) + 1;
                            }
                            else if (regex2.Match(str).Success == true)
                            {
                                i += (int)(System.Text.Encoding.Default.GetBytes(NoteContent).Length / 128) + 1;
                            }
                            else if (regex3.Match(str).Success == true)
                            {
                                i += (int)(System.Text.Encoding.Default.GetBytes(NoteContent).Length / 96) + 1;
                            }
                        }
                    }
                }
            }
            return i;
        }

        /// <summary>
        /// 邦定层级部门下拉框
        /// </summary>
        /// <param name="drp"></param>
        public static void BindOUList(DropDownList drpOUList)
        {
            DataTable dt = null;
            DataTable dt1 = null;
            DataTable dt2 = null;

            string Text = "";
            string Value = "";

            dt = new OUQuery().GetOU(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Text = dt.Rows[i]["OUName"].ToString();
                Value = dt.Rows[i]["OUID"].ToString();
                drpOUList.Items.Add(new ListItem(Text, Value));

                dt1 = new OUQuery().GetOU(NDConvert.ToInt64(Value));

                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    Text = "-----" + dt1.Rows[j]["OUName"].ToString();
                    Value = dt1.Rows[j]["OUID"].ToString();
                    drpOUList.Items.Add(new ListItem(Text, Value));

                    dt2 = new OUQuery().GetOU(NDConvert.ToInt64(Value));

                    for (int k = 0; k < dt2.Rows.Count; k++)
                    {
                        Text = "----------" + dt2.Rows[k]["OUName"].ToString();
                        Value = dt2.Rows[k]["OUID"].ToString();
                        drpOUList.Items.Add(new ListItem(Text, Value));
                    }
                }
            }
            drpOUList.Items.Insert(0, new ListItem("请选择", "0"));
        }

        /// <summary>
        /// 邦定层级部门下拉框（根据部门类型）
        /// </summary>
        /// <param name="drp"></param>
        public static void BindOUList(DropDownList drpOUList, int OUType)
        {
            DataTable dt = null;
            DataTable dt1 = null;
            DataTable dt2 = null;

            string Text = "";
            string Value = "";

            dt = new OUQuery().GetOU(0, OUType);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Text = dt.Rows[i]["OUName"].ToString();
                Value = dt.Rows[i]["OUID"].ToString();
                drpOUList.Items.Add(new ListItem(Text, Value));

                dt1 = new OUQuery().GetOU(NDConvert.ToInt64(Value), OUType);

                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    Text = "-----" + dt1.Rows[j]["OUName"].ToString();
                    Value = dt1.Rows[j]["OUID"].ToString();
                    drpOUList.Items.Add(new ListItem(Text, Value));

                    dt2 = new OUQuery().GetOU(NDConvert.ToInt64(Value), OUType);

                    for (int k = 0; k < dt2.Rows.Count; k++)
                    {
                        Text = "----------" + dt2.Rows[k]["OUName"].ToString();
                        Value = dt2.Rows[k]["OUID"].ToString();
                        drpOUList.Items.Add(new ListItem(Text, Value));
                    }
                }
            }
            drpOUList.Items.Insert(0, new ListItem("请选择", "0"));
        }

        /// <summary>
        /// 获得每个月最后一天的日期
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static int EndDate(int Year, int Month)
        {
            int Day = 0;
            switch (Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    Day = 31;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    Day = 30;
                    break;
                case 2:
                    if (DateTime.IsLeapYear(Year))
                    {
                        Day = 29;
                    }
                    else
                    {
                        Day = 28;
                    }
                    break;
            }
            return Day;
        }

        /// <summary>
        /// 获取远程文件源代码
        /// </summary>
        /// <param name="url">远程url</param>
        /// <returns></returns>
        public static string GetRemoteHtmlCode(string Url)
        {
            string s = "";
            MSXML2.XMLHTTP _xmlhttp = new MSXML2.XMLHTTPClass();
            _xmlhttp.open("GET", Url, false, null, null);
            _xmlhttp.send("");


            if (_xmlhttp.readyState == 4)
            {
                return _xmlhttp.responseText;
                // s = System.Text.Encoding.Default.GetString((byte[])_xmlhttp.responseBody);
            }
            return s;
        }

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
                                @"<!--.*\n" 
                               };
            string[] aryRep = { 
                                "", 
                                "", 
                                "", 
                                "\"", 
                                "&", 
                                "<", 
                                ">", 
                                " ", 
                                "\xa1",//chr(161), 
                                "\xa2",//chr(162), 
                                "\xa3",//chr(163), 
                                "\xa9",//chr(169), 
                                "", 
                                "\r\n", 
                                "" 
                               };

            string newReg = aryReg[0];
            string strOutput = strHtml;

            for (int i = 0; i < aryReg.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aryReg[i], System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            return strOutput;
        }

        

    }
}
