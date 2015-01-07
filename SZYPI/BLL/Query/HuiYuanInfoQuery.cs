using System.Data;
using System.Text;
using Nandasoft;

namespace BLL.Query
{
    /// <summary>
    /// 会员信息查询
    /// 作者：姚东
    /// 时间：20100919
    /// </summary>
    public class HuiYuanInfoQuery
    {
        /// <summary>
        /// 获取所有会员信息
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <returns></returns>
        public DataTable GetInfoHuiYuan()
        {
            string sql = "SELECT ID, LoginAcc, LoginPWD, Name, Email, Tel, CreateTime, TotalPoint, RemainPoint, Status,StatusName,UserID " +
                " FROM UV_QS_HuiYuan_Point ";
                         
            return NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 获取指定会员信息
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <returns></returns>
        public DataTable GetInfoHuiYuan(string huiYuanID)
        {
            string sql = "SELECT ID, LoginAcc, LoginPWD, Name, Email, Tel, CreateTime, TotalPoint, RemainPoint, Status,StatusName,UserID " +
                " FROM UV_QS_HuiYuan_Point where ID='" + huiYuanID + "'";

            return NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 获取指定会员信息
        /// 作者：姚东
        /// 时间：20100925
        /// </summary>
        /// <returns></returns>
        public DataTable GetInfoHuiYuan(string account,string pwd)
        {
            string sql = "SELECT ID, LoginAcc, LoginPWD, Name, Email, Tel, CreateTime, TotalPoint, RemainPoint, Status,StatusName,UserID " +
                " FROM UV_QS_HuiYuan_Point where LoginAcc='" + account + "' and LoginPWD='" + pwd + "'";

            return NDDBAccess.Fill(sql);
        }
        

        /// <summary>
        /// 获取共享信息数据
        /// 作者：姚东
        /// 时间：20100919
        /// </summary>
        /// <returns></returns>
        public DataSet GetInfo(string Name, string Account, int Status, int PageIndex, int PageSize)
        {
            string sql = "SELECT * from UV_QS_HuiYuan_Point";
            if (!string.IsNullOrEmpty(Name))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND Name LIKE '%" + Name + "%'";
                }
                else
                {
                    sql += " WHERE Name LIKE '%" + Name + "%'";
                }
            }

            if (!string.IsNullOrEmpty(Account))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND LoginAcc LIKE '%" + Account + "%'";
                }
                else
                {
                    sql += " WHERE LoginAcc LIKE '%" + Account + "%'";
                }
            }

            if (Status != -1)
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND Status = " + Status + "";
                }
                else
                {
                    sql += " WHERE Status = " + Status + "";
                }
            }

            return Nandasoft.Helper.NDHelperWebControl.GetDataByPagerSP(sql.ToString(), PageIndex, PageSize, "", "");
        }        
    }
}