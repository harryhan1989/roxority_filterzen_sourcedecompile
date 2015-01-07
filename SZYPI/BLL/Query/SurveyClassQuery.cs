using System.Data;
using Nandasoft;

namespace BLL.Query
{
    /// <summary>
    /// 问卷类型查询类
    /// 作者：姚东
    /// 时间：20101027
    /// </summary>
    public class SurveyClassQuery
    {
        /// <summary>
        /// 获取问卷类型
        /// 作者：姚东
        /// 时间：20101027
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public DataSet GetInfo(int PageIndex, int PageSize)
        {
            string sql = "SELECT CID,SurveyClassName,Sort,DefaultClass,case when DefaultClass='1' then '是' else '否' end as DefaultClassName,ParentID FROM UT_QS_SurveyClass ";            

            return Nandasoft.Helper.NDHelperWebControl.GetDataByPagerSP(sql.ToString(), PageIndex, PageSize, "", "");
        }

        /// <summary>
        /// 根据ID获取信息
        /// 作者：姚东
        /// 时间：20101027
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetInfoByID(string cid)
        {
            string sql = "SELECT CID,SurveyClassName,Sort,DefaultClass,ParentID FROM UT_QS_SurveyClass where cid=" + cid;

            return NDDBAccess.Fill(sql);
        }
    }
}