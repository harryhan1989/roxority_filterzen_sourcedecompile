using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// 目的: 菜单表实体类.
    /// 编写日期: 2009-2-9.
    /// </summary>
    [TableName("UT_SYS_Menu")]
    public class MenuEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="menuID"></param>
        public MenuEntity(long menuID)
            : this()
        {
            _menuID = menuID;
            this.SelectByPKeys();
        }

        private long _menuID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("MenuID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
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


        private long _parentMenuID = 0;
        /// <summary>
        /// 父级菜单ID
        /// </summary>
        [ColumnName("ParentMenuID")]
        public long ParentMenuID
        {
            get
            {
                return _parentMenuID;
            }
            set
            {
                _parentMenuID = value;
            }
        }


        private string _menuName = "";
        /// <summary>
        /// 菜单显示名称
        /// </summary>
        [ColumnName("MenuName")]
        public string MenuName
        {
            get
            {
                return _menuName;
            }
            set
            {
                _menuName = value;
            }
        }


        private string _navigateURL = "";
        /// <summary>
        /// 链接URL
        /// </summary>
        [ColumnName("NavigateURL")]
        public string NavigateURL
        {
            get
            {
                return _navigateURL;
            }
            set
            {
                _navigateURL = value;
            }
        }


        private string _imageURL = "";
        /// <summary>
        /// 图片URl
        /// </summary>
        [ColumnName("ImageURL")]
        public string ImageURL
        {
            get
            {
                return _imageURL;
            }
            set
            {
                _imageURL = value;
            }
        }


        private int _sortOrder = 0;
        /// <summary>
        /// 排序号
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


        private bool _isDisplay = false;
        /// <summary>
        /// 是否显示菜单
        /// </summary>
        [ColumnName("IsDisplay")]
        public bool IsDisplay
        {
            get
            {
                return _isDisplay;
            }
            set
            {
                _isDisplay = value;
            }
        }


        private bool _isLink = false;
        /// <summary>
        /// 是否连接
        /// </summary>
        [ColumnName("IsLink")]
        public bool IsLink
        {
            get
            {
                return _isLink;
            }
            set
            {
                _isLink = value;
            }
        }


        private int _type = 0;
        /// <summary>
        /// 菜单类型
        /// </summary>
        [ColumnName("Type")]
        public int Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
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
