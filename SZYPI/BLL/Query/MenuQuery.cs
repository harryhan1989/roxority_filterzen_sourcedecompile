using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;
using System.Data;

namespace BLL
{
    public class MenuQuery
    {    
        /// <summary>
        /// 根据菜单类型获得用户操作菜单
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetMenuAll(long parentMenuID)
        {
            string sql = "SELECT * FROM UT_SYS_Menu WHERE IsDeleted = 0 AND IsDisplay = 1 AND ParentMenuID = " + parentMenuID + " ORDER BY SortOrder";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 根据菜单类型获得用户操作菜单
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetMenuAll(long parentMenuID, CommonEnum.MenuType menuType)
        {
            string sql = "SELECT * FROM UT_SYS_Menu WHERE IsDeleted = 0 AND IsDisplay = 1 AND Type= " + (int)menuType + " AND ParentMenuID = " + parentMenuID + " ORDER BY SortOrder";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 根据菜单类型获得用户操作菜单
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetMenuAll2(long parentMenuID)
        {
            string sql = "SELECT * FROM UT_SYS_Menu WHERE IsDeleted = 0 AND ParentMenuID = " + parentMenuID + " ORDER BY SortOrder";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 根据菜单类型获得用户操作菜单
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static bool IsHaveChileMenu(long parentMenuID)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_Menu WHERE IsDeleted = 0 AND ParentMenuID = " + parentMenuID;
            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取顶级菜单
        /// </summary>
        /// <returns></returns>
        public DataTable GetMenuTop()
        {
            string sql = "SELECT MenuName,MenuID,IsDisplay FROM UT_SYS_Menu WHERE IsDeleted = 0 AND ParentMenuID = 0  ORDER BY SortOrder ";
            return NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 获取子记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetMenuChild(long parentMenuID)
        {
            string sql = "SELECT MenuName,MenuID,IsDisplay FROM UT_SYS_Menu WHERE IsDeleted = 0 AND ParentMenuID = " + parentMenuID +" ORDER BY SortOrder ";
            return NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 获取最大排序号
        /// </summary>
        /// <returns></returns>
        public static int MaxSortIndex(long parentMenuID)
        {
            string sql = "SELECT MAX(SortOrder) FROM UT_SYS_Menu WHERE IsDeleted = 0 AND ParentMenuID = " + parentMenuID;
            DataTable dt = NDDBAccess.Fill(sql);
            return NDConvert.ToInt32(dt.Rows[0][0].ToString()) + 1;
        }
    }
}
