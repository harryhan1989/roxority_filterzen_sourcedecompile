using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;
using System.Data;

namespace BLL
{
    public class RightQuery
    {
        /// <summary>
        /// ��ø����û��˵�Ȩ��
        /// </summary>
        /// <param name="PersonID"></param>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        //public static bool GetUserRightQuery(long UserID, long MenuID)
        //{
        //    string sql = "SELECT Count(MenuID) FROM UT_SYS_Right WHERE UserID = " + UserID + " AND MenuID = " + MenuID;
        //    DataTable dt = NDDBAccess.Fill(sql);
        //    if (NDConvert.ToInt32(dt.Rows[0][0].ToString()) > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// �����û��ɣĻ���û�Ȩ��
        /// </summary>
        /// <param name="PersonID"></param>
        /// <returns></returns>
        public DataTable GetUserRight(long UserID)
        {
            string sql = "SELECT MenuID FROM UT_SYS_Right WHERE UserID = " + UserID;
            DataTable dt = NDDBAccess.Fill(sql);
            return dt;
        }

        /// <summary>
        /// ɾ���û�Ȩ��
        /// </summary>
        /// <param name="PersonID"></param>
        public void DeleteUserRight(long UserID)
        {
            string sql = "DELETE FROM UT_SYS_Right WHERE UserID = " + UserID;
            NDDBAccess.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// �жϾƵ����Ա��ӵ�еĲ˵�Ȩ��
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public static bool GetUserRightQuery(long UserID, long MenuID)
        {
            List<System.Data.SqlClient.SqlParameter> paramList = new List<System.Data.SqlClient.SqlParameter>();
            paramList.Add(new System.Data.SqlClient.SqlParameter("@MenuID", MenuID));
            paramList.Add(new System.Data.SqlClient.SqlParameter("@UserID", UserID));

            DataTable dt = NDDBAccess.Fill("UP_SYS_Oth_CheckUserRight ", paramList, CommandType.StoredProcedure);
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
