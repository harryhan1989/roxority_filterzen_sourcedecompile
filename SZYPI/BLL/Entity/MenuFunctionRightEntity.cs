using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// Ŀ��: �û��˵�����Ȩ�ޱ�ʵ����.
    /// ��д����: 2009-2-9.
    /// </summary>
    [TableName("UT_SYS_MenuFunctionRight")]
    public class MenuFunctionRightEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public MenuFunctionRightEntity()
        {
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="mFRight"></param>
        public MenuFunctionRightEntity(long mFRight)
            : this()
        {
            _mFRight = mFRight;
            this.SelectByPKeys();
        }

        private long _mFRight = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("MFRight")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long MFRight
        {
            get
            {
                return _mFRight;
            }
            set
            {
                _mFRight = value;
            }
        }


        private long _oUID = 0;
        /// <summary>
        /// ����ID
        /// </summary>
        [ColumnName("OUID")]
        public long OUID
        {
            get
            {
                return _oUID;
            }
            set
            {
                _oUID = value;
            }
        }


        private long _userID = 0;
        /// <summary>
        /// ��ԱID
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


        private long _fID = 0;
        /// <summary>
        /// �˵�����ID
        /// </summary>
        [ColumnName("FID")]
        [ColumnMasterName("FID")]
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

    }
}
