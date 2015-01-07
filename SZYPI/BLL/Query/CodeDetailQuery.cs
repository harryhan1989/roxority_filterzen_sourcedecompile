using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;
using System.Data;

namespace BLL
{
    public class CodeDetailQuery
    {
        /// <summary>
        /// ���ݴ������ֵ����Ӵ�����Ϣ
        /// </summary>
        /// <param name="CodeGroupKey"></param>
        /// <returns></returns>
        public DataTable GetCodeDetail(string CodeValue, string CodeGroupKey)
        {
            string sql = "SELECT UT_SYS_CodeDetail.* " +
                         "FROM UT_SYS_CodeDetail INNER JOIN " +
                         "UT_SYS_Code ON UT_SYS_CodeDetail.CodeID = UT_SYS_Code.CodeID INNER JOIN " +
                         "UT_SYS_CodeGroup ON UT_SYS_Code.CodeGroupID = UT_SYS_CodeGroup.CodeGroupID " +
                         "WHERE UT_SYS_CodeDetail.IsDeleted = 0 " +
                         "AND UT_SYS_Code.IsDeleted = 0 " +
                         "AND UT_SYS_CodeGroup.IsDeleted = 0 " +
                         "AND UT_SYS_Code.CodeValue = '" + CodeValue + "' " +
                         "AND UT_SYS_CodeGroup.CodeGroupKey = '" + CodeGroupKey + "'";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// ���ݴ������ֵ����Ӵ�����Ϣ
        /// </summary>
        /// <param name="CodeGroupKey"></param>
        /// <returns></returns>
        public static string GetCodeDetailName(string CodeDetailValue, string CodeValue, string CodeGroupKey)
        {
            string sql = "SELECT UT_SYS_CodeDetail.CodeDetailName " +
                         "FROM UT_SYS_CodeDetail INNER JOIN " +
                         "UT_SYS_Code ON UT_SYS_CodeDetail.CodeID = UT_SYS_Code.CodeID INNER JOIN " +
                         "UT_SYS_CodeGroup ON UT_SYS_Code.CodeGroupID = UT_SYS_CodeGroup.CodeGroupID " +
                         "WHERE UT_SYS_CodeDetail.IsDeleted = 0 " +
                         "AND UT_SYS_Code.IsDeleted = 0 " +
                         "AND UT_SYS_CodeGroup.IsDeleted = 0 " +
                         "AND UT_SYS_Code.CodeValue = '" + CodeValue + "' " +
                         "AND UT_SYS_CodeGroup.CodeGroupKey = '" + CodeGroupKey + "' " +
                         "AND UT_SYS_CodeDetail.CodeDetailValue = '" + CodeDetailValue + "'";

            DataTable dt = NDDBAccess.Fill(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }

        /// <summary>
        /// ���ݴ���ID����Ӵ�����Ϣ
        /// </summary>
        /// <param name="CodeID"></param>
        /// <returns></returns>
        public DataTable GetCodeDetail(long CodeID)
        {
            string sql = "SELECT * FROM UT_SYS_CodeDetail WHERE IsDeleted = 0 AND CodeID = " + CodeID;
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// �ж�ĳ�������Ƿ�����Ӵ�����Ϣ
        /// </summary>
        /// <param name="CodeGroupID"></param>
        /// <returns></returns>
        public static bool IsExistCodeDetail(long CodeID)
        {
            string sql = "SELECT count(*) FROM UT_SYS_CodeDetail WHERE IsDeleted = 0 AND CodeID = " + CodeID;
            DataTable dt = NDDBAccess.Fill(sql);

            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// �������������Ӵ��������Ƿ������ͬ����
        /// </summary>
        /// <param name="CodeName"></param>
        /// <param name="CodeID"></param>
        /// <param name="CodeGroupID"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public static bool CheckCodeDetailName(string CodeDetailName, long CodeDetailID, long CodeID, int Operation)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_CodeDetail WHERE IsDeleted = 0 AND CodeID = " + CodeID + " AND CodeDetailName = '" + CodeDetailName + "'";
            if (Operation == 2)
            {
                sql += " AND CodeDetailID <> " + CodeDetailID;
            }

            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// �������������Ӵ���ֵ�Ƿ������ֵͬ
        /// </summary>
        /// <param name="CodeValue"></param>
        /// <param name="CodeID"></param>
        /// <param name="CodeGroupID"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public static bool CheckCodeDetailValue(string CodeDetailValue, long CodeDetailID, long CodeID, int Operation)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_CodeDetail WHERE IsDeleted = 0 AND CodeID = " + CodeID + " AND CodeDetailValue = '" + CodeDetailValue + "'";
            if (Operation == 2)
            {
                sql += " AND CodeDetailID <> " + CodeDetailID;
            }

            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
