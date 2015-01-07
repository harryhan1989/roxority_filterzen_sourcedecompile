using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nandasoft;
using WebUI;
using BLL;

namespace WebManage.Web.SysManager
{
    public partial class CodeGroup : BasePage
    {
        /// <summary>
        /// 页面加载
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
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindTreeView();
        }

        /// <summary>
        /// toolbar事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            long ID = 0;
            switch (e.Item.Value)
            {
                case "Delete":
                    if (e.Item.Value == "Delete")
                    {
                        int succeed = 0;
                        string CodeGroupNames = "";
                        for (int j = 0; j < tvOU.CheckedNodes.Count; j++)
                        {
                            Infragistics.WebUI.UltraWebNavigator.Node tn = (Infragistics.WebUI.UltraWebNavigator.Node)tvOU.CheckedNodes[j];
                            long CodeGroupID = NDConvert.ToInt64(tn.DataKey.ToString());
                            
                            CodeGroupEntity entity = new CodeGroupEntity(CodeGroupID);
                            if (!CodeQuery.IsExistCode(CodeGroupID))
                            {
                                entity.IsDeleted = true;
                                new CodeGroupRule().Update(entity);
                                succeed++;
                            }
                            else
                            {
                                CodeGroupNames += "[" + entity.CodeGroupName + "]";
                            }
                        }
                        if (succeed > 0)
                        {
                            BindTreeView();
                            PageHelper.ShowMessage("删除成功！");
                        }
                        if (CodeGroupNames != "")
                        {
                            string msgErr = "代码组名称为" + CodeGroupNames + "的记录不可删除！\\n\\n" +
                                            "可先删除代码组中包含的代码记录后再删除该代码组！";
                            PageHelper.ShowExceptionMessage(msgErr);
                        }
                    }
                    break;
                case "Update":
                    ID = NDConvert.ToInt64(((Infragistics.WebUI.UltraWebNavigator.Node)tvOU.CheckedNodes[0]).DataKey.ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "更新", "showModalWindow('CodeGroupEditLY', '修改',750, 220, '../../web/SysManager/CodeGroupEdit.aspx?Operation=2&ID=' + " + ID + ");", true);
                    break;               
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            BindTreeView();
        }

        /// <summary>
        /// 邦定部门列表
        /// </summary>
        private void BindTreeView()
        {
            tvOU.ClearAll();
            DataTable dt = null;

            Infragistics.WebUI.UltraWebNavigator.Node tn1;
            Infragistics.WebUI.UltraWebNavigator.Node tn2;
  
            //一层
            dt = new CodeGroupQuery().GetAllCodeGroup();
           
            tn1 = new Infragistics.WebUI.UltraWebNavigator.Node();
            tn1.Text = "系统代码组";
            tn1.DataKey = "0";
            tn1.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.False;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tn2 = new Infragistics.WebUI.UltraWebNavigator.Node();
                tn2.Text = dt.Rows[i]["CodeGroupName"].ToString();
                tn2.DataKey = dt.Rows[i]["CodeGroupID"].ToString();
                tn2.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
                tn2.TargetUrl = "javascript:window.parent.frames['Code'].location.href = 'Code.aspx?ID=" + tn2.DataKey.ToString() + "';";
                tn1.Nodes.Add(tn2);               
            }
            tvOU.Nodes.Add(tn1);
        
            tvOU.ExpandAll();
        }
    }
}
