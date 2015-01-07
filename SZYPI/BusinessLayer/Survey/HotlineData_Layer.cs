using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using DBAccess;
using Business.Helper;
using System.Data.SqlClient;

namespace BusinessLayer.Survey
{
    public class HotlineData_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";

        public DataSet GetHotline_Analyse(int PageIndex, int PageSize)
        {
            string TabelName = " " + prefix + "Hotline_Analyse ";
            StringBuilder sql = new StringBuilder("select * from UV_QS_Hotline_AnalyseData order by id desc ", 100);

            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@sqlstr", sql.ToString());
            parameters[1] = new SqlParameter("@PageSize", PageSize);
            parameters[2] = new SqlParameter("@pageindex", PageIndex);

            return DbHelperSQL.RunProcedureGetDataSet("Z_UP_GetDataProc", parameters);
        }
    }
}
