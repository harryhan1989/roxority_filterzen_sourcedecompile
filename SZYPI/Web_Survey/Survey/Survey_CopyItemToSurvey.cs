using CreateItem;
using LoginClass;
using System;
using System.Collections;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_CopyItemToSurvey : Page, IRequiresSessionState
    {
        protected string getCheckValue(string[] arrInput, string sCheckItem)
        {
            string str;
            int num;
            int num2;
            goto Label_0023;
        Label_0002:
            switch (num2)
            {
                case 0:
                    return str;

                case 1:
                        goto Label_0070;

                case 2:
                    if (arrInput[num].IndexOf(sCheckItem) != 0)
                    {
                        goto Label_003C;
                    }
                    num2 = 5;
                    goto Label_0002;

                case 3:
                    if (num < arrInput.Length)
                    {
                        num2 = 2;
                    }
                    else
                    {
                        num2 = 0;
                    }
                    goto Label_0002;

                case 4:
                    goto Label_0070;

                case 5:
                    try
                    {
                        str = arrInput[num].Replace(sCheckItem, "");
                    }
                    catch
                    {
                    }
                    goto Label_003C;
            }
        Label_0023:
            str = "";
            num = 0;
            num2 = 1;
            goto Label_0002;
        Label_003C:
            num++;
            num2 = 4;
            goto Label_0002;
        Label_0070:
            num2 = 3;
            goto Label_0002;
        }

        protected string getItemContent(XmlNode xn)
        {
            string innerText = "";
            IEnumerator enumerator = xn.GetEnumerator();
            try
            {
                XmlNode current = null; //赋初值
                int num = 5;
            Label_001C:
                switch (num)
                {
                    case 0:
                        if (!(current.Name == "ItemContent"))
                        {
                            goto Label_0079;
                        }
                        num = 3;
                        goto Label_001C;

                    case 1:
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        if (enumerator.MoveNext())
                        {
                            break;
                        }
                        num = 6;
                        goto Label_001C;

                    case 2:
                        return innerText;

                    case 3:
                        {
                            XmlElement element = (XmlElement)current;
                            innerText = element.InnerText;
                            num = 4;
                            goto Label_001C;
                        }
                    case 6:
                        num = 2;
                        goto Label_001C;

                    default:
                        goto Label_0079;
                }
                current = (XmlNode)enumerator.Current;
                num = 0;
                goto Label_001C;
            Label_0079:
                num = 1;
                goto Label_001C;
            }
            finally
            {
                IDisposable disposable;
                int num2;
                goto Label_00E7;
            Label_00D2:
                switch (num2)
                {
                    case 0:
                        goto Label_011F;

                    case 1:
                        disposable.Dispose();
                        num2 = 0;
                        goto Label_00D2;

                    case 2:
                        if (disposable == null)
                        {
                            goto Label_011F;
                        }
                        num2 = 1;
                        goto Label_00D2;
                }
            Label_00E7:
                disposable = enumerator as IDisposable;
                num2 = 2;
                goto Label_00D2;
            Label_011F: ;
            }
            return innerText;
        }

        protected string getItemXML(long LIID, long UID)
        {
        Label_0017:
            string str = "";
            //objComm.CommandText = "SELECT TOP 1 ItemFrame FROM ItemLib WHERE (LIID=" + LIID.ToString() + " AND UID=" + UID.ToString() + ") OR (LIID=" + LIID.ToString() + " AND Active>=1)";
            SqlDataReader reader = new Survey_ItemLib_Layer().GetItemLib1(UID.ToString(), LIID.ToString());
            int num = 2;
        Label_0002:
            switch (num)
            {
                case 0:
                    str = reader["ItemFrame"].ToString();
                    num = 1;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num = 0;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            reader.Close();
            reader.Dispose();
            return str;
        }

        protected string getOptionStr(XmlNode xn)
        {
            string str;
            IEnumerator enumerator;
            int num;
            goto Label_001B;
        Label_0002:
            switch (num)
            {
                case 0:
                    goto Label_017B;

                case 1:
                    return str;

                case 2:
                    if (!(str != ""))
                    {
                        return str;
                    }
                    num = 0;
                    goto Label_0002;

                case 3:
                    try
                    {
                        XmlNode current = null; //赋初值
                        num = 4;
                    Label_006D:
                        switch (num)
                        {
                            case 0:
                                {
                                    XmlElement element = (XmlElement)current;
                                    str = str + element.GetAttribute("OptionName") + "\r\n";
                                    num = 3;
                                    goto Label_006D;
                                }
                            case 1:
                                if (!(current.Name == "Option"))
                                {
                                    goto Label_00CA;
                                }
                                num = 0;
                                goto Label_006D;

                            case 2:
                                if (enumerator.MoveNext())
                                {
                                    break;
                                }
                                num = 5;
                                goto Label_006D;

                            case 5:
                                num = 6;
                                goto Label_006D;

                            case 6:
                                num = 2;
                                goto Label_0002;

                            default:
                                goto Label_00CA;
                        }
                        current = (XmlNode)enumerator.Current;
                        num = 1;
                        goto Label_006D;
                    Label_00CA:
                        num = 2;
                        goto Label_006D;
                    }
                    finally
                    {
                        IDisposable disposable;
                        goto Label_0142;
                    Label_012D:
                        switch (num)
                        {
                            case 0:
                                if (disposable == null)
                                {
                                    goto Label_017A;
                                }
                                num = 1;
                                goto Label_012D;

                            case 1:
                                disposable.Dispose();
                                num = 2;
                                goto Label_012D;

                            case 2:
                                goto Label_017A;
                        }
                    Label_0142:
                        disposable = enumerator as IDisposable;
                        num = 0;
                        goto Label_012D;
                    Label_017A: ;
                    }
                    goto Label_017B;
            }
        Label_001B:
            str = "";
            enumerator = xn.GetEnumerator();
            num = 3;
            goto Label_0002;
        Label_017B:
            try
            {
                str = str.Substring(0, str.Length - "\r\n".Length);
            }
            catch (Exception ex)
            { 
            }

            num = 1;
            goto Label_0002;
        }

        protected string getOptionStr(XmlNode xn, bool blnGetMatrixRowColumn)
        {
            string str;
            int num;
            IEnumerator enumerator;
            int num2;
            goto Label_001B;
        Label_0002:
            switch (num2)
            {
                case 0:
                    goto Label_026E;

                case 1:
                    return str;

                case 2:
                    try
                    {
                        XmlNode current = null; //赋初值
                        XmlElement element = null; //赋初值
                        num2 = 10;
                    Label_0070:
                        switch (num2)
                        {
                            case 0:
                                if (!blnGetMatrixRowColumn)
                                {
                                    goto Label_0194;
                                }
                                num2 = 5;
                                goto Label_0070;

                            case 1:
                                str = str + element.GetAttribute("OptionName") + "\r\n";
                                num2 = 6;
                                goto Label_0070;

                            case 2:
                                if (enumerator.MoveNext())
                                {
                                    break;
                                }
                                num2 = 13;
                                goto Label_0070;

                            case 3:
                                str = str + element.GetAttribute("OptionName") + "\r\n";
                                num2 = 4;
                                goto Label_0070;

                            case 5:
                                num2 = 9;
                                goto Label_0070;

                            case 7:
                                element = (XmlElement)current;
                                num2 = 0;
                                goto Label_0070;

                            case 8:
                                if (!(element.GetAttribute("IsMatrixRowColumn") == "False"))
                                {
                                    goto Label_0118;
                                }
                                num2 = 3;
                                goto Label_0070;

                            case 9:
                                if (!(element.GetAttribute("IsMatrixRowColumn") == "True"))
                                {
                                    goto Label_0118;
                                }
                                num2 = 1;
                                goto Label_0070;

                            case 11:
                                if (!(current.Name == "Option"))
                                {
                                    goto Label_0118;
                                }
                                num2 = 7;
                                goto Label_0070;

                            case 12:
                                num2 = 3;
                                goto Label_0002;

                            case 13:
                                num2 = 12;
                                goto Label_0070;

                            default:
                                goto Label_0118;
                        }
                        current = (XmlNode)enumerator.Current;
                        num++;
                        num2 = 11;
                        goto Label_0070;
                    Label_0118:
                        num2 = 2;
                        goto Label_0070;
                    Label_0194:
                        num2 = 8;
                        goto Label_0070;
                    }
                    finally
                    {
                        IDisposable disposable;
                        goto Label_0234;
                    Label_021F:
                        switch (num2)
                        {
                            case 0:
                                disposable.Dispose();
                                num2 = 2;
                                goto Label_021F;

                            case 1:
                                if (disposable == null)
                                {
                                    goto Label_026D;
                                }
                                num2 = 0;
                                goto Label_021F;

                            case 2:
                                goto Label_026D;
                        }
                    Label_0234:
                        disposable = enumerator as IDisposable;
                        num2 = 1;
                        goto Label_021F;
                    Label_026D: ;
                    }
                    goto Label_026E;

                case 3:
                    if (!(str != ""))
                    {
                        return str;
                    }
                    num2 = 0;
                    goto Label_0002;
            }
        Label_001B:
            str = "";
            num = 0;
            enumerator = xn.GetEnumerator();
            num2 = 2;
            goto Label_0002;
        Label_026E:
            str = str.Substring(0, str.Length - "\r\n".Length);
            if ((1 != 0) && (0 != 0))
            {
            }
            num2 = 1;
            goto Label_0002;
        }

        protected string getSubItemStr(XmlNode xn)
        {
            string str;
            IEnumerator enumerator;
            int num;
            goto Label_001B;
        Label_0002:
            switch (num)
            {
                case 0:
                    return str;

                case 1:
                    goto Label_0181;

                case 2:
                    try
                    {
                        XmlNode current = null; //赋初值
                        num = 2;
                    Label_006D:
                        switch (num)
                        {
                            case 0:
                                num = 3;
                                goto Label_0002;

                            case 1:
                                if (enumerator.MoveNext())
                                {
                                    break;
                                }
                                num = 5;
                                goto Label_006D;

                            case 4:
                                if (!(current.Name == "SubItem"))
                                {
                                    goto Label_00CA;
                                }
                                num = 6;
                                goto Label_006D;

                            case 5:
                                num = 0;
                                goto Label_006D;

                            case 6:
                                {
                                    XmlElement element = (XmlElement)current;
                                    str = str + element.GetAttribute("ItemName") + "\r\n";
                                    num = 3;
                                    goto Label_006D;
                                }
                            default:
                                goto Label_00CA;
                        }
                        current = (XmlNode)enumerator.Current;
                        num = 4;
                        goto Label_006D;
                    Label_00CA:
                        num = 1;
                        goto Label_006D;
                    }
                    finally
                    {
                        IDisposable disposable;
                        goto Label_0142;
                    Label_012D:
                        switch (num)
                        {
                            case 0:
                                if (disposable == null)
                                {
                                    goto Label_0180;
                                }
                                num = 1;
                                goto Label_012D;

                            case 1:
                                disposable.Dispose();
                                num = 2;
                                goto Label_012D;

                            case 2:
                                goto Label_0180;
                        }
                    Label_0142:
                        disposable = enumerator as IDisposable;
                        num = 0;
                        goto Label_012D;
                    Label_0180: ;
                    }
                    goto Label_0181;

                case 3:
                    if (!(str != ""))
                    {
                        return str;
                    }
                    num = 1;
                    goto Label_0002;
            }
        Label_001B:
            str = "";
            enumerator = xn.GetEnumerator();
            num = 2;
            goto Label_0002;
        Label_0181:
            try
            {
                str = str.Substring(0, str.Length - "\r\n".Length);
            }
            catch(Exception ex)
            { 
            }
            num = 0;
            goto Label_0002;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
            long num3;
            string[] strArray;
            int num4;
            XmlNode node = null; //赋初值
            XmlElement element = null; //赋初值
            string attribute = null; //赋初值
            CreateItem.CreateItem item;
            int num5;
            goto Label_00A2;
        Label_0005:
            switch (num5)
            {
                case 0:
                    if (!(strArray[7] == ""))
                    {
                        goto Label_0838;
                    }
                    num5 = 0x12;
                    goto Label_0005;

                case 1:
                    if (!(strArray[9] == ""))
                    {
                        goto Label_0346;
                    }
                    num5 = 0x15;
                    goto Label_0005;

                case 2:
                    goto Label_0346;

                case 3:
                    if (!(strArray[5] == ""))
                    {
                        goto Label_079A;
                    }
                    num5 = 30;
                    goto Label_0005;

                case 4:
                    strArray[0x1f] = this.getOptionStr(node, true);
                    num5 = 15;
                    goto Label_0005;

                case 5:
                    goto Label_00F8;

                case 6:
                    if (!(strArray[8] == ""))
                    {
                        goto Label_019E;
                    }
                    num5 = 9;
                    goto Label_0005;

                case 7:
                    if (!(strArray[2] == ""))
                    {
                        goto Label_05CC;
                    }
                    num5 = 0x22;
                    goto Label_0005;

                case 8:
                    if (!(strArray[6] == ""))
                    {
                        goto Label_00F8;
                    }
                    num5 = 20;
                    goto Label_0005;

                case 9:
                    strArray[8] = "0";
                    num5 = 11;
                    goto Label_0005;

                case 10:
                    goto Label_0646;

                case 11:
                    goto Label_019E;

                case 12:
                    if (!(strArray[0] == "13"))
                    {
                        goto Label_0890;
                    }
                    num5 = 0x20;
                    goto Label_0005;

                case 13:
                    if (!(strArray[4] == ""))
                    {
                        goto Label_051B;
                    }
                    num5 = 0x19;
                    goto Label_0005;

                case 14:
                    goto Label_079A;

                case 15:
                    goto Label_016B;

                case 0x10:
                case 0x21:
                    num5 = 0x17;
                    goto Label_0005;

                case 0x11:
                    {
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(this.getItemXML(num3, num));
                        node = document.SelectSingleNode("Item");
                        element = (XmlElement)node;
                        strArray[0] = element.GetAttribute("ItemType");
                        strArray[1] = "1";
                        strArray[10] = element.GetAttribute("ItemName");
                        strArray[11] = this.getItemContent(node);
                        strArray[20] = this.getSubItemStr(node);
                        strArray[0x15] = this.getOptionStr(node);
                        num5 = 0x23;
                        goto Label_0005;
                    }
                case 0x12:
                    strArray[7] = "10";
                    num5 = 0x1d;
                    goto Label_0005;

                case 0x13:
                    strArray[3] = "50";
                    num5 = 0x1c;
                    goto Label_0005;

                case 20:
                    strArray[6] = "1";
                    num5 = 5;
                    goto Label_0005;

                case 0x15:
                    strArray[9] = "99999999";
                    num5 = 2;
                    goto Label_0005;

                case 0x16:
                    goto Label_051B;

                case 0x17:
                    if (num4 < strArray.Length)
                    {
                        strArray[num4] = "";
                        num4++;
                        num5 = 0x10;
                    }
                    else
                    {
                        num5 = 0x11;
                    }
                    goto Label_0005;

                case 0x18:
                    goto Label_0890;

                case 0x19:
                    strArray[4] = "0";
                    num5 = 0x16;
                    goto Label_0005;

                case 0x1a:
                    strArray[0x19] = element.GetAttribute("OptionAmount");
                    num5 = 10;
                    goto Label_0005;

                case 0x1b:
                    goto Label_05CC;

                case 0x1c:
                    goto Label_072A;

                case 0x1d:
                    goto Label_0838;

                case 30:
                    strArray[5] = "100";
                    num5 = 14;
                    goto Label_0005;

                case 0x1f:
                    if (!(strArray[3] == ""))
                    {
                        goto Label_072A;
                    }
                    num5 = 0x13;
                    goto Label_0005;

                case 0x20:
                    strArray[0x19] = strArray[7];
                    num5 = 0x18;
                    goto Label_0005;

                case 0x22:
                    strArray[2] = "0";
                    num5 = 0x1b;
                    goto Label_0005;

                case 0x23:
                    if (!(strArray[0] == "18"))
                    {
                        goto Label_016B;
                    }
                    num5 = 4;
                    goto Label_0005;

                case 0x24:
                    if (!(strArray[0] == "11"))
                    {
                        goto Label_0646;
                    }
                    num5 = 0x1a;
                    goto Label_0005;
            }
        Label_00A2:
            num = ConvertHelper.ConvertLong(this.Session["UserID"]); ;
            long sID = Convert.ToInt64(base.Request.QueryString["SID"]);
            num3 = Convert.ToInt64(base.Request.QueryString["LIID"]);
            strArray = new string[0x20];
            num4 = 0;
            num5 = 0x21;
            goto Label_0005;
        Label_00F8: ;
            strArray[7] = this.getCheckValue(attribute.Split(new char[] { '|' }), "MaxTickOff");
            num5 = 0;
            goto Label_0005;
        Label_016B:
            num5 = 0x24;
            goto Label_0005;
        Label_019E: ;
            strArray[9] = this.getCheckValue(attribute.Split(new char[] { '|' }), "MinValue");
            num5 = 1;
            goto Label_0005;
        Label_0346: ;
            strArray[12] = this.getCheckValue(attribute.Split(new char[] { '|' }), "Mob");
            strArray[13] = this.getCheckValue(attribute.Split(new char[] { '|' }), "PostCode");
            strArray[14] = this.getCheckValue(attribute.Split(new char[] { '|' }), "Data");
            strArray[15] = this.getCheckValue(attribute.Split(new char[] { '|' }), "IDCard");
            strArray[0x10] = this.getCheckValue(attribute.Split(new char[] { '|' }), "Cn");
            strArray[0x11] = this.getCheckValue(attribute.Split(new char[] { '|' }), "En");
            strArray[0x12] = this.getCheckValue(attribute.Split(new char[] { '|' }), "URL");
            strArray[0x13] = this.getCheckValue(attribute.Split(new char[] { '|' }), "Email");
            strArray[0x16] = this.getCheckValue(attribute.Split(new char[] { '|' }), "Empty");
            strArray[0x1a] = this.getCheckValue(attribute.Split(new char[] { '|' }), "MaxLevelName");
            strArray[0x1b] = this.getCheckValue(attribute.Split(new char[] { '|' }), "MinLevelName");
            num5 = 12;
            goto Label_0005;
        Label_051B: ;
            strArray[5] = this.getCheckValue(attribute.Split(new char[] { '|' }), "MaxSelect");
            num5 = 3;
            goto Label_0005;
        Label_05CC: ;
            strArray[3] = this.getCheckValue(attribute.Split(new char[] { '|' }), "MaxLen");
            num5 = 0x1f;
            goto Label_0005;
        Label_0646:
            strArray[0x17] = element.GetAttribute("OptionImgModel");
            attribute = element.GetAttribute("DataFormatCheck");
            strArray[2] = this.getCheckValue(attribute.Split(new char[] { '|' }), "MinLen");
            num5 = 7;
            goto Label_0005;
        Label_072A: ;
            strArray[4] = this.getCheckValue(attribute.Split(new char[] { '|' }), "MinSelect");
            num5 = 13;
            goto Label_0005;
        Label_079A: ;
            strArray[6] = this.getCheckValue(attribute.Split(new char[] { '|' }), "MinTickOff");
            num5 = 8;
            goto Label_0005;
        Label_0838: ;
            strArray[8] = this.getCheckValue(attribute.Split(new char[] { '|' }), "MaxValue");
            num5 = 6;
            goto Label_0005;
        Label_0890:
            item = new CreateItem.CreateItem();
            item.getLanguage();
            item.createItem(strArray, sID, num, attribute);
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
