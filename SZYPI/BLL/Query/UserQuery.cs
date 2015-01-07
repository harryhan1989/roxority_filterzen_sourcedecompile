using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nandasoft;

namespace BLL
{
    public class UserQuery
    {
        /// <summary>
        /// 检测是否有重复帐号
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool CheckAccount(string Account, long userID,int Operation)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_User WHERE Account = '" + Account + "' AND IsDeleted=0 ";
            if (Operation==2)
            {
                sql += " AND UserID != " + userID;
            }

            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获得部门用户信息
        /// </summary>
        /// <param name="OUID"></param>
        /// <returns></returns>
        public DataSet GetOUUser(long OUID, int PageIndex, int PageSize)
        {
            string sql = "SELECT UT_SYS_User.UserID,UT_SYS_User.UserName,UT_SYS_User.SortIndex,UT_SYS_OU.OUID,UT_SYS_User.Account,UT_SYS_OU.OUName,Case UT_SYS_User.Status WHEN 1 THEN '正常' WHEN 2 THEN '锁定' END AS StatusName " +
                         "FROM UT_SYS_User INNER JOIN UT_SYS_OU ON UT_SYS_User.OUID = UT_SYS_OU.OUID " +
                         "WHERE UT_SYS_User.IsDeleted = 0 AND UT_SYS_User.UserID <> 1 AND UT_SYS_User.OUID=" + OUID + " " +
                         "ORDER BY UT_SYS_User.SortIndex";
            return Nandasoft.Helper.NDHelperWebControl.GetDataByPagerSP(sql, PageIndex, PageSize, "", "");
        }

