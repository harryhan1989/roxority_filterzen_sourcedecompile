using System;

namespace EntityModel.WebEntity
{
    /// <summary>
    /// 实体类UT_SYS_User 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class UserEntity
    {
        public UserEntity()
        { }
        #region Model
        private long _userid;
        private long _ouid;
        private string _username;
        private string _account;
        private string _password;
        private int _sex;
        private DateTime _birthday;
        private string _email;
        private string _mobilephone;
        private string _officephone;
        private string _homephone;
        private string _duty;
        private int _status;
        private int _sortindex;
        private int _usertype;
        private DateTime _adddate;
        private DateTime _updatedate;
        private bool _isdeleted;
        /// <summary>
        /// ID
        /// </summary>
        public long UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// ID
        /// </summary>
        public long OUID
        {
            set { _ouid = value; }
            get { return _ouid; }
        }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 帐号
        /// </summary>
        public string Account
        {
            set { _account = value; }
            get { return _account; }
        }
        /// <summary>
        /// 密码(预留)
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 性别(1、男 2、女)
        /// </summary>
        public int Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone
        {
            set { _officephone = value; }
            get { return _officephone; }
        }
        /// <summary>
        /// 家庭联系电话
        /// </summary>
        public string HomePhone
        {
            set { _homephone = value; }
            get { return _homephone; }
        }
        /// <summary>
        /// 职位
        /// </summary>
        public string Duty
        {
            set { _duty = value; }
            get { return _duty; }
        }
        /// <summary>
        /// 状态（1、正常 2、锁定）
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortIndex
        {
            set { _sortindex = value; }
            get { return _sortindex; }
        }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType
        {
            set { _usertype = value; }
            get { return _usertype; }
        }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        #endregion Model

    }
}

