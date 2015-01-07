using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using LoginClass;
using Business.Helper;

namespace Web_Survey.Survey
{
    public class Survey_WriteSurveyFile : Page, IRequiresSessionState
    {
        public string getCreateTableSQL(DataTable dtItemTable, DataTable dtOption)
        {
            StringBuilder builder;
            int num;
            int num2;
            int num3;
            int num4;
            int num5 = 0; //赋初值
            int num7;
            goto Label_00BE;
        Label_0005:
            switch (num7)
            {
                case 0:
                case 7:
                case 9:
                case 10:
                case 13:
                case 14:
                case 15:
                case 0x13:
                case 0x17:
                case 0x19:
                case 0x1c:
                case 0x1d:
                case 0x20:
                case 0x24:
                case 0x25:
                case 0x29:
                case 0x2a:
                case 0x2b:
                    goto Label_08B2;

                case 1:
                    num2 = Convert.ToInt32(dtItemTable.Rows[num]["ItemType"]);
                    num3 = Convert.ToInt32(dtItemTable.Rows[num]["IID"]);
                    num5 = num2;
                    num7 = 20;
                    goto Label_0005;

                case 2:
                    if (num4 <= 500)
                    {
                        goto Label_0102;
                    }
                    num7 = 0x21;
                    goto Label_0005;

                case 3:
                    goto Label_0102;

                case 4:
                    num4 = 500;
                    num7 = 0x18;
                    goto Label_0005;

                case 5:
                    goto Label_083F;

                case 6:
                    goto Label_08C9;

                case 8:
                    goto Label_0598;

                case 11:
                case 30:
                    num7 = 0x23;
                    goto Label_0005;

                case 12:
                    if (num4 <= 500)
                    {
                        goto Label_083F;
                    }
                    num7 = 0x16;
                    goto Label_0005;

                case 0x10:
                    num4 = this.getParentOptionAmount(dtItemTable, Convert.ToInt32(num3)) * 11;
                    num7 = 6;
                    goto Label_0005;

                case 0x11:
                    return builder.ToString();

                case 0x12:
                    num4 = 500;
                    num7 = 8;
                    goto Label_0005;

                case 20:
                    switch (num5)
                    {
                        case 1:
                            builder.Append(string.Concat(new object[] { " F", num3, " VARCHAR(", this.getInputObjLen(dtItemTable.Rows[num]["DataFormatCheck"].ToString()).ToString(), ")," }));
                            num7 = 0x17;
                            goto Label_0005;

                        case 2:
                            builder.Append(" F" + num3 + "  INT ,");
                            num7 = 0x24;
                            goto Label_0005;

                        case 3:
                            builder.Append(" F" + num3 + " MEMO,");
                            num7 = 0;
                            goto Label_0005;

                        case 4:
                        case 6:
                        case 11:
                            builder.Append(" F" + num3 + "  INT ,");
                            num7 = 10;
                            goto Label_0005;

                        case 5:
                            builder.Append(string.Concat(new object[] { " F", num3, "  INT ,F", num3, "_Input  VARCHAR(", this.getExpandInputLen(dtItemTable.Rows[num]["DataFormatCheck"].ToString()), ")," }));
                            num7 = 0x1d;
                            goto Label_0005;

                        case 7:
                            builder.Append(this.getItem7SQL(num3, dtItemTable));
                            num7 = 0x19;
                            goto Label_0005;

                        case 8:
                        case 10:
                            num4 = this.getOptionAmount(num3, dtItemTable) * 11;
                            num7 = 0x1a;
                            goto Label_0005;

                        case 9:
                            num4 = this.getOptionAmount(num3, dtItemTable) * 11;
                            num7 = 0x1f;
                            goto Label_0005;

                        case 12:
                            num7 = 0x26;
                            goto Label_0005;

                        case 13:
                            num4 = this.getOptionAmount(num3, dtItemTable) * 50;
                            num7 = 12;
                            goto Label_0005;

                        case 14:
                            goto Label_08B2;

                        case 15:
                            builder.Append(this.getItem15SQL(num3, dtItemTable));
                            num7 = 0x20;
                            goto Label_0005;

                        case 0x10:
                            builder.Append(this.getItem16SQL(num3, dtItemTable, dtOption, this.getExpandInputLen(dtItemTable.Rows[num]["DataFormatCheck"].ToString())));
                            num7 = 0x1c;
                            goto Label_0005;

                        case 0x11:
                            builder.Append(string.Concat(new object[] { " F", num3, "  IMAGE NULL,F", num3, "_FileName VARCHAR(50)," }));
                            num7 = 7;
                            goto Label_0005;

                        case 0x12:
                            builder.Append(this.getItem18SQL(num3, dtItemTable, dtOption));
                            num7 = 13;
                            goto Label_0005;

                        case 0x13:
                            builder.Append(this.getItem19SQL(num3, dtItemTable, this.getExpandInputLen(dtItemTable.Rows[num]["DataFormatCheck"].ToString())));
                            num7 = 0x2a;
                            goto Label_0005;

                        case 20:
                            builder.Append(this.getItem20SQL(num3, dtItemTable, this.getExpandInputLen(dtItemTable.Rows[num]["DataFormatCheck"].ToString())));
                            num7 = 14;
                            goto Label_0005;

                        case 0x15:
                            builder.Append(this.getItem21SQL(num3, dtItemTable));
                            num7 = 15;
                            goto Label_0005;
                    }
                    num7 = 0x27;
                    goto Label_0005;

                case 0x15:
                    if (!(dtItemTable.Rows[num]["ParentID"].ToString() == "0"))
                    {
                        goto Label_08B2;
                    }
                    num7 = 1;
                    goto Label_0005;

                case 0x16:
                    num4 = 500;
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num7 = 5;
                    goto Label_0005;

                case 0x18:
                    goto Label_0361;

                case 0x1a:
                    if (num4 != 0)
                    {
                        goto Label_05FE;
                    }
                    num7 = 0x1b;
                    goto Label_0005;

                case 0x1b:
                    num4 = this.getParentOptionAmount(dtItemTable, Convert.ToInt32(num3)) * 11;
                    num7 = 0x22;
                    goto Label_0005;

                case 0x1f:
                    if (num4 != 0)
                    {
                        goto Label_08C9;
                    }
                    num7 = 0x10;
                    goto Label_0005;

                case 0x21:
                    num4 = 500;
                    num7 = 3;
                    goto Label_0005;

                case 0x22:
                    goto Label_05FE;

                case 0x23:
                    if (num < dtItemTable.Rows.Count)
                    {
                        num7 = 0x15;
                    }
                    else
                    {
                        num7 = 0x11;
                    }
                    goto Label_0005;

                case 0x26:
                    if (num4 <= 500)
                    {
                        goto Label_0598;
                    }
                    num7 = 0x12;
                    goto Label_0005;

                case 0x27:
                    num7 = 0x29;
                    goto Label_0005;

                case 40:
                    if (num4 <= 500)
                    {
                        goto Label_0361;
                    }
                    num7 = 4;
                    goto Label_0005;
            }
        Label_00BE:
            builder = new StringBuilder();
            num = 0;
            num2 = 0;
            num3 = 0;
            num4 = 0;
            num = 0;
            num7 = 11;
            goto Label_0005;
        Label_0102: ;
            builder.Append(string.Concat(new object[] { " F", num3, "  VARCHAR(", num4.ToString(), ")," }));
            builder.Append(string.Concat(new object[] { " F", num3, "_Input   VARCHAR(", this.getExpandInputLen(dtItemTable.Rows[num]["DataFormatCheck"].ToString()), ")," }));
            num7 = 9;
            goto Label_0005;
        Label_0361: ;
            builder.Append(string.Concat(new object[] { " F", num3, "  VARCHAR(", num4.ToString(), ")," }));
            num7 = 0x13;
            goto Label_0005;
        Label_0598:
            num4 = this.getOptionAmount(num3, dtItemTable) * 13;
            builder.Append(string.Concat(new object[] { " F", num3, "  VARCHAR(", num4.ToString(), ")," }));
            num7 = 0x25;
            goto Label_0005;
        Label_05FE:
            num7 = 40;
            goto Label_0005;
        Label_083F: ;
            builder.Append(string.Concat(new object[] { " F", num3, "  VARCHAR(", num4.ToString(), ")," }));
            num7 = 0x2b;
            goto Label_0005;
        Label_08B2:
            num++;
            num7 = 30;
            goto Label_0005;
        Label_08C9:
            num7 = 2;
            goto Label_0005;
        }

