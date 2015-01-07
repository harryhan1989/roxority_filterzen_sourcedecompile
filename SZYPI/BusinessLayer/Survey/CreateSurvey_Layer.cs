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
    public class CreateSurvey_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";

        public void InsertSurveyTable(string surveyName, DateTime createDate, string uID, string classID, string par, string lan, string SurveyType)
        {
            string TabelName =" "+prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO " + TabelName + " (SurveyName,CreateDate,UID,ClassID,Par,Lan,SurveyType) VALUES(@SurveyName,@CreateDate,@UID,@ClassID,@Par,@Lan,@SurveyType)", 100);
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@SurveyName", surveyName);
            parameters[1] = new SqlParameter("@CreateDate", createDate);
            parameters[2] = new SqlParameter("@UID", uID);
            parameters[3] = new SqlParameter("@ClassID", classID);
            parameters[4] = new SqlParameter("@Par", par);
            parameters[5] = new SqlParameter("@Lan", lan);
            parameters[6] = new SqlParameter("@SurveyType", SurveyType);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSIDBySurveyTable()
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 SID FROM" + TabelName + "ORDER BY SID DESC", 50);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql));
        }

        public void InsertPageTable(string UID, string SID, string PageNo, string PageContent)
        {
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO " + TabelName + " (UID,SID,PageNo,PageContent) VALUES(@UID,@SID,@PageNo,@PageContent)", 100);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@PageNo", PageNo);
            parameters[3] = new SqlParameter("@PageContent", PageContent);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void InsertHeadFoot(string PageHead, string UID, string SID)
        {
            string TabelName = " " + prefix + "HeadFoot ";
            StringBuilder sql = new StringBuilder("INSERT INTO " + TabelName + " (PageHead,UID,SID) VALUES(@PageHead,@UID,@SID)", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@PageHead", PageHead);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetSurveyClass()
        {
            string TabelName = " " + prefix + "SurveyClass ";
            StringBuilder sql = new StringBuilder("SELECT [CID], [SurveyClassName], [Sort] FROM" + TabelName + "ORDER BY [Sort]", 50);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
        }
    }
}
