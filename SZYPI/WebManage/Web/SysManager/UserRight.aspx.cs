using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using WebUI;
using Nandasoft;
using BLL;
using Nandasoft.Helper;
using Infragistics.WebUI.UltraWebNavigator;

namespace WebManage.Web.SysManager
{
    public partial class UserRight : BasePage
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                PageHelper.ShowExceptionMessage(hidInfo.Value);
                return;
            }

            try
            {
                Save();
                PageHelper.ShowMessage("保存成功！");
            }
            catch (Exception ex)
            {
                PageHelper.ShowExceptionMessage(ex.Message);
            }
        }

        /// <summary>
        /// 部门下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpOUList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpOUList.SelectedIndex == 0)
            {
                drpChildList.Visible = false;
            }
            else
            {
                if (new OUQuery().IsOUParent(NDConvert.ToInt64(drpOUList.SelectedValue)))
                {
                    drpChildList.Visible = true;

                    BindOUList(NDConvert.ToInt64(drpOUList.SelectedValue));
                }
                else
                {
                    drpChildList.Visible = false;
                    if (drpOUList.SelectedIndex != 0)
                    {
                        drpPersonList.Enabled = true;
                        BindPerson(NDConvert.ToInt64(drpOUList.SelectedValue), 0);
                        BindTreeView();
                    }
                    else
                    {
                        drpPersonList.Enabled = false;
                        tvRight.ClearAll();

                    }
                }
            }
        }

        /// <summary>
        /// 人员下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpPersonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //将所有选中的节点清空
            Node node = null;
            ArrayList arrNode = tvRight.CheckedNodes;
            for (int n = 0; n < arrNode.Count; n++)
            {
                node = (Node)arrNode[n];
                node.Checked = false;
            }

            //选中用户有权限的菜单节点
            if (drpPersonList.SelectedIndex != 0)
            {
                long UserID = NDConvert.ToInt64(drpPersonList.SelectedValue);
                DataTable dt = new RightQuery().GetUserRight(UserID);  //获得用户所有有权限的菜单
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    long MenuID = NDConvert.ToInt64(dt.Rows[i]["MenuID"].ToString());
                    for (int j = 0; j < tvRight.Nodes.Count; j++)
                    {
                        if (MenuID == NDConvert.ToInt64(tvRight.Nodes[j].DataKey))
                        {
                            tvRight.Nodes[j].Checked = true;
                        }
                        for (int k = 0; k < tvRight.Nodes[j].Nodes.Count; k++)
                        {
                            if (MenuID == NDConvert.ToInt64(tvRight.Nodes[j].Nodes[k].DataKey))
                            {
                                tvRight.Nodes[j].Nodes[k].Checked = true;
                            }
                            if (MenuQuery.IsHaveChileMenu(NDConvert.ToInt64(tvRight.Nodes[j].Nodes[k].DataKey.ToString())))   //判断是否有子菜单,如有有子菜单说明本层是菜单,如果没有则是菜单功能项
                            {
                                for (int m = 0; m < tvRight.Nodes[j].Nodes[k].Nodes.Count; m++)
                                {
                                    if (MenuID == NDConvert.ToInt64(tvRight.Nodes[j].Nodes[k].Nodes[m].DataKey))
                                    {
                                        tvRight.Nodes[j].Nodes[k].Nodes[m].Checked = true;
                                    }

                                    Nodes nodes = tvRight.Nodes[j].Nodes[k].Nodes[m].Nodes;
                                    for (int n = 0; n < nodes.Count; n++)
                                    {
                                        if (MenuFunctionRightQuery.CheckMenuFunctionRight(UserID, MenuID, NDConvert.ToInt64(nodes[n].DataKey)))
                                        {
                                            nodes[n].Checked = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Nodes nodes = tvRight.Nodes[j].Nodes[k].Nodes;
                                for (int n = 0; n < nodes.Count; n++)
                                {
                                    if (MenuFunctionRightQuery.CheckMenuFunctionRight(UserID, MenuID, NDConvert.ToInt64(nodes[n].DataKey)))
                                    {
                                        nodes[n].Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 树被选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvRight_NodeChecked(object sender, WebTreeNodeCheckedEventArgs e)
        {
            switch (e.Node.Level)
            {
                case 0:
                    for (int i = 0; i < e.Node.Nodes.Count; i++)
                    {
                        e.Node.Nodes[i].Checked = e.Node.Checked;
                        Nodes childNodes = e.Node.Nodes[i].Nodes;
                        for (int j = 0; j < childNodes.Count; j++)
                        {
                            childNodes[j].Checked = e.Node.Nodes[i].Checked;
                            Nodes mfNodes = childNodes[j].Nodes;
                            for (int k = 0; k < mfNodes.Count; k++)
                            {
                                mfNodes[k].Checked = childNodes[j].Checked;
                            }
                        }
                    }
                    break;
                case 1:
                    if (e.Node.Checked)
                    {
                        e.Node.Parent.Checked = true;
                    }
                    for (int i = 0; i < e.Node.Nodes.Count; i++)
                    {
                        e.Node.Nodes[i].Checked = e.Node.Checked;

                        Nodes mfNodes = e.Node.Nodes[i].Nodes;
                        for (int k = 0; k < mfNodes.Count; k++)
                        {
                            mfNodes[k].Checked = e.Node.Nodes[i].Checked;
                        }
                    }
                    break;
                case 2:
                    if (e.Node.Checked)
                    {
                        e.Node.Parent.Checked = true;
                        e.Node.Parent.Parent.Checked = true;
                    }
                    for (int i = 0; i < e.Node.Nodes.Count; i++)
                    {
                        e.Node.Nodes[i].Checked = e.Node.Checked;
                    }
                    break;
                case 3:
                    if (e.Node.Checked)
                    {
                        e.Node.Parent.Checked = true;
                        e.Node.Parent.Parent.Checked = true;
                        e.Node.Parent.Parent.Parent.Checked = true;

                    }
                    break;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            BindOUList();
            BindTreeView();
        }

        /// <summary>
        /// 邦定父级部门
        /// </summary>
        private void BindOUList()
        {
            drpOUList.Items.Clear();
            DataTable dt = new OUQuery().GetOU(0);
            NDHelperWebControl.LoadDropDownList(drpOUList, dt, "OUName", "OUID", new ListItem("请选择", "0"));
            BindPerson(0, 0);
        }

        private void BindOUList(long OUID)
        {
            drpChildList.Items.Clear();
            DataTable dt = new OUQuery().GetOU(OUID);
            NDHelperWebControl.LoadDropDownList(drpChildList, dt, "OUName", "OUID", new ListItem("请选择", "0"));
            BindPerson(0, 1);
        }

        /// <summary>
        /// 邦定人员
        /// </summary>
        private void BindPerson(long OUID, int flag)
        {
            drpPersonList.Items.Clear();
            DataTable dt = null;
            if (flag == 0)
            {
                if (OUID == 0)
                {
                    dt = new UserQuery().GetRightOUUser(NDConvert.ToInt64(drpOUList.SelectedValue));
                }
                else
                {
                    dt = new UserQuery().GetRightOUUser(OUID);
                }
            }
            else
            {
                if (OUID == 0)
                {
                    dt = new UserQuery().GetRightOUUser(NDConvert.ToInt64(drpChildList.SelectedValue));
                }
                else
                {
                    dt = new UserQuery().GetRightOUUser(OUID);
                }
            }
            NDHelperWebControl.LoadDropDownList(drpPersonList, dt, "UserName", "UserID", new ListItem("请选择", "0"));
        }

        /// <summary>
        /// 邦定列表
        /// </summary>
        private void BindTreeView()
        {
            //if (drpOUList.SelectedIndex == 0)
            //{
            //    return;
            //}

            DataTable dt1 = null;
            DataTable dt2 = null;
            DataTable dt3 = null;
            DataTable dt4 = null;

            Infragistics.WebUI.UltraWebNavigator.Node tn1;
            Infragistics.WebUI.UltraWebNavigator.Node tn2;
            Infragistics.WebUI.UltraWebNavigator.Node tn3;
            Infragistics.WebUI.UltraWebNavigator.Node tn4;


            tvRight.ClearAll();

            //dt1 = new MenuQuery().GetMenuAll2(0);
            dt1 = new MenuQuery().GetMenuAll(0, CommonEnum.MenuType.Inner);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                tn1 = new Infragistics.WebUI.UltraWebNavigator.Node();
                tn1.Text = dt1.Rows[i]["MenuName"].ToString();
                tn1.DataKey = dt1.Rows[i]["MenuID"].ToString();
                tn1.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;

                //dt2 = new MenuQuery().GetMenuAll2(NDConvert.ToInt64(tn1.DataKey.ToString()));
                dt2 = new MenuQuery().GetMenuAll(NDConvert.ToInt64(tn1.DataKey.ToString()), CommonEnum.MenuType.Inner);
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    tn2 = new Infragistics.WebUI.UltraWebNavigator.Node();
                    tn2.Text = dt2.Rows[j]["MenuName"].ToString();
                    tn2.DataKey = dt2.Rows[j]["MenuID"].ToString();
                    tn2.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
                    tn1.Nodes.Add(tn2);

                    //dt3 = new MenuQuery().GetMenuAll2(NDConvert.ToInt64(tn2.DataKey.ToString()));
                    dt3 = new MenuQuery().GetMenuAll(NDConvert.ToInt64(tn2.DataKey.ToString()), CommonEnum.MenuType.Inner);
                    if (dt3.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt3.Rows.Count; k++)
                        {
                            tn3 = new Infragistics.WebUI.UltraWebNavigator.Node();
                            tn3.Text = dt3.Rows[k]["MenuName"].ToString();
                            tn3.DataKey = dt3.Rows[k]["MenuID"].ToString();
                            tn3.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
                            tn2.Nodes.Add(tn3);

                            dt4 = new MenuFunctionQuery().GetMenuFunction(NDConvert.ToInt64(tn3.DataKey.ToString()));
                            for (int m = 0; m < dt4.Rows.Count; m++)
                            {
                                tn4 = new Infragistics.WebUI.UltraWebNavigator.Node();
                                tn4.Text = dt4.Rows[m]["FName"].ToString();
                                tn4.DataKey = dt4.Rows[m]["FID"].ToString();
                                tn4.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
                                tn3.Nodes.Add(tn4);
                            }
                        }
                    }
                    else
                    {
                        dt4 = new MenuFunctionQuery().GetMenuFunction(NDConvert.ToInt64(tn2.DataKey.ToString()));
                        for (int m = 0; m < dt4.Rows.Count; m++)
                        {
                            tn4 = new Infragistics.WebUI.UltraWebNavigator.Node();
                            tn4.Text = dt4.Rows[m]["FName"].ToString();
                            tn4.DataKey = dt4.Rows[m]["FID"].ToString();
                            tn4.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
                            tn2.Nodes.Add(tn4);
                        }
                    }
                }
                tvRight.Nodes.Add(tn1);
            }
            tvRight.ExpandAll();
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        private bool CheckData()
        {
            if (drpOUList.SelectedIndex == 0)
            {
                hidInfo.Value = "请选择部门！";
                return false;
            }
            if (drpPersonList.SelectedIndex == 0)
            {
                hidInfo.Value = "请选择人员！";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存用户设置的权限
        /// </summary>
        private void Save()
        {
            long UserID = NDConvert.ToInt64(drpPersonList.SelectedValue);
            new RightQuery().DeleteUserRight(UserID);
            UserEntity Pentity = new UserEntity(UserID);

            new MenuFunctionRightQuery().DeleteUserMenuFunctionRight(UserID);

            Node node = null;
            RightEntity entity = null;
            MenuFunctionRightEntity mfEntity = null;
            for (int i = 0; i < tvRight.CheckedNodes.Count; i++)
            {
                node = (Node)tvRight.CheckedNodes[i];
                switch (node.Level)
                {
                    case 0:
                    case 1:
                        entity = new RightEntity();
                        entity.UserID = UserID;
                        entity.MenuID = NDConvert.ToInt64(node.DataKey.ToString());
                        new RightRule().Add(entity);
                        break;
                    case 2:
                        if (MenuQuery.IsHaveChileMenu(NDConvert.ToInt64(node.Parent.DataKey.ToString())))   //判断是否有子菜单,如有有子菜单说明本层是菜单,如果没有则是菜单功能项
                        {
                            entity = new RightEntity();
                            entity.UserID = UserID;
                            entity.MenuID = NDConvert.ToInt64(node.DataKey.ToString());
                            new RightRule().Add(entity);
                        }
                        else
                        {
                            mfEntity = new MenuFunctionRightEntity();
                            mfEntity.FID = NDConvert.ToInt64(node.DataKey.ToString());
                            mfEntity.UserID = UserID;
                            mfEntity.MenuID = NDConvert.ToInt64(node.Parent.DataKey.ToString());
                            new MenuFunctionRightRule().Add(mfEntity);
                        }
                        break;
                    case 3:
                        mfEntity = new MenuFunctionRightEntity();
                        mfEntity.FID = NDConvert.ToInt64(node.DataKey.ToString());
                        mfEntity.UserID = UserID;
                        mfEntity.MenuID = NDConvert.ToInt64(node.Parent.DataKey.ToString());
                        new MenuFunctionRightRule().Add(mfEntity);
                        break;
                }
            }
        }

        protected void drpChildList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpChildList.SelectedIndex != 0)
            {
                drpPersonList.Enabled = true;
                BindPerson(NDConvert.ToInt64(drpChildList.SelectedValue), 1);
                BindTreeView();
            }
            else
            {
                drpPersonList.Enabled = false;
                tvRight.ClearAll();
            }
        }
    }
}
