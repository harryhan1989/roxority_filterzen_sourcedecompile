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
    [TableName("UT_SYS_Code")]
    public class CodeEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CodeEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="codeID"></param>
        public CodeEntity(long codeID)
            : this()
        {
            _codeID = codeID;
            this.SelectByPKeys();
        }

        private long _codeID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("CodeID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long CodeID
        {
            get
            {
                return _codeID;
            }
            set
            {
                _codeID = value;
            }
        }


        private long _codeGroupID = 0;
        /// <summary>
        /// 代码组ID
        /// </summary>
        [ColumnName("CodeGroupID")]
        [ColumnMasterName("CodeGroupID")]
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


        private string _codeName = "";
        /// <summary>
        /// 名称
        /// </summary>
        [ColumnName("CodeName")]
        public string CodeName
        {
            get
            {
                return _codeName;
            }
            set
            {
                _codeName = value;
            }
        }


        private string _codeValue = "";
        /// <summary>
        /// 备注
        /// </summary>
        [ColumnName("CodeValue")]
        public string CodeValue
        {
            get
            {
                return _codeValue;
            }
            set
            {
                _codeValue = value;
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
