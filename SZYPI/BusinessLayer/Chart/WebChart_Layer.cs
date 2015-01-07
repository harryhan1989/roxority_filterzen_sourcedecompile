using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using System.Data;

namespace BusinessLayer.Chart
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
            StringBuilder sql = new StringBuilder("SELECT [IID],[UID],[SID],[PageNo],[ItemName],[ItemType],[DataFormatCheck],[ItemContent],[Sort],[ItemHTML],[OptionAmount],[ParentID],[StyleMode],[OrderModel],[OtherProperty],[Logic],[StatAmount],[OptionImgModel],[ChildID],[ItemConclusion] FROM " + TabelName + " WHERE SID=@SID and ParentID=0", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetTtem(string SID,string IID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("SELECT [IID],[UID],[SID],[PageNo],[ItemName],[ItemType],[DataFormatCheck],[ItemContent],[Sort],[ItemHTML],[OptionAmount],[ParentID],[StyleMode],[OrderModel],[OtherProperty],[Logic],[StatAmount],[OptionImgModel],[ChildID],[ItemConclusion] FROM " + TabelName + " WHERE SID=@SID AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@IID", IID);
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

        public DataTable GetChoosedOption(string SID,string Condition)
        {
            if (string.IsNullOrEmpty(Condition) || Condition == "")
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SID", SID);
                return DbHelperSQL.ExecProcedure(parameters, "UP_QS_GetChoosedOption");
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@SID", SID);
                parameters[1] = new SqlParameter("@Condition", Condition);
                return DbHelperSQL.ExecProcedure(parameters, "UP_QS_GetChoosedOption");
            }
        }

        public int SetItemConclusion(string SID, string IID, string ItemConclusion)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("Update" + TabelName + "set ItemConclusion=@ItemConclusion WHERE SID=@SID AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@IID", IID);
            parameters[2] = new SqlParameter("@ItemConclusion", ItemConclusion);
            return DbHelperSQL.ExecuteSql(sql.ToString(), parameters);
            
        }
    }
}
