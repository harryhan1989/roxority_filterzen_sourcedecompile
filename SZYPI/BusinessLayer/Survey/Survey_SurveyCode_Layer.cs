using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DBAccess;
using System.Data.SqlClient;
using Business.Helper;

namespace BusinessLayer.Survey
{
    public class Survey_SurveyCode_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public SqlDataReader GetCondtionTable(string SID)
        {
            string TabelName = " " + prefix + "CondtionTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 Content FROM" + TabelName + "WHERE SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql),parameters);
        }
         
        public SqlDataReader GetIP(string sSubmitIP)
        {
            string TabelName = " " + prefix + "IP ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 Province,City FROM" + TabelName + " WHERE @sSubmitIP >=StartIP AND @sSubmitIP <=EndIP", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@sSubmitIP", sSubmitIP);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyExpand(string SID)
        {
            string TabelName = " " + prefix + "SurveyExpand ";
            StringBuilder sql = new StringBuilder("SELECT ExpandContent,ExpandType FROM" + TabelName + " WHERE SID=@SID AND ExpandType IN(0,1,8)", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyExpand1(string SID)
        {
            string TabelName = " " + prefix + "SurveyExpand ";
            StringBuilder sql = new StringBuilder("SELECT ExpandContent,ExpandType FROM" + TabelName + " WHERE SID=@SID AND ExpandType =5", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyTable(string SID)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT SurveyPSW,State,Active,Par,EndDate FROM" + TabelName + " WHERE SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