        public int getExpandInputLen(string sCheckStr)
        {
            int num = 10;
            try
            {
                sCheckStr = sCheckStr.Substring(sCheckStr.IndexOf("InputLength") + "InputLength".Length);
            }
            catch
            {
                base.Response.Write(sCheckStr);
                base.Response.End();
            }
            try
            {
                if ((1 != 0) && (0 != 0))
                {
                }
                num = Convert.ToInt32(sCheckStr);
            }
            catch
            {
            }
            return num;
        }

        public int getInputObjLen(string sCheckStr)
        {
            int num;
        Label_004F:
            num = 50;
            string[] strArray = sCheckStr.Split(new char[] { '|' });
            int num2 = 0x10;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (!(strArray[4] == "Mob1"))
                    {
                        num2 = 4;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 1:
                    return num;

                case 2:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num = 200;
                    num2 = 14;
                    goto Label_0002;

                case 3:
                    num = 11;
                    num2 = 12;
                    goto Label_0002;

                case 4:
                    if (!(strArray[2] == "IDCard1"))
                    {
                        num2 = 8;
                    }
                    else
                    {
                        num2 = 9;
                    }
                    goto Label_0002;

                case 5:
                    num = 6;
                    num2 = 1;
                    goto Label_0002;

                case 6:
                    return num;

                case 7:
                    return num;

                case 8:
                    if (!(strArray[3] == "Date1"))
                    {
                        num2 = 11;
                    }
                    else
                    {
                        num2 = 13;
                    }
                    goto Label_0002;

                case 9:
                    num = 0x12;
                    num2 = 7;
                    goto Label_0002;

                case 10:
                    if (num <= 200)
                    {
                        return num;
                    }
                    num2 = 2;
                    goto Label_0002;

                case 11:
                    if (!(strArray[1] == "PostCode1"))
                    {
                        return num;
                    }
                    num2 = 5;
                    goto Label_0002;

                case 12:
                    return num;

                case 13:
                    num = 11;
                    num2 = 6;
                    goto Label_0002;

                case 14:
                    return num;

                case 15:
                    num = Convert.ToInt32(strArray[10].Substring(6));
                    num2 = 10;
                    goto Label_0002;

                case 0x10:
                    if (!(strArray[10] != "MaxLen"))
                    {
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 15;
                    }
                    goto Label_0002;
            }
            goto Label_004F;
        }

