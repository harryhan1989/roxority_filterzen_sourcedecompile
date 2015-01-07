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
    public class Survey_DelItem_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";

        public void UpdateItemTable(string Sort, string UID, string SID, string PageNo)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort-1 WHERE Sort>@Sort AND UID=@UID AND SID=@SID AND PageNo=@PageNo", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort-1 WHERE Sort>@Sort AND "+initRightSql+" AND SID=@SID AND PageNo=@PageNo", 100);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@Sort", Sort);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);
            parameters[3] = new SqlParameter("@PageNo", PageNo);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void DeleteItemTable(string IID, string UID, string SID, string ParentID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE (IID=@IID AND  UID=@UID AND SID=@SID ) OR (ParentID=@ParentID AND UID=@UID AND SID=@SID) ", 100);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + " WHERE (IID=@IID AND  UID=@UID AND SID=@SID ) OR (ParentID=@ParentID AND "+initRightSql+" AND SID=@SID) ", 100);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@IID", IID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);
            parameters[3] = new SqlParameter("@ParentID", ParentID);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void DeleteOptionTable(string IID, string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + "WHERE IID=@IID AND UID=@UID AND SID=@SID ", 100);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + "WHERE IID=@IID AND "+initRightSql+" AND SID=@SID ", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@IID", IID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);

            try
            {
                DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
            }
            catch
            { 
              
            }
        }

        public SqlDataReader GetItemTable(string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 PageNo,Sort FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 PageNo,Sort FROM" + TabelName + "WHERE SID=@SID AND "+initRightSql+" AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

    }
}
