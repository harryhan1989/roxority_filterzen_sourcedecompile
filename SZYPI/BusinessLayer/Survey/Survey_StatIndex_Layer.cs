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
    public class Survey_StatIndex_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetSurveyTable(int topnum,string UID)
        {
            initRightSql = new InitRight().initResultSurveyRight("UID=@UID ", " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP " + topnum + " SID,SurveyName from" + TabelName + " WHERE  UID=@UID AND State=1 and IsDel=0 ORDER BY SID DESC", 100);
            StringBuilder sql = new StringBuilder("SELECT SID,SurveyName from" + TabelName + " WHERE  " + initRightSql + " AND State=1 and IsDel=0 ORDER BY SID DESC", 100);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UID", UID);
            //return DbHelperSQL.RunProcedureGetDataSet("Z_UP_GetDataProc", parameters);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataSet GetSurveyTableSelectGrid(string UID, string TxtSurveyName, int GetBackNum, int DdlSurveyClass, DateTime WdcBeginDate, DateTime WdcEndDate, int PageIndex, int PageSize)
        {
            initRightSql = new InitRight().initResultSurveyRight("UID= " + UID, " ");
            string TabelName = " " + "UV_QS_SurveyTable";
            //StringBuilder sql = new StringBuilder("SELECT TOP " + topnum + " SID,SurveyName from" + TabelName + " WHERE  UID=@UID AND State=1 and IsDel=0 ORDER BY SID DESC", 100);
            string sql = "SELECT * from" + TabelName + " WHERE  " + initRightSql + " AND State=1 and IsDel=0 ";


            //根据名称筛选
            if (!string.IsNullOrEmpty(TxtSurveyName))
            {
                if (sql.ToString().ToLower().Contains("where"))
                {
                    sql += " SurveyName like '%" + TxtSurveyName + "%' ";
                }
                else
                {
                    sql += " where SurveyName like '%" + TxtSurveyName + "%' ";
                }
            }

            //回收数筛选
            if (!GetBackNum.Equals(-1))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND AnswerAmount=" + GetBackNum.ToString();
                }
                else
                {
                    sql += " WHERE AnswerAmount=" + GetBackNum.ToString();
                }
            }

            //问卷类型筛选
            if (!DdlSurveyClass.Equals(-1))
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND ClassID=" + DdlSurveyClass.ToString();
                }
                else
                {
                    sql += " WHERE ClassID=" + DdlSurveyClass.ToString();
                }
            }

            //开始时间筛选
            if (WdcBeginDate != null)
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND datediff(day,createdate,'" + WdcBeginDate.ToString("yyyy-MM-dd") + "')<=0";
                }
                else
                {
                    sql += " WHERE datediff(day,createdate,'" + WdcBeginDate.ToString("yyyy-MM-dd") + "')<=0";
                }
            }

            //结束时间筛选
            if (WdcEndDate != null)
            {
                if (sql.ToLower().Contains("where"))
                {
                    sql += " AND datediff(day,createdate,'" + WdcEndDate.ToString("yyyy-MM-dd") + "')>=0";
                }
                else
                {
                    sql += " WHERE datediff(day,createdate,'" + WdcEndDate.ToString("yyyy-MM-dd") + "')>=0";
                }
            }

            sql+=" ORDER BY SID DESC ";

            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@sqlstr", sql.ToString());
            parameters[1] = new SqlParameter("@PageSize", PageSize);
            parameters[2] = new SqlParameter("@pageindex", PageIndex);
            return DbHelperSQL.RunProcedureGetDataSet("Z_UP_GetDataProc", parameters);
        }

        public DataTable GetItemTable(string SID, string UID)
        {
            initRightSql = new InitRight().initSIDResultSurveyRight("UID=@UID ",SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT  * FROM" + TabelName + " WHERE SID=@SID AND UID=@UID AND ParentID=0 ORDER BY PageNo,Sort", 100);
            StringBuilder sql = new StringBuilder("SELECT  * FROM" + TabelName + " WHERE SID=@SID AND " + initRightSql + " AND ParentID=0 ORDER BY PageNo,Sort", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetSurveyTable1(string SID, string UID)
        {
            initRightSql = new InitRight().initSIDResultSurveyRight("UID=@UID ",SID, " ");
            string TabelName = " " + prefix + "SurveyTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 SurveyName FROM" + TabelName + " WHERE SID=@SID AND UID=@UID AND State=1", 100);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 SurveyName FROM" + TabelName + " WHERE SID=@SID AND " + initRightSql + " AND State=1", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
