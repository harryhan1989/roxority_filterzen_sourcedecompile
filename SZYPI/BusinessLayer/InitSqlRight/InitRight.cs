using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Helper;
using System.Web;
using System.Data.SqlClient;
using DBAccess;
using System.Data;

namespace BusinessLayer.InitSqlRight
{
    public class InitRight
    {
        public string initSurveyRight(string beforeSql,string SID,string afterSql)
        {
            string UID = "";
            UID = ConvertHelper.ConvertString(HttpContext.Current.Session["UserID"]);
            string GID = GetGID(UID);
            string st1 = beforeSql.Split('=')[0];
            if (st1.Split('.').Length > 1)
            {
                st1 = st1.Split('.')[0];
            }
            else
            {
                st1 = "";
            }
            string UserGroup = GetGIDList(GID);
            string UIDList = GetUIDList(UserGroup);
            string sql = "";
            sql +="("+ beforeSql;
            if (UIDList != "")
            {
                sql += " or ";
                if (st1 == "" || st1 == null)
                {
                    sql += " UID IN (";
                }
                else
                {
                    sql += " " + st1 + ".UID IN (";
                }
                sql += UIDList;
                sql += "))";
            }
            else
            {
                sql += ")";
            }

            return sql;
        }

        public string initResultSurveyRight(string beforeSql, string afterSql)
        {
            string UID = "";
            UID = ConvertHelper.ConvertString(HttpContext.Current.Session["UserID"]);
            //string Spar = GetSurveyPar(SID);

            string GID = GetGID(UID);
            string st1 = beforeSql.Split('=')[0];
            if (st1.Split('.').Length > 1)
            {
                st1 = st1.Split('.')[0];
            }
            else
            {
                st1 = "";
            }
            string UserGroup = GetGIDList(GID);
            string UIDList = GetUIDList(UserGroup);
            string sql = "";
            sql += "(" + beforeSql;
            if (UIDList != "")
            {
                sql += " or ";
                if (st1 == "" || st1 == null)
                {
                    sql += " UID IN (";
                    sql += UIDList;
                    sql += ")";
                }
                else
                {
                    sql += " " + st1 + ".UID IN (";
                    sql += UIDList;
                    sql += ")";
                }               
                sql += GetParSqlRight( UID, st1);
                sql += ")";
            }
            else
            {
                sql += ")";
            }

            return sql;
        }

        public string initSIDResultSurveyRight(string beforeSql, string SID,string afterSql)
        {
            string UID = "";
            UID = ConvertHelper.ConvertString(HttpContext.Current.Session["UserID"]);
            string Spar = GetSurveyPar(SID);

            string GID = GetGID(UID);
            string st1 = beforeSql.Split('=')[0];
            if (st1.Split('.').Length > 1)
            {
                st1 = st1.Split('.')[0];
            }
            else
            {
                st1 = "";
            }
            string UserGroup = GetGIDList(GID);
            string UIDList = GetUIDList(UserGroup);
            string sql = "";
            sql += "(" + beforeSql;
            if (UIDList != "")
            {
                sql += " or ";
                if (st1 == "" || st1 == null)
                {
                    sql += " UID IN (";
                    sql += UIDList;
                    sql += ")";
                }
                else
                {
                    sql += " " + st1 + ".UID IN (";
                    sql += UIDList;
                    sql += ")";
                }
                sql += GetSIDParSqlRight(Spar, UID, st1);
                sql += ")";
            }
            else
            {
                sql += ")";
            }

            return sql;
        }

        public string GetSIDParSqlRight(string Par,string UID, string UIDTitle)
        {
            string sql = "";
            string UIDList = GetSameGroupUID(UID);
            if (Par.IndexOf("|ResultPublish:1") > 0)
            {
            
            }
            else if (Par.IndexOf("|ResultPublish:2") > 0)
            {
                sql += " or 2=2"; //完全对外公开

            }
            else if (Par.IndexOf("|ResultPublish:3") > 0)
            {
                sql += " or ";
                sql += UIDTitle +  "UID IN (" + UIDList + ")";  //仅对组内公开  
            }


            //string UIDList = GetSameGroupUID(UID);
            //if (UIDList != "")
            //{
            //    sql += " or ";
            //    sql += " ( patindex('%ResultPublish:3%',Par)>0 ";
            //    sql += UIDTitle + " and UID IN (" + UIDList + "))";  //仅对组内公开  
            //    sql += " or ( patindex('%ResultPublish:2%',Par)>0 )";
            //}
            return sql;
        }

