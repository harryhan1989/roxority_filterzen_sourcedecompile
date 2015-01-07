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
    public class Survey_SaveHeadOrFoot_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public void InsertHeadFoot(string PageHead, string SID, string UID)
        {
            string TabelName = " " + prefix + "HeadFoot ";
            StringBuilder sql = new StringBuilder("INSERT INTO " + TabelName + "(PageHead,SID,UID) VALUES(@PageFoot,@SID,@UID)", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@PageHead", PageHead);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateHeadFoot(string PageHead, string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "HeadFoot ";
            //StringBuilder sql = new StringBuilder("UPDATE  " + TabelName + "SET PageHead=@PageHead WHERE SID=@SID AND UID=@UID" , 100);
            StringBuilder sql = new StringBuilder("UPDATE  " + TabelName + "SET PageHead=@PageHead WHERE SID=@SID AND "+initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@PageHead", PageHead);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void InsertHeadFoot1(string PageFoot, string SID, string UID)
        {
            string TabelName = " " + prefix + "HeadFoot ";
            StringBuilder sql = new StringBuilder("INSERT INTO " + TabelName + "(PageFoot,SID,UID) VALUES(@PageFoot,@SID,@UID)", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@PageFoot", PageFoot);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateHeadFoot1(string PageFoot, string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "HeadFoot ";
            //StringBuilder sql = new StringBuilder("UPDATE  " + TabelName + "SET PageFoot=@PageFoot WHERE SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("UPDATE  " + TabelName + "SET PageFoot=@PageFoot WHERE SID=@SID AND "+initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@PageFoot", PageFoot);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetHeadFoot(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "HeadFoot ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ID FROM  " + TabelName + "WHERE SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ID FROM  " + TabelName + "WHERE SID=@SID AND " + initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
