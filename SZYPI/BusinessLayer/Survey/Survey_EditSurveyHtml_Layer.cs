using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using System.Configuration;

namespace BusinessLayer.Survey
{
    public class Survey_EditSurveyHtml_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public SqlDataReader GetPageStyle(string PageType)
        {
            string TabelName = " " + prefix + "PageStyle ";
            StringBuilder sql = new StringBuilder("SELECT P_ID,PageFileName,StyleName,PageType,PageImage FROM" + TabelName + "WHERE PageType=@PageType ORDER BY ID DESC", 200);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@PageType", PageType);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    } 
}
