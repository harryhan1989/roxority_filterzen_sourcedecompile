using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Business.Helper;
using DBAccess;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.Survey
{
    public class Survey_AddToLib_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetItemTable(string UID, string IID)
        {
            initRightSql = new InitRight().initUserRight("UID=@UID ", " ");
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("SELECT * FROM" + TabelName + "WHERE ( "+initRightSql+" AND IID=@IID AND ParentID=0)", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemName(string UID, string IID)
        {
            initRightSql = new InitRight().initUserRight("UID=@UID ", " ");
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("SELECT ItemName FROM" + TabelName + "WHERE ( "+initRightSql+" AND ParentID=@ParentID)", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@ParentID", IID);

            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetOptionTable(string UID, string IID)
        {
            initRightSql = new InitRight().initUserRight("UID=@UID ", " ");
            string TabelName = " " + prefix + "OptionTable ";
            StringBuilder sql = new StringBuilder("SELECT OptionName,Point,IsMatrixRowColumn FROM" + TabelName + "WHERE ( "+initRightSql+" AND IID=@IID)", 100);
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UID", UID);
            parameters[1] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public int InsertItemLib(string SID, string UID, string IID, string ItemName, string ItemType, string ItemHTML, string ItemFrame)
        {
            string TabelName = " " + prefix + "ItemLib ";
            StringBuilder sql = new StringBuilder("INSERT INTO " + TabelName + "(SID,UID,IID,ItemName,ItemType,ItemHTML,ItemFrame) VALUES(@SID,@UID,@IID,@ItemName,@ItemType,@ItemHTML,@ItemFrame)", 100);
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            parameters[3] = new SqlParameter("@ItemName", ItemName);
            parameters[4] = new SqlParameter("@ItemType", ItemType);
            parameters[5] = new SqlParameter("@ItemHTML", ItemHTML);
            parameters[6] = new SqlParameter("@ItemFrame", ItemFrame);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
