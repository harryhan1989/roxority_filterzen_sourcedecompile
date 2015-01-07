using System;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Configuration;
using LoginClass;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_AddToLib : Page, IRequiresSessionState
    {
        /// <summary>
        /// 韩亮修改
        /// </summary>
        /// 修改了16进制数字为19进制，初始化了num2的值

        public string sClientJs = "var sMessage='';\n";

        protected void addToLib(long SID, long UID, long IID)
        {
            string str;
            string str2;
            string str3;
            int num;
            string str4;
            string str5;
            string str6;
            string str7;
            string str8;
            string str9;
            SqlDataReader reader;
            int num2;
            int num3; //自己添加的，原始没有初始化

            goto Label_0096;

        Label_0096:
            str = "";
            str2 = "";
            str3 = "";
            num = 0;
            str4 = "";
            str5 = "";
            str6 = "";
            str7 = "";
            str8 = "";
            str9 = "0";
            //objComm.CommandText = "SELECT * FROM ItemTable WHERE (UID=" + UID.ToString() + " AND IID=" + IID.ToString() + " AND ParentID=0)";
            reader = new Survey_AddToLib_Layer().GetItemTable(UID.ToString(), IID.ToString());
            num3 = 33;
            num2 = num;
            goto Label_0005;

        Label_0005:
            switch (num3)
            {
                case 0:
                    goto Label_092D;

                case 1:
                    reader.Close();
                    num3 = 0;
                    goto Label_0005;

                case 2:
                    reader.Close();
                    num3 = 10;
                    goto Label_0005;

                case 3:
                    if (reader.Read())
                    {
                        object obj5 = str7;
                        str7 = string.Concat(new object[] { obj5, "<SubItem ItemName=\"", reader["ItemName"], "\" ItemType=\"", num.ToString(), "\"></SubItem>" });
                        num3 = 20;
                    }
                    else
                    {
                        num3 = 16;
                    }
                    goto Label_0005;

                case 4:
                    if (reader.Read())
                    {
                        object obj6 = str7;
                        str7 = string.Concat(new object[] { obj6, "<SubItem ItemName=\"", reader["ItemName"], "\" ItemType=\"", num.ToString(), "\"></SubItem>" });
                        num3 = 32;
                    }
                    else
                    {
                        num3 = 19;
                    }
                    goto Label_0005;

                case 5:

                case 6:
                    reader.Close();
                    num3 = 24;
                    goto Label_0005;

                case 7:
                    goto Label_07EA;

                case 8:
                case 9:
                    num3 = 12;
                    goto Label_0005;

                case 10:
                case 11:

                case 12:
                    if ((reader.Read() ? 0 : 1) != 0)
                    {
                    }
                    num3 = 6;
                    goto Label_0005;

                case 13:
                case 14:
                case 15:
                    {
                        goto Label_03B2;
                        object obj7 = str8;
                        str8 = string.Concat(new object[] { obj7, "<Option OptionName=\"", reader["OptionName"], "\" Point=\"", reader["Point"], "\" IsMatrixRowColumn=\"True\"></Option>" });
                        num3 = 9;
                        goto Label_0005;
                    }
                case 16:
                    reader.Close();
                    //objComm.CommandText = "SELECT OptionName,Point FROM OptionTable WHERE ( UID=" + UID.ToString() + " AND IID=" + IID.ToString() + ")";
                    reader = new Survey_AddToLib_Layer().GetOptionTable(UID.ToString(), IID.ToString());
                    reader.Close();
                    num3 = 22;
                    goto Label_0005;

                case 17:
                    reader.Close();
                    //objComm.CommandText = "SELECT OptionName,Point FROM OptionTable WHERE ( UID=" + UID.ToString() + " AND IID=" + IID.ToString() + ")";
                    reader = new Survey_AddToLib_Layer().GetOptionTable(UID.ToString(), IID.ToString());
                    num3 = 13;
                    goto Label_0005;

                case 18:
                    if (reader.Read())
                    {
                        object obj2 = str8;
                        str8 = string.Concat(new object[] { obj2, "<Option OptionName=\"", reader["OptionName"], "\" Point=\"", reader["Point"], "\" IsMatrixRowColumn=\"False\"></Option>" });
                        num3 = 28;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0005;

                case 19:
                    reader.Close();
                    //objComm.CommandText = "SELECT OptionName,Point,IsMatrixRowColumn FROM OptionTable WHERE ( UID=" + UID.ToString() + " AND IID=" + IID.ToString() + ")";
                    reader = new Survey_AddToLib_Layer().GetOptionTable(UID.ToString(), IID.ToString());
                    num3 = 8;
                    goto Label_0005;

                case 20:
                    num3 = 3;
                    goto Label_0005;

                case 21:
                    if (reader.Read())
                    {
                        object obj4 = str8;
                        str8 = string.Concat(new object[] { obj4, "<Option OptionName=\"", reader["OptionName"], "\" Point=\"", reader["Point"], "\"></Option>" });
                        num3 = 25;
                    }
                    else
                    {
                        num3 = 2;
                    }
                    goto Label_0005;

                case 22:

                case 23:
                    switch (num2)
                    {
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                            //objComm.CommandText = "SELECT OptionName,Point FROM OptionTable WHERE (UID=" + UID.ToString() + " AND IID=" + IID.ToString() + ")";
                            reader = new Survey_AddToLib_Layer().GetOptionTable(UID.ToString(), IID.ToString());
                            num3 = 29;
                            goto Label_0005;
                        case 13:
                        case 14:
                        case 15:
                        case 16:

                        case 17:
                            goto Label_092D;

                        case 18:
                            //objComm.CommandText = "SELECT ItemName FROM ItemTable WHERE (UID=" + UID.ToString() + " AND ParentID=" + IID.ToString() + ")";
                            reader = new Survey_AddToLib_Layer().GetItemName(UID.ToString(), IID.ToString());
                            num3 = 11;
                            goto Label_0005;

                        case 19:
                        case 20:
                            //objComm.CommandText = "SELECT ItemName FROM ItemTable WHERE (UID=" + UID.ToString() + " AND ParentID=" + IID.ToString() + ")";
                            reader = new Survey_AddToLib_Layer().GetItemName(UID.ToString(), IID.ToString());
                            num3 = 27;
                            goto Label_0005;
                        case 21:
                            //objComm.CommandText = "SELECT ItemName FROM ItemTable WHERE (UID=" + UID.ToString() + " AND ParentID=" + IID.ToString() + ")";
                            reader = new Survey_AddToLib_Layer().GetItemName(UID.ToString(), IID.ToString());
                            num3 = 5;
                            goto Label_0005;
                    }
                    num3 = 30;
                    goto Label_0005;



                case 24:
                    goto Label_092D;

                case 25:
                    num3 = 21;
                    goto Label_0005;

                case 26:
                    num = Convert.ToInt32(reader["ItemType"]);
                    str = reader["ItemName"].ToString();
                    str4 = reader["ItemContent"].ToString();
                    str2 = reader["DataFormatCheck"].ToString();
                    str3 = reader["ItemHTML"].ToString();
                    str6 = reader["OtherProperty"].ToString();
                    str5 = reader["OptionAmount"].ToString();
                    str9 = reader["OptionImgModel"].ToString();
                    num3 = 7;
                    goto Label_0005;

                case 27:
                    goto Label_03B2;

                case 28:
                case 29:
                    num3 = 18;
                    goto Label_0005;

                case 30:
                    num3 = 14;
                    goto Label_0005;

                case 31:
                    if (reader.Read())
                    {
                        object obj3 = str7;
                        str7 = string.Concat(new object[] { obj3, "<SubItem ItemName=\"", reader["ItemName"], "\" ItemType=\"", reader["ItemType"].ToString(), "\"></SubItem>" });
                        num3 = 15;
                    }
                    else
                    {
                        num3 = 17;
                    }
                    goto Label_0005;

                case 32:
                    num3 = 4;
                    goto Label_0005;

                case 33:
                    if (!reader.Read())
                    {
                        goto Label_07EA;
                    }
                    num3 = 26;
                    goto Label_0005;
            }


        Label_03B2:
            num3 = 31;
            goto Label_0005;
        Label_07EA:
            reader.Close();
            num2 = num;
            num3 = 23;
            goto Label_0005;
        Label_092D:
            reader.Dispose();
            str3 = str3.Replace("'", "''");
            string str10 = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Item ItemName=\"" + str + "\" ItemType=\"" + num.ToString() + "\" PageNo=\"1\" ParentID=\"0\" DataFormatCheck=\"" + str2 + "\" OptionAmount=\"" + str5 + "\" OptionImgModel=\"" + str9 + "\" OtherProperty=\"" + str6 + "\"><ItemContent>" + str4.Replace("'", "''") + "</ItemContent><ItemHTML><![CDATA[" + str3 + "]]></ItemHTML>" + str7 + str8 + "</Item>";
            str10 = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Item ItemName=\"" + str + "\" ItemType=\"" + num.ToString() + "\" PageNo=\"1\" ParentID=\"0\" DataFormatCheck=\"" + str2 + "\" OptionAmount=\"" + str5 + "\" OptionImgModel=\"" + str9 + "\" OtherProperty=\"" + str6 + "\"><ItemContent><![CDATA[" + str4.Replace("'", "''") + "]]></ItemContent><ItemHTML><![CDATA[" + str3 + "]]></ItemHTML>" + str7 + str8 + "</Item>";
            //objComm.CommandText = "INSERT INTO ItemLib(SID,UID,IID,ItemName,ItemType,ItemHTML,ItemFrame) VALUES(" + SID.ToString() + "," + UID.ToString() + "," + IID.ToString() + ",'" + str + "'," + num.ToString() + ",'" + str3 + "','" + str10 + "')";
            new Survey_AddToLib_Layer().InsertItemLib(SID.ToString(), UID.ToString(), IID.ToString(), str, num.ToString(), str3, str10);
        }

        protected void Page_Load(object sender, EventArgs e)//前： This item is obfuscated and can not be translated.
        {
            long uID = 0;
            long sID = 0;
            long iID = Convert.ToInt64(base.Request.QueryString["IID"]);
            uID=ConvertHelper.ConvertLong(this.Session["UserID"]);
            this.addToLib(sID, uID, iID);

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