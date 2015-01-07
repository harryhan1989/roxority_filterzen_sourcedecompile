using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;
using System.Data;

namespace BLL
{
    public class MenuFunctionRightQuery
    {
        /// <summary>
        /// 判断用户是否具有某项功能
        /// </summary>
        /// <returns></returns>
        public static bool CheckMenuFunctionRight(long UserID, long MenuID, long FID)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_MenuFunctionRight WHERE  UserID = " + UserID + " AND MenuID = " + MenuID + " AND FID = " + FID;
            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除用户菜单权限中的功能
        /// </summary>
        /// <param name="EmployeID"></param>
        /// <returns></returns>
        public bool DeleteUserMenuFunctionRight(long UserID)
        {
            string sql = "DELETE UT_SYS_MenuFunctionRight WHERE UserID = " + UserID;
            NDDBAccess.ExecuteNonQuery(sql);
            return true;
        }

        /// <summary>
        /// 判断用户是否具有某项功能
        /// </summary>
        /// <returns></returns>
        public static bool CheckRightFunction(long UserID, long MenuID, string toolbarItem)
        {
            string sql = "SELECT COUNT(*) " +
                        "FROM UT_SYS_MenuFunctionRight INNER JOIN " +
                        "UT_SYS_MenuFunction ON  " +
                        "UT_SYS_MenuFunctionRight.FID = UT_SYS_MenuFunction.FID WHERE FCode = '" + toolbarItem + "'  AND UserID = " + UserID + " AND UT_SYS_MenuFunctionRight.MenuID = " + MenuID;
            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断酒店管理员所拥有的菜单功能项权限
        /// </summary>
        /// <param name="MenuID"></param>
        /// <param name="FID"></param>
        /// <param name="OUID"></param>
        /// <returns></returns>
        public static bool CheckRightFunction1(long UserID, long MenuID, string FCode)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
            paramList.Add(new System.Data.SqlClient.SqlParameter("@UserID", UserID));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@MenuID", MenuID));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@FCode", FCode));

            DataTable dt = NDDBAccess.Fill("UP_SYS_Oth_CheckUserFunctionRight ", paramList, CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
                {
                    return true;
                }
                return false;
            }
            return false;

        }
    }
}
