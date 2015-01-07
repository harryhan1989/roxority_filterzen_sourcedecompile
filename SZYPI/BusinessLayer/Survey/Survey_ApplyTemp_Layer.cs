using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DBAccess;
using Business.Helper;
using System.Data.SqlClient;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_ApplyTemp_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";

        public SqlDataReader GetPageStyle()
        {
            string TabelName = " " + prefix + "PageStyle ";
            StringBuilder sql = new StringBuilder("SELECT StyleName,PageFileName,PageImage FROM" + TabelName + "WHERE PageType=0 ORDER BY Sort DESC", 50);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql));
        }

        public SqlDataReader GetSurveyTable(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 TempPage FROM" + TabelName + "WHERE SID=@SID AND UID=@UID", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 TempPage FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql, 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql),parameters);
        }

        public SqlDataReader UpdateSurveyTable_Save(string TempPage, string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET TempPage=@TempPage WHERE SID=@SID AND UID=@UID" , 50);
            StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET TempPage=@TempPage WHERE SID=@SID AND " + initRightSql, 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@TempPage", TempPage);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql),parameters);
        }
    }
}
