using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// 目的: 实体类.
    /// 编写日期: 2009-6-3.
    /// </summary>
    [TableName("UT_SYS_CodeDetail")]
    public class CodeDetailEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CodeDetailEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="codeDetailID"></param>
        public CodeDetailEntity(long codeDetailID)
            : this()
        {
            _codeDetailID = codeDetailID;
            this.SelectByPKeys();
        }

        private long _codeDetailID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("CodeDetailID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long CodeDetailID
        {
            get
            {
                return _codeDetailID;
            }
            set
            {
                _codeDetailID = value;
            }
        }


        private long _codeID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("CodeID")]
        [ColumnMasterName("CodeID")]
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


        private string _codeDetailName = "";
        /// <summary>
        /// 名称
        /// </summary>
        [ColumnName("CodeDetailName")]
        public string CodeDetailName
        {
            get
            {
                return _codeDetailName;
            }
            set
            {
                _codeDetailName = value;
            }
        }


        private string _codeDetailValue = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("CodeDetailValue")]
        public string CodeDetailValue
        {
            get
            {
                return _codeDetailValue;
            }
            set
            {
                _codeDetailValue = value;
            }
        }


        private string _detailMemo = "";
        /// <summary>
        /// 说明
        /// </summary>
        [ColumnName("DetailMemo")]
        public string DetailMemo
        {
            get
            {
                return _detailMemo;
            }
            set
            {
                _detailMemo = value;
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
