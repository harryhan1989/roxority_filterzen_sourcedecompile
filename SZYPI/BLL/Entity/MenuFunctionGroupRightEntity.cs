using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// Ŀ��: Ⱥ��˵�����Ȩ�ޱ�ʵ����.
    /// ��д����: 2009-2-9.
    /// </summary>
    [TableName("UT_SYS_MenuFunctionGroupRight")]
    public class MenuFunctionGroupRightEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public MenuFunctionGroupRightEntity()
        {
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="mFGRightID"></param>
        public MenuFunctionGroupRightEntity(long mFGRightID)
            : this()
        {
            _mFGRightID = mFGRightID;
            this.SelectByPKeys();
        }

        private long _mFGRightID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("MFGRightID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long MFGRightID
        {
            get
            {
                return _mFGRightID;
            }
            set
            {
                _mFGRightID = value;
            }
        }


        private long _groupID = 0;
        /// <summary>
        /// Ⱥ��ID
        /// </summary>
        [ColumnName("GroupID")]
        [ColumnMasterName("GroupID")]
        public long GroupID
        {
            get
            {
                return _groupID;
            }
            set
            {
                _groupID = value;
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
