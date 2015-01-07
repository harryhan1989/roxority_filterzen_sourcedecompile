namespace GetJs
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Text;
    using System.Web;
    using BusinessLayer.Survey;
    using System.Data.SqlClient;

    public class GetJs
    {
        private long SID;
        private long IID;
        private long UID;

        public GetJs(long SID__, long UID__)
        {
            this.SID = SID__;
            this.UID = UID__;
        }

        public string getDataHTML(string sSQL)
        {
            StringBuilder builder;
            int num;
            int num2;
            DataSet set;
            int num3;
            goto Label_002F;
        Label_0002:
            switch (num3)
            {
                case 0:
                    if (num2 < set.Tables["Data"].Rows.Count)
                    {
                        num = 0;
                        num3 = 5;
                    }
                    else
                    {
                        num3 = 8;
                    }
                    goto Label_0002;

                case 1:
                    if (num < set.Tables["Data"].Columns.Count)
                    {
                        builder.Append("<span id='" + set.Tables["Data"].Columns[num].ColumnName.ToString() + "_" + num2.ToString() + "'>" + set.Tables["Data"].Rows[num2][num].ToString() + "</span><BR>");
                        num++;
                        num3 = 2;
                    }
                    else
                    {
                        num3 = 6;
                    }
                    goto Label_0002;

                case 2:
                    goto Label_0065;

                case 3:
                case 7:
                    num3 = 0;
                    goto Label_0002;

                case 4:
                    try
                    {
                        //adapter.Fill(set, "Data");
                        DataTable Data=new Survey_QueryState_Layer().GetBySql(sSQL);
                        Data.TableName="Data";
                        set.Tables.Add(Data) ;
                    }
                    catch (Exception)
                    {
                        set.Dispose();
                        HttpContext.Current.Response.Write("Error:GetJs错误");
                        HttpContext.Current.Response.End();
                    }
                    goto Label_021A;

                case 5:

                    goto Label_0065;
                    //goto Label_021A;

                case 6:
                    num2++;
                    num3 = 3;
                    goto Label_0002;

                case 8:
                    set.Dispose();
                    return builder.ToString();
            }
        Label_002F:
            builder = new StringBuilder();
            num = 0;
            num2 = 0;
            //OleDbCommand selectCommand = new OleDbCommand(sSQL, this.a);
            //adapter = new OleDbDataAdapter(selectCommand);
            set = new DataSet();
            num3 = 4;
            goto Label_0002;
        Label_0065:
            num3 = 1;
            goto Label_0002;
        Label_021A:
            num2 = 0;
            num3 = 7;
            goto Label_0002;
        }

        public string getItemAndOptionArr()
        {
            StringBuilder builder;
        Label_002B:
            builder = new StringBuilder();
            builder.Append("var arrItem = new Array();var arrOption = new Array();");
            int num = 0;
            //this.b.CommandText = "SELECT IID,ItemName,ItemType,ParentID,OptionAmount FROM ItemTable WHERE SID=" + this.SID.ToString() + " AND UID=" + this.UID.ToString();
            SqlDataReader reader = new Survey_QueryState_Layer().GetItemTable(this.SID.ToString(), this.UID.ToString());
            int num2 = 6;
        Label_0002:
            switch (num2)
            {
                case 0:
                    reader.Close();
                    num = 0;
                    //this.b.CommandText = "SELECT OID,OptionName,IID,Point,IsMatrixRowColumn FROM OptionTable WHERE SID=" + this.SID.ToString() + " AND UID=" + this.UID.ToString();
                    reader = new Survey_QueryState_Layer().GetOptionTable(this.SID.ToString(), this.UID.ToString());
                    num2 = 3;
                    goto Label_0002;

                case 1:
                case 6:
                    num2 = 7;
                    goto Label_0002;

                case 2:
                    reader.Dispose();
                    return builder.ToString();

                case 3:
                case 5:
                    num2 = 4;
                    goto Label_0002;

                case 4:
                    if (reader.Read())
                    {
                        builder.Append(string.Concat(new object[] { "arrOption[", num.ToString(), "]=[", reader["OID"], ",'", reader["OptionName"], "',", reader["IID"], ",'", reader["IsMatrixRowColumn"], "',", reader["Point"], "];" }));
                        num++;
                        num2 = 5;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 7:
                    if (reader.Read())
                    {
                        builder.Append(string.Concat(new object[] { "arrItem[", num.ToString(), "]=[", reader["IID"], ",'", reader["ItemName"], "',", reader["ItemType"], ",", reader["ParentID"], ",", reader["OptionAmount"], "];" }));
                        num++;
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 0;
                    }
                    goto Label_0002;
            }
            goto Label_002B;
        }

        public string getSurveyItemJs()
        {
            StringBuilder builder;
        Label_001B:
            builder = new StringBuilder();
            builder.Append("var arrItem = new Array();var arrOption = new Array();");
            int num = 0;
            //this.b.CommandText = "SELECT IID,ItemName,ItemType,ParentID,OptionAmount,Logic FROM ItemTable WHERE SID=" + this.SID.ToString() + " AND UID=" + this.UID.ToString();
            SqlDataReader reader = new Survey_QueryState_Layer().GetItemTable1(this.SID.ToString(),this.UID.ToString());
            int num2 = 2;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 2:
                    num2 = 3;
                    goto Label_0002;

                case 1:
                    reader.Dispose();
                    return builder.ToString();

                case 3:
                    if (reader.Read())
                    {
                        builder.Append(string.Concat(new object[] { "arrItem[", num.ToString(), "]=[", reader["IID"], ",'", reader["ItemName"], "',", reader["ItemType"], ",", reader["ParentID"], ",", reader["OptionAmount"], ",'", reader["Logic"], "'];" }));
                        num++;
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 1;
                    }
                    goto Label_0002;
            }
            goto Label_001B;
        }

        public string getSurveyItemOptionJs()
        {
            StringBuilder builder;
        Label_002B:
            builder = new StringBuilder();
            builder.Append("var arrItem = new Array();var arrOption = new Array();");
            int num = 0;
            //this.b.CommandText = "SELECT IID,ItemName,ItemType,ParentID,OptionAmount,Logic FROM ItemTable WHERE SID=" + this.SID.ToString() + " AND UID=" + this.UID.ToString() + " ORDER BY PageNo,Sort";
            SqlDataReader reader = new Survey_QueryState_Layer().GetItemTable1(this.SID.ToString(), this.UID.ToString());
            int num2 = 1;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (reader.Read())
                    {
                        builder.Append(string.Concat(new object[] { "arrOption[", num.ToString(), "]=[", reader["OID"], ",'", reader["OptionName"], "',", reader["IID"], "];" }));
                        num++;
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 5;
                    }
                    goto Label_0002;

                case 1:
                case 6:
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    if (reader.Read())
                    {
                        builder.Append(string.Concat(new object[] { "arrItem[", num.ToString(), "]=[", reader["IID"], ",'", reader["ItemName"], "',", reader["ItemType"], ",", reader["ParentID"], ",", reader["OptionAmount"], ",'", reader["Logic"], "'];" }));
                        num++;
                        num2 = 6;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    goto Label_0002;

                case 3:
                case 7:
                    num2 = 0;
                    goto Label_0002;

                case 4:
                    reader.Close();
                    num = 0;
                    //this.b.CommandText = "SELECT OID,OptionName,IID FROM OptionTable WHERE SID=" + this.SID.ToString() + " AND UID=" + this.UID.ToString();
                    reader = new Survey_QueryState_Layer().GetOptionTable1(this.SID.ToString(), this.UID.ToString());
                    num2 = 7;
                    goto Label_0002;

                case 5:
                    reader.Dispose();
                    return builder.ToString();
            }
            goto Label_002B;
        }

        public long IID_
        {
            get
            {
                return this.IID;
            }
            set
            {
                this.IID = value;
            }
        }

        public long SID_
        {
            get
            {
                return this.SID;
            }
            set
            {
                this.SID = value;
            }
        }

        public long UID_
        {
            get
            {
                return this.UID;
            }
            set
            {
                this.UID = value;
            }
        }
    }
}

