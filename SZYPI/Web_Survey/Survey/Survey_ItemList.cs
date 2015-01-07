using checkState;
using CreateItem;
using System;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Configuration;
using BusinessLayer.Survey;
using System.Data;
using System.IO;
using System.Globalization;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_ItemList : Page, IRequiresSessionState, System.Web.UI.ICallbackEventHandler
    {
        protected HtmlForm form1;
        protected GridView GridView1;
        public string sClientJs = "";
        public long SID;
        //protected SqlDataSource SqlDataSource1;
        public long UID;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1 = Page.FindControl("GridView1") as GridView;
            GridView1.PageIndexChanging+=new GridViewPageEventHandler(GridView1_PageIndexChanging);
            string str;
            long num;
            string[] strArray = null; //赋初值
            long num2;

            goto Label_003F;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (this.SID != 0)
                    {
                        //this.SqlDataSource1.SelectCommand = "SELECT IID,ItemName,ItemType,PageNo FROM ItemTable WHERE SID=" + this.SID.ToString() + " AND ParentID=0 ORDER BY Sort";
                        //DataSet GridViewDataSource = new Survey_ItemList_Layer().GetGridViewItemTable(this.SID.ToString(),"0");
                        //this.GridView1.DataSource = GridViewDataSource;
                        //this.GridView1.DataBind();
                        this.DataBd();

                        num2 = 9;
                    }
                    else
                    {
                        num2 = 5;
                    }
                    goto Label_0002;

                case 1:
                    goto Label_02AD;

                case 2:
                    if (!(strArray[0] == "18"))
                    {
                        CreateItem.CreateItem item = new CreateItem.CreateItem(); //赋初值
                        string sClientJs = this.sClientJs;
                        this.sClientJs = sClientJs + "blnActioned='True';\nintPageNo=" + strArray[1] + ";\nintItemType=" + strArray[0] + ";\n";
                        //OleDbConnection connection = new OleDbConnection(this.sConn);
                        //OleDbCommand objComm = new OleDbCommand("", connection);
                        //connection.Open();
                        item.getLanguage();
                        item.createItem(strArray,this.SID, this.UID, "");

                        this.DataBd();

                        num2 = 6;
                    }
                    else
                    {
                        num2 = 11;
                    }
                    goto Label_0002;

                case 3:
                    strArray = new CreateItem.CreateItem().getFormsData();
                    num2 = 2;
                    goto Label_0002;

                case 4:
                    goto Label_01C1;

                case 5:
                    return;

                case 6:
                    return;                                                                                                                  

                case 7:
                    this.delItem(this.SID, num);
                    this.sClientJs = this.sClientJs + "blnActioned='True';\n";
                    num2 = 1;
                    goto Label_0002;

                case 8:
                    if (!(base.Request.QueryString["f"] == "ok"))
                    {
                        goto Label_01C1;
                    }
                    num2 = 12;
                    goto Label_0002;

                case 9:
                    if (!(str == "Del"))
                    {
                        goto Label_02AD;
                    }
                    num2 = 7;
                    goto Label_0002;

                case 10:
                    if (base.Request.Form["Submit"] == null)
                    {
                        return;
                    }
                    num2 = 3;
                    goto Label_0002;

                case 11:
                    return;

                case 12:
                    this.sClientJs = this.sClientJs + "blnActioned='True';\n";
                    num2 = 4;
                    goto Label_0002;
            }
        Label_003F:
            UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            str = "";
            str = base.Request.QueryString["A"];
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            num = Convert.ToInt64(base.Request.QueryString["IID"]);
            num2 = 0;
            goto Label_0002;
        Label_01C1:
            num2 = 10;
            goto Label_0002;
        Label_02AD:
            num2 = 8;
            goto Label_0002;
        }

        public void DataBd()
        {
            DataSet GridViewDataSource = new Survey_ItemList_Layer().GetGridViewItemTable(this.SID.ToString(), "0");
            this.GridView1.DataSource = GridViewDataSource.Tables[0];
            this.GridView1.DataBind();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string itemName = ConvertHelper.ConvertString(GridView1.DataKeys[i]["ItemName"]);

                if (itemName.Length > 6)
                {
                    GridView1.Rows[i].Cells[1].Text = Truncate(itemName, 11, "...");
                    GridView1.Rows[i].Cells[1].ToolTip = itemName;
                }

                string itemType = GridView1.Rows[i].Cells[2].Text;

                if (itemType.Length > 8)
                {
                    GridView1.Rows[i].Cells[2].Text = Truncate(itemType, 15, "...");
                    GridView1.Rows[i].Cells[2].ToolTip = itemType;
                }

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = new Survey_ItemList_Layer().GetGridViewItemTable(this.SID.ToString(), "0");              //引用刚才建立的数据源
            GridView1.DataBind();
        }
 

        public string GetCallbackResult()
        {
            return RenderControl(GridView1);
        }
        public void RaiseCallbackEvent(string eventArgument)
        {
            this.DataBd();
        }
        private string RenderControl(Control c)
        {
            StringWriter writer1 = new StringWriter(CultureInfo.InvariantCulture);
            HtmlTextWriter writer = new HtmlTextWriter(writer1);
            c.RenderControl(writer);
            writer.Flush();
            writer.Close();
            return writer1.ToString();
        }

        protected bool checkUserItemLimist(string sUserLimits, string sItemType)
        {
            string str = null; //赋初值
            int num = 14;
        Label_000D:
            switch (num)
            {
                case 0:
                    if (str == "6")
                    {
                        goto Label_013C;
                    }
                    num = 0x11;
                    goto Label_000D;

                case 1:
                case 13:
                case 15:
                case 0x12:
                    goto Label_00D7;

                case 2:
                    num = 0;
                    goto Label_000D;

                case 3:
                    if (sUserLimits.IndexOf(sItemType) < 0)
                    {
                        return false;
                    }
                    num = 0x13;
                    goto Label_000D;

                case 4:
                    num = 0x10;
                    goto Label_000D;

                case 5:
                    if (str == "5")
                    {
                        goto Label_013C;
                    }
                    num = 2;
                    goto Label_000D;

                case 6:
                    num = 8;
                    goto Label_000D;

                case 7:
                    if (str == "11")
                    {
                        goto Label_013C;
                    }
                    num = 11;
                    goto Label_000D;

                case 8:
                    if (str == "14")
                    {
                        sItemType = "1";
                        num = 13;
                    }
                    else
                    {
                        num = 10;
                    }
                    goto Label_000D;

                case 9:
                    if (str == "9")
                    {
                        break;
                    }
                    num = 4;
                    goto Label_000D;

                case 10:
                    num = 0x12;
                    goto Label_000D;

                case 11:
                    if ((1 == 0) || (0 == 0))
                    {
                        num = 9;
                        goto Label_000D;
                    }
                    goto Label_00D7;

                case 12:
                    num = 5;
                    goto Label_000D;

                case 0x10:
                    if (str == "10")
                    {
                        break;
                    }
                    num = 6;
                    goto Label_000D;

                case 0x11:
                    num = 7;
                    goto Label_000D;

                case 0x13:
                    return true;

                default:
                    str = sItemType;
                    if (str == null)
                    {
                        goto Label_00D7;
                    }
                    num = 12;
                    goto Label_000D;
            }
            sItemType = "8";
            num = 15;
            goto Label_000D;
        Label_00D7:
            string text1 = "it:" + sItemType;
            num = 3;
            goto Label_000D;
        Label_013C:
            sItemType = "4";
            num = 1;
            goto Label_000D;
        }

        public void delItem(long SID1, long IID1)
        {
        Label_0017:
            checkState.checkState state = new checkState.checkState();
            int num = 2;
        Label_0002:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    base.Response.End();
                    num = 0;
                    goto Label_0002;

                case 2:
                    if (state.getState( SID1, this.UID) < 1)
                    {
                        break;
                    }
                    num = 1;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            //command.CommandText = "UPDATE ItemTable SET Sort=Sort-1 WHERE SID=" + this.SID.ToString() + " AND UID=" + this.UID.ToString() + " AND PageNo=(SELECT TOP 1 PageNo FROM ItemTable WHERE SID=" + SID1.ToString() + " AND IID=" + IID1.ToString() + " AND UID=" + this.UID.ToString() + ") AND Sort>(SELECT TOP 1 Sort FROM ItemTable WHERE SID=" + SID1.ToString() + " AND IID=" + IID1.ToString() + " AND UID=" + this.UID.ToString() + ")";
            new Survey_ItemList_Layer().CreateItemText_UpdateItemTable(this.SID.ToString(), this.UID.ToString(), SID1.ToString(), IID1.ToString());
            //command.CommandText = " DELETE FROM ItemTable WHERE SID=" + SID1.ToString() + " AND (IID=" + IID1.ToString() + " OR ParentID=" + IID1.ToString() + ") AND UID=" + this.UID.ToString();
            new Survey_ItemList_Layer().DeleteItemTable(SID1.ToString(), IID1.ToString(), IID1.ToString(), this.UID.ToString());
            //command.CommandText = " DELETE FROM OptionTable WHERE SID=" + SID1.ToString() + " AND IID=" + IID1.ToString() + " AND UID=" + this.UID.ToString();
            new Survey_ItemList_Layer().DeleteOptionTable(SID1.ToString(), IID1.ToString(), this.UID.ToString());
            this.DataBd();
        }

        public string getItemType(int intItemType1)
        {
            string[] strArray = new string[] { 
            "", "文字输入题", "数值输入题", "多行文字输入题", "单选题[点选式]", "单选+文字输入题[点选式]", "单选题[下拉式]", "单选矩阵题[点选式]", "多选题[点选式]", "多选+文字输入题[点选式]", "多选题[列表式]", "等级题", "排序题", "列举题", "文字", "多选矩阵题", 
            "矩阵输入题", "文件上传", "3D矩阵-下拉框", "矩阵单选+输入", "矩阵多选+输入", "百分比题"
         };
            return strArray[intItemType1];
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int num;
            string str;
            int num3;
            goto Label_0023;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_0066;
                    }
                    goto Label_014F;

                case 1:
                    str = str.Substring(0, 9) + "...";
                    num3 = 0;
                    goto Label_0002;

                case 2:
                    if (e.Row.RowType != DataControlRowType.DataRow)
                    {
                        return;
                    }
                    num3 = 3;
                    goto Label_0002;

                case 3:
                    goto Label_014F;

                case 4:
                    if (str.Length <= 10)
                    {
                        goto Label_0066;
                    }
                    num3 = 1;
                    goto Label_0002;

                case 5:
                    return;
            }
        Label_0023:
            num = 0;
            str = "";
            int num2 = 1;
            string str2 = "";
            string str3 = "";
            num3 = 2;
            goto Label_0002;
        Label_0066:
            e.Row.Cells[1].Text = str;
            e.Row.Cells[1].ToolTip = str3;
            str2 = this.getItemType(num2);
            e.Row.Cells[2].Text = str2;
            e.Row.Cells[4].Text = "<a href='ItemList.aspx?SID=" + this.SID.ToString() + "&IID=" + num.ToString() + "&A=Del'><img src='images/del.gif' alt='删除' border='0' /></a>";
            num3 = 5;
            goto Label_0002;
        Label_014F:
            num = Convert.ToInt32(this.GridView1.DataKeys[e.Row.RowIndex][0]);
            num2 = Convert.ToInt32(this.GridView1.DataKeys[e.Row.RowIndex][2]);
            Convert.ToInt32(this.GridView1.DataKeys[e.Row.RowIndex][3]);
            str = this.GridView1.DataKeys[e.Row.RowIndex][1].ToString();
            str3 = str;
            num3 = 4;
            goto Label_0002;
        }


        #region 截取字符串方法（中英区分）
        /// <summary>   
        /// 截取字符串长度(高效)   
        /// </summary>   
        /// <param name="original">要截取的字符串对象</param>   
        /// <param name="length">要保留的字符个数</param>   
        /// <param name="suffix">后缀(用以替换超出长度部分)</param>   
        /// <returns></returns>   
        public static string Truncate(string original, int length, string suffix)
        {
            int len = original.Length;
            int i = 0;
            for (; i < length && i < len; ++i)
            {
                if ((int)(original[i]) > 0xFF)
                    --length;
            }
            if (length < i)
                length = i;
            else if (length > len)
                length = len;
            return original.Substring(0, length) + suffix;
        }
        #endregion

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
