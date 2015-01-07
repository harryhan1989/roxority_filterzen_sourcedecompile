using System;
using System.IO;
using BLL;
using BLL.Entity;
using BLL.Rule;
using Nandasoft;
using WebUI;

namespace WebManage.Web.SysManager
{
    public partial class PartnerEdit : BasePage
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('PartnerEdit','btnRefresh');", true);
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndReload", "modalWindowReloadParentPage('PartnerEdit','btnRefresh');", true);
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
                this.PartnerID = Request.QueryString["ID"].ToString();
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
                //tdSpan1.Visible = false;
                //btnUpload.Visible = false;
                //btnUpdate.Visible = true;
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            PartnerEntity entity = new PartnerEntity(this.PartnerID);
            txtName.Text = entity.Name;
            txtURL.Text = entity.URL;
            txtSort.Text = entity.sort.ToString();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            PartnerEntity entity = new PartnerEntity();
            Stream fileStream = fuPicture.PostedFile.InputStream;

            byte[] FileContent = new byte[fileStream.Length];
            fileStream.Read(FileContent, 0, FileContent.Length);
            fileStream.Close();

            byte[] LittlePicture = FileContent;

            entity.ID = Guid.NewGuid().ToString();
            entity.Name = txtName.Text.Trim();
            entity.URL = txtURL.Text.Trim();
            if (txtSort.Text.Trim() == "")
            {
                entity.sort = 0;
            }
            else
            {
                entity.sort = NDConvert.ToInt32(txtSort.Text.Trim());
            }

            entity.Status = (int)CommonEnum.PartnerStatus.Online;

            if (FileContent.Length > 0)
            {
                entity.Image = FileContent;
            }

            new PartnerRule().Add(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void Update()
        {
            PartnerEntity entity = new PartnerEntity(this.PartnerID);
            if (fuPicture.PostedFile.ContentLength > 0)
            {
                Stream fileStream = fuPicture.PostedFile.InputStream;

                byte[] FileContent = new byte[fileStream.Length];
                fileStream.Read(FileContent, 0, FileContent.Length);
                fileStream.Close();

                byte[] LittlePicture = FileContent;

                entity.Image = LittlePicture;
            }
            entity.Name = txtName.Text.Trim();
            entity.URL = txtURL.Text.Trim();
            if (txtSort.Text.Trim() == "")
            {
                entity.sort = 0;
            }
            else
            {
                entity.sort = NDConvert.ToInt32(txtSort.Text.Trim());
            }

            new PartnerRule().Update(entity);
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
            if (string.IsNullOrEmpty(txtName.Text))
            {
                hidInfo.Value = "请输入合作伙伴名称";
                return false;
            }
            if (string.IsNullOrEmpty(txtURL.Text))
            {
                hidInfo.Value = "请输入超链接地址！";
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
        /// 合作伙伴ID
        /// </summary>
        private string PartnerID
        {
            set { ViewState["PartnerID"] = value.ToString(); }
            get
            {
                if (ViewState["PartnerID"] != null)
                {
                    return ViewState["PartnerID"].ToString();
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