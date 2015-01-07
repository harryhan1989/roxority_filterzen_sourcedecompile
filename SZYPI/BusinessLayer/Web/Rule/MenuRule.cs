using System;
using DBAccess;
using Business.Helper;
using System.Data;
using System.Text;
using EntityModel.WebEntity;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BusinessLayer.Web.Rule
{
    public class MenuRule
    {
        /// <summary>
        /// 根据菜单类型获得用户操作菜单
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetMenuAll(long parentMenuID)
        {
            string sql = "SELECT * FROM UT_Sys_Menu WHERE IsDel = 0 AND IsShow = 1 AND ParentID = " + parentMenuID + " ORDER BY SortID";
            DataTable dt = DbHelperSQL.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 根据菜单类型获得用户操作菜单
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetMenuAll(long parentMenuID,Common.CommonEnum.MenuType menuType)
        {
            string sql = "SELECT * FROM UT_Sys_Menu WHERE IsDel = 0 AND IsShow = 1 AND Type= " + (int)menuType + " AND ParentID = " + parentMenuID + " ORDER BY SortID";
            DataTable dt = DbHelperSQL.Fill(sql);
            return dt;
        }
    }
}
