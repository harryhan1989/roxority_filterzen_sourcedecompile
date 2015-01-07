using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// 目的: 礼品兑换记录实体类.
    /// 编写日期: 2010-9-20.
    /// </summary>
    [TableName("UT_QS_Gifts_Exchange")]
    public class ExchangeEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeEntity()
        {
        }


        /// <summary>
        /// 构造函数
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
        /// 会员GUID
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
        /// 礼品GUID
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
        /// 使用积分
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
        /// 兑换时间
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
        /// 兑换状态（1:未兑换   2:已兑换   3:已放弃）
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
        /// 快递地址
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
        /// 留言
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
