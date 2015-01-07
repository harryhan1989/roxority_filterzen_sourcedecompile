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
    public class Survey_SetPoint_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public DataTable GetOptionTable(string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("SELECT OID,OptionName,Point FROM " + TabelName + " WHERE SID=@SID  AND UID=@UID AND IID=@IID", 100);
            StringBuilder sql = new StringBuilder("SELECT OID,OptionName,Point FROM " + TabelName + " WHERE SID=@SID  AND "+initRightSql+" AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return  DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }


        public int UpdateOptionTable(string Point, string OID, string UID, string SID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET Point=@Point WHERE OID=@OID AND UID=@UID AND SID=@SID AND IID=@IID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET Point=@Point WHERE OID=@OID AND "+initRightSql+" AND SID=@SID AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@Point", Point);
            parameters[1] = new SqlParameter("@OID", OID);
            parameters[2] = new SqlParameter("@UID", UID);
            parameters[3] = new SqlParameter("@SID", SID);
            parameters[4] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
