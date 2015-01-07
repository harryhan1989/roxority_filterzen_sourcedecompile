using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// 目的: 调查结果实体类.
    /// 编写日期: 2010-9-25.
    /// </summary>
    [TableName("UT_DiaoChaResult")]
    public class DiaoChaResultEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DiaoChaResultEntity()
        {
        }



        private long _sID = -1;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("sid")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long SID
        {
            get
            {
                return _sID;
            }
            set
            {
                _sID = value;
            }
        }

        private string _diaoChaID = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("DiaoChaID")]        
        public string DiaoChaID
        {
            get
            {
                return _diaoChaID;
            }
            set
            {
                _diaoChaID = value;
            }
        }


        private string _ip = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("ip")]
        public string ip
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
            }
        }


        private int _a1 = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("a1")]
        public int a1
        {
            get
            {
                return _a1;
            }
            set
            {
                _a1 = value;
            }
        }


        private int _a2 = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("a2")]
        public int a2
        {
            get
            {
                return _a2;
            }
            set
            {
                _a2 = value;
            }
        }


        private int _a3 = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("a3")]
        public int a3
        {
            get
            {
                return _a3;
            }
            set
            {
                _a3 = value;
            }
        }


        private int _a4 = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("a4")]
        public int a4
        {
            get
            {
                return _a4;
            }
            set
            {
                _a4 = value;
            }
        }


        private int _a5 = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("a5")]
        public int a5
        {
            get
            {
                return _a5;
            }
            set
            {
                _a5 = value;
            }
        }


        private int _a6 = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("a6")]
        public int a6
        {
            get
            {
                return _a6;
            }
            set
            {
                _a6 = value;
            }
        }


        private DateTime _edittime = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("edittime")]
        public DateTime edittime
        {
            get
            {
                return _edittime;
            }
            set
            {
                _edittime = value;
            }
        }


        private string _id = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("id")]        
        public string id
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

    }
}
