using System.Data;
using BLL.Rule;
using Nandasoft;
using BLL.Entity;

namespace BLL.Rule
{
    /// <summary>
    /// 会员表业务类.
    /// 作者：姚东
    /// 时间：20100919
    /// </summary>
    public class HuiYuanRule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HuiYuanRule()
        {
        }


        /// <summary>
        /// 默认查询方法
        /// </summary>
        public DataTable GetAllData()
        {
            return null;
        }


        /// <summary>
        /// 默认查询方法
        /// </summary>
        /// <param name = "empID">员工ID</param>
        /// <param name = "pageIndex">当前页索引</param>
        /// <param name = "pageSize">每页记录数</param>
        public DataSet GetAllData(long empID,int pageIndex,int pageSize)
        {
            string sql = "";
            //return Nandasoft.Helper.NDHelperWebControl.GetDataByPagerSP(sql, pageIndex, pageSize, "", "");
            return null;
        }


        /// <summary>
        /// 新增业务过程
        /// </summary>
        /// <param name = "entity">实体类</param>
        public void Add(HuiYuanEntity entity)
        {
            NDEntityCtl.Insert(entity);
        }


        /// <summary>
        /// 修改业务过程
        /// </summary>
        /// <param name = "entity">实体类</param>
        public void Update(HuiYuanEntity entity)
        {
            NDEntityCtl.Update(entity);
        }


        /// <summary>
        /// 删除业务过程
        /// </summary>
        /// <param name = "entity">实体类</param>
        public void Delete(HuiYuanEntity entity)
        {
            NDEntityCtl.Delete(entity);
        }
        /// <summary>
        /// 删除业务过程
        /// </summary>
        /// <param name = "entity">实体类</param>
        public void Delete(long[] ids)
        {
        }

        /// <summary>
        /// 逻辑删除
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <param name="huiYuanID"></param>
        /// <returns></returns>
        public int DelInfoHuiYuan(string huiYuanID)
        {
            string sql = "select * from UT_QS_HuiYuan_Point where  HuiYuanGuid='" + huiYuanID + "'";

            DataTable dt = NDDBAccess.Fill(sql);

            if (dt.Rows.Count > 0)
            {

                sql = "Update UT_QS_HuiYuan_Point set Status=0 where HuiYuanGuid='" + huiYuanID + "'";

                return NDDBAccess.ExecuteNonQuery(sql);
            }
            else
            {
                sql = string.Format("Insert into UT_QS_HuiYuan_Point(HuiYuanGuid,TotalPoint,RemainPoint,Status) values ('{0}',0,0,0)", huiYuanID);

                return NDDBAccess.ExecuteNonQuery(sql);
            }
        }

        /// <summary>
        /// 启用用户
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <param name="huiYuanID"></param>
        /// <returns></returns>
        public int StartInfoHuiYuan(string huiYuanID)
        {
            string sql = "select * from UT_QS_HuiYuan_Point where  HuiYuanGuid='" + huiYuanID + "'";

            DataTable dt = NDDBAccess.Fill(sql);

            if (dt.Rows.Count > 0)
            {

                sql = "Update UT_QS_HuiYuan_Point set Status=1 where HuiYuanGuid='" + huiYuanID + "'";

                return NDDBAccess.ExecuteNonQuery(sql);
            }
            else
            {
                sql = string.Format("Insert into UT_QS_HuiYuan_Point(HuiYuanGuid,TotalPoint,RemainPoint,Status) values ('{0}',0,0,1)", huiYuanID);

                return NDDBAccess.ExecuteNonQuery(sql);
            }
        }


        /// <summary>
        /// 更改会员剩余积分
        /// 作者：姚东
        /// 时间：20100920
        /// </summary>
        /// <param name="huiyuanID"></param>
        /// <param name="UsedPoint"></param>
        /// <returns></returns>
        public int UpdateRemainPoint(string huiYuanID, int UsedPoint)
        {
            string sql = "Update UT_QS_HuiYuan_Point set RemainPoint=RemainPoint-" + UsedPoint.ToString() + " where HuiYuanGuid='" + huiYuanID + "'";

            return NDDBAccess.ExecuteNonQuery(sql);
        }
    }
}