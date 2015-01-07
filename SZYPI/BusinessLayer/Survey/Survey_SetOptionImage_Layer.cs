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
    public class Survey_SetOptionImage_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public DataTable GetOptionTable(string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("O.UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            string TabelName1 = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT OID,OptionName,Image FROM" + TabelName + "AS O INNER JOIN" + TabelName1 + "AS I ON O.IID=I.IID WHERE O.SID=@SID AND O.UID=@UID AND O.IID=@IID AND (ItemType=4 OR ItemType=5 OR ItemType=8 OR ItemType=9) ", 50);
            StringBuilder sql = new StringBuilder("SELECT OID,OptionName,Image FROM" + TabelName + "AS O INNER JOIN" + TabelName1 + "AS I ON O.IID=I.IID WHERE O.SID=@SID AND "+initRightSql+" AND O.IID=@IID AND (ItemType=4 OR ItemType=5 OR ItemType=8 OR ItemType=9) ", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public int UpdateOptionTable(string Image, string OID, string UID, string SID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET [Image]=@Image WHERE OID=@OID AND UID=@UID AND SID=@SID AND IID=@IID ", 50);
            StringBuilder sql = new StringBuilder("UPDATE" + TabelName + "SET [Image]=@Image WHERE OID=@OID AND "+initRightSql+" AND SID=@SID AND IID=@IID ", 50);
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@Image", Image);
            parameters[1] = new SqlParameter("@OID", OID);
            parameters[2] = new SqlParameter("@UID", UID);
            parameters[3] = new SqlParameter("@SID", SID);
            parameters[4] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

    }
}
