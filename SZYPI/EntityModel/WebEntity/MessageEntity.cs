using System;

namespace EntityModel.WebEntity
{
    /// <summary>
    /// 实体类UT_Info_Message 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class MessageEntity
    {
        public MessageEntity()
        { }
        #region Model
        private long _messageboardid;
        private int _messagecategory;
        private string _personname;
        private string _address;
        private string _contactphone;
        private string _email;
        private string _subject;
        private string _content;
        private DateTime _submitdate;
        private string _returncontent;
        private string _returnunit;
        private DateTime _rerutndate;
        private int _status;
        private long _clickrate;
        private long _userid;
        private string _updatedate;
        private int _ispublic;
        private bool _isdeleted;
        /// <summary>
        /// ID
        /// </summary>
        public long MessageBoardID
        {
            set { _messageboardid = value; }
            get { return _messageboardid; }
        }
        /// <summary>
        /// 类型分类：1、局长信箱  2、在线咨询   3、意见建议   4、举报投诉
        /// </summary>
        public int MessageCategory
        {
            set { _messagecategory = value; }
            get { return _messagecategory; }
        }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonName
        {
            set { _personname = value; }
            get { return _personname; }
        }
        /// <summary>
        /// 联系电子或单位名称
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone
        {
            set { _contactphone = value; }
            get { return _contactphone; }
        }
        /// <summary>
        /// 企业邮箱
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmitDate
        {
            set { _submitdate = value; }
            get { return _submitdate; }
        }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReturnContent
        {
            set { _returncontent = value; }
            get { return _returncontent; }
        }
        /// <summary>
        /// 回复单位
        /// </summary>
        public string ReturnUnit
        {
            set { _returnunit = value; }
            get { return _returnunit; }
        }
        /// <summary>
        /// 回复日期
        /// </summary>
        public DateTime RerutnDate
        {
            set { _rerutndate = value; }
            get { return _rerutndate; }
        }
        /// <summary>
        /// 1:提交状态 2:回复状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 点击率（预留）
        /// </summary>
        public long ClickRate
        {
            set { _clickrate = value; }
            get { return _clickrate; }
        }
        /// <summary>
        /// 村官用户ID
        /// </summary>
        public long UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }

        /// <summary>
        /// 是否公开（0不公开，1公开）(预留)
        /// </summary>
        public int IsPublic
        {
            set { _ispublic = value; }
            get { return _ispublic; }
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

