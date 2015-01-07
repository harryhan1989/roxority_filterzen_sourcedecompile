using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// Ŀ��: ʵ����.
    /// ���ߣ�Ҧ��
    /// ��д����: 2010-9-22.
    /// </summary>
    [TableName("UT_QS_SurveyTable")]
    public class SurveyTableEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public SurveyTableEntity()
        {
        }


        /// <summary>
        /// ���캯��
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
        /// �ʾ�ID
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
        /// �û�ID
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
        /// �ʾ�����
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
        /// ��������
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
        /// ��������
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
        /// �ʾ�����
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
        /// ����ҳ
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
        /// ��ʼҳ
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
        /// �������
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
        /// 0-δ���ɣ�1-������
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
        /// �״̬
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
        /// ������ʱ��
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
        /// ������
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
        /// ģ��ҳ
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
        /// email֪ͨ
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
        /// ���ID
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
        /// �ϱ�
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
        /// ����URL
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
        /// ��������Ϣ
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
        /// �ʾ����
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
        /// �����ʾ���
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
        /// ����Ա���ûش���
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
        /// ����Ա�����ѻش���
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
        /// ��ʾ��
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
        /// ����״̬
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
        /// �ʾ�����
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
        /// �ʾ�����
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
        /// �ʾ�����
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
