using System;
using WebUI;
using Nandasoft;
using BLL;
using System.IO;
using BLL.Rule;
using BLL.Entity;

namespace WebManage.Web.Gifts
{
    /// <summary>
    /// 礼品编辑页面
    /// 作者：姚东
    /// 时间：20100920
    /// </summary>
    public partial class Edit : BasePage
    {
        /// <summary>
        /// 加载页面
        /// 作者:姚东
        /// 时间：20100920
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetUrlParameter();
                InitPage();
            }
        }

        /// <summary>
        /// 保存（修改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                PageHelper.ShowExceptionMessage(hidInfo.Value);
                return;
            }
            try
            {
                if (this.CurOperation == (int)OperationEnum.INSERT)
                {
                    Save();
                }
                else
                {
                    Update();
                }
                
                PageHelper.ShowMessage("修改成功！");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('GiftEdit','btnRefresh');", true);
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// 页面关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('GiftEdit','btnRefresh');", true);
        }

        /// <summary>
        /// 获得URL参数
        /// </summary>
        private void GetUrlParameter()
        {
            if (Request.QueryString["Operation"] != null && Request.QueryString["Operation"] != "")
            {
                this.CurOperation = NDConvert.ToInt32(Request.QueryString["Operation"].ToString());
            }
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                this.GiftID = Request.QueryString["ID"].ToString();
            }
        }

        /// <summary>
        /// 初始化页面操作
        /// </summary>
        private void InitPage()
        {
            if (this.CurOperation == (int)OperationEnum.UPDATE)
            {
                LoadData();
                tdSpan1.Visible = false;
                //btnUpload.Visible = false;
                btnUpdate.Visible = true;
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            GiftsEntity entity = new GiftsEntity(this.GiftID);
            txtGiftName.Text = entity.GiftName;
            txtNeedPoints.Text = entity.NeedPoint.ToString();
            txtRemainAmount.Text = entity.RemainAmount.ToString();
            txtDescription.Text = entity.Description;
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            GiftsEntity entity = new GiftsEntity();
            Stream fileStream = fuPicture.PostedFile.InputStream;

            byte[] FileContent = new byte[fileStream.Length];
            fileStream.Read(FileContent, 0, FileContent.Length);
            fileStream.Close();

            byte[] LittlePicture = FileContent;

            entity.ID = Guid.NewGuid().ToString();
            entity.GiftName = txtGiftName.Text.Trim();
            entity.NeedPoint = NDConvert.ToInt32(txtNeedPoints.Text.Trim());
            entity.RemainAmount = NDConvert.ToInt32(txtRemainAmount.Text.Trim());
            entity.Description = txtDescription.Text.Trim();
            entity.Status = (int)CommonEnum.GiftStatus.Close;
            entity.CreateTime = DateTime.Now;

            if (FileContent.Length > 0)
            {
                entity.Picture = FileContent;
            }

            new GiftsRule().Add(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void Update()
        {
            GiftsEntity entity = new GiftsEntity(this.GiftID);
            if (fuPicture.PostedFile.ContentLength > 0)
            {
                Stream fileStream = fuPicture.PostedFile.InputStream;

                byte[] FileContent = new byte[fileStream.Length];
                fileStream.Read(FileContent, 0, FileContent.Length);
                fileStream.Close();

                byte[] LittlePicture = FileContent;

                entity.Picture = LittlePicture;
            }
            entity.GiftName = txtGiftName.Text.Trim();
            entity.NeedPoint = NDConvert.ToInt32(txtNeedPoints.Text.Trim());
            entity.RemainAmount = NDConvert.ToInt32(txtRemainAmount.Text.Trim());
            entity.Description = txtDescription.Text.Trim();
            entity.UpdateTime = DateTime.Now;

            new GiftsRule().Update(entity);
        }

        /// <summary>
        /// 验证页面数据
        /// </summary>
        private bool CheckData()
        {
            if (this.CurOperation == (int)OperationEnum.INSERT)
            {
                if (fuPicture.PostedFile.ContentLength == 0)
                {
                    hidInfo.Value = "请上传图片！";
                    return false;
                }
            }
            if (fuPicture.PostedFile.ContentLength > 0)
            {
                string PictureType = SystemParamQuery.GetSystemParam("PictureType").ToLower();
                string ExtensionName = Path.GetExtension(fuPicture.PostedFile.FileName).ToLower();

                if (PictureType.IndexOf(ExtensionName) == -1)
                {
                    hidInfo.Value = "请选择jpg、jpeg、gif、bmp、png类型的图片！";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtGiftName.Text))
            {
                hidInfo.Value = "请输入礼品名称";
                return false;
            }
            if (string.IsNullOrEmpty(txtNeedPoints.Text))
            {
                hidInfo.Value = "请输入兑换所需积分！";
                return false;
            }
            if (string.IsNullOrEmpty(txtRemainAmount.Text))
            {
                hidInfo.Value = "请输入剩余礼品数量！";
                return false;
            }
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                hidInfo.Value = "请输入礼品描述！";
                return false;
            }
           
            return true;
        }

        #region 属性

        /// <summary>
        /// 页面状态属性
        /// </summary>
        private int CurOperation
        {
            set { ViewState["Operation"] = value.ToString(); }
            get
            {
                if (ViewState["Operation"] != null)
                {
                    return NDConvert.ToInt32(ViewState["Operation"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 礼品ID
        /// </summary>
        private string GiftID
        {
            set { ViewState["GiftID"] = value.ToString(); }
            get
            {
                if (ViewState["GiftID"] != null)
                {
                    return ViewState["GiftID"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        #endregion
    }
}
