using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using System.Data;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_WriteSurveyFile_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetSurveyExpand(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent,ExpandType FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND (ExpandType=0 OR ExpandType=8)", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent,ExpandType FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND (ExpandType=0 OR ExpandType=8)", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyExpand1(string SID, string ExpandType)
        {
            string TabelName = " " + prefix + "SurveyExpand ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ExpandContent,ExpandType FROM" + TabelName + "WHERE SID=@SID AND ExpandType=@ExpandType ", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@ExpandType", ExpandType);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetItemTable(string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("I.UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            string TabelName1 = " " + prefix + "ItemTableExpand ";
            //StringBuilder sql = new StringBuilder("SELECT ItemHTML,I.IID,ItemName,PageNo,DataFormatCheck,ItemType,Logic,ParentID,OptionAmount,OptionImgModel,ChildID,MultiReject FROM" + TabelName + "I LEFT JOIN" + TabelName1 + "I1 ON I.IID=I1.IID WHERE I.UID=@UID AND I.SID=@SID ORDER BY PageNo,Sort", 50);
            StringBuilder sql = new StringBuilder("SELECT ItemHTML,I.IID,ItemName,PageNo,DataFormatCheck,ItemType,Logic,ParentID,OptionAmount,OptionImgModel,ChildID,MultiReject FROM" + TabelName + "I LEFT JOIN" + TabelName1 + "I1 ON I.IID=I1.IID WHERE " + initRightSql + " AND I.SID=@SID ORDER BY PageNo,Sort", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql),parameters);
        }

        public DataTable GetSurveyTable(string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 SurveyName,TempPage,State,Active,SID,Par,SID FROM" + TabelName + " WHERE UID=@UID AND SID=@SID", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 SurveyName,TempPage,State,Active,SID,Par,SID FROM" + TabelName + " WHERE " + initRightSql + " AND SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetPageTable(string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "PageTable ";
            //StringBuilder sql = new StringBuilder("SELECT MAX(PageNo) AS MAXPageNo FROM" + TabelName + " WHERE UID=@UID AND SID=@SID", 50);
            StringBuilder sql = new StringBuilder("SELECT MAX(PageNo) AS MAXPageNo FROM" + TabelName + " WHERE " + initRightSql + " AND SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetOptionTable(string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("SELECT  OID,Point,IID,ParentNode,OptionName,ISMatrixRowColumn FROM" + TabelName + " WHERE UID=@UID AND SID=@SID", 50);
            StringBuilder sql = new StringBuilder("SELECT  OID,Point,IID,ParentNode,OptionName,ISMatrixRowColumn FROM" + TabelName + " WHERE " + initRightSql + " AND SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public int UpdateSurveyTable(string State, string Active, string SID)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + "SET State=@State,Active=@Active WHERE SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@State", State);
            parameters[1] = new SqlParameter("@Active", Active);
            parameters[2] = new SqlParameter("@SID", SID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public void ExcutSql(string sql)
        {
            DbHelperSQL.Fill(sql);
        }


        public int DeleteSurveyExpand(string SID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "SurveyExpand ";
            //StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND (ExpandType=3 OR ExpandType=4)", 50);
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND (ExpandType=3 OR ExpandType=4)", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int InsertSurveyExpand(string SID,string UID,string ExpandType,string ExpandContent)
        {
            string TabelName = " " + prefix + "SurveyExpand ";
            StringBuilder sql = new StringBuilder("INSERT INTO " + TabelName + "(SID,UID,ExpandType,ExpandContent) VALUES(@SID,@UID,@ExpandType,@ExpandContent)", 50);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ExpandType", ExpandType);
            parameters[3] = new SqlParameter("@ExpandContent", ExpandContent);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
