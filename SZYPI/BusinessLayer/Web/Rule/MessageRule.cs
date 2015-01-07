using DBAccess;
using Business.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using EntityModel.WebEntity;
using System.Collections.Generic;

namespace BusinessLayer.Web.Rule
{
    public class MessageRule
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddMessage(MessageEntity messageEntity)
        {
            var strSql = new StringBuilder(150);
            strSql.Append("insert into UT_Info_MessageBoard(");
            strSql.Append("MessageCategory,Subject,Content,PersonName,SubmitDate,Address,ContactPhone,Email,Status,IsDeleted)");
            strSql.Append(" values (");
            strSql.Append("@MessageCategory,@Subject,@Content,@PersonName,@SubmitDate,@Address,@LinkPhone,@Email,@Status,@IsDel)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] parameters = {
                    new SqlParameter("@MessageCategory", SqlDbType.Int),
					new SqlParameter("@Subject", SqlDbType.NVarChar,200),
					new SqlParameter("@Content", SqlDbType.Text),
					new SqlParameter("@PersonName", SqlDbType.NVarChar,50),
                    new SqlParameter("@SubmitDate", SqlDbType.DateTime),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@LinkPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@IsDel",SqlDbType.Bit)
                                        };

            parameters[0].Value = messageEntity.MessageCategory;
            parameters[1].Value = messageEntity.Subject;
            parameters[2].Value = messageEntity.Content;
            parameters[3].Value = messageEntity.PersonName;
            parameters[4].Value = messageEntity.SubmitDate;
            parameters[5].Value = messageEntity.Address;
            parameters[6].Value = messageEntity.ContactPhone;
            parameters[7].Value = messageEntity.Email;
            parameters[8].Value = messageEntity.Status;
            parameters[9].Value = messageEntity.IsDeleted ;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);

            if (obj == null)
            {
                return 1;
            }
            return ConvertHelper.ConvertInt(obj);
        }

        #region 获取详细信息
        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <returns></returns>
        public List<MessageEntity> MessageByID(long MessageID)
        {
            string strSql = "select * from UT_Info_MessageBoard where MessageBoardID=@MessageID";

            SqlParameter[] parameters = {
                    new SqlParameter("@MessageID", SqlDbType.BigInt)
                                        };

            parameters[0].Value = MessageID;

            List<MessageEntity> MessageEntity = null;

            MessageEntity messageEntity = new MessageEntity();
            SqlDataReader sqlReader;

            sqlReader = DbHelper.ExecuteReader(strSql,parameters);

            while (sqlReader.Read())
            {
                if (MessageEntity == null)
                {
                    MessageEntity = new List<MessageEntity>();
                    messageEntity.Address = sqlReader["Address"].ToString();
                    messageEntity.Subject = sqlReader["Subject"].ToString();
                    messageEntity.SubmitDate = ConvertHelper.ConvertDateTime(sqlReader["SubmitDate"].ToString());
                    messageEntity.PersonName = sqlReader["PersonName"].ToString();
                    messageEntity.RerutnDate = ConvertHelper.ConvertDateTime(sqlReader["RerutnDate"].ToString());
                    messageEntity.ReturnContent = sqlReader["ReturnContent"].ToString();
                    messageEntity.Content = sqlReader["Content"].ToString();
                    messageEntity.ReturnUnit = sqlReader["ReturnUnit"].ToString();
                    messageEntity.UserID = ConvertHelper.ConvertLong(sqlReader["UserID"].ToString());
                    messageEntity.MessageCategory = ConvertHelper.ConvertInt(sqlReader["MessageCategory"]);
                    messageEntity.IsPublic = ConvertHelper.ConvertInt(sqlReader["IsPublic"]);
                    messageEntity.ClickRate = ConvertHelper.ConvertLong(sqlReader["ClickRate"]);
                }
            }

            MessageEntity.Add(messageEntity);

            return MessageEntity;
        }
        #endregion

        #region 更新点击率
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageID"></param>
        public void UpdateClickRateByID(long MessageID)
        {
            string strSql = "update UT_Info_MessageBoard set ClickRate=ClickRate+1 where MessageBoardID=@MessageID";
            SqlParameter[] parameters = {
					new SqlParameter("@MessageID", SqlDbType.BigInt)
                                         };
            parameters[0].Value = MessageID;

            DbHelperSQL.ExecuteSql(strSql, parameters);
        }
        #endregion
    }
}
