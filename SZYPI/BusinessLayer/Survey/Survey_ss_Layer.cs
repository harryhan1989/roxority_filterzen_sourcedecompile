using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using System.Data;
using DBAccess.Entity;

namespace BusinessLayer.Survey
{
    public class Survey_ss_Layer
    {
        public string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();

        public SqlDataReader GetAnswerInfo(string SID) //获取答卷总数
        {
            string TabelName = " " + prefix + "AnswerInfo ";
            StringBuilder sql = new StringBuilder("SELECT COUNT(1) FROM  " + TabelName + "where SID=@SID", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetAnswerInfo1(string SID,string str) //防重复答卷
        {
            string TabelName = " " + prefix + "AnswerInfo ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ID FROM  " + TabelName + "where SID=@SID and ( @str )", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@str", str);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetAnswerInfoIP(string IP,string SID) //防重复答卷
        {
            string TabelName = " " + prefix + "AnswerInfo ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ID FROM " + TabelName + "WHERE IP=@IP AND SID=@SID", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@IP", IP);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyTableUID(string SID) //防重复答卷
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 UID FROM " + TabelName + "where SID=@SID", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemTable(string SID) //防重复答卷
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("SELECT IID,ItemType,OptionAmount,ParentID,DataFormatCheck FROM " + TabelName + "where SID=@SID ORDER BY PageNo,Sort", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetOptionTable(string SID) //防重复答卷
        {
            string TabelName = " " + prefix + "OptionTable ";
            StringBuilder sql = new StringBuilder("SELECT OID,IID FROM " + TabelName + "where SID=@SID", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyExpand(string SID) //防重复答卷
        {
            string TabelName = " " + prefix + "SurveyExpand ";
            StringBuilder sql = new StringBuilder("SELECT ExpandType,ExpandContent FROM " + TabelName + "where SID=@SID AND (ExpandType IN (2,3,4))", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public int UpdateSurveyTable(DateTime LastUpdate, string SID) //防重复答卷
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "set LastUpdate=@LastUpdate,AnswerAmount=AnswerAmount+1 where  SID=@SID ", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@LastUpdate", LastUpdate);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetSurveyTable(string SID) //防重复答卷
        {
            string TabelName = " " + prefix + "SurveyTable ";
            string TabelName1 = " " + prefix + "PageStyle ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 UID,Active,State,Par,MaxAnswerAmount,PageFileName,PageType,ToURL,Point,AdminSetAnswerAmount,AdminSetAnsweredAmount FROM " + TabelName + "S LEFT JOIN " + TabelName1 + "P ON S.EndPage=P.P_ID WHERE SID=@SID AND State=1", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyExpand1(string SID) //防重复答卷
        {
            string TabelName = " " + prefix + "SurveyExpand ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent FROM" + TabelName + "WHERE SID=@SID AND ExpandType=6", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetOptionTablePoint(string SID, string OID) //防重复答卷
        {
            string TabelName = " " + prefix + "OptionTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 Point FROM" + TabelName + "WHERE SID=@SID AND OID=@OID", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@OID", OID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetUserGUID(string UID) //防重复答卷
        {
            string TabelName = " " +  "UT_HuiYuan ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 id FROM" + TabelName + "WHERE UserID=@UID", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UID", UID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetSIntegral(string SID) //防重复答卷
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 Point FROM" + TabelName + "WHERE SID=@SID", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetHuiYuan_Point(string HuiYuanGuid) //防重复答卷
        {
            string TabelName = " " + prefix + "HuiYuan_Point ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 TotalPoint FROM" + TabelName + "WHERE HuiYuanGuid=@HuiYuanGuid", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@HuiYuanGuid", HuiYuanGuid);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
         

        public string GetUploadTargetConfig()
        {
            string UploadTarget = ConvertHelper.ConvertString(ConfigurationManager.AppSettings["UploadTarget"]);
            return UploadTarget;
        }

        public SqlDataReader ExcuteSql(string sql)
        {
            return DbHelperSQL.ExecuteReader(sql);
        }

        public void ExecProcedureByTrans(List<SqlTransEntity> sqlTransEntities)
        {
            DbHelperSQL.ExecProcedureByTrans(sqlTransEntities);
        }
    }
}
