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
    public class HotlineAnalysis_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";

        public DataTable SelectCodeType()
        {
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("SELECT DISTINCT [CName],[CodeType] FROM UT_CodeType WHERE codetype IN('Sex','AgeRange','MarriageStatus','District','VisitorType','ConsultType','OpinionType') ", 100);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
        }

        /// <summary>
        /// 获取需要分析的热线数据
        /// </summary>
        /// <param name="BeginDate">选择的开头日期</param>
        /// <param name="EndDate">选择的结束日期</param>
        /// <param name="StatisticsClass">选的的分析类型</param>
        /// <returns></returns>
        public DataTable GetStatisticsDetail(DateTime BeginDate, DateTime EndDate, string StatisticsClass)
        {
            StringBuilder sql = new StringBuilder("SELECT COUNT(@StatisticsClass) as CountStatisticsClass,(SELECT CName FROM UT_CodeInfo WHERE code=UV_QS_Hotline_Analyse." + StatisticsClass + " AND codetype=@StatisticsClass) AS StatisticsClass from UV_QS_Hotline_Analyse", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@StatisticsClass", StatisticsClass);
            //大于等于最小日期
            if (BeginDate != null)
            {
                sql.Append(" where datediff(day,Date,'" + BeginDate.ToString("yyyy-MM-dd") + "')<=0");
            }
            //小于等于最大日期
            if (EndDate != null)
            {
                sql.Append(" AND datediff(day,Date,'" + EndDate.ToString("yyyy-MM-dd") + "')>=0");
            }
            sql.Append(" GROUP BY ");
            sql.Append(StatisticsClass);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql),parameters);
        }

    }
}
