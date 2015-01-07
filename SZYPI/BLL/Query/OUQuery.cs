using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nandasoft;

namespace BLL
{
    public class OUQuery
    {
        /// <summary>
        /// 根据父级部门ID获得下级部门信息
        /// </summary>
        /// <param name="OUParentID"></param>
        /// <returns></returns>
        public DataTable GetOU(long OUParentID)
        {
            string sql = "SELECT OUID,OUName FROM UT_SYS_OU WHERE IsDeleted = 0 AND OUParentID=" + OUParentID + " ORDER BY SortIndex";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        public bool IsOUParent(long OUParentID)
        {
            string sql = "SELECT count(*) FROM UT_SYS_OU WHERE IsDeleted = 0 AND OUParentID=" + OUParentID;
            DataTable dt = NDDBAccess.Fill(sql);
           
            if(NDConvert.ToInt16(dt.Rows[0][0])>0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据父级部门ID获得下级部门信息
        /// </summary>
        /// <param name="OUParentID"></param>
        /// <returns></returns>
        public DataTable GetOU(long OUParentID, int OUType)
        {
            string sql = "SELECT OUID,OUName FROM UT_SYS_OU " +
                         "WHERE IsDeleted = 0 " +
                         "AND OUParentID=" + OUParentID + " " +
                         "AND OUType=" + OUType + " " +
                         "ORDER BY SortIndex";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 根据父级部门ID获得下级部门信息
        /// </summary>
        /// <param name="OUParentID"></param>
        /// <returns></returns>
        public DataTable GetOU1(long OUID)
        {
            string sql = "SELECT OUID,OUName FROM UT_SYS_OU WHERE IsDeleted = 0 AND OUID=" + OUID;
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 获得父级部门列表，只能邦定两级部门
        /// </summary>
        /// <returns></returns>
        public DataTable GetParentOU()
        {
            string sql = "SELECT OUID,OUName FROM UT_SYS_OU WHERE IsDeleted = 0 AND OUParentID IN (0,1) ORDER BY SortIndex";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        public DataTable GetParentOU(long ParentID)
        {
            string sql = string.Format("SELECT OUID,OUName FROM UT_SYS_OU WHERE IsDeleted = 0 AND OUParentID={0} ORDER BY SortIndex", ParentID);
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 获得部门列表，只能邦定两级部门
        /// </summary>
        /// <returns></returns>
        public DataTable GetOU()
        {
            string sql = "SELECT OUID,OUName FROM UT_SYS_OU WHERE IsDeleted = 0 ORDER BY SortIndex";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 获得部门最大排序号
        /// </summary>
        /// <returns></returns>
        public static int GetMaxSortIndex()
        {
            string sql = "SELECT MAX(sortIndex) + 1 FROM UT_SYS_OU";
            DataTable dt = NDDBAccess.Fill(sql);
            return NDConvert.ToInt32(dt.Rows[0][0].ToString());
        }

        public static int GetMaxSortIndex(long OUParentID)
        {
            string sql = string.Format("SELECT MAX(sortIndex) + 1 FROM UT_SYS_OU where OUParentID={0}", OUParentID);
            DataTable dt = NDDBAccess.Fill(sql);
            return NDConvert.ToInt32(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// 获得部门名
        /// </summary>
        /// <param name="OUID"></param>
        /// <returns></returns>
        public static string GetOUName(long OUID)
        {
            string sql = "SELECT OUName FROM UT_SYS_OU WHERE IsDeleted = 0 AND OUID=" + OUID + "ORDER BY SortIndex";
            DataTable dt = NDDBAccess.Fill(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }

        /// <summary>
        /// 部门排序上移
        /// </summary>
        /// <param name="curOUID"></param>
        /// <param name="perOUID"></param>
        /// <returns></returns>
        public static void OUSortMoveup(long curOUID, long perOUID)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
            paramList.Add(new System.Data.SqlClient.SqlParameter("@CurOUID", curOUID));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@PerOUID", perOUID));
            NDDBAccess.ExecuteNonQuery("UP_ORG_Oth_OUSortMoveup", paramList, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 部门排序下移
        /// </summary>
        /// <param name="curOUID"></param>
        /// <param name="perOUID"></param>
        /// <returns></returns>
        public static void OUSortMovedown(long curOUID, long NextOUID)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
            paramList.Add(new System.Data.SqlClient.SqlParameter("@CurOUID", curOUID));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@NextOUID", NextOUID));
            NDDBAccess.ExecuteNonQuery("UP_ORG_Oth_OUSortMovedown", paramList, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 检测部门号码
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="userID"></param>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public static bool CheckOUNumber(string OUNumber, long OUID, int Operation)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
            paramList.Add(new System.Data.SqlClient.SqlParameter("@OUNumber", OUNumber));
            string sql = "SELECT COUNT(*) FROM UT_SYS_OU WHERE IsDeleted = 0 AND  OUNumber = @OUNumber";     
            if (Operation == 2)
            {
                sql += " AND OUID <> " + OUID;
            }
            DataTable dt = NDDBAccess.Fill(sql, paramList);
            if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得部门列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetOUNow()
        {
            string sql = "SELECT OUID,OUName,OUDeptID FROM UT_SYS_OU WHERE IsDeleted = 0 ORDER BY SortIndex";
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// 获得本地部门ＩＤ
        /// </summary>
        /// <returns></returns>
        public static long GetOUIDLocal(string DeptID)
        {
            string sql = "SELECT OUID FROM UT_SYS_OU WHERE IsDeleted = 0 AND OUDeptID = '" + DeptID + "' ORDER BY SortIndex";
            DataTable dt = NDDBAccess.Fill(sql);
            if (dt.Rows.Count > 0)
            {
                return NDConvert.ToInt64(dt.Rows[0][0].ToString());
            }
            return 0;
        }


    }
}
