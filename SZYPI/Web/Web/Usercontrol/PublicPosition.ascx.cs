using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityModel.WebEntity;
using Business.Helper;
using System.Text;
using BusinessLayer.Web.Logic;

namespace Web.Web.Usercontrol
{
    public partial class PublicPosition : System.Web.UI.UserControl
    {
        #region 属性
        /// <summary>
        /// 类别ID
        /// </summary>
        private long MessageType
        {
            get
            {
                if (ViewState["MessageType"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertHelper.ConvertLong(ViewState["MessageType"].ToString());
                }
            }
            set
            {
                ViewState["MessageType"] = value;
            }
        }

        private long MessageID
        {
            get
            {
                if (ViewState["MessageID"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertHelper.ConvertLong(ViewState["MessageID"].ToString());
                }
            }
            set
            {
                ViewState["MessageID"] = value;
            }
        }

        private string _str = string.Empty;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGetUrlPara(); //获取页面传递参数 

                LoadPosition(); //加载数据位置
            }
        }

        #region 获得URL参数
        /// <summary>
        /// 获得URL参数
        /// </summary>
        public void LoadGetUrlPara()
        {
            if (!string.IsNullOrEmpty(RequestQueryString.GetQueryString("MessageType")))
            {
                MessageType = ConvertHelper.ConvertLong(RequestQueryString.GetQueryString("MessageType"));
            }
            if (!string.IsNullOrEmpty(RequestQueryString.GetQueryString("MessageID")))
            {
                MessageID = ConvertHelper.ConvertLong(RequestQueryString.GetQueryString("MessageID"));
            }
        }
        #endregion

        #region 绑定位置
        /// <summary>
        /// 绑定位置 
        /// </summary>
        public void LoadPosition()
        {
            if (MessageType != 0)
            {
                LoadInfo(MessageType);
            }
            else if (MessageID != 0)
            {
                List<MessageEntity> message = MessageLogic.GetMessageByID(MessageID);
                LoadInfo(message[0].MessageCategory);
            }
            else 
            {
                LoadInfo(MessageType);
            }
        }
        #endregion

        #region 位置信息
        /// <summary>
        /// 位置信息
        /// </summary>
        /// <param name="webInfoID"></param>
        public void LoadInfo(long messageType)
        {
            _str = string.Format(">><a href=SuggestionConsults.aspx>{0}</a>", "公众监督");

            if (messageType != 0)
            {
                switch (messageType)
                {
                    case 1: _str = _str + string.Format(">><a href=SuggestionConsults.aspx?MessageType={0}>{1}</a>", 1, "局长信箱");
                        break;
                    case 2: _str = _str + string.Format(">><a href=SuggestionConsults.aspx?MessageType={0}>{1}</a>", 2, "在线咨询");
                        break;
                    case 3: _str = _str + string.Format(">><a href=SuggestionConsults.aspx?MessageType={0}>{1}</a>", 3, "意见建议");
                        break;
                    case 4:
                        _str = _str + string.Format(">><a href=SuggestionConsults.aspx?MessageType={0}>{1}</a>", 4, "举报投诉");
                        break;
                    default:
                        break;
                }
            }
            if (MessageID != 0)
            {
                _str = _str + ">>正文";
            }
            divPosition.InnerHtml = "您当前位置：<a href=\"Index.aspx\" target=\"_self\">首页</a>" + _str;
        }
        #endregion
    }
}