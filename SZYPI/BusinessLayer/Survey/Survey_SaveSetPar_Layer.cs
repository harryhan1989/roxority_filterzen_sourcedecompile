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
    public class Survey_SaveSetPar_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public int UpdateSurveyTable(string Par, string Report, string MaxAnswerAmount, string EndDate, string ComplateMessage, string ClassID, string EndPage, string Active, string SurveyPSW, string ToURL, string Point, string Lan, string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET Par=@Par,Report=@Report,MaxAnswerAmount=@MaxAnswerAmount,EndDate=@EndDate,ComplateMessage=@ComplateMessage,ClassID=@ClassID ", 50);
            sql.Append(",EndPage=@EndPage,Active=@Active,SurveyPSW=@SurveyPSW,ToURL=@ToURL,Point=@Point,Lan=@Lan WHERE "+initRightSql+" AND SID=@SID");
            SqlParameter[] parameters = new SqlParameter[14];
            parameters[0] = new SqlParameter("@Par", Par);
            parameters[1] = new SqlParameter("@Report", Report);
            parameters[2] = new SqlParameter("@MaxAnswerAmount", MaxAnswerAmount);
            parameters[3] = new SqlParameter("@EndDate", EndDate);
            parameters[4] = new SqlParameter("@ComplateMessage", ComplateMessage);
            parameters[5] = new SqlParameter("@ClassID", ClassID);
            parameters[6] = new SqlParameter("@EndPage", EndPage);
            parameters[7] = new SqlParameter("@Active", Active);
            parameters[8] = new SqlParameter("@SurveyPSW", SurveyPSW);
            parameters[9] = new SqlParameter("@ToURL", ToURL);
            parameters[10] = new SqlParameter("@Point", Point);
            parameters[11] = new SqlParameter("@Lan", Lan);
            parameters[12] = new SqlParameter("@UID", UID);
            parameters[13] = new SqlParameter("@SID", SID);
            try
            {
                return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
            }
            catch
            {
                return 1;
            }
        }
    }
}
