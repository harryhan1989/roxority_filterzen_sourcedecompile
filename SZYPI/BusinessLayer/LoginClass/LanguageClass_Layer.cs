using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Business.Helper;
using DBAccess;

namespace BusinessLayer.LoginClass
{
    public class LanguageClass_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public SqlDataReader GetSurveyTable(string SID)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 Lan FROM " + TabelName + " WHERE SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetLanTable()
        {
            string TabelName = " " + prefix + "LanTable ";
            StringBuilder sql = new StringBuilder("SELECT ID,JS,CSharp FROM " + TabelName, 50);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql));
        }

        public SqlDataReader GetLanTable1()
        {
            string TabelName = " " + prefix + "LanTable ";
            StringBuilder sql = new StringBuilder("SELECT ID,LanName FROM " + TabelName, 50);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql));
        }
    }
}
