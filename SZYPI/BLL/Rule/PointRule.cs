using System.Data;
using BLL.Entity;
using Nandasoft;

namespace BLL.Rule
{
    /// <summary>
    /// ��Ա���ֱ�ҵ����.
    /// ��д����: 2010-10-15.
    /// Ŀ��:
    /// </summary>
    public class PointRule
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PointRule()
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
        public void Add(PointEntity entity)
        {
              NDEntityCtl.Insert(entity);
                
        }


        /// <summary>
        /// �޸�ҵ�����
        /// </summary>
        /// <param name = "entity">ʵ����</param>
        public void Update(PointEntity entity)
        {
            
                NDEntityCtl.Update(entity);
               
        }


        /// <summary>
        /// ɾ��ҵ�����
        /// </summary>
        /// <param name = "entity">ʵ����</param>
        public void Delete(PointEntity entity)
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
    }
}
