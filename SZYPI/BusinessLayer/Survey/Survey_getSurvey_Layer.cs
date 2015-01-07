using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DBAccess;
using Business.Helper;
using System.Data.SqlClient;
using System.Data;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_getSurvey_Layer
    {
        string initRightSql = "";
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public DataTable GetItemTable(string UID,string SID,long ParentID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT IID,ItemName,ItemType,ParentID,ItemHTML,OrderModel,OptionAmount,ItemContent,DataFormatCheck,OtherProperty,PageNo,Sort FROM" + TabelName + "WHERE UID=@UID AND SID=@SID AND ParentID=@ParentID ORDER BY PageNo,Sort", 200);
            StringBuilder sql = new StringBuilder("SELECT IID,ItemName,ItemType,ParentID,ItemHTML,OrderModel,OptionAmount,ItemContent,DataFormatCheck,OtherProperty,PageNo,Sort FROM" + TabelName + "WHERE "+initRightSql+" AND SID=@SID AND ParentID=@ParentID ORDER BY PageNo,Sort", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@ParentID", ParentID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql),parameters);
        }

        public DataTable GetPageTable(string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("SELECT PID,PageNo,PageContent FROM" + TabelName + "WHERE UID=@UID AND SID=@SID ORDER BY PageNo", 50);
            StringBuilder sql = new StringBuilder("SELECT PID,PageNo,PageContent FROM" + TabelName + "WHERE "+initRightSql+" AND SID=@SID ORDER BY PageNo", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetSurveyExpand(string UID, string SID, int ExpandType)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("SELECT ExpandContent FROM" + TabelName + "WHERE UID=@UID AND SID=@SID AND ExpandType=@ExpandType", 50);
            StringBuilder sql = new StringBuilder("SELECT ExpandContent FROM" + TabelName + "WHERE "+initRightSql+" AND SID=@SID AND ExpandType=@ExpandType", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@ExpandType", ExpandType);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetSurveyTable(string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 SurveyName,State FROM" + TabelName + "WHERE "+initRightSql+" AND SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetHeadFoot(string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "HeadFoot ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 PageHead,PageFoot FROM" + TabelName + "WHERE "+initRightSql+" AND SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
