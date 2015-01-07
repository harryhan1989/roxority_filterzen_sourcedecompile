using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nandasoft;

namespace BLL
{
    public class SystemParamQuery
    {
        /// <summary>
        /// �Ƿ���Ҫ��װSessionState
        /// </summary>
        /// <returns></returns>
        public static bool GetInstallSessionState()
        {
            string sql = "SELECT * FROM sysobjects WHERE name = 'ASPStateTempSessions' OR name = 'ASPStateTempApplications'";
            DataTable dt = NDDBAccess.Fill(sql);
            if (dt.Rows.Count == 2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ���ݼ�ֵ��ȡ���ò���ֵ
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetSystemParam(string Key)
        {
            string sql = "SELECT ParamValue FROM UT_SYS_SystemParam WHERE  EnglishName = '" + Key + "'";
            DataTable dt = NDDBAccess.Fill(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }

        /// <summary>
        /// ����������ò���
        /// </summary>
        /// <returns></returns>
        public DataTable GetGardenSystemParam()
        {
            string sql = "SELECT * FROM UT_SYS_SystemParam ORDER BY ParamName";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }
    }
}
