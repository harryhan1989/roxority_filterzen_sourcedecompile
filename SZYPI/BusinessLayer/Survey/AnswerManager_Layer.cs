using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Business.Helper;
using DBAccess;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer.Survey
{
    public class AnswerManager_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public DataSet GetAnswerInfoBySID(string SID, string AnswerName, int MinAnswerRecord, int MaxAnswerRecord, int MinUseTime, int MaxUseTime, int ApprovalStaus, DateTime BeginDate, int PageIndex, int PageSize, DateTime EndDate)
        {
            StringBuilder sql = new StringBuilder("SELECT ID,Anonymity,UID,IP,SubmittIime,Point,SID,AnswerGUID,ApprovalStaus,AnswerUserKind,UserName,SurveyName,SPar,SecondTime FROM" + " UV_QS_AnswerInfo " + "WHERE SID="+SID+" AND ApprovalStaus IN(0,1) ", 50);

            //答卷者
            if (!string.IsNullOrEmpty(AnswerName))
            {
                sql.Append(" AND UserName like '%" + AnswerName + "%' ");
            }
            //大于等于最小得分
            if (!MinAnswerRecord.Equals(-1))
            {
                sql.Append(" AND Point >=" + MinAnswerRecord + " ");
            }
            //小于等于最大得分
            if (!MaxAnswerRecord.Equals(-1))
            {
                sql.Append(" AND Point <=" + MinAnswerRecord + " ");
            }
            //大于等于最小时间
            if (!MinUseTime.Equals(-1))
            {
                sql.Append(" AND SecondTime >=" + MinUseTime + " ");
            }
            //小于等于最大时间
            if (!MaxUseTime.Equals(-1))
            {
                sql.Append(" AND SecondTime <=" + MaxUseTime + " ");
            }
            //审批状态
            if (!ApprovalStaus.Equals(-1))
            {
                sql.Append(" AND ApprovalStaus =" + ApprovalStaus + " ");
            }
            //大于等于最小日期
            if (BeginDate != null)
            {
                sql.Append(" AND datediff(day,SubmittIime,'" + BeginDate.ToString("yyyy-MM-dd") + "')<=0");
            }
            //小于等于最大日期
            if (EndDate != null)
            {
                sql.Append(" AND datediff(day,SubmittIime,'" + EndDate.ToString("yyyy-MM-dd") + "')>=0");
            }


            sql.Append("ORDER BY SID DESC,ApprovalStaus ASC,SubmittIime DESC");

            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@sqlstr", sql.ToString());
            parameters[1] = new SqlParameter("@PageSize",PageSize);
            parameters[2] = new SqlParameter("@pageindex", PageIndex);
            //return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
            //return DbHelperSQL.ExecProcedure(parameters, "Z_UP_GetDataProc");
            return DbHelperSQL.RunProcedureGetDataSet("Z_UP_GetDataProc", parameters);
        }

        public DataTable GetAnswerInfo()
        {
            StringBuilder sql = new StringBuilder("SELECT ID,Anonymity,UID,IP,SubmittIime,Point,SID,AnswerGUID,ApprovalStaus,AnswerUserKind,UserName,SurveyName,SPar,SecondTime FROM" + " UV_QS_AnswerInfo " + "ORDER BY SID DESC,ApprovalStaus ASC", 50);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
        }

        public DataTable GetHuiyuanUserInfo(string UserID)
        {
            StringBuilder sql = new StringBuilder("SELECT LoginAcc FROM" + " UT_HuiYuan " + "UserID=@UserID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UserID", UserID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetSparBySID(string SID)
        {
            string tableName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT Par FROM" + tableName + "where SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public int SetApprovalStaus(string ApprovalStaus, string AnswerGUID)
        {
            string tableName = " " + prefix + "AnswerInfo ";
            StringBuilder sql = new StringBuilder("UPDATE " + tableName + "SET ApprovalStaus=@ApprovalStaus WHERE AnswerGUID=@AnswerGUID and ApprovalStaus<>@ApprovalStaus", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@ApprovalStaus", ApprovalStaus);
            parameters[1] = new SqlParameter("@AnswerGUID", AnswerGUID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int DeleteAnswer(string AnswerGUID)
        {
            string tableName = " " + prefix + "AnswerInfo ";
            string tableName1 = " " + prefix + "AnswerDetail ";
            StringBuilder sql = new StringBuilder("Delete from" + tableName + "WHERE AnswerGUID=@AnswerGUID", 50);
            StringBuilder sql1 = new StringBuilder("Delete from" + tableName1 + "WHERE AnswerGUID=@AnswerGUID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@AnswerGUID", AnswerGUID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql1), parameters) + DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int UpdateAnswerNum(int num, string sid)
        {
            string tableName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("update " + tableName + "set AnswerAmount=AnswerAmount-@num WHERE sid=@sid", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@num", num);
            parameters[1] = new SqlParameter("@sid", sid);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public string GetManager(string UserID)
        {
            StringBuilder sql = new StringBuilder("select Account from  " + " UT_SYS_User " + " where UserID=@UserID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UserID", UserID);
            return DbHelperSQL.ExecuteScalar(sql.ToString(), parameters);
        }

        public int ExcuteSql(string sql, SqlParameter[] parameters)
        {
            return DbHelperSQL.ExecuteSql(sql, parameters);
        }
    }
}
