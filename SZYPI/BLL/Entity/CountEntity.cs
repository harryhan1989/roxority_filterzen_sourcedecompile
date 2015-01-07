using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// 目的: 实体类.
    /// 编写日期: 2010-3-2.
    /// </summary>
    [TableName("UT_Info_Count")]
    public class CountEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CountEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="countID"></param>
        public CountEntity(long countID)
            : this()
        {
            _countID = countID;
            this.SelectByPKeys();
        }

        private long _countID = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("CountID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long CountID
        {
            get
            {
                return _countID;
            }
            set
            {
                _countID = value;
            }
        }


        private long _countNum = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("CountNum")]
        public long CountNum
        {
            get
            {
                return _countNum;
            }
            set
            {
                _countNum = value;
            }
        }

    }
}
