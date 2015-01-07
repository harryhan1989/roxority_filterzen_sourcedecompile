using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// 目的: 群组菜单功能权限表实体类.
    /// 编写日期: 2009-2-9.
    /// </summary>
    [TableName("UT_SYS_MenuFunctionGroupRight")]
    public class MenuFunctionGroupRightEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuFunctionGroupRightEntity()
        {
        }


        /// <summary>
        /// 构造函数
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
        /// 群组ID
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
        /// 菜单ID
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
        /// 菜单功能ID
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
