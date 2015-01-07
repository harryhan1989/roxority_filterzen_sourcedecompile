using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using System.Data;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_SurveyList1_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public void DelSurveyTable(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND " + initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void DelItemTable(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND " + initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void DelPageTable(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND " + initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void DelOptionTable(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND "+initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void DelHeadFoot(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "HeadFoot ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND " + initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void DelSurveyExpand(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE SID=@SID AND " + initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetSurveyTable(int topnum, string UID)
        {
            initRightSql = new InitRight().initUserRight("UID=@UID ", " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP " + topnum + " * from" + TabelName + " WHERE UID=@UID ORDER BY SID DESC", 100);
            StringBuilder sql = new StringBuilder("SELECT TOP " + topnum + " * from" + TabelName + " WHERE " + initRightSql + " ORDER BY SID DESC", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UID", UID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetCount(string UID)
        {
            initRightSql = new InitRight().initUserRight("UID=@UID ", " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("SELECT COUNT(1) FROM " + TabelName + " WHERE UID=@UID", 100);
            StringBuilder sql = new StringBuilder("SELECT COUNT(1) FROM " + TabelName + " WHERE " + initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UID", UID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateSurveyTable(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET Active=ABS(Active-1) WHERE SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET Active=ABS(Active-1) WHERE SID=@SID AND " + initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateSurveyTable1(string SurveyName, string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET SurveyName=@SurveyName WHERE SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET SurveyName=@SurveyName WHERE SID=@SID AND " + initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SurveyName", SurveyName);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
