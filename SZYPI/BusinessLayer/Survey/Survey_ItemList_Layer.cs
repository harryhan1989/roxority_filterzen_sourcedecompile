using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Business.Helper;
using DBAccess;
using System.Data;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_ItemList_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public int CreateItemText_UpdateItemTable(string SID, string UID, string SID1, string IID1)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort-1 WHERE SID=@SID AND UID=@UID AND PageNo=(SELECT TOP 1 PageNo FROM " + TabelName + " WHERE SID=@SID1 AND IID=@IID1 AND UID=@UID1) AND Sort>(SELECT TOP 1 Sort FROM "+ TabelName +" WHERE SID=@SID2 AND IID=@IID2 AND UID=@UID2)", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=Sort-1 WHERE SID=@SID AND "+initRightSql+" AND PageNo=(SELECT TOP 1 PageNo FROM " + TabelName + " WHERE SID=@SID1 AND IID=@IID1 AND "+initRightSql+") AND Sort>(SELECT TOP 1 Sort FROM " + TabelName + " WHERE SID=@SID2 AND IID=@IID2 AND "+initRightSql+")", 100);
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID1", SID1);
            parameters[3] = new SqlParameter("@IID1", IID1);
            parameters[4] = new SqlParameter("@SID2", SID1);
            parameters[5] = new SqlParameter("@IID2", IID1);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int DeleteItemTable(string SID, string IID, string ParentID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + "WHERE SID=@SID AND (IID=@IID OR ParentID=@ParentID)AND UID=@UID", 50);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + "WHERE SID=@SID AND (IID=@IID OR ParentID=@ParentID)AND "+initRightSql, 50);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@IID", IID);
            parameters[2] = new SqlParameter("@ParentID", ParentID);
            parameters[3] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int DeleteOptionTable(string SID, string IID,string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + "WHERE SID=@SID AND IID=@IID AND UID=@UID", 50);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + "WHERE SID=@SID AND IID=@IID AND "+initRightSql, 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@IID", IID);
            parameters[2] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataSet GetGridViewItemTable(string SID, string ParentID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("SELECT IID,ItemName,ItemType,PageNo FROM " + TabelName + " WHERE SID=@SID AND ParentID=@ParentID ORDER BY Sort desc", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@ParentID", ParentID);
            return DbHelperSQL.Query(ConvertHelper.ConvertString(sql),parameters);
        }
    }
}
