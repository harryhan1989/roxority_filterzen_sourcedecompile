using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DBAccess;
using Business.Helper;
using System.Data.SqlClient;
using System.Data;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_ModifyItems_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public DataTable GetItemTable(string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 IID,ItemName,ItemType,DataFormatCheck,PageNo,OtherProperty,OptionImgModel,OrderModel,OptionAmount,ItemContent FROM" + TabelName + "where SID=@SID AND UID=@UID AND IID=@IID", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 IID,ItemName,ItemType,DataFormatCheck,PageNo,OtherProperty,OptionImgModel,OrderModel,OptionAmount,ItemContent FROM" + TabelName + "where SID=@SID AND "+initRightSql+"AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            try
            {
                return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetSubItemTable(string SID, string UID, string ParentID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 200 IID,ItemName,ItemType,DataFormatCheck,PageNo,OtherProperty,OptionImgModel,OrderModel FROM" + TabelName + "where SID=@SID AND UID=@UID AND ParentID=@ParentID", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 200 IID,ItemName,ItemType,DataFormatCheck,PageNo,OtherProperty,OptionImgModel,OrderModel FROM" + TabelName + "where SID=@SID AND "+initRightSql+" AND ParentID=@ParentID", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ParentID", ParentID);

            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetOptionTable(string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("SELECT OptionName,ISMatrixRowColumn FROM" + TabelName + "where SID=@SID AND UID=@UID AND IID=@IID", 50);
            StringBuilder sql = new StringBuilder("SELECT OptionName,ISMatrixRowColumn FROM" + TabelName + "where SID=@SID AND "+initRightSql+" AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);

            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetPageTable(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("SELECT PID FROM" + TabelName + "where SID=@SID AND " + initRightSql, 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);

            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }


    }
}
