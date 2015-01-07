namespace WebManage.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using Nandasoft;
    using WebUI;
    using BLL.Rule;

	/// <summary>
	///		utlDiaoCha ��ժҪ˵����
	/// </summary>
	public class utlDiaoCha2 : System.Web.UI.UserControl
	{
        protected System.Web.UI.WebControls.ImageButton btnTiJiao;
        protected System.Web.UI.WebControls.ImageButton ImageButton1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // �ڴ˴������û������Գ�ʼ��ҳ��


        }

        //public NewEase.Framework.DataBase.DBFactory DBAcc
        //{
        //    get
        //    {
        //        return ((FrameWeb.BasePage)this.Page).DBAcc;
        //    }
        //}

        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
        ///		�޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTiJiao.Click += new System.Web.UI.ImageClickEventHandler(this.btnTiJiao_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        private void btnTiJiao_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string ip = Request.UserHostAddress;
            //DataTable dt = DBAcc.Fill("");
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            for (int i = 0; i < 20; i++)
            {
                string id = this.Request.Form["t_" + i.ToString()];
                if ((id == null) || id == "")
                {
                    break;
                }

                #region ��⣺5������ͬһip���������ύ
                if (i == 0)
                {
                    DataTable dt = NDDBAccess.Fill("select edittime from UT_DiaoChaResult where diaochaid='" + id + "' and ip='" + ip + "'");
                    if (dt.Rows.Count > 0)
                    {
                        TimeSpan sp = DateTime.Now - Convert.ToDateTime(dt.Rows[0]["edittime"]);
                        if (sp.Minutes <= 5)
                        {
                            PageHelper.ShowExceptionMessage("5������ͬһip���������ύ");
                            return;
                        }
                    }
                }
                #endregion

                #region ����ʵ��
                BLL.Entity.DiaoChaResultEntity entity = new BLL.Entity.DiaoChaResultEntity();
                entity.id = Guid.NewGuid().ToString();
                entity.ip = ip;
                entity.edittime = DateTime.Now;
                entity.DiaoChaID = id;
                
                string a1 = this.Request.Form["d_" + i.ToString()];
                if (a1 == null || a1 == "")
                {
                    continue;
                }
                foreach (string j in a1.Split(','))
                {
                    //entity.SetFieldValue("a" + j.ToString(), "1");

                    if (j == "1") 
                        entity.a1 = 1;
                    if (j == "2") 
                        entity.a2 = 1;
                    if (j == "3") 
                        entity.a3 = 1;
                    if (j == "4") 
                        entity.a4 = 1;
                    if (j == "5") 
                        entity.a5 = 1;
                    if (j == "6") 
                        entity.a6 = 1;
                }
                #endregion
                list.Add(entity);
            }

            if (list.Count > 0)
            {
                
                try
                {
                    DiaoChaResultRule rule = new DiaoChaResultRule();

                    foreach (BLL.Entity.DiaoChaResultEntity entity in list)
                    {
                        rule.Add(entity);
                    }
                    
                    PageHelper.ShowExceptionMessage("лл����");
                }
                catch (Exception E)
                {
                    
                    PageHelper.ShowExceptionMessage(E);
                }
            }

        }

        private void Button2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

        }
	
	}
}
