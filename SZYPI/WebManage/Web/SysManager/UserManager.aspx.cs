using System;
using System.Data;
using System.Web.UI.WebControls;
using WebUI;
using Nandasoft;
using BLL;

namespace WebManage.Web.SystemManager
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserManager : BasePage
    {
        /// <summary>
        /// ҳ�����
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
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                PageHelper.ShowExceptionMessage(hidInfo.Value);
                return;
            }
            BindGridView();
        }

        /// <summary>
        /// toolbar�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            long ID = 0;
            switch (e.Item.Value)
            {
                case "Setting":
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            ID = NDConvert.ToInt64(grid.DataKeys[i]["UserID"].ToString());
                            UserEntity entity = new UserEntity(ID);
                            entity.UpdateDate = DateTime.Now;
                            entity.Status = 2;
                            new UserRule().Update(entity);
                        }
                    }
                    BindGridView();
                    PageHelper.ShowMessage("�����ɹ���");
                    break;
                case "Authorization":
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            ID = NDConvert.ToInt64(grid.DataKeys[i]["UserID"].ToString());
                            UserEntity entity = new UserEntity(ID);
                            entity.UpdateDate = DateTime.Now;
                            entity.Status = 1;
                            new UserRule().Update(entity);
                        }
                    }
                    BindGridView();
                    PageHelper.ShowMessage("�����ɹ���");
                    break;
                case "Delete":                    
                    int count = 0;
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        count++;
                        CheckBox chkItem = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chkItem != null && chkItem.Checked == true)
                        {
                            ID = NDConvert.ToInt64(grid.DataKeys[i]["UserID"].ToString());
                            UserEntity entity = new UserEntity(ID);
                            entity.UpdateDate = DateTime.Now;
                            entity.IsDeleted = true;
                            new UserRule().Update(entity);
                        }
                    }
                    if (count == this.grid.Rows.Count)
                    {
                        viewpage1.CurrentPageIndex = viewpage1.CurrentPageIndex == 1 ? 1 : viewpage1.CurrentPageIndex - 1;
                    }
                    BindGridView();
                    PageHelper.ShowMessage("ɾ���ɹ���");
                    break;
                case "Moveup":
                    int rowup = -1;
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chk != null)
                        {
                            if (chk.Checked)
                            {
                                if (i == 0 && viewpage1.CurrentPageIndex == 1)
                                {
                                    PageHelper.ShowExceptionMessage("�޷���������λ����Աִ�����Ʋ�����");
                                    return;
                                }
                                else
                                {
                                    long curUserID = NDConvert.ToInt64(grid.DataKeys[i]["UserID"].ToString());
                                    if (i == 0)
                                    {
                                        viewpage1.CurrentPageIndex--;
                                        BindGridView();
                                        rowup = grid.Rows.Count - 1;
                                        long perUserID = NDConvert.ToInt64(grid.DataKeys[rowup]["UserID"].ToString());
                                        UserQuery.UserSortMoveup(curUserID, perUserID);
                                        BindGridView();
                                    }
                                    else
                                    {
                                        long perUserID = NDConvert.ToInt64(grid.DataKeys[i - 1]["UserID"].ToString());
                                        UserQuery.UserSortMoveup(curUserID, perUserID);
                                        BindGridView();
                                        rowup = i - 1;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    if (rowup >= 0)
                    {
                        ((CheckBox)grid.Rows[rowup].FindControl("chkItem")).Checked = true;
                    }
                    break;
                case "Movedown":
                    int rowdown = -1;
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)grid.Rows[i].FindControl("chkItem");
                        if (chk != null)
                        {
                            if (chk.Checked)
                            {
                                if (i == grid.Rows.Count - 1 && viewpage1.CurrentPageIndex == viewpage1.PageCount)
                                {
                                    PageHelper.ShowExceptionMessage("�޷�������ĩλ����Աִ�����Ʋ�����");
                                    return;
                                }
                                else
                                {
                                    long curUserID = NDConvert.ToInt64(grid.DataKeys[i]["UserID"].ToString());
                                    if (i == grid.Rows.Count - 1)
                                    {
                                        viewpage1.CurrentPageIndex++;
                                        BindGridView();
                                        rowdown = 0;
                                        long nextUserID = NDConvert.ToInt64(grid.DataKeys[rowdown]["UserID"].ToString());
                                        UserQuery.UserSortMovedown(curUserID, nextUserID);
                                        BindGridView();
                                    }
                                    else
                                    {
                                        long nextUserID = NDConvert.ToInt64(grid.DataKeys[i + 1]["UserID"].ToString());
                                        UserQuery.UserSortMovedown(curUserID, nextUserID);
                                        BindGridView();
                                        rowdown = i + 1;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    if (rowdown >= 0)
                    {
                        ((CheckBox)grid.Rows[rowdown].FindControl("chkItem")).Checked = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// ˢ�²���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            if (Session["OperationEnum"] != null)
            {
                if ((int)Session["OperationEnum"] == (int)OperationEnum.INSERT)
                {
                    BindGridView();
                    viewpage1.CurrentPageIndex = viewpage1.PageCount;
                }
                Session["OperationEnum"] = null;
            }
            BindGridView();
        }

        /// <summary>
        /// ��ҳ�ؼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        /// <summary>
        /// ���URL����
        /// </summary>
        private void GetUrlParameter()
        {
            if (Request.QueryString["OUID"] != null && Request.QueryString["OUID"] != "")
            {
                this.OUID = NDConvert.ToInt64(Request.QueryString["OUID"].ToString());
            }
            else
            {
                this.OUID = SessionState.OUID;
            }
            hidOUID.Value = this.OUID.ToString();
        }

        /// <summary>
        /// ��ʼ��ҳ��
        /// </summary>
        private void InitPage()
        {
            BindGridView();
        }

        ///<summary>
        /// �������Ա
        /// </summary>
        private void BindGridView()
        {
            DataSet ds = this.GetData();
            DataTable dt = ds.Tables[1];
            int rows = dt.Rows.Count;
            grid.DataSource = dt;
            grid.DataBind();

            BindviewPage(NDConvert.ToInt32(ds.Tables[0].Rows[0][0].ToString()));
        }

        /// <summary>
        /// ���ҳ�ؼ�
        /// </summary>
        private void BindviewPage(int RecordCount)
        {
            Nandasoft.Helper.NDHelperWebControl.BindPagerControl(viewpage1, RecordCount);
            viewpage1.RecordCount = RecordCount;
        }

        /// <summary>
        /// �����Ա����
        /// </summary>
        private DataSet GetData()
        {
            DataSet ds = new UserQuery().GetOUUser(this.OUID, viewpage1.CurrentPageIndex, viewpage1.PageSize);
            return ds;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            return true;
        }

        /// <summary>
        /// ����ID
        /// </summary>
        private long OUID
        {
            set { ViewState["OUID"] = value.ToString(); }
            get
            {
                if (ViewState["OUID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["OUID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// ְ��ID
        /// </summary>
        private string UserName
        {
            set { ViewState["UserName"] = value; }
            get
            {
                if (ViewState["UserName"] != null)
                {
                    return ViewState["UserName"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
