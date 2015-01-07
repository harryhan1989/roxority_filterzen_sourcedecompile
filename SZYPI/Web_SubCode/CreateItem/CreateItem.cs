namespace CreateItem
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Web;
    using BusinessLayer.CreateItem;
    using System.Data.SqlClient;

    public class CreateItem
    {
        private  string[,] a = new string[10, 2];

        public string createCheckStr(string sCheckStr, string[] arrFormatData, int intItemType)
        {
            string str;
            int num = 0; //∏≥≥ı÷µ
            int num2;
            goto Label_0047;
        Label_0002:
            switch (num2)
            {
                case 0:
                    return str;

                case 1:
                    intItemType = Convert.ToInt32(arrFormatData[0]);
                    num2 = 9;
                    goto Label_0002;

                case 2:
                    return str;

                case 3:
                    num2 = 0;
                    goto Label_0002;

                case 4:
                    if (intItemType != 0)
                    {
                        goto Label_0165;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 5:
                    return str;

                case 6:
                    return str;

                case 7:
                    return str;

                case 8:
                    return str;

                case 9:
                    goto Label_0165;

                case 10:
                    switch (num)
                    {
                        case 1:
                            str = "Empty" + arrFormatData[0x16] + "|PostCode" + arrFormatData[13] + "|IDCard" + arrFormatData[15] + "|Date" + arrFormatData[14] + "|Mob" + arrFormatData[12] + "|Email" + arrFormatData[0x13] + "|En" + arrFormatData[0x11] + "|Cn" + arrFormatData[0x10] + "|URL" + arrFormatData[0x12] + "|MinLen" + arrFormatData[2] + "|MaxLen" + arrFormatData[3];
                            num2 = 5;
                            goto Label_0002;

                        case 2:
                            str = "Empty" + arrFormatData[0x16] + "|MinValue" + arrFormatData[9] + "|MaxValue" + arrFormatData[8];
                            num2 = 12;
                            goto Label_0002;

                        case 3:
                        case 4:
                        case 6:
                        case 7:
                        case 11:
                        case 0x12:
                            str = "Empty" + arrFormatData[0x16];
                            num2 = 8;
                            goto Label_0002;

                        case 5:
                        case 0x10:
                        case 0x13:
                            str = "Empty" + arrFormatData[0x16] + "|InputLength" + arrFormatData[0x23];
                            num2 = 7;
                            goto Label_0002;

                        case 8:
                        case 10:
                        case 15:
                            str = "Empty" + arrFormatData[0x16] + "|MinSelect" + arrFormatData[4] + "|MaxSelect" + arrFormatData[5];
                            if ((1 != 0) && (0 != 0))
                            {
                            }
                            num2 = 14;
                            goto Label_0002;

                        case 9:
                            str = "Empty" + arrFormatData[0x16] + "|MinSelect" + arrFormatData[4] + "|MaxSelect" + arrFormatData[5] + "|InputLength" + arrFormatData[0x23];
                            num2 = 13;
                            goto Label_0002;

                        case 12:
                        case 14:
                            return str;

                        case 13:
                            str = "Empty" + arrFormatData[0x16] + "|MinTickOff" + arrFormatData[6] + "|MaxTickOff" + arrFormatData[7];
                            num2 = 11;
                            goto Label_0002;

                        case 0x11:
                            return ("Empty" + arrFormatData[0x16] + "|MaxFileLen" + arrFormatData[0x1c] + "|UploadMode" + arrFormatData[0x1d] + "|FileType" + arrFormatData[30]);

                        case 20:
                            str = "Empty" + arrFormatData[0x16] + "|MinSelect" + arrFormatData[4] + "|MaxSelect" + arrFormatData[5] + "|InputLength" + arrFormatData[0x23];
                            num2 = 2;
                            goto Label_0002;

                        case 0x15:
                            str = "Empty" + arrFormatData[0x16] + "|TotalPerent" + arrFormatData[0x20] + "|MinPercent" + arrFormatData[0x21] + "|MaxPercent" + arrFormatData[0x22];
                            num2 = 6;
                            goto Label_0002;
                    }
                    num2 = 3;
                    goto Label_0002;

                case 11:
                    return str;

                case 12:
                    return str;

                case 13:
                    return str;

                case 14:
                    return str;
            }
        Label_0047:
            str = "";
            num2 = 4;
            goto Label_0002;
        Label_0165:
            num = intItemType;
            num2 = 10;
            goto Label_0002;
        }

        public void createItem(string[] arrFormsData, long SID, long UID, long intModifyIID)
        {
            int num;
        Label_0017:
            num = Convert.ToInt32(arrFormsData[0]);
            int num2 = 1;
        Label_0002:
            switch (num2)
            {
                case 0:
                    break;

                case 1:
                    switch (num)
                    {
                        case 1:
                        case 3:
                            this.createItem_Text(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 2:
                            this.createItem_Number(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 4:
                        case 5:
                        case 6:
                            this.createItem_Single(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 7:
                            this.createItem_MatrixSingle(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 8:
                        case 9:
                        case 10:
                            this.createItem_Multi(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 11:
                            this.createItem_Level(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 12:
                            this.createItem_Sort(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 13:
                            this.createItem_ListInput(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 14:
                            this.createItem_Text_NotInput(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 15:
                            this.createItem_MatrixMulti(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 0x10:
                            if ((1 != 0) && (0 != 0))
                            {
                            }
                            this.createItem_MatrixInput(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 0x12:
                            this.createItem_MatrixDropList(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 0x13:
                            this.createItem_MatrixSingleInput(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 20:
                            this.createItem_MatrixMultiInput(SID, UID, arrFormsData, 0, intModifyIID);
                            return;

                        case 0x15:
                            this.createItem_Percent(SID, UID, arrFormsData, 0, intModifyIID);
                            num2 = 0;
                            goto Label_0002;
                    }
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    return;

                default:
                    goto Label_0017;
            }
        }

        public void createItem(string[] arrFormsData, long SID, long UID, string sCheckStr)
        {
            int num;
        Label_0017:
            num = this.getSort(SID, Convert.ToInt32(arrFormsData[1]));
            int num2 = Convert.ToInt32(arrFormsData[0]);
            int num3 = 2;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if ((1 == 0) || (0 == 0))
                    {
                        return;
                    }
                    break;

                case 1:
                    return;

                case 2:
                    switch (num2)
                    {
                        case 1:
                        case 3:
                            this.createItem_Text(SID, UID, arrFormsData, num, sCheckStr);
                            return;

                        case 2:
                            this.createItem_Number(SID, UID, arrFormsData, num, sCheckStr);
                            return;

                        case 4:
                        case 5:
                        case 6:
                            this.createItem_Single(SID, UID, arrFormsData, num, sCheckStr);
                            return;

                        case 8:
                        case 9:
                        case 10:
                            this.createItem_Multi(SID, UID, arrFormsData, num, sCheckStr);
                            return;

                        case 11:
                            this.createItem_Level(SID, UID, arrFormsData, num, sCheckStr);
                            return;

                        case 12:
                            this.createItem_Sort(SID, UID, arrFormsData, num, sCheckStr);
                            return;

                        case 13:
                            this.createItem_ListInput(SID, UID, arrFormsData, num, sCheckStr);
                            return;

                        case 14:
                            this.createItem_Text_NotInput(SID, UID, arrFormsData, num, sCheckStr);
                            return;

                        case 15:
                            this.createItem_MatrixMulti(SID, UID, arrFormsData, num, sCheckStr);
                            return;

                        case 0x10:
                            this.createItem_MatrixInput(SID, UID, arrFormsData, num, sCheckStr);
                            return;

                        case 0x11:
                        case 0x12:
                        case 0x13:
                        case 20:
                            return;

                        case 0x15:
                            this.createItem_Percent(SID, UID, arrFormsData, num, sCheckStr);
                            num3 = 0;
                            goto Label_0002;
                    }
                    num3 = 1;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            this.createItem_MatrixSingle(SID, UID, arrFormsData,num, sCheckStr);
        }

        public void createItem_FileUpload(long SID, long UID, string[] arrFormsData, int intSort, int intModifyIID)
        {
            //objComm.CommandText = string.Concat(new object[] { "UPDATE ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), " AND IID=", intModifyIID });
            new CreateItem_Layer().CreateItemText_UpdateItemTable(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID);
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_FileUpload_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_FileUpload_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
        }

        public void createItem_FileUpload(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            int num2 = 0;
        Label_000D:
            switch (num2)
            {
                case 1:
                    break;

                case 2:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num2 = 1;
                    goto Label_000D;

                default:
                    if (sCheckStr == "")
                    {
                        num2 = 2;
                        goto Label_000D;
                    }
                    break;
            }
            //objComm.CommandText = "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ItemContent,PageNo,Sort) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + arrFormsData[10] + "'," + arrFormsData[0] + ",'" + sCheckStr + "','" + arrFormsData[11] + "'," + arrFormsData[1] + "," + intSort.ToString() + ")";
            new CreateItem_Layer().CreateItemNumber_InsertItemTable5(SID.ToString(), UID.ToString(), arrFormsData[10], arrFormsData[0], sCheckStr, arrFormsData[11], arrFormsData[1], intSort.ToString());
            string str = this.getCurrIID(SID, UID).ToString();
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_FileUpload_HTML(SID, UID, Convert.ToInt32(str), objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + str;
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_FileUpload_HTML(SID, UID, Convert.ToInt32(str)), SID.ToString(), str);
        }

        public string createItem_FileUpload_HTML(long SID, long UID, long IID)
        {
            string str;
        Label_0017:
            str = "";
            //objComm.CommandText = "SELECT ItemName,ItemType,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            SqlDataReader reader = new CreateItem_Layer().GetItemTable1(SID.ToString(), UID.ToString(), IID);
            int num = 0;
        Label_0002:
            switch (num)
            {
                case 0:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num = 2;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                {
                    string str2 = reader["ItemContent"].ToString();
                    str = string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", reader["ItemName"], "</span></div><div class='ItemContent'>", str2, "</div><input type='file' name='f", IID.ToString(), "' id='f", IID.ToString(), "' /></div>" });
                    num = 1;
                    goto Label_0002;
                }
                default:
                    goto Label_0017;
            }
            reader.Close();
            reader.Dispose();
            return str;
        }

        public void createItem_Level(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            //objComm.CommandText = "UPDATE ItemTable SET ItemName='" + arrFormsData[10] + "',OptionAmount=" + arrFormsData[0x19] + ",ItemContent='" + arrFormsData[11] + "',PageNo=" + arrFormsData[1] + ",OtherProperty='" + arrFormsData[0x1b] + "|" + arrFormsData[0x1a] + "',DataFormatCheck='" + this.createCheckStr("", arrFormsData, 0) + "',OptionImgModel=" + arrFormsData[0x17] + " WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + intModifyIID.ToString();
            new CreateItem_Layer().CreateItemText_UpdateItemTable5(arrFormsData[10], arrFormsData[0x19], arrFormsData[11], arrFormsData[1], arrFormsData[0x1b] + "|" + arrFormsData[0x1a], this.createCheckStr("", arrFormsData, 0), arrFormsData[0x17], SID.ToString(), UID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = " UPDATE ItemTable SET ItemHTML='" + this.createItem_Level_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Level_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
        }

        public void createItem_Level(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            int num2 = 0;
        Label_000D:
            switch (num2)
            {
                case 1:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    break;

                case 2:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num2 = 1;
                    goto Label_000D;

                default:
                    if (sCheckStr == "")
                    {
                        num2 = 2;
                        goto Label_000D;
                    }
                    break;
            }
            this.getCurrIID(SID, UID);
            //objComm.CommandText = "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,ItemContent,PageNo,Sort,OtherProperty,OptionImgModel) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + arrFormsData[10] + "',11,'" + sCheckStr + "',0," + arrFormsData[0x19] + ",'" + arrFormsData[11] + "'," + arrFormsData[1] + "," + intSort.ToString() + ",'" + arrFormsData[0x1b] + "|" + arrFormsData[0x1a] + "'," + arrFormsData[0x17] + ")";
            new CreateItem_Layer().CreateItemNumber_InsertItemTable3(SID.ToString(), UID.ToString(), arrFormsData[10], "11", sCheckStr, "0", arrFormsData[0x19], arrFormsData[11], arrFormsData[1], intSort.ToString(), arrFormsData[0x1b] + "|" + arrFormsData[0x1a], arrFormsData[0x17]);
            string str = this.getCurrIID(SID, UID).ToString();
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Level_HTML(SID, UID, Convert.ToInt32(str), objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + str;
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Level_HTML(SID, UID, Convert.ToInt32(str)), SID.ToString(), str);
        }

        public string createItem_Level_HTML(long SID, long UID, long IID)
        {
            string str;
            string str5 = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num3 = 0; //∏≥≥ı÷µ
            int num4 = 0; //∏≥≥ı÷µ
        Label_0063:
            str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            //objComm.CommandText = "SELECT ItemName,ItemType,ItemContent,OptionAmount,OtherProperty,OptionImgModel FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            SqlDataReader reader = new CreateItem_Layer().GetItemTable5(SID.ToString(), UID.ToString(), IID);
            int num5 = 5;
        Label_0002:
            switch (num5)
            {
                case 0:
                case 15:
                case 20:
                    break;

                case 1:
                    if (num4 == 0)
                    {
                        str5 = "<table style='width:auto'  border='0' cellpadding='5' cellspacing='1'><tr><td><table width='100%'  border='0' cellpadding='0' cellspacing='0'><tr><td style='width:50%' align='left'><strong>" + str5.Substring(0, str5.IndexOf('|')) + "</strong></td><td style='width:50%' align='right'><strong>" + str5.Substring(str5.IndexOf('|') + 1) + "</strong></td></tr></table><table style='width:auto'>";
                        num = 1;
                        num5 = 8;
                    }
                    else
                    {
                        num5 = 3;
                    }
                    goto Label_0002;

                case 2:
                    if (num3 <= Convert.ToInt32(reader["OptionAmount"]))
                    {
                        string str8 = str;
                        str = str8 + "<td><input type='radio' name='f" + IID.ToString() + "' id='f" + IID.ToString() + "_" + num3.ToString() + "' value='" + num3.ToString() + "'></td>";
                        str4 = str4 + "<td><span class='OptionName'>" + num3.ToString() + "</span></td>";
                        string str9 = str5;
                        str5 = str9 + "<td><span class=OptionImgGray" + reader["OptionImgModel"].ToString() + "  id='Img_f" + IID.ToString() + "_" + num3.ToString() + "'></span></td>";
                        num3++;
                        num5 = 12;
                    }
                    else
                    {
                        num5 = 14;
                    }
                    goto Label_0002;

                case 3:
                    num5 = 0x10;
                    goto Label_0002;

                case 4:
                case 8:
                    num5 = 11;
                    goto Label_0002;

                case 5:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num5 = 7;
                    goto Label_0002;

                case 6:
                    num5 = 0x11;
                    goto Label_0002;

                case 7:
                    str2 = reader["ItemName"].ToString();
                    str3 = reader["ItemContent"].ToString();
                    str5 = reader["OtherProperty"].ToString();
                    num4 = Convert.ToInt32(reader["OptionImgModel"]);
                    num5 = 1;
                    goto Label_0002;

                case 9:
                    str = str5 + "<tr>" + str4 + "</tr><tr>" + str + "</tr></table></td></tr></table>";
                    num5 = 15;
                    goto Label_0002;

                case 10:
                case 0x12:
                    num5 = 13;
                    goto Label_0002;

                case 11:
                    if (num <= Convert.ToInt32(reader["OptionAmount"]))
                    {
                        str4 = str4 + "<td align='center' style='font-family:Arial'><span class='OptionName'>" + num.ToString() + "</span></td>";
                        string str6 = str;
                        str = str6 + "<td align='center' style='font-family:Arial'><input type='radio' name='f" + IID.ToString() + "' id='f" + IID.ToString() + "_" + num.ToString() + "' value='" + num.ToString() + "' class='RadioObj'></td>";
                        num++;
                        num5 = 4;
                    }
                    else
                    {
                        num5 = 9;
                    }
                    goto Label_0002;

                case 12:
                case 0x13:
                    num5 = 2;
                    goto Label_0002;

                case 13:
                    if (num2 <= Convert.ToInt32(reader["OptionAmount"]))
                    {
                        string str7 = str;
                        str = str7 + "<input type='radio' name='f" + IID.ToString() + "' id='f" + IID.ToString() + "_" + num2.ToString() + "' value='" + num2.ToString() + "'>";
                        num2++;
                        num5 = 0x12;
                    }
                    else
                    {
                        num5 = 0x15;
                    }
                    goto Label_0002;

                case 14:
                    str = "<table><tr style='display:none'>" + str + "</tr><tr>" + str4 + "</tr><tr>" + str5 + "</tr></table>";
                    num5 = 0;
                    goto Label_0002;

                case 0x10:
                    if (num4 == 6)
                    {
                        num2 = 1;
                        num5 = 10;
                    }
                    else
                    {
                        num5 = 6;
                    }
                    goto Label_0002;

                case 0x11:
                    str5 = "";
                    num3 = 1;
                    num5 = 0x13;
                    goto Label_0002;

                case 0x15:
                    str = "<div style='display:none'>" + str + "</div><div><span>" + str5.Substring(0, str5.IndexOf('|')) + "</span><img src='../survey/images/adjustBar.png' id='Rule" + IID.ToString() + "'><span>" + str5.Substring(str5.IndexOf('|') + 1) + "</span></div><div   id='AdjustBt" + IID.ToString() + "' style='background:url(../survey/images/adjustPoint.png) no-repeat; width:17px; height:21px; left:100px;'></div>";
                    num5 = 20;
                    goto Label_0002;

                default:
                    goto Label_0063;
            }
            reader.Close();
            reader.Dispose();
            return ("<div id='Item" + IID.ToString() + "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>" + str2 + "</span></div><div class='ItemContent'>" + str3 + "</div>" + str + "</div>");
        }

        public void createItem_ListInput(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            //objComm.CommandText = "UPDATE ItemTable SET ItemName='" + arrFormsData[10] + "',OptionAmount," + arrFormsData[0x19] + ",ItemContent='" + arrFormsData[11] + "',PageNo=" + arrFormsData[1] + ",DataFormatCheck='" + this.createCheckStr("", arrFormsData, 0) + "' WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + intModifyIID.ToString();
            new CreateItem_Layer().CreateItemText_UpdateItemTable6(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), arrFormsData[0x19], arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_ListInput_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_ListInput_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
        }

        public void createItem_ListInput(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            int num2 = 1;
        Label_000D:
            switch (num2)
            {
                case 0:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num2 = 2;
                    goto Label_000D;

                case 2:
                    break;

                default:
                    if (sCheckStr == "")
                    {
                        num2 = 0;
                        goto Label_000D;
                    }
                    break;
            }
            //objComm.CommandText = "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,ItemContent,PageNo,Sort) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + arrFormsData[10] + "',13,'" + sCheckStr + "',0," + arrFormsData[0x19] + ",'" + arrFormsData[11] + "'," + arrFormsData[1] + "," + intSort.ToString() + ")";
            new CreateItem_Layer().CreateItemNumber_InsertItemTable4(SID.ToString(), UID.ToString(), arrFormsData[10], "13", sCheckStr, "0", arrFormsData[0x19], arrFormsData[11], arrFormsData[1], intSort.ToString());
            string str = this.getCurrIID( SID, UID).ToString();
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_ListInput_HTML(SID, UID, Convert.ToInt32(str), objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + str;
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_ListInput_HTML(SID, UID, Convert.ToInt32(str)), SID.ToString(), str);
        }

        public string createItem_ListInput_HTML(long SID, long UID, long IID)
        {
            string str;
            int num = 0; //∏≥≥ı÷µ
        Label_0029:
            str = "";
            string str2 = "";
            string str3 = "";
            //objComm.CommandText = "SELECT ItemName,ItemContent,OptionAmount FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            SqlDataReader reader = new CreateItem_Layer().GetItemTable3(SID.ToString(), UID.ToString(), IID);
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num2 = 5;
                    goto Label_0002;

                case 1:
                    if (num < Convert.ToInt32(reader["OptionAmount"]))
                    {
                        string str4 = str;
                        str = str4 + "<input type='text' name='f" + IID.ToString() + "_" + num.ToString() + "'  id='f" + IID.ToString() + "_" + num.ToString() + "' class='InputCSS'><BR>";
                        num++;
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 2:
                    break;

                case 3:
                case 4:
                    num2 = 1;
                    goto Label_0002;

                case 5:
                    str2 = reader["ItemName"].ToString();
                    str3 = reader["ItemContent"].ToString();
                    num = 0;
                    num2 = 4;
                    goto Label_0002;

                default:
                    goto Label_0029;
            }
            reader.Close();
            reader.Dispose();
            return ("<div id='Item" + IID.ToString() + "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>" + str2 + "</span></div><div class='ItemContent'>" + str3 + "</div>" + str + "</div>");
        }

        public void createItem_MatrixDropList(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
        Label_003B:;
            string[] strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            string[] strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            string[] strArray3 = arrFormsData[0x1f].Replace("\r\n", "\n").Split(new char[] { '\n' });
            int index = 0;
            //string[] strArray4 = new string[] { 
            //    "UPDATE  ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',OptionAmount=", strArray3.Length.ToString(), ",OrderModel='", arrFormsData[0x18], "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), 
            //    " AND IID=", intModifyIID.ToString()
            // };
            //objComm.CommandText = string.Concat(strArray4);
            new CreateItem_Layer().CreateItemText_UpdateItemTable3(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), strArray3.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM ItemTable WHERE ParentID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteItemTableByParentID(intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM OptionTable WHERE IID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteOptionTableByIID(intModifyIID.ToString());
            index = 0;
            int num3 = 10;
        Label_0002:
            switch (num3)
            {
                case 0:
                case 5:
                    num3 = 4;
                    goto Label_0002;

                case 1:
                    index = 0;
                    num3 = 3;
                    goto Label_0002;

                case 2:
                    if (index < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + intModifyIID.ToString() + "," + UID.ToString() + ",'" + strArray2[index] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), intModifyIID.ToString(), UID.ToString(), strArray2[index]);
                        index++;
                        num3 = 11;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0002;

                case 3:
                case 7:
                    num3 = 8;
                    goto Label_0002;

                case 4:
                    if (index < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[index] + "',3," + intModifyIID.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[index], "3", intModifyIID.ToString());
                        index++;
                        num3 = 0;
                    }
                    else
                    {
                        num3 = 6;
                    }
                    goto Label_0002;

                case 6:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixDropList_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixDropList_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
                    return;

                case 8:
                    if (index < strArray3.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName,IsMatrixRowColumn) VALUES(" + SID.ToString() + "," + intModifyIID.ToString() + "," + UID.ToString() + ",'" + strArray3[index] + "',1)";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable1(SID.ToString(), intModifyIID.ToString(), UID.ToString(), strArray3[index], "1");
                        index++;
                        num3 = 7;
                    }
                    else
                    {
                        num3 = 9;
                    }
                    goto Label_0002;

                case 9:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    index = 0;
                    num3 = 5;
                    goto Label_0002;

                case 10:
                case 11:
                    num3 = 2;
                    goto Label_0002;
            }
            goto Label_003B;
        }

        public void createItem_MatrixDropList(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            int num;
            string[] strArray = null; //∏≥≥ı÷µ
            string[] strArray2 = null; //∏≥≥ı÷µ
            string[] strArray3 = null; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num4;
            goto Label_0047;
        Label_0002:
            switch (num4)
            {
                case 0:
                    if (num < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + num2.ToString() + "," + UID.ToString() + ",'" + strArray2[num] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), num2.ToString(), UID.ToString(), strArray2[num]);
                        num++;
                        num4 = 1;
                    }
                    else
                    {
                        num4 = 12;
                    }
                    goto Label_0002;

                case 1:
                    goto Label_03AA;

                case 2:
                case 4:
                    num4 = 13;
                    goto Label_0002;

                case 3:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixDropList_HTML(SID, UID, num2, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + num2.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixDropList_HTML(SID, UID, num2), SID.ToString(), num2.ToString());
                    return;

                case 5:
                case 14:
                    num4 = 7;
                    goto Label_0002;

                case 6:
                    if (!(sCheckStr == ""))
                    {
                        goto Label_0112;
                    }
                    num4 = 8;
                    goto Label_0002;

                case 7:
                    if (num < strArray.Length)
                    {
                        goto Label_02AC;
                    }
                    num4 = 3;
                    goto Label_0002;

                case 8:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num4 = 10;
                    goto Label_0002;

                case 9:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_03AA;
                    }
                    goto Label_02AC;

                case 10:
                    goto Label_0112;

                case 11:
                    num = 0;
                    num4 = 5;
                    goto Label_0002;

                case 12:
                    num = 0;
                    num4 = 2;
                    goto Label_0002;

                case 13:
                    if (num < strArray3.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName,IsMatrixRowColumn) VALUES(" + SID.ToString() + "," + num2.ToString() + "," + UID.ToString() + ",'" + strArray3[num] + "',1)";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable1(SID.ToString(), num2.ToString(), UID.ToString(), strArray3[num],"1");
                        num++;
                        num4 = 4;
                    }
                    else
                    {
                        num4 = 11;
                    }
                    goto Label_0002;
            }
        Label_0047:
            num = 0;
            num4 = 6;
            goto Label_0002;
        Label_0112:;
            strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            strArray3 = arrFormsData[0x1f].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray4 = new string[] { 
            //    "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OrderModel,ItemContent,PageNo,Sort) VALUES(", SID.ToString(), ",", UID.ToString(), ",'", arrFormsData[10], "',", arrFormsData[0], ",'", sCheckStr, "',0,", strArray3.Length.ToString(), ",'", arrFormsData[0x18], "','", arrFormsData[11], 
            //    "',", arrFormsData[1], ",", intSort.ToString(), ")"
            // };
            //objComm.CommandText = string.Concat(strArray4);
            new CreateItem_Layer().CreateItemNumber_InsertItemTable6(SID.ToString(), UID.ToString(), arrFormsData[10], arrFormsData[0], sCheckStr, "0", strArray3.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], intSort.ToString());
            num2 = this.getCurrIID(SID, UID);
            num = 0;
            num4 = 9;
            goto Label_0002;
        Label_02AC:;
            //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num] + "',3," + num2.ToString() + ")";
            new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num], "3", num2.ToString());
            num++;
            num4 = 14;
            goto Label_0002;
        Label_03AA:
            num4 = 0;
            goto Label_0002;
        }

        public string createItem_MatrixDropList_HTML(long SID, long UID, long IID)
        {
            string str;
            string str2;
            int num;
            int num2;
            DataSet set;
            string str3;
            string str4;
            object obj5;
            int num3;
            goto Label_0057;
        Label_0002:
            switch (num3)
            {
                case 0:
                case 14:
                    num3 = 8;
                    goto Label_0002;

                case 1:
                    set.Dispose();
                    return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", set.Tables["ItemTable"].Rows[0]["ItemName"], "</span></div><div class='ItemContent'>", str4, "</div><table  border='0' cellpadding='2' cellspacing='1' bgcolor='#CCCCCC'>", str2, str, "</table></div>" });

                case 2:
                case 6:
                    num3 = 0x12;
                    goto Label_0002;

                case 3:
                case 13:
                    num3 = 10;
                    goto Label_0002;

                case 4:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_054F;
                    }
                    goto Label_03C9;

                case 5:
                    if (!(set.Tables["OptionTable"].Rows[num]["IsMatrixRowColumn"].ToString() == "False"))
                    {
                        object obj3 = str2;
                        str2 = string.Concat(new object[] { obj3, "<td  bgcolor='#EEEEEE'><span class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num3 = 0x10;
                    }
                    else
                    {
                        num3 = 9;
                    }
                    goto Label_0002;

                case 7:
                    goto Label_03C9;

                case 8:
                    if (num2 < set.Tables["OptionTable"].Rows.Count)
                    {
                        num3 = 0x11;
                    }
                    else
                    {
                        num3 = 12;
                    }
                    goto Label_0002;

                case 9:
                {
                    object obj2 = str3;
                    str3 = string.Concat(new object[] { obj2, "<option value=", set.Tables["OptionTable"].Rows[num]["OID"], ">", set.Tables["OptionTable"].Rows[num]["OptionName"], "</option>" });
                    num3 = 4;
                    goto Label_0002;
                }
                case 10:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        num3 = 5;
                    }
                    else
                    {
                        num3 = 15;
                    }
                    goto Label_0002;

                case 11:
                    goto Label_0219;

                case 12:
                    str = str + "</tr>";
                    num++;
                    num3 = 6;
                    goto Label_0002;

                case 15:
                    str2 = "<tr><td  bgcolor='#FFFFFF'></td>" + str2 + "</tr>";
                    num = 0;
                    num3 = 2;
                    goto Label_0002;

                case 0x10:
                    goto Label_054F;

                case 0x11:
                    if (!(set.Tables["OptionTable"].Rows[num2]["IsMatrixRowColumn"].ToString() == "True"))
                    {
                        goto Label_0219;
                    }
                    num3 = 7;
                    goto Label_0002;

                case 0x12:
                    if (num < set.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj4 = str;
                        str = string.Concat(new object[] { obj4, "<tr><td  bgcolor='#EEEEEE'  class='SubItemName'>", set.Tables["SubItemTable"].Rows[num]["ItemName"], "</td>" });
                        num2 = 0;
                        num3 = 0;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0002;
            }
        Label_0057:
            str = "";
            str2 = "";
            num = 0;
            num2 = 0;
            set = new DataSet();
            //objComm.CommandText = "SELECT ItemName,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(set, "ItemTable");
            DataTable ItemTable = new CreateItem_Layer().GetItemTable8(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";
            //objComm.CommandText = "SELECT IID,ItemName FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND ParentID=" + IID.ToString();
            //adapter.Fill(set, "SubItemTable");
            DataTable SubItemTable = new CreateItem_Layer().GetItemTable4(SID.ToString(), UID.ToString(), IID);
            SubItemTable.TableName="SubItemTable";
            //objComm.CommandText = "SELECT OID,OptionName,IsMatrixRowColumn FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(set, "OptionTable");
            DataTable OptionTable=new CreateItem_Layer().GetOptionTable1(SID.ToString(),UID.ToString(),IID);
            OptionTable.TableName = "OptionTable";
            set.Tables.Add(ItemTable);
            set.Tables.Add(SubItemTable);
            set.Tables.Add(OptionTable);

            str3 = "<option value=''>" + a[this.getLan(SID), 0].Split(new char[] { '|' })[0x11] + "</option>";
            str4 = set.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            num = 0;
            num3 = 3;
            goto Label_0002;
        Label_0219:
            num2++;
            num3 = 14;
            goto Label_0002;
        Label_03C9:
            obj5 = str;
            str = string.Concat(new object[] { obj5, "<td  bgcolor='#FFFFFF'><select name='f", set.Tables["SubItemTable"].Rows[num]["IID"], "_", set.Tables["OptionTable"].Rows[num2]["OID"].ToString(), "'  id='f", set.Tables["SubItemTable"].Rows[num]["IID"], "_", set.Tables["OptionTable"].Rows[num2]["OID"].ToString(), "' class='MatrixSelectCSS'>", str3, "</select></td>" });
            num3 = 11;
            goto Label_0002;
        Label_054F:
            num++;
            num3 = 13;
            goto Label_0002;
        }

        public void createItem_MatrixInput(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            int num2 = 0; //∏≥≥ı÷µ
        Label_002B:;
            string[] strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            string[] strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray3 = new string[] { 
            //    "UPDATE  ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',OptionAmount=", strArray2.Length.ToString(), ",OrderModel='", arrFormsData[0x18], "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), 
            //    " AND IID=", intModifyIID.ToString()
            // };
            //objComm.CommandText = string.Concat(strArray3);
            new CreateItem_Layer().CreateItemText_UpdateItemTable3(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), strArray2.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM ItemTable WHERE ParentID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteItemTableByParentID(intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM OptionTable WHERE IID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteOptionTableByIID(intModifyIID.ToString());
            int index = 0;
            int num4 = 6;
        Label_0002:
            switch (num4)
            {
                case 0:
                    num2 = 0;
                    num4 = 7;
                    goto Label_0002;

                case 1:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixInput_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixInput_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
                    return;

                case 2:
                case 7:
                    num4 = 5;
                    goto Label_0002;

                case 3:
                case 6:
                    num4 = 4;
                    goto Label_0002;

                case 4:
                    if (index < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + intModifyIID.ToString() + "," + UID.ToString() + ",'" + strArray2[index] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), intModifyIID.ToString(), UID.ToString(), strArray2[index]);
                        index++;
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num4 = 3;
                    }
                    else
                    {
                        num4 = 0;
                    }
                    goto Label_0002;

                case 5:
                    if (num2 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num2] + "',3," + intModifyIID.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num2], "3", intModifyIID.ToString());
                        num2++;
                        num4 = 2;
                    }
                    else
                    {
                        num4 = 1;
                    }
                    goto Label_0002;
            }
            goto Label_002B;
        }

        public void createItem_MatrixInput(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            string[] strArray = null; //∏≥≥ı÷µ
            string[] strArray2 = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num3 = 0; //∏≥≥ı÷µ
            int num5 = 10;
        Label_000D:
            switch (num5)
            {
                case 0:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num5 = 1;
                    goto Label_000D;

                case 1:
                    break;

                case 2:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixInput_HTML(SID, UID, num, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + num.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixInput_HTML(SID, UID, num), SID.ToString(), num.ToString());
                    return;

                case 3:
                case 4:
                    num5 = 8;
                    goto Label_000D;

                case 5:
                    if (num2 < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + num.ToString() + "," + UID.ToString() + ",'" + strArray2[num2] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), num.ToString(), UID.ToString(), strArray2[num2]);
                        num2++;
                        num5 = 9;
                    }
                    else
                    {
                        num5 = 6;
                    }
                    goto Label_000D;

                case 6:
                    num3 = 0;
                    num5 = 3;
                    goto Label_000D;

                case 7:
                case 9:
                    num5 = 5;
                    goto Label_000D;

                case 8:
                    if (num3 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num3] + "',3," + num.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num3], "3", num.ToString());
                        num3++;
                        num5 = 4;
                    }
                    else
                    {
                        num5 = 2;
                    }
                    goto Label_000D;

                default:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (sCheckStr == "")
                    {
                        num5 = 0;
                        goto Label_000D;
                    }
                    break;
            }
            strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray3 = new string[] { 
            //    "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OrderModel,ItemContent,PageNo,Sort) VALUES(", SID.ToString(), ",", UID.ToString(), ",'", arrFormsData[10], "',16,'", sCheckStr, "',0,", strArray2.Length.ToString(), ",'", arrFormsData[0x18], "','", arrFormsData[11], "',", arrFormsData[1], 
            //    ",", intSort.ToString(), ")"
            // };
            //objComm.CommandText = string.Concat(strArray3);
            new CreateItem_Layer().CreateItemNumber_InsertItemTable6(SID.ToString(), UID.ToString(), arrFormsData[10], "16", sCheckStr, "0", strArray2.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], intSort.ToString());
            num = this.getCurrIID( SID, UID);
            num2 = 0;
            num5 = 7;
            goto Label_000D;
        }

        public string createItem_MatrixInput_HTML(long SID, long UID, long IID)
        {
            string str;
        Label_003B:
            str = "";
            DataSet dataSet = new DataSet();
            //objComm.CommandText = "SELECT ItemName,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "ItemTable");
            DataTable ItemTable = new CreateItem_Layer().GetItemTable8(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName="ItemTable";
            //objComm.CommandText = "SELECT IID,ItemName FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND ParentID=" + IID.ToString();
            //adapter.Fill(dataSet, "SubItemTable");
            DataTable SubItemTable= new CreateItem_Layer().GetItemTable4(SID.ToString(), UID.ToString(), IID);
            SubItemTable.TableName = "SubItemTable";
            //objComm.CommandText = "SELECT OID,OptionName FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "OptionTable");
            DataTable OptionTable= new CreateItem_Layer().GetOptionTable(SID.ToString(), UID.ToString(), IID);
            OptionTable.TableName = "OptionTable";
            dataSet.Tables.Add(ItemTable);
            dataSet.Tables.Add(SubItemTable);
            dataSet.Tables.Add(OptionTable);
            string str2 = "";
            int num = 0;
            int num2 = 0;
            string str3 = dataSet.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            num = 0;
            int num3 = 11;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num2 < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj4 = str;
                        str = string.Concat(new object[] { obj4, "<td  bgcolor='#FFFFFF'><input type='text' class='MatrixInputCSS' name='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_", dataSet.Tables["OptionTable"].Rows[num2]["OID"], "'  id='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_", dataSet.Tables["OptionTable"].Rows[num2]["OID"], "' size='10' maxlength='50'></td>" });
                        num2++;
                        num3 = 9;
                    }
                    else
                    {
                        num3 = 2;
                    }
                    goto Label_0002;

                case 1:
                case 4:
                    num3 = 7;
                    goto Label_0002;

                case 2:
                    str = str + "</tr>";
                    num++;
                    num3 = 4;
                    goto Label_0002;

                case 3:
                case 9:
                    num3 = 0;
                    goto Label_0002;

                case 5:
                    str2 = "<tr><td  bgcolor='#FFFFFF'></td>" + str2 + "</tr>";
                    num = 0;
                    num3 = 1;
                    goto Label_0002;

                case 6:
                case 11:
                    num3 = 10;
                    goto Label_0002;

                case 7:
                    if (num < dataSet.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj3 = str;
                        str = string.Concat(new object[] { obj3, "<tr><td  bgcolor='#EEEEEE'  class='SubItemName'>", dataSet.Tables["SubItemTable"].Rows[num]["ItemName"], "</td>" });
                        num2 = 0;
                        num3 = 3;
                    }
                    else
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num3 = 8;
                    }
                    goto Label_0002;

                case 8:
                    dataSet.Dispose();
                    return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", dataSet.Tables["ItemTable"].Rows[0]["ItemName"], "</span></div><div class='ItemContent'>", str3, "</div><table  border='0' cellpadding='2' cellspacing='1' bgcolor='#CCCCCC'>", str2, str, "</table></div>" });

                case 10:
                    if (num < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj2 = str2;
                        str2 = string.Concat(new object[] { obj2, "<td  bgcolor='#EEEEEE'><span class='OptionName'>", dataSet.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num++;
                        num3 = 6;
                    }
                    else
                    {
                        num3 = 5;
                    }
                    goto Label_0002;
            }
            goto Label_003B;
        }

        public void createItem_MatrixMulti(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            string[] strArray;
            string[] strArray2;
            int num;
            int num2 = 0; //∏≥≥ı÷µ
            int num4;
            goto Label_002B;
        Label_0002:
            switch (num4)
            {
                case 0:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixMulti_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixMulti_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
                    return;

                case 1:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_02E7;
                    }
                    goto Label_024A;

                case 2:
                    goto Label_024A;

                case 3:
                    goto Label_02E7;

                case 4:
                case 6:
                    num4 = 5;
                    goto Label_0002;

                case 5:
                    if (num < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + intModifyIID.ToString() + "," + UID.ToString() + ",'" + strArray2[num] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), intModifyIID.ToString(), UID.ToString(), strArray2[num]);
                        num++;
                        num4 = 4;
                    }
                    else
                    {
                        num4 = 2;
                    }
                    goto Label_0002;

                case 7:
                    if (num2 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num2] + "',8," + intModifyIID.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num2], "8", intModifyIID.ToString());
                        num2++;
                        num4 = 1;
                    }
                    else
                    {
                        num4 = 0;
                    }
                    goto Label_0002;
            }
        Label_002B:;
            strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray3 = new string[] { 
            //    "UPDATE  ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',OptionAmount=", strArray2.Length.ToString(), ",OrderModel='", arrFormsData[0x18], "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), 
            //    " AND IID=", intModifyIID.ToString()
            // };
            //objComm.CommandText = string.Concat(strArray3);
            new CreateItem_Layer().CreateItemText_UpdateItemTable3(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), strArray2.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM ItemTable WHERE ParentID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteItemTableByParentID(intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM OptionTable WHERE IID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteOptionTableByIID(intModifyIID.ToString());

            num = 0;
            num4 = 6;
            goto Label_0002;
        Label_024A:
            num2 = 0;
            num4 = 3;
            goto Label_0002;
        Label_02E7:
            num4 = 7;
            goto Label_0002;
        }

        public void createItem_MatrixMulti(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            string[] strArray = null; //∏≥≥ı÷µ
            string[] strArray2 = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num3 = 0; //∏≥≥ı÷µ
            if ((1 != 0) && (0 != 0))
            {
            }
            int num5 = 8;
        Label_0013:
            switch (num5)
            {
                case 0:
                    break;

                case 1:
                    if (num2 < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + num.ToString() + "," + UID.ToString() + ",'" + strArray2[num2] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), num.ToString(), UID.ToString(), strArray2[num2]);
                        num2++;
                        num5 = 6;
                    }
                    else
                    {
                        num5 = 5;
                    }
                    goto Label_0013;

                case 2:
                case 6:
                    num5 = 1;
                    goto Label_0013;

                case 3:
                case 7:
                    num5 = 9;
                    goto Label_0013;

                case 4:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixMulti_HTML(SID, UID, num, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + num.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixMulti_HTML(SID, UID, num), SID.ToString(), num.ToString());
                    return;

                case 5:
                    num3 = 0;
                    num5 = 3;
                    goto Label_0013;

                case 9:
                    if (num3 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num3] + "',8," + num.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num3], "8", num.ToString());
                        num3++;
                        num5 = 7;
                    }
                    else
                    {
                        num5 = 4;
                    }
                    goto Label_0013;

                case 10:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num5 = 0;
                    goto Label_0013;

                default:
                    if (sCheckStr == "")
                    {
                        num5 = 10;
                        goto Label_0013;
                    }
                    break;
            }
            strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray3 = new string[] { 
            //    "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OrderModel,ItemContent,PageNo,Sort) VALUES(", SID.ToString(), ",", UID.ToString(), ",'", arrFormsData[10], "',15,'", sCheckStr, "',0,", strArray2.Length.ToString(), ",'", arrFormsData[0x18], "','", arrFormsData[11], "',", arrFormsData[1], 
            //    ",", intSort.ToString(), ")"
            // };
            //objComm.CommandText = string.Concat(strArray3);
            new CreateItem_Layer().CreateItemNumber_InsertItemTable6(SID.ToString(), UID.ToString(), arrFormsData[10], "15", sCheckStr, "0", strArray2.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], intSort.ToString());
            num = this.getCurrIID( SID, UID);
            num2 = 0;
            num5 = 2;
            goto Label_0013;
        }

        public string createItem_MatrixMulti_HTML(long SID, long UID, long IID)
        {
            string str;
        Label_0077:
            str = "";
            DataSet dataSet = new DataSet();
            //objComm.CommandText = "SELECT ItemName,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "ItemTable");
            DataTable ItemTable = new CreateItem_Layer().GetItemTable8(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";
            //objComm.CommandText = "SELECT IID,ItemName FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND ParentID=" + IID.ToString();
            //adapter.Fill(dataSet, "SubItemTable");
            DataTable SubItemTable = new CreateItem_Layer().GetItemTable4(SID.ToString(), UID.ToString(), IID);
            SubItemTable.TableName = "SubItemTable";
            //objComm.CommandText = "SELECT OID,OptionName FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "OptionTable");
            DataTable OptionTable = new CreateItem_Layer().GetOptionTable(SID.ToString(), UID.ToString(), IID);
            OptionTable.TableName = "OptionTable";
            dataSet.Tables.Add(ItemTable);
            dataSet.Tables.Add(SubItemTable);
            dataSet.Tables.Add(OptionTable);

            string str2 = "";
            int num = 0;
            int num2 = 0;
            string str3 = dataSet.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            int num3 = 0x13;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num2 < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj4 = str;
                        str = string.Concat(new object[] { obj4, "<td bgcolor='#FFFFFF'><input type='checkbox' class='CheckBoxCSS' name='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "'  id='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_", num2.ToString(), "' value='", dataSet.Tables["OptionTable"].Rows[num2]["OID"], "'></td>" });
                        num2++;
                        num3 = 2;
                    }
                    else
                    {
                        num3 = 9;
                    }
                    goto Label_0002;

                case 1:
                case 20:
                    num3 = 0x12;
                    goto Label_0002;

                case 2:
                case 0x15:
                    num3 = 0;
                    goto Label_0002;

                case 3:
                    num3 = 6;
                    goto Label_0002;

                case 4:
                    if (num2 < dataSet.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj7 = str;
                        str = string.Concat(new object[] { obj7, "<td  bgcolor='#FFFFFF'><input type='checkbox' class='CheckBoxCSS' name='f", dataSet.Tables["SubItemTable"].Rows[num2]["IID"], "'  id='f", dataSet.Tables["SubItemTable"].Rows[num2]["IID"], "_", num.ToString(), "' value='", dataSet.Tables["OptionTable"].Rows[num]["OID"], "'></td>" });
                        num2++;
                        num3 = 0x10;
                    }
                    else
                    {
                        num3 = 0x1a;
                    }
                    goto Label_0002;

                case 5:
                    if (num < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj2 = str2;
                        str2 = string.Concat(new object[] { obj2, "<td  bgcolor='#EEEEEE'><span class='OptionName'>", dataSet.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num++;
                        num3 = 0x17;
                    }
                    else
                    {
                        num3 = 8;
                    }
                    goto Label_0002;

                case 6:
                case 15:
                    dataSet.Dispose();

                    return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", dataSet.Tables["ItemTable"].Rows[0]["ItemName"], "</span></div><div class='ItemContent'>", str3, "</div><table  border='0' cellpadding='2' cellspacing='1' bgcolor='#CCCCCC'>", str2, str, "</table></div>" });

                case 7:
                case 0x10:
                    num3 = 4;
                    goto Label_0002;

                case 8:
                    str2 = "<tr><td  bgcolor='#FFFFFF'></td>" + str2 + "</tr>";
                    num = 0;
                    num3 = 0x11;
                    goto Label_0002;

                case 9:
                    str = str + "</tr>";
                    num++;
                    num3 = 13;
                    goto Label_0002;

                case 10:
                    num = 0;
                    num3 = 0x18;
                    goto Label_0002;

                case 11:
                    if (num < dataSet.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj3 = str;
                        str = string.Concat(new object[] { obj3, "<tr><td  bgcolor='#EEEEEE'  class='SubItemName'>", dataSet.Tables["SubItemTable"].Rows[num]["ItemName"], "</td>" });
                        num2 = 0;
                        num3 = 0x15;
                    }
                    else
                    {
                        num3 = 3;
                    }
                    goto Label_0002;

                case 12:
                    if (num < dataSet.Tables["SubItemTable"].Rows.Count)
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        object obj5 = str2;
                        str2 = string.Concat(new object[] { obj5, "<td  bgcolor='#EEEEEE'  class='SubItemName'>", dataSet.Tables["SubItemTable"].Rows[num]["ItemName"], "</td>" });
                        num++;
                        num3 = 14;
                    }
                    else
                    {
                        num3 = 0x19;
                    }
                    goto Label_0002;

                case 13:
                case 0x11:
                    num3 = 11;
                    goto Label_0002;

                case 14:
                case 0x16:
                    num3 = 12;
                    goto Label_0002;

                case 0x12:
                    if (num < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj6 = str;
                        str = string.Concat(new object[] { obj6, "<tr><td  bgcolor='#EEEEEE'><span class='OptionName'>", dataSet.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num2 = 0;
                        num3 = 7;
                    }
                    else
                    {
                        num3 = 15;
                    }
                    goto Label_0002;

                case 0x13:
                    if (!(dataSet.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() == "1"))
                    {
                        num = 0;
                        num3 = 0x16;
                    }
                    else
                    {
                        num3 = 10;
                    }
                    goto Label_0002;

                case 0x17:
                case 0x18:
                    num3 = 5;
                    goto Label_0002;

                case 0x19:
                    str2 = "<tr><td  bgcolor='#FFFFFF'></td>" + str2 + "</tr>";
                    num = 0;
                    num3 = 20;
                    goto Label_0002;

                case 0x1a:
                    str = str + "</tr>";
                    num++;
                    num3 = 1;
                    goto Label_0002;
            }
            goto Label_0077;
        }

        public void createItem_MatrixMultiInput(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            int num2 = 0; //∏≥≥ı÷µ
        Label_002B:;
            string[] strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            string[] strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray3 = new string[] { 
            //    "UPDATE  ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',OptionAmount=", strArray2.Length.ToString(), ",OrderModel='", arrFormsData[0x18], "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), 
            //    " AND IID=", intModifyIID.ToString()
            // };
            //objComm.CommandText = string.Concat(strArray3);
            new CreateItem_Layer().CreateItemText_UpdateItemTable3(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), strArray2.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM ItemTable WHERE ParentID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteItemTableByParentID(intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM OptionTable WHERE IID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteOptionTableByIID(intModifyIID.ToString());
 
            int index = 0;
            int num4 = 6;
        Label_0002:
            switch (num4)
            {
                case 0:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixMultiInput_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixMultiInput_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
                    return;

                case 1:
                case 6:
                    num4 = 3;
                    goto Label_0002;

                case 2:
                    num2 = 0;
                    num4 = 4;
                    goto Label_0002;

                case 3:
                    if (index < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + intModifyIID.ToString() + "," + UID.ToString() + ",'" + strArray2[index] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), intModifyIID.ToString(), UID.ToString(), strArray2[index]);
                        index++;
                        num4 = 1;
                    }
                    else
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num4 = 2;
                    }
                    goto Label_0002;

                case 4:
                case 5:
                    num4 = 7;
                    goto Label_0002;

                case 7:
                    if (num2 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num2] + "',8," + intModifyIID.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num2], "8", intModifyIID.ToString());
                        num2++;
                        num4 = 5;
                    }
                    else
                    {
                        num4 = 0;
                    }
                    goto Label_0002;
            }
            goto Label_002B;
        }

        public void createItem_MatrixMultiInput(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            string[] strArray = null; //∏≥≥ı÷µ
            string[] strArray2 = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num3 = 0; //∏≥≥ı÷µ
            int num5 = 7;
        Label_000D:
            switch (num5)
            {
                case 0:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num5 = 4;
                    goto Label_000D;

                case 1:
                    if (num3 < strArray.Length)
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num3] + "',8," + num.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num3], "8", num.ToString());
                        num3++;
                        num5 = 8;
                    }
                    else
                    {
                        num5 = 5;
                    }
                    goto Label_000D;

                case 2:
                    if (num2 < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + num.ToString() + "," + UID.ToString() + ",'" + strArray2[num2] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), num.ToString(), UID.ToString(), strArray2[num2]);
                        num2++;
                        num5 = 3;
                    }
                    else
                    {
                        num5 = 9;
                    }
                    goto Label_000D;

                case 3:
                case 10:
                    num5 = 2;
                    goto Label_000D;

                case 4:
                    break;

                case 5:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixMultiInput_HTML(SID, UID, num, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + num.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixMultiInput_HTML(SID, UID, num), SID.ToString(), num.ToString());
                    return;

                case 6:
                case 8:
                    num5 = 1;
                    goto Label_000D;

                case 9:
                    num3 = 0;
                    num5 = 6;
                    goto Label_000D;

                default:
                    if (sCheckStr == "")
                    {
                        num5 = 0;
                        goto Label_000D;
                    }
                    break;
            }
            strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray3 = new string[] { 
            //    "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OrderModel,ItemContent,PageNo,Sort) VALUES(", SID.ToString(), ",", UID.ToString(), ",'", arrFormsData[10], "',", arrFormsData[0], ",'", sCheckStr, "',0,", strArray2.Length.ToString(), ",'", arrFormsData[0x18], "','", arrFormsData[11], 
            //    "',", arrFormsData[1], ",", intSort.ToString(), ")"
            // };
            //objComm.CommandText = string.Concat(strArray3);
            new CreateItem_Layer().CreateItemNumber_InsertItemTable6(SID.ToString(), UID.ToString(), arrFormsData[10], arrFormsData[0], sCheckStr, "0", strArray2.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], intSort.ToString());
            num = this.getCurrIID(SID, UID);
            num2 = 0;
            num5 = 10;
            goto Label_000D;
        }

        public string createItem_MatrixMultiInput_HTML(long SID, long UID, long IID)
        {
            string str;
        Label_0077:
            str = "";
            DataSet dataSet = new DataSet();
            //objComm.CommandText = "SELECT ItemName,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "ItemTable");
            DataTable ItemTable = new CreateItem_Layer().GetItemTable8(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";

            //objComm.CommandText = "SELECT IID,ItemName FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND ParentID=" + IID.ToString();
            //adapter.Fill(dataSet, "SubItemTable");
            DataTable SubItemTable = new CreateItem_Layer().GetItemTable4(SID.ToString(), UID.ToString(), IID);
            SubItemTable.TableName = "SubItemTable";

            //objComm.CommandText = "SELECT OID,OptionName FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "OptionTable");
            DataTable OptionTable = new CreateItem_Layer().GetOptionTable(SID.ToString(), UID.ToString(), IID);
            OptionTable.TableName = "OptionTable";
            dataSet.Tables.Add(ItemTable);
            dataSet.Tables.Add(SubItemTable);
            dataSet.Tables.Add(OptionTable);

            string str2 = "";
            string str3 = "";
            int num = 0;
            int num2 = 0;
            string str4 = dataSet.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            int num3 = 0x15;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num2 < dataSet.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj9 = str;
                        str = string.Concat(new object[] { obj9, "<td  bgcolor='#FFFFFF'><input type='checkbox' class='CheckBoxCSS' name='f", dataSet.Tables["SubItemTable"].Rows[num2]["IID"], "'  id='f", dataSet.Tables["SubItemTable"].Rows[num2]["IID"], "_", num.ToString(), "' value='", dataSet.Tables["OptionTable"].Rows[num]["OID"], "'></td>" });
                        num2++;
                        num3 = 0x1a;
                    }
                    else
                    {
                        num3 = 8;
                    }
                    goto Label_0002;

                case 1:
                case 20:
                    num3 = 0x19;
                    goto Label_0002;

                case 2:
                case 10:
                    num3 = 0x12;
                    goto Label_0002;

                case 3:
                case 0x1a:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num3 = 0;
                    goto Label_0002;

                case 4:
                case 0x17:
                    dataSet.Dispose();
                    return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", dataSet.Tables["ItemTable"].Rows[0]["ItemName"], "</span></div><div class='ItemContent'>", str4, "</div><table  border='0' cellpadding='2' cellspacing='1' bgcolor='#CCCCCC'>", str2, str, str3, "</table></div>" });

                case 5:
                    num = 0;
                    num3 = 2;
                    goto Label_0002;

                case 6:
                case 7:
                    num3 = 15;
                    goto Label_0002;

                case 8:
                    str = str + "</tr>";
                    num++;
                    num3 = 20;
                    goto Label_0002;

                case 9:
                    str2 = "<tr><td  bgcolor='#FFFFFF'></td>" + str2 + "<td  bgcolor='#FFFFFF'></td></tr>";
                    num = 0;
                    num3 = 0x18;
                    goto Label_0002;

                case 11:
                case 0x18:
                    num3 = 0x10;
                    goto Label_0002;

                case 12:
                case 0x16:
                    num3 = 14;
                    goto Label_0002;

                case 13:
                {
                    object obj5 = str;
                    str = string.Concat(new object[] { obj5, "<td   bgcolor='#FFFFFF'><input  name='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_Input' id='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_Input'  class='OtherInputCSS'></td></tr>" });
                    num++;
                    num3 = 11;
                    goto Label_0002;
                }
                case 14:
                    if (num < dataSet.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj6 = str2;
                        str2 = string.Concat(new object[] { obj6, "<td  bgcolor='#EEEEEE'  class='SubItemName'>", dataSet.Tables["SubItemTable"].Rows[num]["ItemName"], "</td>" });
                        object obj7 = str3;
                        str3 = string.Concat(new object[] { obj7, "<td   bgcolor='#FFFFFF'><input    name='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_Input' id='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_Input'  class='OtherInputCSS'></td>" });
                        num++;
                        num3 = 0x16;
                    }
                    else
                    {
                        num3 = 0x13;
                    }
                    goto Label_0002;

                case 15:
                    if (num2 < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj4 = str;
                        str = string.Concat(new object[] { obj4, "<td bgcolor='#FFFFFF'><input type='checkbox' class='CheckBoxCSS' name='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "'  id='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_", num2.ToString(), "' value='", dataSet.Tables["OptionTable"].Rows[num2]["OID"], "'></td>" });
                        num2++;
                        num3 = 7;
                    }
                    else
                    {
                        num3 = 13;
                    }
                    goto Label_0002;

                case 0x10:
                    if (num < dataSet.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj3 = str;
                        str = string.Concat(new object[] { obj3, "<tr><td  bgcolor='#EEEEEE'  class='SubItemName'>", dataSet.Tables["SubItemTable"].Rows[num]["ItemName"], "</td>" });
                        num2 = 0;
                        num3 = 6;
                    }
                    else
                    {
                        num3 = 0x11;
                    }
                    goto Label_0002;

                case 0x11:
                    num3 = 4;
                    goto Label_0002;

                case 0x12:
                    if (num < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj2 = str2;
                        str2 = string.Concat(new object[] { obj2, "<td  bgcolor='#EEEEEE'><span class='OptionName'>", dataSet.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num++;
                        num3 = 10;
                    }
                    else
                    {
                        num3 = 9;
                    }
                    goto Label_0002;

                case 0x13:
                    str2 = "<tr><td  bgcolor='#FFFFFF'></td>" + str2 + "</tr>";
                    str3 = "<tr><td  bgcolor='#FFFFFF'></td>" + str3 + "</tr>";
                    num = 0;
                    num3 = 1;
                    goto Label_0002;

                case 0x15:
                    if (!(dataSet.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() == "1"))
                    {
                        num = 0;
                        num3 = 12;
                    }
                    else
                    {
                        num3 = 5;
                    }
                    goto Label_0002;

                case 0x19:
                    if (num < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj8 = str;
                        str = string.Concat(new object[] { obj8, "<tr><td  bgcolor='#EEEEEE'><span class='OptionName'>", dataSet.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num2 = 0;
                        num3 = 3;
                    }
                    else
                    {
                        num3 = 0x17;
                    }
                    goto Label_0002;
            }
            goto Label_0077;
        }

        public void createItem_MatrixSingle(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            int num2 = 0; //∏≥≥ı÷µ
        Label_002B:;
            string[] strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            string[] strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray3 = new string[] { 
            //    "UPDATE  ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',OptionAmount=", strArray2.Length.ToString(), ",OrderModel='", arrFormsData[0x18], "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), 
            //    " AND IID=", intModifyIID.ToString()
            // };
            //objComm.CommandText = string.Concat(strArray3);
            new CreateItem_Layer().CreateItemText_UpdateItemTable3(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), strArray2.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID.ToString());

            //objComm.CommandText = " DELETE FROM ItemTable WHERE ParentID=" + intModifyIID.ToString();

            new CreateItem_Layer().DeleteItemTableByParentID(intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM OptionTable WHERE IID=" + intModifyIID.ToString();

            new CreateItem_Layer().DeleteOptionTableByIID(intModifyIID.ToString());

            int index = 0;
            int num4 = 1;
        Label_0002:
            switch (num4)
            {
                case 0:
                    if (num2 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num2] + "',4," + intModifyIID.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num2], "4", intModifyIID.ToString());
                        num2++;
                        num4 = 7;
                    }
                    else
                    {
                        num4 = 2;
                    }
                    goto Label_0002;

                case 1:
                case 5:
                    num4 = 4;
                    goto Label_0002;

                case 2:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixSingle_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixSingle_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
                    return;

                case 3:
                case 7:
                    num4 = 0;
                    goto Label_0002;

                case 4:
                    if (index < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + intModifyIID.ToString() + "," + UID.ToString() + ",'" + strArray2[index] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), intModifyIID.ToString(), UID.ToString(), strArray2[index]);
                        index++;
                        num4 = 5;
                    }
                    else
                    {
                        num4 = 6;
                    }
                    goto Label_0002;

                case 6:
                    num2 = 0;
                    num4 = 3;
                    goto Label_0002;
            }
            goto Label_002B;
        }

        public void createItem_MatrixSingle(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            string[] strArray = null; //∏≥≥ı÷µ
            string[] strArray2 = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num3 = 0; //∏≥≥ı÷µ
            int num5 = 3;
        Label_000D:
            switch (num5)
            {
                case 0:
                case 10:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num5 = 9;
                    goto Label_000D;

                case 1:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num5 = 4;
                    goto Label_000D;

                case 2:
                    num3 = 0;
                    num5 = 0;
                    goto Label_000D;

                case 4:
                    break;

                case 5:
                case 6:
                    num5 = 7;
                    goto Label_000D;

                case 7:
                    if (num2 < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + num.ToString() + "," + UID.ToString() + ",'" + strArray2[num2] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), num.ToString(), UID.ToString(), strArray2[num2]);
                        num2++;
                        num5 = 6;
                    }
                    else
                    {
                        num5 = 2;
                    }
                    goto Label_000D;

                case 8:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixSingle_HTML(SID, UID, num, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + num.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixSingle_HTML(SID, UID, num), SID.ToString(), num.ToString());
                    return;

                case 9:
                    if (num3 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num3] + "',4," + num.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num3], "4", num.ToString());
                        num3++;
                        num5 = 10;
                    }
                    else
                    {
                        num5 = 8;
                    }
                    goto Label_000D;

                default:
                    if (sCheckStr == "")
                    {
                        num5 = 1;
                        goto Label_000D;
                    }
                    break;
            }
            strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray3 = new string[] { 
            //    "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OrderModel,ItemContent,PageNo,Sort) VALUES(", SID.ToString(), ",", UID.ToString(), ",'", arrFormsData[10], "',7,'", sCheckStr, "',0,", strArray2.Length.ToString(), ",'", arrFormsData[0x18], "','", arrFormsData[11], "',", arrFormsData[1], 
            //    ",", intSort.ToString(), ")"
            // };
            //objComm.CommandText = string.Concat(strArray3);
            new CreateItem_Layer().CreateItemNumber_InsertItemTable6(SID.ToString(), UID.ToString(), arrFormsData[10], "7", sCheckStr, "0", strArray2.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], intSort.ToString());
            num = this.getCurrIID(SID, UID);
            num2 = 0;
            num5 = 5;
            goto Label_000D;
        }

        public string createItem_MatrixSingle_HTML(long SID, long UID, long IID)
        {
            string str;
            DataSet set;
            string str2;
            int num;
            int num2;
            string str3;
            int num3;
            goto Label_0077;
        Label_0002:
            switch (num3)
            {
                case 0:
                case 14:
                    num3 = 0x13;
                    goto Label_0002;

                case 1:
                case 0x17:
                    num3 = 0x12;
                    goto Label_0002;

                case 2:
                case 11:
                    num3 = 6;
                    goto Label_0002;

                case 3:
                case 10:
                    num3 = 0x11;
                    goto Label_0002;

                case 4:
                case 0x18:
                    goto Label_04B6;

                case 5:
                    if (!(set.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() == "1"))
                    {
                        num = 0;
                        num3 = 2;
                    }
                    else
                    {
                        num3 = 8;
                    }
                    goto Label_0002;

                case 6:
                    if (num < set.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj5 = str2;
                        str2 = string.Concat(new object[] { obj5, "<td   bgcolor='#EEEEEE'  class='SubItemName'>", set.Tables["SubItemTable"].Rows[num]["ItemName"], "</td>" });
                        num++;
                        num3 = 11;
                    }
                    else
                    {
                        num3 = 0x10;
                    }
                    goto Label_0002;

                case 7:
                    num3 = 0x19;
                    goto Label_0002;

                case 8:
                    num = 0;
                    num3 = 0;
                    goto Label_0002;

                case 9:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_053A;
                    }
                    goto Label_04B6;

                case 12:
                    goto Label_053A;

                case 13:
                    if (num2 < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj4 = str;
                        str = string.Concat(new object[] { obj4, "<td  bgcolor='#FFFFFF'><input type='radio' class='RadioCSS' name='f", set.Tables["SubItemTable"].Rows[num]["IID"], "'  id='f", set.Tables["SubItemTable"].Rows[num]["IID"], "_", num2.ToString(), "'  value='", set.Tables["OptionTable"].Rows[num2]["OID"], "'></td>" });
                        num2++;
                        num3 = 12;
                    }
                    else
                    {
                        num3 = 0x16;
                    }
                    goto Label_0002;

                case 15:
                case 0x19:
                    set.Dispose();
                    return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", set.Tables["ItemTable"].Rows[0]["ItemName"], "</span></div><div class='ItemContent'>", str3, "</div><table  border='0' cellpadding='2' cellspacing='1' bgcolor='#CCCCCC'>", str2, str, "</table></div>" });

                case 0x10:
                    str2 = "<tr><td bgcolor='#FFFFFF'></td>" + str2 + "</tr>";
                    num = 0;
                    num3 = 3;
                    goto Label_0002;

                case 0x11:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj6 = str;
                        str = string.Concat(new object[] { obj6, "<tr><td bgcolor='#EEEEEE'><span class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num2 = 0;
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 15;
                    }
                    goto Label_0002;

                case 0x12:
                    if (num < set.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj3 = str;
                        str = string.Concat(new object[] { obj3, "<tr><td  bgcolor='#EEEEEE' class='SubItemName'>", set.Tables["SubItemTable"].Rows[num]["ItemName"], "</td>" });
                        num2 = 0;
                        num3 = 9;
                    }
                    else
                    {
                        num3 = 7;
                    }
                    goto Label_0002;

                case 0x13:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj2 = str2;
                        str2 = string.Concat(new object[] { obj2, "<td  bgcolor='#EEEEEE'><span class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num++;
                        num3 = 14;
                    }
                    else
                    {
                        num3 = 0x1a;
                    }
                    goto Label_0002;

                case 20:
                    if (num2 < set.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj7 = str;
                        str = string.Concat(new object[] { obj7, "<td bgcolor='#FFFFFF'><input type='radio' class='RadiObj' name='f", set.Tables["SubItemTable"].Rows[num2]["IID"], "'  id='f", set.Tables["SubItemTable"].Rows[num2]["IID"], "_", num.ToString(), "' value='", set.Tables["OptionTable"].Rows[num]["OID"], "'></td>" });
                        num2++;
                        num3 = 0x18;
                    }
                    else
                    {
                        num3 = 0x15;
                    }
                    goto Label_0002;

                case 0x15:
                    str = str + "</tr>";
                    num++;
                    num3 = 10;
                    goto Label_0002;

                case 0x16:
                    str = str + "</tr>";
                    num++;
                    num3 = 1;
                    goto Label_0002;

                case 0x1a:
                    str2 = "<tr><td  bgcolor='#FFFFFF'></td>" + str2 + "</tr>";
                    num = 0;
                    num3 = 0x17;
                    goto Label_0002;
            }
        Label_0077:
            str = "";
            set = new DataSet();
            //objComm.CommandText = "SELECT TOP 1 ItemName,OrderModel,ItemContent,StyleMode FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            DataTable ItemTable = new CreateItem_Layer().GetItemTable7(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";
            //objComm.CommandText = "SELECT IID,ItemName FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND ParentID=" + IID.ToString();
            //adapter.Fill(set, "SubItemTable");
            DataTable SubItemTable = new CreateItem_Layer().GetItemTable4(SID.ToString(), UID.ToString(), IID);
            SubItemTable.TableName = "SubItemTable";
            //objComm.CommandText = "SELECT OID,OptionName FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            DataTable OptionTable = new CreateItem_Layer().GetOptionTable(SID.ToString(), UID.ToString(), IID);
            OptionTable.TableName = "OptionTable";
            str2 = "";
            num = 0;
            num2 = 0;
            str3 = set.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            num3 = 5;
            goto Label_0002;
        Label_04B6:
            num3 = 20;
            goto Label_0002;
        Label_053A:
            num3 = 13;
            goto Label_0002;
        }

        public void createItem_MatrixSingleInput(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            int num2 = 0; //∏≥≥ı÷µ
        Label_002B:;
            string[] strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            string[] strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray3 = new string[] { 
            //    "UPDATE  ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',OptionAmount=", strArray2.Length.ToString(), ",OrderModel='", arrFormsData[0x18], "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), 
            //    " AND IID=", intModifyIID.ToString()
            // };
            //objComm.CommandText = string.Concat(strArray3);
            new CreateItem_Layer().CreateItemText_UpdateItemTable3(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), strArray2.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM ItemTable WHERE ParentID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteItemTableByParentID(intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM OptionTable WHERE IID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteOptionTableByIID(intModifyIID.ToString());
            int index = 0;
            int num4 = 3;
        Label_0002:
            switch (num4)
            {
                case 0:
                case 4:
                    num4 = 2;
                    goto Label_0002;

                case 1:
                    num2 = 0;
                    num4 = 0;
                    goto Label_0002;

                case 2:
                    if (num2 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num2] + "',4," + intModifyIID.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num2], "4", intModifyIID.ToString());
                        num2++;
                        num4 = 4;
                    }
                    else
                    {
                        num4 = 7;
                    }
                    goto Label_0002;

                case 3:
                case 6:
                    num4 = 5;
                    goto Label_0002;

                case 5:
                    if (index < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + intModifyIID.ToString() + "," + UID.ToString() + ",'" + strArray2[index] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), intModifyIID.ToString(), UID.ToString(), strArray2[index]);
                        index++;
                        num4 = 6;
                    }
                    else
                    {
                        num4 = 1;
                    }
                    goto Label_0002;

                case 7:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixSingleInput_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixSingleInput_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
                    return;
            }
            goto Label_002B;
        }

        public void createItem_MatrixSingleInput(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            string[] strArray = null; //∏≥≥ı÷µ
            string[] strArray2 = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num3 = 0; //∏≥≥ı÷µ
            int num5 = 7;
        Label_000D:
            switch (num5)
            {
                case 0:
                case 6:
                    num5 = 2;
                    goto Label_000D;

                case 1:
                    break;

                case 2:
                    if (num2 < strArray2.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + num.ToString() + "," + UID.ToString() + ",'" + strArray2[num2] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), num.ToString(), UID.ToString(), strArray2[num2]);
                        num2++;
                        num5 = 6;
                    }
                    else
                    {
                        num5 = 3;
                    }
                    goto Label_000D;

                case 3:
                    num3 = 0;
                    num5 = 9;
                    goto Label_000D;

                case 4:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num5 = 1;
                    goto Label_000D;

                case 5:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_MatrixSingleInput_HTML(SID, UID, num, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + num.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_MatrixSingleInput_HTML(SID, UID, num), SID.ToString(), num.ToString());
                    return;

                case 8:
                    if (num3 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num3] + "',4," + num.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num3],"4", num.ToString());
                        num3++;
                        num5 = 10;
                    }
                    else
                    {
                        num5 = 5;
                    }
                    goto Label_000D;

                case 9:
                case 10:
                    num5 = 8;
                    goto Label_000D;

                default:
                    if (sCheckStr == "")
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num5 = 4;
                        goto Label_000D;
                    }
                    break;
            }
            strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            strArray2 = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray3 = new string[] { 
            //    "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OrderModel,ItemContent,PageNo,Sort) VALUES(", SID.ToString(), ",", UID.ToString(), ",'", arrFormsData[10], "',", arrFormsData[0], ",'", sCheckStr, "',0,", strArray2.Length.ToString(), ",'", arrFormsData[0x18], "','", arrFormsData[11], 
            //    "',", arrFormsData[1], ",", intSort.ToString(), ")"
            // };
            //objComm.CommandText = string.Concat(strArray3);
            new CreateItem_Layer().CreateItemNumber_InsertItemTable6(SID.ToString(), UID.ToString(), arrFormsData[10], arrFormsData[0], sCheckStr, "0", strArray2.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], intSort.ToString());
            num = this.getCurrIID(SID, UID);
            num2 = 0;
            num5 = 0;
            goto Label_000D;
        }

        public string createItem_MatrixSingleInput_HTML(long SID, long UID, long IID)
        {
            string str;
        Label_0077:
            str = "";
            DataSet dataSet = new DataSet();
            //objComm.CommandText = "SELECT TOP 1 ItemName,OrderModel,ItemContent,StyleMode FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "ItemTable");
            //objComm.CommandText = "SELECT IID,ItemName FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND ParentID=" + IID.ToString();
            //adapter.Fill(dataSet, "SubItemTable");
            //objComm.CommandText = "SELECT OID,OptionName FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "OptionTable");

            //objComm.CommandText = "SELECT ItemName,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "ItemTable");
            DataTable ItemTable = new CreateItem_Layer().GetItemTable8(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";

            //objComm.CommandText = "SELECT IID,ItemName FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND ParentID=" + IID.ToString();
            //adapter.Fill(dataSet, "SubItemTable");
            DataTable SubItemTable = new CreateItem_Layer().GetItemTable4(SID.ToString(), UID.ToString(), IID);
            SubItemTable.TableName = "SubItemTable";

            //objComm.CommandText = "SELECT OID,OptionName FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "OptionTable");
            DataTable OptionTable = new CreateItem_Layer().GetOptionTable(SID.ToString(), UID.ToString(), IID);
            OptionTable.TableName = "OptionTable";
            dataSet.Tables.Add(ItemTable);
            dataSet.Tables.Add(SubItemTable);
            dataSet.Tables.Add(OptionTable);

            string str2 = "";
            string str3 = "";
            int num = 0;
            int num2 = 0;
            string str4 = dataSet.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            int num3 = 0x18;
        Label_0002:
            switch (num3)
            {
                case 0:
                    str = str + "</tr>";
                    num++;
                    num3 = 0x11;
                    goto Label_0002;

                case 1:
                    if (num < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj8 = str;
                        str = string.Concat(new object[] { obj8, "<tr><td bgcolor='#EEEEEE'><span class='OptionName'>", dataSet.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num2 = 0;
                        num3 = 12;
                    }
                    else
                    {
                        num3 = 5;
                    }
                    goto Label_0002;

                case 2:
                case 6:
                    num3 = 0x16;
                    goto Label_0002;

                case 3:
                    num = 0;
                    num3 = 15;
                    goto Label_0002;

                case 4:
                case 12:
                    num3 = 20;
                    goto Label_0002;

                case 5:
                case 0x15:
                    dataSet.Dispose();
                    return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", dataSet.Tables["ItemTable"].Rows[0]["ItemName"], "</span></div><div class='ItemContent'>", str4, "</div><table  border='0' cellpadding='2' cellspacing='1' bgcolor='#CCCCCC'>", str2, str, str3, "</table></div>" });

                case 7:
                case 0x10:
                    num3 = 0x19;
                    goto Label_0002;

                case 8:
                {
                    object obj5 = str;
                    str = string.Concat(new object[] { obj5, "<td  bgcolor='#FFFFFF'><input name='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_Input'  id='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_Input'  class='OtherInputCSS'></td></tr>" });
                    num++;
                    num3 = 6;
                    goto Label_0002;
                }
                case 9:
                    str2 = "<tr><td bgcolor='#FFFFFF'></td>" + str2 + "</tr>";
                    str3 = "<tr><td bgcolor='#FFFFFF'></td>" + str3 + "</tr>";
                    num = 0;
                    num3 = 0x17;
                    goto Label_0002;

                case 10:
                    if (num2 < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj4 = str;
                        str = string.Concat(new object[] { obj4, "<td  bgcolor='#FFFFFF'><input type='radio' class='RadioCSS' name='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "'  id='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_", num2.ToString(), "'  value='", dataSet.Tables["OptionTable"].Rows[num2]["OID"], "'></td>" });
                        num2++;
                        num3 = 0x12;
                    }
                    else
                    {
                        num3 = 8;
                    }
                    goto Label_0002;

                case 11:
                    str2 = "<tr><td  bgcolor='#FFFFFF'></td>" + str2 + "<td bgcolor='#FFFFFF'></td></tr>";
                    num = 0;
                    num3 = 2;
                    goto Label_0002;

                case 13:
                    if (num < dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        object obj2 = str2;
                        str2 = string.Concat(new object[] { obj2, "<td  bgcolor='#EEEEEE'><span class='OptionName'>", dataSet.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num++;
                        num3 = 0x13;
                    }
                    else
                    {
                        num3 = 11;
                    }
                    goto Label_0002;

                case 14:
                    num3 = 0x15;
                    goto Label_0002;

                case 15:
                case 0x13:
                    num3 = 13;
                    goto Label_0002;

                case 0x11:
                case 0x17:
                    num3 = 1;
                    goto Label_0002;

                case 0x12:
                case 0x1a:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num3 = 10;
                    goto Label_0002;

                case 20:
                    if (num2 < dataSet.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj9 = str;
                        str = string.Concat(new object[] { obj9, "<td bgcolor='#FFFFFF'><input type='radio' class='RadiObj' name='f", dataSet.Tables["SubItemTable"].Rows[num2]["IID"], "'  id='f", dataSet.Tables["SubItemTable"].Rows[num2]["IID"], "_", num.ToString(), "' value='", dataSet.Tables["OptionTable"].Rows[num]["OID"], "'></td>" });
                        num2++;
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 0;
                    }
                    goto Label_0002;

                case 0x16:
                    if (num < dataSet.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj3 = str;
                        str = string.Concat(new object[] { obj3, "<tr><td  bgcolor='#EEEEEE'  class='SubItemName'>", dataSet.Tables["SubItemTable"].Rows[num]["ItemName"], "</td>" });
                        num2 = 0;
                        num3 = 0x1a;
                    }
                    else
                    {
                        num3 = 14;
                    }
                    goto Label_0002;

                case 0x18:
                    if (!(dataSet.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() == "1"))
                    {
                        num = 0;
                        num3 = 7;
                    }
                    else
                    {
                        num3 = 3;
                    }
                    goto Label_0002;

                case 0x19:
                    if (num < dataSet.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj6 = str2;
                        str2 = string.Concat(new object[] { obj6, "<td   bgcolor='#EEEEEE'>", dataSet.Tables["SubItemTable"].Rows[num]["ItemName"], "</td>" });
                        object obj7 = str3;
                        str3 = string.Concat(new object[] { obj7, "<td   bgcolor='#FFFFFF'  class='SubItemName'><input name='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_Input'  id='f", dataSet.Tables["SubItemTable"].Rows[num]["IID"], "_Input' class='OtherInputCSS'></td>" });
                        num++;
                        num3 = 0x10;
                    }
                    else
                    {
                        num3 = 9;
                    }
                    goto Label_0002;
            }
            goto Label_0077;
        }

        public string createItem_Mulit_HTML(long SID, long UID, long IID)
        {
            string str;
            DataSet set;
            string str2;
            int num = 0; //∏≥≥ı÷µ
            string str4 = null; //∏≥≥ı÷µ
            int num3;
            goto Label_0077;
        Label_0002:
            switch (num3)
            {
                case 0:
                    str2 = "<br />";
                    num3 = 13;
                    goto Label_0002;

                case 1:
                    if (str4 == "8")
                    {
                        num = 0;
                        num3 = 5;
                    }
                    else
                    {
                        num3 = 11;
                    }
                    goto Label_0002;

                case 2:
                {
                    string str5 = str;
                    str = str5 + "<input name='f" + IID.ToString() + "_Input' type='text' id='f" + IID.ToString() + "_Input' size='30' maxlength='50' class='OtherInputCSS'  disabled='disabled'>";
                    num3 = 0x16;
                    goto Label_0002;
                }
                case 3:
                    num3 = 9;
                    goto Label_0002;

                case 4:
                case 10:
                    num3 = 0x10;
                    goto Label_0002;

                case 5:
                case 6:
                    num3 = 0x19;
                    goto Label_0002;

                case 7:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj3 = str;
                        str = string.Concat(new object[] { obj3, "<input type='checkbox' class='CheckBoxCSS' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'><label for='" + "f", IID.ToString(), "_", num.ToString(), "'" + " class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</label>", str2 });
                        num++;
                        num3 = 15;
                    }
                    else
                    {
                        num3 = 2;
                    }
                    goto Label_0002;

                case 8:
                case 15:
                    num3 = 7;
                    goto Label_0002;

                case 9:
                    if (str4 == "10")
                    {
                        str = "<select name='f" + IID.ToString() + "' id='f" + IID.ToString() + "'  size='" + set.Tables["OptionTable"].Rows.Count.ToString() + "' multiple='multiple' class='ListMulitCSS'>";
                        num = 0;
                        num3 = 10;
                    }
                    else
                    {
                        num3 = 12;
                    }
                    goto Label_0002;

                case 11:
                    num3 = 0x12;
                    goto Label_0002;

                case 12:
                    num3 = 0x1a;
                    goto Label_0002;

                case 13:
                    goto Label_02FD;

                case 14:
                    str = str + "</select>";
                    num3 = 0x13;
                    goto Label_0002;

                case 0x10:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj4 = str;
                        str = string.Concat(new object[] { obj4, " <option value='", set.Tables["OptionTable"].Rows[num]["OID"], "'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</option>" });
                        num++;
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 14;
                    }
                    goto Label_0002;

                case 0x11:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num3 = 1;
                    goto Label_0002;

                case 0x12:
                    if (str4 == "9")
                    {
                        num = 0;
                        num3 = 8;
                    }
                    else
                    {
                        num3 = 3;
                    }
                    goto Label_0002;

                case 0x13:
                case 0x16:
                case 0x17:
                case 0x1a:
                    goto Label_0767;

                case 20:
                    num3 = 0x17;
                    goto Label_0002;

                case 0x15:
                    if (!(set.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() == "0"))
                    {
                        goto Label_02FD;
                    }
                    num3 = 0;
                    goto Label_0002;

                case 0x18:
                    str4 = set.Tables["ItemTable"].Rows[0]["ItemType"].ToString();
                    if (str4 == null)
                    {
                        goto Label_0767;
                    }
                    num3 = 0x11;
                    goto Label_0002;

                case 0x19:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, "<input type='checkbox' class='CheckBoxCSS' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'><Label for='" + "f", IID.ToString(), "_", num.ToString(), "'" + " class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</Label>", str2 });
                        num++;
                        num3 = 6;
                    }
                    else
                    {
                        num3 = 20;
                    }
                    goto Label_0002;
            }
        Label_0077:
            str = "";
            set = new DataSet();
            //objComm.CommandText = "SELECT ItemName,ItemType,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(set, "ItemTable");
            DataTable ItemTable = new CreateItem_Layer().GetItemTable9(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";

            //objComm.CommandText = "SELECT OID,OptionName FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(set, "OptionTable");
            DataTable OptionTable = new CreateItem_Layer().GetOptionTable(SID.ToString(), UID.ToString(), IID);
            OptionTable.TableName = "OptionTable";
            set.Tables.Add(ItemTable);
            set.Tables.Add(OptionTable);
            str2 = "&nbsp;";
            string str3 = set.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            num3 = 0x15;
            goto Label_0002;
        Label_02FD:
            num = 0;
            num3 = 0x18;
            goto Label_0002;
        Label_0767:
            set.Dispose();
            return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class=\"SerialNumberSortStyle\"></span><span class='ItemName'>", set.Tables["ItemTable"].Rows[0]["ItemName"], "</span><span class=\"IsRequiredStyle\"></span></div><div class='ItemContent'>", str3, "</div>", str, "</div>" });
        }

        public string createItem_Mulit_HTML(long SID, long UID, long IID, bool blnIsImage)
        {
            string str;
            string str2;
            DataSet set;
            string str3;
            int num = 0; //∏≥≥ı÷µ
            string str5 = null; //∏≥≥ı÷µ
            object obj2;
            object obj3;
            int num3;
            goto Label_0092;
        Label_0005:
            switch (num3)
            {
                case 0:
                    goto Label_01F7;

                case 1:
                    str = str + "</select>";
                    num3 = 12;
                    goto Label_0005;

                case 2:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        str2 = this.getURL() + "UserData/u" + UID.ToString() + "/" + set.Tables["OptionTable"].Rows[num]["Image"].ToString();
                        num3 = 15;
                    }
                    else
                    {
                        num3 = 0x10;
                    }
                    goto Label_0005;

                case 3:
                    num3 = 14;
                    goto Label_0005;

                case 4:
                case 5:
                    num3 = 0x1f;
                    goto Label_0005;

                case 6:
                    str2 = "<img src ='" + str2 + "' alt='' style='border:#000 1px solid'>";
                    num3 = 0;
                    goto Label_0005;

                case 7:
                    if (str5 == "8")
                    {
                        num = 0;
                        num3 = 0x18;
                    }
                    else
                    {
                        num3 = 0x1a;
                    }
                    goto Label_0005;

                case 8:
                    num3 = 20;
                    goto Label_0005;

                case 9:
                    num3 = 0x17;
                    goto Label_0005;

                case 10:
                    str3 = "<br />";
                    num3 = 0x1c;
                    goto Label_0005;

                case 11:
                    num3 = 7;
                    goto Label_0005;

                case 12:
                case 20:
                case 0x16:
                case 0x17:
                    goto Label_0911;

                case 13:
                case 0x18:
                    num3 = 0x11;
                    goto Label_0005;

                case 14:
                    if (str5 == "10")
                    {
                        str = "<select name='f" + IID.ToString() + "' id='f" + IID.ToString() + "'  size='" + set.Tables["OptionTable"].Rows.Count.ToString() + "' multiple='multiple'>";
                        num = 0;
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 8;
                    }
                    goto Label_0005;

                case 15:
                    if (!(str2 != ""))
                    {
                        goto Label_01F7;
                    }
                    num3 = 6;
                    goto Label_0005;

                case 0x10:
                {
                    string str6 = str;
                    str = str6 + "<input name='f" + IID.ToString() + "_Input' type='text' id='f" + IID.ToString() + "_Input' size='30' maxlength='50'  class='OtherInputCSS'  disabled='disabled'>";
                    num3 = 0x16;
                    goto Label_0005;
                }
                case 0x11:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        str2 = this.getURL() + "UserData/u" + UID.ToString() + "/" + set.Tables["OptionTable"].Rows[num]["Image"].ToString();
                        num3 = 0x13;
                    }
                    else
                    {
                        num3 = 9;
                    }
                    goto Label_0005;

                case 0x12:
                    str5 = set.Tables["ItemTable"].Rows[0]["ItemType"].ToString();
                    if (str5 == null)
                    {
                        goto Label_0911;
                    }
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num3 = 11;
                    goto Label_0005;

                case 0x13:
                    if (!(str2 != ""))
                    {
                        goto Label_071D;
                    }
                    num3 = 0x20;
                    goto Label_0005;

                case 0x15:
                case 30:
                    num3 = 2;
                    goto Label_0005;

                case 0x19:
                    goto Label_071D;

                case 0x1a:
                    num3 = 0x1d;
                    goto Label_0005;

                case 0x1b:
                    if (!(set.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() == "1"))
                    {
                        goto Label_08B5;
                    }
                    num3 = 10;
                    goto Label_0005;

                case 0x1c:
                    goto Label_08B5;

                case 0x1d:
                    if (str5 == "9")
                    {
                        num = 0;
                        num3 = 30;
                    }
                    else
                    {
                        num3 = 3;
                    }
                    goto Label_0005;

                case 0x1f:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj4 = str;
                        str = string.Concat(new object[] { obj4, " <option value='", set.Tables["OptionTable"].Rows[num]["OID"], "'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</option>" });
                        num++;
                        num3 = 5;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0005;

                case 0x20:
                    str2 = "<img src ='" + str2 + "' alt='' style='border:#000 1px solid'>";
                    num3 = 0x19;
                    goto Label_0005;
            }
        Label_0092:
            str = "";
            str2 = "";
            set = new DataSet();
            //objComm.CommandText = "SELECT ItemName,ItemType,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(set, "ItemTable");
            DataTable ItemTable = new CreateItem_Layer().GetItemTable9(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";

            //objComm.CommandText = "SELECT OID,OptionName,Image FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(set, "OptionTable");
            DataTable OptionTable = new CreateItem_Layer().GetOptionTable2(SID.ToString(), UID.ToString(), IID);
            OptionTable.TableName = "OptionTable";
            set.Tables.Add(ItemTable);
            set.Tables.Add(OptionTable);

            str3 = "&nbsp;";
            string str4 = set.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            num3 = 0x1b;
            goto Label_0005;
        Label_01F7:
            obj3 = str;
            str = string.Concat(new object[] { obj3, "<input type='checkbox' class='CheckBoxCSS' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'><label for='" + "f", IID.ToString(), "_", num.ToString(), "'" + " class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</label>", str2, str3 });
            num++;
            num3 = 0x15;
            goto Label_0005;
        Label_071D:
            obj2 = str;
            str = string.Concat(new object[] { obj2, "<input type='checkbox' class='CheckBoxCSS' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'><label for='" + "f", IID.ToString(), "_", num.ToString(), "'" + " class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</label>", str2, str3 });
            num++;
            num3 = 13;
            goto Label_0005;
        Label_08B5:
            num = 0;
            num3 = 0x12;
            goto Label_0005;
        Label_0911:
            set.Dispose();
            return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class=\"SerialNumberSortStyle\"></span><span class='ItemName'>", set.Tables["ItemTable"].Rows[0]["ItemName"], "</span><span class=\"IsRequiredStyle\"></span></div><br /><div class='ItemContent'>", str4, "</div>", str, "</div>" });
        }

        public void createItem_Multi(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            string[] strArray;
            int num = 0; //∏≥≥ı÷µ
            int num3;
            goto Label_002D;
        Label_0002:
            if ((1 != 0) && (0 != 0))
            {
            }
            switch (num3)
            {
                case 0:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Mulit_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Mulit_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
                    return;

                case 1:
                    goto Label_0200;

                case 2:
                    if (num < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + intModifyIID.ToString() + "," + UID.ToString() + ",'" + strArray[num] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), intModifyIID.ToString(), UID.ToString(), strArray[num]);
                        num++;
                        num3 = 4;
                    }
                    else
                    {
                        num3 = 0;
                    }
                    goto Label_0002;

                case 3:
                    this.createItem_Text(SID, UID, intModifyIID);
                    num3 = 1;
                    goto Label_0002;

                case 4:
                case 5:
                    num3 = 2;
                    goto Label_0002;

                case 6:
                    if (!(arrFormsData[0] == "9"))
                    {
                        goto Label_0200;
                    }
                    num3 = 3;
                    goto Label_0002;
            }
        Label_002D:;
            strArray = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray2 = new string[] { "UPDATE  ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',OptionAmount=", strArray.Length.ToString(), ",OrderModel='", arrFormsData[0x18], "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND IID=", intModifyIID.ToString() };
            //objComm.CommandText = string.Concat(strArray2);
            new CreateItem_Layer().CreateItemText_UpdateItemTable4(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), strArray.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], SID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM ItemTable WHERE ParentID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteItemTableByParentID(intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM OptionTable WHERE IID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteOptionTableByIID(intModifyIID.ToString());
            num3 = 6;
            goto Label_0002;
        Label_0200:
            num = 0;
            num3 = 5;
            goto Label_0002;
        }

        public void createItem_Multi(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            string[] strArray = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num4 = 9;
        Label_000D:
            switch (num4)
            {
                case 0:
                    if (!(arrFormsData[0] == "9"))
                    {
                        goto Label_02B4;
                    }
                    num4 = 2;
                    goto Label_000D;

                case 1:
                    if (num2 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + num.ToString() + "," + UID.ToString() + ",'" + strArray[num2] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), num.ToString(), UID.ToString(), strArray[num2]);
                        num2++;
                        num4 = 7;
                    }
                    else
                    {
                        num4 = 4;
                    }
                    goto Label_000D;

                case 2:
                    if ((1 == 0) || (0 == 0))
                    {
                        this.createItem_Text(SID, UID, num);
                        num4 = 5;
                        goto Label_000D;
                    }
                    goto Label_02B4;

                case 3:
                    break;

                case 4:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Mulit_HTML(SID, UID, num, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + num.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Mulit_HTML(SID, UID, num), SID.ToString(), num.ToString());
                    return;

                case 5:
                    goto Label_02B4;

                case 6:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num4 = 3;
                    goto Label_000D;

                case 7:
                case 8:
                    num4 = 1;
                    goto Label_000D;

                default:
                    if (sCheckStr == "")
                    {
                        num4 = 6;
                        goto Label_000D;
                    }
                    break;
            }
            strArray = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray2 = new string[] { 
            //    "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OptionImgModel,OrderModel,ItemContent,PageNo,Sort) VALUES(", SID.ToString(), ",", UID.ToString(), ",'", arrFormsData[10], "',", arrFormsData[0], ",'", sCheckStr, "',0,", strArray.Length.ToString(), ",'", arrFormsData[0x17], "','", arrFormsData[0x18], 
            //    "','", arrFormsData[11], "',", arrFormsData[1], ",", intSort.ToString(), ")"
            // };
            //objComm.CommandText = string.Concat(strArray2);
            new CreateItem_Layer().CreateItemNumber_InsertItemTable1(SID.ToString(), UID.ToString(), arrFormsData[10], arrFormsData[0], sCheckStr, "0", strArray.Length.ToString(), arrFormsData[0x17], arrFormsData[0x18], arrFormsData[11], arrFormsData[1], intSort.ToString());
            num = this.getCurrIID(SID, UID);
            num4 = 0;
            goto Label_000D;
        Label_02B4:
            num2 = 0;
            num4 = 8;
            goto Label_000D;
        }

        public void createItem_Number(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            //objComm.CommandText = "UPDATE ItemTable SET ItemName='" + arrFormsData[10] + "',DataFormatCheck='" + this.createCheckStr("", arrFormsData, 0) + "',ItemContent='" + arrFormsData[11] + "',PageNo=" + arrFormsData[1] + " WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
            new CreateItem_Layer().CreateItemNumber_UpdateItemTable(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), arrFormsData[11], arrFormsData[1], SID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Number_HTML(SID, UID, intModifyIID).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Number_HTML(SID, UID, intModifyIID).Replace("'", "''"), SID.ToString(), intModifyIID.ToString());
        }

        public void createItem_Number(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            int num2 = 1;
        Label_000D:
            switch (num2)
            {
                case 0:
                    break;

                case 2:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num2 = 0;
                    goto Label_000D;

                default:
                    if (sCheckStr == "")
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num2 = 2;
                        goto Label_000D;
                    }
                    break;
            }
            //objComm.CommandText = "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,ItemContent,PageNo,Sort) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + arrFormsData[10] + "',2,'" + sCheckStr + "',0,'" + arrFormsData[11] + "'," + arrFormsData[1] + "," + intSort.ToString() + ")";
            new CreateItem_Layer().CreateItemNumber_InsertItemTable(SID.ToString(), UID.ToString(), arrFormsData[10], "2", sCheckStr, "0", arrFormsData[11], arrFormsData[1], intSort.ToString());
            string str = this.getCurrIID(SID, UID).ToString();
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Number_HTML(SID, UID, Convert.ToInt32(str)).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + str;
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Number_HTML(SID, UID, Convert.ToInt32(str)), SID.ToString(), str);
        }

        public string createItem_Number_HTML(long SID, long UID, long IID)
        {
            string str;
        Label_0017:
            str = "";
            //objComm.CommandText = "SELECT ItemName,OptionImgModel,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            SqlDataReader reader = new CreateItem_Layer().CreateItemNumberHTML_SelectItemTable(SID.ToString(), UID.ToString(), IID.ToString());
            int num = 2;
        Label_0002:
            switch (num)
            {
                case 0:
                {
                    string str2 = reader["ItemContent"].ToString();
                    str = string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", reader["ItemName"], "</span></div><div class='ItemContent'>", str2, "</div><input name='f", IID.ToString(), "' type=text id='f", IID.ToString(), "' size='50' maxlength='10' class='NumInputCSS'></div>" });
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num = 1;
                    goto Label_0002;
                }
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

        public void createItem_Percent(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
        Label_001B:
            if ((1 != 0) && (0 != 0))
            {
            }
            string[] strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray2 = new string[] { 
            //    "UPDATE  ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',OptionAmount=", strArray.Length.ToString(), ",OrderModel='", arrFormsData[0x18], "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), 
            //    " AND IID=", intModifyIID.ToString()
            // };
            //objComm.CommandText = string.Concat(strArray2);
            new CreateItem_Layer().CreateItemText_UpdateItemTable3(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), strArray.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM ItemTable WHERE ParentID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteItemTableByParentID(intModifyIID.ToString());
            int index = 0;
            int num3 = 0;
        Label_0002:
            switch (num3)
            {
                case 0:
                case 2:
                    num3 = 3;
                    goto Label_0002;

                case 1:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Percent_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Percent_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString()); 
                    return;

                case 3:
                    if (index < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[index] + "',2," + intModifyIID.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[index], "2", intModifyIID.ToString());
                        index++;
                        num3 = 2;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0002;
            }
            goto Label_001B;
        }

        public void createItem_Percent(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            string[] strArray = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num4 = 5;
        Label_000D:
            switch (num4)
            {
                case 0:
                    goto Label_0241;

                case 1:
                    break;

                case 2:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num4 = 1;
                    goto Label_000D;

                case 3:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_0241;
                    }
                    break;

                case 4:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Percent_HTML(SID, UID, num, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + num.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Percent_HTML(SID, UID, num), SID.ToString(), num.ToString());
                    return;

                case 6:
                    if (num2 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO ItemTable(SID,UID,ItemName,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + strArray[num2] + "',2," + num.ToString() + ")";
                        new CreateItem_Layer().CreateItemNumber_InsertItemTable2(SID.ToString(), UID.ToString(), strArray[num2], "2", num.ToString());
                        num2++;
                        num4 = 3;
                    }
                    else
                    {
                        num4 = 4;
                    }
                    goto Label_000D;

                default:
                    if (sCheckStr == "")
                    {
                        num4 = 2;
                        goto Label_000D;
                    }
                    break;
            }
            strArray = arrFormsData[20].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray2 = new string[] { 
            //    "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OrderModel,ItemContent,PageNo,Sort) VALUES(", SID.ToString(), ",", UID.ToString(), ",'", arrFormsData[10], "',", arrFormsData[0], ",'", sCheckStr, "',0,", strArray.Length.ToString(), ",'", arrFormsData[0x18], "','", arrFormsData[11], 
            //    "',", arrFormsData[1], ",", intSort.ToString(), ")"
            // };
            //objComm.CommandText = string.Concat(strArray2);
            new CreateItem_Layer().CreateItemNumber_InsertItemTable6(SID.ToString(), UID.ToString(), arrFormsData[10], arrFormsData[0], sCheckStr, "0", strArray.Length.ToString(), arrFormsData[0x18], arrFormsData[11], arrFormsData[1], intSort.ToString());
            num = this.getCurrIID(SID, UID);

            num2 = 0;
            num4 = 0;
            goto Label_000D;
        Label_0241:
            num4 = 6;
            goto Label_000D;
        }

        public string createItem_Percent_HTML(long SID, long UID, long IID)
        {
            string str;
            OleDbDataAdapter adapter;
            DataSet set;
            int num;
            string str2;
            int num2;
            if ((1 == 0) || (0 == 0))
            {
                goto Label_001F;
            }
        Label_0006:
            switch (num2)
            {
                case 0:
                    str = "<table  border='0' cellpadding='0' cellspacing='1'><tr><td  bgcolor='#EEEEEE' colspan='2'></td></tr>" + str + "</table>";
                    set.Dispose();
                    return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", set.Tables["ItemTable"].Rows[0]["ItemName"], "</span><div class='ItemContent'>", str2, "</div>", str, "</div></div>" });

                case 1:
                case 3:
                    num2 = 2;
                    goto Label_0006;

                case 2:
                    if (num < set.Tables["SubItemTable"].Rows.Count)
                    {
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, "<tr><td  bgcolor='#FFFFFF'>", set.Tables["SubItemTable"].Rows[num]["ItemName"], "</td><td   bgcolor='#FFFFFF'  class='SubItemName'><input    type='input' name='f", set.Tables["SubItemTable"].Rows[num]["IID"], "' id='f", set.Tables["SubItemTable"].Rows[num]["IID"], "' size='10' maxlength='5' class='PercentInputCSS' /></td></tr>" });
                        num++;
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 0;
                    }
                    goto Label_0006;
            }
        Label_001F:
            str = "";
            set = new DataSet();
            //objComm.CommandText = "SELECT TOP 1 ItemName,OrderModel,ItemContent,StyleMode FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(set, "ItemTable");
            DataTable ItemTable = new CreateItem_Layer().GetItemTable10(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";
            //objComm.CommandText = "SELECT IID,ItemName FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND ParentID=" + IID.ToString();
            //adapter.Fill(set, "SubItemTable");
            DataTable SubItemTable = new CreateItem_Layer().GetItemTable4(SID.ToString(), UID.ToString(), IID);
            SubItemTable.TableName = "SubItemTable";
            set.Tables.Add(ItemTable);
            set.Tables.Add(SubItemTable);
            num = 0;
            str2 = set.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            num = 0;
            num2 = 3;
            goto Label_0006;
        }

        public void createItem_Single(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            string[] strArray;
            int num = 0; //∏≥≥ı÷µ
            int num3;
            goto Label_0027;
        Label_0002:
            switch (num3)
            {
                case 0:
                case 5:
                    num3 = 2;
                    goto Label_0002;

                case 1:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Single_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Single_HTML(SID, UID, intModifyIID), SID.ToString(), intModifyIID.ToString());
                    return;

                case 2:
                    if (num < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + intModifyIID.ToString() + "," + UID.ToString() + ",'" + strArray[num] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), intModifyIID.ToString(), UID.ToString(), strArray[num]);
                        num++;
                        num3 = 5;
                    }
                    else
                    {
                        num3 = 1;
                    }
                    goto Label_0002;

                case 3:
                    if (!(arrFormsData[0] == "5"))
                    {
                        goto Label_0217;
                    }
                    num3 = 4;
                    goto Label_0002;

                case 4:
                    this.createItem_Text(SID, UID, intModifyIID);
                    num3 = 6;
                    goto Label_0002;

                case 6:
                    goto Label_0217;
            }
        Label_0027:;
            strArray = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray2 = new string[] { 
            //    "UPDATE  ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',OptionAmount=", strArray.Length.ToString(), ",OptionImgModel='", arrFormsData[0x17], "',OrderModel='", arrFormsData[0x18], "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), 
            //    " AND IID=", intModifyIID.ToString()
            // };
            //objComm.CommandText = string.Concat(strArray2);
            
            new CreateItem_Layer().CreateItemText_UpdateItemTable2(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), strArray.Length.ToString(), arrFormsData[0x17], arrFormsData[0x18], arrFormsData[11],arrFormsData[1], SID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM ItemTable WHERE ParentID=" + intModifyIID.ToString();
           
            new CreateItem_Layer().DeleteItemTableByParentID(intModifyIID.ToString());
            
            //objComm.CommandText = " DELETE FROM OptionTable WHERE IID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteOptionTableByIID(intModifyIID.ToString());
            //objComm.CommandText = "";
            num3 = 3;
            goto Label_0002;
        Label_0217:
            num = 0;
            num3 = 0;
            goto Label_0002;
        }

        public void createItem_Single(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            string[] strArray = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num4 = 6;
        Label_000D:
            switch (num4)
            {
                case 0:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Single_HTML(SID, UID, num, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + num.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Single_HTML(SID, UID, num), SID.ToString(), num.ToString());
                    return;

                case 1:
                case 7:
                    num4 = 3;
                    goto Label_000D;

                case 2:
                    if (!(arrFormsData[0] == "5"))
                    {
                        goto Label_02BF;
                    }
                    num4 = 5;
                    goto Label_000D;

                case 3:
                    if (num2 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + num.ToString() + "," + UID.ToString() + ",'" + strArray[num2] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), num.ToString(), UID.ToString(), strArray[num2]);
                        num2++;
                        num4 = 7;
                    }
                    else
                    {
                        num4 = 0;
                    }
                    goto Label_000D;

                case 4:
                    goto Label_02BF;

                case 5:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    this.createItem_Text(SID, UID, num);
                    num4 = 4;
                    goto Label_000D;

                case 8:
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num4 = 9;
                    goto Label_000D;

                case 9:
                    break;

                default:
                    if (sCheckStr == "")
                    {
                        num4 = 8;
                        goto Label_000D;
                    }
                    break;
            }
            strArray = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray2 = new string[] { 
            //    "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OptionImgModel,OrderModel,ItemContent,PageNo,Sort) VALUES(", SID.ToString(), ",", UID.ToString(), ",'", arrFormsData[10], "',", arrFormsData[0], ",'", sCheckStr, "',0,", strArray.Length.ToString(), ",'", arrFormsData[0x17], "','", arrFormsData[0x18], 
            //    "','", arrFormsData[11], "',", arrFormsData[1], ",", intSort.ToString(), ")"
            // };
            //objComm.CommandText = string.Concat(strArray2);
            new CreateItem_Layer().CreateItemNumber_InsertItemTable1(SID.ToString(), UID.ToString(), arrFormsData[10], arrFormsData[0], sCheckStr, "0", strArray.Length.ToString(), arrFormsData[0x17], arrFormsData[0x18], arrFormsData[11], arrFormsData[1], intSort.ToString());
            num = this.getCurrIID(SID, UID);
            num4 = 2;
            goto Label_000D;
        Label_02BF:
            num2 = 0;
            num4 = 1;
            goto Label_000D;
        }

        public string createItem_Single_HTML(long SID, long UID, long IID)
        {
            string str;
            string str2;
            string str3;
            DataSet set;
            string str4;
            int num = 0; //∏≥≥ı÷µ
            string str5 = null; //∏≥≥ı÷µ
            string str6 = null; //∏≥≥ı÷µ
            string str7 = null; //∏≥≥ı÷µ
            int num2;
            goto Label_00B6;
        Label_0005:
            switch (num2)
            {
                case 0:
                    str = str + "</select>";
                    num2 = 0x1b;
                    goto Label_0005;

                case 1:
                case 11:
                    num2 = 9;
                    goto Label_0005;

                case 2:
                case 4:
                    num2 = 0x16;
                    goto Label_0005;

                case 3:
                    num2 = 0x23;
                    goto Label_0005;

                case 5:
                    num2 = 14;
                    goto Label_0005;

                case 6:
                    if (str7 == "0")
                    {
                        num = 0;
                        num2 = 11;
                    }
                    else
                    {
                        num2 = 5;
                    }
                    goto Label_0005;

                case 7:
                case 10:
                    num2 = 0x15;
                    goto Label_0005;

                case 8:
                case 20:
                    num2 = 12;
                    goto Label_0005;

                case 9:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj3 = str;
                        str = string.Concat(new object[] { obj3, "<input type='radio' class='RadioCSS' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'><label for='" + "f", IID.ToString(), "_", num.ToString(), "'" + " class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</label>", str4 });
                        num++;
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 40;
                    }
                    goto Label_0005;

                case 12:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj7 = str;
                        str = string.Concat(new object[] { obj7, "<input type='radio' style='display:none' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'>" });
                        object obj8 = str2;
                        str2 = string.Concat(new object[] { obj8, "<tr><td><span class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td><td><span class=OptionImgGray", set.Tables["ItemTable"].Rows[0]["OptionImgModel"].ToString(), "  id='Img_f", IID.ToString(), "_", num.ToString(), "'></span></td></tr>" });
                        num++;
                        num2 = 20;
                    }
                    else
                    {
                        num2 = 0x25;
                    }
                    goto Label_0005;

                case 13:
                case 15:
                case 0x12:
                case 0x1b:
                case 0x1c:
                    set.Dispose();
                    return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class=\"SerialNumberSortStyle\"></span><span class='ItemName'>", set.Tables["ItemTable"].Rows[0]["ItemName"], "</span><span class=\"IsRequiredStyle\"></span></div><div class='ItemContent'>", str6, "</div>", str, "</div>" });

                case 14:
                    if (str7 == "6")
                    {
                        str4 = "<div style='margin-left:100px;'><img src='../survey/images/adjustBar.png' id='Rule" + IID.ToString() + "'></div><div   id='AdjustBt" + IID.ToString() + "' style='background:url(../survey/images/adjustPoint.png) no-repeat; width:17px; height:21px; left:100px;'></div>";
                        num = 0;
                        num2 = 2;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0005;

                case 0x10:
                    if (!(set.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() == "1"))
                    {
                        goto Label_0BFC;
                    }
                    num2 = 0x11;
                    goto Label_0005;

                case 0x11:
                    str4 = "&nbsp;";
                    num2 = 0x22;
                    goto Label_0005;

                case 0x13:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj5 = str;
                        str = string.Concat(new object[] { obj5, "<input type='radio' style='display:none' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'>" });
                        string str8 = str3;
                        str3 = str8 + "<td><span class=OptionImgGray" + set.Tables["ItemTable"].Rows[0]["OptionImgModel"].ToString() + "  id='Img_f" + IID.ToString() + "_" + num.ToString() + "'></span></td>";
                        object obj6 = str2;
                        str2 = string.Concat(new object[] { obj6, "<td><span class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num++;
                        num2 = 30;
                    }
                    else
                    {
                        num2 = 0x29;
                    }
                    goto Label_0005;

                case 0x15:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, " <option value='", set.Tables["OptionTable"].Rows[num]["OID"], "'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</option>" });
                        num++;
                        num2 = 7;
                    }
                    else
                    {
                        num2 = 0;
                    }
                    goto Label_0005;

                case 0x16:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj4 = str;
                        str = string.Concat(new object[] { obj4, "<input type='radio' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'>", set.Tables["OptionTable"].Rows[num]["OptionName"] });
                        num++;
                        num2 = 4;
                    }
                    else
                    {
                        num2 = 0x27;
                    }
                    goto Label_0005;

                case 0x17:
                case 30:
                    num2 = 0x13;
                    goto Label_0005;

                case 0x18:
                    str7 = set.Tables["ItemTable"].Rows[0]["OptionImgModel"].ToString();
                    if (str7 == null)
                    {
                        goto Label_01F9;
                    }
                    num2 = 0x24;
                    goto Label_0005;

                case 0x19:
                    if (!(set.Tables["ItemTable"].Rows[0]["ItemType"].ToString() == "5"))
                    {
                        goto Label_03A2;
                    }
                    num2 = 0x1a;
                    goto Label_0005;

                case 0x1a:
                    str5 = "<input name='f" + IID.ToString() + "_Input' type='text' id='f" + IID.ToString() + "_Input' size='30' maxlength='50' class='OtherInputCSS'  disabled='disabled'>";
                    num2 = 0x26;
                    goto Label_0005;

                case 0x1d:
                    if (!(set.Tables["ItemTable"].Rows[0]["ItemType"].ToString() == "6"))
                    {
                        num2 = 0x18;
                    }
                    else
                    {
                        num2 = 0x21;
                    }
                    goto Label_0005;

                case 0x1f:
                    num = 0;
                    num2 = 0x17;
                    goto Label_0005;

                case 0x20:
                    if (!(set.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() == "1"))
                    {
                        num = 0;
                        num2 = 8;
                    }
                    else
                    {
                        num2 = 0x1f;
                    }
                    goto Label_0005;

                case 0x21:
                    try
                    {
                        str = "<select name='f" + IID.ToString() + "' id='f" + IID.ToString() + "' class='SelectCSS'><option value=''>" + a[this.getLan(SID), 0].Split(new char[] { '|' })[0x11] + "</option>";
                    }
                    catch (Exception e)
                    {
                        str = "<select name='f" + IID.ToString() + "' id='f" + IID.ToString() + "' class='SelectCSS'><option value=''>" + "«Î—°‘Ò"+ "</option>";
                    }

                        num = 0;
                    num2 = 10;
                    goto Label_0005;

                case 0x22:
                    goto Label_0BFC;

                case 0x23:
                    goto Label_01F9;

                case 0x24:
                    num2 = 6;
                    goto Label_0005;

                case 0x25:
                    str = str + "<table>" + str2 + "</table>";
                    num2 = 0x1c;
                    goto Label_0005;

                case 0x26:
                    goto Label_03A2;

                case 0x27:
                    str = "<div style='display:none'>" + str + "</div>" + str4;
                    num2 = 15;
                    goto Label_0005;

                case 40:
                    str = str + str5;
                    num2 = 13;
                    goto Label_0005;

                case 0x29:
                    str = str + "<table><tr>" + str3 + "</tr><tr>" + str2 + "</tr></table>";
                    num2 = 0x12;
                    goto Label_0005;
            }
        Label_00B6:
            str = "";
            str2 = "";
            str3 = "";
            set = new DataSet();
            //objComm.CommandText = "SELECT ItemName,ItemType,OptionImgModel,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            DataTable ItemTable = new CreateItem_Layer().GetItemTable(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";
            //objComm.CommandText = "SELECT OID,OptionName FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            DataTable OptionTable = new CreateItem_Layer().GetOptionTable(SID.ToString(), UID.ToString(), IID);
            OptionTable.TableName = "OptionTable";
            set.Tables.Add(ItemTable);
            set.Tables.Add(OptionTable);
            str4 = "<br />";
            num2 = 0x10;
            goto Label_0005;
        Label_01F9:
            num2 = 0x20;
            goto Label_0005;
        Label_03A2:
            num2 = 0x1d;
            goto Label_0005;
        Label_0BFC:
            num = 0;
            str5 = "";
            str6 = set.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            num2 = 0x19;
            goto Label_0005;
        }

        public string createItem_Single_HTML(long SID, long UID, long IID, bool blnIsImage)
        {
            string str;
            string str2;
            string str3;
            string str4;
            DataSet set;
            string str5;
            int num = 0; //∏≥≥ı÷µ
            string str6 = null; //∏≥≥ı÷µ
            string str7 = null; //∏≥≥ı÷µ
            string str8 = null; //∏≥≥ı÷µ
            object obj3;
            int num2;
            goto Label_00C2;
        Label_0005:
            switch (num2)
            {
                case 0:
                    str = "<div style='display:none'>" + str + "</div>" + str5;
                    num2 = 15;
                    goto Label_0005;

                case 1:
                case 0x29:
                    num2 = 13;
                    goto Label_0005;

                case 2:
                    num2 = 11;
                    goto Label_0005;

                case 3:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, " <option value='", set.Tables["OptionTable"].Rows[num]["OID"], "'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</option>" });
                        num++;
                        num2 = 0x19;
                    }
                    else
                    {
                        num2 = 0x1d;
                    }
                    goto Label_0005;

                case 4:
                    str5 = "&nbsp;&nbsp;";
                    num2 = 0x16;
                    goto Label_0005;

                case 5:
                    if (!(set.Tables["ItemTable"].Rows[0]["ItemType"].ToString() == "6"))
                    {
                        num2 = 30;
                    }
                    else
                    {
                        num2 = 40;
                    }
                    goto Label_0005;

                case 6:
                    str6 = "<input name='f" + IID.ToString() + "_Input' type='text' id='f" + IID.ToString() + "_Input' size='30' maxlength='50' class='OtherInputCSS'  disabled='disabled'>";
                    num2 = 9;
                    goto Label_0005;

                case 7:
                    num = 0;
                    num2 = 0x2c;
                    goto Label_0005;

                case 8:
                    goto Label_0B79;

                case 9:
                    goto Label_02D6;

                case 10:
                    str4 = "<img src ='" + str4 + "' alt='' style='border:#000 1px solid'>";
                    num2 = 8;
                    goto Label_0005;

                case 11:
                    if (str8 == "6")
                    {
                        str5 = "<div style='margin-left:100px;'><img src='images/adjustBar.png' id='Rule" + IID.ToString() + "'></div><div   id='AdjustBt" + IID.ToString() + "' style='background:url(images/adjustPoint.png) no-repeat; width:17px; height:21px; left:100px;'></div>";
                        num = 0;
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 0x12;
                    }
                    goto Label_0005;

                case 12:
                    num2 = 0x13;
                    goto Label_0005;

                case 13:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj4 = str;
                        str = string.Concat(new object[] { obj4, "<input type='radio' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "' >", set.Tables["OptionTable"].Rows[num]["OptionName"] });
                        num++;
                        num2 = 0x29;
                    }
                    else
                    {
                        num2 = 0;
                    }
                    goto Label_0005;

                case 14:
                case 0x19:
                    num2 = 3;
                    goto Label_0005;

                case 15:
                case 0x10:
                case 20:
                case 0x20:
                case 0x27:
                    set.Dispose();
                    return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class=\"SerialNumberSortStyle\"></span><span class='ItemName'>", set.Tables["ItemTable"].Rows[0]["ItemName"], "</span><span class=\"IsRequiredStyle\"></span></div><div class='ItemContent'>", str7, "</div>", str, "</div>" });

                case 0x11:
                    str = str + "<table>" + str2 + "</table>";
                    num2 = 0x20;
                    goto Label_0005;

                case 0x12:
                    num2 = 0x23;
                    goto Label_0005;

                case 0x13:
                    if (str8 == "0")
                    {
                        num = 0;
                        num2 = 0x1b;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0005;

                case 0x15:
                case 0x1a:
                    num2 = 0x24;
                    goto Label_0005;

                case 0x16:
                    goto Label_0CC4;

                case 0x17:
                case 0x2c:
                    num2 = 0x26;
                    goto Label_0005;

                case 0x18:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        str4 = this.getURL() + "UserData/u" + UID.ToString() + "/" + set.Tables["OptionTable"].Rows[num]["Image"].ToString();
                        num2 = 0x21;
                    }
                    else
                    {
                        num2 = 0x25;
                    }
                    goto Label_0005;

                case 0x1b:
                case 0x1f:
                    num2 = 0x18;
                    goto Label_0005;

                case 0x1c:
                    if (!(set.Tables["ItemTable"].Rows[0]["ItemType"].ToString() == "5"))
                    {
                        goto Label_02D6;
                    }
                    num2 = 6;
                    goto Label_0005;

                case 0x1d:
                    str = str + "</select>";
                    num2 = 20;
                    goto Label_0005;

                case 30:
                    str8 = set.Tables["ItemTable"].Rows[0]["OptionImgModel"].ToString();
                    if (str8 == null)
                    {
                        goto Label_0C69;
                    }
                    num2 = 12;
                    goto Label_0005;

                case 0x21:
                    if (!(str4 != ""))
                    {
                        goto Label_0B79;
                    }
                    num2 = 10;
                    goto Label_0005;

                case 0x22:
                    str = str + "<table><tr>" + str3 + "</tr><tr>" + str2 + "</tr></table>";
                    num2 = 0x10;
                    goto Label_0005;

                case 0x23:
                    goto Label_0C69;

                case 0x24:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj7 = str;
                        str = string.Concat(new object[] { obj7, "<input type='radio' style='display:none' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'>" });
                        object obj8 = str2;
                        str2 = string.Concat(new object[] { obj8, "<tr><td><span class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td><td><span class=OptionImgGray", set.Tables["ItemTable"].Rows[0]["OptionImgModel"].ToString(), "  id='Img_f", IID.ToString(), "_", num.ToString(), "'></span></td></tr>" });
                        num++;
                        num2 = 0x1a;
                    }
                    else
                    {
                        num2 = 0x11;
                    }
                    goto Label_0005;

                case 0x25:
                    str = str + str6;
                    num2 = 0x27;
                    goto Label_0005;

                case 0x26:
                    if (num < set.Tables["OptionTable"].Rows.Count)
                    {
                        object obj5 = str;
                        str = string.Concat(new object[] { obj5, "<input type='radio' style='display:none' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'>" });
                        string str9 = str3;
                        str3 = str9 + "<td><span class=OptionImgGray" + set.Tables["ItemTable"].Rows[0]["OptionImgModel"].ToString() + "  id='Img_f" + IID.ToString() + "_" + num.ToString() + "'></span></td>";
                        object obj6 = str2;
                        str2 = string.Concat(new object[] { obj6, "<td><span class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td>" });
                        num++;
                        num2 = 0x17;
                    }
                    else
                    {
                        num2 = 0x22;
                    }
                    goto Label_0005;

                case 40:
                    try
                    {
                        str = "<select name='f" + IID.ToString() + "' id='f" + IID.ToString() + "' class='SelectCSS'><option value=''>" + a[this.getLan(SID), 0].Split(new char[] { '|' })[0x11] + "</option>";
                    }
                    catch
                    {
                        str = "<select name='f" + IID.ToString() + "' id='f" + IID.ToString() + "' class='SelectCSS'><option value=''>" + "«Î—°‘Ò" + "</option>";
                    }
                        num = 0;
                    num2 = 14;
                    goto Label_0005;

                case 0x2a:
                    if (!(set.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() == "1"))
                    {
                        num = 0;
                        num2 = 0x15;
                    }
                    else
                    {
                        num2 = 7;
                    }
                    goto Label_0005;

                case 0x2b:
                    if (!(set.Tables["ItemTable"].Rows[0]["OrderModel"].ToString() == "1"))
                    {
                        goto Label_0CC4;
                    }
                    num2 = 4;
                    goto Label_0005;
            }
        Label_00C2:
            str = "";
            str2 = "";
            str3 = "";
            str4 = "";
            set = new DataSet();
            //objComm.CommandText = "SELECT ItemName,ItemType,OptionImgModel,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            DataTable ItemTable = new CreateItem_Layer().GetItemTable(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";
            //objComm.CommandText = "SELECT OID,OptionName,Image FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            DataTable OptionTable = new CreateItem_Layer().GetOptionTable2(SID.ToString(), UID.ToString(), IID);
            OptionTable.TableName = "OptionTable";
            set.Tables.Add(ItemTable);
            set.Tables.Add(OptionTable);
            str5 = "<br />";
            num2 = 0x2b;
            goto Label_0005;
        Label_02D6:
            num2 = 5;
            goto Label_0005;
        Label_0B79:
            obj3 = str;
            str = string.Concat(new object[] { obj3, "<input type='radio' class='RadioCSS' value='", set.Tables["OptionTable"].Rows[num]["OID"], "' name='f", IID.ToString(), "'  id='f", IID.ToString(), "_", num.ToString(), "'><span class='OptionName'>", set.Tables["OptionTable"].Rows[num]["OptionName"], "</span>", str4, str5 });
            num++;
            num2 = 0x1f;
            goto Label_0005;
        Label_0C69:
            num2 = 0x2a;
            goto Label_0005;
        Label_0CC4:
            num = 0;
            str6 = "";
            str7 = set.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            num2 = 0x1c;
            goto Label_0005;
        }

        public void createItem_Sort(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
        Label_001B:
            string[] strArray = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray2 = new string[] { "UPDATE ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 0), "',OptionAmount=", strArray.Length.ToString(), ",ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), " AND IID=", intModifyIID.ToString() };
            //objComm.CommandText = string.Concat(strArray2);
            new CreateItem_Layer().CreateItemText_UpdateItemTable6(arrFormsData[10], this.createCheckStr("", arrFormsData, 0), strArray.Length.ToString(), arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID.ToString());
            //objComm.CommandText = " DELETE FROM OptionTable WHERE IID=" + intModifyIID.ToString();
            new CreateItem_Layer().DeleteOptionTableByIID(intModifyIID.ToString());
            int index = 0;
            int num3 = 2;
        Label_0002:
            switch (num3)
            {
                case 0:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Sort_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Sort_HTML(SID, UID, intModifyIID).Replace("'", "''"), SID.ToString(), intModifyIID.ToString());
                    return;

                case 1:
                    if (index < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + intModifyIID.ToString() + "," + UID.ToString() + ",'" + strArray[index] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), intModifyIID.ToString(), UID.ToString(), strArray[index]);
                        index++;
                        num3 = 3;
                    }
                    else
                    {
                        num3 = 0;
                    }
                    goto Label_0002;

                case 2:
                case 3:
                    num3 = 1;
                    goto Label_0002;
            }
            goto Label_001B;
        }

        public void createItem_Sort(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            string[] strArray = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2 = 0; //∏≥≥ı÷µ
            int num4 = 6;
        Label_000D:
            switch (num4)
            {
                case 0:
                    //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Sort_HTML(SID, UID, num, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + num.ToString();
                    new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Sort_HTML(SID, UID, num).Replace("'", "''"), SID.ToString(), num.ToString());
                    return;

                case 1:
                    if (num2 < strArray.Length)
                    {
                        //objComm.CommandText = " INSERT INTO OptionTable(SID,IID,UID,OptionName) VALUES(" + SID.ToString() + "," + num.ToString() + "," + UID.ToString() + ",'" + strArray[num2] + "')";
                        new CreateItem_Layer().CreateItemNumber_InsertOptionTable(SID.ToString(), num.ToString(), UID.ToString(), strArray[num2]);
                        num2++;
                        num4 = 3;
                    }
                    else
                    {
                        num4 = 0;
                    }
                    goto Label_000D;

                case 2:
                    break;

                case 3:
                case 4:
                    num4 = 1;
                    goto Label_000D;

                case 5:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num4 = 2;
                    goto Label_000D;

                default:
                    if (sCheckStr == "")
                    {
                        num4 = 5;
                        goto Label_000D;
                    }
                    break;
            }
            strArray = arrFormsData[0x15].Replace("\r\n", "\n").Split(new char[] { '\n' });
            //string[] strArray2 = new string[] { 
            //    "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,ItemContent,PageNo,Sort) VALUES(", SID.ToString(), ",", UID.ToString(), ",'", arrFormsData[10], "',12,'", sCheckStr, "',0,", strArray.Length.ToString(), ",'", arrFormsData[11], "',", arrFormsData[1], ",", intSort.ToString(), 
            //    ")"
            // };
            //objComm.CommandText = string.Concat(strArray2);
            new CreateItem_Layer().CreateItemNumber_InsertItemTable4(SID.ToString(), UID.ToString(), arrFormsData[10], "12", sCheckStr, "0", strArray.Length.ToString(), arrFormsData[11], arrFormsData[1], intSort.ToString());
            num = this.getCurrIID(SID, UID);
            num2 = 0;
            num4 = 4;
            goto Label_000D;
        }

        public string createItem_Sort_HTML(long SID, long UID, long IID)
        {
            string str;
        Label_001B:
            str = "";
            DataSet dataSet = new DataSet();
            //objComm.CommandText = "SELECT ItemName,ItemType,OptionImgModel,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "ItemTable");
            //objComm.CommandText = "SELECT OID,OptionName FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            //adapter.Fill(dataSet, "OptionTable");

            //objComm.CommandText = "SELECT ItemName,ItemType,OptionImgModel,OrderModel,ItemContent FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            DataTable ItemTable = new CreateItem_Layer().GetItemTable(SID.ToString(), UID.ToString(), IID);
            ItemTable.TableName = "ItemTable";
            //objComm.CommandText = "SELECT OID,OptionName FROM OptionTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            DataTable OptionName = new CreateItem_Layer().GetOptionTable(SID.ToString(), UID.ToString(), IID);
            OptionName.TableName = "OptionName";
            dataSet.Tables.Add(ItemTable);
            dataSet.Tables.Add(OptionName);

            string str2 = dataSet.Tables["ItemTable"].Rows[0]["ItemContent"].ToString();
            int num = 0;
            int num2 = 1;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 1:
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (num >= dataSet.Tables["OptionTable"].Rows.Count)
                    {
                        num2 = 3;
                    }
                    else
                    {
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, "<tr><td><span class='OptionName'>", dataSet.Tables["OptionTable"].Rows[num]["OptionName"], "</span></td><td><input id='f", IID.ToString(), "_", num.ToString(), "' name='f", IID.ToString(), "_", dataSet.Tables["OptionTable"].Rows[num]["OID"], "_", num.ToString(), "' type='text' size='5' maxlength='2' class='InputCSS'></td></tr>" });
                        num++;
                        num2 = 0;
                    }
                    goto Label_0002;

                case 3:
                    dataSet.Dispose();
                    return string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", dataSet.Tables["ItemTable"].Rows[0]["ItemName"], "</span></div><div class='ItemContent'>", str2, "</div><table>", str, "</table></div>" });
            }
            goto Label_001B;
        }

        public void createItem_Text(long SID, long UID, long intParentID)
        {
            //objComm.CommandText = "INSERT INTO ItemTable(SID,UID,ItemType,ParentID) VALUES(" + SID.ToString() + "," + UID.ToString() + ",1," + intParentID.ToString() + ")";
            new CreateItem_Layer().CreateItemNumber_InsertItemTable7(SID.ToString(), UID.ToString(), "1", intParentID.ToString());
        }

        public void createItem_Text(long SID, long UID, string[] arrFormsData,int intSort, long intModifyIID)
        {
            //objComm.CommandText = string.Concat(new object[] { "UPDATE ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 1), "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), " AND IID=", intModifyIID });
            new CreateItem_Layer().CreateItemText_UpdateItemTable(arrFormsData[10], this.createCheckStr("", arrFormsData, 1), arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID);
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Text_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Text_HTML(SID, UID, intModifyIID).Replace("'", "''"), SID.ToString(), intModifyIID.ToString());
        }

        public void createItem_Text(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            int num2 = 2;
        Label_000D:
            switch (num2)
            {
                case 0:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    sCheckStr = this.createCheckStr("", arrFormsData, 0);
                    num2 = 1;
                    goto Label_000D;

                case 1:
                    break;

                default:
                    if (sCheckStr == "")
                    {
                        num2 = 0;
                        goto Label_000D;
                    }
                    break;
            }
            //objComm.CommandText = "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,ItemContent,PageNo,Sort) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + arrFormsData[10] + "'," + arrFormsData[0] + ",'" + sCheckStr + "',0,'" + arrFormsData[11] + "'," + arrFormsData[1] + "," + intSort.ToString() + ")";
            new CreateItem_Layer().CreateItemNumber_InsertItemTable(SID.ToString(), UID.ToString(), arrFormsData[10], arrFormsData[0], sCheckStr, "0", arrFormsData[11], arrFormsData[1], intSort.ToString());
            string str = this.getCurrIID(SID, UID).ToString();
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Text_HTML(SID, UID, Convert.ToInt32(str)).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + str;
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Text_HTML(SID, UID, Convert.ToInt32(str)).Replace("'", "''"), SID.ToString(), str);
        }

        public string createItem_Text_HTML(long SID, long UID, long IID)
        {
            string str;
            string str2 = null; //∏≥≥ı÷µ
        Label_0029:
            str = "";
            //objComm.CommandText = "SELECT ItemName,ItemType,ItemContent,DataFormatCheck FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            SqlDataReader reader = new CreateItem_Layer().CreateItem_Text_HTML_SelectItemTable(SID.ToString(), UID.ToString(), IID.ToString());
            int num = 1;
        Label_0002:
            switch (num)
            {
                case 0:
                case 3:
                    break;

                case 1:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num = 4;
                    goto Label_0002;

                case 2:
                    if (!(reader["ItemType"].ToString() == "1"))
                    {
                        str = string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", reader["ItemName"], "</span></div><div class='ItemContent'>", str2, "</div><textarea  cols='50' rows='6'  name='f", IID.ToString(), "'  id='f", IID.ToString(), "' class='TextAreaCSS'></textarea></div>" });
                        num = 3;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_0002;

                case 4:
                    str2 = reader["ItemContent"].ToString();
                    num = 2;
                    goto Label_0002;

                case 5:
                    str = string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", reader["ItemName"], "</span></div><div class='ItemContent'>", str2, "</div><input name='f", IID.ToString(), "' type=text id='f", IID.ToString(), "' size='50' maxlength='", this.getInputObjLen(reader["DataFormatCheck"].ToString()), "' class='InputCSS'></div>" });
                    num = 0;
                    goto Label_0002;

                default:
                    goto Label_0029;
            }
            reader.Close();
            reader.Dispose();
            return str;
        }

        public void createItem_Text_NotInput(long SID, long UID, string[] arrFormsData, int intSort, long intModifyIID)
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            //objComm.CommandText = string.Concat(new object[] { "UPDATE ItemTable SET ItemName='", arrFormsData[10], "',DataFormatCheck='", this.createCheckStr("", arrFormsData, 1), "',ItemContent='", arrFormsData[11], "',PageNo=", arrFormsData[1], " WHERE SID=", SID.ToString(), " AND UID=", UID.ToString(), " AND IID=", intModifyIID });
            new CreateItem_Layer().CreateItemText_UpdateItemTable(arrFormsData[10], this.createCheckStr("", arrFormsData, 1), arrFormsData[11], arrFormsData[1], SID.ToString(), UID.ToString(), intModifyIID);
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Text_NotInput_HTML(SID, UID, intModifyIID, objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + intModifyIID.ToString();
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Text_NotInput_HTML(SID, UID, intModifyIID).Replace("'", "''"), SID.ToString(), intModifyIID.ToString());
        }

        public void createItem_Text_NotInput(long SID, long UID, string[] arrFormsData, int intSort, string sCheckStr)
        {
            //objComm.CommandText = "INSERT INTO ItemTable(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,ItemContent,PageNo,Sort) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + arrFormsData[10] + "'," + arrFormsData[0] + ",'" + sCheckStr + "',0,'" + arrFormsData[11] + "'," + arrFormsData[1] + "," + intSort.ToString() + ")";
            new CreateItem_Layer().CreateItemNumber_InsertItemTable(SID.ToString(), UID.ToString(), arrFormsData[10], arrFormsData[0], sCheckStr, "0", arrFormsData[11], arrFormsData[1], intSort.ToString());
            string str = this.getCurrIID( SID, UID).ToString();
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + this.createItem_Text_NotInput_HTML(SID, UID, Convert.ToInt32(str), objComm).Replace("'", "''") + "' WHERE SID=" + SID.ToString() + " AND IID=" + str;
            new CreateItem_Layer().CreateItemText_UpdateItemTable1(this.createItem_Text_NotInput_HTML(SID, UID, Convert.ToInt32(str)).Replace("'", "''"), SID.ToString(), str);
        }

        public string createItem_Text_NotInput_HTML(long SID, long UID, long IID)
        {
        Label_0017:
            string str = "";
            //objComm.CommandText = "SELECT ItemName,ItemType,ItemContent,DataFormatCheck FROM ItemTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND IID=" + IID.ToString();
            SqlDataReader reader = new CreateItem_Layer().GetItemTable11(SID.ToString(), UID.ToString(), IID);
            int num = 0;
        Label_0002:
            switch (num)
            {
                case 0:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num = 2;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    str = string.Concat(new object[] { "<div id='Item", IID.ToString(), "' class='ItemBox'><div class='ItemBar'><span class='ItemName'>", reader["ItemName"], "</span></div><div class='ItemContent'>", reader["ItemContent"].ToString(), "</div><div class='ExpandContentStyle' id='Introduction", IID.ToString(), "'></div></div>" });
                    num = 1;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            reader.Close();
            reader.Dispose();
            return str;
        }

        public string createItemHTML(long IID, long SID, long UID, int intItemType, bool blnIsImage)
        {
            string str;
            SqlDataReader reader = null; //∏≥≥ı÷µ
            int num = 0; //∏≥≥ı÷µ
            int num2;
            goto Label_007F;
        Label_0002:
            switch (num2)
            {
                case 0:
                    return str;

                case 1:
                    return str;

                case 2:
                    num2 = 0x16;
                    goto Label_0002;

                case 3:
                    switch (num)
                    {
                        case 1:
                        case 2:
                        case 3:
                            str = this.createItem_Text_HTML(SID, UID, IID);
                            num2 = 5;
                            goto Label_0002;

                        case 4:
                        case 5:
                        case 6:
                            num2 = 0x17;
                            goto Label_0002;

                        case 7:
                            str = this.createItem_MatrixSingle_HTML(SID, UID, IID);
                            num2 = 9;
                            goto Label_0002;

                        case 8:
                        case 9:
                        case 10:
                            num2 = 14;
                            goto Label_0002;

                        case 11:
                            str = this.createItem_Level_HTML(SID, UID, IID);
                            num2 = 0;
                            goto Label_0002;

                        case 12:
                            str = this.createItem_Sort_HTML(SID, UID, IID);
                            num2 = 7;
                            goto Label_0002;

                        case 13:
                            str = this.createItem_ListInput_HTML(SID, UID, IID);
                            num2 = 10;
                            goto Label_0002;

                        case 14:
                            str = this.createItem_Text_NotInput_HTML(SID, UID, IID);
                            num2 = 0x13;
                            goto Label_0002;

                        case 15:
                            str = this.createItem_MatrixMulti_HTML(SID, UID, IID);
                            num2 = 0x19;
                            goto Label_0002;

                        case 0x10:
                            str = this.createItem_MatrixInput_HTML(SID, UID, IID);
                            num2 = 13;
                            goto Label_0002;

                        case 0x11:
                            str = this.createItem_FileUpload_HTML(SID, UID, IID);
                            num2 = 0x1a;
                            goto Label_0002;

                        case 0x12:
                            str = this.createItem_MatrixDropList_HTML(SID, UID, IID);
                            num2 = 11;
                            goto Label_0002;

                        case 0x13:
                            goto Label_0164;

                        case 20:
                            str = this.createItem_MatrixMultiInput_HTML(SID, UID, IID);
                            num2 = 20;
                            goto Label_0002;
                    }
                    num2 = 2;
                    goto Label_0002;

                case 4:
                    if (intItemType != 0)
                    {
                        goto Label_029A;
                    }
                    num2 = 0x10;
                    goto Label_0002;

                case 5:
                    return str;

                case 6:
                    intItemType = Convert.ToInt32(reader["ItemType"]);
                    num2 = 0x15;
                    goto Label_0002;

                case 7:
                    return str;

                case 8:
                    return str;

                case 9:
                    return str;

                case 10:
                    return str;

                case 11:
                    return str;

                case 12:
                    return str;

                case 13:
                    return str;

                case 14:
                    if (blnIsImage)
                    {
                        str = this.createItem_Mulit_HTML(SID, UID, IID, blnIsImage);
                        num2 = 0x1b;
                    }
                    else
                    {
                        num2 = 0x12;
                    }
                    goto Label_0002;

                case 15:
                    return str;

                case 0x10:
                    //objComm.CommandText = "SELECT TOP 1 ItemType FROM ItemTable WHERE IID=" + IID.ToString() + " AND UID=" + UID.ToString() + " AND SID=" + SID.ToString();
                    reader = new CreateItem_Layer().GetItemTable12(IID.ToString(), UID.ToString(), SID.ToString());
                    num2 = 0x11;
                    goto Label_0002;

                case 0x11:
                    if (!reader.Read())
                    {
                        goto Label_011D;
                    }
                    num2 = 6;
                    goto Label_0002;

                case 0x12:
                    str = this.createItem_Mulit_HTML(SID, UID, IID);
                    num2 = 15;
                    goto Label_0002;

                case 0x13:
                    return str;

                case 20:
                    return str;

                case 0x15:
                    goto Label_011D;

                case 0x16:
                    return str;

                case 0x17:
                    if (blnIsImage)
                    {
                        str = this.createItem_Single_HTML(SID, UID, IID, blnIsImage);
                        num2 = 8;
                    }
                    else
                    {
                        num2 = 0x1c;
                    }
                    goto Label_0002;

                case 0x18:
                    goto Label_029A;

                case 0x19:
                    return str;

                case 0x1a:
                    return str;

                case 0x1b:
                    if ((1 == 0) || (0 == 0))
                    {
                        return str;
                    }
                    goto Label_0164;

                case 0x1c:
                    str = this.createItem_Single_HTML(SID, UID, IID);
                    num2 = 12;
                    goto Label_0002;
            }
        Label_007F:
            str = "";
            num2 = 4;
            goto Label_0002;
        Label_011D:
            reader.Close();
            reader.Dispose();
            num2 = 0x18;
            goto Label_0002;
        Label_0164:
            str = this.createItem_MatrixSingleInput_HTML(SID, UID, IID);
            num2 = 1;
            goto Label_0002;
        Label_029A:
            num = intItemType;
            num2 = 3;
            goto Label_0002;
        }

        public void createPage(long SID, long UID, string sPageContent, int intPageNo)
        {
            //objComm.CommandText = "INSERT INTO PageTable(SID,UID,PageContent,PageNo) VALUES(" + SID.ToString() + "," + UID.ToString() + ",'" + sPageContent + "'," + intPageNo.ToString() + ")";
            new CreateItem_Layer().InsertPageTable(SID.ToString(), UID.ToString(), sPageContent, intPageNo.ToString());
        }

        public void createPage(long SID, long UID, string sPageContent, int intPageNo, int intPageAmount)
        {
            int num;
        Label_001B:
            num = 1;
            int num2 = 2;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 2:
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    if (num <= intPageAmount)
                    {
                        //objComm.CommandText = " INSERT INTO PageTable(SID,UID,PageNo) VALUES(" + SID.ToString() + "," + UID.ToString() + "," + num.ToString() + ")";
                        new CreateItem_Layer().InsertPageTable1(SID.ToString(), UID.ToString(), num.ToString());
                        num++;
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 3:
                    return;
            }
            goto Label_001B;
        }

        public int createSurvey(string sSurveyName, int intSurveyClass, string sDefaultPar, long UID)
        {
            SqlDataReader reader;
            int num;
            int num2;
            if ((1 == 0) || (0 == 0))
            {
                goto Label_001B;
            }
        Label_0006:
            switch (num2)
            {
                case 0:
                    num = Convert.ToInt32(reader["CurrSID"]);
                    num2 = 1;
                    goto Label_0006;

                case 1:
                    goto Label_00E6;

                case 2:
                    if (!reader.Read())
                    {
                        goto Label_00E6;
                    }
                    num2 = 0;
                    goto Label_0006;
            }
        Label_001B:
            sDefaultPar = "|RecordIP:1|RecordTime:1";
            //objComm.CommandText = "INSERT INTO SurveyTable(SurveyName,CreateDate,UID,ClassID,Par) VALUES('" + sSurveyName + "','" + DateTime.Now.ToShortDateString() + "'," + UID.ToString() + "," + intSurveyClass.ToString() + ",'" + sDefaultPar + "') SELECT   IDENT_CURRENT('SurveyTable')   AS CurrSID";
            reader=new CreateItem_Layer().InsertSurveyTable(sSurveyName, DateTime.Now.ToShortDateString(), UID.ToString(), intSurveyClass.ToString(), sDefaultPar);
            num = 0;
            num2 = 2;
            goto Label_0006;
        Label_00E6:
            reader.Close();
            //objComm.CommandText = "INSERT INTO PageTable(UID,SID,PageNo,PageContent) VALUES(" + UID.ToString() + "," + num.ToString() + ",1,'') ";
            new CreateItem_Layer().InsertPageTable(UID.ToString(), num.ToString(),"","1");
            //objComm.CommandText = " INSERT INTO HeadFoot(PageHead,UID,SID) VALUES('" + sSurveyName + "'," + UID.ToString() + "," + num.ToString() + ")";
            new CreateItem_Layer().InsertHeadFoot(sSurveyName, UID.ToString(), num.ToString());
            return num;
        }

        public int createSurvey(string sSurveyName, int intSurveyClass, string sDefaultPar, long UID, bool blnNotPage)
        {
        Label_0017:
            sDefaultPar = "|RecordIP:1|RecordTime:1";
            //objComm.CommandText = "INSERT INTO SurveyTable(SurveyName,CreateDate,UID,ClassID,Par) VALUES('" + sSurveyName + "','" + DateTime.Now.ToShortDateString() + "'," + UID.ToString() + "," + intSurveyClass.ToString() + ",'" + sDefaultPar + "')";
            new CreateItem_Layer().InsertSurveyTable1(sSurveyName, DateTime.Now.ToShortDateString(), UID.ToString(), intSurveyClass.ToString(), sDefaultPar);
            //objComm.CommandText = " SELECT MAX(SID) FROM SurveyTable";
            SqlDataReader reader = new CreateItem_Layer().GetSurveyTable();
            int num = 0;
            int num2 = 2;
        Label_0002:
            switch (num2)
            {
                case 0:
                    num = Convert.ToInt32(reader[0]);

                    num2 = 1;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num2 = 0;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            reader.Close();
            //objComm.CommandText = " INSERT INTO HeadFoot(PageHead,UID,SID) VALUES('" + sSurveyName + "'," + UID.ToString() + "," + num.ToString() + ")";
            new CreateItem_Layer().InsertHeadFoot(sSurveyName, UID.ToString(), num.ToString());
            return num;
        }

        public int createSurvey(string sSurveyName, int intSurveyClass, string sDefaultPar, long UID, string sTempPage)
        {
        Label_0017:
            sDefaultPar = "|RecordIP:1|RecordTime:1";
            //objComm.CommandText = "INSERT INTO SurveyTable(SurveyName,CreateDate,UID,ClassID,Par,TempPage) VALUES('" + sSurveyName + "','" + DateTime.Now.ToShortDateString() + "'," + UID.ToString() + "," + intSurveyClass.ToString() + ",'" + sDefaultPar + "','" + sTempPage + "') SELECT   IDENT_CURRENT('SurveyTable')   AS CurrSID";
            SqlDataReader reader = new CreateItem_Layer().InsertSurveyTable2(sSurveyName, DateTime.Now.ToShortDateString(), UID.ToString(), intSurveyClass.ToString(), sDefaultPar, sTempPage);
            int num = 0;
            int num2 = 1;
        Label_0002:
            switch (num2)
            {
                case 0:
                    num = Convert.ToInt32(reader["CurrSID"]);
                    num2 = 2;
                    goto Label_0002;

                case 1:
                    if (!reader.Read())
                    {
                        break;
                    }
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num2 = 0;
                    goto Label_0002;

                case 2:
                    break;

                default:
                    goto Label_0017;
            }
            reader.Close();
            //objComm.CommandText = "INSERT INTO PageTable(UID,SID,PageNo,PageContent) VALUES(" + UID.ToString() + "," + num.ToString() + ",1,'') ";
            new CreateItem_Layer().InsertPageTable(UID.ToString(), num.ToString(), "", "1");
            //objComm.CommandText = " INSERT INTO HeadFoot(PageHead,UID,SID) VALUES('" + sSurveyName + "'," + UID.ToString() + "," + num.ToString() + ")";
            new CreateItem_Layer().InsertHeadFoot(sSurveyName, UID.ToString(), num.ToString());
            return num;
        }

        protected int getCurrIID(long SID, long UID)
        {
            int num;
        Label_0017:
            num = 0;
            //objComm.CommandText = "SELECT TOP 1 IID FROM ItemTable WHERE SID=" + SID.ToString() + " AND ParentID=0 AND UID=" + UID.ToString() + " ORDER BY IID DESC";
            SqlDataReader reader = new CreateItem_Layer().GetCurrDataFromItemTable(SID.ToString(), 0, UID.ToString());
            int num2 = 2;
        Label_0002:
            switch (num2)
            {
                case 0:
                    num = Convert.ToInt32(reader[0]);
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num2 = 0;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            reader.Close();
            reader.Dispose();
            return num;
        }

        public string[] getFormsData()
        {
            string[] strArray;
            int num;
            goto Label_0023;
        Label_0002:
            switch (num)
            {
                case 0:
                    if (!(strArray[0] == "13"))
                    {
                        return strArray;
                    }
                    num = 1;
                    goto Label_0002;

                case 1:
                    strArray[0x19] = strArray[7];
                    num = 5;
                    goto Label_0002;

                case 2:
                    if (!(strArray[11] != ""))
                    {
                        goto Label_020F;
                    }
                    num = 3;
                    goto Label_0002;

                case 3:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    strArray[11] = strArray[11].Replace("\r\n", "<BR>");
                    num = 4;
                    goto Label_0002;

                case 4:
                    goto Label_020F;

                case 5:
                    return strArray;
            }
        Label_0023:
            strArray = new string[0x24];
            strArray[0] = Convert.ToString(HttpContext.Current.Request.Form["flag"]);
            strArray[1] = Convert.ToString(HttpContext.Current.Request.Form["PageNo"]);
            strArray[2] = Convert.ToString(HttpContext.Current.Request.Form["MinLen"]);
            strArray[3] = Convert.ToString(HttpContext.Current.Request.Form["MaxLen"]);
            strArray[4] = Convert.ToString(HttpContext.Current.Request.Form["MinSelect"]);
            strArray[5] = Convert.ToString(HttpContext.Current.Request.Form["MaxSelect"]);
            strArray[6] = Convert.ToString(HttpContext.Current.Request.Form["MinTickOff"]);
            strArray[7] = Convert.ToString(HttpContext.Current.Request.Form["MaxTickOff"]);
            strArray[8] = Convert.ToString(HttpContext.Current.Request.Form["MaxValue"]);
            strArray[9] = Convert.ToString(HttpContext.Current.Request.Form["MinValue"]);
            strArray[10] = Convert.ToString(HttpContext.Current.Request.Form["ItemName"]).Trim();
            strArray[11] = Convert.ToString(HttpContext.Current.Request.Form["Memo"]).Trim();
            num = 2;
            goto Label_0002;
        Label_020F:
            strArray[12] = Convert.ToString(HttpContext.Current.Request.Form["Mob"]);
            strArray[13] = Convert.ToString(HttpContext.Current.Request.Form["PostCode"]);
            strArray[14] = Convert.ToString(HttpContext.Current.Request.Form["Data"]);
            strArray[15] = Convert.ToString(HttpContext.Current.Request.Form["IDCard"]);
            strArray[0x10] = Convert.ToString(HttpContext.Current.Request.Form["Cn"]);
            strArray[0x11] = Convert.ToString(HttpContext.Current.Request.Form["En"]);
            strArray[0x12] = Convert.ToString(HttpContext.Current.Request.Form["URL"]);
            strArray[0x13] = Convert.ToString(HttpContext.Current.Request.Form["Email"]);
            strArray[20] = Convert.ToString(HttpContext.Current.Request.Form["SubItem"]).Trim();
            strArray[0x15] = Convert.ToString(HttpContext.Current.Request.Form["BackSelect"]).Trim();
            strArray[0x16] = Convert.ToString(HttpContext.Current.Request.Form["Empty"]);
            strArray[0x17] = Convert.ToString(HttpContext.Current.Request.Form["UseOptionIMG"]);
            strArray[0x18] = Convert.ToString(HttpContext.Current.Request.Form["OrderModel"]);
            strArray[0x19] = Convert.ToString(HttpContext.Current.Request.Form["LevelAmount"]);
            strArray[0x1a] = Convert.ToString(HttpContext.Current.Request.Form["MaxLevelName"]);
            strArray[0x1b] = Convert.ToString(HttpContext.Current.Request.Form["MinLevelName"]);
            strArray[0x1c] = Convert.ToString(HttpContext.Current.Request.Form["MaxFileLen"]);
            strArray[0x1d] = Convert.ToString(HttpContext.Current.Request.Form["UploadMode"]);
            strArray[30] = Convert.ToString(HttpContext.Current.Request.Form["FileType"]);
            strArray[0x1f] = Convert.ToString(HttpContext.Current.Request.Form["MatrixRowColumn"]);
            strArray[0x20] = Convert.ToString(HttpContext.Current.Request.Form["TotalPercent"]);
            strArray[0x21] = Convert.ToString(HttpContext.Current.Request.Form["MinPercent"]);
            strArray[0x22] = Convert.ToString(HttpContext.Current.Request.Form["MaxPercent"]);
            strArray[0x23] = Convert.ToString(HttpContext.Current.Request.Form["InputLength"]);
            num = 0;
            goto Label_0002;
        }

        public int getInputObjLen(string sCheckStr)
        {
            int num;
        Label_004F:
            num = 50;
            string[] strArray = sCheckStr.Split(new char[] { '|' });
            int num2 = 14;
        Label_0002:
            switch (num2)
            {
                case 0:
                    return num;

                case 1:
                    num = 11;
                    num2 = 0;
                    goto Label_0002;

                case 2:
                    return num;

                case 3:
                    return num;

                case 4:
                    return num;

                case 5:
                    return num;

                case 6:
                    if (!(strArray[2] == "IDCard1"))
                    {
                        num2 = 11;
                    }
                    else
                    {
                        num2 = 0x10;
                    }
                    goto Label_0002;

                case 7:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (!(strArray[1] == "PostCode1"))
                    {
                        return num;
                    }
                    num2 = 9;
                    goto Label_0002;

                case 8:
                    num = 11;
                    num2 = 2;
                    goto Label_0002;

                case 9:
                    num = 6;
                    num2 = 3;
                    goto Label_0002;

                case 10:
                    if (!(strArray[4] == "Mob1"))
                    {
                        num2 = 6;
                    }
                    else
                    {
                        num2 = 8;
                    }
                    goto Label_0002;

                case 11:
                    if (!(strArray[3] == "Date1"))
                    {
                        num2 = 7;
                    }
                    else
                    {
                        num2 = 1;
                    }
                    goto Label_0002;

                case 12:
                    if (num <= 200)
                    {
                        return num;
                    }
                    num2 = 15;
                    goto Label_0002;

                case 13:
                    num = Convert.ToInt32(strArray[10].Substring(6));
                    num2 = 12;
                    goto Label_0002;

                case 14:
                    if (!(strArray[10] != "MaxLen"))
                    {
                        num2 = 10;
                    }
                    else
                    {
                        num2 = 13;
                    }
                    goto Label_0002;

                case 15:
                    num = 200;
                    num2 = 4;
                    goto Label_0002;

                case 0x10:
                    num = 0x12;
                    num2 = 5;
                    goto Label_0002;
            }
            goto Label_004F;
        }

        public int getLan(long SID)
        {
        Label_0017:
            int num = 1;
            //objComm.CommandText = "SELECT TOP 1 Lan FROM SurveyTable WHERE SID=" + SID.ToString();
            SqlDataReader reader = new CreateItem_Layer().GetSurveyTable1(SID.ToString());
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    num = Convert.ToInt32(reader["Lan"]);
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    break;

                default:
                    goto Label_0017;
            }
            reader.Dispose();
            return num;
        }

        public void getLanguage()
        {
            SqlDataReader reader = null; //∏≥≥ı÷µ
            int num = 5;
        Label_000D:
            switch (num)
            {
                case 0:
                    if (reader.Read())
                    {
                        break;
                    }
                    num = 2;
                    goto Label_000D;

                case 1:
                    if ((1 == 0) || (0 == 0))
                    {
                        return;
                    }
                    break;

                case 2:
                    reader.Dispose();
                    return;

                case 3:
                case 4:
                    num = 0;
                    goto Label_000D;

                default:
                    if (a[1, 0] == null)
                    {
                        //objComm.CommandText = "SELECT ID,JS,CSharp FROM LanTable";
                        reader = new CreateItem_Layer().GetLanTable();
                        num = 3;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_000D;
            }
            a[Convert.ToInt32(reader["ID"]), 0] = reader["JS"].ToString();
            a[Convert.ToInt32(reader["ID"]), 1] = reader["CSharp"].ToString();
            num = 4;
            goto Label_000D;
        }

        protected int getSort(long SID, int intPageNo)
        {
            int num;
        Label_0027:
            num = 1;
            //objComm.CommandText = "SELECT MAX(Sort) FROM ItemTable WHERE SID=" + SID.ToString() + " AND PageNo=" + intPageNo.ToString() + " AND ParentID=0";
            SqlDataReader reader = new CreateItem_Layer().GetItemTable13(SID.ToString(), intPageNo.ToString(),"0");
            int num2 = 6;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (Convert.IsDBNull(reader[0]))
                    {
                        num = 1;
                        num2 = 5;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 1:
                case 2:
                case 5:
                    reader.Close();
                    reader.Dispose();
                    return num;

                case 3:
                    num = Convert.ToInt16(reader[0]) + 1;
                    num2 = 2;
                    goto Label_0002;

                case 4:
                    num2 = 0;
                    goto Label_0002;

                case 6:
                    if (!reader.Read())
                    {
                        num = 1;
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    goto Label_0002;
            }
            goto Label_0027;
        }

        public string getURL()
        {
            string str = "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.ServerVariables["URL"];
            return str.Substring(0, str.LastIndexOf("/") + 1);
        }

        public void updateItemHtml(long SID, long UID, long IID, string sHTML)
        {
            //sHTML = sHTML.Replace("'", "''");
            //objComm.CommandText = "UPDATE ItemTable SET ItemHTML='" + sHTML + "' WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + "  AND IID=" + IID.ToString();
            new CreateItem_Layer().CreateItemText_UpdateItemTable7(sHTML, SID.ToString(), UID.ToString(), IID.ToString());
        }
    }
}

