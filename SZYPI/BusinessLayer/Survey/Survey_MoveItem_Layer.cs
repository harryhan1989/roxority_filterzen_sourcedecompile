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
    public class Survey_MoveItem_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public void UpdateItemTable(string PageNo, string UID, string SID, string Sort)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort-1 WHERE PageNo=@PageNo AND UID=@UID AND SID=@SID AND Sort=@Sort", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort-1 WHERE PageNo=@PageNo AND "+initRightSql+" AND SID=@SID AND Sort=@Sort", 100);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@PageNo", PageNo);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);
            parameters[3] = new SqlParameter("@Sort", Sort);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateItemTable1(string PageNo, string UID, string SID, string Sort)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort+1 WHERE PageNo=@PageNo AND UID=@UID AND SID=@SID AND Sort=@Sort", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort+1 WHERE PageNo=@PageNo AND "+initRightSql+" AND SID=@SID AND Sort=@Sort", 100);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@PageNo", PageNo);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);
            parameters[3] = new SqlParameter("@Sort", Sort);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateItemTable2(string UID, string SID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort+1 WHERE  UID=@UID AND SID=@SID AND IID=@IID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort+1 WHERE  "+initRightSql+" AND SID=@SID AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@IID", IID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void UpdateItemTable3(string UID, string SID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort-1 WHERE  UID=@UID AND SID=@SID AND IID=@IID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort-1 WHERE  "+initRightSql+" AND SID=@SID AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@IID", IID);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemTable(string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 PageNo,Sort FROM" + TabelName + "WHERE  SID=@SID AND UID=@UID AND IID=@IID", 100);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 PageNo,Sort FROM" + TabelName + "WHERE  SID=@SID AND "+initRightSql+" AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
