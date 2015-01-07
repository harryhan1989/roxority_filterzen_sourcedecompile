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
    public class Survey_SetPar_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public DataTable GetPageStyle()
        {
            string TabelName = " " + prefix + "PageStyle ";
            StringBuilder sql = new StringBuilder("SELECT P_ID,PageFileName,StyleName,PageType FROM" + TabelName + "WHERE PageType>=1  ORDER BY Sort", 50);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
        }

        public DataTable GetSurveyClass()
        {
            string TabelName = " " + prefix + "SurveyClass ";
            StringBuilder sql = new StringBuilder("SELECT CID,SurveyClassName FROM " + TabelName + "ORDER BY Sort", 50);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
        }

        public DataTable GetSurveyExpand(string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("SELECT ExpandContent,ExpandType FROM " + TabelName + "WHERE UID=@UID AND SID=@SID", 50);
            StringBuilder sql = new StringBuilder("SELECT ExpandContent,ExpandType FROM " + TabelName + "WHERE "+initRightSql+" AND SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql),parameters);
        }

        public SqlDataReader GetSurveyTable(string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("SELECT * FROM" + TabelName + "WHERE UID=@UID AND SID=@SID", 50);
            StringBuilder sql = new StringBuilder("SELECT * FROM" + TabelName + "WHERE "+initRightSql+" AND SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
