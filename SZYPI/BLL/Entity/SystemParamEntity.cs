using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// 目的: 实体类.
    /// 编写日期: 2009-3-5.
    /// </summary>
    [TableName("UT_SYS_SystemParam")]
    public class SystemParamEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemParamEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramID"></param>
        public SystemParamEntity(long paramID)
            : this()
        {
            _paramID = paramID;
            this.SelectByPKeys();
        }

        private long _paramID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("ParamID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long ParamID
        {
            get
            {
                return _paramID;
            }
            set
            {
                _paramID = value;
            }
        }


        private string _paramName = "";
        /// <summary>
        /// 参数中文名称
        /// </summary>
        [ColumnName("ParamName")]
        public string ParamName
        {
            get
            {
                return _paramName;
            }
            set
            {
                _paramName = value;
            }
        }


        private string _englishName = "";
        /// <summary>
        /// 英文名称
        /// </summary>
        [ColumnName("EnglishName")]
        public string EnglishName
        {
            get
            {
                return _englishName;
            }
            set
            {
                _englishName = value;
            }
        }


        private string _paramValue = "";
        /// <summary>
        /// 参数值
        /// </summary>
        [ColumnName("ParamValue")]
        public string ParamValue
        {
            get
            {
                return _paramValue;
            }
            set
            {
                _paramValue = value;
            }
        }


        private string _paramType = "";
        /// <summary>
        /// 参数类型（用于区分参数类别）
        /// </summary>
        [ColumnName("ParamType")]
        public string ParamType
        {
            get
            {
                return _paramType;
            }
            set
            {
                _paramType = value;
            }
        }


        private string _description = "";
        /// <summary>
        /// 描述
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
