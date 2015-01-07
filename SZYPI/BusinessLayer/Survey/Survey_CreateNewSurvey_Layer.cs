using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;

namespace BusinessLayer.Survey
{
    public  class Survey_CreateNewSurvey_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public void InsertSurveyTable(string surveyName, DateTime createDate, string uID, string classID, string par, string lan, string SurveyType)
        {
            string TabelName = " " + prefix + "SurveyTable ";
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

        public SqlDataReader GetSurveyTable()
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT   MAX(SID) FROM" + TabelName , 50);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql));
        }

        public int InsertPageTable(string SID, string UID, string PageContent, string PageNo)
        {
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,PageContent,PageNo) VALUES(@SID,@UID,@PageContent,@PageNo)", 50);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PageContent", PageContent);
            parameters[3] = new SqlParameter("@PageNo", PageNo);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int InsertHeadFoot(string PageHead, string UID, string SID)
        {
            string TabelName = " " + prefix + "HeadFoot ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(PageHead,UID,SID) VALUES(@PageHead,@UID,@SID)", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@PageHead", PageHead);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);
            try
            {
                return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
            }
            catch
            {
                return 0;
            }
        }
    }
}