        public string getItem15SQL(long IID, DataTable dtItemTable)
        {
            string str;
            int num;
            int num2;
            int num3;
            goto Label_0057;
        Label_0002:
            switch (num3)
            {
                case 0:
                case 11:
                    num3 = 12;
                    goto Label_0002;

                case 1:
                    return str;

                case 2:
                case 15:
                    goto Label_016E;

                case 3:
                    if (!(dtItemTable.Rows[num]["IID"].ToString() == IID.ToString()))
                    {
                        num++;
                        num3 = 0;
                    }
                    else
                    {
                        num3 = 10;
                    }
                    goto Label_0002;

                case 4:
                case 0x12:
                    num3 = 5;
                    goto Label_0002;

                case 5:
                    if (num < dtItemTable.Rows.Count)
                    {
                        num3 = 0x11;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0002;

                case 6:
                    {
                        string str2 = str;
                        str = str2 + "F" + dtItemTable.Rows[num]["IID"].ToString() + " VARCHAR(" + num2.ToString() + ") DEFAULT '',";
                        num3 = 8;
                        goto Label_0002;
                    }
                case 7:
                    goto Label_029E;

                case 8:
                    goto Label_0117;

                case 9:
                    num2 = 500;
                    num3 = 15;
                    goto Label_0002;

                case 10:
                    num2 = Convert.ToInt16(dtItemTable.Rows[num]["OptionAmount"]) * 11;
                    num3 = 0x10;
                    goto Label_0002;

                case 12:
                    if (num < dtItemTable.Rows.Count)
                    {
                        num3 = 3;
                    }
                    else
                    {
                        num3 = 2;
                    }
                    goto Label_0002;

                case 13:
                    if (num2 <= 500)
                    {
                        goto Label_016E;
                    }
                    num3 = 9;
                    goto Label_0002;

                case 14:
                    num2 = this.getParentOptionAmount(dtItemTable, Convert.ToInt32(IID)) * 11;
                    num3 = 7;
                    goto Label_0002;

                case 0x10:
                    if (num2 != 0)
                    {
                        goto Label_029E;
                    }
                    num3 = 14;
                    goto Label_0002;

                case 0x11:
                    if (!(dtItemTable.Rows[num]["ParentID"].ToString() == IID.ToString()))
                    {
                        goto Label_0117;
                    }
                    num3 = 6;
                    goto Label_0002;
            }
        Label_0057:
            if ((1 != 0) && (0 != 0))
            {
            }
            str = "";
            num = 0;
            num2 = 200;
            num = 0;
            num3 = 11;
            goto Label_0002;
        Label_0117:
            num++;
            num3 = 4;
            goto Label_0002;
        Label_016E:
            num = 0;
            num3 = 0x12;
            goto Label_0002;
        Label_029E:
            num3 = 13;
            goto Label_0002;
        }

        public string getItem16SQL(long IID, DataTable dtItemTable, DataTable dtOption, int intInputLength)
        {
            StringBuilder builder;
            int num;
            int num2;
            int num3;
            goto Label_003F;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num2 < dtOption.Rows.Count)
                    {
                        num3 = 10;
                    }
                    else
                    {
                        num3 = 11;
                    }
                    goto Label_0002;

                case 1:
                case 7:
                    num3 = 5;
                    goto Label_0002;

                case 2:
                    return builder.ToString();

                case 3:
                case 12:
                    num3 = 0;
                    goto Label_0002;

                case 4:
                    if (!(dtItemTable.Rows[num]["ParentID"].ToString() == IID.ToString()))
                    {
                        goto Label_0135;
                    }
                    num3 = 9;
                    goto Label_0002;

                case 5:
                    if (num < dtItemTable.Rows.Count)
                    {
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 2;
                    }
                    goto Label_0002;

                case 6:
                    builder.Append("F" + dtItemTable.Rows[num]["IID"].ToString() + "_" + dtOption.Rows[num2]["OID"].ToString() + " VARCHAR(" + intInputLength.ToString() + ") DEFAULT '',");
                    num3 = 8;
                    goto Label_0002;

                case 8:
                    goto Label_0087;

                case 9:
                    num2 = 0;
                    num3 = 3;
                    goto Label_0002;

                case 10:
                    if (!(dtOption.Rows[num2]["IID"].ToString() == IID.ToString()))
                    {
                        goto Label_0087;
                    }
                    num3 = 6;
                    goto Label_0002;

                case 11:
                    goto Label_0135;
            }
        Label_003F:
            builder = new StringBuilder();
            num = 0;
            num2 = 0;
            num = 0;
            num3 = 7;
            goto Label_0002;
        Label_0087:
            num2++;
            if ((1 != 0) && (0 != 0))
            {
            }
            num3 = 12;
            goto Label_0002;
        Label_0135:
            num++;
            num3 = 1;
            goto Label_0002;
        }

