using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace WebUI
{
    public class MailHelp
    {
        /// <summary>
        /// 获得配置文件配置值(整型)
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static int GetConfigValueInt(string Key)
        {
            try
            {
                string Value = System.Configuration.ConfigurationManager.AppSettings[Key];
                return int.Parse(Value);
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
                string Value = System.Configuration.ConfigurationManager.AppSettings[Key];
                return Value;
            }
            catch
            {
                throw new Exception("未找到" + Key + "配置信息,或配置值错误！");
            }
        }

    }
}
