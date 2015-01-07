namespace LoginClass
{
    using System;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using BusinessLayer.LoginClass;

    public class languageClass
    {
        public static string[,] arrLanguage = new string[10, 2];
        public static string sLanList = "";

        public int getLan(long SID)
        {
            int num;
        Label_0017:
            num = 1;
            //objComm.CommandText = "SELECT TOP 1 Lan FROM SurveyTable WHERE SID=" + SID.ToString();
            SqlDataReader reader = new LanguageClass_Layer().GetSurveyTable(SID.ToString());
            int num2 = 2;
        Label_0002:
            switch (num2)
            {
                case 0:
                    num = Convert.ToInt32(reader["Lan"]);

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
            reader.Dispose();
            return num;
        }

        public void getLanguage()
        {
            SqlDataReader reader = null; //∏≥≥ı÷µ
            int num = 1;
        Label_000D:
            switch (num)
            {
                case 0:
                case 5:
                    num = 4;
                    goto Label_000D;

                case 2:
                    reader.Dispose();
                    return;

                case 3:
                    return;

                case 4:
                    if (reader.Read())
                    {
                        arrLanguage[Convert.ToInt32(reader["ID"]), 0] = reader["JS"].ToString();
                        arrLanguage[Convert.ToInt32(reader["ID"]), 1] = reader["CSharp"].ToString();
                        num = 0;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;
            }
            if (arrLanguage[1, 0] == null)
            {
                //objComm.CommandText = "SELECT ID,JS,CSharp FROM LanTable";
                reader = new LanguageClass_Layer().GetLanTable();
                num = 5;
            }
            else
            {
                if ((1 != 0) && (0 != 0))
                {
                }
                num = 3;
            }
            goto Label_000D;
        }

        public void getLanList()
        {
            SqlDataReader reader = null; //∏≥≥ı÷µ
            int num = 3;
        Label_000D:
            switch (num)
            {
                case 0:
                case 4:
                    num = 5;
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

                case 5:
                    if (reader.Read())
                    {
                        break;
                    }
                    num = 2;
                    goto Label_000D;

                default:
                    if (!(languageClass.sLanList != ""))
                    {
                        //objComm.CommandText = "SELECT ID,LanName FROM LanTable";
                        reader = new LanguageClass_Layer().GetLanTable1();
                        num = 0;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_000D;
            }
            string sLanList = languageClass.sLanList;
            languageClass.sLanList = sLanList + reader["ID"].ToString() + ":" + reader["LanName"].ToString() + "|";
            num = 4;
            goto Label_000D;
        }

        public string[,] _arrLanguage
        {
            get
            {
                return arrLanguage;
            }
        }

        public string _sLanList
        {
            get
            {
                return sLanList;
            }
        }
    }
}

