using System.Data;
using Nandasoft;

namespace BLL.Query
{
    public class PartnerQuery
    {
        /// <summary>
        /// 获取合作伙伴信息
        /// 作者：姚东
        /// 时间：20100920
        /// </summary>
        /// <returns></returns>
        public DataSet GetPartnerInfo(string name, int status, int PageIndex, int PageSize)
        {
            string sql = "SELECT ID, Name, URL,Status, sort, Image, " +
                         "       CASE Status WHEN 1 THEN '上线' WHEN '0' THEN '下线' END AS StatusName " +
                         "  FROM UT_QS_Partner ";

            if (!string.IsNullOrEmpty(name))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND Name like '%" + name + "%' ";
                }
                else
                {
                    sql += " WHERE Name like '%" + name + "%' ";
                }
            }

            if (!status.Equals(-1))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND Status=" + status.ToString();
                }
                else
                {
                    sql += " WHERE Status=" + status.ToString();
                }
            }

            sql += "  order by sort";

            return Nandasoft.Helper.NDHelperWebControl.GetDataByPagerSP(sql.ToString(), PageIndex, PageSize, "", "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetOnLinePartner()
        {
            string sql = "SELECT ID, Name, URL,Status, sort, Image, " +
                                   "       CASE Status WHEN 1 THEN '上线' WHEN '0' THEN '下线' END AS StatusName " +
                                   "  FROM UT_QS_Partner where status=1 order by sort";

            return NDDBAccess.Fill(sql);

        }
    }
}