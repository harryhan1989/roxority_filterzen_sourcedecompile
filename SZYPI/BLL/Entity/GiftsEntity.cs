using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// Ŀ��: ��Ʒ��ʵ����.
    /// ���ߣ�Ҧ��
    /// ��д����: 2010-9-20.
    /// </summary>
    [TableName("UT_QS_Gifts")]
    public class GiftsEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public GiftsEntity()
        {
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="id"></param>
        public GiftsEntity(string id)
            : this()
        {
            _id = id;
            this.SelectByPKeys();
        }

        private string _id = "";
        /// <summary>
        /// ��ƷID
        /// </summary>
        [ColumnName("ID")]
        [ColumnPrimaryKey(true)]
        public string ID
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


        private string _giftName = "";
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        [ColumnName("GiftName")]
        public string GiftName
        {
            get
            {
                return _giftName;
            }
            set
            {
                _giftName = value;
            }
        }


        private int _needPoint = 0;
        /// <summary>
        /// ��Ҫ����
        /// </summary>
        [ColumnName("NeedPoint")]
        public int NeedPoint
        {
            get
            {
                return _needPoint;
            }
            set
            {
                _needPoint = value;
            }
        }


        private string _description = "";
        /// <summary>
        /// ��Ʒ����
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


        private byte[] _picture = new byte[0];
        /// <summary>
        /// ��ƷͼƬ
        /// </summary>
        [ColumnName("Picture")]
        public byte[] Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
            }
        }

        private int _status = -1;
        /// <summary>
        /// ��Ʒ״̬
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

        private int _remainAmount = -1;
        /// <summary>
        /// ʣ����Ʒ����
        /// </summary>
        [ColumnName("RemainAmount")]
        public int RemainAmount
        {
            get
            {
                return _remainAmount;
            }
            set
            {
                _remainAmount = value;
            }
        }

        private DateTime _createTime = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [ColumnName("CreateTime")]
        public DateTime CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
            }
        }


        private DateTime _updateTime = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [ColumnName("UpdateTime")]
        public DateTime UpdateTime
        {
            get
            {
                return _updateTime;
            }
            set
            {
                _updateTime = value;
            }
        }

    }
}
