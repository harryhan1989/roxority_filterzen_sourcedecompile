using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;
using System.Data;

namespace BLL
{
    public class MenuFunctionGroupRightQuery
    {
        /// <summary>
        /// 判断群组是否具有某项功能
        /// </summary>
        /// <returns></returns>
        public static bool CheckMenuFunctionGroupRight(long GroupID, long MenuID, long FID)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_MenuFunctionGroupRight WHERE  GroupID = " + GroupID + " AND MenuID = " + MenuID + " AND FID = " + FID;
            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除群组菜单权限中的功能
        /// </summary>
        /// <param name="EmployeID"></param>
        /// <returns></returns>
        public bool DeleteMenuFunctionGroupRight(long GroupID)
        {
            string sql = "DELETE UT_SYS_MenuFunctionGroupRight WHERE GroupID = " + GroupID;
            NDDBAccess.ExecuteNonQuery(sql);
            return true;
        }
    }
}
