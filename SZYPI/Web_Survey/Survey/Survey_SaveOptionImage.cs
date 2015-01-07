using CreateItem;
using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_SaveOptionImage : Page, IRequiresSessionState
    {
        public long IID;
        public long SID;
        public string sItemHTML = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            string str2;
            string[] strArray = null; //¸³³õÖµ
            long num = 0; //¸³³õÖµ
            string str3;
            string str4;
            short num2 = 0; //¸³³õÖµ
            int num3;
            goto Label_004F;
        Label_0002:
            switch (num3)
            {
                case 0:
                    num3 = 14;
                    goto Label_0002;

                case 1:
                    if (str2.Length > 2)
                    {
                        str2 = str2.Substring(0, str2.Length - 1);
                        strArray = str2.Split(new char[] { ';' });
                        this.IID = Convert.ToInt64(base.Request.Form["IID"]);
                        this.SID = Convert.ToInt64(base.Request.Form["SID"]);
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 10;
                    }
                    goto Label_0002;

                case 2:
                    if (!(strArray[num2] != ""))
                    {
                        goto Label_0244;
                    }
                    num3 = 11;
                    goto Label_0002;

                case 3:
                    {
                        CreateItem.CreateItem item = new CreateItem.CreateItem();
                        this.sItemHTML = item.createItemHTML(this.IID, this.SID, num, 0, true);
                        item.updateItemHtml(this.SID, num, this.IID, this.sItemHTML);
                        num3 = 7;
                        goto Label_0002;
                    }
                case 4:
                    if (this.SID == 0)
                    {
                        goto Label_02B6;
                    }
                    num3 = 0;
                    goto Label_0002;

                case 5:
                    goto Label_02B6;

                case 6:
                    if (num2 < strArray.Length)
                    {
                        num3 = 2;
                    }
                    else
                    {
                        num3 = 3;
                    }
                    goto Label_0002;

                case 7:
                    return;

                case 8:
                    if (str2 == null)
                    {
                        return;
                    }
                    num3 = 15;
                    goto Label_0002;

                case 9:
                    goto Label_0244;

                case 10:
                    return;

                case 11:
                    str3 = strArray[num2].Substring(strArray[num2].LastIndexOf(':') + 1, (strArray[num2].Length - strArray[num2].LastIndexOf(':')) - 1);
                    str4 = strArray[num2].Substring(0, strArray[num2].LastIndexOf(':'));
                    //command.CommandText = " UPDATE OptionTable SET [Image]='" + str3 + "' WHERE OID=" + str4 + " AND UID=" + num.ToString() + " AND SID=" + this.SID.ToString() + " AND IID=" + this.IID.ToString();
                    new Survey_SetOptionImage_Layer().UpdateOptionTable(str3, str4, num.ToString(), this.SID.ToString(), this.IID.ToString());
                    num3 = 9;
                    goto Label_0002;

                case 12:
                    goto Label_01F2;

                case 13:
                    goto Label_01A7;

                case 14:
                    if (this.IID != 0)
                    {
                        goto Label_01A7;
                    }
                    num3 = 5;
                    goto Label_0002;

                case 15:
                    num3 = 1;
                    goto Label_0002;

                case 0x10:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_01F2;
                    }
                    return;
            }
        Label_004F:
            str2 = Convert.ToString(base.Request.Form["OptionImageStr"]);
            num = 0;
            num = ConvertHelper.ConvertLong(this.Session["UserID"]);
            num3 = 8;
            goto Label_0002;
        Label_01A7:
            str3 = "";
            str4 = "";
            num2 = 0;
            num3 = 0x10;
            goto Label_0002;
        Label_01F2:
            num3 = 6;
            goto Label_0002;
        Label_0244:
            num2 = (short)(num2 + 1);
            num3 = 12;
            goto Label_0002;
        Label_02B6:
            base.Response.Write("SID=" + this.SID.ToString() + "<BR>IID=" + this.IID.ToString());
            base.Response.End();
            num3 = 13;
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
