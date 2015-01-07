using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// Ŀ��: �û�Ȩ�ޱ�ʵ����.
    /// ��д����: 2009-2-9.
    /// </summary>
    [TableName("UT_SYS_Right")]
    public class RightEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public RightEntity()
        {
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="rightID"></param>
        public RightEntity(long rightID)
            : this()
        {
            _rightID = rightID;
            this.SelectByPKeys();
        }

        private long _rightID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("RightID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long RightID
        {
            get
            {
                return _rightID;
            }
            set
            {
                _rightID = value;
            }
        }


        private long _userID = 0;
        /// <summary>
        /// �û�ID
        /// </summary>
        [ColumnName("UserID")]
        [ColumnMasterName("UserID")]
        public long UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }


        private long _menuID = 0;
        /// <summary>
        /// �˵�ID
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

    }
}
