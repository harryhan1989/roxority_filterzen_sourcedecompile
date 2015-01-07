using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;
using System.Data;

namespace BLL
{
    public class CodeGroupQuery
    {
        /// <summary>
        /// 根据父级部门ID获得下级部门信息
        /// </summary>
        /// <param name="OUParentID"></param>
        /// <returns></returns>
        public DataTable GetAllCodeGroup()
        {
            string sql = "SELECT * FROM UT_SYS_CodeGroup WHERE IsDeleted = 0 ORDER BY SortOrder DESC";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 检测代码组名称是否存在相同名称
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="userID"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public static bool CheckCodeGroupName(string CodeGroupName, long CodeGroupID, int Operation)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_CodeGroup WHERE IsDeleted = 0 AND  CodeGroupName = '" + CodeGroupName + "'";
            if (Operation == 2)
            {
                sql += " AND CodeGroupID <> " + CodeGroupID;
            }

            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检测代码组代码是否存在相同代码
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="userID"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public static bool CheckCodeGroupKey(string CodeGroupKey, long CodeGroupID, int Operation)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_CodeGroup WHERE IsDeleted = 0 AND  CodeGroupKey = '" + CodeGroupKey + "'";
            if (Operation == 2)
            {
                sql += " AND CodeGroupID <> " + CodeGroupID;
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
