using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;
using System.Data;

namespace BLL.Query
{
    /// <summary>
    /// 礼品查询规则类
    /// 作者：姚东
    /// 时间：20100920
    /// </summary>
    public class GiftsQuery
    {
        /// <summary>
        /// 获取礼品信息
        /// 作者：姚东
        /// 时间：20100920
        /// </summary>
        /// <returns></returns>
        public DataSet GetGifts(string name, int status, int PageIndex, int PageSize)
        {
            string sql = "SELECT ID, GiftName, NeedPoint,RemainAmount, Description, Picture, Status, " +
                         "       CASE Status WHEN 1 THEN '开放' WHEN '0' THEN '下架' END AS StatusName, " +
                         " CreateTime,UpdateTime FROM UT_QS_Gifts ";

            if (!string.IsNullOrEmpty(name))
            {
                if(sql.ToLower().Contains("where"))
                {
                    sql += " AND GiftName like '%" + name + "%' ";
                }
                else
                {
                    sql += " WHERE GiftName like '%" + name + "%' ";
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
        
            return Nandasoft.Helper.NDHelperWebControl.GetDataByPagerSP(sql.ToString(), PageIndex, PageSize, "", "");
        }

        /// <summary>
        /// 获取所有上架的礼品信息
        /// 作者：姚东
        /// 时间：20101014
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllGifts()
        {
            string sql = @"SELECT ID, 
                GiftName , 
                NeedPoint,RemainAmount, Description, Picture, Status, " +
                                    " CreateTime,UpdateTime FROM UT_QS_Gifts where status=1";
            return NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 获取热门礼品
        /// 作者：姚东
        /// 时间：20101014
        /// </summary>
        /// <returns></returns>
        public DataTable GetHotGifts()
        {
            string sql = @"select top 3 a.id,
                    a.giftname,
                    a.needpoint,a.RemainAmount,a.description,a.status,a.createtime,a.updatetime,b.count 
                    from UT_QS_Gifts as a
                    inner join 
                    (
                    SELECT g.ID, count(g.ID) as count
                    FROM UT_QS_Gifts as g
                    left outer join UT_QS_Gifts_Exchange e
                    on g.id = e.giftguid
                    group by g.ID
                    ) as b
                    on a.id=b.id
                    where a.status=1
                    order by b.id desc,a.createtime desc";
            return NDDBAccess.Fill(sql);
        }
    }
}