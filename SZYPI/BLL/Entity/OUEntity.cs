using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// 目的: 组织机构部门表实体类.
    /// 编写日期: 2009-2-9.
    /// </summary>
    [TableName("UT_SYS_OU")]
    public class OUEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OUEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oUID"></param>
        public OUEntity(long oUID)
            : this()
        {
            _oUID = oUID;
            this.SelectByPKeys();
        }

        private long _oUID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("OUID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
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


        private string _oUName = "";
        /// <summary>
        /// 部门名称
        /// </summary>
        [ColumnName("OUName")]
        public string OUName
        {
            get
            {
                return _oUName;
            }
            set
            {
                _oUName = value;
            }
        }

        private string _oUNumber = "";
        /// <summary>
        /// 部门编号
        /// </summary>
        [ColumnName("OUNumber")]
        public string OUNumber
        {
            get
            {
                return _oUNumber;
            }
            set
            {
                _oUNumber = value;
            }
        }


        private long _oUParentID = 0;
        /// <summary>
        /// 父级部门唯一号
        /// </summary>
        [ColumnName("OUParentID")]
        public long OUParentID
        {
            get
            {
                return _oUParentID;
            }
            set
            {
                _oUParentID = value;
            }
        }


        private string _oUEmail = "";
        /// <summary>
        /// 部门电子邮件地址
        /// </summary>
        [ColumnName("OUEmail")]
        public string OUEmail
        {
            get
            {
                return _oUEmail;
            }
            set
            {
                _oUEmail = value;
            }
        }


        private string _oUPhone = "";
        /// <summary>
        /// 部门电话
        /// </summary>
        [ColumnName("OUPhone")]
        public string OUPhone
        {
            get
            {
                return _oUPhone;
            }
            set
            {
                _oUPhone = value;
            }
        }


        private string _oULinkman = "";
        /// <summary>
        /// 部门联系人
        /// </summary>
        [ColumnName("OULinkman")]
        public string OULinkman
        {
            get
            {
                return _oULinkman;
            }
            set
            {
                _oULinkman = value;
            }
        }


        private int _oUType = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("OUType")]
        public int OUType
        {
            get
            {
                return _oUType;
            }
            set
            {
                _oUType = value;
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


        private DateTime _addDate = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// 添加时间
        /// </summary>
        [ColumnName("AddDate")]
        public DateTime AddDate
        {
            get
            {
                return _addDate;
            }
            set
            {
                _addDate = value;
            }
        }


        private DateTime _updateDate = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// 更新时间
        /// </summary>
        [ColumnName("UpdateDate")]
        public DateTime UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                _updateDate = value;
            }
        }


        private bool _isDeleted = false;
        /// <summary>
        /// 删除标记(0:未删除,1:删除)
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

        private string _description = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("Description")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
    }

}


