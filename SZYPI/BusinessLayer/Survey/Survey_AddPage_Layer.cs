using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_AddPage_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";

        public int UpdatePageTable(string PageNo, string UID, string SID)
        {
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo+1 WHERE PageNo>=@PageNo AND UID=@UID AND SID=@SID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@PageNo", PageNo);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int InsertPageTable(string PageNo, string UID, string SID)
        {
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(PageNo,SID,UID) VALUES (@PageNo,@SID,@UID)", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@PageNo", PageNo);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int UpdateItemTable(string UID, string SID, string PageNo)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET PageNo=PageNo+1 WHERE UID=@UID AND SID=@SID AND PageNo>=@PageNo", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@PageNo", PageNo);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetPageTable(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID," ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 PageNo FROM" + TabelName + "WHERE SID=@SID AND UID=@UID ORDER BY PageNo DESC", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 PageNo FROM" + TabelName + "WHERE SID=@SID AND" + initRightSql + " ORDER BY PageNo DESC", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

    }
}
