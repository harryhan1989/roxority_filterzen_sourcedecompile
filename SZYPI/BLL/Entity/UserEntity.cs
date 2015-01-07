using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// Ŀ��: ��֯������Ա��ʵ����.
    /// ��д����: 2010-2-8.
    /// </summary>
    [TableName("UT_SYS_User")]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public UserEntity()
        {
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="userID"></param>
        public UserEntity(long userID)
            : this()
        {
            _userID = userID;
            this.SelectByPKeys();
        }

        private long _userID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("UserID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
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


        private long _oUID = 0;
        /// <summary>
        /// ID
        /// </summary>
        [ColumnName("OUID")]
        [ColumnMasterName("OUID")]
        public long OUID
        {
            get
            {
                return _oUID;
            }
            set
            {
                _oUID = value;
            }
        }


        private string _userName = "";
        /// <summary>
        /// Ա������
        /// </summary>
        [ColumnName("UserName")]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }


        private string _account = "";
        /// <summary>
        /// �ʺ�
        /// </summary>
        [ColumnName("Account")]
        public string Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
            }
        }


        private string _password = "";
        /// <summary>
        /// ����(Ԥ��)
        /// </summary>
        [ColumnName("Password")]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }


        private int _sex = 0;
        /// <summary>
        /// �Ա�(1���� 2��Ů)
        /// </summary>
        [ColumnName("Sex")]
        public int Sex
        {
            get
            {
                return _sex;
            }
            set
            {
                _sex = value;
            }
        }


        private DateTime _birthday = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// ��������
        /// </summary>
        [ColumnName("Birthday")]
        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
            }
        }


        private string _email = "";
        /// <summary>
        /// Email
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


        private string _mobilePhone = "";
        /// <summary>
        /// �ֻ�����
        /// </summary>
        [ColumnName("MobilePhone")]
        public string MobilePhone
        {
            get
            {
                return _mobilePhone;
            }
            set
            {
                _mobilePhone = value;
            }
        }


        private string _officePhone = "";
        /// <summary>
        /// �칫�绰
        /// </summary>
        [ColumnName("OfficePhone")]
        public string OfficePhone
        {
            get
            {
                return _officePhone;
            }
            set
            {
                _officePhone = value;
            }
        }


        private string _homePhone = "";
        /// <summary>
        /// ��ͥ��ϵ�绰
        /// </summary>
        [ColumnName("HomePhone")]
        public string HomePhone
        {
            get
            {
                return _homePhone;
            }
            set
            {
                _homePhone = value;
            }
        }


        private string _duty = "";
        /// <summary>
        /// ְλ
        /// </summary>
        [ColumnName("Duty")]
        public string Duty
        {
            get
            {
                return _duty;
            }
            set
            {
                _duty = value;
            }
        }


        private int _status = 0;
        /// <summary>
        /// ״̬��1������ 2��������
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


        private int _sortIndex = 0;
        /// <summary>
        /// ����
        /// </summary>
        [ColumnName("SortIndex")]
        public int SortIndex
        {
            get
            {
                return _sortIndex;
            }
            set
            {
                _sortIndex = value;
            }
        }


        private int _userType = 0;
        /// <summary>
        /// �û�����
        /// </summary>
        [ColumnName("UserType")]
        public int UserType
        {
            get
            {
                return _userType;
            }
            set
            {
                _userType = value;
            }
        }


        private DateTime _addDate = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// �������
        /// </summary>
        [ColumnName("AddDate")]
        public DateTime AddDate
        {
            get
            {
                return _addDate;
            }
            set
            {
                _addDate = value;
            }
        }


        private DateTime _updateDate = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// ��������
        /// </summary>
        [ColumnName("UpdateDate")]
        public DateTime UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                _updateDate = value;
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
