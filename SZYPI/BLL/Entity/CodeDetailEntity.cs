using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// Ŀ��: ʵ����.
    /// ��д����: 2009-6-3.
    /// </summary>
    [TableName("UT_SYS_CodeDetail")]
    public class CodeDetailEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public CodeDetailEntity()
        {
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="codeDetailID"></param>
        public CodeDetailEntity(long codeDetailID)
            : this()
        {
            _codeDetailID = codeDetailID;
            this.SelectByPKeys();
        }

        private long _codeDetailID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("CodeDetailID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long CodeDetailID
        {
            get
            {
                return _codeDetailID;
            }
            set
            {
                _codeDetailID = value;
            }
        }


        private long _codeID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("CodeID")]
        [ColumnMasterName("CodeID")]
        public long CodeID
        {
            get
            {
                return _codeID;
            }
            set
            {
                _codeID = value;
            }
        }


        private string _codeDetailName = "";
        /// <summary>
        /// ����
        /// </summary>
        [ColumnName("CodeDetailName")]
        public string CodeDetailName
        {
            get
            {
                return _codeDetailName;
            }
            set
            {
                _codeDetailName = value;
            }
        }


        private string _codeDetailValue = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("CodeDetailValue")]
        public string CodeDetailValue
        {
            get
            {
                return _codeDetailValue;
            }
            set
            {
                _codeDetailValue = value;
            }
        }


        private string _detailMemo = "";
        /// <summary>
        /// ˵��
        /// </summary>
        [ColumnName("DetailMemo")]
        public string DetailMemo
        {
            get
            {
                return _detailMemo;
            }
            set
            {
                _detailMemo = value;
            }
        }


        private int _sortOrder = 0;
        /// <summary>
        /// ����˳���
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
