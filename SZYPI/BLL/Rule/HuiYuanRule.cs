using System.Data;
using BLL.Rule;
using Nandasoft;
using BLL.Entity;

namespace BLL.Rule
{
    /// <summary>
    /// ��Ա��ҵ����.
    /// ���ߣ�Ҧ��
    /// ʱ�䣺20100919
    /// </summary>
    public class HuiYuanRule
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public HuiYuanRule()
        {
        }


        /// <summary>
        /// Ĭ�ϲ�ѯ����
        /// </summary>
        public DataTable GetAllData()
        {
            return null;
        }


        /// <summary>
        /// Ĭ�ϲ�ѯ����
        /// </summary>
        /// <param name = "empID">Ա��ID</param>
        /// <param name = "pageIndex">��ǰҳ����</param>
        /// <param name = "pageSize">ÿҳ��¼��</param>
        public DataSet GetAllData(long empID,int pageIndex,int pageSize)
        {
            string sql = "";
            //return Nandasoft.Helper.NDHelperWebControl.GetDataByPagerSP(sql, pageIndex, pageSize, "", "");
            return null;
        }


        /// <summary>
        /// ����ҵ�����
        /// </summary>
        /// <param name = "entity">ʵ����</param>
        public void Add(HuiYuanEntity entity)
        {
            NDEntityCtl.Insert(entity);
        }


        /// <summary>
        /// �޸�ҵ�����
        /// </summary>
        /// <param name = "entity">ʵ����</param>
        public void Update(HuiYuanEntity entity)
        {
            NDEntityCtl.Update(entity);
        }


        /// <summary>
        /// ɾ��ҵ�����
        /// </summary>
        /// <param name = "entity">ʵ����</param>
        public void Delete(HuiYuanEntity entity)
        {
            NDEntityCtl.Delete(entity);
        }
        /// <summary>
        /// ɾ��ҵ�����
        /// </summary>
        /// <param name = "entity">ʵ����</param>
        public void Delete(long[] ids)
        {
        }

        /// <summary>
        /// �߼�ɾ��
        /// ���ߣ�Ҧ��
        /// ʱ�䣺20100919
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
        /// �����û�
        /// ���ߣ�Ҧ��
        /// ʱ�䣺20100919
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
        /// ���Ļ�Աʣ�����
        /// ���ߣ�Ҧ��
        /// ʱ�䣺20100920
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