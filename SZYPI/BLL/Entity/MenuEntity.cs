using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// Ŀ��: �˵���ʵ����.
    /// ��д����: 2009-2-9.
    /// </summary>
    [TableName("UT_SYS_Menu")]
    public class MenuEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public MenuEntity()
        {
        }


        /// <summary>
        /// ���캯��
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
        /// �����˵�ID
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
        /// �˵���ʾ����
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
        /// ����URL
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
        /// ͼƬURl
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
        /// �����
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
        /// �Ƿ���ʾ�˵�
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
        /// �Ƿ�����
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
        /// �˵�����
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
        /// ɾ�����
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
