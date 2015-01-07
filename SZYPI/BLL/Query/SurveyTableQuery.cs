
using System.Data;
using System;
using BusinessLayer.InitSqlRight;
namespace BLL.Query
{
    public class SurveyTableQuery
    {
        /// <summary>
        /// 调查表信息
        /// 作者：姚东
        /// 时间：20100922
        /// </summary>
        /// <returns></returns>
        public DataSet GetSurveyInfo(string surveyName, int state, int active, int ClassID, DateTime beginDate, DateTime endDate, int PageIndex, int PageSize, string UserID)
        {
            string sql = "SELECT * from UV_QS_SurveyTable ";

            //根据用户筛选
            if (sql.ToLower().Contains("where"))
            {
                sql += GetRightUID(" UID=" + UserID, UserID);
            }
            else
            {
                sql += " WHERE "+GetRightUID(" UID=" + UserID, UserID);
            }

            //问卷名称
            if (!string.IsNullOrEmpty(surveyName))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND surveyName like '%" + surveyName + "%' ";
                }
                else
                {
                    sql += " WHERE surveyName like '%" + surveyName + "%' ";
                }
            }

            //编辑状态
            if (!state.Equals(-1))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND state = " + state.ToString();
                }
                else
                {
                    sql += " WHERE state = " + state.ToString();
                }
            }

            //活动状态
            if (!active.Equals(-1))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND active = " + active.ToString();
                }
                else
                {
                    sql += " WHERE active = " + active.ToString();
                }
            }

            //问卷类型
            if (!ClassID.Equals(-1))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND ClassID=" + ClassID.ToString();
                }
                else
                {
                    sql += " WHERE ClassID=" + ClassID.ToString();
                }
            }

            //开始时间
            if (beginDate != null)
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND datediff(day,createdate,'" + beginDate.ToString("yyyy-MM-dd") + "')<=0";
                }
                else
                {
                    sql += " WHERE datediff(day,createdate,'" + beginDate.ToString("yyyy-MM-dd") + "')<=0";
                }
            }

            //结束时间
            if (endDate != null)
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND datediff(day,createdate,'" + endDate.ToString("yyyy-MM-dd") + "')>=0";
                }
                else
                {
                    sql += " WHERE datediff(day,createdate,'" + endDate.ToString("yyyy-MM-dd") + "')>=0";
                }
            }
            sql += " order by sid desc";

            return Nandasoft.Helper.NDHelperWebControl.GetDataByPagerSP(sql.ToString(), PageIndex, PageSize, "", "");
        }

        public string GetRightUID(string beforeSql,string UID)
        {
            string sql = "";
            string GID =new InitRight().GetGID(UID);
            string UserGroup = new InitRight().GetGIDList(GID);
            string UIDList = new InitRight().GetUIDList(UserGroup);
            sql += "(" + beforeSql;
            sql += " or ";
            sql += " UID IN (";
            sql += UIDList;
            sql += "))";
            return sql;
        }

        /// <summary>
        /// 获取问卷类型
        /// 作者：姚东
        /// 时间：20100922
        /// </summary>
        /// <returns></returns>
        public DataTable GetClassID()
        {
            string sql = "SELECT * from UT_QS_SurveyClass ";

            return Nandasoft.NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 获取我的问卷的数据源
        /// 作者：姚东
        /// 时间：20100928
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public DataTable GetMySurvey(string UID, string SurveyType)
        {
            string sql = string.Empty;
            if (SurveyType == "-1")
            {
                sql = "SELECT * FROM UV_QS_MySurveyTable where UID=" + UID;
            }
            else
            {
                sql = "SELECT * FROM UV_QS_MySurveyTable where UID='" + UID + "' and SurveyType = '" + SurveyType + "'";
            }

            return Nandasoft.NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 获取我的问卷的数据源用到的SQL文
        /// 作者：姚东
        /// 时间：20101012
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public string GetMySurveySql(string UID, string SurveyType)
        {
            string sql = string.Empty;
            if (SurveyType == "-1")
            {
                sql = "SELECT * FROM UV_QS_MySurveyTable where UID=" + UID;
            }
            else
            {
                sql = "SELECT * FROM UV_QS_MySurveyTable where UID='" + UID + "' and SurveyType = '" + SurveyType + "'";
            }

            return sql;
        }

        /// <summary>
        /// 获取问卷的数据源用到的SQL文
        /// 作者：姚东
        /// 时间：20101012
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public string GetSurveySql(string SurveyType)
        {
            string sql = string.Empty;
            if (SurveyType == "-1")
            {
                sql = @"SELECT * FROM UV_QS_SurveyTable where
                    state=1 and active=1 and datediff(day,getdate(),isnull(EndDate,'2199-12-31'))>=0 
                    and (MaxAnswerAmount=0 or MaxAnswerAmount<>0 and AnswerAmount<MaxAnswerAmount) and ApprovalStaus=1 ";
            }
            else
            {
                sql = @"SELECT * FROM UV_QS_SurveyTable where 
                    state=1 and active=1 and datediff(day,getdate(),isnull(EndDate,'2199-12-31'))>=0 
                    and (MaxAnswerAmount=0 or MaxAnswerAmount<>0 and AnswerAmount<MaxAnswerAmount) and ApprovalStaus = 1 and SurveyType = '" + SurveyType + "'";
            }

            return sql;
        }


    }
}