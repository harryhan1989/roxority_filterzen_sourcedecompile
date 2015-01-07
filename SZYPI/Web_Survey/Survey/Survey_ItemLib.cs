using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_ItemLib : Page, IRequiresSessionState
    {
        protected HtmlForm form1;
        protected GridView GridView1;

        public string getItemType(int intItemType1)
        {
            string[] strArray = new string[] { 
            "", "文字输入题", "数值输入题", "多行文字输入题", "单选题[点选式]", "单选+文字输入题[点选式]", "单选题[下拉式]", "单选矩阵题[点选式]", "多选题[点选式]", "多选+文字输题[点选式]", "多选题[列表式]", "等级题", "排序题", "列举题", "文字", "多选矩阵题", 
            "矩阵输入题", "文件上传", "3D矩阵-下拉框", "矩阵单选+输入", "矩阵多选+输入", "百分比题"
         };
            return strArray[intItemType1];
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        Label_0017:
            long num = 0;
            long num2 = Convert.ToInt64(base.Request.QueryString["SID"]);
            int num3 = 0;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (e.Row.RowType != DataControlRowType.DataRow)
                    {
                        break;
                    }
                    num3 = 2;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    num = Convert.ToInt64(this.GridView1.DataKeys[e.Row.RowIndex][0]);
                    e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
                    e.Row.Cells[1].Text = this.getItemType(Convert.ToInt32(e.Row.Cells[1].Text));
                    e.Row.Cells[2].Attributes.Add("onclick", "delItem(" + num.ToString() + ")");
                    e.Row.Cells[2].Text = "删除";
                    e.Row.Cells[3].Text = "加入";
                    e.Row.Cells[3].Attributes.Add("onclick", "copyItem(" + num2.ToString() + "," + num.ToString() + ");");
                    e.Row.Cells[3].Attributes.Add("title", "复制题目到问卷");
                    e.Row.Cells[3].Attributes.Add("style='cursor:pointer'", "");
                    e.Row.Cells[2].Attributes.Add("title", "从题目库删除题目");
                    e.Row.Cells[2].Attributes.Add("style='cursor:pointer'", "");
                    num3 = 1;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
        Label_001F:
            num = 0;
            num = ConvertHelper.ConvertLong(this.Session["UserID"]);
            Convert.ToInt32(base.Request.QueryString["SID"]);
            //this.SqlDataSource1.SelectCommand = "SELECT LIID,IID,ItemName,ItemType,Active FROM ItemLib WHERE UID=" + num.ToString() + " OR Active=1";
            DataTable dtItemLib = new Survey_ItemLib_Layer().GetItemLib(num.ToString());
            GridView1.DataSource = dtItemLib;
            GridView1.DataBind();
            string str = Convert.ToString(base.Request.QueryString["A"]);
            long num2 = Convert.ToInt64(base.Request.QueryString["LIID"]);
            int num3 = 3;
        Label_0002:
            switch (num3)
            {
                case 0:
                    num3 = 1;
                    goto Label_0002;

                case 1:
                    if (num2 <= 0)
                    {
                        break;
                    }
                    num3 = 2;
                    goto Label_0002;

                case 2:
                    {
                        //command.CommandText = "DELETE FROM ItemLib WHERE (UID=" + num.ToString() + " AND LIID=" + num2.ToString() + ")";
                        new Survey_ItemLib_Layer().DelItemLib(num.ToString(), num2.ToString());
                        num3 = 4;
                        goto Label_0002;
                    }
                case 3:
                    if (!(str == "Del"))
                    {
                        break;
                    }
                    num3 = 0;
                    goto Label_0002;

                case 4:
                    break;

                default:
                    goto Label_001F;
            }
        }

        protected HttpApplication ApplicationInstance
        {
            get
            {
                return this.Context.ApplicationInstance;
            }
        }

        protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile)this.Context.Profile;
            }
        }
    }
}
