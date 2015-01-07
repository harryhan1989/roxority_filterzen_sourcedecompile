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
    public class Survey_SetStyle_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetSurveyExpand(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM  " + TabelName + " WHERE SID=@SID AND UID=@UID AND ExpandType=9", 100);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM  " + TabelName + " WHERE SID=@SID AND "+initRightSql+" AND ExpandType=9", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);

           return  DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public int InsertSurveyExpand(string SID, string UID,string ExpandContent,string ExpandType)
        {
            string TabelName = " " + prefix + "SurveyExpand ";
            StringBuilder sql = new StringBuilder("INSERT INTO  " + TabelName + " (SID,UID,ExpandContent,ExpandType)VALUES(@SID,@UID,@ExpandContent,@ExpandType)", 100);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ExpandContent", ExpandContent);
            parameters[3] = new SqlParameter("@ExpandType", ExpandType);

           return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int UpdateSurveyExpand(string ExpandContent, string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("UPDATE  " + TabelName + "SET ExpandContent=@ExpandContent WHERE SID=@SID AND UID=@UID   AND ExpandType=9", 100);
            StringBuilder sql = new StringBuilder("UPDATE  " + TabelName + "SET ExpandContent=@ExpandContent WHERE SID=@SID AND "+initRightSql+"  AND ExpandType=9", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@ExpandContent", ExpandContent);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
