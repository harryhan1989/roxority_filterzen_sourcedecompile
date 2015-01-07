using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BusinessLayer.CreateItem;
using Business.Helper;
using DBAccess.Entity;
using System.Text.RegularExpressions;

namespace Web_SubCode.CreateItem
{
    public class SerialNumber
    {
        /// <summary>
        /// 根据问卷ID进行问题编号，编号规则，根据页排序，在根据题目排序
        /// </summary>
        /// <param name="SID">问卷ID</param>
        public void SetSerialNumber(string SID)
        {
            SqlDataReader sqlDataReader;
            int count = 1;
            string TabelName;
            string prefix=new SerialNumber_Layer().prefix;
            TabelName= " " + prefix + "ItemTable ";
            List<SqlTransEntity> sqlparameters = new List<SqlTransEntity>();
            sqlDataReader = new SerialNumber_Layer().GetItemTable(SID);
            while (sqlDataReader.Read())
            {
                string itemHTML = ConvertHelper.ConvertString(sqlDataReader["ItemHTML"]);
                string DataFormatCheck = ConvertHelper.ConvertString(sqlDataReader["DataFormatCheck"]);
                if (!string.IsNullOrEmpty(itemHTML) && itemHTML != "")
                {
                    itemHTML = Regex.Replace(itemHTML, "<span class=\"SerialNumberSortStyle\">[^<]*</span>", "<span class=\"SerialNumberSortStyle\">" + count+"、" + "</span>");

                    if (DataFormatCheck.IndexOf("Empty1") >= 0)
                    {
                        itemHTML = Regex.Replace(itemHTML, "<span class=\"IsRequiredStyle\">[^<]*</span>", "<span class=\"IsRequiredStyle\">" + "*" + "</span>");
                    }

                    SqlTransEntity sqlTransEntity = new SqlTransEntity();
                    sqlTransEntity.SqlCommandText = "Update " + TabelName + "set ItemHTML=@itemHTML where IID=@IID";

                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@itemHTML", itemHTML);
                    parameters[1] = new SqlParameter("@IID", ConvertHelper.ConvertString(sqlDataReader["IID"]));
                    sqlTransEntity.SqlParameters = parameters;
                    sqlparameters.Add(sqlTransEntity);                   

                    count++;
                }
            }
            new SerialNumber_Layer().ExecProcedureByTrans(sqlparameters);
        }

    }
}
