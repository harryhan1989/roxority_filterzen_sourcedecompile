using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Business.Helper;
using DBAccess;
using System.Data;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_ItemLib_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public DataTable GetItemLib(string UID)
        {
            string TabelName = " " + prefix + "ItemLib ";
            StringBuilder sql = new StringBuilder("SELECT LIID,IID,ItemName,ItemType,Active FROM  " + TabelName + "WHERE UID=@UID and Active=1", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UID", UID);

            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public int DelItemLib(string UID, string LIID)
        {
            string TabelName = " " + prefix + "ItemLib ";
            StringBuilder sql = new StringBuilder("DELETE FROM  " + TabelName + "WHERE (UID=@UID AND LIID=@LIID)", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@LIID", LIID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemLib1(string UID, string LIID)
        {
            string TabelName = " " + prefix + "ItemLib ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ItemFrame FROM  " + TabelName + "WHERE LIID=@LIID AND UID=@UID  AND Active=1", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@LIID", LIID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
