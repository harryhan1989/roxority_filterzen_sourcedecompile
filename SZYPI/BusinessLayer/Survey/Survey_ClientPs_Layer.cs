using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DBAccess;
using Business.Helper;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_ClientPs_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public SqlDataReader GetSurveyExpand(string SID)
        {
            string TabelName = " " + prefix + "SurveyExpand ";
            StringBuilder sql = new StringBuilder("SELECT ExpandContent,ExpandType FROM" + TabelName + "WHERE SID=@SID AND ExpandType IN(0,1,8,9)", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyTable(string SID)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 Temppage FROM" + TabelName + "WHERE SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetSurveyTable1(string UID, string SID)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 SurveyName,TempPage,State,Active,SID,Par,Lan FROM" + TabelName + "WHERE  SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetItemTable(string UID, string SID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            string TabelName1 = " " + prefix + "ItemTableExpand ";
            StringBuilder sql = new StringBuilder("SELECT ItemHTML,I.IID,ItemName,PageNo,DataFormatCheck,ItemType,Logic,ParentID,OptionAmount,OptionImgModel,ChildID,MultiReject FROM" + TabelName + "I LEFT JOIN " + TabelName1 + " I1 ON I.IID=I1.IID WHERE  I.SID=@SID ORDER BY PageNo,Sort", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            try
            {
                return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetPageTable(string UID, string SID)
        {
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("SELECT PageNo,PageContent FROM" + TabelName + "WHERE SID=@SID ORDER BY PageNo", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetHeadFoot(string UID, string SID)
        {
            string TabelName = " " + prefix + "HeadFoot ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 PageHead,PageFoot FROM" + TabelName + "WHERE SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetOptionTable(string UID, string SID)
        {
            string TabelName = " " + prefix + "OptionTable ";
            StringBuilder sql = new StringBuilder("SELECT  OID,Point,IID,ParentNode,OptionName,IsMatrixRowColumn FROM" + TabelName + "WHERE SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetAnswerDetail(string AnswerGUID, string SID)
        {
            string TabelName = " " + prefix + "AnswerDetail ";
            StringBuilder sql = new StringBuilder("SELECT [ID],[SID],[IID],[Answer],[Point],[AnswerGUID] FROM" + TabelName + "WHERE AnswerGUID=@AnswerGUID AND SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@AnswerGUID", AnswerGUID);
            parameters[1] = new SqlParameter("@SID", SID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

    }
}
