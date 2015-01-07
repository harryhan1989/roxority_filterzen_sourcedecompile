using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
   public  class Survey_SortOutPage_Layer
    {
       string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
       string initRightSql = "";
       public SqlDataReader GetItemTable(string UID, string SID)
       {
           initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
           string TabelName = " " + prefix + "ItemTable ";
           //StringBuilder sql = new StringBuilder("SELECT IID,ItemName,PageNo FROM " + TabelName + "WHERE UID=@UID AND SID=@SID AND ParentID=0 ORDER BY PageNo", 50);
           StringBuilder sql = new StringBuilder("SELECT IID,ItemName,PageNo FROM " + TabelName + "WHERE " + initRightSql + " AND SID=@SID AND ParentID=0 ORDER BY PageNo", 50);
           SqlParameter[] parameters = new SqlParameter[2];
           parameters[0] = new SqlParameter("@UID", UID);
           parameters[1] = new SqlParameter("@SID", SID);
           return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
       }

       public SqlDataReader GetPageTable(string UID, string SID)
       {
           initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
           string TabelName = " " + prefix + "PageTable ";
           //StringBuilder sql = new StringBuilder("SELECT PID,PageNo FROM " + TabelName + "WHERE UID=@UID AND SID=@SID ORDER BY PageNo", 50);
           StringBuilder sql = new StringBuilder("SELECT PID,PageNo FROM " + TabelName + "WHERE " + initRightSql + " AND SID=@SID ORDER BY PageNo", 50);
           SqlParameter[] parameters = new SqlParameter[2];
           parameters[0] = new SqlParameter("@UID", UID);
           parameters[1] = new SqlParameter("@SID", SID);
           return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
       }

       public SqlDataReader GetPageTable1(string UID, string SID)
       {
           initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
           string TabelName = " " + prefix + "PageTable ";
           //StringBuilder sql = new StringBuilder("SELECT Max(PageNo) FROM " + TabelName + "WHERE UID=@UID AND SID=@SID", 50);
           StringBuilder sql = new StringBuilder("SELECT Max(PageNo) FROM " + TabelName + "WHERE " + initRightSql + " AND SID=@SID", 50);
           SqlParameter[] parameters = new SqlParameter[2];
           parameters[0] = new SqlParameter("@UID", UID);
           parameters[1] = new SqlParameter("@SID", SID);
           return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
       }

       public int UpdateItemTable(string PageNo, string SID, string UID, string IID)
       {
           initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
           string TabelName = " " + prefix + "ItemTable ";
           //StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET PageNo=@PageNo WHERE SID=@SID AND UID=@UID AND IID IN(@IID)", 100);
           StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET PageNo=@PageNo WHERE SID=@SID AND " + initRightSql + " AND IID IN ( " + IID + " ) ", 100);
           SqlParameter[] parameters = new SqlParameter[3];
           parameters[0] = new SqlParameter("@PageNo", PageNo);
           parameters[1] = new SqlParameter("@SID", SID);
           parameters[2] = new SqlParameter("@UID", UID);
           //parameters[3] = new SqlParameter("@IID", IID);
           try
           {
               return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
           }
           catch
           {
               return 0;
           }
       }

       public int InsertPageTable(string UID, string SID, string PageNo)
       {
           string TabelName = " " + prefix + "PageTable ";
           StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(UID,SID,PageNo) VALUES(@UID,@SID,@PageNo)", 100);
           SqlParameter[] parameters = new SqlParameter[3];
           parameters[0] = new SqlParameter("@UID", UID);
           parameters[1] = new SqlParameter("@SID", SID);
           parameters[2] = new SqlParameter("@PageNo", PageNo);
           return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
       }

       public int DeletePageTable(string UID, string SID, string PID)
       {
           initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
           string TabelName = " " + prefix + "PageTable ";
           //StringBuilder sql = new StringBuilder("DELETE FROM" + TabelName + "WHERE UID=@UID AND SID=@SID AND PID=@PID", 100);
           StringBuilder sql = new StringBuilder("DELETE FROM" + TabelName + "WHERE " + initRightSql + " AND SID=@SID AND PID=@PID", 100);
           SqlParameter[] parameters = new SqlParameter[3];
           parameters[0] = new SqlParameter("@UID", UID);
           parameters[1] = new SqlParameter("@SID", SID);
           parameters[2] = new SqlParameter("@PID", PID);
           return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
       }

       public int UpdatePageTable(string UID, string SID, string PageNo)
       {
           initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
           string TabelName = " " + prefix + "PageTable ";
           //StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET PageNo=PageNo-1 WHERE UID=@UID AND SID=@SID AND PageNo>@PageNo", 100);
           StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET PageNo=PageNo-1 WHERE " + initRightSql + " AND SID=@SID AND PageNo>@PageNo", 100);
           SqlParameter[] parameters = new SqlParameter[3];
           parameters[0] = new SqlParameter("@UID", UID);
           parameters[1] = new SqlParameter("@SID", SID);
           parameters[2] = new SqlParameter("@PageNo", PageNo);
           return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
       }

       public int UpdateItemTable(string UID, string SID, string PageNo)
       {
           initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
           string TabelName = " " + prefix + "ItemTable ";
           //StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET PageNo=PageNo-1 WHERE  UID=@UID AND SID=@SID AND PageNo>@PageNo", 100);
           StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET PageNo=PageNo-1 WHERE  " + initRightSql + " AND SID=@SID AND PageNo>@PageNo", 100);
           SqlParameter[] parameters = new SqlParameter[3];
           parameters[0] = new SqlParameter("@UID", UID);
           parameters[1] = new SqlParameter("@SID", SID);
           parameters[2] = new SqlParameter("@PageNo", PageNo);
           return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
       }

       public int UpdateItemTable1(string SID,string UID)
       {
           initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
           string TabelName = " " + prefix + "ItemTable ";
           //StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET Logic='' WHERE  SID=@SID AND UID=@UID ", 100);
           StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET Logic='' WHERE  SID=@SID AND "+initRightSql, 100);
           SqlParameter[] parameters = new SqlParameter[2];
           parameters[0] = new SqlParameter("@SID", SID);
           parameters[1] = new SqlParameter("@UID", UID);
           return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
       }

    }
}
