using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using DBAccess;
using Business.Helper;
using DBAccess.Entity;

namespace BusinessLayer.CreateItem
{
    public class SerialNumber_Layer
    {
        public string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        public SqlDataReader GetItemTable(string SID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("select IID, UID, SID, PageNo, ItemName, ItemType, DataFormatCheck, ItemContent, Sort, ItemHTML, OptionAmount, ParentID, StyleMode, OrderModel, OtherProperty, Logic, StatAmount, OptionImgModel, ChildID From" + TabelName + " where ParentID=0 and SID=@SID order by PageNo asc,Sort asc ", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql),parameters);
        }

        public void ExecProcedureByTrans(List<SqlTransEntity> sqlTransEntities)
        {
            DbHelperSQL.ExecProcedureByTrans(sqlTransEntities);
        }
    }
}