        public string getItem18SQL(long IID, DataTable dtItemTable, DataTable dtOption)
        {
            StringBuilder builder;
            int num;
            int num2;
            int num3;
            goto Label_0047;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    builder.Append("F" + dtItemTable.Rows[num2]["IID"].ToString() + "_" + dtOption.Rows[num]["OID"].ToString() + " INT DEFAULT 0,");
                    num3 = 4;
                    goto Label_0002;

                case 1:
                    if (!(dtOption.Rows[num]["ISMatrixRowColumn"].ToString() == "True"))
                    {
                        goto Label_0183;
                    }
                    num3 = 0;
                    goto Label_0002;

                case 2:
                case 8:
                    num3 = 10;
                    goto Label_0002;

                case 3:
                    goto Label_0125;

                case 4:
                    goto Label_0183;

                case 5:
                    if (num2 < dtItemTable.Rows.Count)
                    {
                        num3 = 14;
                    }
                    else
                    {
                        num3 = 7;
                    }
                    goto Label_0002;

                case 6:
                    num = 0;
                    num3 = 8;
                    goto Label_0002;

                case 7:
                    return builder.ToString();

                case 9:
                    num3 = 1;
                    goto Label_0002;

                case 10:
                    if (num < dtOption.Rows.Count)
                    {
                        num3 = 13;
                    }
                    else
                    {
                        num3 = 3;
                    }
                    goto Label_0002;

                case 11:
                case 12:
                    num3 = 5;
                    goto Label_0002;

                case 13:
                    if (!(dtOption.Rows[num]["IID"].ToString() == IID.ToString()))
                    {
                        goto Label_0183;
                    }
                    num3 = 9;
                    goto Label_0002;

                case 14:
                    if (!(dtItemTable.Rows[num2]["ParentID"].ToString() == IID.ToString()))
                    {
                        goto Label_0125;
                    }
                    num3 = 6;
                    goto Label_0002;
            }
        Label_0047:
            builder = new StringBuilder();
            num = 0;
            num2 = 0;
            num2 = 0;
            num3 = 11;
            goto Label_0002;
        Label_0125:
            num2++;
            num3 = 12;
            goto Label_0002;
        Label_0183:
            num++;
            num3 = 2;
            goto Label_0002;
        }

        public string getItem19SQL(long IID, DataTable dtItemTable, int intInputLength)
        {
            string str;
            int num;
            int num2;
            goto Label_0027;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_0134;
                    }
                    goto Label_0046;

                case 1:
                    if (num < dtItemTable.Rows.Count)
                    {
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    goto Label_0002;

                case 2:
                    goto Label_0134;

                case 3:
                    if (!(dtItemTable.Rows[num]["ParentID"].ToString() == IID.ToString()))
                    {
                        goto Label_0046;
                    }
                    num2 = 5;
                    goto Label_0002;

                case 4:
                    return str;

                case 5:
                    {
                        string str2 = str;
                        str = str2 + "F" + dtItemTable.Rows[num]["IID"].ToString() + " INT ,F" + dtItemTable.Rows[num]["IID"].ToString() + "_Input VARCHAR(" + intInputLength.ToString() + ") ,";
                        num2 = 6;
                        goto Label_0002;
                    }
                case 6:
                    goto Label_0046;
            }
        Label_0027:
            str = "";
            num = 0;
            num2 = 0;
            goto Label_0002;
        Label_0046:
            num++;
            num2 = 2;
            goto Label_0002;
        Label_0134:
            num2 = 1;
            goto Label_0002;
        }

        public string getItem20SQL(long IID, DataTable dtItemTable, int intInputLength)
        {
            string str;
            int num;
            int num2;
            int num3;
            goto Label_0057;
        Label_0002:
            switch (num3)
            {
                case 0:
                    return str;

                case 1:
                    if (!(dtItemTable.Rows[num]["IID"].ToString() == IID.ToString()))
                    {
                        num++;
                        num3 = 13;
                    }
                    else
                    {
                        num3 = 5;
                    }
                    goto Label_0002;

                case 2:
                    if (num2 <= 500)
                    {
                        goto Label_0175;
                    }
                    num3 = 0x10;
                    goto Label_0002;

                case 3:
                    goto Label_011B;

                case 4:
                case 0x12:
                    goto Label_0175;

                case 5:
                    num2 = Convert.ToInt16(dtItemTable.Rows[num]["OptionAmount"]) * 11;
                    num3 = 9;
                    goto Label_0002;

                case 6:
                    num2 = this.getParentOptionAmount(dtItemTable, Convert.ToInt32(IID)) * 11;
                    num3 = 15;
                    goto Label_0002;

                case 7:
                    if (num < dtItemTable.Rows.Count)
                    {
                        num3 = 0x11;
                    }
                    else
                    {
                        num3 = 0;
                    }
                    goto Label_0002;

                case 8:
                    {
                        string str2 = str;
                        str = str2 + "F" + dtItemTable.Rows[num]["IID"].ToString() + " VARCHAR(" + num2.ToString() + "),F" + dtItemTable.Rows[num]["IID"].ToString() + "_Input VARCHAR(" + intInputLength.ToString() + ") ,";
                        num3 = 3;
                        goto Label_0002;
                    }
                case 9:
                    if (num2 != 0)
                    {
                        goto Label_02E3;
                    }
                    num3 = 6;
                    goto Label_0002;

                case 10:
                    if (num < dtItemTable.Rows.Count)
                    {
                        num3 = 1;
                    }
                    else
                    {
                        num3 = 0x12;
                    }
                    goto Label_0002;

                case 11:
                case 12:
                    num3 = 7;
                    goto Label_0002;

                case 13:
                case 14:
                    num3 = 10;
                    goto Label_0002;

                case 15:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_02E3;
                    }
                    goto Label_011B;

                case 0x10:
                    num2 = 500;
                    num3 = 4;
                    goto Label_0002;

                case 0x11:
                    if (!(dtItemTable.Rows[num]["ParentID"].ToString() == IID.ToString()))
                    {
                        goto Label_011B;
                    }
                    num3 = 8;
                    goto Label_0002;
            }
        Label_0057:
            str = "";
            num = 0;
            num2 = 200;
            num = 0;
            num3 = 14;
            goto Label_0002;
        Label_011B:
            num++;
            num3 = 12;
            goto Label_0002;
        Label_0175:
            num = 0;
            num3 = 11;
            goto Label_0002;
        Label_02E3:
            num3 = 2;
            goto Label_0002;
        }

        public string getItem21SQL(long IID, DataTable dtItemTable)
        {
            string str;
            int num;
            int num2;
            goto Label_0027;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 6:
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    if (num < dtItemTable.Rows.Count)
                    {
                        num2 = 4;
                    }
                    else
                    {
                        num2 = 5;
                    }
                    goto Label_0002;

                case 2:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    str = str + "F" + dtItemTable.Rows[num]["IID"].ToString() + " INT ,";
                    num2 = 3;
                    goto Label_0002;

                case 3:
                    goto Label_003F;

                case 4:
                    if (!(dtItemTable.Rows[num]["ParentID"].ToString() == IID.ToString()))
                    {
                        goto Label_003F;
                    }
                    num2 = 2;
                    goto Label_0002;

                case 5:
                    return str;
            }
        Label_0027:
            str = "";
            num = 0;
            num2 = 6;
            goto Label_0002;
        Label_003F:
            num++;
            num2 = 0;
            goto Label_0002;
        }

        public string getItem7SQL(long IID, DataTable dtItemTable)
        {
            string str;
            int num;
            int num2;
            goto Label_0027;
        Label_0002:
            switch (num2)
            {
                case 0:
                    return str;

                case 1:
                    str = str + "F" + dtItemTable.Rows[num]["IID"].ToString() + " INT DEFAULT 0,";
                    num2 = 3;
                    goto Label_0002;

                case 2:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (num < dtItemTable.Rows.Count)
                    {
                        num2 = 6;
                    }
                    else
                    {
                        num2 = 0;
                    }
                    goto Label_0002;

                case 3:
                    goto Label_003F;

                case 4:
                case 5:
                    num2 = 2;
                    goto Label_0002;

                case 6:
                    if (!(dtItemTable.Rows[num]["ParentID"].ToString() == IID.ToString()))
                    {
                        goto Label_003F;
                    }
                    num2 = 1;
                    goto Label_0002;
            }
        Label_0027:
            str = "";
            num = 0;
            num2 = 4;
            goto Label_0002;
        Label_003F:
            num++;
            num2 = 5;
            goto Label_0002;
        }

        public int getOptionAmount(long IID, DataTable dtItemTable)
        {
            int num;
            int num2;
            int num3;
            goto Label_0027;
        Label_0002:
            switch (num3)
            {
                case 0:
                case 3:
                    num3 = 5;
                    goto Label_0002;

                case 1:
                    goto Label_003B;

                case 2:
                    return num;

                case 4:
                    if (!(dtItemTable.Rows[num2]["IID"].ToString() == IID.ToString()))
                    {
                        goto Label_003B;
                    }
                    num3 = 6;
                    goto Label_0002;

                case 5:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (num2 < dtItemTable.Rows.Count)
                    {
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 2;
                    }
                    goto Label_0002;

                case 6:
                    num = Convert.ToInt32(dtItemTable.Rows[num2]["OptionAmount"]);
                    num3 = 1;
                    goto Label_0002;
            }
        Label_0027:
            num = 0;
            num2 = 0;
            num3 = 0;
            goto Label_0002;
        Label_003B:
            num2++;
            num3 = 3;
            goto Label_0002;
        }

        protected string[] getOptionArrJs(DataTable dtOptionTable)
        {
            string[] strArray;
        Label_001B:
            strArray = new string[2];
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("var _O = new Array();");
            int num = 0;
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 1:
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    if (num < dtOptionTable.Rows.Count)
                    {
                        builder.Append(string.Concat(new object[] { "_O[", num.ToString(), "] = [", dtOptionTable.Rows[num]["OID"], ",", dtOptionTable.Rows[num]["Point"], ",", dtOptionTable.Rows[num]["IID"], ",", dtOptionTable.Rows[num]["ParentNode"], ",'", dtOptionTable.Rows[num]["OptionName"], "','", dtOptionTable.Rows[num]["IsMatrixRowColumn"], "'];\n" }));
                        builder2.Append(string.Concat(new object[] { dtOptionTable.Rows[num]["OID"], "-", dtOptionTable.Rows[num]["IID"], "-", dtOptionTable.Rows[num]["IsMatrixRowColumn"], "/" }));
                        num++;
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 3:
                    strArray[0] = builder.ToString();
                    strArray[1] = builder2.ToString();
                    return strArray;
            }
            goto Label_001B;
        }

        protected int getParentOptionAmount(DataTable dt, int intCurrIID)
        {
            int num;
        Label_0043:
            num = 0;
            int num2 = 0;
            num2 = 0;
            int num3 = 7;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num2 < dt.Rows.Count)
                    {
                        num3 = 10;
                    }
                    else
                    {
                        num3 = 6;
                    }
                    goto Label_0002;

                case 1:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num = Convert.ToInt32(dt.Rows[num2]["ParentID"]);
                    num3 = 13;
                    goto Label_0002;

                case 2:
                    if (num2 < dt.Rows.Count)
                    {
                        num3 = 8;
                    }
                    else
                    {
                        num3 = 11;
                    }
                    goto Label_0002;

                case 3:
                    return num;

                case 4:
                    num = Convert.ToInt32(dt.Rows[num2]["OptionAmount"]);
                    num3 = 3;
                    goto Label_0002;

                case 5:
                case 9:
                    num3 = 0;
                    goto Label_0002;

                case 6:
                    return num;

                case 7:
                case 12:
                    num3 = 2;
                    goto Label_0002;

                case 8:
                    if (!(dt.Rows[num2]["IID"].ToString() == intCurrIID.ToString()))
                    {
                        num2++;
                        num3 = 12;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0002;

                case 10:
                    if (!(dt.Rows[num2]["IID"].ToString() == num.ToString()))
                    {
                        num2++;
                        num3 = 5;
                    }
                    else
                    {
                        num3 = 4;
                    }
                    goto Label_0002;

                case 11:
                case 13:
                    num2 = 0;
                    num3 = 9;
                    goto Label_0002;
            }
            goto Label_0043;
        }

        protected string[] getSurveyExpand(long UID, long SID)
        {
        Label_0027:
            ;
            string[] strArray = new string[] { "", "" };
            //objComm.CommandText = "SELECT TOP 1 ExpandContent,ExpandType FROM SurveyExpand WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND (ExpandType=0 OR ExpandType=8)";
            SqlDataReader reader = new Survey_WriteSurveyFile_Layer().GetSurveyExpand(SID.ToString(), UID.ToString());
            int num = 2;
        Label_0002:
            switch (num)
            {
                case 0:
                    reader.Dispose();
                    return strArray;

                case 1:
                    if (!(reader[1].ToString() == "0"))
                    {
                        strArray[1] = reader[0].ToString();
                        num = 6;
                    }
                    else
                    {
                        num = 4;
                    }
                    goto Label_0002;

                case 2:
                case 3:
                case 6:
                    num = 5;
                    goto Label_0002;

                case 4:
                    strArray[0] = reader[0].ToString();
                    num = 3;
                    goto Label_0002;

                case 5:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (reader.Read())
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_0002;
            }
            goto Label_0027;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            StringBuilder builder;
            string[] strArray;
            StringBuilder builder2;
            long num;
            long num2;
            string sqlCommand="";
            DataSet set;
            int num3;
            string str6;
            int num4;
            string str7;
            int num5;
            string str8;
            short num6;
            int num9 = 0;
            string str12;
            string str14;
            int num11;
            goto Label_00AE;
        Label_0005:
            switch (num11)
            {
                case 0:
                    num5 = 500;
                    num11 = 3;
                    goto Label_0005;

                case 1:
                    if (!(set.Tables["ItemTable"].Rows[num6]["ParentID"].ToString() == "0"))
                    {
                        goto Label_0E83;
                    }
                    num11 = 5;
                    goto Label_0005;

                case 2:
                case 7:
                case 0x15:
                case 0x16:
                case 0x18:
                case 0x1b:
                case 0x1c:
                case 0x1d:
                case 0x20:
                case 0x26:
                    goto Label_0E83;

                case 3:
                    goto Label_0EF6;

                case 4:
                    if (num4 != 5)
                    {
                        goto Label_0E83;
                    }
                    num11 = 0x25;
                    goto Label_0005;

                case 5:
                    {
                        string str11 = str8;
                        str8 = str11 + " F" + str7 + " VARCHAR(" + this.getInputObjLen(set.Tables["ItemTable"].Rows[num6]["DataFormatCheck"].ToString()).ToString() + ") ,";
                        num11 = 0x16;
                        goto Label_0005;
                    }
                case 6:
                    str8 = str8.Substring(0, str8.Length - 1) + ")";
                    sqlCommand = str8;
                    num11 = 0x22;
                    goto Label_0005;

                case 8:
                    goto Label_1051;

                case 9:
                case 0x1a:
                    num11 = 0x27;
                    goto Label_0005;

                case 10:
                    str8 = str8 + " F" + str7 + "_input   VARCHAR(50) ,";
                    num11 = 2;
                    goto Label_0005;

                case 11:
                    if (num5 <= 500)
                    {
                        goto Label_03EC;
                    }
                    num11 = 13;
                    goto Label_0005;

                case 12:
                    num5 = this.getParentOptionAmount(set.Tables["ItemTable"], Convert.ToInt32(str7)) * 11;
                    num11 = 0x19;
                    goto Label_0005;

                case 13:
                    num5 = 500;
                    num11 = 0x13;
                    goto Label_0005;

                case 14:
                    if (num4 != 9)
                    {
                        goto Label_0E83;
                    }
                    num11 = 10;
                    goto Label_0005;

                case 15:
                    if (!(set.Tables["SurveyTable"].Rows[0]["State"].ToString() == "0"))
                    {
                        goto Label_1051;
                    }
                    num11 = 6;
                    goto Label_0005;

                case 0x10:
                    if (num5 <= 500)
                    {
                        goto Label_0DBD;
                    }
                    num11 = 0x23;
                    goto Label_0005;

                case 0x11:
                    num11 = 0x1d;
                    goto Label_0005;

                case 0x12:
                    if (num5 != 0)
                    {
                        goto Label_0D79;
                    }
                    num11 = 12;
                    goto Label_0005;

                case 0x13:
                    goto Label_03EC;

                case 20:
                    num9 = num4;
                    num11 = 30;
                    goto Label_0005;

                case 0x17:
                    if (!(set.Tables["SurveyTable"].Rows[0]["State"].ToString() == "0"))
                    {
                        goto Label_0E83;
                    }
                    num11 = 20;
                    goto Label_0005;

                case 0x19:
                    goto Label_0D79;

                case 30:
                    switch (num9)
                    {
                        case 1:
                            num11 = 1;
                            goto Label_0005;

                        case 2:
                            str8 = str8 + " F" + str7 + "  INT ,";
                            num11 = 0x1b;
                            goto Label_0005;

                        case 3:
                            str8 = str8 + " F" + str7 + " MEMO  ,";
                            num11 = 0x15;
                            goto Label_0005;

                        case 4:
                        case 5:
                        case 11:
                            if ((1 != 0) && (0 != 0))
                            {
                            }
                            str8 = str8 + " F" + str7 + "  INT ,";
                            num11 = 4;
                            goto Label_0005;

                        case 6:
                            str8 = str8 + " F" + str7 + "  INT ,";
                            num11 = 0x1c;
                            goto Label_0005;

                        case 7:
                        case 14:
                        case 15:
                        case 0x10:
                            goto Label_0E83;

                        case 8:
                        case 9:
                        case 10:
                            num5 = Convert.ToInt16(set.Tables["ItemTable"].Rows[num6]["OptionAmount"]) * 11;
                            num11 = 0x12;
                            goto Label_0005;

                        case 12:
                            num11 = 0x10;
                            goto Label_0005;

                        case 13:
                            num5 = Convert.ToInt16(set.Tables["ItemTable"].Rows[num6]["OptionAmount"]) * 50;
                            num11 = 11;
                            goto Label_0005;

                        case 0x11:
                            {
                                string str15 = str8;
                                str8 = str15 + " F" + str7 + "  IMAGE NULL,F" + str7 + "_FileName VARCHAR(50) ,";
                                num11 = 0x20;
                                goto Label_0005;
                            }
                    }
                    num11 = 0x11;
                    goto Label_0005;

                case 0x1f:
                    if (num5 <= 500)
                    {
                        goto Label_0EF6;
                    }
                    num11 = 0;
                    goto Label_0005;

                case 0x21:
                    goto Label_0DBD;

                case 0x22:
                    try
                    {
                        //new Survey_WriteSurveyFile_Layer().ExcutSql(sqlCommand);
                    }
                    catch
                    {
                        base.Response.End();
                    }
                    this.saveSurveyExpand(builder2.ToString(), strArray[1], num, num2);
                    num11 = 8;
                    goto Label_0005;

                case 0x23:
                    num5 = 500;
                    num11 = 0x21;
                    goto Label_0005;

                case 0x24:
                    num11 = 15;
                    goto Label_0005;

                case 0x25:
                    str8 = str8 + " F" + str7 + "_input  VARCHAR(50)  ,";
                    num11 = 0x18;
                    goto Label_0005;

                case 0x27:
                    if (num6 < set.Tables["ItemTable"].Rows.Count)
                    {
                        str7 = Convert.ToString(set.Tables["ItemTable"].Rows[num6]["IID"]);
                        num4 = Convert.ToInt32(set.Tables["ItemTable"].Rows[num6]["ItemType"]);
                        str6 = "_I[" + num3.ToString() + "]";
                        builder.Append(str6 + " = new Array();");
                        builder.Append(str6 + "[0] = " + str7 + ";");
                        builder.Append(string.Concat(new object[] { str6, "[1] = '", set.Tables["ItemTable"].Rows[num6]["ItemName"], "';" }));
                        builder.Append(string.Concat(new object[] { str6, "[2] = ", set.Tables["ItemTable"].Rows[num6]["PageNo"], ";" }));
                        builder.Append(string.Concat(new object[] { str6, "[3] = '", set.Tables["ItemTable"].Rows[num6]["DataFormatCheck"], "';" }));
                        builder.Append(string.Concat(new object[] { str6, "[4] = ", set.Tables["ItemTable"].Rows[num6]["ItemType"], ";" }));
                        builder.Append(string.Concat(new object[] { str6, "[5] = '", set.Tables["ItemTable"].Rows[num6]["Logic"], "';" }));
                        builder.Append(string.Concat(new object[] { str6, "[6] = ", set.Tables["ItemTable"].Rows[num6]["ParentID"], ";" }));
                        builder.Append(string.Concat(new object[] { str6, "[7] = ", set.Tables["ItemTable"].Rows[num6]["OptionAmount"], ";" }));
                        builder.Append(string.Concat(new object[] { str6, "[8] = ", set.Tables["ItemTable"].Rows[num6]["ChildID"], ";" }));
                        builder.Append(string.Concat(new object[] { str6, "[9] = '", set.Tables["ItemTable"].Rows[num6]["OptionImgModel"], "';" }));
                        builder.Append(string.Concat(new object[] { str6, "[10] = '", set.Tables["ItemTable"].Rows[num6]["MultiReject"], "';" }));
                        builder2.Append(string.Concat(new object[] { str7, "-", set.Tables["ItemTable"].Rows[num6]["ItemType"], "-", set.Tables["ItemTable"].Rows[num6]["OptionAmount"], "-", set.Tables["ItemTable"].Rows[num6]["ParentID"], "-", set.Tables["ItemTable"].Rows[num6]["DataFormatCheck"], "/" }));
                        num3++;
                        num11 = 0x17;
                    }
                    else
                    {
                        num11 = 0x24;
                    }
                    goto Label_0005;
            }
        Label_00AE:
            str = "";
            string input = "";
            builder = new StringBuilder("var _I = new Array();");
            string str3 = "";
            string str4 = "";
            strArray = new string[2];
            builder2 = new StringBuilder();
            num = 0;
            num2 = 0;
            num2 = ConvertHelper.ConvertLong(this.Session["UserID"]);
            languageClass class3 = new languageClass();
            str = base.Request.Form["Memo"].ToString();
            Convert.ToString(base.Request.Form["Active"]);
            num = Convert.ToInt32(base.Request.Form["SID"]);
            class3.getLanguage();
            string str5 = "var sLanguage=\"" + class3._arrLanguage[class3.getLan(num), 0].Replace("\"", "'") + "\";";
            //command.CommandText = "SELECT ItemHTML,I.IID,ItemName,PageNo,DataFormatCheck,ItemType,Logic,ParentID,OptionAmount,OptionImgModel,ChildID,MultiReject FROM ItemTable I LEFT JOIN ItemTableExpand I1 ON I.IID=I1.IID WHERE I.UID=" + num2.ToString() + " AND I.SID=" + num.ToString() + "  ORDER BY PageNo,Sort";
            //adapter.Fill(set, "ItemTable");
            set = new DataSet();

            DataTable ItemTable = new Survey_WriteSurveyFile_Layer().GetItemTable(num2.ToString(), num.ToString());
            ItemTable.TableName = "ItemTable";

            
            //command.CommandText = "SELECT TOP 1 SurveyName,TempPage,State,Active,SID,Par,SID FROM SurveyTable WHERE UID=" + num2.ToString() + " AND SID=" + num.ToString();
            //adapter.Fill(set, "SurveyTable");
            DataTable SurveyTable = new Survey_WriteSurveyFile_Layer().GetSurveyTable(num2.ToString(), num.ToString());
            SurveyTable.TableName = "SurveyTable";

            //command.CommandText = "SELECT MAX(PageNo) AS MAXPageNo FROM PageTable WHERE UID=" + num2.ToString() + " AND SID=" + num.ToString();
            //adapter.Fill(set, "PageTable");
            DataTable PageTable = new Survey_WriteSurveyFile_Layer().GetPageTable(num2.ToString(), num.ToString());
            PageTable.TableName = "PageTable";

            //command.CommandText = "SELECT  OID,Point,IID,ParentNode,OptionName,ISMatrixRowColumn FROM OptionTable WHERE SID=" + num.ToString() + " AND UID=" + num2.ToString();
            //adapter.Fill(set, "OptionTable");
            DataTable OptionTable = new Survey_WriteSurveyFile_Layer().GetOptionTable(num2.ToString(), num.ToString());
            OptionTable.TableName = "OptionTable";

            set.Tables.Add(ItemTable);
            set.Tables.Add(SurveyTable);
            set.Tables.Add(PageTable);
            set.Tables.Add(OptionTable);


            builder.Append("sHiddenItem='" + this.getHiddenItem( num) + "';");
            builder.Append("intpageamount=" + set.Tables["PageTable"].Rows[0][0].ToString() + ";");
            strArray = this.getOptionArrJs(set.Tables["OptionTable"]);
            str4 = set.Tables["SurveyTable"].Rows[0]["Par"].ToString();
            str4.IndexOf("CheckCode:1").ToString();
            str4.IndexOf("PSW:1").ToString();
            num3 = 0;
            str6 = "";
            num4 = 0;
            str7 = "";
            num5 = 50;
            str8 = " CREATE TABLE Z" + num.ToString() + " (ID bigint,IP VARCHAR(16) ,[GUID] VARCHAR(16) ,SUBMITTIME DATETIME,Point INT ,AnswerTime INT ,";
            num6 = 0;
            num6 = 0;
            num11 = 9;
            goto Label_0005;
        Label_03EC:
            str14 = str8;
            str8 = str14 + " F" + str7 + "  VARCHAR(" + num5.ToString() + ") ,";
            num11 = 0x26;
            goto Label_0005;
        Label_0D79:
            num11 = 0x1f;
            goto Label_0005;
        Label_0DBD:
            num5 = Convert.ToInt16(set.Tables["ItemTable"].Rows[num6]["OptionAmount"]) * 13;
            string str13 = str8;
            str8 = str13 + " F" + str7 + "  VARCHAR(" + num5.ToString() + ") ,";
            num11 = 7;
            goto Label_0005;
        Label_0E83:
            num6 = (short)(num6 + 1);
            num11 = 0x1a;
            goto Label_0005;
        Label_0EF6:
            str12 = str8;
            str8 = str12 + " F" + str7 + "  VARCHAR(" + num5.ToString() + ") ,";
            num11 = 14;
            goto Label_0005;
        Label_1051:
            //command.CommandText = "UPDATE SurveyTable SET State=1,Active=1 WHERE SID=" + num.ToString();
            new Survey_WriteSurveyFile_Layer().UpdateSurveyTable("1","1",num.ToString());
            
            //创建问卷到对应文件夹中
            //string path = base.Server.MapPath(@"UserData\U" + num2.ToString() + @"\") + @"\s" + num.ToString() + ".aspx";
            string path = base.Server.MapPath(@"UserData\U" + 1 + @"\") + @"\s" + num.ToString() + ".aspx";

            string pattern = "(<style>[^<]*</style>)";
            Match match = Regex.Match(str, pattern, RegexOptions.IgnoreCase);
            input = "<link href='../../../css/AdvObj.css' rel='stylesheet' type='text/css'>";
            input = input + match.Groups[1].Value + "<style type=\"text/css\">.PageBox{display:none;} #submitbt{visibility:hidden;} #nextpagebt{visibility:hidden;}</style>";
            str = Regex.Replace(Regex.Replace(str, pattern, "", RegexOptions.IgnoreCase), "<IMG SRC=\"UserData/U" + num2.ToString() + "/", "<IMG SRC=\"", RegexOptions.IgnoreCase);
            str3 = "<input type='hidden' name='SID' value='" + num.ToString() + "'><input type='hidden' name='GUID' ID='GUID' value='0'><input type='hidden' name='Point' id='Point' value='0'><input type='hidden' name='AnswerTime' id='AnswerTime' value='<%=dStartTime%>'><asp:Label ID=\"SID\" runat=\"server\" Visible=\"False\">" + num.ToString() + "</asp:Label>";
            str = Regex.Replace(str, "template/surveystyle/", "../../template/surveystyle/", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "template/surveystyle/", "../../template/surveystyle/", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "</form>", str3 + "</form>", RegexOptions.IgnoreCase);
            str = "<%@ page language=\"C#\" autoeventwireup=\"true\" inherits=\"Web_Survey.Survey.Survey_SurveyCode, Web_Survey\" %><!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\"><html><head>" + input + "<script language='javascript' type='text/javascript'><%=sClientJs%></script><script src='../../js/client.js' type='text/javascript' language='javascript'></script><script src='s" + num.ToString() + ".aspx.js' type='text/javascript' language='javascript'></script><title>" + set.Tables["SurveyTable"].Rows[0]["SurveyName"].ToString() + "--</title></head><body>" + str + "</body></html>";
            StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("utf-8"));
            writer.WriteLine(str);
            writer.Close();
            writer = new StreamWriter(path + ".js", false, Encoding.GetEncoding("utf-8"));
            writer.WriteLine(builder.ToString() + strArray[0] + str5);
            writer.Close();
            writer.Dispose();

        }

        protected void saveSurveyExpand(string sItemStr, string sOptionStr, long SID, long UID)
        {
            //objComm.CommandText = "DELETE FROM SurveyExpand WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND (ExpandType=3 OR ExpandType=4)";
            new Survey_WriteSurveyFile_Layer().DeleteSurveyExpand(SID.ToString(), UID.ToString());
            //objComm.CommandText = " INSERT INTO SurveyExpand(SID,UID,ExpandType,ExpandContent) VALUES(" + SID.ToString() + "," + UID.ToString() + ",3,'" + sItemStr + "')";
            new Survey_WriteSurveyFile_Layer().InsertSurveyExpand(SID.ToString(), UID.ToString(), "3", sItemStr);
            //objComm.CommandText = " INSERT INTO SurveyExpand(SID,UID,ExpandType,ExpandContent) VALUES(" + SID.ToString() + "," + UID.ToString() + ",4,'" + sOptionStr + "')";
            new Survey_WriteSurveyFile_Layer().InsertSurveyExpand(SID.ToString(), UID.ToString(), "4", sOptionStr);
        }

        protected string getHiddenItem(long SID)
        {
            string str;
        Label_0017:
            str = "";
            //objComm.CommandText = "SELECT TOP 1 ExpandContent FROM SurveyExpand WHERE SID=" + SID.ToString() + " AND ExpandType=0";
            SqlDataReader reader = new Survey_WriteSurveyFile_Layer().GetSurveyExpand1(SID.ToString(),"0");
            int num = 1;
        Label_0002:
            switch (num)
            {
                case 0:
                    str = reader[0].ToString();
                    num = 2;
                    goto Label_0002;

                case 1:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num = 0;
                    goto Label_0002;

                case 2:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    break;

                default:
                    goto Label_0017;
            }
            reader.Dispose();
            return str;
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