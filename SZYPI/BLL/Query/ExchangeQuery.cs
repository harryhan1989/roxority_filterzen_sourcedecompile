
using System.Data;
namespace BLL.Query
{
    public class ExchangeQuery
    {
        /// <summary>
        /// 获取礼品信息
        /// 作者：姚东
        /// 时间：20100920
        /// </summary>
        /// <returns></returns>
        public DataSet GetExchangeInfo(string huiyuanAccount, string huiyuanName, string giftName, int status, int PageIndex, int PageSize)
        {
            string sql = "SELECT * from UV_QS_Gifts_Exchange ";

            //会员账号
            if (!string.IsNullOrEmpty(huiyuanAccount))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND HuiYuanAccount like '%" + huiyuanAccount + "%' ";
                }
                else
                {
                    sql += " WHERE HuiYuanAccount like '%" + huiyuanAccount + "%' ";
                }
            }

            //会员名称
            if (!string.IsNullOrEmpty(huiyuanName))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND HuiYuanName like '%" + huiyuanName + "%' ";
                }
                else
                {
                    sql += " WHERE HuiYuanName like '%" + huiyuanName + "%' ";
                }
            }

            //礼品名称
            if (!string.IsNullOrEmpty(giftName))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND GiftName like '%" + giftName + "%' ";
                }
                else
                {
                    sql += " WHERE GiftName like '%" + giftName + "%' ";
                }
            }

            //兑换状态
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

            return Nandasoft.Helper.NDHelperWebControl.GetDataByPagerSP(sql.ToString(), PageIndex, PageSize, "", "");
        }

        /// <summary>
        /// 获取最新的兑换记录
        /// 作者：姚东
        /// 时间：20101014
        /// </summary>
        /// <returns></returns>
        public DataTable GetNewExchangeList()
        {
            string sql = @"select top 10 *,convert(nvarchar(10),isnull(exchangetime,applytime),120) as date 
                from UV_QS_Gifts_Exchange where status=2 order by isnull(exchangetime,applytime) desc";

            return Nandasoft.NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 获取兑换记录的SQL文
        /// 作者：姚东
        /// 时间：20101018
        /// </summary>
        /// <returns></returns>
        public string GetExchangeListSql()
        {
            string sql = @" select * from UV_QS_Gifts_Exchange where status=2";

            return sql;
        }

        /// <summary>
        /// 获取我的兑换记录的SQL文
        /// 作者：姚东
        /// 时间：20101018
        /// </summary>
        /// <returns></returns>
        public string GetMyExchangeListSql(string huiYuanGuid)
        {
            string sql = @" select * from UV_QS_Gifts_Exchange where HuiYuanGuid='" + huiYuanGuid + "'";

            return sql;
        }
    }
}