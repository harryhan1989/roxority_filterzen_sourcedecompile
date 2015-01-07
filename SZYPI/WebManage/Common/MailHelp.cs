using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace WebUI
{
    public class MailHelp
    {
        /// <summary>
        /// ��������ļ�����ֵ(����)
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
                throw new Exception("δ�ҵ�" + Key + "������Ϣ,������ֵ����");
            }
        }

        /// <summary>
        /// ��������ļ�����ֵ(�ַ���)
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
                throw new Exception("δ�ҵ�" + Key + "������Ϣ,������ֵ����");
            }
        }

    }
}
