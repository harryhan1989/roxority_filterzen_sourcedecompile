using System;
using System.Data;
using System.Collections.Generic;

using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// 群组菜单功能权限表业务类.
    /// 编写日期: 2009-2-9.
    /// 目的:
    /// </summary>
    public class MenuFunctionGroupRightRule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuFunctionGroupRightRule()
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
        public void Add(MenuFunctionGroupRightEntity entity)
        {
            
            {
                NDEntityCtl.Insert(entity);
               
            }
        }


        /// <summary>
        /// 修改业务过程
        /// </summary>
        /// <param name = "entity">实体类</param>
        public void Update(MenuFunctionGroupRightEntity entity)
        {
            
            {
                NDEntityCtl.Update(entity);
               
            }
        }


        /// <summary>
        /// 删除业务过程
        /// </summary>
        /// <param name = "entity">实体类</param>
        public void Delete(MenuFunctionGroupRightEntity entity)
        {
            
            {
                NDEntityCtl.Delete(entity);
               
            }
        }
        /// <summary>
        /// 删除业务过程
        /// </summary>
        /// <param name = "entity">实体类</param>
        public void Delete(long[] ids)
        {
        }
    }
}
