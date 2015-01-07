namespace shareclass
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Text;
    using System.Web;

    //赋值一些必要的初值

    public class depmanage
    {
        private OleDbConnection a;
        private OleDbCommand b = new OleDbCommand();
        private int c;
        private int d;
        private int e;
        public int intCount;

        public depmanage(OleDbConnection objConn__, int SID__, int UID__)
        {
            this.c = SID__;
            this.a = objConn__;
            this.b.Connection = this.a;
            this.e = UID__;
        }

        public void changeDepName(int intDID, string sDepName)
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            this.b.CommandText = "UPDATE Dep SET DepName='" + sDepName + "' WHERE DID=" + intDID.ToString();
            HttpContext.Current.Response.Write(this.b.CommandText);
            this.b.ExecuteNonQuery();
        }

        public void changeDepSort(int intDID, string sDirection, int intParentID)
        {
            OleDbDataReader reader;
            int num;
            int num2;
            goto Label_001F;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (!reader.Read())
                    {
                        goto Label_0121;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    intParentID = Convert.ToInt32(reader["ParentID"]);
                    num = Convert.ToInt32(reader["Sort"]);
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    goto Label_0121;

                case 3:
                    this.b.CommandText = "UPDATE DEP SET Sort=Sort+1 WHERE DID=" + intDID.ToString() + " AND Sort<(SELECT MAX(Sort) FROM Dep WHERE ParentID=" + intParentID.ToString() + ")";
                    this.b.ExecuteNonQuery();
                    this.b.CommandText = " UPDATE DEP SET Sort=Sort-1 WHERE DID<>" + intDID.ToString() + " AND ParentID=" + intParentID.ToString() + " AND Sort=" + Convert.ToString((int) (num + 1));
                    this.b.ExecuteNonQuery();
                    return;

                case 4:
                    if (!(sDirection == "Down"))
                    {
                        this.b.CommandText = "UPDATE DEP  SET Sort=Sort-1 WHERE DID=" + intDID.ToString() + " AND Sort>(SELECT MIN(Sort) FROM Dep WHERE ParentID=" + intParentID.ToString() + ")";
                        this.b.ExecuteNonQuery();
                        this.b.CommandText = " UPDATE DEP  SET Sort=Sort+1 WHERE DID<>" + intDID.ToString() + " AND ParentID=" + intParentID.ToString() + " AND Sort=" + Convert.ToString((int) (num - 1));
                        this.b.ExecuteNonQuery();
                        return;
                    }
                    num2 = 3;
                    goto Label_0002;
            }
        Label_001F:
            num = 0;
            this.b.CommandText = "SELECT TOP 1 ParentID,Sort FROM Dep WHERE DID=" + intDID.ToString();
            reader = this.b.ExecuteReader();
            num2 = 0;
            goto Label_0002;
        Label_0121:
            reader.Close();
            num2 = 4;
            goto Label_0002;
        }

        public void createDep(int intParentID, string sDepName, int intSort, int intLevel)
        {
            OleDbDataReader reader = null; //赋初值
            int num = 1;
        Label_000D:
            switch (num)
            {
                case 0:
                    break;

                case 2:
                    try
                    {
                        intSort = Convert.ToInt32(reader["Sort"]) + 1;
                    }
                    catch
                    {
                        intSort = 1;
                    }
                    goto Label_0181;

                case 3:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (!reader.Read())
                    {
                        goto Label_00F0;
                    }
                    num = 8;
                    goto Label_000D;

                case 4:
                    this.b.CommandText = "SELECT DepLevel FROM Dep WHERE DID=" + intParentID.ToString();
                    reader = this.b.ExecuteReader();
                    num = 3;
                    goto Label_000D;

                case 5:
                    this.b.CommandText = "SELECT MAX(Sort) Sort FROM Dep WHERE ParentID=" + intParentID.ToString();
                    reader = this.b.ExecuteReader();
                    num = 6;
                    goto Label_000D;

                case 6:
                    if (!reader.Read())
                    {
                        goto Label_0181;
                    }
                    num = 2;
                    goto Label_000D;

                case 7:
                    goto Label_019A;

                case 8:
                    intLevel = Convert.ToInt32(reader["DepLevel"]) + 1;
                    num = 9;
                    goto Label_000D;

                case 9:
                    goto Label_00F0;

                case 10:
                    if (intLevel != 0)
                    {
                        goto Label_019A;
                    }
                    num = 4;
                    goto Label_000D;

                default:
                    if (intSort == 0)
                    {
                        num = 5;
                        goto Label_000D;
                    }
                    break;
            }
            num = 10;
            goto Label_000D;
        Label_00F0:
            reader.Close();
            num = 7;
            goto Label_000D;
        Label_0181:
            reader.Close();
            num = 0;
            goto Label_000D;
        Label_019A:;
            this.b.CommandText = "INSERT INTO Dep(DepName,ParentID,Sort,DepLevel) VALUES('" + sDepName + "'," + intParentID.ToString() + "," + intSort.ToString() + "," + intLevel.ToString() + ")";
            this.b.ExecuteNonQuery();
        }

        public void delbbs(int intID, int UID)
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            new OleDbCommand("DELETE FROM BBS WHERE (ID=" + intID.ToString() + " AND UID=" + UID.ToString() + ") OR (ReID=" + intID.ToString() + " AND UID=" + UID.ToString() + ")", this.a).ExecuteNonQuery();
        }

        public void delbbs(string sIDs, int UID)
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            new OleDbCommand("DELETE FROM BBS WHERE ID IN(" + sIDs + "0) AND UID=" + UID.ToString(), this.a).ExecuteNonQuery();
        }

        public void delbbsForManage(int intID)
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            new OleDbCommand("DELETE FROM BBS WHERE ID=" + intID.ToString() + " OR ReID=" + intID.ToString(), this.a).ExecuteNonQuery();
        }

        public void delDep(int intDID, bool delAllChild)
        {
            string str;
            int num = 0; //赋初值
            OleDbDataReader reader = null; //赋初值
            int num2;
            goto Label_0023;
        Label_0002:
            switch (num2)
            {
                case 0:
                    goto Label_0063;

                case 1:
                    if ((1 == 0) || (0 == 0))
                    {
                        this.b.CommandText = "DELETE FROM Dep WHERE DID IN(" + str + ")  AND ParentID>0";
                        num2 = 4;
                        goto Label_0002;
                    }
                    goto Label_0063;

                case 2:
                case 4:
                    this.b.CommandText = "DELETE FROM Dep WHERE DID=" + intDID.ToString() + " AND ParentID>0";
                    this.b.ExecuteNonQuery();
                    return;

                case 3:
                    if (!delAllChild)
                    {
                        num = 0;
                        this.b.CommandText = "SELECT MAX(Sort) FROM Dep WHERE DID=(SELECT TOP 1 ParentID FROM Dep WHERE DID=" + intDID.ToString() + ")";
                        reader = this.b.ExecuteReader();
                        num2 = 5;
                    }
                    else
                    {
                        num2 = 1;
                    }
                    goto Label_0002;

                case 5:
                    if (!reader.Read())
                    {
                        goto Label_00A3;
                    }
                    num2 = 0;
                    goto Label_0002;
            }
        Label_0023:
            str = this.getAllChild(this.getDepTable(), intDID, false, "") + "-1";
            num2 = 3;
            goto Label_0002;
        Label_0063:
            try
            {
                num = Convert.ToInt32(reader[0]);
            }
            catch
            {
            }
        Label_00A3:
            reader.Close();
            this.b.CommandText = "UPDATE Dep Set Sort=Sort+" + num.ToString() + " WHERE ParentID=" + intDID.ToString();
            this.b.ExecuteNonQuery();
            this.b.CommandText = "UPDATE Dep SET DepLevel=DepLevel-1,ParentID=(SELECT TOP 1 ParentID FROM DEP WHERE DID=" + intDID.ToString() + ") WHERE DID IN(" + str + ")";
            this.b.ExecuteNonQuery();
            num2 = 2;
            goto Label_0002;
        }

        public void delGU(int intGUID)
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            new OleDbCommand("DELETE FROM GUTable WHERE GUID=" + intGUID.ToString(), this.a).ExecuteNonQuery();
        }

        public void delGU(string sIDs)
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            new OleDbCommand("DELETE FROM GUTable WHERE GUID IN(" + sIDs + "0)", this.a).ExecuteNonQuery();
        }

        public void delStyle(int intID, int UID)
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            new OleDbCommand("DELETE FROM StyleLib WHERE ID=" + intID.ToString() + " AND UID=" + UID.ToString(), this.a).ExecuteNonQuery();
        }

        public void delStyle(string sIDs, int UID)
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            new OleDbCommand("DELETE FROM StyleLib WHERE ID IN(" + sIDs + "0) AND UID=" + UID.ToString(), this.a).ExecuteNonQuery();
        }

        public string getAllChild(DataTable dt, int intDID, bool finished, string sResult)
        {
            int num;
            int num2;
            goto Label_0027;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (!(dt.Rows[num]["ParentID"].ToString() == intDID.ToString()))
                    {
                        goto Label_003B;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    sResult = sResult + dt.Rows[num]["DID"].ToString() + ",";
                    sResult = this.getAllChild(dt, Convert.ToInt32(dt.Rows[num]["DID"]), false, sResult);
                    num2 = 3;
                    goto Label_0002;

                case 2:
                    return sResult;

                case 3:
                    goto Label_003B;

                case 4:
                    if (num < dt.Rows.Count)
                    {
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 5:
                case 6:
                    num2 = 4;
                    goto Label_0002;
            }
        Label_0027:
            num = 0;
            num = 0;
            num2 = 6;
            goto Label_0002;
        Label_003B:
            num++;
            num2 = 5;
            goto Label_0002;
        }

        public string getBBSList(int UID, int intPageNo, int intPageSize, int intRecordCount)
        {
            StringBuilder builder;
            OleDbCommand command;
            OleDbDataReader reader;
            int num;
            int num2;
            int num3 = 0; //赋初值
            string[] strArray = null; //赋初值
            string str = null; //赋初值
            string str2 = null; //赋初值
            int num4;
            goto Label_009E;
        Label_0005:
            switch (num4)
            {
                case 0:
                    num4 = 7;
                    goto Label_0005;

                case 1:
                case 5:
                case 6:
                case 7:
                case 0x1c:
                    goto Label_01E5;

                case 2:
                    num4 = 0x1a;
                    goto Label_0005;

                case 3:
                    num4 = 0x1d;
                    goto Label_0005;

                case 4:
                    num4 = 15;
                    goto Label_0005;

                case 8:
                    if ((1 == 0) || (0 == 0))
                    {
                        goto Label_01E5;
                    }
                    goto Label_02F8;

                case 9:
                    intRecordCount = this.getRecordCount();
                    num4 = 14;
                    goto Label_0005;

                case 10:
                    if (strArray[num2] == null)
                    {
                        goto Label_02C1;
                    }
                    num4 = 0x16;
                    goto Label_0005;

                case 11:
                case 0x17:
                    num4 = 20;
                    goto Label_0005;

                case 12:
                    goto Label_02C1;

                case 13:
                    str2 = reader["Status"].ToString();
                    if (str2 == null)
                    {
                        goto Label_01E5;
                    }
                    num4 = 0x12;
                    goto Label_0005;

                case 14:
                    goto Label_04E6;

                case 15:
                    if (str2 == "4")
                    {
                        str = "<span class='Share_Item' title='已回'></span>";
                        num4 = 6;
                    }
                    else
                    {
                        num4 = 0;
                    }
                    goto Label_0005;

                case 0x10:
                case 0x20:
                    num4 = 0x18;
                    goto Label_0005;

                case 0x11:
                    num = intRecordCount - ((num3 - 1) * intPageSize);
                    num4 = 0x1b;
                    goto Label_0005;

                case 0x12:
                    num4 = 0x15;
                    goto Label_0005;

                case 0x13:
                case 0x1b:
                    strArray = new string[intPageSize];
                    command.CommandText = "SELECT TOP " + num.ToString() + " ID,Caption,SubmitDate,Status,ReID,ReDate FROM (SELECT TOP " + Convert.ToString((int) (intPageSize * intPageNo)) + " * FROM BBS WHERE UID=" + UID.ToString() + " AND ReID=0 ORDER BY ID DESC) V ORDER BY ID ";
                    reader = command.ExecuteReader();
                    str = "";
                    num4 = 0x17;
                    goto Label_0005;

                case 20:
                    if (reader.Read())
                    {
                        goto Label_02F8;
                    }
                    num4 = 0x19;
                    goto Label_0005;

                case 0x15:
                    if (str2 == "0")
                    {
                        str = "<span class='BBS_Speak' title='未回'></span>";
                        num4 = 8;
                    }
                    else
                    {
                        num4 = 3;
                    }
                    goto Label_0005;

                case 0x16:
                    builder.Append(strArray[num2]);
                    num4 = 12;
                    goto Label_0005;

                case 0x18:
                    if (num2 >= 0)
                    {
                        num4 = 10;
                    }
                    else
                    {
                        num4 = 30;
                    }
                    goto Label_0005;

                case 0x19:
                    num2 = strArray.Length - 1;
                    num4 = 0x10;
                    goto Label_0005;

                case 0x1a:
                    if (str2 == "3")
                    {
                        str = "<span class='BBS_Continue' title='续问'></span>";
                        num4 = 5;
                    }
                    else
                    {
                        num4 = 4;
                    }
                    goto Label_0005;

                case 0x1d:
                    if (str2 == "1")
                    {
                        str = "<span class='Lamp' title='已阅'></span>";
                        num4 = 1;
                    }
                    else
                    {
                        num4 = 0x23;
                    }
                    goto Label_0005;

                case 30:
                    return ("<table  border=0 cellpadding=0 cellspacing=0 width=100%><tr class='Share_BrightBlue' style='font-weight:bold'><td  style='width:40px'>状态</td><td>主题</td><td  style='width:300px'>发布/最后回复日期</td><td align=center>查看</td><td  align=center>删除</td></tr>" + builder.ToString() + "</table>");

                case 0x1f:
                    if (intRecordCount != -1)
                    {
                        goto Label_04E6;
                    }
                    num4 = 9;
                    goto Label_0005;

                case 0x21:
                    if (num3 != intPageNo)
                    {
                        num = intPageSize;
                        num4 = 0x13;
                    }
                    else
                    {
                        num4 = 0x11;
                    }
                    goto Label_0005;

                case 0x22:
                    if (str2 == "2")
                    {
                        str = "<span class='BBS_OK' title='已回'></span>";
                        num4 = 0x1c;
                    }
                    else
                    {
                        num4 = 2;
                    }
                    goto Label_0005;

                case 0x23:
                    num4 = 0x22;
                    goto Label_0005;
            }
        Label_009E:
            builder = new StringBuilder();
            command = new OleDbCommand("", this.a);
            reader = null;
            num = 0;
            num2 = 0;
            num4 = 0x1f;
            goto Label_0005;
        Label_01E5:;
            strArray[num2] = string.Concat(new object[] { "<tr style='height:25px' onmouseout=switchShow(this,0) onmouseover=switchShow(this,1)><td>", str, "</td><td><a href='viewbbs.aspx?id=", reader["ID"], "'>", reader["Caption"], "</a></td><td>", reader["SubmitDate"], "/", reader["ReDate"], "</td><td  style='width:60px'><span   onclick='viewbbs(", reader["ID"], ")' class='view'>查看</span></td><td style='width:60px'><span class='del' onclick='delbbs(", reader["ID"], ",0)'>删除</span></td></tr>" });
            num2++;
            num4 = 11;
            goto Label_0005;
        Label_02C1:
            num2--;
            num4 = 0x20;
            goto Label_0005;
        Label_02F8:
            num4 = 13;
            goto Label_0005;
        Label_04E6:
            num3 = this.getMaxPage(intRecordCount, intPageSize);
            num4 = 0x21;
            goto Label_0005;
        }

        public string getBBSListForManage(int intPageNo, int intPageSize, int intRecordCount)
        {
            StringBuilder builder;
            OleDbCommand command;
            OleDbDataReader reader;
            int num;
            int num2;
            int num3 = 0; //赋初值
            string[] strArray = null; //赋初值
            string str = null; //赋初值
            string str2 = null; //赋初值
            int num4;
            goto Label_009E;
        Label_0005:
            switch (num4)
            {
                case 0:
                case 2:
                    num4 = 5;
                    goto Label_0005;

                case 1:
                case 4:
                case 8:
                case 15:
                case 0x1b:
                case 0x1c:
                    goto Label_01CF;

                case 3:
                    if (str2 == "4")
                    {
                        str = "<span class='Share_Item' title='已回'></span>";
                        num4 = 1;
                    }
                    else
                    {
                        num4 = 0x23;
                    }
                    goto Label_0005;

                case 5:
                    if (num2 >= 0)
                    {
                        num4 = 0x10;
                    }
                    else
                    {
                        num4 = 0x1f;
                    }
                    goto Label_0005;

                case 6:
                    num4 = 0x22;
                    goto Label_0005;

                case 7:
                    goto Label_04E8;

                case 9:
                    num4 = 0x11;
                    goto Label_0005;

                case 10:
                    if (intRecordCount != -1)
                    {
                        goto Label_04E8;
                    }
                    num4 = 0x1d;
                    goto Label_0005;

                case 11:
                    goto Label_02CA;

                case 12:
                    if (str2 == "1")
                    {
                        str = "<span class='Lamp' title='已阅'></span>";
                        num4 = 15;
                    }
                    else
                    {
                        num4 = 9;
                    }
                    goto Label_0005;

                case 13:
                    if (reader.Read())
                    {
                        num4 = 0x18;
                    }
                    else
                    {
                        num4 = 0x17;
                    }
                    goto Label_0005;

                case 14:
                case 0x13:
                    strArray = new string[intPageSize];
                    command.CommandText = "SELECT TOP " + num.ToString() + " ID,Caption,SubmitDate,Status,ReID,ReDate,UserName FROM (SELECT TOP " + Convert.ToString((int) (intPageSize * intPageNo)) + " B.*,U.UserName FROM BBS AS B INNER JOIN [User] AS U ON B.UID=U.UID WHERE  ReID=0 ORDER BY ID DESC) V ORDER BY ID ";
                    reader = command.ExecuteReader();
                    str = "";
                    num4 = 20;
                    goto Label_0005;

                case 0x10:
                    if (strArray[num2] == null)
                    {
                        goto Label_02CA;
                    }
                    num4 = 0x1a;
                    goto Label_0005;

                case 0x11:
                    if (str2 == "2")
                    {
                        str = "<span class='BBS_OK' title='已回'></span>";
                        num4 = 0x1b;
                    }
                    else
                    {
                        num4 = 0x19;
                    }
                    goto Label_0005;

                case 0x12:
                    num4 = 12;
                    goto Label_0005;

                case 20:
                case 0x21:
                    num4 = 13;
                    goto Label_0005;

                case 0x15:
                    num = intRecordCount - ((num3 - 1) * intPageSize);
                    num4 = 0x13;
                    goto Label_0005;

                case 0x16:
                    if (num3 != intPageNo)
                    {
                        num = intPageSize;
                        num4 = 14;
                    }
                    else
                    {
                        num4 = 0x15;
                    }
                    goto Label_0005;

                case 0x17:
                    if ((1 == 0) || (0 == 0))
                    {
                        num2 = strArray.Length - 1;
                        num4 = 0;
                        goto Label_0005;
                    }
                    goto Label_0561;

                case 0x18:
                    str2 = reader["Status"].ToString();
                    if (str2 == null)
                    {
                        goto Label_01CF;
                    }
                    num4 = 6;
                    goto Label_0005;

                case 0x19:
                    num4 = 0x20;
                    goto Label_0005;

                case 0x1a:
                    builder.Append(strArray[num2]);
                    num4 = 11;
                    goto Label_0005;

                case 0x1d:
                    intRecordCount = this.getRecordCount();
                    num4 = 7;
                    goto Label_0005;

                case 30:
                    num4 = 3;
                    goto Label_0005;

                case 0x1f:
                    goto Label_0561;

                case 0x20:
                    if (str2 == "3")
                    {
                        str = "<span class='BBS_Continue' title='续问'></span>";
                        num4 = 0x1c;
                    }
                    else
                    {
                        num4 = 30;
                    }
                    goto Label_0005;

                case 0x22:
                    if (str2 == "0")
                    {
                        str = "<span class='BBS_Speak' title='未回'></span>";
                        num4 = 4;
                    }
                    else
                    {
                        num4 = 0x12;
                    }
                    goto Label_0005;

                case 0x23:
                    num4 = 8;
                    goto Label_0005;
            }
        Label_009E:
            builder = new StringBuilder();
            command = new OleDbCommand("", this.a);
            reader = null;
            num = 0;
            num2 = 0;
            num4 = 10;
            goto Label_0005;
        Label_01CF:;
            strArray[num2] = string.Concat(new object[] { 
                "<tr style='height:25px' onmouseout=switchShow(this,0) onmouseover=switchShow(this,1)><td>", str, "</td><td><a href='viewbbs.aspx?id=", reader["ID"], "'><span style='font-size:14px'>", reader["Caption"].ToString(), "</span>----[", reader["UserName"], "]</a></td><td>", reader["SubmitDate"], "/", reader["ReDate"], "</td><td  style='width:60px'><span   onclick='viewbbs(", reader["ID"], ")' class='view'>查看</span></td><td style='width:60px'><span class='del' onclick='delbbs(", reader["ID"], 
                ",0)'>删除</span></td></tr>"
             });
            num2++;
            num4 = 0x21;
            goto Label_0005;
        Label_02CA:
            num2--;
            num4 = 2;
            goto Label_0005;
        Label_04E8:
            num3 = this.getMaxPage(intRecordCount, intPageSize);
            num4 = 0x16;
            goto Label_0005;
        Label_0561:
            return ("<table  border=0 cellpadding=0 cellspacing=0 width=100%><tr style='font-weight:bold'><td  style='width:40px'>状态</td><td>主题</td><td  style='width:300px'>发布/最后回复日期</td><td align=center>查看</td><td  align=center>删除</td></tr>" + builder.ToString() + "</table>");
        }

        public DataTable getDepTable()
        {
            if ((1 != 0) && (0 != 0))
            {
            }
            this.b.CommandText = "SELECT * FROM Dep ORDER BY DepLevel,Sort";
            OleDbDataAdapter adapter = new OleDbDataAdapter(this.b);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Dep");
            return dataSet.Tables[0];
        }

        public string getGUIDAndDep(int SID, int UID)
        {
            string str;
        Label_0017:
            str = "";
            OleDbDataReader reader = new OleDbCommand("SELECT TOP 1 ExpandContent FROM SurveyExpand WHERE UID=" + UID.ToString() + " AND SID=" + SID.ToString() + " AND ExpandType=5", this.a).ExecuteReader();
            int num = 2;
        Label_0002:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    str = reader[0].ToString();
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num = 0;
                    goto Label_0002;

                case 2:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num = 1;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            reader.Close();
            return str;
        }

        public string[] getGUIDJs_NotPage(int SID)
        {
            StringBuilder builder;
            StringBuilder builder2;
            OleDbDataReader reader;
            int num;
            int num2;
            if ((1 == 0) || (0 == 0))
            {
                goto Label_001F;
            }
        Label_0006:
            switch (num2)
            {
                case 0:
                case 3:
                    num2 = 1;
                    goto Label_0006;

                case 1:
                    if (reader.Read())
                    {
                        builder.Append(string.Concat(new object[] { "arrGU[", num.ToString(), "] = [", reader["GUID"], ",'", reader["GUUserName"], "','", reader["Name"], "','','", reader["Dep"], "'];\n" }));
                        builder2.Append(reader["GUID"] + ",");
                        num++;
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0006;

                case 2:
                    reader.Close();
                    return new string[] { ("var sGUIDStr='" + builder2.ToString() + "';\n"), builder.ToString() };
            }
        Label_001F:
            builder = new StringBuilder();
            builder2 = new StringBuilder();
            builder.Append("var arrGU = new Array();");
            reader = new OleDbCommand("SELECT G.GUID,G.GUUserName,G.Name,G.Dep FROM GUTable AS G INNER JOIN enAnswerList AS E ON G.GUID=E.GUID WHERE E.SID=" + SID.ToString(), this.a).ExecuteReader();
            num = 0;
            num2 = 3;
            goto Label_0006;
        }

        public string getGUList(int intPageNo, int intPageSize, int intRecordCount)
        {
            string[] strArray;
            StringBuilder builder;
            OleDbCommand command;
            OleDbDataReader reader;
            int num;
            int num2;
            int num3 = 0; //赋初值
            string str = null; //赋初值
            int num4;
            goto Label_0063;
        Label_0002:
            switch (num4)
            {
                case 0:
                case 2:
                    command.CommandText = "SELECT TOP " + num.ToString() + " * FROM (SELECT TOP " + Convert.ToString((int) (intPageSize * intPageNo)) + " GUID,GULevel,DepName,State,GUUserName,Name,RegDate FROM GUTable G LEFT OUTER JOIN Dep D ON G.Dep=D.DID ORDER BY GUID DESC) G ORDER BY GUID";
                    reader = command.ExecuteReader();
                    str = "";
                    num4 = 8;
                    goto Label_0002;

                case 1:
                    goto Label_02F5;

                case 3:
                case 5:
                    num4 = 0x10;
                    goto Label_0002;

                case 4:
                    num = intRecordCount - ((num3 - 1) * intPageSize);
                    num4 = 2;
                    goto Label_0002;

                case 6:
                    str = "启用";
                    num4 = 13;
                    goto Label_0002;

                case 7:
                    goto Label_0249;

                case 8:
                case 0x15:
                    num4 = 12;
                    goto Label_0002;

                case 9:
                    if (intRecordCount != -1)
                    {
                        goto Label_0249;
                    }
                    num4 = 14;
                    goto Label_0002;

                case 10:
                    if (num3 != intPageNo)
                    {
                        num = intPageSize;
                        num4 = 0;
                    }
                    else
                    {
                        num4 = 4;
                    }
                    goto Label_0002;

                case 11:
                case 13:
                    strArray[num2] = string.Concat(new object[] { 
                        "<tr style='height:25px' onmouseout=switchShow(this,0) onmouseover=switchShow(this,1)><td><input type='checkbox' value='", reader["GUID"], "' name=selectuser id='selectuser", Convert.ToString((int) (num2 + 1)), "'><td>", reader["GUUserName"].ToString(), "</td><td>", reader["Name"], "</td><td>", reader["DepName"], "</td><td>", reader["RegDate"], "</td><td>", reader["GULevel"], "</td><td>", str, 
                        "</td><td><span class='view'  onclick='viewgu(", reader["GUID"], ")'>查看</span></td><td><span   onclick='modifygu(", reader["GUID"], ")' class='switchuser'>修改</span></td><td><span class='del' onclick='delgu(", reader["GUID"], ",0)'>删除</span></td></tr>"
                     });
                    num2++;
                    num4 = 0x15;
                    goto Label_0002;

                case 12:
                    if (reader.Read())
                    {
                        num4 = 20;
                    }
                    else
                    {
                        num4 = 0x11;
                    }
                    goto Label_0002;

                case 14:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    intRecordCount = this.getRecordCount();
                    num4 = 7;
                    goto Label_0002;

                case 15:
                    return ("<table  border=0 cellpadding=0 cellspacing=0 width=100%><tr><td><input type='checkbox' value='0' name=selectuser id='selectuser0' onclick='selectdata(0)'></td><td>用户名</td><td>姓名</td><td>部门</td><td>创建日期</td><td>等级</td><td>状态</td><td>详细</td><td>修改</td><td>删除</td></tr>" + builder.ToString() + "</table>");

                case 0x10:
                    if (num2 >= 0)
                    {
                        num4 = 0x13;
                    }
                    else
                    {
                        num4 = 15;
                    }
                    goto Label_0002;

                case 0x11:
                    num2 = strArray.Length - 1;
                    num4 = 3;
                    goto Label_0002;

                case 0x12:
                    builder.Append(strArray[num2]);
                    num4 = 1;
                    goto Label_0002;

                case 0x13:
                    if (strArray[num2] == null)
                    {
                        goto Label_02F5;
                    }
                    num4 = 0x12;
                    goto Label_0002;

                case 20:
                    if (!(reader["State"].ToString() == "True"))
                    {
                        str = "禁用";
                        num4 = 11;
                    }
                    else
                    {
                        num4 = 6;
                    }
                    goto Label_0002;
            }
        Label_0063:
            strArray = new string[intPageSize];
            builder = new StringBuilder();
            command = new OleDbCommand("", this.a);
            reader = null;
            num = 0;
            num2 = 0;
            num4 = 9;
            goto Label_0002;
        Label_0249:
            num3 = this.getMaxPage(intRecordCount, intPageSize);
            num4 = 10;
            goto Label_0002;
        Label_02F5:
            num2--;
            num4 = 5;
            goto Label_0002;
        }

        public string[] getGUList(int intPageNo, int intPageSize, int intRecordCount, bool notUser)
        {
            string[] strArray;
            StringBuilder builder;
            StringBuilder builder2 = null; //赋初值
            OleDbCommand command;
            OleDbDataReader reader;
            int num;
            int num2;
            int num3 = 0; //赋初值
            int num4;
            goto Label_0053;
        Label_0002:
            switch (num4)
            {
                case 0:
                    if (strArray[num2] == null)
                    {
                        goto Label_0122;
                    }
                    num4 = 10;
                    goto Label_0002;

                case 1:
                case 12:
                    num4 = 3;
                    goto Label_0002;

                case 2:
                    goto Label_0122;

                case 3:
                    if (num2 >= 0)
                    {
                        num4 = 0;
                    }
                    else
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num4 = 8;
                    }
                    goto Label_0002;

                case 4:
                case 7:
                    num4 = 11;
                    goto Label_0002;

                case 5:
                    intRecordCount = this.getRecordCount();
                    num4 = 0x11;
                    goto Label_0002;

                case 6:
                case 9:
                    command.CommandText = "SELECT TOP " + num.ToString() + " GUID,GUUserName,Name,DepName,Dep FROM (SELECT TOP " + Convert.ToString((int) (intPageSize * intPageNo)) + " GUID,GULevel,DepName,Dep,State,GUUserName,Name,RegDate FROM GUTable G LEFT OUTER JOIN Dep D ON G.Dep=D.DID ORDER BY GUID DESC) G ORDER BY GUID";
                    reader = command.ExecuteReader();
                    num4 = 4;
                    goto Label_0002;

                case 8:
                    return new string[] { ("<table  border=0 cellpadding=0 cellspacing=0 width=100%><tr><td><input type='checkbox' value='0' name=selectuser id='selectuser0' onclick='selectdata(0)'></td><td>用户名</td><td>姓名</td><td>部门</td></tr>" + builder.ToString() + "</table>"), builder2.ToString() };

                case 10:
                    builder.Append(strArray[num2]);
                    num4 = 2;
                    goto Label_0002;

                case 11:
                    if (reader.Read())
                    {
                        strArray[num2] = string.Concat(new object[] { "<tr style='height:25px' onmouseout=switchShow(this,0) onmouseover=switchShow(this,1)><td><input type='checkbox' value='", reader["GUID"], "' name=selectuser id='selectuser", Convert.ToString((int) (num2 + 1)), "'><td>", reader["GUUserName"].ToString(), "</td><td>", reader["Name"], "</td><td>", reader["DepName"], "</td></tr>" });
                        builder2.Append(string.Concat(new object[] { "arrGU[", num2.ToString(), "] = [", reader["GUID"], ",'", reader["GUUserName"], "','", reader["Name"], "','", reader["DepName"], "','", reader["Dep"], "'];\n" }));
                        num2++;
                        num4 = 7;
                    }
                    else
                    {
                        num4 = 13;
                    }
                    goto Label_0002;

                case 13:
                    num2 = strArray.Length - 1;
                    num4 = 1;
                    goto Label_0002;

                case 14:
                    num = intRecordCount - ((num3 - 1) * intPageSize);
                    num4 = 9;
                    goto Label_0002;

                case 15:
                    if (intRecordCount != -1)
                    {
                        goto Label_0138;
                    }
                    num4 = 5;
                    goto Label_0002;

                case 0x10:
                    if (num3 != intPageNo)
                    {
                        num = intPageSize;
                        num4 = 6;
                    }
                    else
                    {
                        num4 = 14;
                    }
                    goto Label_0002;

                case 0x11:
                    goto Label_0138;
            }
        Label_0053:
            strArray = new string[intPageSize];
            builder = new StringBuilder();
            new StringBuilder().Append("var arrGU = new Array();");
            command = new OleDbCommand("", this.a);
            reader = null;
            num = 0;
            num2 = 0;
            num4 = 15;
            goto Label_0002;
        Label_0122:
            num2--;
            num4 = 12;
            goto Label_0002;
        Label_0138:
            num3 = this.getMaxPage(intRecordCount, intPageSize);
            num4 = 0x10;
            goto Label_0002;
        }

        public string[] getGUListJs(int intPageNo, int intPageSize, int intRecordCount)
        {
            string[] strArray;
            StringBuilder builder;
            StringBuilder builder2;
            OleDbCommand command;
            OleDbDataReader reader;
            int num;
            int num2;
            int num3 = 0; //赋初值
            int num4;
            goto Label_0053;
        Label_0002:
            switch (num4)
            {
                case 0:
                    builder.Append(strArray[num2]);
                    num4 = 12;
                    goto Label_0002;

                case 1:
                case 10:
                    num4 = 2;
                    goto Label_0002;

                case 2:
                    if (num2 >= 0)
                    {
                        goto Label_00BB;
                    }
                    num4 = 14;
                    goto Label_0002;

                case 3:
                    if (reader.Read())
                    {
                        strArray[num2] = string.Concat(new object[] { "arr[", num2.ToString(), "] = [", reader["GUID"], ",'", reader["GUUserName"], "','", reader["Name"], "','", reader["DepName"], "','", reader["Dep"], "'];\n" });
                        builder2.Append(reader["GUID"] + ",");
                        num2++;
                        num4 = 11;
                    }
                    else
                    {
                        num4 = 9;
                    }
                    goto Label_0002;

                case 4:
                    if (intRecordCount != -1)
                    {
                        goto Label_013F;
                    }
                    num4 = 15;
                    goto Label_0002;

                case 5:
                    if (strArray[num2] == null)
                    {
                        goto Label_0129;
                    }
                    num4 = 0;
                    goto Label_0002;

                case 6:
                case 11:
                    num4 = 3;
                    goto Label_0002;

                case 7:
                case 0x10:
                    command.CommandText = "SELECT TOP " + num.ToString() + " GUID,GUUserName,Name,DepName,Dep FROM (SELECT TOP " + Convert.ToString((int) (intPageSize * intPageNo)) + " GUID,GULevel,DepName,State,GUUserName,Name,RegDate FROM GUTable G LEFT OUTER JOIN Dep D ON G.Dep=D.DID ORDER BY GUID DESC) G ORDER BY GUID";
                    reader = command.ExecuteReader();
                    num4 = 6;
                    goto Label_0002;

                case 8:
                    if (num3 != intPageNo)
                    {
                        num = intPageSize;
                        num4 = 7;
                    }
                    else
                    {
                        num4 = 0x11;
                    }
                    goto Label_0002;

                case 9:
                    num2 = strArray.Length - 1;
                    num4 = 10;
                    goto Label_0002;

                case 12:
                    goto Label_0129;

                case 13:
                    goto Label_013F;

                case 14:
                    return new string[] { ("var sGUIDStr='" + builder2.ToString() + "';\n"), builder.ToString() };

                case 15:
                    if ((1 == 0) || (0 == 0))
                    {
                        intRecordCount = this.getRecordCount();
                        num4 = 13;
                        goto Label_0002;
                    }
                    goto Label_00BB;

                case 0x11:
                    num = intRecordCount - ((num3 - 1) * intPageSize);
                    num4 = 0x10;
                    goto Label_0002;
            }
        Label_0053:
            strArray = new string[intPageSize];
            builder = new StringBuilder();
            builder2 = new StringBuilder();
            builder.Append("var arrGU = new Array();");
            command = new OleDbCommand("", this.a);
            reader = null;
            num = 0;
            num2 = 0;
            num4 = 4;
            goto Label_0002;
        Label_00BB:
            num4 = 5;
            goto Label_0002;
        Label_0129:
            num2--;
            num4 = 1;
            goto Label_0002;
        Label_013F:
            num3 = this.getMaxPage(intRecordCount, intPageSize);
            num4 = 8;
            goto Label_0002;
        }

        public int getMaxPage(int intRecordCount, int intPageSize)
        {
            return (int) Math.Abs(Math.Floor((double) (-((double) intRecordCount) / ((double) intPageSize))));
        }

        public string getPageList(int intCurrPage, int intMaxPage, int intBigPage, string sTargetURL)
        {
            string str;
            int num;
            int num2;
            int num3;
            int num4;
            goto Label_007F;
        Label_0002:
            switch (num4)
            {
                case 0:
                    if (intCurrPage >= intMaxPage)
                    {
                        num4 = 0x18;
                    }
                    else
                    {
                        num4 = 5;
                    }
                    goto Label_0002;

                case 1:
                case 0x17:
                    num++;
                    num4 = 10;
                    goto Label_0002;

                case 2:
                    return str;

                case 3:
                    return str;

                case 4:
                case 10:
                    num4 = 14;
                    goto Label_0002;

                case 5:
                {
                    string str3 = str;
                    str = str3 + " <span class='NextPage_Link'><a href=" + sTargetURL + "?p=" + Convert.ToString((int) (intCurrPage + 1)) + ">下页</a></span> ";
                    num4 = 2;
                    goto Label_0002;
                }
                case 6:
                    if (intMaxPage < 1)
                    {
                        goto Label_028C;
                    }
                    num4 = 7;
                    goto Label_0002;

                case 7:
                    str = "<span class='UpPage' title='上一页'>上页</span>";
                    num4 = 12;
                    goto Label_0002;

                case 8:
                    goto Label_02B2;

                case 9:
                    num2 = 1;
                    num4 = 0x19;
                    goto Label_0002;

                case 11:
                    if (num3 <= intMaxPage)
                    {
                        goto Label_02B2;
                    }
                    num4 = 0x1a;
                    goto Label_0002;

                case 12:
                case 0x13:
                    goto Label_028C;

                case 13:
                    goto Label_01C2;

                case 14:
                    if (num <= num3)
                    {
                        num4 = 0x11;
                    }
                    else
                    {
                        num4 = 15;
                    }
                    goto Label_0002;

                case 15:
                    num4 = 0;
                    goto Label_0002;

                case 0x10:
                    if (num3 <= intMaxPage)
                    {
                        goto Label_01C2;
                    }
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num4 = 0x16;
                    goto Label_0002;

                case 0x11:
                    if (num == intCurrPage)
                    {
                        str = str + "<span class='PageNum'>" + num.ToString() + "</span>&nbsp;";
                        num4 = 1;
                    }
                    else
                    {
                        num4 = 0x12;
                    }
                    goto Label_0002;

                case 0x12:
                {
                    string str2 = str;
                    str = str2 + "<span class='PageNum_Link'>[<a href=" + sTargetURL + "?p=" + num.ToString() + ">" + num.ToString() + "</a>]</span>&nbsp;";
                    num4 = 0x17;
                    goto Label_0002;
                }
                case 20:
                    if (intCurrPage < 2)
                    {
                        num4 = 6;
                    }
                    else
                    {
                        num4 = 0x15;
                    }
                    goto Label_0002;

                case 0x15:
                    str = " <span class='UpPage_Link' title='上一页'><a href=" + sTargetURL + ">上页</a></span> ";
                    num4 = 0x13;
                    goto Label_0002;

                case 0x16:
                    num3 = intMaxPage;
                    num4 = 13;
                    goto Label_0002;

                case 0x18:
                    if (intMaxPage < 1)
                    {
                        return str;
                    }
                    num4 = 0x1c;
                    goto Label_0002;

                case 0x19:
                    goto Label_02DE;

                case 0x1a:
                    num3 = intMaxPage;
                    num4 = 8;
                    goto Label_0002;

                case 0x1b:
                    if (num2 > 0)
                    {
                        goto Label_02DE;
                    }
                    num4 = 9;
                    goto Label_0002;

                case 0x1c:
                    str = str + "<span class='NextPage'>下页</span>";
                    num4 = 3;
                    goto Label_0002;
            }
        Label_007F:
            str = "";
            num = 0;
            num2 = 1;
            num3 = 10;
            num2 = (intCurrPage / intBigPage) * intBigPage;
            num3 = num2 + intBigPage;
            num4 = 11;
            goto Label_0002;
        Label_01C2:
            num = num2;
            num4 = 4;
            goto Label_0002;
        Label_028C:
            num2 -= 5;
            num4 = 0x1b;
            goto Label_0002;
        Label_02B2:
            num2++;
            num4 = 20;
            goto Label_0002;
        Label_02DE:
            num3 += 5;
            num4 = 0x10;
            goto Label_0002;
        }

        public string getPageList(int intCurrPage, int intMaxPage, int intBigPage, string sTargetURL, string sURLVar)
        {
            string str;
            int num;
            int num2;
            int num3;
            int num4;
            goto Label_007F;
        Label_0002:
            switch (num4)
            {
                case 0:
                    if (intMaxPage < 1)
                    {
                        return str;
                    }
                    num4 = 12;
                    goto Label_0002;

                case 1:
                case 3:
                    goto Label_0298;

                case 2:
                    if (intCurrPage < 2)
                    {
                        num4 = 7;
                    }
                    else
                    {
                        num4 = 0x15;
                    }
                    goto Label_0002;

                case 4:
                case 0x1a:
                    num++;
                    num4 = 0x18;
                    goto Label_0002;

                case 5:
                    if (num3 <= intMaxPage)
                    {
                        goto Label_01C8;
                    }
                    num4 = 14;
                    goto Label_0002;

                case 6:
                    if (intCurrPage >= intMaxPage)
                    {
                        num4 = 0;
                    }
                    else
                    {
                        num4 = 0x1c;
                    }
                    goto Label_0002;

                case 7:
                    if (intMaxPage < 1)
                    {
                        goto Label_0298;
                    }
                    num4 = 20;
                    goto Label_0002;

                case 8:
                    num3 = intMaxPage;
                    num4 = 15;
                    goto Label_0002;

                case 9:
                    if (num2 > 0)
                    {
                        goto Label_02EA;
                    }
                    num4 = 0x1b;
                    goto Label_0002;

                case 10:
                    return str;

                case 11:
                    return str;

                case 12:
                    str = str + "<span class='NextPage'>下页</span>";
                    num4 = 11;
                    goto Label_0002;

                case 13:
                    if (num == intCurrPage)
                    {
                        str = str + "<span class='PageNum'>" + num.ToString() + "</span>&nbsp;";
                        num4 = 0x1a;
                    }
                    else
                    {
                        num4 = 0x17;
                    }
                    goto Label_0002;

                case 14:
                    num3 = intMaxPage;
                    num4 = 0x12;
                    goto Label_0002;

                case 15:
                    goto Label_02BE;

                case 0x10:
                case 0x18:
                    num4 = 0x19;
                    goto Label_0002;

                case 0x11:
                    goto Label_02EA;

                case 0x12:
                    goto Label_01C8;

                case 0x13:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (num3 <= intMaxPage)
                    {
                        goto Label_02BE;
                    }
                    num4 = 8;
                    goto Label_0002;

                case 20:
                    str = "<span class='UpPage' title='上一页'>上页</span>";
                    num4 = 3;
                    goto Label_0002;

                case 0x15:
                    str = " <span class='UpPage_Link' title='上一页'><a href=" + sTargetURL + ">上页</a></span> ";
                    num4 = 1;
                    goto Label_0002;

                case 0x16:
                    num4 = 6;
                    goto Label_0002;

                case 0x17:
                {
                    string str2 = str;
                    str = str2 + "<span class='PageNum_Link'>[<a href=" + sTargetURL + sURLVar + "p=" + num.ToString() + ">" + num.ToString() + "</a>]</span>&nbsp;";
                    num4 = 4;
                    goto Label_0002;
                }
                case 0x19:
                    if (num <= num3)
                    {
                        num4 = 13;
                    }
                    else
                    {
                        num4 = 0x16;
                    }
                    goto Label_0002;

                case 0x1b:
                    num2 = 1;
                    num4 = 0x11;
                    goto Label_0002;

                case 0x1c:
                {
                    string str3 = str;
                    str = str3 + " <span class='NextPage_Link'><a href=" + sTargetURL + sURLVar + "p=" + Convert.ToString((int) (intCurrPage + 1)) + ">下页</a></span> ";
                    num4 = 10;
                    goto Label_0002;
                }
            }
        Label_007F:
            str = "";
            num = 0;
            num2 = 1;
            num3 = 10;
            num2 = (intCurrPage / intBigPage) * intBigPage;
            num3 = num2 + intBigPage;
            num4 = 0x13;
            goto Label_0002;
        Label_01C8:
            num = num2;
            num4 = 0x10;
            goto Label_0002;
        Label_0298:
            num2 -= 5;
            num4 = 9;
            goto Label_0002;
        Label_02BE:
            num2++;
            num4 = 2;
            goto Label_0002;
        Label_02EA:
            num3 += 5;
            num4 = 5;
            goto Label_0002;
        }

        public int getRecordCount()
        {
            int num;
        Label_0017:
            num = 0;
            OleDbDataReader reader = new OleDbCommand("SELECT COUNT(1) FROM GUTable", this.a).ExecuteReader();
            if ((1 != 0) && (0 != 0))
            {
            }
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
            return num;
        }

        public int getRecordCountForBBS(int UID)
        {
        Label_0017:
            if ((1 != 0) && (0 != 0))
            {
            }
            int num = 0;
            OleDbDataReader reader = new OleDbCommand("SELECT COUNT(1) FROM BBS WHERE UID=" + UID.ToString() + " AND ReID=0 ", this.a).ExecuteReader();
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
            return num;
        }

        public int getRecordCountForBBSManage()
        {
            int num;
        Label_0017:
            num = 0;
            OleDbDataReader reader = new OleDbCommand("SELECT COUNT(1) FROM BBS WHERE ReID=0", this.a).ExecuteReader();
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    if (!reader.Read())
                    {
                        break;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    num = Convert.ToInt32(reader[0]);
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    break;

                default:
                    goto Label_0017;
            }
            reader.Close();
            return num;
        }

        public int getRecordCountForStyleLib(int UID)
        {
        Label_0017:
            if ((1 != 0) && (0 != 0))
            {
            }
            int num = 0;
            OleDbDataReader reader = new OleDbCommand("SELECT COUNT(1) FROM StyleLib WHERE UID=" + UID.ToString() + " OR Share=1", this.a).ExecuteReader();
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
            return num;
        }

        public string getStyleList(int UID, int intPageNo, int intPageSize, int intRecordCount)
        {
            string[] strArray;
            StringBuilder builder;
            OleDbCommand command;
            OleDbDataReader reader;
            int num;
            int num2;
            int num3 = 0; //赋初值
            string str = null; //赋初值
            int num4;
            goto Label_0063;
        Label_0002:
            switch (num4)
            {
                case 0:
                    goto Label_02C7;

                case 1:
                    if (strArray[num2] == null)
                    {
                        goto Label_02C7;
                    }
                    num4 = 3;
                    goto Label_0002;

                case 2:
                    if (!(reader["Share"].ToString() == "1"))
                    {
                        str = "<span class='share_Gray' title='自有的样式'></span>";
                        num4 = 4;
                    }
                    else
                    {
                        num4 = 0x11;
                    }
                    goto Label_0002;

                case 3:
                    builder.Append(strArray[num2]);
                    num4 = 0;
                    goto Label_0002;

                case 4:
                case 12:
                    strArray[num2] = string.Concat(new object[] { 
                        "<tr style='height:25px' onmouseout=switchShow(this,0) onmouseover=switchShow(this,1)><td><input type='checkbox' value='", reader["ID"], "' name=selectstyle id='selectstyle", reader["ID"], "'>", reader["StyleName"], "</td><td>", str, "</td><td><span class='view'  onclick='teststyle(", reader["ID"], ")'>预览</span></td><td><span   onclick='applyStyle(", reader["ID"], ")' class='switchuser'>应用</span></td><td><span class='del' onclick='delStyle(", reader["ID"], ",0)'>删除</span><input type='hidden' id='S", reader["ID"], 
                        "' value='", reader["StyleContent"], "' /></td></tr>"
                     });
                    num2++;
                    num4 = 0x10;
                    goto Label_0002;

                case 5:
                    if (reader.Read())
                    {
                        num4 = 2;
                    }
                    else
                    {
                        num4 = 7;
                    }
                    goto Label_0002;

                case 6:
                    if (intRecordCount != -1)
                    {
                        goto Label_021A;
                    }
                    num4 = 8;
                    goto Label_0002;

                case 7:
                    num2 = strArray.Length - 1;
                    num4 = 15;
                    goto Label_0002;

                case 8:
                    intRecordCount = this.getRecordCount();
                    num4 = 13;
                    goto Label_0002;

                case 9:
                    if (num2 >= 0)
                    {
                        num4 = 1;
                    }
                    else
                    {
                        num4 = 11;
                    }
                    goto Label_0002;

                case 10:
                case 15:
                    num4 = 9;
                    goto Label_0002;

                case 11:
                    return ("<table  border=0 cellpadding=0 cellspacing=0 width=100%><tr><td><input type='checkbox' value='0' name=selectuser id='selectuser0' onclick='selectdata(0)'><td>状态</td><td>预览</td><td>应用</td><td>删除</td></tr>" + builder.ToString() + "</table>");

                case 13:
                    goto Label_021A;

                case 14:
                case 0x12:
                    command.CommandText = "SELECT TOP " + num.ToString() + " ID,StyleName,Share,StyleContent FROM (SELECT TOP " + Convert.ToString((int) (intPageSize * intPageNo)) + " ID,StyleName,Share,StyleContent FROM StyleLib WHERE UID=" + UID.ToString() + " OR Share=1 ORDER BY ID) V ORDER BY ID DESC";
                    reader = command.ExecuteReader();
                    str = "";
                    num4 = 20;
                    goto Label_0002;

                case 0x10:
                case 20:
                    num4 = 5;
                    goto Label_0002;

                case 0x11:
                    str = "<span class='share' title='共享的样式'></span>";
                    num4 = 12;
                    goto Label_0002;

                case 0x13:
                    num = intRecordCount - ((num3 - 1) * intPageSize);
                    num4 = 0x12;
                    goto Label_0002;

                case 0x15:
                    if (num3 != intPageNo)
                    {
                        num = intPageSize;
                        num4 = 14;
                    }
                    else
                    {
                        num4 = 0x13;
                    }
                    goto Label_0002;
            }
        Label_0063:
            if ((1 != 0) && (0 != 0))
            {
            }
            strArray = new string[intPageSize];
            builder = new StringBuilder();
            command = new OleDbCommand("", this.a);
            reader = null;
            num = 0;
            num2 = 0;
            num4 = 6;
            goto Label_0002;
        Label_021A:
            num3 = this.getMaxPage(intRecordCount, intPageSize);
            num4 = 0x15;
            goto Label_0002;
        Label_02C7:
            num2--;
            num4 = 10;
            goto Label_0002;
        }

        public string getSurveyName(int SID, int UID)
        {
            string str;
            OleDbDataReader reader;
            int num;
            if ((1 == 0) || (0 == 0))
            {
                goto Label_001B;
            }
        Label_0006:
            switch (num)
            {
                case 0:
                    str = reader[0].ToString();
                    num = 1;
                    goto Label_0006;

                case 1:
                    goto Label_0092;

                case 2:
                    if (!reader.Read())
                    {
                        goto Label_0092;
                    }
                    num = 0;
                    goto Label_0006;
            }
        Label_001B:
            str = "";
            this.b.CommandText = "SELECT TOP 1 SurveyName FROM SurveyTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString();
            reader = this.b.ExecuteReader();
            num = 2;
            goto Label_0006;
        Label_0092:
            reader.Close();
            return str;
        }

        public void moveDep(int intDID, int intTargetDID)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            string str;
            OleDbDataReader reader = null; //赋初值
            int num6;
            goto Label_003B;
        Label_0002:
            switch (num6)
            {
                case 0:
                    return;

                case 1:
                    return;

                case 2:
                    if (num2 != 0)
                    {
                        this.b.CommandText = "SELECT TOP 1 DepLevel FROM Dep WHERE DID=" + intTargetDID.ToString();
                        reader = this.b.ExecuteReader();
                        num6 = 8;
                    }
                    else
                    {
                        num6 = 0;
                    }
                    goto Label_0002;

                case 3:
                    num4 = Convert.ToInt32(reader["DepLevel"]);
                    num6 = 10;
                    goto Label_0002;

                case 4:
                    if (!reader.Read())
                    {
                        goto Label_0224;
                    }
                    num6 = 7;
                    goto Label_0002;

                case 5:
                    if (Convert.ToString("," + str + ",").IndexOf("," + intTargetDID.ToString() + ",") < 0)
                    {
                        this.b.CommandText = "SELECT MAX(SORT) FROM Dep WHERE ParentID=" + intTargetDID.ToString();
                        reader = this.b.ExecuteReader();
                        num6 = 4;
                    }
                    else
                    {
                        num6 = 1;
                    }
                    goto Label_0002;

                case 6:
                    goto Label_01BB;

                case 7:
                    try
                    {
                        num5 = Convert.ToInt32(reader[0]) + 1;
                    }
                    catch
                    {
                        num5 = 1;
                    }
                    goto Label_0224;

                case 8:
                    if (!reader.Read())
                    {
                        goto Label_0287;
                    }
                    num6 = 3;
                    goto Label_0002;

                case 9:
                    if (!reader.Read())
                    {
                        goto Label_01BB;
                    }
                    num6 = 11;
                    goto Label_0002;

                case 10:
                    goto Label_0287;

                case 11:
                    num = Convert.ToInt32(reader["Sort"]);
                    num3 = Convert.ToInt32(reader["DepLevel"]);
                    num2 = Convert.ToInt32(reader["ParentID"]);
                    num6 = 6;
                    goto Label_0002;
            }
        Label_003B:
            num = 0;
            num2 = 0;
            num3 = 0;
            num4 = 0;
            this.b.CommandText = "SELECT * FROM Dep";
            num5 = 0;
            str = this.getAllChild(this.getDepTable(), intDID, false, "") + "-1";
            num6 = 5;
            goto Label_0002;
        Label_01BB:
            reader.Close();
            num6 = 2;
            goto Label_0002;
        Label_0224:
            if ((1 != 0) && (0 != 0))
            {
            }
            reader.Close();
            this.b.CommandText = "SELECT TOP 1 Sort,ParentID,DepLevel FROM Dep WHERE DID=" + intDID.ToString();
            reader = this.b.ExecuteReader();
            num6 = 9;
            goto Label_0002;
        Label_0287:
            reader.Close();
            num3 = (num4 + 1) - num3;
            this.b.CommandText = " UPDATE Dep SET Sort=Sort-1 WHERE ParentID=" + num2.ToString() + " AND Sort>" + num.ToString();
            this.b.ExecuteNonQuery();
            this.b.CommandText = " UPDATE Dep SET ParentID=" + intTargetDID.ToString() + ",Sort=" + num5.ToString() + " WHERE DID=" + intDID.ToString();
            this.b.ExecuteNonQuery();
            this.b.CommandText = " UPDATE Dep SET DepLevel=DepLevel+" + num3.ToString() + " WHERE DID IN(" + intDID.ToString() + "," + str + ")";
            this.b.ExecuteNonQuery();
        }

        public string writeDepJs(DataTable dt)
        {
            StringBuilder builder = null; //赋初值
            int num = 0; //赋初值
            int num2 = 2;
        Label_000D:
            switch (num2)
            {
                case 0:
                case 3:
                    num2 = 5;
                    goto Label_000D;

                case 1:
                    break;

                case 4:
                    dt = this.getDepTable();
                    num2 = 1;
                    goto Label_000D;

                case 5:
                    if (num < dt.Rows.Count)
                    {
                        builder.Append(string.Concat(new object[] { "arrDep[", num.ToString(), "]=[", dt.Rows[num]["DID"], ",'", dt.Rows[num]["DepName"], "',", dt.Rows[num]["Sort"], ",", dt.Rows[num]["DepLevel"], ",", dt.Rows[num]["ParentID"], "];\n" }));
                        num++;
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 6;
                    }
                    goto Label_000D;

                case 6:
                    return builder.ToString();

                default:
                    if (dt == null)
                    {
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num2 = 4;
                        goto Label_000D;
                    }
                    break;
            }
            new StringBuilder().Append("var arrDep = new Array();\n");
            num = 0;
            num2 = 3;
            goto Label_000D;
        }

        public int IID_
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

        public OleDbCommand objComm_
        {
            get
            {
                return this.b;
            }
            set
            {
                this.b = value;
            }
        }

        public OleDbConnection objConn_
        {
            get
            {
                return this.a;
            }
            set
            {
                this.objConn_ = value;
            }
        }

        public int SID_
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

        public int UID_
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

