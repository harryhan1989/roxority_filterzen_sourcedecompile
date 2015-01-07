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
    public class Survey_GetItem_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetItemTable(string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ItemHTML FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ItemHTML FROM " + TabelName + "WHERE SID=@SID AND "+initRightSql+" AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyExpand(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND ExpandType=9", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM" + TabelName + "WHERE SID=@SID AND "+initRightSql+" AND ExpandType=9", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);

            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
        public int UpdateItemTable(string ItemHTML, string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET ItemHTML =@ItemHTML WHERE SID=@SID AND UID=@UID AND IID=@IID", 50);
            StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET ItemHTML =@ItemHTML WHERE SID=@SID AND "+initRightSql+" AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@ItemHTML", ItemHTML);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);
            parameters[3] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

    }
}
