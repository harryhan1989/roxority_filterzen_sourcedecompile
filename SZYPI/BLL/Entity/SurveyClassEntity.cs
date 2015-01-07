using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// 目的: 实体类.
    /// 编写日期: 2010-10-27.
    /// </summary>
    [TableName("UT_QS_SurveyClass")]
    public class SurveyClassEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SurveyClassEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cID"></param>
        public SurveyClassEntity(long cID)
            : this()
        {
            _cID = cID;
            this.SelectByPKeys();
        }

        private long _cID = 0;
        /// <summary>
        /// 分类ID
        /// </summary>
        [ColumnName("CID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long CID
        {
            get
            {
                return _cID;
            }
            set
            {
                _cID = value;
            }
        }


        private string _surveyClassName = "";
        /// <summary>
        /// 问卷分类名称
        /// </summary>
        [ColumnName("SurveyClassName")]
        public string SurveyClassName
        {
            get
            {
                return _surveyClassName;
            }
            set
            {
                _surveyClassName = value;
            }
        }


        private int _sort = 0;
        /// <summary>
        /// 分类
        /// </summary>
        [ColumnName("Sort")]
        public int Sort
        {
            get
            {
                return _sort;
            }
            set
            {
                _sort = value;
            }
        }


        private bool _defaultClass = false;
        /// <summary>
        /// 默认分类
        /// </summary>
        [ColumnName("DefaultClass")]
        public bool DefaultClass
        {
            get
            {
                return _defaultClass;
            }
            set
            {
                _defaultClass = value;
            }
        }


        private long _parentID = 0;
        /// <summary>
        /// 父ID
        /// </summary>
        [ColumnName("ParentID")]
        public long ParentID
        {
            get
            {
                return _parentID;
            }
            set
            {
                _parentID = value;
            }
        }

    }
}
