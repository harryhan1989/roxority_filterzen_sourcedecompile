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
    public class Survey_MovePage_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public void UpdatePageTable(string SID, string UID, string PageNo)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo-1 WHERE SID=@SID AND UID=@UID AND PageNo=@PageNo", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo-1 WHERE SID=@SID AND "+initRightSql+" AND PageNo=@PageNo", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PageNo", PageNo);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdatePageTable1(string SID, string UID, string PID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo+1 WHERE SID=@SID AND UID=@UID AND PID=@PID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo+1 WHERE SID=@SID AND "+initRightSql+"AND PID=@PID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PID", PID);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdatePageTable2(string SID, string UID, string PageNo)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo+1 WHERE SID=@SID AND UID=@UID AND PageNo=@PageNo", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo+1 WHERE SID=@SID AND "+initRightSql+" AND PageNo=@PageNo", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PageNo", PageNo);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdatePageTable3(string SID, string UID, string PID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo-1 WHERE SID=@SID AND UID=@UID AND PID=@PID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo-1 WHERE SID=@SID AND "+initRightSql+" AND PID=@PID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PID", PID);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateItemTable(string SID, string UID, string PageNo)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=0 WHERE SID=@SID AND UID=@UID AND PageNo=@PageNo", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=0 WHERE SID=@SID AND "+initRightSql+" AND PageNo=@PageNo", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PageNo", PageNo);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateItemTable1(string SID, string UID, string PageNo)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo+1 WHERE SID=@SID AND UID=@UID AND PageNo=@PageNo", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo+1 WHERE SID=@SID AND "+initRightSql+" AND PageNo=@PageNo", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PageNo", PageNo);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateItemTable2(string PageNo,string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=@PageNo WHERE SID=@SID AND UID=@UID AND PageNo=0", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=@PageNo WHERE SID=@SID AND "+initRightSql+" AND PageNo=0", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@PageNo", PageNo);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateItemTable3(string SID, string UID, string PageNo)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo-1 WHERE SID=@SID AND UID=@UID AND PageNo=@PageNo", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageNo=PageNo-1 WHERE SID=@SID AND "+initRightSql+" AND PageNo=@PageNo", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PageNo", PageNo);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetPageTable(string SID, string UID, string PID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 PageNo FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND PID=@PID", 100);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 PageNo FROM " + TabelName + "WHERE SID=@SID AND "+initRightSql+" AND PID=@PID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PID", PID);

            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
