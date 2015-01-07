using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;
using System.Data;

namespace BLL
{
    public class MenuFunctionQuery
    {
        /// <summary>
        /// 邦定下拉框
        /// </summary>
        /// <returns></returns>
        public DataTable GetMenuFunction(long MenuID)
        {
            string sql = "SELECT * FROM UT_SYS_MenuFunction WHERE IsDeleted = 0 AND IsShow = 1 AND MenuID = " + MenuID + " ORDER BY SortIndex";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetMenuFunctionNow(long MenuID)
        {
            string sql = "SELECT FID,FName,FCode,CASE IsShow WHEN 0 THEN '否' WHEN 1 THEN '是' END AS IsShowName FROM UT_SYS_MenuFunction WHERE IsDeleted = 0 AND MenuID = " + MenuID + " ORDER BY SortIndex";
            return NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 获取最大排序号
        /// </summary>
        /// <returns></returns>
        public static int MaxSortIndex(long MenuID)
        {
            string sql = "SELECT MAX(SortIndex) FROM UT_SYS_MenuFunction WHERE IsDeleted = 0 AND MenuID = " + MenuID;
            DataTable dt = NDDBAccess.Fill(sql);
            return NDConvert.ToInt32(dt.Rows[0][0].ToString()) + 1;
        }

        /// <summary>
        /// 检查功能名称是否已经存在
        /// </summary>
        /// <returns></returns>
        public static bool IsExisitName(long MenuID,string FName)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_MenuFunction WHERE IsDeleted = 0 AND MenuID = "+ MenuID +" AND FName = '"+ FName +"'";
            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt64(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查代码名称是否已经存在
        /// </summary>
        /// <returns></returns>
        public static bool IsExisitCode(long MenuID, string FCode)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_MenuFunction WHERE IsDeleted = 0 AND MenuID = " + MenuID + " AND FCode = '" + FCode + "'";
            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt64(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
