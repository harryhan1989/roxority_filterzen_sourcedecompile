
using Nandasoft;
using System.Data;
namespace BLL.Query
{
    /// <summary>
    /// 首页数据查询业务类
    /// 作者：姚东
    /// 时间：20100925
    /// </summary>
    public class IndexQuery
    {
        /// <summary>
        /// 获取会员数量
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <returns></returns>
        public int GetHuiYuanAmount()
        {
            string sql = "SELECT count(1) FROM UV_QS_HuiYuan_Point where status=1 ";

            return NDConvert.ToInt32(NDDBAccess.Fill(sql).Rows[0][0]);
        }

        /// <summary>
        /// 获取调查问卷的数量
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <returns></returns>
        public int GetSurveyAmount()
        {
            string sql = @"SELECT count(1) FROM UV_QS_SurveyTable WHERE State=1 and Active=1 and 
                datediff(day,getdate(),isnull(EndDate,'2199-12-31'))>=0  
                and (MaxAnswerAmount=0 or MaxAnswerAmount<>0 and AnswerAmount<MaxAnswerAmount) and ApprovalStaus=1";

            return NDConvert.ToInt32(NDDBAccess.Fill(sql).Rows[0][0]);
        }

        /// <summary>
        /// 获取答卷的数量
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <returns></returns>
        public int GetAnswerAmount()
        {
            string sql = "SELECT count(1) FROM UT_QS_AnswerInfo ";

            return NDConvert.ToInt32(NDDBAccess.Fill(sql).Rows[0][0]);
        }

        /// <summary>
        /// 获取主要的一些调查问卷信息
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <returns></returns>
        public DataTable GetSurveyList()
        {
            string sql = @"select top 4 st.sID,
                case when len(st.SurveyName) <= 34 then 
                st.SurveyName else
                left(st.SurveyName,34)+'......' end as SurveyName,                
                convert(nvarchar(max),pt.PageContent) as PageContent,
                u.UserName,st.CreateDate,st.AnswerAmount,st.Point
                from UT_QS_SurveyTable as st
                inner join 
                UT_QS_PageTable as pt
                on st.sid = pt.sid
                and pt.PageNo=1
                inner join UT_Sys_User as u
                on st.uid = u.userid
                where st.IsDel=0 and st.state=1 and st.active=1 and datediff(day,getdate(),isnull(st.EndDate,'2199-12-31'))>=0 
                and (st.MaxAnswerAmount=0 or st.MaxAnswerAmount<>0 and st.AnswerAmount<st.MaxAnswerAmount) and ApprovalStaus=1 
                order by st.createdate desc";

            DataTable dt = NDDBAccess.Fill(sql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string str = CommonHelp.StripHTML(dr["PageContent"].ToString());

                    if (str.Length > 120)
                    {
                        dr["PageContent"] = str.Substring(0, 120) + "...";
                    }
                    else
                    {
                        dr["PageContent"] = str;
                    }                    
                }
            }

            return dt;
        }

        /// <summary>
        /// 获取热门问卷部分的调查问卷信息
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <returns></returns>
        public DataTable GetHotSurveyList()
        {
            string sql = @"select top 5 st.sID,
                case when len(st.SurveyName) <= 20 then 
                st.SurveyName else
                left(st.SurveyName,20)+'......' end as SurveyName
                ,pt.PageContent,u.UserName,st.CreateDate,st.AnswerAmount,st.Point
                from UT_QS_SurveyTable as st
                inner join 
                UT_QS_PageTable as pt
                on st.sid = pt.sid
                and pt.PageNo=1
                inner join UT_Sys_User as u
                on st.uid = u.userid
                where st.IsDel=0 and st.state=1 and st.active=1 and datediff(day,getdate(),isnull(st.EndDate,'2199-12-31'))>=0 
                and (st.MaxAnswerAmount=0 or st.MaxAnswerAmount<>0 and st.AnswerAmount<st.MaxAnswerAmount) and ApprovalStaus=1 
                order by st.answerAmount desc";

            return NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 获取推荐问卷的信息
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecommendSurveyList()
        {
            string sql = @"select top 1 st.sID,
                case when len(st.SurveyName) <= 18 then 
                st.SurveyName else
                left(st.SurveyName,18)+'......' end as SurveyName,               
                convert(nvarchar(max),pt.PageContent) as PageContent
                ,u.UserName,st.CreateDate,st.AnswerAmount,st.Point
                from UT_QS_SurveyTable as st
                inner join 
                UT_QS_PageTable as pt
                on st.sid = pt.sid
                and pt.PageNo=1
                inner join UT_Sys_User as u
                on st.uid = u.userid
                where st.IsDel=0 and st.state=1 and st.active=1 and st.Recommend=1 and datediff(day,getdate(),isnull(st.EndDate,'2199-12-31'))>=0 
                and (st.MaxAnswerAmount=0 or st.MaxAnswerAmount<>0 and st.AnswerAmount<st.MaxAnswerAmount) and ApprovalStaus=1 
                order by st.point,st.createdate desc";

            DataTable dt = NDDBAccess.Fill(sql);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string str = CommonHelp.StripHTML(dr["PageContent"].ToString());

                        if (str.Length > 80)
                        {
                            dr["PageContent"] = str.Substring(0, 80) + "...";
                        }
                        else
                        {
                            dr["PageContent"] = str;
                        }
                    }
                }

                return dt;
            }
            else
            {
                sql = @"select top 1 st.sID,
                case when len(st.SurveyName) <= 18 then 
                st.SurveyName else
                left(st.SurveyName,18)+'......' end as SurveyName,                
                convert(nvarchar(max),pt.PageContent) as PageContent,
                u.UserName,st.CreateDate,st.AnswerAmount,st.Point
                from UT_QS_SurveyTable as st
                inner join 
                UT_QS_PageTable as pt
                on st.sid = pt.sid
                and pt.PageNo=1
                inner join UT_Sys_User as u
                on st.uid = u.userid
                where st.IsDel=0 and st.state=1 and st.active=1 and datediff(day,getdate(),isnull(st.EndDate,'2199-12-31'))>=0 
                and (st.MaxAnswerAmount=0 or st.MaxAnswerAmount<>0 and st.AnswerAmount<st.MaxAnswerAmount) and ApprovalStaus=1 
                order by st.point,st.answerAmount,st.createdate desc";

                dt = NDDBAccess.Fill(sql);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string str = CommonHelp.StripHTML(dr["PageContent"].ToString());

                        if (str.Length > 80)
                        {
                            dr["PageContent"] = str.Substring(0, 80) + "...";
                        }
                        else
                        {
                            dr["PageContent"] = str;
                        }
                    }
                }

                return dt;
            }
        }

        /// <summary>
        /// 小调查数据源
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <returns></returns>
        public DataTable GetDiaoChaInfo()
        {
            string sql = "select top 1 * from ut_diaocha where ishome=1 order by edittime asc";

            DataTable dt = NDDBAccess.Fill(sql);

            return dt;
        }

        /// <summary>
        /// 判断会员是否已经回答过该问卷
        /// 作者：姚东
        /// 时间：20101102
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool CheckHasAnswered(string sid, string uid)
        {
            string sql = string.Format("select * from UT_QS_AnswerInfo where AnswerUserKind=0 and SID='{0}' and uid='{1}'", sid, uid);

            DataTable dt = NDDBAccess.Fill(sql);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}