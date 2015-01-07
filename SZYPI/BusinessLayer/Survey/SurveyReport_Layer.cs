using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using System.Data;

namespace BusinessLayer.Survey
{
    public class SurveyReport_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public DataTable GetSurveyTableInfo(string SID)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT [SID],[UID],[SurveyName],[CreateDate],[EndDate],[SurveyPSW],[EndPage],[StartPage],[AnswerAmount],[State],[Active],[Recommend],[LastUpdate],[MaxAnswerAmount],[TempPage],[Par],[ClassID],[Report],[ToURL],[ComplateMessage],[Point],[AnswerArea],[AdminSetAnswerAmount],[AdminSetAnsweredAmount],[Lan],[SurveyType],[IsDel] FROM " + TabelName + " where SID=@SID ", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetAnswerInfoStatistics(string SID)
        {
            //string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT MAX(SecondTime) AS MaxSecondTime,MIN(SecondTime) AS MinSecondTime,SUM(SecondTime) AS SumSecondTime,AVG(SecondTime) AS AvgSecondTime  FROM " + " UV_QS_AnswerInfo " + " where SID=@SID ", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetPageTable(string SID)
        {
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("SELECT [PID],[PageNo],[SID],[UID],[Sort],[PageContent] FROM " + TabelName + " where SID=@SID ", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetItemTable(string SID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("SELECT [IID],[UID],[SID],[PageNo],[ItemName],[ItemType],[DataFormatCheck],[ItemContent],[Sort],[ItemHTML],[OptionAmount],[ParentID],[StyleMode],[OrderModel],[OtherProperty],[Logic],[StatAmount],[OptionImgModel],[ChildID],[ItemConclusion] FROM " + TabelName + " where SID=@SID ", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

    }
}
