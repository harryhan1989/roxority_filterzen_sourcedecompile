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
    public class Survey_ItemBatchSortSave : Page, IRequiresSessionState
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            long uID = 0;
            long sID = 0;
            //loginClass class2 = new loginClass();
            //class2.checkLogin(out uID, "0");
            uID = ConvertHelper.ConvertLong(this.Session["UserID"]);
            //class2.checkLimits(Convert.ToString(this.Session["Limits3"]), "itembatchsort.aspx", 2, "没有权限", "");
            sID = Convert.ToInt32(base.Request.Form["SID"]);
            string sItemSortStr = base.Request.Form["Result"];            
            this.saveItemBatchSort(sID, uID, sItemSortStr);        }

        protected void saveItemBatchSort(long SID, long UID, string sItemSortStr)
        {
        Label_001B:
            string[] strArray = sItemSortStr.Split(new char[] { ',' });
            int index = 0;
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 3:
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    if (index < strArray.Length)
                    {
                        //command.CommandText = "UPDATE ItemTable SET Sort=" + strArray[index].Split(new char[] { ':' })[1] + " WHERE IID=" + strArray[index].Split(new char[] { ':' })[0];
                        new Survey_ItemBatchSort_Layer().UpdateItemTable(strArray[index].Split(new char[] { ':' })[1], strArray[index].Split(new char[] { ':' })[0]);
                        index++;
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 2:
                    return;
            }
            goto Label_001B;
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
