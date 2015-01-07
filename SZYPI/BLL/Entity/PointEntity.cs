using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// 目的: 会员积分表实体类.
    /// 编写日期: 2010-10-15.
    /// </summary>
    [TableName("UT_QS_HuiYuan_Point")]
    public class PointEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PointEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="huiYuanGuid"></param>
        public PointEntity(string huiYuanGuid)
            : this()
        {
            _huiYuanGuid = huiYuanGuid;
            this.SelectByPKeys();
        }

        private string _huiYuanGuid = "";
        /// <summary>
        /// 会员GUID
        /// </summary>
        [ColumnName("HuiYuanGuid")]
        [ColumnPrimaryKey(true)]
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


        private int _totalPoint = 0;
        /// <summary>
        /// 总积分
        /// </summary>
        [ColumnName("TotalPoint")]
        public int TotalPoint
        {
            get
            {
                return _totalPoint;
            }
            set
            {
                _totalPoint = value;
            }
        }


        private int _remainPoint = 0;
        /// <summary>
        /// 剩余积分
        /// </summary>
        [ColumnName("RemainPoint")]
        public int RemainPoint
        {
            get
            {
                return _remainPoint;
            }
            set
            {
                _remainPoint = value;
            }
        }


        private int _status = 0;
        /// <summary>
        /// 会员状态(0:停用  1：启用)
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

    }
}