        public string GetParSqlRight(string UID,string UIDTitle)
        {
            string sql = "";
            
            string UIDList = GetSameGroupUID(UID);
            if (UIDList != "")
            {
                sql += " or ";
                sql += " ( patindex('%ResultPublish:3%',Par)>0 ";
                sql += " and "+UIDTitle + "UID IN (" + UIDList + "))";  //仅对组内公开  
                sql += " or ( patindex('%ResultPublish:2%',Par)>0 )";
            }
            return sql;
        }

        public string initUserRight(string beforeSql, string afterSql)
        {
            string UID = "";
            UID = ConvertHelper.ConvertString(HttpContext.Current.Session["UserID"]);
            string GID = GetGID(UID);
            string st1 = beforeSql.Split('=')[0];
            if (st1.Split('.').Length > 1)
            {
                st1 = st1.Split('.')[0];
            }
            else
            {
                st1 = "";
            }
            string UserGroup = GetGIDList(GID);
            string UIDList=GetUIDList(UserGroup);
            string sql = "";
            sql += "("+beforeSql;
            if (UIDList != "")
            {
                sql += " or ";
                if (st1 == "" || st1 == null)
                {
                    sql += " UID IN (";
                }
                else
                {
                    sql += " " + st1 + ".UID IN (";
                }
                sql += UIDList;
                sql += "))";
            }
            else
            {
                sql += ")";
            }
            return sql;
        }

        public string GetGIDList(string GID)
        {
            string GIDList = "";

            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@Type", "all");
            parameters[1] = new SqlParameter("@TableName", "UT_QS_UserGroup");
            parameters[2] = new SqlParameter("@PrimaryField", "GID");
            parameters[3] = new SqlParameter("@ParentField", "ParentGID");
            parameters[4] = new SqlParameter("@CurrentID", GID);
            parameters[5] = new SqlParameter("@ReturnValueType", "0");
            parameters[6] = new SqlParameter("@IsIncludeSelf", 0);

            DataTable dt= DbHelperSQL.ExecProcedure(parameters, "UP_Navigate");
            if (dt.Rows.Count > 0)
            {
                GIDList = ConvertHelper.ConvertString(dt.Rows[0][0]);
            }
            return GIDList;
        }

        public string GetUIDList(string GIDList)
        {
            string UIDList = "";
            string Condition = " GID in(" + GIDList + ")";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@tableName", "UT_SYS_User");
            parameters[1] = new SqlParameter("@columnName", "UserID");
            parameters[2] = new SqlParameter("@Condition", Condition);

            DataTable dt = DbHelperSQL.ExecProcedure(parameters, "UP_GetColomnValue");
            if (dt.Rows.Count > 0)
            {
                UIDList = ConvertHelper.ConvertString(dt.Rows[0][0]);
            }

            return UIDList;

        }

        public string GetGID(string UID)
        {
            StringBuilder sql = new StringBuilder("SELECT TOP 1 GID FROM UT_SYS_User WHERE UserID=@UID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UID", UID);

            return DbHelperSQL.ExecuteScalar(sql.ToString(), parameters);
        }

        public string GetSurveyPar(string SID)
        {
            StringBuilder sql = new StringBuilder("SELECT TOP 1 Par FROM UT_QS_SurveyTable WHERE SID=@SID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);

            return DbHelperSQL.ExecuteScalar(sql.ToString(), parameters);
        }

        public string GetSameGroupUID(string UID)
        {
            string UIDList = "";
            //StringBuilder sql = new StringBuilder("SELECT UserID FROM UT_SYS_User WHERE GID IN (SELECT GID FROM UT_SYS_User WHERE UserID=@UID)", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@UID", UID);

            DataTable dt = DbHelperSQL.ExecProcedure(parameters, "UP_QS_GetUserByGID");
            if (dt.Rows.Count > 0)
            {
                UIDList = ConvertHelper.ConvertString(dt.Rows[0][0]);
            }
            return UIDList;
        }
    }
}
