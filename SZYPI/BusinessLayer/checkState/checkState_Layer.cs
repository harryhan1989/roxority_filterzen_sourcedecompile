using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Business.Helper;
using DBAccess;
using System.Configuration;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.checkState
{
    public class checkState_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetSurveyTable(string SID,string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 State FROM" + TabelName + "WHERE SID=@SID AND UID=@UID", 200);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 State FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql, 200);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyTable1(string SID)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 State FROM" + TabelName + "WHERE SID=@SID", 200);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyExpand(string SID, string UID, string ExpandType)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND ExpandType=@ExpandType", 200);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND ExpandType=@ExpandType", 200);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ExpandType", ExpandType);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
