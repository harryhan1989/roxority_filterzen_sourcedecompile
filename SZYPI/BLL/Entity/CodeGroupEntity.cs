using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// 目的: 实体类.
    /// 编写日期: 2009-3-6.
    /// </summary>
    [TableName("UT_SYS_CodeGroup")]
    public class CodeGroupEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CodeGroupEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="codeGroupID"></param>
        public CodeGroupEntity(long codeGroupID)
            : this()
        {
            _codeGroupID = codeGroupID;
            this.SelectByPKeys();
        }

        private long _codeGroupID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("CodeGroupID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long CodeGroupID
        {
            get
            {
                return _codeGroupID;
            }
            set
            {
                _codeGroupID = value;
            }
        }


        private string _codeGroupKey = "";
        /// <summary>
        /// 编码
        /// </summary>
        [ColumnName("CodeGroupKey")]
        public string CodeGroupKey
        {
            get
            {
                return _codeGroupKey;
            }
            set
            {
                _codeGroupKey = value;
            }
        }


        private string _codeGroupName = "";
        /// <summary>
        /// 中文名称
        /// </summary>
        [ColumnName("CodeGroupName")]
        public string CodeGroupName
        {
            get
            {
                return _codeGroupName;
            }
            set
            {
                _codeGroupName = value;
            }
        }


        private int _sortOrder = 0;
        /// <summary>
        /// 排序顺序号
        /// </summary>
        [ColumnName("SortOrder")]
        public int SortOrder
        {
            get
            {
                return _sortOrder;
            }
            set
            {
                _sortOrder = value;
            }
        }


        private string _memo = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("Memo")]
        public string Memo
        {
            get
            {
                return _memo;
            }
            set
            {
                _memo = value;
            }
        }


        private bool _isDeleted = false;
        /// <summary>
        /// 删除标记
        /// </summary>
        [ColumnName("IsDeleted")]
        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
            set
            {
                _isDeleted = value;
            }
        }

    }
}
