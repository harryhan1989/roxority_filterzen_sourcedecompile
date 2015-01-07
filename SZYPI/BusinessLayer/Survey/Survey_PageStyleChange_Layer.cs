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
    public class Survey_PageStyleChange_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public int UpdateSurveyTable(string TempPage, string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET TempPage=@TempPage WHERE " + initRightSql + " AND  SID=@SID ", 30);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET TempPage=@TempPage WHERE UID=@UID AND  SID=@SID ", 30);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@TempPage", TempPage);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
