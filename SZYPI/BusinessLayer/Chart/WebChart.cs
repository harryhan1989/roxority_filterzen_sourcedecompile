using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using System.Data;

namespace Web_SubCode.Chart
{
    public class WebChart_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public DataTable GetSurveyTableName(string SID)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 SurveyName FROM " + TabelName + " WHERE SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetAllSurveyTtem(string SID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("SELECT [IID],[UID],[SID],[PageNo],[ItemName],[ItemType],[DataFormatCheck],[ItemContent],[Sort],[ItemHTML],[OptionAmount],[ParentID],[StyleMode],[OrderModel],[OtherProperty],[Logic],[StatAmount],[OptionImgModel],[ChildID] FROM " + TabelName + " WHERE SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetTtemOption(string SID, string IID)
        {
            string TabelName = " " + prefix + "OptionTable ";
            StringBuilder sql = new StringBuilder("SELECT [OID],[IID],[SID],[UID],[OptionName],[Sort],[Point],[Image],[ParentNode],[ISMatrixRowColumn] FROM" + TabelName + " WHERE SID=@SID AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetChoosedOption(string SID)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecProcedure(parameters, "UP_QS_GetChoosedOption");
        }
    }
}
