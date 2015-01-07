using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// 目的: 会员表实体类.
    /// 作者：姚东
    /// 时间：20100919
    /// </summary>
    [TableName("UT_HuiYuan")]
    public class HuiYuanEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HuiYuanEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        public HuiYuanEntity(string id)
            : this()
        {
            _id = id;
            this.SelectByPKeys();
        }

        private string _id = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("id")]
        [ColumnPrimaryKey(true)]
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


        private string _loginAcc = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("LoginAcc")]
        public string LoginAcc
        {
            get
            {
                return _loginAcc;
            }
            set
            {
                _loginAcc = value;
            }
        }


        private string _loginPWD = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("LoginPWD")]
        public string LoginPWD
        {
            get
            {
                return _loginPWD;
            }
            set
            {
                _loginPWD = value;
            }
        }


        private string _getPWDTitle = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("GetPWDTitle")]
        public string GetPWDTitle
        {
            get
            {
                return _getPWDTitle;
            }
            set
            {
                _getPWDTitle = value;
            }
        }


        private long _getPWDAns = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("GetPWDAns")]
        public long GetPWDAns
        {
            get
            {
                return _getPWDAns;
            }
            set
            {
                _getPWDAns = value;
            }
        }


        private string _name = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("Name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }


        private string _email = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("Email")]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }


        private string _tel = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("Tel")]
        public string Tel
        {
            get
            {
                return _tel;
            }
            set
            {
                _tel = value;
            }
        }


        private string _createTime = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("CreateTime")]
        public string CreateTime
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

        private long _userID = -1;
        /// <summary>
        /// 
        /// </summary>
        [ColumnAutoIncrement(true)]
        [ColumnName("UserID")]
        public long UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }
    }
}