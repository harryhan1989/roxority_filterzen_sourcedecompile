using System;
using System.Data;

namespace WebManage.Web.Gifts
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Show : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //所有礼品
                DataTable giftDT = new BLL.Query.GiftsQuery().GetAllGifts();
                rGiftsShow.DataSource = giftDT;
                rGiftsShow.DataBind();

                //热门礼品
                DataTable hotGiftDT = new BLL.Query.GiftsQuery().GetHotGifts();                
                rHotGift.DataSource = hotGiftDT;
                rHotGift.DataBind();

                //兑换记录
                DataTable hotExchangeHistoryDT = new BLL.Query.ExchangeQuery().GetNewExchangeList();
                rExchangeHistory.DataSource = hotExchangeHistoryDT;
                rExchangeHistory.DataBind();
            }
        }
    }
}