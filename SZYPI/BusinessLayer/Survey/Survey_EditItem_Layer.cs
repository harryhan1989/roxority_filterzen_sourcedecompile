using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Business.Helper;
using DBAccess;
using System.Data;

namespace BusinessLayer.Survey
{
    public class Survey_EditItem_Layer
    {
        public SqlDataReader GetPageTable(long SID)
        {
            string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("SELECT PageNo FROM" + TabelName + "WHERE SID=@SID ORDER BY PageNo", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
