using LoginClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SetOptionImage : Page, IRequiresSessionState
    {
        public long IID;
        public string sClientJs = "";
        public long SID;
        public string sImgHidden = "";
        public string sOptionList = "";
        public long UID;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            DataSet set = null; //赋初值
            int num = 0; //赋初值
            int num2;
            goto Label_0037;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (this.IID != 0)
                    {
                        goto Label_04A7;
                    }
                    num2 = 10;
                    goto Label_0002;

                case 1:
                    num2 = 0;
                    goto Label_0002;

                case 2:
                case 7:
                    num2 = 6;
                    goto Label_0002;

                case 3:
                    goto Label_04A7;

                case 4:
                    base.Response.End();
                    return;

                case 5:
                    if (this.SID == 0)
                    {
                        goto Label_012E;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 6:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        string sClientJs = this.sClientJs;
                        this.sClientJs = sClientJs + "arrOption[" + num.ToString() + "] = [" + set.Tables[0].Rows[num][0].ToString() + ", '" + set.Tables[0].Rows[num][1].ToString() + "','" + set.Tables[0].Rows[num][2].ToString() + "'];\n";
                        string sOptionList = this.sOptionList;
                        this.sOptionList = sOptionList + " <tr><td>" + set.Tables[0].Rows[num][1].ToString() + "</td><td id='image" + set.Tables[0].Rows[num][0].ToString() + "'>" + set.Tables[0].Rows[num][2].ToString() + "</td><td title='点击应用图片'  style='cursor:pointer' onclick=applyImage(" + set.Tables[0].Rows[num][0].ToString() + ")><img src='images/image.gif' alt='点击应用图片' /></td> ";
                        string sImgHidden = this.sImgHidden;
                        this.sImgHidden = sImgHidden + "<input type='hidden'  id='image" + set.Tables[0].Rows[num][0].ToString() + "_' value='" + set.Tables[0].Rows[num][2].ToString() + "' />";
                        this.sOptionList = this.sOptionList + "<td onclick='clearImage(" + set.Tables[0].Rows[num][0].ToString() + ")' title='清除图片' style='cursor:pointer'><img src='images/clearimage.gif' alt='清除图片'></td>";
                        this.sOptionList = this.sOptionList + "<td onclick='viewImage(" + set.Tables[0].Rows[num][0].ToString() + ")' title='预览图片' style='cursor:pointer'><img src='images/viewimage.gif' alt='预览图片'></td></tr>";
                        num++;
                        num2 = 7;
                    }
                    else
                    {
                        num2 = 9;
                    }
                    goto Label_0002;

                case 8:
                    if (set.Tables["OptionTable"].Rows.Count != 0)
                    {
                        num = 0;

                        num2 = 2;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    goto Label_0002;

                case 9:
                    set.Clear();
                    return;

                case 10:
                    goto Label_012E;
            }
        Label_0037:
            this.IID = Convert.ToInt64(base.Request.QueryString["IID"]);
            this.SID = Convert.ToInt64(base.Request.QueryString["SID"]);
            UID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num2 = 5;
            goto Label_0002;
        Label_012E:
            base.Response.End();
            num2 = 3;
            goto Label_0002;
        Label_04A7:
            set = new DataSet();
            //new OleDbDataAdapter("SELECT OID,OptionName,Image FROM OptionTable AS O INNER JOIN ItemTable AS I ON O.IID=I.IID WHERE O.SID=" + this.SID.ToString() + " AND O.UID=" + this.UID.ToString() + " AND O.IID=" + this.IID.ToString() + " AND (ItemType=4 OR ItemType=5 OR ItemType=8 OR ItemType=9)", connection).Fill(set, "OptionTable");
            DataTable OptionTable = new Survey_SetOptionImage_Layer().GetOptionTable(this.SID.ToString(), this.UID.ToString(), this.IID.ToString());
            OptionTable.TableName = "OptionTable";
            set.Tables.Add(OptionTable);

            num2 = 8;
            goto Label_0002;
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
