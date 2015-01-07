using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DBAccess;
using Business.Helper;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_Default3_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetSurveyTableSID(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 SID FROM" + TabelName + "WHERE SID=@SID AND UID=@UID", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 SID FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql, 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql),parameters);
        }
    }
}
