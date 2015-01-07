namespace checkState
{
    using System;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using BusinessLayer.checkState;

    public class checkState
    {
        public short getState(long SID2, long UID2)
        {
            short num;
        Label_0027:
            num = 0;
            //command.CommandText = "SELECT TOP 1 State FROM SurveyTable WHERE SID=" + SID2.ToString() + " AND UID=" + UID2.ToString();
            SqlDataReader reader = new checkState_Layer().GetSurveyTable(SID2.ToString(), UID2.ToString());
            int num2 = 1;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 2:
                case 4:
                    reader.Close();
                    return num;

                case 1:
                    if ((!reader.Read() ? 0 : 1) != 0)
                    {
                    }
                    num2 = 5;
                    goto Label_0002;

                case 3:
                    if (Convert.ToBoolean(reader[0]))
                    {
                        num = 1;
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 6;
                    }
                    goto Label_0002;

                case 5:
                    num2 = 3;
                    goto Label_0002;

                case 6:
                    num = 0;
                    num2 = 2;
                    goto Label_0002;
                    num = 2;
                    num2 = 4;
                    goto Label_0002;
            }
            goto Label_0027;
        }

        public string getSurveyNotRepeatItem(long SID, long UID)
        {
            string str;
        Label_0017:
            str = "";
            //objComm.CommandText = "SELECT TOP 1 ExpandContent FROM SurveyExpand WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString() + " AND ExpandType=2";
            SqlDataReader reader = new checkState_Layer().GetSurveyExpand(SID.ToString(), UID.ToString(), "2");
            int num = 0;
        Label_0002:
            switch (num)
            {
                case 0:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num = 1;
                    goto Label_0002;

                case 1:
                    str = reader["ExpandContent"].ToString();
                    num = 2;
                    goto Label_0002;

                case 2:
                    break;

                default:
                    goto Label_0017;
            }
            reader.Dispose();
            return str;
        }

        public int getSurveyStatus(OleDbCommand objComm, long SID, long UID)
        {
            int num;
        Label_0017:
            num = 0;
            objComm.CommandText = "SELECT TOP 1 State FROM SurveyTable WHERE SID=" + SID.ToString() + " AND UID=" + UID.ToString();
            SqlDataReader reader = new checkState_Layer().GetSurveyTable(SID.ToString(), UID.ToString());
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num2 = 2;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    num = Convert.ToInt32(reader["State"]);
                    if ((1 != 0) && (0 != 0))
                    {
                    }
                    num2 = 1;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            reader.Dispose();
            return num;
        }

        public int getUID(long SID)
        {
        Label_0017:
            int num = 0;
            //objComm.CommandText = "SELECT TOP 1 UID FROM SurveyTable WHERE SID=" + SID.ToString();
            SqlDataReader reader = new checkState_Layer().GetSurveyTable1(SID.ToString());
            int num2 = 1;
        Label_0002:
            switch (num2)
            {
                case 0:
                    break;

                case 1:
                    if (!reader.Read())
                    {
                        break;
                    }
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    num = Convert.ToInt32(reader["UID"]);
                    num2 = 0;
                    goto Label_0002;

                default:
                    goto Label_0017;
            }
            reader.Dispose();
            return num;
        }
    }
}

