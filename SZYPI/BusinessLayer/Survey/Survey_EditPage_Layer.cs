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
    public class Survey_EditPage_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetSurveyExpand(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND ExpandType=1", 100);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM " + TabelName + "WHERE SID=@SID AND "+initRightSql+" AND ExpandType=1", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);

           return  DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetPageTable(string SID, string UID, string PID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 PageContent FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND PID=@PID", 100);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 PageContent FROM " + TabelName + "WHERE SID=@SID AND "+initRightSql+" AND PID=@PID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PID", PID);

            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public int UpdatePageTable(string PageContent, string SID, string UID, string PID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageContent=@PageContent WHERE SID=@SID AND UID=@UID AND PID=@PID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET PageContent=@PageContent WHERE SID=@SID AND "+initRightSql+" AND PID=@PID", 100);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@PageContent", PageContent);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);
            parameters[3] = new SqlParameter("@PID", PID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
