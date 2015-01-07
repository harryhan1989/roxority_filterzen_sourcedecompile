using DBAccess;
using Business.Helper;
using System.Data;
using System.Text;
using EntityModel.WebEntity;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BusinessLayer.Web.Rule
{
    /// <summary>
    /// 作者：刘娟
    /// 时间：2010-4-15
    /// 目的：用户帐户信息
    /// </summary>
    public class UserRule
    {
        #region  根据帐号获得人员信息
        /// <summary>
        /// 根据帐号获得人员信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetUserInfoByAccount(string Account)
        {
            string sql = "SELECT * FROM UT_SYS_User WHERE IsDeleted=0 AND Account = @Account";

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Account", Account));

            DataTable dt = DbHelper.Fill(sql, paramList);

            return dt;
        }

        /// <summary>
        /// 是否存在帐户 
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable IsExistByAccount(string Account)
        {
            string sql = "SELECT count(*) FROM UT_SYS_User WHERE IsDeleted=0 AND Account = @Account";

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Account", Account));

            DataTable dt = DbHelper.Fill(sql, paramList);

            return dt;
        }

        /// <summary>
        /// 泛型获取信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public List<UserEntity> GetUserByAccount(string Account)
        {
            string sql = "SELECT * FROM UT_SYS_User WHERE IsDeleted=0 AND Account = @Account";

            SqlParameter[] parameters = {
					new SqlParameter("@Account", SqlDbType.NVarChar,50)
                                         };
            parameters[0].Value = Account;

            List<UserEntity> userEntity = null;
            UserEntity model = new UserEntity();
            SqlDataReader sqlReader;

            sqlReader = DbHelper.ExecuteReader(sql, parameters);

            while (sqlReader.Read())
            {
                if (userEntity == null)
                {
                    userEntity = new List<UserEntity>();
                    model.UserID = ConvertHelper.ConvertLong(sqlReader["UserID"]);
                    model.OUID = ConvertHelper.ConvertLong(sqlReader["OUID"]);
                    model.Password = sqlReader["Password"].ToString();
                    model.UserType = ConvertHelper.ConvertInt(sqlReader["UserType"]);
                    model.Status = ConvertHelper.ConvertInt(sqlReader["Status"]);
                }
            }

            userEntity.Add(model);

            return userEntity;
        }
        #endregion
    }
}
