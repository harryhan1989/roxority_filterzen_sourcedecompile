using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.ShareClass
{
    public class SClass
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";

        public void InsertSurveyExpand(string SID, string UID, string ExpandContent, string ExpandType)
        {
            string TabelName = " " + prefix + "SurveyExpand ";
            StringBuilder sql = new StringBuilder("INSERT INTO " + TabelName + " (SID,UID,ExpandContent,ExpandType) VALUES(@SID,@UID,@ExpandContent,@ExpandType)", 100);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ExpandContent", ExpandContent);
            parameters[3] = new SqlParameter("@ExpandType", ExpandType);
            DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetAllStyleLib(string ID)
        {
            string TabelName = " " + prefix + "StyleLib ";
            StringBuilder sql = new StringBuilder("SELECT * FROM " + TabelName + "WHERE ID=@ID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", ID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetTop1DefaultStyleLib()
        {
            string TabelName = " " + prefix + "StyleLib ";
            StringBuilder sql = new StringBuilder("SELECT * FROM " + TabelName + "WHERE ID=1", 50);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql));
        }

        public int UpdateSurveyExpand(string ExpandContent, string ExpandType, string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ",SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "Set ExpandContent=@ExpandContent WHERE ExpandType=@ExpandType AND SID=@SID AND UID=@UID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "Set ExpandContent=@ExpandContent WHERE ExpandType=@ExpandType AND SID=@SID AND " + initRightSql, 100);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@ExpandContent", ExpandContent);
            parameters[1] = new SqlParameter("@ExpandType", ExpandType);
            parameters[2] = new SqlParameter("@SID", SID);
            parameters[3] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetParBySysBaseInfo(string ParName)
        {
            string TabelName = " " + prefix + "SysBaseInfo ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 Par FROM " + TabelName + "WHERE ParName=@ParName", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ParName", ParName);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetStyleLibByParas(string UID, string FingerPrint)
        {
            initRightSql = new InitRight().initUserRight("UID=@UID ", " ");
            string TabelName = " " + prefix + "StyleLib ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ID FROM " + TabelName + "WHERE UID=@UID and FingerPrint=@FingerPrint", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ID FROM " + TabelName + "WHERE " + initRightSql + " and FingerPrint=@FingerPrint", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@FingerPrint", FingerPrint);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader InsertStyleLib(string UID, string StyleName, string StyleContent, string FingerPrint)
        {
            string TabelName = " " + prefix + "StyleLib ";
            StringBuilder sql = new StringBuilder("INSERT INTO " + TabelName + "(UID,StyleName,StyleContent,FingerPrint) VALUES(@UID,@StyleName,@StyleContent,@FingerPrint)", 50);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@StyleName", FingerPrint);
            parameters[2] = new SqlParameter("@StyleContent", StyleContent);
            parameters[3] = new SqlParameter("@FingerPrint", FingerPrint);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