        /// <summary>
        /// 获得要分配权限的员工信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRightPerson()
        {
            string sql = "SELECT dbo.UT_SYS_Employee.EmployeeName, dbo.UT_SYS_User.UserID," +
                            "dbo.UT_SYS_Employee.EmployeeID, dbo.UT_SYS_User.Type, "+
                            "dbo.UT_SYS_User.Account, dbo.UT_SYS_User.SourceID "+
                            "FROM dbo.UT_SYS_User INNER JOIN "+
                            "dbo.UT_SYS_Employee ON "+
                            "dbo.UT_SYS_User.SourceID = dbo.UT_SYS_Employee.EmployeeID WHERE type=2";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 根据部门ＩＤ获得部门人员
        /// </summary>
        /// <param name="OUID"></param>
        /// <returns></returns>
        public DataTable GetRightOUUser(long OUID)
        {
            string sql = "SELECT UserID, UserName FROM UT_SYS_User WHERE IsDeleted = 0 AND UserID <> 1  AND OUID = " + OUID + "  ORDER BY SortIndex ";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 判断部门是否有人员
        /// </summary>
        /// <param name="OUID"></param>
        /// <returns></returns>
        public static bool IsHavePerson(long OUID)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_User WHERE IsDeleted = 0 AND OUID = " + OUID;
            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取人员
        /// </summary>
        /// <returns></returns>
        public DataSet GetUsers(long OUID, string UserName, int PageIndex, int PageSize)
        {
            string sql = "SELECT dbo.UT_SYS_User.UserID, dbo.UT_SYS_User.Duty,dbo.UT_SYS_User.UserName, dbo.UT_SYS_OU.OUName " +
                         "FROM dbo.UT_SYS_User INNER JOIN " +
                         "dbo.UT_SYS_OU ON dbo.UT_SYS_User.OUID = dbo.UT_SYS_OU.OUID "+
                         "WHERE UT_SYS_User.IsDeleted = 0 AND UT_SYS_OU.IsDeleted = 0 ";
            if (OUID != 0)
            {
                sql += " AND UT_SYS_OU.OUID = " + OUID ;
            }
            if (!string.IsNullOrEmpty(UserName))
            {
                sql += " AND UT_SYS_User.UserName LIKE '%" + UserName + "%' ";
            }
            return Nandasoft.Helper.NDHelperWebControl.GetDataByPagerSP(sql.ToString(), PageIndex, PageSize, "", "");
        }

        /// <summary>
        /// 根据人员ID获取成本
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserCost(long UserID)
        {
            string sql = "SELECT UT_SYS_User.UserID,UT_SYS_User.UserName,UT_BLL_PersonCostConfig.CostQuotiety,projectID=0,ProjectPersonID =0 " +
                         "FROM dbo.UT_SYS_User INNER JOIN " +
                         "dbo.UT_BLL_PersonCostConfig ON " +
                         "dbo.UT_SYS_User.UserID = dbo.UT_BLL_PersonCostConfig.UserID " +
                         "WHERE UT_SYS_User.Isdeleted = 0 AND UT_BLL_PersonCostConfig.IsDeleted = 0";
            if (UserID != 0)
            {
                sql += " AND UT_SYS_User.UserID = "+ UserID;
            }
            return NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 根据部门ＩＤ获得部门人员
        /// </summary>
        /// <param name="OUID"></param>
        /// <returns></returns>
        public DataTable GetWaitChooseUser(long OUID, string UserID)
        {
            string sql = "SELECT UserID, UserName FROM UT_SYS_User " +
                        " WHERE IsDeleted = 0 AND UserID <> 1  AND UserID NOT IN (" + UserID + ") ";
            if(OUID != 0)
            {
                sql += " AND OUID =" + OUID;
            }
                          
            sql += " ORDER BY SortIndex";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 获取接待办人员
        /// </summary>
        /// <returns></returns>
        public DataTable GetManager(long OUID)
        {
            string sql = "SELECT UserID, UserName FROM UT_SYS_User " +
                               "WHERE IsDeleted = 0 AND OUID = " + OUID +""+
                               "ORDER BY SortIndex ";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 获取现有数据中某部门人员
        /// </summary>
        /// <param name="OUDeptID"></param>
        /// <returns></returns>
        public DataTable UsersNow(string OUDeptID)
        {
            string sql = "SELECT UserID,OUDeptID,UserServiceID,UserName FROM UT_SYS_User WHERE IsDeleted = 0 AND OUDeptID = '" + OUDeptID + "'";
            return NDDBAccess.Fill(sql);
        }
        
        /// <summary>
        /// 根据帐号获得人员信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetUserByAccount(string Account)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
            paramList.Add(new System.Data.SqlClient.SqlParameter("@Account", Account));
            string sql = "SELECT * FROM UT_SYS_User WHERE  IsDeleted=0  AND Account = @Account ";
            DataTable dt = NDDBAccess.Fill(sql, paramList);
            return dt;
        }

        /// <summary>
        /// 检测是否有重复手机号
        /// </summary>
        /// <param name="MobilePhone"></param>
        /// <param name="userID"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public static bool CheckMobilePhone(string MobilePhone, long userID, int Operation)
        {
            string sql = "SELECT COUNT(*) FROM UT_SYS_User WHERE MobilePhone = '" + MobilePhone + "' AND IsDeleted=0 ";
            if (Operation == 2)
            {
                sql += " AND UserID != " + userID;
            }

            DataTable dt = NDDBAccess.Fill(sql);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据手机号与密码获得人员信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetUserByMobilePhone(string MobilePhone, string Password)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
            paramList.Add(new System.Data.SqlClient.SqlParameter("@MobilePhone", MobilePhone));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@Password", Password));
            string sql = "SELECT * FROM UT_SYS_User WHERE IsDeleted=0 AND IsWapUser = 1 AND MobilePhone = @MobilePhone AND Password = @Password ";
            DataTable dt = NDDBAccess.Fill(sql, paramList);
            return dt;
        }

        /// <summary>
        /// 人员排序上移
        /// </summary>
        /// <param name="curUserID"></param>
        /// <param name="perUserID"></param>
        /// <returns></returns>
        public static void UserSortMoveup(long curUserID, long perUserID)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
            paramList.Add(new System.Data.SqlClient.SqlParameter("@CurUserID", curUserID));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@PerUserID", perUserID));
            NDDBAccess.ExecuteNonQuery("UP_ORG_Oth_UserSortMoveup", paramList, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 人员排序下移
        /// </summary>
        /// <param name="curUserID"></param>
        /// <param name="nextUserID"></param>
        /// <returns></returns>
        public static void UserSortMovedown(long curUserID, long nextUserID)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
            paramList.Add(new System.Data.SqlClient.SqlParameter("@CurUserID", curUserID));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@NextUserID", nextUserID));
            NDDBAccess.ExecuteNonQuery("UP_ORG_Oth_UserSortMovedown", paramList, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 生成最新排序号
        /// </summary>
        /// <returns></returns>
        public static int GetMaxSortIndex()
        {
            int MaxSortIndex = 1;
            string sql = "SELECT MAX(SortIndex) + 1 AS MaxSortIndex FROM UT_SYS_User";
            DataTable dt = NDDBAccess.Fill(sql);
            if (dt.Rows.Count > 0)
            {
                MaxSortIndex = NDConvert.ToInt32(dt.Rows[0]["MaxSortIndex"].ToString());
            }
            return MaxSortIndex;
        }

        /// <summary>
        /// 获得村官用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCGUser()
        {
            string sql = "SELECT UserID, UserName FROM UT_SYS_User " +
                         "WHERE IsDeleted = 0 " +
                         "AND OUID IN (SELECT OUID FROM UT_SYS_OU WHERE IsDeleted = 0 AND OUType = " + (int)CommonEnum.OUType.Outer + ") " +
                         "ORDER BY SortIndex ";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 获取正常人员
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserUs()
        {
            string sql = "SELECT UserID,UserName FROM UT_SYS_User WHERE IsDeleted = 0 AND Status = 1 ORDER BY SortIndex";
            return NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 根据userid获得人员信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetUserByUserID(string UserID)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();

            paramList.Add(new System.Data.SqlClient.SqlParameter("@UserID", UserID));

            string sql = "SELECT * FROM UT_SYS_User WHERE  IsDeleted=0  AND UserID = @UserID ";

            DataTable dt = NDDBAccess.Fill(sql, paramList);
            return dt;
        }
    }
}
