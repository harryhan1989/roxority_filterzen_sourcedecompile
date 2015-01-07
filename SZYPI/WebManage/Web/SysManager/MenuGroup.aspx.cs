using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Nandasoft;
using Nandasoft.Helper;
using WebUI;
using BLL;

namespace SZCC.Web.SysManager
{
    public partial class MenuGroup : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindTreeView();
        }

        /// <summary>
        /// 树壮结点被点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvMenu_NodeClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
        {
            this.MenuID = NDConvert.ToInt64(tvMenu.SelectedNode.DataKey.ToString());
        }

        /// <summary>
        /// toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "Delete":
                    if (e.Item.Value == "Delete")
                    {
                        for (int j = 0; j < tvMenu.CheckedNodes.Count; j++)
                        {
                            Infragistics.WebUI.UltraWebNavigator.Node tn = (Infragistics.WebUI.UltraWebNavigator.Node)tvMenu.CheckedNodes[j];
                            MenuEntity entity = new MenuEntity(NDConvert.ToInt64(tn.DataKey.ToString()));
                            entity.IsDeleted = true;
                            new MenuRule().Update(entity);
                        }
                        BindTreeView();
                        PageHelper.ShowMessage("删除成功！");
                    }
                    break;
                case "Add":
                    this.MenuID = 0;
                    if (tvMenu.CheckedNodes.Count != 0)
                    {
                        this.MenuID = NDConvert.ToInt64(((Infragistics.WebUI.UltraWebNavigator.Node)tvMenu.CheckedNodes[0]).DataKey.ToString());
                    }
                    MenuEntity entity1 = new MenuEntity(this.MenuID);
                    if (entity1.ParentMenuID != 0)
                    {
                        PageHelper.ShowExceptionMessage("此菜单下不能再添加子菜单！");
                        return;
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "新增", "showModalWindow('MenuDetailLY', '新增',750, 220, '../../web/SysManager/MenuDetail.aspx?Operation=1&ID=' + " + this.MenuID + ");", true);
                    break;
                case "Update":
                    this.MenuID = NDConvert.ToInt64(((Infragistics.WebUI.UltraWebNavigator.Node)tvMenu.CheckedNodes[0]).DataKey.ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "更新", "showModalWindow('MenuDetailLY', '修改',750, 220, '../../web/SysManager/MenuDetail.aspx?Operation=2&ID=' + " + this.MenuID + ");", true);
                    break;

            }
        }

        /// <summary>
        /// 上移，下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int num = 0;
            int numi = 0;
            int topNodeIndex = 0;
            int childNodeIndex = 0;
            int parentNodeIndex = 0;

            Infragistics.WebUI.UltraWebNavigator.Node tn;

            switch (e.Item.Value)
            {
                case "UP":
                    for (int i = 0; i < tvMenu.CheckedNodes.Count; i++)
                    {
                        num++;
                        numi = i;
                    }
                    if (num == 0)
                    {
                        PageHelper.ShowExceptionMessage("请至少选择一条记录！");
                        return;
                    }
                    else if (num > 1)
                    {
                        PageHelper.ShowExceptionMessage("上移只能选择一条记录！");
                        return;
                    }
                    else
                    {
                        tn = (Infragistics.WebUI.UltraWebNavigator.Node)tvMenu.CheckedNodes[numi];
                        if (tn.PrevNode == null)
                        {
                            PageHelper.ShowExceptionMessage("第一个无法上移！");
                            return;
                        }
                        if (tn.Level == 1)
                        {
                            topNodeIndex = tn.Parent.Index;
                        }
                        else if (tn.Level == 2)
                        {
                            parentNodeIndex = tn.Parent.Index;
                            topNodeIndex = tn.Parent.Parent.Index;
                        }
                        childNodeIndex = tn.PrevNode.Index;
                        int Mid = 0;
                        MenuEntity entity = new MenuEntity(NDConvert.ToInt64(tn.DataKey.ToString()));
                        MenuEntity entityPrev = new MenuEntity(NDConvert.ToInt64(tn.PrevNode.DataKey.ToString()));
                        Mid = entity.SortOrder;
                        entity.SortOrder = entityPrev.SortOrder;
                        entityPrev.SortOrder = Mid;
                        new MenuRule().Update(entity);
                        new MenuRule().Update(entityPrev);
                    }
                    BindTreeView();
                    if (tn.Level == 1)
                    {
                        tvMenu.Nodes[topNodeIndex].Nodes[childNodeIndex].Checked = true;
                    }
                    else if (tn.Level == 2)
                    {
                        tvMenu.Nodes[topNodeIndex].Nodes[parentNodeIndex].Nodes[childNodeIndex].Checked = true;
                    }
                    break;
                case "Down":
                    for (int i = 0; i < tvMenu.CheckedNodes.Count; i++)
                    {
                        num++;
                        numi = i;
                    }
                    if (num == 0)
                    {
                        PageHelper.ShowExceptionMessage("请至少选择一条记录！");
                        return;
                    }
                    else if (num > 1)
                    {
                        PageHelper.ShowExceptionMessage("下移只能选择一条记录！");
                        return;
                    }
                    else
                    {
                        tn = (Infragistics.WebUI.UltraWebNavigator.Node)tvMenu.CheckedNodes[numi];
                        if (tn.NextNode == null)
                        {
                            PageHelper.ShowExceptionMessage("最后一个无法下移！");
                            return;
                        }
                        if (tn.Level == 1)
                        {
                            topNodeIndex = tn.Parent.Index;
                        }
                        else if (tn.Level == 2)
                        {
                            parentNodeIndex = tn.Parent.Index;
                            topNodeIndex = tn.Parent.Parent.Index;
                        }
                        childNodeIndex = tn.NextNode.Index;
                        int Mid = 0;
                        MenuEntity entity = new MenuEntity(NDConvert.ToInt64(tn.DataKey.ToString()));
                        MenuEntity entityNext = new MenuEntity(NDConvert.ToInt64(tn.NextNode.DataKey.ToString()));
                        Mid = entity.SortOrder;
                        entity.SortOrder = entityNext.SortOrder;
                        entityNext.SortOrder = Mid;
                        new MenuRule().Update(entity);
                        new MenuRule().Update(entityNext);
                    }
                    BindTreeView();
                    if (tn.Level == 1)
                    {
                        tvMenu.Nodes[topNodeIndex].Nodes[childNodeIndex].Checked = true;
                    }
                    else if (tn.Level == 2)
                    {
                        tvMenu.Nodes[topNodeIndex].Nodes[parentNodeIndex].Nodes[childNodeIndex].Checked = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            BindTreeView();
        }

        /// <summary>
        /// 绑定树状结构
        /// </summary>
        private void BindTreeView()
        {
            tvMenu.ClearAll();
            DataTable dt = null;

            Infragistics.WebUI.UltraWebNavigator.Node tn1;
            Infragistics.WebUI.UltraWebNavigator.Node tn2;
            Infragistics.WebUI.UltraWebNavigator.Node tn3;

            //一层
            dt = new MenuQuery().GetMenuTop();

            tn1 = new Infragistics.WebUI.UltraWebNavigator.Node();
            tn1.Text = "系统菜单";
            tn1.DataKey = "0";
            tn1.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.False;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tn2 = new Infragistics.WebUI.UltraWebNavigator.Node();
                tn2.Text = dt.Rows[i]["MenuName"].ToString();
                tn2.DataKey = dt.Rows[i]["MenuID"].ToString();
                tn2.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
                if (dt.Rows[i]["IsDisplay"].ToString() == "0")
                {
                    tn2.Style.Font.Bold = true;
                }
                //tn2.TargetUrl = "javascript:window.parent.frames['Code'].location.href = 'Code.aspx?ID=" + tn2.DataKey.ToString() + "';";
                tn1.Nodes.Add(tn2);
                //二层
                DataTable dtChild = new MenuQuery().GetMenuChild(NDConvert.ToInt64(dt.Rows[i]["MenuID"].ToString()));
                for (int j = 0; j < dtChild.Rows.Count; j++)
                {
                    tn3 = new Infragistics.WebUI.UltraWebNavigator.Node();
                    tn3.Text = dtChild.Rows[j]["MenuName"].ToString();
                    tn3.DataKey = dtChild.Rows[j]["MenuID"].ToString();
                    tn3.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
                    if (dtChild.Rows[j]["IsDisplay"].ToString() == "False")
                    {
                        tn3.Style.ForeColor = System.Drawing.Color.Blue;
                    }
                    tn3.TargetUrl = "javascript:window.parent.frames['MenuFunction'].location.href = 'MenuFunction.aspx?ID=" + tn3.DataKey.ToString() + "';";
                    tn2.Nodes.Add(tn3);
                }
            }
            tvMenu.Nodes.Add(tn1);

            tvMenu.ExpandAll();
        }

        #region 属性
        /// <summary>
        /// 菜单ID
        /// </summary>
        private long MenuID
        {
            set { ViewState["MenuID"] = value.ToString(); }
            get
            {
                if (ViewState["MenuID"] != null)
                {
                    return NDConvert.ToInt64(ViewState["MenuID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion
    }
}
