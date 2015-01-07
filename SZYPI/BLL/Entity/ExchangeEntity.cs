using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// Ŀ��: ��Ʒ�һ���¼ʵ����.
    /// ��д����: 2010-9-20.
    /// </summary>
    [TableName("UT_QS_Gifts_Exchange")]
    public class ExchangeEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ExchangeEntity()
        {
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="id"></param>
        public ExchangeEntity(long id)
            : this()
        {
            _id = id;
            this.SelectByPKeys();
        }

        private long _id = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("ID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }


        private string _huiYuanGuid = "";
        /// <summary>
        /// ��ԱGUID
        /// </summary>
        [ColumnName("HuiYuanGuid")]
        public string HuiYuanGuid
        {
            get
            {
                return _huiYuanGuid;
            }
            set
            {
                _huiYuanGuid = value;
            }
        }


        private string _giftGuid = "";
        /// <summary>
        /// ��ƷGUID
        /// </summary>
        [ColumnName("GiftGuid")]
        public string GiftGuid
        {
            get
            {
                return _giftGuid;
            }
            set
            {
                _giftGuid = value;
            }
        }


        private int _usedPoint = 0;
        /// <summary>
        /// ʹ�û���
        /// </summary>
        [ColumnName("UsedPoint")]
        public int UsedPoint
        {
            get
            {
                return _usedPoint;
            }
            set
            {
                _usedPoint = value;
            }
        }


        private DateTime _applyTime = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("ApplyTime")]
        public DateTime ApplyTime
        {
            get
            {
                return _applyTime;
            }
            set
            {
                _applyTime = value;
            }
        }


        private DateTime _exchangeTime = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// �һ�ʱ��
        /// </summary>
        [ColumnName("ExchangeTime")]
        public DateTime ExchangeTime
        {
            get
            {
                return _exchangeTime;
            }
            set
            {
                _exchangeTime = value;
            }
        }


        private int _status = 0;
        /// <summary>
        /// �һ�״̬��1:δ�һ�   2:�Ѷһ�   3:�ѷ�����
        /// </summary>
        [ColumnName("Status")]
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }


        private string _memo = "";
        /// <summary>
        /// ��ݵ�ַ
        /// </summary>
        [ColumnName("Memo")]
        public string Memo
        {
            get
            {
                return _memo;
            }
            set
            {
                _memo = value;
            }
        }


        private string _leaveWord = "";
        /// <summary>
        /// ����
        /// </summary>
        [ColumnName("LeaveWord")]
        public string LeaveWord
        {
            get
            {
                return _leaveWord;
            }
            set
            {
                _leaveWord = value;
            }
        }

    }
}
