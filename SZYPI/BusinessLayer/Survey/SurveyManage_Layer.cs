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
    public class SurveyManage_Layer
    {
        string initRightSql = "";
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();

        /// <summary>
        /// 韩亮
        /// 20100927
        /// 假删除问卷表数据
        /// </summary>
        /// <param name="SID">问卷ID</param>
        public void UpdateSurveyTable(string SID)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("Update" + TabelName + " set IsDel=1 where SID=@SID", 30);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);

            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        /// <summary>
        /// 韩亮
        /// 20100927
        /// 根据问卷ID获取对应问卷的信息
        /// </summary>
        /// <param name="SID">问卷ID</param>
        public DataTable GetSurveyTable(string SID,string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT [UID],[SurveyName],[CreateDate],[EndDate],[SurveyPSW],[EndPage],[StartPage],[AnswerAmount],[State],[Active],[Recommend],[LastUpdate]",50);
                          sql.Append(",[MaxAnswerAmount],[TempPage],[Par],[ClassID],[Report],[ToURL],[ComplateMessage],[Point],[AnswerArea],[AdminSetAnswerAmount],[AdminSetAnsweredAmount]");
                          sql.Append(",[Lan],[SurveyType],[IsDel] From" + TabelName + "where SID=@SID and " + initRightSql);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);

           return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        /// <summary>
        /// 韩亮
        /// 20101103
        /// 根据用户ID获取用户类型
        /// </summary>
        /// <param name="UID">用户ID</param>
        public string GetUserType(string UID)
        {
            StringBuilder sql = new StringBuilder("select UserType from UT_SYS_User where UserID=@UID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteScalar(ConvertHelper.ConvertString(sql), parameters);
        }

        /// <summary>
        /// 韩亮
        /// 20101103
        /// 问卷审批
        /// </summary>
        /// <param name="UID">用户ID</param>
        public int GetUserType(string SID,string Status)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " set ApprovalStaus=@Status where SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@Status", Status);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
