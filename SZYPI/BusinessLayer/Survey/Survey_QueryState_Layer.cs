using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using System.Data;

namespace BusinessLayer.Survey
{
    public class Survey_QueryState_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public DataTable GetBySql(string sql)
        {
            //initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("SELECT Max(PageNo) FROM " + TabelName + "WHERE " + initRightSql + " AND SID=@SID", 50);
            //SqlParameter[] parameters = new SqlParameter[2];
            //parameters[0] = new SqlParameter("@UID", UID);
            //parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(sql);
        }

        public SqlDataReader GetItemTable(string SID,string UID)
        {
            //initRightSql = new InitRight().initSurveyRight("UID=@UID AND", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("SELECT IID,ItemName,ItemType,ParentID,OptionAmount FROM " + TabelName + "WHERE " + initRightSql + " SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(sql.ToString(),parameters);
        }

        public SqlDataReader GetOptionTable(string SID, string UID)
        {
            //initRightSql = new InitRight().initSurveyRight("UID=@UID AND", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            StringBuilder sql = new StringBuilder("SELECT OID,OptionName,IID,Point,IsMatrixRowColumn FROM" + TabelName + "WHERE " + initRightSql + " SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(sql.ToString(), parameters);
        }

        public SqlDataReader GetItemTable1(string SID, string UID)
        {
            //initRightSql = new InitRight().initSurveyRight("UID=@UID AND", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("SELECT IID,ItemName,ItemType,ParentID,OptionAmount,Logic FROM " + TabelName + "WHERE " + initRightSql + " SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(sql.ToString(), parameters);
        }

        public SqlDataReader GetOptionTable1(string SID, string UID)
        {
            //initRightSql = new InitRight().initSurveyRight("UID=@UID AND", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            StringBuilder sql = new StringBuilder("SELECT OID,OptionName,IID FROM " + TabelName + "WHERE " + initRightSql + " SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(sql.ToString(), parameters);
        }
    }
}
