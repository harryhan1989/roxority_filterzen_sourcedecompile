using System;
using System.Data;
using System.Collections.Generic;

using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// �˵��������ñ�ҵ����.
    /// ��д����: 2009-2-9.
    /// Ŀ��:
    /// </summary>
    public class MenuFunctionRule
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public MenuFunctionRule()
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
        public void Add(MenuFunctionEntity entity)
        {
            
            {
                NDEntityCtl.Insert(entity);
               
            }
        }


        /// <summary>
        /// �޸�ҵ�����
        /// </summary>
        /// <param name = "entity">ʵ����</param>
        public void Update(MenuFunctionEntity entity)
        {
            
            {
                NDEntityCtl.Update(entity);
               
            }
        }


        /// <summary>
        /// ɾ��ҵ�����
        /// </summary>
        /// <param name = "entity">ʵ����</param>
        public void Delete(MenuFunctionEntity entity)
        {
            
            {
                NDEntityCtl.Delete(entity);
               
            }
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