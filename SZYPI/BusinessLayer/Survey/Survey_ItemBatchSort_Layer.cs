using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using DBAccess;
using Business.Helper;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_ItemBatchSort_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";

        public SqlDataReader GetItemTable(string SID, string UID, string PID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            string TabelName1 = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("SELECT IID,ItemName,ItemType,Sort FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND PageNo=(SELECT TOP 1 PageNo FROM" + TabelName1 + "WHERE PID=@PID ) ORDER BY Sort", 50);
            StringBuilder sql = new StringBuilder("SELECT IID,ItemName,ItemType,Sort FROM" + TabelName + "WHERE SID=@SID AND "+initRightSql+" AND PageNo=(SELECT TOP 1 PageNo FROM" + TabelName1 + "WHERE PID=@PID ) ORDER BY Sort", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PID", PID);

            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql),parameters);
        }

        public int UpdateItemTable(string Sort,string IID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET Sort=@Sort WHERE  IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Sort", Sort);
            parameters[1] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
