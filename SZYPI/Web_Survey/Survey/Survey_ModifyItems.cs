using LoginClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_ModifyItems : Page, IRequiresSessionState
    {
        protected HtmlTextArea BackSelect1 = new HtmlTextArea();
        protected HtmlInputHidden flag1 = new HtmlInputHidden();
        protected HtmlInputHidden iid1 = new HtmlInputHidden();
        public long  IID;
        public int intItemType;
        public int intLevelAmount;
        public int intMaxTickoff;
        public int intMinTickoff;
        public short intPageNo;
        protected HtmlInputText ItemName1 = new HtmlInputText();
        protected HtmlInputHidden ItemType1 = new HtmlInputHidden();
        protected HtmlInputText LevelAmount1 = new HtmlInputText();
        protected HtmlTextArea MatrixRowColumn1 = new HtmlTextArea();
        protected HtmlInputText MaxLevelName1 = new HtmlInputText();
        protected HtmlInputText MaxPercent1 = new HtmlInputText();
        protected HtmlInputText MaxSelect1 = new HtmlInputText();
        protected HtmlInputText MaxTickoff1 = new HtmlInputText();
        protected HtmlInputText MaxValue1 = new HtmlInputText();
        protected HtmlTextArea Memo1 = new HtmlTextArea();
        protected HtmlInputText MinLevelName1 = new HtmlInputText();
        protected HtmlInputText MinPercent1 = new HtmlInputText();
        protected HtmlInputText MinSelect1 = new HtmlInputText();
        protected HtmlInputText MinTickoff1 = new HtmlInputText();
        protected HtmlInputText MinValue1 = new HtmlInputText();
        protected HtmlInputHidden OldPageNo1 = new HtmlInputHidden();
        public string sBackSelectList = "";
        public string sClientJs = "";
        protected HtmlInputHidden sid1 = new HtmlInputHidden();
        public long  SID;
        public string sIsMatrixRowColumn = "";
        public string sItemName = "";
        public string sMemo = "";
        public string sSubItemList = "";
        protected HtmlTextArea SubItem1 = new HtmlTextArea();
        protected HtmlInputText TotalPercent1 = new HtmlInputText();

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            short num2;
            DataSet set;
            int num6;
            goto Label_006B;
        Label_0002:
            switch (num6)
            {
                case 0:
                    if (!(this.sMemo != ""))
                    {
                        goto Label_0960;
                    }
                    num6 = 14;
                    goto Label_0002;

                case 1:
                    if (num2 < set.Tables["SubItemTable"].Rows.Count)
                    {
                        this.sSubItemList = this.sSubItemList + set.Tables["SubItemTable"].Rows[num2]["ItemName"].ToString() + "\n";
                        num2 = (short)(num2 + 1);
                        num6 = 4;
                    }
                    else
                    {
                        num6 = 0x11;
                    }
                    goto Label_0002;

                case 2:
                case 8:
                    num6 = 15;
                    goto Label_0002;

                case 3:
                    goto Label_0960;

                case 4:
                case 0x10:
                    num6 = 1;
                    goto Label_0002;

                case 5:
                    if (!(this.sBackSelectList != ""))
                    {
                        goto Label_080C;
                    }
                    num6 = 0x15;
                    goto Label_0002;

                case 6:
                    goto Label_080C;

                case 7:
                case 0x16:
                    num2 = (short)(num2 + 1);
                    num6 = 8;
                    goto Label_0002;

                case 9:
                    this.sSubItemList = this.sSubItemList.Remove(this.sSubItemList.Length - 1, 1);
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num6 = 13;
                    goto Label_0002;

                case 10:
                    if (set.Tables["ItemTable"].Rows.Count != 0)
                    {
                        goto Label_03F9;
                    }
                    num6 = 0x13;
                    goto Label_0002;

                case 11:
                    this.sBackSelectList = this.sBackSelectList + set.Tables["OptionTable"].Rows[num2]["OptionName"].ToString() + "\n";
                    num6 = 0x16;
                    goto Label_0002;

                case 12:
                    num2 = 0;
                    num6 = 0x10;
                    goto Label_0002;

                case 13:
                    goto Label_0712;

                case 14:
                    this.sMemo = this.sMemo.Replace("<BR>", "\r\n");
                    num6 = 3;
                    goto Label_0002;

                case 15:
                    if (num2 < set.Tables["OptionTable"].Rows.Count)
                    {
                        num6 = 20;
                    }
                    else
                    {
                        num6 = 12;
                    }
                    goto Label_0002;

                case 0x11:
                    num6 = 5;
                    goto Label_0002;

                case 0x12:
                    if (!(this.sSubItemList != ""))
                    {
                        goto Label_0712;
                    }
                    num6 = 9;
                    goto Label_0002;

                case 0x13:
                    base.Response.Write("Error");
                    base.Response.End();
                    num6 = 0x17;
                    goto Label_0002;

                case 20:
                    if (!(set.Tables["OptionTable"].Rows[num2]["IsMatrixRowColumn"].ToString() == "False"))
                    {
                        this.sIsMatrixRowColumn = this.sIsMatrixRowColumn + set.Tables["OptionTable"].Rows[num2]["OptionName"].ToString() + "\n";
                        num6 = 7;
                    }
                    else
                    {
                        num6 = 11;
                    }
                    goto Label_0002;

                case 0x15:
                    this.sBackSelectList = this.sBackSelectList.Remove(this.sBackSelectList.Length - 1, 1);
                    num6 = 6;
                    goto Label_0002;

                case 0x17:
                    goto Label_03F9;
            }
        Label_006B:
            long uID = 0;
            uID=ConvertHelper.ConvertLong(this.Session["UserID"]);
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            this.IID = Convert.ToInt64(base.Request.QueryString["IID"]);
            num2 = 0;
            StringBuilder builder = new StringBuilder();
            set = new DataSet();
            //selectCommand.CommandText = "SELECT TOP 1 IID,ItemName,ItemType,DataFormatCheck,PageNo,OtherProperty,OptionImgModel,OrderModel,OptionAmount,ItemContent FROM ItemTable WHERE SID=" + this.SID.ToString() + " AND UID=" + uID.ToString() + " AND IID=" + this.IID.ToString();
            //adapter.Fill(set, "ItemTable");
            DataTable ItemTable = new Survey_ModifyItems_Layer().GetItemTable(this.SID.ToString(), uID.ToString(), this.IID.ToString());
            ItemTable.TableName = "ItemTable";

            //selectCommand.CommandText = "SELECT TOP 200 IID,ItemName,ItemType,DataFormatCheck,PageNo,OtherProperty,OptionImgModel,OrderModel FROM ItemTable WHERE SID=" + this.SID.ToString() + " AND UID=" + uID.ToString() + " AND ParentID=" + this.IID.ToString();
            //adapter.Fill(set, "SubItemTable");
            DataTable SubItemTable = new Survey_ModifyItems_Layer().GetItemTable(this.SID.ToString(), uID.ToString(), this.IID.ToString());
            SubItemTable.TableName = "SubItemTable";

            //selectCommand.CommandText = "SELECT OptionName,ISMatrixRowColumn FROM OptionTable WHERE SID=" + this.SID.ToString() + " AND UID=" + uID.ToString() + " AND IID=" + this.IID.ToString();
            //adapter.Fill(set, "OptionTable");
            DataTable OptionTable = new Survey_ModifyItems_Layer().GetOptionTable(this.SID.ToString(), uID.ToString(), this.IID.ToString());
            OptionTable.TableName = "OptionTable";

            //selectCommand.CommandText = "SELECT PID FROM PageTable WHERE SID=" + this.SID.ToString() + " AND UID=" + uID.ToString();
            //adapter.Fill(set, "PageTable");
            DataTable PageTable = new Survey_ModifyItems_Layer().GetPageTable(this.SID.ToString(), uID.ToString());
            PageTable.TableName = "PageTable";

            set.Tables.Add(ItemTable);
            set.Tables.Add(SubItemTable);
            set.Tables.Add(OptionTable);
            set.Tables.Add(PageTable);


            num6 = 10;
            goto Label_0002;
        Label_03F9:
            this.intPageNo = Convert.ToInt16(set.Tables["ItemTable"].Rows[0]["PageNo"]);
            builder.Append("var sCheckStr='" + set.Tables["ItemTable"].Rows[0]["DataFormatCheck"].ToString() + "';\n");
            builder.Append("var intPageNo=" + set.Tables["ItemTable"].Rows[0]["PageNo"].ToString() + ";\n");
            builder.Append("var sValueName = '" + set.Tables["ItemTable"].Rows[0]["OtherProperty"].ToString() + "';\n");
            builder.Append("var sOrderModel = '" + set.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() + "';\n");
            builder.Append("var sOtherProperty = '" + set.Tables["ItemTable"].Rows[0]["OtherProperty"].ToString() + "';\n");
            builder.Append("var intItemType = " + set.Tables["ItemTable"].Rows[0]["ItemType"].ToString() + ";\n");
            builder.Append("var intOptionImg = '" + set.Tables["ItemTable"].Rows[0]["OptionImgModel"].ToString() + "';\n");
            this.intItemType = Convert.ToInt32(set.Tables["ItemTable"].Rows[0]["ItemType"]);
            num2 = 0;
            num6 = 2;
            goto Label_0002;
        Label_0712:
            this.intMaxTickoff = Convert.ToInt16(set.Tables["ItemTable"].Rows[0]["OptionAmount"]);
            this.intLevelAmount = Convert.ToInt16(set.Tables["ItemTable"].Rows[0]["OptionAmount"]);
            this.sItemName = set.Tables["ItemTable"].Rows[0]["ItemName"].ToString();
            this.sMemo = set.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            num6 = 0;
            goto Label_0002;
        Label_080C:
            num6 = 0x12;
            goto Label_0002;
        Label_0960:
            this.Memo1 = Page.FindControl("Memo") as HtmlTextArea;
            this.Memo1.Value = this.sMemo;

            this.ItemName1 = Page.FindControl("ItemName") as HtmlInputText;
            this.ItemName1.Value = this.sItemName;

            this.MaxTickoff1 = Page.FindControl("MaxTickoff") as HtmlInputText;
            this.MaxTickoff1.Value = this.intMaxTickoff.ToString();

            this.MatrixRowColumn1 = Page.FindControl("MatrixRowColumn") as HtmlTextArea;
            this.MatrixRowColumn1.Value = this.sIsMatrixRowColumn;

            this.LevelAmount1 = Page.FindControl("LevelAmount") as HtmlInputText;
            this.LevelAmount1.Value = this.intLevelAmount.ToString();

            this.BackSelect1 = Page.FindControl("BackSelect") as HtmlTextArea;
            this.BackSelect1.Value = this.sBackSelectList;

            this.SubItem1 = Page.FindControl("SubItem") as HtmlTextArea;
            this.SubItem1.Value = this.sSubItemList;

            this.sid1 = Page.FindControl("sid") as HtmlInputHidden;
            this.sid1.Value = this.SID.ToString();

            this.iid1 = Page.FindControl("iid") as HtmlInputHidden;
            this.iid1.Value = this.IID.ToString();

            this.flag1 = Page.FindControl("flag") as HtmlInputHidden;
            this.flag1.Value = this.intItemType.ToString();

            this.ItemType1 = Page.FindControl("ItemType") as HtmlInputHidden;
            this.ItemType1.Value = this.intItemType.ToString();

            builder.Append("var intOptionAmount = " + set.Tables["OptionTable"].Rows.Count.ToString() + ";\n");
            builder.Append("var intSubItemAmount = " + set.Tables["SubItemTable"].Rows.Count.ToString() + ";\n");
            builder.Append("var intPageAmount = " + set.Tables["PageTable"].Rows.Count.ToString() + ";\n");
            this.sClientJs = builder.ToString();
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
