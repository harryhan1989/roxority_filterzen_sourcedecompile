using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DBAccess;
using Business.Helper;
using System.Data.SqlClient;
using System.Data;
using BusinessLayer.InitSqlRight;

namespace BusinessLayer.CreateItem
{
    public class CreateItem_Layer
    {
        string prefix = ConfigurationManager.AppSettings["DbTitle"].ToString();
        string initRightSql = "";
        public SqlDataReader GetLanTable()
        {
            string TabelName = " " + prefix + "LanTable ";
            StringBuilder sql = new StringBuilder("SELECT ID,JS,CSharp FROM" + TabelName, 50);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql));
        }

        public int CreateItemText_UpdateItemTable(string ItemName, string DataFormatCheck, string ItemContent, string PageNo, string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,DataFormatCheck=@DataFormatCheck,ItemContent=@ItemContent,PageNo=@PageNo WHERE SID=@SID AND UID=@UID AND IID=@IID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,DataFormatCheck=@DataFormatCheck,ItemContent=@ItemContent,PageNo=@PageNo WHERE SID=@SID AND " + initRightSql + " AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@ItemName", ItemName);
            parameters[1] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[2] = new SqlParameter("@ItemContent", ItemContent);
            parameters[3] = new SqlParameter("@PageNo", PageNo);
            parameters[4] = new SqlParameter("@SID", SID);
            parameters[5] = new SqlParameter("@UID", UID);
            parameters[6] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemText_UpdateItemTable1(string ItemHTML, string SID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemHTML=@ItemHTML WHERE SID=@SID AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@ItemHTML", ItemHTML);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemText_UpdateItemTable2(string ItemName, string DataFormatCheck, string OptionAmount, string OptionImgModel, string OrderModel, string ItemContent, string PageNo, string SID, string IID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,DataFormatCheck=@DataFormatCheck,OptionAmount=@OptionAmount,OptionImgModel=@OptionImgModel,OrderModel=@OrderModel,ItemContent=@ItemContent,PageNo=@PageNo WHERE SID=@SID AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[9];
            parameters[0] = new SqlParameter("@ItemName", ItemName);
            parameters[1] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[2] = new SqlParameter("@OptionAmount", OptionAmount);
            parameters[3] = new SqlParameter("@OptionImgModel", OptionImgModel);
            parameters[4] = new SqlParameter("@OrderModel", OrderModel);
            parameters[5] = new SqlParameter("@ItemContent", ItemContent);
            parameters[6] = new SqlParameter("@PageNo", PageNo);
            parameters[7] = new SqlParameter("@SID", SID);
            parameters[8] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemText_UpdateItemTable3(string ItemName, string DataFormatCheck, string OptionAmount, string OrderModel, string ItemContent, string PageNo, string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,DataFormatCheck=@DataFormatCheck,OptionAmount=@OptionAmount,OrderModel=@OrderModel,ItemContent=@ItemContent,PageNo=@PageNo WHERE SID=@SID AND UID=@UID AND IID=@IID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,DataFormatCheck=@DataFormatCheck,OptionAmount=@OptionAmount,OrderModel=@OrderModel,ItemContent=@ItemContent,PageNo=@PageNo WHERE SID=@SID AND " + initRightSql + " AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[9];
            parameters[0] = new SqlParameter("@ItemName", ItemName);
            parameters[1] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[2] = new SqlParameter("@OptionAmount", OptionAmount);
            parameters[3] = new SqlParameter("@OrderModel", OrderModel);
            parameters[4] = new SqlParameter("@ItemContent", ItemContent);
            parameters[5] = new SqlParameter("@PageNo", PageNo);
            parameters[6] = new SqlParameter("@SID", SID);
            parameters[7] = new SqlParameter("@UID", UID);
            parameters[8] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemText_UpdateItemTable4(string ItemName, string DataFormatCheck, string OptionAmount, string OrderModel, string ItemContent, string PageNo, string SID, string IID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,DataFormatCheck=@DataFormatCheck,OptionAmount=@OptionAmount,OrderModel=@OrderModel,ItemContent=@ItemContent,PageNo=@PageNo WHERE SID=@SID AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@ItemName", ItemName);
            parameters[1] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[2] = new SqlParameter("@OptionAmount", OptionAmount);
            parameters[3] = new SqlParameter("@OrderModel", OrderModel);
            parameters[4] = new SqlParameter("@ItemContent", ItemContent);
            parameters[5] = new SqlParameter("@PageNo", PageNo);
            parameters[6] = new SqlParameter("@SID", SID);
            parameters[7] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemText_UpdateItemTable5(string ItemName, string OptionAmount, string ItemContent, string PageNo, string OtherProperty, string DataFormatCheck, string OptionImgModel, string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,OptionAmount=@OptionAmount,ItemContent=@ItemContent,PageNo=@PageNo,OtherProperty=@OtherProperty,DataFormatCheck=@DataFormatCheck,OptionImgModel=@OptionImgModel WHERE SID=@SID AND UID=@UID AND IID=@IID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,OptionAmount=@OptionAmount,ItemContent=@ItemContent,PageNo=@PageNo,OtherProperty=@OtherProperty,DataFormatCheck=@DataFormatCheck,OptionImgModel=@OptionImgModel WHERE SID=@SID AND " + initRightSql + " AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[10];
            parameters[0] = new SqlParameter("@ItemName", ItemName);
            parameters[1] = new SqlParameter("@OptionAmount", OptionAmount);
            parameters[2] = new SqlParameter("@ItemContent", ItemContent);
            parameters[3] = new SqlParameter("@PageNo", PageNo);
            parameters[4] = new SqlParameter("@OtherProperty", OtherProperty);
            parameters[5] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[6] = new SqlParameter("@OptionImgModel", OptionImgModel);
            parameters[7] = new SqlParameter("@SID", SID);
            parameters[8] = new SqlParameter("@UID", UID);
            parameters[9] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemText_UpdateItemTable6(string ItemName, string DataFormatCheck, string OptionAmount, string ItemContent, string PageNo, string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,DataFormatCheck=@DataFormatCheck,OptionAmount=@OptionAmount,ItemContent=@ItemContent,PageNo=@PageNo WHERE SID=@SID AND UID=@UID AND IID=@IID", 100);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,DataFormatCheck=@DataFormatCheck,OptionAmount=@OptionAmount,ItemContent=@ItemContent,PageNo=@PageNo WHERE SID=@SID AND "+initRightSql+" AND IID=@IID", 100);
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@ItemName", ItemName);
            parameters[1] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[2] = new SqlParameter("@OptionAmount", OptionAmount);
            parameters[3] = new SqlParameter("@ItemContent", ItemContent);
            parameters[4] = new SqlParameter("@PageNo", PageNo);
            parameters[5] = new SqlParameter("@SID", SID);
            parameters[6] = new SqlParameter("@UID", UID);
            parameters[7] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemText_UpdateItemTable7(string ItemHTML, string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemHTML=@ItemHTML WHERE SID=@SID AND UID=@UID AND IID=@IID", 50);
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemHTML=@ItemHTML WHERE SID=@SID AND "+initRightSql+" AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@ItemHTML", ItemHTML);
            parameters[1] = new SqlParameter("@SID", SID);
            parameters[2] = new SqlParameter("@UID", UID);
            parameters[3] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader CreateItem_Text_HTML_SelectItemTable(string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,ItemContent,DataFormatCheck FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID", 50);
            StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,ItemContent,DataFormatCheck FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetCurrDataFromItemTable(string SID, long ParentID, string UID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 IID FROM" + TabelName + "WHERE SID=@SID AND ParentID=@ParentID AND UID=@UID  ORDER BY IID DESC", 50);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 IID FROM" + TabelName + "WHERE SID=@SID AND ParentID=@ParentID AND " + initRightSql + "  ORDER BY IID DESC", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@ParentID", ParentID);
            parameters[2] = new SqlParameter("@UID", UID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_UpdateItemTable(string ItemName, string DataFormatCheck, string ItemContent, string PageNo, string SID, string IID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("UPDATE " + TabelName + " SET ItemName=@ItemName,DataFormatCheck=@DataFormatCheck,ItemContent=@ItemContent,PageNo=@PageNo  WHERE SID=@SID AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@ItemName", ItemName);
            parameters[1] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[2] = new SqlParameter("@ItemContent", ItemContent);
            parameters[3] = new SqlParameter("@PageNo", PageNo);
            parameters[4] = new SqlParameter("@SID", SID);
            parameters[5] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader CreateItemNumberHTML_SelectItemTable(string SID, string UID, string IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,OptionImgModel,OrderModel,ItemContent FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID", 50);
            StringBuilder sql = new StringBuilder("SELECT ItemName,OptionImgModel,OrderModel,ItemContent FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_InsertItemTable(string SID, string UID, string ItemName, string ItemType, string DataFormatCheck, string ParentID, string ItemContent, string PageNo, string Sort)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,ItemContent,PageNo,Sort) VALUES(@SID,@UID,@ItemName,@ItemType,@DataFormatCheck,@ParentID,@ItemContent,@PageNo,@Sort)", 50);
            SqlParameter[] parameters = new SqlParameter[9];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ItemName", ItemName);
            parameters[3] = new SqlParameter("@ItemType", ItemType);
            parameters[4] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[5] = new SqlParameter("@ParentID", ParentID);
            parameters[6] = new SqlParameter("@ItemContent", ItemContent);
            parameters[7] = new SqlParameter("@PageNo", PageNo);
            parameters[8] = new SqlParameter("@Sort", Sort);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_InsertItemTable1(string SID, string UID, string ItemName, string ItemType, string DataFormatCheck, string ParentID, string OptionAmount, string OptionImgModel, string OrderModel, string ItemContent, string PageNo, string Sort)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OptionImgModel,OrderModel,ItemContent,PageNo,Sort) VALUES(@SID,@UID,@ItemName,@ItemType,@DataFormatCheck,@ParentID,@OptionAmount,@OptionImgModel,@OrderModel,@ItemContent,@PageNo,@Sort)", 50);
            SqlParameter[] parameters = new SqlParameter[12];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ItemName", ItemName);
            parameters[3] = new SqlParameter("@ItemType", ItemType);
            parameters[4] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[5] = new SqlParameter("@ParentID", ParentID);
            parameters[6] = new SqlParameter("@OptionAmount", OptionAmount);
            parameters[7] = new SqlParameter("@OptionImgModel", OptionImgModel);
            parameters[8] = new SqlParameter("@OrderModel", OrderModel);
            parameters[9] = new SqlParameter("@ItemContent", ItemContent);
            parameters[10] = new SqlParameter("@PageNo", PageNo);
            parameters[11] = new SqlParameter("@Sort", Sort);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_InsertItemTable2(string SID, string UID, string ItemName, string ItemType, string ParentID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,ItemName,ItemType,ParentID) VALUES(@SID,@UID,@ItemName,@ItemType,@ParentID)", 50);
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ItemName", ItemName);
            parameters[3] = new SqlParameter("@ItemType", ItemType);
            parameters[4] = new SqlParameter("@ParentID", ParentID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_InsertItemTable3(string SID, string UID, string ItemName, string ItemType, string DataFormatCheck, string ParentID, string OptionAmount, string ItemContent, string PageNo, string Sort, string OtherProperty, string OptionImgModel)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,ItemContent,PageNo,Sort,OtherProperty,OptionImgModel) VALUES(@SID,@UID,@ItemName,@ItemType,@DataFormatCheck,@ParentID,@OptionAmount,@ItemContent,@PageNo,@Sort,@OtherProperty,@OptionImgModel)", 100);
            SqlParameter[] parameters = new SqlParameter[12];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ItemName", ItemName);
            parameters[3] = new SqlParameter("@ItemType", ItemType);
            parameters[4] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[5] = new SqlParameter("@ParentID", ParentID);
            parameters[6] = new SqlParameter("@OptionAmount", OptionAmount);
            parameters[7] = new SqlParameter("@ItemContent", ItemContent);
            parameters[8] = new SqlParameter("@PageNo", PageNo);
            parameters[9] = new SqlParameter("@Sort", Sort);
            parameters[10] = new SqlParameter("@OtherProperty", OtherProperty);
            parameters[11] = new SqlParameter("@OptionImgModel", OptionImgModel);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_InsertItemTable4(string SID, string UID, string ItemName, string ItemType, string DataFormatCheck, string ParentID, string OptionAmount, string ItemContent, string PageNo, string Sort)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,ItemContent,PageNo,Sort) VALUES(@SID,@UID,@ItemName,@ItemType,@DataFormatCheck,@ParentID,@OptionAmount,@ItemContent,@PageNo,@Sort)", 100);
            SqlParameter[] parameters = new SqlParameter[10];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ItemName", ItemName);
            parameters[3] = new SqlParameter("@ItemType", ItemType);
            parameters[4] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[5] = new SqlParameter("@ParentID", ParentID);
            parameters[6] = new SqlParameter("@OptionAmount", OptionAmount);
            parameters[7] = new SqlParameter("@ItemContent", ItemContent);
            parameters[8] = new SqlParameter("@PageNo", PageNo);
            parameters[9] = new SqlParameter("@Sort", Sort);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_InsertItemTable5(string SID, string UID, string ItemName, string ItemType, string DataFormatCheck, string ItemContent, string PageNo, string Sort)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,ItemName,ItemType,DataFormatCheck,ItemContent,PageNo,Sort) VALUES(@SID,@UID,@ItemName,@ItemType,@DataFormatCheck,@ItemContent,@PageNo,@Sort)", 100);
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ItemName", ItemName);
            parameters[3] = new SqlParameter("@ItemType", ItemType);
            parameters[4] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[5] = new SqlParameter("@ItemContent", ItemContent);
            parameters[6] = new SqlParameter("@PageNo", PageNo);
            parameters[7] = new SqlParameter("@Sort", Sort);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_InsertItemTable6(string SID, string UID, string ItemName, string ItemType, string DataFormatCheck, string ParentID, string OptionAmount, string OrderModel, string ItemContent, string PageNo, string Sort)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,ItemName,ItemType,DataFormatCheck,ParentID,OptionAmount,OrderModel,ItemContent,PageNo,Sort) VALUES(@SID,@UID,@ItemName,@ItemType,@DataFormatCheck,@ParentID,@OptionAmount,@OrderModel,@ItemContent,@PageNo,@Sort)", 100);
            SqlParameter[] parameters = new SqlParameter[11];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ItemName", ItemName);
            parameters[3] = new SqlParameter("@ItemType", ItemType);
            parameters[4] = new SqlParameter("@DataFormatCheck", DataFormatCheck);
            parameters[5] = new SqlParameter("@ParentID", ParentID);
            parameters[6] = new SqlParameter("@OptionAmount", OptionAmount);
            parameters[7] = new SqlParameter("@OrderModel", OrderModel);
            parameters[8] = new SqlParameter("@ItemContent", ItemContent);
            parameters[9] = new SqlParameter("@PageNo", PageNo);
            parameters[10] = new SqlParameter("@Sort", Sort);


            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_InsertItemTable7(string SID, string UID, string ItemType, string ParentID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,ItemType,ParentID) VALUES(@SID,@UID,@ItemType,@ParentID)", 100);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@ItemType", ItemType);
            parameters[3] = new SqlParameter("@ParentID", ParentID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_InsertOptionTable(string SID, string IID, string UID, string OptionName)
        {
            string TabelName = " " + prefix + "OptionTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,IID,UID,OptionName) VALUES(@SID,@IID,@UID,@OptionName)", 50);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@IID", IID);
            parameters[2] = new SqlParameter("@UID", UID);
            parameters[3] = new SqlParameter("@OptionName", OptionName);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int CreateItemNumber_InsertOptionTable1(string SID, string IID, string UID, string OptionName, string IsMatrixRowColumn)
        {
            string TabelName = " " + prefix + "OptionTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,IID,UID,OptionName,IsMatrixRowColumn) VALUES(@SID,@IID,@UID,@OptionName,@IsMatrixRowColumn)", 50);
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@IID", IID);
            parameters[2] = new SqlParameter("@UID", UID);
            parameters[3] = new SqlParameter("@OptionName", OptionName);
            parameters[4] = new SqlParameter("@OptionName", OptionName);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int InsertPageTable(string SID, string UID, string PageContent, string PageNo)
        {
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,PageContent,PageNo) VALUES(@SID,@UID,@PageContent,@PageNo)", 50);
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PageContent", PageContent);
            parameters[3] = new SqlParameter("@PageNo", PageNo);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int InsertPageTable1(string SID, string UID, string PageNo)
        {
            string TabelName = " " + prefix + "PageTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SID,UID,PageNo) VALUES(@SID,@UID,@PageNo)", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@PageNo", PageNo);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int InsertHeadFoot(string PageHead, string UID, string SID)
        {
            string TabelName = " " + prefix + "HeadFoot ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(PageHead,UID,SID) VALUES(@PageHead,@UID,@SID)", 50);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@PageHead", PageHead);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader InsertSurveyTable(string SurveyName, string CreateDate, string UID, string ClassID, string Par)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SurveyName,CreateDate,UID,ClassID,Par) VALUES(@SurveyName,@CreateDate,@UID,@ClassID,@Par)", 50);
            sql.Append("SELECT   IDENT_CURRENT('SurveyTable')   AS CurrSID");
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@SurveyName", SurveyName);
            parameters[1] = new SqlParameter("@CreateDate", CreateDate);
            parameters[2] = new SqlParameter("@UID", UID);
            parameters[3] = new SqlParameter("@ClassID", ClassID);
            parameters[4] = new SqlParameter("@Par", Par);

            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public int InsertSurveyTable1(string SurveyName, string CreateDate, string UID, string ClassID, string Par)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SurveyName,CreateDate,UID,ClassID,Par) VALUES(@SurveyName,@CreateDate,@UID,@ClassID,@Par)", 50);
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@SurveyName", SurveyName);
            parameters[1] = new SqlParameter("@CreateDate", CreateDate);
            parameters[2] = new SqlParameter("@UID", UID);
            parameters[3] = new SqlParameter("@ClassID", ClassID);
            parameters[4] = new SqlParameter("@Par", Par);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader InsertSurveyTable2(string SurveyName, string CreateDate, string UID, string ClassID, string Par, string TempPage)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("INSERT INTO" + TabelName + "(SurveyName,CreateDate,UID,ClassID,Par,TempPage) VALUES(@SurveyName,@CreateDate,@UID,@ClassID,@Par,@TempPage)", 50);
            sql.Append("SELECT   IDENT_CURRENT('SurveyTable')   AS CurrSID");
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@SurveyName", SurveyName);
            parameters[1] = new SqlParameter("@CreateDate", CreateDate);
            parameters[2] = new SqlParameter("@UID", UID);
            parameters[3] = new SqlParameter("@ClassID", ClassID);
            parameters[4] = new SqlParameter("@Par", Par);
            parameters[5] = new SqlParameter("@TempPage", TempPage);

            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public int DeleteItemTableByParentID(string ParentID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + "WHERE ParentID=@ParentID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ParentID", ParentID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public int DeleteOptionTableByIID(string IID)
        {
            string TabelName = " " + prefix + "OptionTable ";
            StringBuilder sql = new StringBuilder("DELETE FROM " + TabelName + "WHERE IID=@IID", 50);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@IID", IID);

            return DbHelperSQL.ExecuteSql(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetItemTable(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,OptionImgModel,OrderModel,ItemContent FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,OptionImgModel,OrderModel,ItemContent FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemTable1(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,ItemContent FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,ItemContent FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemTable2(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ItemName,OrderModel,ItemContent,StyleMode FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ItemName,OrderModel,ItemContent,StyleMode FROM " + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemTable5(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,ItemContent,OptionAmount,OtherProperty,OptionImgModel from" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,ItemContent,OptionAmount,OtherProperty,OptionImgModel from" + TabelName + "WHERE SID=@SID AND "+initRightSql+" AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemTable3(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,ItemContent,OptionAmount FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT ItemName,ItemContent,OptionAmount FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetItemTable4(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT IID,ItemName FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT IID,ItemName FROM " + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemTable6(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,OrderModel,ItemContent FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT ItemName,OrderModel,ItemContent FROM " + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetItemTable7(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,ItemContent,OptionAmount FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT ItemName,ItemContent,OptionAmount FROM" + TabelName + "WHERE SID=@SID AND "+initRightSql+" AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetItemTable8(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,OrderModel,ItemContent FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT ItemName,OrderModel,ItemContent FROM " + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetItemTable9(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,OrderModel,ItemContent FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,OrderModel,ItemContent FROM " + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetItemTable10(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ItemName,OrderModel,ItemContent,StyleMode FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ItemName,OrderModel,ItemContent,StyleMode FROM " + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemTable11(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,ItemContent,DataFormatCheck FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT ItemName,ItemType,ItemContent,DataFormatCheck FROM " + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemTable12(string IID, string UID, string SID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "ItemTable ";
            //StringBuilder sql = new StringBuilder("SELECT TOP 1 ItemType  FROM " + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID", 200);
            StringBuilder sql = new StringBuilder("SELECT TOP 1 ItemType  FROM " + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@IID", IID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetItemTable13(string SID, string PageNo, string ParentID)
        {
            string TabelName = " " + prefix + "ItemTable ";
            StringBuilder sql = new StringBuilder("SELECT MAX(Sort)  FROM " + TabelName + "WHERE SID=@SID AND PageNo=@PageNo AND ParentID=@ParentID ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@PageNo", PageNo);
            parameters[2] = new SqlParameter("@ParentID", ParentID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetOptionTable(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("SELECT OID,OptionName FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT OID,OptionName FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetOptionTable1(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("SELECT OID,OptionName,IsMatrixRowColumn FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT OID,OptionName,IsMatrixRowColumn FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public DataTable GetOptionTable2(string SID, string UID, long IID)
        {
            initRightSql = new InitRight().initSurveyRight("UID=@UID ", SID, " ");
            string TabelName = " " + prefix + "OptionTable ";
            //StringBuilder sql = new StringBuilder("SELECT OID,OptionName,Image FROM" + TabelName + "WHERE SID=@SID AND UID=@UID AND IID=@IID    ", 200);
            StringBuilder sql = new StringBuilder("SELECT OID,OptionName,Image FROM" + TabelName + "WHERE SID=@SID AND " + initRightSql + " AND IID=@IID    ", 200);
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@SID", SID);
            parameters[1] = new SqlParameter("@UID", UID);
            parameters[2] = new SqlParameter("@IID", IID);
            return DbHelperSQL.Fill(ConvertHelper.ConvertString(sql), parameters);
        }

        public SqlDataReader GetSurveyTable()
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT MAX(SID) FROM" + TabelName, 200);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql));
        }

        public SqlDataReader GetSurveyTable1(string SID)
        {
            string TabelName = " " + prefix + "SurveyTable ";
            StringBuilder sql = new StringBuilder("SELECT TOP 1 Lan FROM" + TabelName + "WHERE SID=@SID ", 200);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@SID", SID);
            return DbHelperSQL.ExecuteReader(ConvertHelper.ConvertString(sql), parameters);
        }
    }
}
