using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// Ŀ��: ʵ����.
    /// ��д����: 2010-10-27.
    /// </summary>
    [TableName("UT_QS_SurveyClass")]
    public class SurveyClassEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public SurveyClassEntity()
        {
        }


        /// <summary>
        /// ���캯��
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
        /// ����ID
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
        /// �ʾ��������
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
        /// ����
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
        /// Ĭ�Ϸ���
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
        /// ��ID
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
