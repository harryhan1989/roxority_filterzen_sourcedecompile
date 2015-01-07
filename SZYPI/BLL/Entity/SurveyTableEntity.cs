using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// 目的: 实体类.
    /// 作者：姚东
    /// 编写日期: 2010-9-22.
    /// </summary>
    [TableName("UT_QS_SurveyTable")]
    public class SurveyTableEntity : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SurveyTableEntity()
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sID"></param>
        public SurveyTableEntity(long sID)
            : this()
        {
            _sID = sID;
            this.SelectByPKeys();
        }

        private long _sID = 0;
        /// <summary>
        /// 问卷ID
        /// </summary>
        [ColumnName("SID")]
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


        private long _uID = 0;
        /// <summary>
        /// 用户ID
        /// </summary>
        [ColumnName("UID")]
        public long UID
        {
            get
            {
                return _uID;
            }
            set
            {
                _uID = value;
            }
        }


        private string _surveyName = "";
        /// <summary>
        /// 问卷名称
        /// </summary>
        [ColumnName("SurveyName")]
        public string SurveyName
        {
            get
            {
                return _surveyName;
            }
            set
            {
                _surveyName = value;
            }
        }


        private DateTime _createDate = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// 创建日期
        /// </summary>
        [ColumnName("CreateDate")]
        public DateTime CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
            }
        }


        private DateTime _endDate = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// 结束日期
        /// </summary>
        [ColumnName("EndDate")]
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }


        private string _surveyPSW = "";
        /// <summary>
        /// 问卷密码
        /// </summary>
        [ColumnName("SurveyPSW")]
        public string SurveyPSW
        {
            get
            {
                return _surveyPSW;
            }
            set
            {
                _surveyPSW = value;
            }
        }


        private int _endPage = 0;
        /// <summary>
        /// 结束页
        /// </summary>
        [ColumnName("EndPage")]
        public int EndPage
        {
            get
            {
                return _endPage;
            }
            set
            {
                _endPage = value;
            }
        }


        private int _startPage = 0;
        /// <summary>
        /// 开始页
        /// </summary>
        [ColumnName("StartPage")]
        public int StartPage
        {
            get
            {
                return _startPage;
            }
            set
            {
                _startPage = value;
            }
        }


        private long _answerAmount = 0;
        /// <summary>
        /// 任务进度
        /// </summary>
        [ColumnName("AnswerAmount")]
        public long AnswerAmount
        {
            get
            {
                return _answerAmount;
            }
            set
            {
                _answerAmount = value;
            }
        }


        private int _state = 0;
        /// <summary>
        /// 0-未生成；1-已生成
        /// </summary>
        [ColumnName("State")]
        public int State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }


        private int _active = 0;
        /// <summary>
        /// 活动状态
        /// </summary>
        [ColumnName("Active")]
        public int Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }


        private DateTime _lastUpdate = Nandasoft.NDConvert.DateTimeDefaultValue;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [ColumnName("LastUpdate")]
        public DateTime LastUpdate
        {
            get
            {
                return _lastUpdate;
            }
            set
            {
                _lastUpdate = value;
            }
        }


        private long _maxAnswerAmount = 0;
        /// <summary>
        /// 回收数
        /// </summary>
        [ColumnName("MaxAnswerAmount")]
        public long MaxAnswerAmount
        {
            get
            {
                return _maxAnswerAmount;
            }
            set
            {
                _maxAnswerAmount = value;
            }
        }


        private string _tempPage = "";
        /// <summary>
        /// 模板页
        /// </summary>
        [ColumnName("TempPage")]
        public string TempPage
        {
            get
            {
                return _tempPage;
            }
            set
            {
                _tempPage = value;
            }
        }


        private string _par = "";
        /// <summary>
        /// email通知
        /// </summary>
        [ColumnName("Par")]
        public string Par
        {
            get
            {
                return _par;
            }
            set
            {
                _par = value;
            }
        }


        private long _classID = 0;
        /// <summary>
        /// 类别ID
        /// </summary>
        [ColumnName("ClassID")]
        public long ClassID
        {
            get
            {
                return _classID;
            }
            set
            {
                _classID = value;
            }
        }


        private string _report = "";
        /// <summary>
        /// 上报
        /// </summary>
        [ColumnName("Report")]
        public string Report
        {
            get
            {
                return _report;
            }
            set
            {
                _report = value;
            }
        }


        private string _toURL = "";
        /// <summary>
        /// 到达URL
        /// </summary>
        [ColumnName("ToURL")]
        public string ToURL
        {
            get
            {
                return _toURL;
            }
            set
            {
                _toURL = value;
            }
        }


        private string _complateMessage = "";
        /// <summary>
        /// 完整的消息
        /// </summary>
        [ColumnName("ComplateMessage")]
        public string ComplateMessage
        {
            get
            {
                return _complateMessage;
            }
            set
            {
                _complateMessage = value;
            }
        }


        private int _point = 0;
        /// <summary>
        /// 问卷积分
        /// </summary>
        [ColumnName("Point")]
        public int Point
        {
            get
            {
                return _point;
            }
            set
            {
                _point = value;
            }
        }


        private int _answerArea = 0;
        /// <summary>
        /// 开放问卷结果
        /// </summary>
        [ColumnName("AnswerArea")]
        public int AnswerArea
        {
            get
            {
                return _answerArea;
            }
            set
            {
                _answerArea = value;
            }
        }


        private int _adminSetAnswerAmount = 0;
        /// <summary>
        /// 管理员设置回答数
        /// </summary>
        [ColumnName("AdminSetAnswerAmount")]
        public int AdminSetAnswerAmount
        {
            get
            {
                return _adminSetAnswerAmount;
            }
            set
            {
                _adminSetAnswerAmount = value;
            }
        }


        private int _adminSetAnsweredAmount = 0;
        /// <summary>
        /// 管理员设置已回答数
        /// </summary>
        [ColumnName("AdminSetAnsweredAmount")]
        public int AdminSetAnsweredAmount
        {
            get
            {
                return _adminSetAnsweredAmount;
            }
            set
            {
                _adminSetAnsweredAmount = value;
            }
        }


        private int _lan = 0;
        /// <summary>
        /// 提示语
        /// </summary>
        [ColumnName("Lan")]
        public int Lan
        {
            get
            {
                return _lan;
            }
            set
            {
                _lan = value;
            }
        }

        private int _approvalStaus = 0;
        /// <summary>
        /// 审批状态
        /// </summary>
        [ColumnName("ApprovalStaus")]
        public int ApprovalStaus
        {
            get
            {
                return _approvalStaus;
            }
            set
            {
                _approvalStaus = value;
            }
        }

        private int _recommend = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("Recommend")]
        public int Recommend
        {
            get
            {
                return _recommend;
            }
            set
            {
                _recommend = value;
            }
        }

        private int _surveyType = 0;
        /// <summary>
        /// 问卷种类
        /// </summary>
        [ColumnName("SurveyType")]
        public int SurveyType
        {
            get
            {
                return _surveyType;
            }
            set
            {
                _surveyType = value;
            }
        }

        private bool _isDel = false;
        /// <summary>
        /// 问卷种类
        /// </summary>
        [ColumnName("IsDel")]
        public bool IsDel
        {
            get
            {
                return _isDel;
            }
            set
            {
                _isDel = value;
            }
        }

        private int _surveyRight = 0;
        /// <summary>
        /// 问卷种类
        /// </summary>
        [ColumnName("SurveyRight")]
        public int SurveyRight
        {
            get
            {
                return _surveyRight;
            }
            set
            {
                _surveyRight = value;
            }
        }

    }
}
