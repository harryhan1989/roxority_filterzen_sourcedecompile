using System;

namespace WebUI
{
    /// <summary>
    /// 操作枚举
    /// </summary>
    public enum OperationEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        DEFAULT = 0,

        /// <summary>
        /// 新增
        /// </summary>
        INSERT = 1,

        /// <summary>
        /// 修改
        /// </summary>
        UPDATE = 2,

        /// <summary>
        /// 删除
        /// </summary>
        DELETE = 3,

        /// <summary>
        /// 查看
        /// </summary>
        VIEW = 4,
        
        /// <summary>
        /// 查询
        /// </summary>
        SEARCH = 5,

        /// <summary>
        /// 高级查询
        /// </summary>
        ADVSEARCH = 6,

        /// <summary>
        /// 审核
        /// </summary>
        Auth = 7,

    }
}