using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUI;
using Nandasoft;
using BLL;

namespace WebManage.Web.SystemManager
{
    public partial class OUManager : BasePage
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
                InitPage();
            }
        }

        /// <summary>
        /// ˢ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindTreeView();
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
                case "Delete":
                    if (e.Item.Value == "Delete")
                    {
                        for (int i = 0; i < tvOU.CheckedNodes.Count; i++)
                        {
                            Infragistics.WebUI.UltraWebNavigator.Node tn = (Infragistics.WebUI.UltraWebNavigator.Node)tvOU.CheckedNodes[i];
                            if (tn.Nodes.Count > 0)
                            {
                                PageHelper.ShowExceptionMessage("ѡ��ɾ���Ĳ����в��ܰ������¼����ţ�");
                                return;
                            }

                            if (UserQuery.IsHavePerson(NDConvert.ToInt64(tn.DataKey.ToString())))
                            {
                                PageHelper.ShowExceptionMessage("ѡ��ɾ���Ĳ���������Ա,����ɾ����Ա����ɾ�����ţ�");
                                return;
                            }
                        }

                        for (int j = 0; j < tvOU.CheckedNodes.Count; j++)
                        {
                            Infragistics.WebUI.UltraWebNavigator.Node tn = (Infragistics.WebUI.UltraWebNavigator.Node)tvOU.CheckedNodes[j];
                            OUEntity entity = new OUEntity(NDConvert.ToInt64(tn.DataKey.ToString()));
                            entity.UpdateDate = DateTime.Now;
                            entity.IsDeleted = true;
                            new OURule().Update(entity);
                        }
                        BindTreeView();
                        PageHelper.ShowMessage("ɾ���ɹ���");
                    }
                    break;
                case "Add":
                    if (tvOU.CheckedNodes.Count > 0)
                    {
                        int tnLevel = ((Infragistics.WebUI.UltraWebNavigator.Node)tvOU.CheckedNodes[0]).Level;
                        if (tnLevel >= 3)
                        {
                            PageHelper.ShowExceptionMessage("�˴���������ͼֻ֧�ֵ�4�㣡");
                            return;
                        }
                        ID = NDConvert.ToInt64(((Infragistics.WebUI.UltraWebNavigator.Node)tvOU.CheckedNodes[0]).DataKey.ToString());
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "add", "showModalWindow('OUDetailLY', '���',600, 450, '../../web/SysManager/OUDetail.aspx?Operation=1&ID=' + " + ID + ");", true);
                    break;
                case "Update":
                    ID = NDConvert.ToInt64(((Infragistics.WebUI.UltraWebNavigator.Node)tvOU.CheckedNodes[0]).DataKey.ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "update", "showModalWindow('OUDetailLY', '�޸�',600, 450, '../../web/SysManager/OUDetail.aspx?Operation=2&ID=' + " + ID + ");", true);
                    break;
            }
        }

        /// <summary>
        /// ��ʼ��ҳ��
        /// </summary>
        private void InitPage()
        {
            BindTreeView();
        }

        /// <summary>
        /// ������б�
        /// </summary>
        private void BindTreeView()
        {
            tvOU.ClearAll();
            DataTable dt = null;
            DataTable dt1 = null;
            DataTable dt2 = null;
            DataTable dt3 = null;

            Infragistics.WebUI.UltraWebNavigator.Node tn1;
            Infragistics.WebUI.UltraWebNavigator.Node tn2;
            Infragistics.WebUI.UltraWebNavigator.Node tn3;
            Infragistics.WebUI.UltraWebNavigator.Node tn4;

            //һ��
            dt = new OUQuery().GetOU(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tn1 = new Infragistics.WebUI.UltraWebNavigator.Node();
                tn1.Text = dt.Rows[i]["OUName"].ToString();
                tn1.DataKey = dt.Rows[i]["OUID"].ToString();
                tn1.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;

                if (new OUQuery().IsOUParent(NDConvert.ToInt64(tn1.DataKey)) == false)
                {
                    tn1.TargetUrl = "javascript:window.parent.frames['PersonManager'].location.href = 'UserManager.aspx?OUID=" + tn1.DataKey.ToString() + "';";
                }

                //����
                dt1 = new OUQuery().GetOU(NDConvert.ToInt64(tn1.DataKey));
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    tn2 = new Infragistics.WebUI.UltraWebNavigator.Node();
                    tn2.Text = dt1.Rows[j]["OUName"].ToString();
                    tn2.DataKey = dt1.Rows[j]["OUID"].ToString();
                    tn2.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
                    tn2.TargetUrl = "javascript:window.parent.frames['PersonManager'].location.href = 'UserManager.aspx?OUID=" + tn2.DataKey.ToString() + "';";
                    tn1.Nodes.Add(tn2);

                    //����
                    dt2 = new OUQuery().GetOU(NDConvert.ToInt64(tn2.DataKey));
                    for (int m = 0; m < dt2.Rows.Count; m++)
                    {
                        tn3 = new Infragistics.WebUI.UltraWebNavigator.Node();
                        tn3.Text = dt2.Rows[m]["OUName"].ToString();
                        tn3.DataKey = dt2.Rows[m]["OUID"].ToString();
                        tn3.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
                        tn3.TargetUrl = "javascript:window.parent.frames['PersonManager'].location.href = 'UserManager.aspx?OUID=" + tn3.DataKey.ToString() + "';";
                        tn2.Nodes.Add(tn3);

                        //�Ĳ�
                        dt3 = new OUQuery().GetOU(NDConvert.ToInt64(tn3.DataKey));
                        for (int n = 0; n < dt3.Rows.Count; n++)
                        {
                            tn4 = new Infragistics.WebUI.UltraWebNavigator.Node();
                            tn4.Text = dt3.Rows[n]["OUName"].ToString();
                            tn4.DataKey = dt3.Rows[n]["OUID"].ToString();
                            tn4.CheckBox = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
                            tn4.TargetUrl = "javascript:window.parent.frames['PersonManager'].location.href = 'UserManager.aspx?OUID=" + tn4.DataKey.ToString() + "';";
                            tn3.Nodes.Add(tn4);
                        }
                    }
                }
                tvOU.Nodes.Add(tn1);
            }
            tvOU.ExpandAll();
        }
    }
}
