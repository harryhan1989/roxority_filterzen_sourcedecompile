using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// 目的: 菜单功能配置表实体类.
    /// 编写日期: 2009-2-9.
    /// </summary>
    [TableName("UT_SYS_MenuFunction")]
    public class MenuFunctionEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuFunctionEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fID"></param>
        public MenuFunctionEntity(long fID)
            : this()
        {
            _fID = fID;
            this.SelectByPKeys();
        }

        private long _fID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("FID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long FID
        {
            get
            {
                return _fID;
            }
            set
            {
                _fID = value;
            }
        }


        private long _menuID = 0;
        /// <summary>
        /// 菜单ID
        /// </summary>
        [ColumnName("MenuID")]
        [ColumnMasterName("MenuID")]
        public long MenuID
        {
            get
            {
                return _menuID;
            }
            set
            {
                _menuID = value;
            }
        }


        private string _fName = "";
        /// <summary>
        /// 功能名称
        /// </summary>
        [ColumnName("FName")]
        public string FName
        {
            get
            {
                return _fName;
            }
            set
            {
                _fName = value;
            }
        }


        private string _fCode = "";
        /// <summary>
        /// 代码名称
        /// </summary>
        [ColumnName("FCode")]
        public string FCode
        {
            get
            {
                return _fCode;
            }
            set
            {
                _fCode = value;
            }
        }


        private bool _isShow = false;
        /// <summary>
        /// 是否显示
        /// </summary>
        [ColumnName("IsShow")]
        public bool IsShow
        {
            get
            {
                return _isShow;
            }
            set
            {
                _isShow = value;
            }
        }


        private int _sortIndex = 0;
        /// <summary>
        /// 排序号
        /// </summary>
        [ColumnName("SortIndex")]
        public int SortIndex
        {
            get
            {
                return _sortIndex;
            }
            set
            {
                _sortIndex = value;
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
