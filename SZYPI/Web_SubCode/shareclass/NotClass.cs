namespace shareclass
{
    using System;
    using System.Data.OleDb;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Security;
    using BusinessLayer.ShareClass;
    using System.Data.SqlClient;

    public class NotClass
    {
        private long c;
        private long d;
        private long e;

        public NotClass(long SID__, long UID__)
        {
            this.c = SID__;
            this.e = UID__;
        }

        //public void BBS_Update(int BBSID)  //现在不包含bbs
        //{
        //    //this.b.CommandText = "UPDATE BBS SET Status=1 WHERE ID=" + BBSID.ToString() + " AND Status<>2";
        //    //this.b.ExecuteNonQuery();
        //}

        public void fromStyleLibCopy(long ID, long TargetSID, long UID)
        {
            string str;
            SqlDataReader reader;
            SqlDataReader reader1;
            int num = 0; //赋初值
            int num2;
            goto Label_0023;
        Label_0002:
            switch (num2)
            {
                case 0:
                    return;

                case 1:
                    goto Label_00F5;

                case 2:
                    if (!reader.Read())
                    {
                        goto Label_00F5;
                    }
                    num2 = 3;
                    goto Label_0002;

                case 3:
                    str = reader["StyleContent"].ToString();
                    num2 = 1;
                    goto Label_0002;

                case 4:
                    if (num != 0)
                    {
                        return;
                    }
                    num2 = 5;
                    goto Label_0002;

                case 5:
                    //this.b.CommandText = "INSERT INTO SurveyExpand (SID,UID,ExpandContent,ExpandType) VALUES(" + TargetSID.ToString() + "," + UID.ToString() + ",'" + str + "',9)";
                    
                    reader1 = new SClass().GetTop1DefaultStyleLib();
                    if (reader1.Read())
                    {
                        str = reader1["StyleContent"].ToString();  //设置默认样式
                    }
                    new SClass().InsertSurveyExpand(TargetSID.ToString(), UID.ToString(), str,"9");
                    num2 = 0;
                    goto Label_0002;
            }
        Label_0023:
            str = "";
            //this.b.CommandText = "SELECT * FROM StyleLib WHERE ID=" + ID.ToString();
            reader = new SClass().GetAllStyleLib(ID.ToString());
            num2 = 2;
            goto Label_0002;
        Label_00F5:
            reader.Dispose();
            //this.b.CommandText = "UPDATE SurveyExpand SET ExpandContent='" + str + "'  WHERE ExpandType=9 AND SID=" + TargetSID.ToString() + " AND UID=" + UID.ToString();
            num = new SClass().UpdateSurveyExpand(str, "9", TargetSID.ToString(), UID.ToString());
            num2 = 4;
            goto Label_0002;
        }

        //public string GetBBS(OleDbConnection objConn, int ID, int UID)
        //{
        //    bool flag;
        //    StringBuilder builder;
        //    int num;
        //    goto Label_003B;
        //Label_0002:
        //    switch (num)
        //    {
        //        case 0:
        //            if (flag)
        //            {
        //                goto Label_0363;
        //            }
        //            num = 4;
        //            goto Label_0002;

        //        case 1:
        //        case 10:
        //            goto Label_01D6;

        //        case 2:
        //            goto Label_01A9;

        //        case 3:
        //            //builder.Append(string.Concat(new object[] { "<div class=BBS_Box><span class=BBS_Speak style='float:left'></span><span class=BBS_User>", reader["UserName"], ":</span><span class=BBS_Title>", reader["Caption"].ToString(), "</span><span class=BBS_Date>[", reader["SubmitDate"], "]</span><div class=BBS_Content>", reader["Content"].ToString().Replace("\r\n", "<br />"), "</div></div>" }));
        //            num = 10;
        //            goto Label_0002;

        //        case 4:
        //            return "";

        //        case 5:
        //            if ((1 == 0) || (0 == 0))
        //            {
        //                goto Label_01D6;
        //            }
        //            goto Label_0363;

        //        case 6:
        //            return builder.ToString();

        //        case 7:
        //            //if (reader.Read())
        //            //{
        //            //    num = 11;
        //            //}
        //            //else
        //            //{
        //                num = 6;
        //            //}
        //            goto Label_0002;

        //        case 8:
        //            flag = true;
        //            builder.Append(string.Concat(new object[] { "<div class=BBS_Box><span class=BBS_Speak style='float:left'></span><span class=BBS_User>", reader["UserName"], ":</span><span class=BBS_Title>", reader["Caption"].ToString(), "</span><span class=BBS_Date>[", reader["SubmitDate"], "]</span><div class=BBS_Content>", reader["Content"].ToString().Replace("\r\n", "<br />"), "</div></div>" }));
        //            num = 2;
        //            goto Label_0002;

        //        case 9:
        //            //if (!reader.Read())
        //            //{
        //            //    goto Label_01A9;
        //            //}
        //            num = 8;
        //            goto Label_0002;

        //        case 11:
        //            //if (!(reader["IsAdminRe"].ToString() == "False"))
        //            //{
        //                builder.Append(string.Concat(new object[] { "<div class=BBS_Box><span class=BBS_AdminSpeak style='float:left'></span><span class=BBS_User>", reader["UserName"], ":</span><span class=BBS_Title>", reader["Caption"].ToString(), "</span><span class=BBS_Date>[", reader["SubmitDate"], "]</span><div class=BBS_Content>", reader["Content"].ToString().Replace("\r\n", "<br />"), "</div></div>" }));
        //                num = 5;
        //            //}
        //            //else
        //            //{
        //            //    num = 3;
        //            //}
        //            goto Label_0002;
        //    }
        //Label_003B:
        //    flag = false;
        //    builder = new StringBuilder();
        //    //OleDbCommand command = new OleDbCommand("SELECT TOP 1 B.*,U.UserName FROM BBS B INNER JOIN [User] U ON B.UID=U.UID WHERE B.ID=" + ID.ToString() + " AND B.UID=" + UID.ToString() + " AND ReID=0", objConn);
        //    //reader = command.ExecuteReader();
        //    num = 9;
        //    goto Label_0002;
        //Label_01A9:
        //    //reader.Close();
        //    num = 0;
        //    goto Label_0002;
        //Label_01D6:
        //    num = 7;
        //    goto Label_0002;
        //Label_0363:
        //    //command.CommandText = "SELECT  B.*,U.UserName FROM BBS B INNER JOIN [User] U ON B.UID=U.UID WHERE  ReID=" + ID.ToString();
        //    //reader = command.ExecuteReader();
        //    num = 1;
        //    goto Label_0002;
        //}

        //public string GetBBSForManage(OleDbConnection objConn, int ID)
        //{
        //    bool flag;
        //    StringBuilder builder;
        //    OleDbDataReader reader;
        //    int num;
        //    goto Label_003B;
        //Label_0002:
        //    switch (num)
        //    {
        //        case 0:
        //        case 7:
        //        case 8:
        //            num = 3;
        //            goto Label_0002;

        //        case 1:
        //            //reader.Close();
        //            return builder.ToString();

        //        case 2:
        //            builder.Append(string.Concat(new object[] { "<div class=BBS_Box><span class=BBS_Speak style='float:left'></span><span class=BBS_User>", reader["UserName"], ":</span><span class=BBS_Title>", reader["Caption"].ToString(), "</span><span class=BBS_Date>[", reader["SubmitDate"], "]</span><div class=BBS_Content>", reader["Content"].ToString().Replace("\r\n", "<br />"), "</div></div>" }));
        //            num = 7;
        //            goto Label_0002;

        //        case 3:
        //            if (reader.Read())
        //            {
        //                num = 4;
        //            }
        //            else
        //            {
        //                num = 1;
        //            }
        //            goto Label_0002;

        //        case 4:
        //            if (!(reader["IsAdminRe"].ToString() == "False"))
        //            {
        //                builder.Append(string.Concat(new object[] { "<div class=BBS_Box><span class=BBS_AdminSpeak style='float:left'></span><span class=BBS_User>", reader["UserName"], ":</span><span class=BBS_Title>", reader["Caption"].ToString(), "</span><span class=BBS_Date>[", reader["SubmitDate"], "]</span><div class=BBS_Content>", reader["Content"].ToString().Replace("\r\n", "<br />"), "</div></div>" }));
        //                num = 8;
        //            }
        //            else
        //            {
        //                num = 2;
        //            }
        //            goto Label_0002;

        //        case 5:
        //            if (flag)
        //            {
        //                //OleDbCommand command=new OleDbCommand(); //new了一个新的实例
        //                //command.CommandText = "SELECT  B.*,U.UserName FROM BBS B INNER JOIN [User] U ON B.UID=U.UID WHERE  ReID=" + ID.ToString();
        //                //reader = command.ExecuteReader();
        //                num = 0;
        //            }
        //            else
        //            {
        //                num = 11;
        //            }
        //            goto Label_0002;

        //        case 6:
        //            if (!reader.Read())
        //            {
        //                goto Label_017C;
        //            }
        //            num = 10;
        //            goto Label_0002;

        //        case 9:
        //            goto Label_017C;

        //        case 10:
        //            if ((1 != 0) && (0 != 0))
        //            {
        //            }
        //            flag = true;
        //            builder.Append(string.Concat(new object[] { "<div class=BBS_Box><span class=BBS_Speak style='float:left'></span><span class=BBS_User>", reader["UserName"], ":</span><span class=BBS_Title>", reader["Caption"].ToString(), "</span><span class=BBS_Date>[", reader["SubmitDate"], "]</span><div class=BBS_Content>", reader["Content"].ToString().Replace("\r\n", "<br />"), "</div></div>" }));
        //            num = 9;
        //            goto Label_0002;

        //        case 11:
        //            return "";
        //    }
        //Label_003B:
        //    flag = false;
        //    builder = new StringBuilder();
        //    //reader = new OleDbCommand("SELECT TOP 1 B.*,U.UserName FROM BBS B INNER JOIN [User] U ON B.UID=U.UID WHERE B.ID=" + ID.ToString() + "  AND ReID=0", objConn).ExecuteReader();
        //    num = 6;
        //    goto Label_0002;
        //Label_017C:
        //    reader.Close();
        //    num = 5;
        //    goto Label_0002;
        //}

        //public string[] getBBSInfo(int ID)
        //{
        //    //OleDbDataReader reader = null; //赋初值
        //Label_0027:;
        //    string[] strArray = new string[] { "", "", "", "", "", "", "" };
        //    int num = 1;
        //Label_0002:
        //    switch (num)
        //    {
        //        case 0:
        //        case 5:
        //            //reader = this.b.ExecuteReader();
        //            num = 4;
        //            goto Label_0002;

        //        case 1:
        //            if (ID <= 0)
        //            {
        //                //this.b.CommandText = "SELECT TOP 1 Caption,Content,UserName,SubmitDate,ReDate,Status,ReID FROM BBS B INNER JOIN [User] U ON B.UID=U.UID WHERE ReID=0 ORDER BY ID DESC";

        //                num = 5;
        //            }
        //            else
        //            {
        //                num = 6;
        //            }
        //            goto Label_0002;

        //        case 2:
        //            //strArray[0] = reader["Caption"].ToString();
        //            //strArray[1] = reader["Content"].ToString();
        //            //strArray[2] = reader["UserName"].ToString();
        //            //strArray[3] = reader["SubmitDate"].ToString();
        //            //strArray[4] = reader["ReDate"].ToString();
        //            //strArray[5] = reader["Status"].ToString();
        //            //strArray[6] = reader["ReID"].ToString();
        //            num = 3;
        //            goto Label_0002;

        //        case 3:
        //            break;

        //        case 4:
        //            //if (!reader.Read())
        //            //{
        //                break;
        //            //}
        //            num = 2;
        //            goto Label_0002;

        //        case 6:
        //            //this.b.CommandText = "SELECT TOP 1 Caption,Content,UserName,SubmitDate,ReDate,Status,ReID FROM BBS B INNER JOIN [User] U ON B.UID=U.UID WHERE ID=" + ID.ToString();
        //            num = 0;
        //            goto Label_0002;

        //        default:
        //            goto Label_0027;
        //    }
        //    //reader.Close();
        //    return strArray;
        //}

        public string[] getEmailAccount()
        {
        Label_0017:
            if ((1 != 0) && (0 != 0))
            {
            }
            string[] strArray = new string[] { "" };
            //this.b.CommandText = "SELECT TOP 1 Par FROM OQSSSysInfo WHERE ParName='Email'";
            SqlDataReader reader = new SClass().GetParBySysBaseInfo("Email");
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
                    strArray = Regex.Split(reader["Par"].ToString(), @"{\$\$}", RegexOptions.IgnoreCase);
                    num = 1;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            reader.Close();
            return strArray;
        }

        //public void saveBBS(string sTitle, string sContent, int ReID, int UID, int blnIsAdmintRe, int intStatus)
        //{
        //Label_001F:;
        //    //this.b.CommandText = "INSERT INTO BBS(Caption,Content,ReID,UID,SubmitDate,IsAdminRe,Status) VALUES('" + sTitle + "','" + sContent.Replace("'", "''") + "'," + ReID.ToString() + "," + UID.ToString() + ",'" + DateTime.Now.ToString() + "'," + blnIsAdmintRe.ToString() + "," + intStatus.ToString() + ")";
        //    //this.b.ExecuteNonQuery();
        //    int num = 4;
        //Label_0002:
        //    switch (num)
        //    {
        //        case 0:
        //            num = 3;
        //            goto Label_0002;

        //        case 1:
        //            //this.b.CommandText = " UPDATE BBS SET Status=3,ReDate='" + DateTime.Now.ToString() + "' WHERE ID=" + ReID.ToString();
        //            //this.b.ExecuteNonQuery();
        //            return;

        //        case 2:
        //            break;

        //        case 3:
        //            if ((1 != 0) && (0 != 0))
        //            {
        //            }
        //            if (blnIsAdmintRe != 0)
        //            {
        //                //this.b.CommandText = " UPDATE BBS SET Status=2,ReDate='" + DateTime.Now.ToString() + "' WHERE ID=" + ReID.ToString();
        //                //this.b.ExecuteNonQuery();
        //                num = 2;
        //            }
        //            else
        //            {
        //                num = 1;
        //            }
        //            goto Label_0002;

        //        case 4:
        //            if (ReID <= 0)
        //            {
        //                break;
        //            }
        //            num = 0;
        //            goto Label_0002;

        //        default:
        //            goto Label_001F;
        //    }
        //}

        public string saveStyleToLib(long UID, string sStyleName, string sStyleContent)
        {
            string str;
            string str2 = null; //赋初值
            SqlDataReader reader = null; //赋初值
        Label_003F:
            str = "";
            int num = 10;
        Label_0002:
            switch (num)
            {
                case 0:
                    if (sStyleName.Length < 0x33)
                    {
                        //str2 = FormsAuthentication.HashPasswordForStoringInConfigFile(sStyleContent, "MD5").ToLower();

                        str2 = new Business.Helper.RequestQueryString().EncryptQueryString(sStyleContent); //框架加密
                        //this.b.CommandText = "SELECT TOP 1 ID FROM StyleLib WHERE UID=" + UID.ToString() + " AND FingerPrint='" + str2 + "'";
                        reader = new SClass().GetStyleLibByParas(UID.ToString(), str2);

                        num = 2;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_0002;

                case 1:
                    num = 5;
                    goto Label_0002;

                case 2:
                    if (!reader.Read())
                    {
                        //this.b.CommandText = "INSERT INTO StyleLib(UID,StyleName,StyleContent,FingerPrint) VALUES(" + UID.ToString() + ",'" + sStyleName + "','" + sStyleContent.Replace("'", "''") + "','" + str2 + "')";
                        num = 7;
                    }
                    else
                    {
                        num = 4;
                    }
                    goto Label_0002;

                case 3:
                    return "未成功入库:样式名长度大于50";

                case 4:
                    //this.b.CommandText = "";
                    str = "未成功入库，因为在库中已经有相同样式内容的记录";
                    num = 9;
                    goto Label_0002;

                case 5:
                    if (!(sStyleName == ""))
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 11;
                    }
                    goto Label_0002;

                case 6:
                    new SClass().InsertStyleLib(UID.ToString(), sStyleName, sStyleContent.Replace("'", "''"), str2);
                    str = "样式入库完成";
                    num = 8;
                    goto Label_0002;

                case 7:
                case 9:
                    reader.Dispose();
                    num = 12;
                    goto Label_0002;

                case 8:
                    return str;

                case 10:
                    if (sStyleContent == "")
                    {
                        break;
                    }
                    num = 1;
                    goto Label_0002;

                case 11:
                    break;

                case 12:
                    //if (!(this.b.CommandText != ""))
                    //{
                    //    return str;
                    //}
                    num = 6;
                    goto Label_0002;

                default:
                    goto Label_003F;
            }
            return "参数为空";
        }

        public long IID_
        {
            get
            {
                return this.d;
            }
            set
            {
                this.d = value;
            }
        }

        //public OleDbCommand objComm_
        //{
        //    get
        //    {
        //        return this.b;
        //    }
        //    set
        //    {
        //        this.b = value;
        //    }
        //}

        //public OleDbConnection objConn_
        //{
        //    get
        //    {
        //        return this.a;
        //    }
        //    set
        //    {
        //        this.objConn_ = value;
        //    }
        //}

        public long SID_
        {
            get
            {
                return this.c;
            }
            set
            {
                this.c = value;
            }
        }

        public long UID_
        {
            get
            {
                return this.e;
            }
            set
            {
                this.e = value;
            }
        }
    }
}

