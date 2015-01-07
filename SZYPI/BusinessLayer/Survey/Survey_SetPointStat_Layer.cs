using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DBAccess;
using Business.Helper;

namespace BusinessLayer.Survey
{
    public class Survey_SetPointStat_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetReviewPoint(string SID, string UID, string ID)
        {

            string TabelName = " " + prefix + "ReviewPoint ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 * FROM" + TabelName + "  WHERE SID=@SID AND UID=@UID AND ID=@ID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ID", ID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public int InsertReviewPoint(string Relation1,string Relation2,string MaxValue,string MinValue,string Result,string SID,string UID)
        {
            string TabelName = " " + prefix + "ReviewPoint ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(Relation1,Relation2,MaxValue,MinValue,Result,SID,UID) VALUES(@Relation1,@Relation2,@MaxValue,@MinValue,@Result,@SID,@UID)", 100);
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@Relation1", Relation1);
            parameters[1] = new SqlParameter("@Relation2", Relation2);
            parameters[2] = new SqlParameter("@MaxValue", MaxValue);
            parameters[3] = new SqlParameter("@MinValue", MinValue);
            parameters[4] = new SqlParameter("@Result", Result);
            parameters[5] = new SqlParameter("@SID", SID);
            parameters[6] = new SqlParameter("@UID", UID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int DeleteReviewPoint(string SID, string UID, string ID)
        {
            string TabelName = " " + prefix + "ReviewPoint ";
            StringBuilder sql = new StringBuilder("DELETE FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND ID=@ID", 100);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ID", ID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int UpdateReviewPoint(string Relation1, string Relation2, string MaxValue, string MinValue, string Result, string SID, string UID,string ID)
        {
            string TabelName = " " + prefix + "ReviewPoint ";
            StringBuilder sql = new StringBuilder("Update" + TabelName + "set Relation1=@Relation1,Relation2=@Relation2,MaxValue=@MaxValue,MinValue=@MinValue,Result=@Result WHERE SID=@SID AND UID=@UID AND ID=@ID", 100);
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@Relation1", Relation1);
            parameters[1] = new SqlParameter("@Relation2", Relation2);
            parameters[2] = new SqlParameter("@MaxValue", MaxValue);
            parameters[3] = new SqlParameter("@MinValue", MinValue);
            parameters[4] = new SqlParameter("@Result", Result);
            parameters[5] = new SqlParameter("@SID", SID);
            parameters[6] = new SqlParameter("@UID", UID);
            parameters[7] = new SqlParameter("@ID", ID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetReviewPoint1(string SID, string UID)
        {
            string TabelName = " " + prefix + "ReviewPoint ";
            StringBuilder sql = new StringBuilder("SELECT  * FROM" + TabelName + "  WHERE SID=@SID AND UID=@UID", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

    }
}
