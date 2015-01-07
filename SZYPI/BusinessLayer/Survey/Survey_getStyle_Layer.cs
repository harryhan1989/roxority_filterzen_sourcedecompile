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
     public class Survey_getStyle_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetSurveyTable(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 SurveyName FROM " + TabelName + "WHERE SID=@SID AND UID=@UID", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 SurveyName FROM " + TabelName + "WHERE SID=@SID AND " + initRightSql, 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql),parameters);
        }

        public SqlDataReader GetSurveyExpand(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND ExpandType=9", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM " + TabelName + "WHERE SID=@SID AND "+initRightSql+" AND ExpandType=9", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetHeadFoot(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "HeadFoot ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 PageHead,PageFoot FROM" + TabelName + "WHERE SID=@SID AND UID=@UID", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 PageHead,PageFoot FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql, 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
