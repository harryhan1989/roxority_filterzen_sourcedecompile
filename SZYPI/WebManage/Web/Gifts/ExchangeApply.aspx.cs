using System;
using WebUI;
using BLL.Rule;

namespace WebManage.Web.Gifts
{
    /// <summary>
    /// 礼品描述详细页面
    /// 作者：姚东
    /// 时间：20101015
    /// </summary>
    public partial class ExchangeApply : System.Web.UI.Page
    {
        /// <summary>
        /// 礼品GUID
        /// </summary>
        private string GiftGuid
        {
            set { ViewState["GiftGuid"] = value; }
            get
            {
                if (ViewState["GiftGuid"] != null)
                {
                    return ViewState["GiftGuid"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 页面加载事件
        /// 作者：姚东
        /// 时间：20101015
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(this);

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        /// <summary>
        /// 数据加载
        /// 作者：姚东
        /// 时间：20101015
        /// </summary>
        private void LoadData()
        {
            if (Request.QueryString["GiftGuid"] != null)
            {
                GiftGuid = Request.QueryString["GiftGuid"];
            }

            BLL.Entity.GiftsEntity entity = new BLL.Entity.GiftsEntity(GiftGuid);

            img.ImageUrl = "ImagePhoto.aspx?ID=" + GiftGuid;
            lblGiftName.Text = entity.GiftName;
            lblNeedPoint.Text = entity.NeedPoint.ToString();
            divDescription.InnerHtml = entity.Description;
        }

        /// <summary>
        /// 立即兑换按钮点击事件
        /// 作者：姚东
        /// 时间：20101015
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExchange_Click(object sender, EventArgs e)
        {
            ////判断会员是否登录系统了
            //if (Session["UserGUID"] == null)
            //{
            //    PageHelper.ShowExceptionMessage("登录系统后才能进行礼品兑换！");
            //    return;
            //}

            BLL.Entity.GiftsEntity giftEntity = new BLL.Entity.GiftsEntity(GiftGuid);
            BLL.Entity.PointEntity pointEntity = new BLL.Entity.PointEntity(Session["UserGUID"].ToString());

            //判断剩余积分是否够兑换礼品
            if (giftEntity.NeedPoint > pointEntity.RemainPoint)
            {
                PageHelper.ShowExceptionMessage(string.Format("您的余额为{0}个积分，兑换此礼品需要{1}个积分！",
                    pointEntity.RemainPoint.ToString(),giftEntity.NeedPoint.ToString()));
                return;
            }

            //判断礼品余量是否够兑换
            if (giftEntity.RemainAmount <= 0)
            {
                PageHelper.ShowExceptionMessage("暂时没有多余的礼品可被兑换，请稍后再试！");                
                return;
            }

            //更新兑换记录
            BLL.Entity.ExchangeEntity exchangeEntity = new BLL.Entity.ExchangeEntity();
            exchangeEntity.ApplyTime = DateTime.Now;
            exchangeEntity.GiftGuid = GiftGuid;
            exchangeEntity.HuiYuanGuid = Session["UserGUID"].ToString();
            exchangeEntity.Memo = "";
            exchangeEntity.Status = 1;
            exchangeEntity.UsedPoint = giftEntity.NeedPoint;

            ExchangeRule exchangeRule = new ExchangeRule();
            exchangeRule.Add(exchangeEntity);

            //更新积分信息
            pointEntity.RemainPoint = pointEntity.RemainPoint - giftEntity.NeedPoint;
            PointRule pointRule = new PointRule();
            pointRule.Update(pointEntity);

            //更新礼品余量
            giftEntity.RemainAmount -= 1;
            new GiftsRule().Update(giftEntity);            

            PageHelper.ShowExceptionMessage("已发送兑换申请。");
        }

        /// <summary>
        /// 检测是否已经登录系统了
        /// </summary>
        /// <returns></returns>
        [Anthem.Method]
        public bool CheckHasLogin()
        {
            if (Session["UserGUID"] == null)
            {
                return false;
            }

            return true;
        }
    }
}