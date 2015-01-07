using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;
using System.Data;

namespace BLL
{
    public class CodeQuery
    {
        public DataTable GetCode()
        {
            string sql = "SELECT CodeID,CodeName,CodeValue FROM UT_SYS_Code WHERE IsDeleted = 0 ORDER BY SortOrder";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// ���ݴ�����ID��ô�����Ϣ
        /// </summary>
        /// <param name="CodeGroupID"></param>
        /// <returns></returns>
        public DataTable GetAllCode(long CodeGroupID)
        {
            string sql = "SELECT * FROM UT_SYS_Code WHERE IsDeleted = 0 AND CodeGroupID = " + CodeGroupID + " ORDER BY CodeValue";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }
        
        /// <summary>
        /// ���ݴ������ֵ��ô�����Ϣ
        /// </summary>
        /// <param name="CodeGroupKey"></param>
        /// <returns></returns>
        public DataTable GetAllCode(string CodeGroupKey)
        {
            string sql = "SELECT * FROM UT_SYS_Code WHERE IsDeleted = 0 AND CodeGroupID in (SELECT CodeGroupID FROM UT_SYS_CodeGroup WHERE CodeGroupKey = '" + CodeGroupKey + "') ORDER BY CodeValue";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// ���ݴ������ֵ��ô�����Ϣ
        /// ��CodeValueֵΪ����ʱ�ɵ��ô˺���
        /// </summary>
        /// <param name="CodeGroupKey"></param>
        /// <returns></returns>
        public DataTable GetAllCode2(string CodeGroupKey)
        {
            string sql = "SELECT * FROM UT_SYS_Code WHERE IsDeleted = 0 AND CodeGroupID in (SELECT CodeGroupID FROM UT_SYS_CodeGroup WHERE CodeGroupKey = '" + CodeGroupKey + "') ORDER BY CAST(CodeValue AS INT) ";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// ����������������Ƿ������ͬ����
        /// </summary>
        /// <param name="CodeName"></param>
        /// <param name="CodeID"></param>
        /// <param name="CodeGroupID"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public static bool CheckCodeName(string CodeName, long CodeID, long CodeGroupID, int Operation)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_Code WHERE IsDeleted = 0 AND CodeGroupID = " + CodeGroupID + " AND CodeName = '" + CodeName + "'";
            if (Operation == 2) 
            {
                sql += " AND CodeID <> " + CodeID;
            }

            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// �����������ֵ�Ƿ������ͬ����
        /// </summary>
        /// <param name="CodeValue"></param>
        /// <param name="CodeID"></param>
        /// <param name="CodeGroupID"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public static bool CheckCodeValue(string CodeValue, long CodeID, long CodeGroupID, int Operation)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_Code WHERE IsDeleted = 0 AND CodeGroupID = " + CodeGroupID + " AND CodeValue = '" + CodeValue + "'";
            if (Operation == 2)
            {
                sql += " AND CodeID <> " + CodeID;
            }

            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ���ݴ������ֵ�Լ�����ֵ��ô�������
        /// </summary>
        /// <param name="CodeGroupKey"></param>
        /// <param name="CodeValue"></param>
        /// <returns></returns>
        public static string GetCodeName(string CodeGroupKey, string CodeValue)
        {
            string sql = "SELECT CodeName FROM UT_SYS_Code " +
                               "WHERE IsDeleted = 0 " +
                               "AND CodeGroupID IN (SELECT CodeGroupID FROM UT_SYS_CodeGroup WHERE IsDeleted = 0 AND CodeGroupKey = '" + CodeGroupKey + "') " +
                               "AND CodeValue = '" + CodeValue + "' ";
            DataTable dt = NDDBAccess.Fill(sql);

            string CodeName = "";
            if (dt.Rows.Count > 0)
            {
                CodeName = dt.Rows[0]["CodeName"].ToString();
            }
            return CodeName;
        }

        /// <summary>
        /// ���ݴ������ֵ�Լ�����ֵ��ô��뱸ע
        /// </summary>
        /// <param name="CodeGroupKey"></param>
        /// <param name="CodeValue"></param>
        /// <returns></returns>
        public static string GetCodeMemo(string CodeGroupKey, string CodeValue)
        {
            string sql = "SELECT Memo FROM UT_SYS_Code " +
                         "WHERE IsDeleted = 0 " +
                         "AND CodeGroupID IN (SELECT CodeGroupID FROM UT_SYS_CodeGroup WHERE IsDeleted = 0 AND CodeGroupKey = '" + CodeGroupKey + "') " +
                         "AND CodeValue = '" + CodeValue + "' ";
            DataTable dt = NDDBAccess.Fill(sql);

            string Memo = "";
            if (dt.Rows.Count > 0)
            {
                Memo = dt.Rows[0]["Memo"].ToString().Trim();
            }
            return Memo;
        }

        /// <summary>
        /// �ж�ĳ�������Ƿ���ڴ�����Ϣ
        /// </summary>
        /// <param name="CodeGroupID"></param>
        /// <returns></returns>
        public static bool IsExistCode(long CodeGroupID)
        {
            string sql = "SELECT count(*) FROM UT_SYS_Code WHERE IsDeleted = 0 AND CodeGroupID = " + CodeGroupID;
            DataTable dt = NDDBAccess.Fill(sql);

            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
