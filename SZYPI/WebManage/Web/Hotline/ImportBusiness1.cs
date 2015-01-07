using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Business.Helper;
using DBAccess.Rule;
using Business.Const;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;

namespace Business.Import
{
    /// <summary>
    /// 文件导入业务操作类
    /// </summary>
    public class ImportBusiness1
    {
        /// <summary>
        /// XLS文件导入到数据库中
        /// </summary>
        /// <param name="fu">文件流</param>
        /// <param name="tableName"></param>
        /// <param name="msgList"></param>
        /// <param name="errMsg"></param>
        public bool ImportXls2Db(FileUpload fu, string tableName, ref string msgList, ref string errMsg)
        {
            //1.将文件上传到服务器
            string filePath = string.Empty;
            if (Trans2ServerPath(fu, ref filePath, ref errMsg))
            {
                //2.分析服务器端的文件是否符合规范，如扩展名、文件大小、是否已经被打开等等。
                if (!CheckFileValid(fu, ref errMsg))
                {
                    return false;
                }
                //3.将文件导入内存中的DT中
                DataSet ds = GetExcel(filePath, fu, ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    errMsg = (string.Format("DialogboxShow(\"{0}\")", errMsg));
                    return false;
                }
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dataTable = ds.Tables[0];
                    int i = dataTable.Rows.Count * dataTable.Columns.Count;
                    //4.在内存中对数据做Check
                    var errInfos = new List<ErrInfo>(i);
                    var rule = new ImportItemsRule();
                    //不需要导入部分是否包含该字段
                    DataTable inImportFileButNotImportDT = rule.GetImportItemsDTByColType(tableName, ((int)PageConst.ImportItemTypeEnum.InImportFileButNotImport).ToString());
                    //从导入文件中获取数据的字段列表
                    DataTable fromImportItemsDT = rule.GetImportItemsDTByColType(tableName, ((int)PageConst.ImportItemTypeEnum.FromImportFile).ToString());
                    //从默认值中获取数据的字段列表
                    DataTable fromDefaulValueItemsDT = rule.GetImportItemsDTByColType(tableName, ((int)PageConst.ImportItemTypeEnum.FromDefaultValue).ToString());
                    bool s = CheckDataValid(ds, tableName, ref errInfos, ref  errMsg, ref fromImportItemsDT, ref fromDefaulValueItemsDT, ref inImportFileButNotImportDT);
                    if (s)
                    {
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            errMsg = (string.Format("DialogboxShow(\"{0}\")", errMsg));
                            return false;
                        }
                        s = ImportDate(tableName, dataTable, fromImportItemsDT, fromDefaulValueItemsDT, ref errInfos);
                    }
                    if (errInfos.Count > 0)
                    {
                        var scriptSerializer = new JavaScriptSerializer();
                        msgList = scriptSerializer.Serialize(errInfos);
                    }
                    return s;

                }

            }
            else
            {
                if (!string.IsNullOrEmpty(errMsg))
                {
                    errMsg = (string.Format("DialogboxShow(\"{0}\")", errMsg));
                    return false;
                }
            }
            return false;

        }
        /// <summary>
        ///  将路径转换为服务器端路径并且保存上传文件
        /// </summary>
        /// <param name="fu"></param>
        /// <param name="filePath"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Trans2ServerPath(FileUpload fu, ref string filePath, ref string msg)
        {
            if (fu.PostedFile != null)
                if (fu.PostedFile.ContentLength > 4096000)
                {
                    msg = "上传文件超过4M！";
                }
                else if (fu.PostedFile.ContentLength < 1)
                {
                    msg = "请选择上传文件！";
                }
                else
                {
                    filePath = fu.PostedFile.FileName.ToLower().Trim();

                    //取得文件名
                    string name = Path.GetFileName(filePath);// +DateTime.Now.ToString("yyyyMMddhhmmss");
                    string serverPath = HttpContext.Current.Server.MapPath(@"..\..\ND\Import\"); //获得服务器端的根目录

                    //判断是否有该目录
                    if (!Directory.Exists(string.Format("{0}UploadFile", serverPath)))
                    {
                        Directory.CreateDirectory(string.Format("{0}UploadFile", serverPath));
                    }
                    serverPath = string.Format("{0}UploadFile", serverPath);
                    filePath = string.Format("{0}\\{1}", serverPath, name);
                    //如果存在,删除文件
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    // 上传文件
                    fu.PostedFile.SaveAs(filePath);
                    return true;
                }
            else
            {
                msg = "请选择上传文件！";
            }
            return false;
        }

        /// <summary>
        /// 检测文件是否符合规范
        /// </summary>
        /// <param name="fu"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected static bool CheckFileValid(FileUpload fu, ref string errMsg)
        {
            //得到文件的大小
            int fileSize = fu.PostedFile.ContentLength;
            //得到扩展名
            string fileExtend = Path.GetExtension(fu.PostedFile.FileName).ToLower();
            if (fileSize == 0)
            {
                errMsg = ("DialogboxShow('找不到该文件！')");
                return false;
            }
            if (!Equals(fileExtend, ".xls") && !Equals(fileExtend, ".xlsx") && !Equals(fileExtend, ".csv"))
            {
                errMsg = ("DialogboxShow('请确认您所导入的文件是否为Excel或Csv文件！')");
                return false;
            }
            if (!fu.PostedFile.ContentType.ToLower().Contains("application/vnd.ms-excel") && fu.PostedFile.ContentType.ToLower().Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
            {
                errMsg = ("DialogboxShow('请确认您所导入的文件是否为Excel或Csv文件！')");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 根据路径获取EXCEL的数据源，将其加载到内存中
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fu"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DataSet GetExcel(string path, FileUpload fu, ref string msg)
        {
            var data = new DataSet();
            try
            {
                if (path.ToLower().EndsWith(".xls") || (path.ToLower().EndsWith(".xlsx")))
                {
                    string strConn = string.Empty;
                    if (path.ToLower().EndsWith(".xls"))
                    {
                        //Excel 2000/2003 格式
                        strConn = string.Format("Provider=Microsoft.Jet.OleDb.4.0;data source={0};Extended Properties='Excel 8.0'", path);
                    }
                    else if (path.ToLower().EndsWith(".xlsx"))
                    {
                        //Excel 2007格式
                        strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;data source={0};Extended Properties='Excel 12.0;HDR=YES'", path);
                    }
                    using (var objConn = new OleDbConnection(strConn))
                    {
                        objConn.Open();
                        DataTable schemaTable = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (schemaTable != null && schemaTable.Rows.Count > 0 && schemaTable.Columns.Count > 1)
                        {
                            string tableName = schemaTable.Rows[0][2].ToString().Trim();
                            string strSql = string.Format("Select   *   From   [Sheet1$A3:K]", tableName);
                            var objCmd = new OleDbCommand(strSql, objConn);
                            var sqlada = new OleDbDataAdapter { SelectCommand = objCmd };
                            sqlada.Fill(data);
                            objConn.Close();
                        }
                    }
                }
                else if (path.ToLower().EndsWith(".csv"))
                {
                    var csvFile = new CSVFile();
                    csvFile.Open(fu.PostedFile.InputStream);

                    int i = 0;
                    var dt = new DataTable();
                    while (csvFile.ReadNextLine())
                    {
                        if (i == 0)
                        {
                            for (int j = 0; j < csvFile.Cells.Count; j++)
                            {
                                if (!string.IsNullOrEmpty(csvFile.Cells[j]))
                                {
                                    dt.Columns.Add(csvFile.Cells[j]);
                                }
                            }
                        }
                        else
                        {
                            DataRow dr = dt.NewRow();

                            int j = 0;
                            foreach (DataColumn dc in dt.Columns)
                            {
                                if (csvFile.Cells.Count > j && csvFile.Cells[j] != null && !string.IsNullOrEmpty(csvFile.Cells[j]))
                                {
                                    dr[dc.ColumnName] = csvFile.Cells[j];
                                }
                                j++;
                            }

                            dt.Rows.Add(dr);
                        }

                        i++;
                    }

                    data.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return data;
        }

        /// <summary>
        /// 检测数据合法性
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <param name="errInfos"></param>
        /// <param name="errmessage">错误记录</param>
        /// <param name="fromImportItemsDT"></param>
        /// <param name="fromDefaulValueItemsDT"></param>
        /// <param name="inImportFileButNotImportDT"></param>
        /// <returns></returns>
        virtual public bool CheckDataValid(DataSet ds, string tableName, ref List<ErrInfo> errInfos, ref  string errmessage, ref  DataTable fromImportItemsDT, ref DataTable fromDefaulValueItemsDT, ref DataTable inImportFileButNotImportDT)
        {
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                int rowNo = 1;

                #region 检测导入文件中字段是否为有效字段（字段名是否存在）
                foreach (DataColumn dc in dt.Columns)
                {
                    bool hasExist = false;          //是否存在该列字段

                    //Import需要导入部分是否包含该字段
                    foreach (DataRow dr in fromImportItemsDT.Rows)
                    {
                        if (dc.ColumnName.ToLower().Trim().Equals(dr["CName"].ToString().ToLower().Trim()))
                        {
                            hasExist = true;
                            break;
                        }
                    }
                    if (hasExist)
                        continue;
                    //Import不需要导入部分是否包含该字段
                    foreach (DataRow dr in inImportFileButNotImportDT.Rows)
                    {
                        if (dc.ColumnName.ToLower().Trim().Equals(dr["CName"].ToString().ToLower().Trim()))
                        {
                            hasExist = true;
                            break;
                        }
                    }
                    if (hasExist)
                        continue;

                    //默认值中是否包含该字段
                    foreach (DataRow dr in fromDefaulValueItemsDT.Rows)
                    {
                        if (dc.ColumnName.ToLower().Trim().Equals(dr["CName"].ToString().ToLower().Trim()))
                        {
                            hasExist = true;
                            break;
                        }
                    }
                    //该字段不存在，不能导入
                    if (hasExist == false)
                    {
                        var err = new ErrInfo
                        {
                            ErrRowNo = "所有行",
                            ErrType = PageConst.ImportDataErrorType.ItemNotExist,
                            ErrData = dc.ColumnName
                        };
                        var paras = new List<string> { dc.ColumnName };
                        err.ErrMsg = GetMessage("700003", paras);
                        errInfos.Add(err);
                    }
                }
                #endregion

                #region 字段个数不对应

                //if (fromImportItemsDT != null && fromImportItemsDT.Rows.Count > 0)
                //{
                //    DataRow importTemplateInfoDR = new ImportTemplateRule().GetImportTemplateDr(fromImportItemsDT.Rows[0]["ObjName"].ToString());
                //    if (importTemplateInfoDR != null &&
                //        dt.Columns.Count != ConvertHelper.ConvertInt(importTemplateInfoDR["ItemsCount"]))
                //    {
                //        var err = new ErrInfo
                //                      {
                //                          ErrRowNo = "所有行",
                //                          ErrType = PageConst.ImportDataErrorType.ItemNumNotMatch,
                //                          ErrData = "所有行"
                //                      };
                //        var paras = new List<string> {importTemplateInfoDR["ItemsCount"].ToString()};
                //        err.ErrMsg = GetMessage("700001", paras);
                //    }
                //}

                #endregion

                #region 检测数据的合法性
                bool success = true;    //是否检验通过

                foreach (DataRow dr in dt.Rows)
                {
                    
                    //检测每行数据的合法性

                        if (CheckRowData(dr, tableName, fromImportItemsDT, fromDefaulValueItemsDT, rowNo.ToString(), ref errInfos, ref errmessage))
                        {
                            continue;
                        }
                    rowNo++;
                    success = false;
                }
                #endregion

                return success;
            }
            return false;
        }
        /// <summary>
        /// 如果检测成功，则把数据导入到数据库
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        /// <param name="fromImportItemsDT"></param>
        /// <param name="fromDefaulValueItemsDT"></param>
        /// <param name="errInfos"></param>
        virtual public bool ImportDate(string tableName, DataTable dt, DataTable fromImportItemsDT, DataTable fromDefaulValueItemsDT, ref List<ErrInfo> errInfos)
        {
            string sql = string.Format("insert into {0}", tableName);
            string fields;
            string values;
            List<SqlParameter> paramList;
            int j = 0;
            foreach (DataRow dr in dt.Rows)
            {
                j++;
                fields = "(";
                values = "(";
                paramList = new List<SqlParameter>(dt.Rows.Count);

                //导入文件中的数据
                if (fromImportItemsDT != null && fromImportItemsDT.Rows.Count > 0)
                {
                    foreach (DataRow dr2 in fromImportItemsDT.Rows)
                    {
                        fields += string.Format("{0},", dr2["xColumn"].ToString().Trim());


                        string pDataSource = new ReplaceParaHelper.ImportCsvReplaceParameter().ReplaceSqlValue(
                            dr2["DataSource"].ToString().Replace("\"", "'"), dr);
                        if (!pDataSource.Equals(string.Empty))
                        {
                            DataTable pDataSourceDT = DBAccess.DbHelperSQL.Fill(pDataSource);
                            if (pDataSourceDT.Rows.Count > 0)
                            {
                                string value = pDataSourceDT.Rows[0][0].ToString();
                                paramList.Add(new SqlParameter(string.Format("@{0}", dr2["xColumn"].ToString().Trim()), value));
                                values += string.Format("@{0},", dr2["xColumn"].ToString().Trim());
                            }
                            else
                            {
                                paramList.Add(new SqlParameter(string.Format("@{0}", dr2["xColumn"].ToString().Trim()), DBNull.Value));
                                values += string.Format("@{0},", dr2["xColumn"].ToString().Trim());
                            }
                        }
                        else
                        {
                            paramList.Add(new SqlParameter(string.Format("@{0}", dr2["xColumn"].ToString().Trim()),
                                                           dr[dr2["CName"].ToString()].ToString()));
                            values += string.Format("@{0},", dr2["xColumn"].ToString().Trim());
                        }

                        //paramList.Add(new SqlParameter("@" + dr2["xColumn"].ToString(), dr[dr2["CName"].ToString()].ToString()));
                        //values += "@" + dr2["xColumn"].ToString() + ",";
                    }
                }

                //导入默认值的数据
                if (fromDefaulValueItemsDT.Rows.Count > 0)
                {
                    foreach (DataRow dr2 in fromDefaulValueItemsDT.Rows)
                    {

                        fields += string.Format("{0},", dr2["xColumn"].ToString().Trim());
                        string pDataSource = new ReplaceParaHelper.ImportCsvReplaceParameter().ReplaceSqlValue(dr2["DataSource"].ToString().Replace("\"", "'"), dr);
                        if (!pDataSource.Equals(string.Empty))
                        {
                            DataTable pDataSourceDT = DBAccess.DbHelperSQL.Query(pDataSource).Tables[0];
                            if (pDataSourceDT.Rows.Count > 0)
                            {
                                string value = pDataSourceDT.Rows[0][0].ToString();
                                paramList.Add(new SqlParameter(string.Format("@{0}", dr2["xColumn"].ToString().Trim()), value));
                                values += string.Format("@{0},", dr2["xColumn"].ToString().Trim());

                            }
                            else
                            {

                                paramList.Add(new SqlParameter(string.Format("@{0}", dr2["xColumn"].ToString().Trim()), DBNull.Value));
                                values += string.Format("@{0},", dr2["xColumn"].ToString().Trim());
                            }
                        }
                        else
                        {
                            paramList.Add(new SqlParameter(string.Format("@{0}", dr2["xColumn"].ToString().Trim()), DBNull.Value));
                            values += string.Format("@{0},", dr2["xColumn"].ToString().Trim());
                        }

                    }
                }

                fields = fields.Substring(0, fields.Length - 1);
                values = values.Substring(0, values.Length - 1);
                fields += (")");
                values += (")");
                string insertSql = string.Format("{0}{1} Values {2};SELECT @@identity AS 'id'", sql, fields, values);
                try
                {
                    DbHelper.ExecuteSql(insertSql, paramList);
                }
                catch (Exception exception)
                {
                    var err = new ErrInfo
                    {
                        ErrRowNo = j.ToString(),
                        ErrType = "",
                        ErrData = "",
                        ErrMsg = exception.Message.Replace("'", "")
                    };
                    errInfos.Add(err);
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 检测数据是否合法
        /// </summary>
        /// <param name="csvDataDR"></param>
        /// <param name="tableName"></param>
        /// <param name="fromCsvItemsDT"></param>
        /// <param name="fromDefaulValueItemsDT"></param>
        /// <param name="rowNo"></param>
        /// <param name="listErrInfo"></param>
        /// <param name="msg"></param>zzzz
        /// <returns></returns>
        public static bool CheckRowData(DataRow csvDataDR, string tableName, DataTable fromCsvItemsDT, DataTable fromDefaulValueItemsDT, string rowNo, ref List<ErrInfo> listErrInfo, ref string msg)
        {
            bool retVal = true;
            #region 数据重复Check
            DataTable dt = new ImportItemsRule().GetImportItemsDtbyPk(fromCsvItemsDT.Rows[0]["ObjName"].ToString());

            string sql = string.Format("select * from {0} where ", tableName);

            bool sqlError = false;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ColType"].ToString().Equals(((int)PageConst.ImportItemTypeEnum.FromImportFile).ToString()))
                    {
                        //导入文件中的数据
                        sql += string.Format("{0}='{1}' and ", dr["xColumn"].ToString().Trim(), csvDataDR[dr["CName"].ToString().Trim()]);
                    }
                    else
                    {
                        //默认值数据
                        string pDataSource = new ReplaceParaHelper.ImportCsvReplaceParameter().ReplaceSqlValue(dr["DataSource"].ToString().Replace("\"", "'"), csvDataDR);

                        try
                        {
                            DataTable pDT = DBAccess.DbHelperSQL.QueryBySQL(pDataSource);

                            if (pDT.Rows.Count > 0)
                            {
                                sql += string.Format("{0}='{1}' and ", dr["xColumn"].ToString().Trim(), pDT.Rows[0][0].ToString().Trim());
                            }
                            else
                            {
                                sqlError = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            sqlError = true;
                            msg = ex.Message;
                        }
                    }
                }

                if (sqlError == false)
                {
                    dt = DBAccess.DbHelperSQL.QueryBySQL(sql.Trim().Substring(0, sql.Length - 4));

                    if (dt.Rows.Count > 0)
                    {
                        var paras = new List<string>(1);
                        //主键重复
                        var er = new ErrInfo
                        {
                            ErrRowNo = rowNo,
                            ErrType = PageConst.ImportDataErrorType.DataHasExist,
                            ErrMsg = GetMessage("700002", paras),
                            ErrData = GetImportFileRowData(csvDataDR)
                        };

                        listErrInfo.Add(er);
                        return false;
                    }
                }
                else
                {
                    //SQL执行错误
                    var paras = new List<string>(1);
                    var er = new ErrInfo
                    {
                        ErrRowNo = rowNo,
                        ErrType = PageConst.ImportDataErrorType.DataIsValid,
                        ErrMsg = GetMessage("700010", paras),
                        ErrData = GetImportFileRowData(csvDataDR)
                    };
                    listErrInfo.Add(er);
                    retVal = false;
                }
            }

            #endregion

            //检测导入文件中的字段是否符合规范
            foreach (DataRow dr in fromCsvItemsDT.Rows)
            {
                #region 模板文件中不存在该列
                if (!CheckItemExist(dr["CName"].ToString(), csvDataDR))
                {
                    var paras = new List<string>(1) { dr["CName"].ToString() };

                    var er = new ErrInfo
                    {
                        ErrRowNo = rowNo,
                        ErrType = PageConst.ImportDataErrorType.ItemNotExist,
                        ErrMsg = GetMessage("700011", paras),
                        ErrData = dr["CName"].ToString()
                    };
                    listErrInfo.Add(er);
                    return false;
                }
                #endregion

                #region 找不到符合条件的数据，一般出现在数据替换的时候
                if (!string.IsNullOrEmpty(dr["DataSource"].ToString()))
                {
                    string pDataSource = new ReplaceParaHelper.ImportCsvReplaceParameter().ReplaceSqlValue(dr["DataSource"].ToString().Replace("\"", "'"), csvDataDR);

                    DataTable dt2 = DBAccess.DbHelperSQL.QueryBySQL(pDataSource);

                    if (dt2.Rows.Count <= 0)
                    {
                        var paras = new List<string>(2)
                                        {
                                            csvDataDR[dr["CName"].ToString()].ToString(),
                                            dr["CName"].ToString()
                                        };

                        var er = new ErrInfo
                        {
                            ErrRowNo = rowNo,
                            ErrType = PageConst.ImportDataErrorType.KeyDataNotExist,
                            ErrMsg = GetMessage("700012", paras),
                            ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                        };
                        listErrInfo.Add(er);
                    }
                }
                #endregion

                #region 检测数据类型是否正确
                if (dr["DataType"].ToString().ToLower().StartsWith("varchar") ||
                    dr["DataType"].ToString().ToLower().StartsWith("char") ||
                    dr["DataType"].ToString().ToLower().StartsWith("nvarchar"))
                {
                    //获取长度
                    int left = dr["DataType"].ToString().ToLower().IndexOf('(') + 1;
                    int right = dr["DataType"].ToString().ToLower().IndexOf(')');
                    int length = Convert.ToInt32(dr["DataType"].ToString().ToLower().Substring(left, right - left));

                    #region Check字符窜长度是否超出范围
                    if (PageHelper.CheckCharIsLong(csvDataDR[dr["CName"].ToString()].ToString(), length))
                    {
                        var paras = new List<string>(2) { dr["CName"].ToString(), length.ToString() };
                        int halfLength = length / 2;
                        paras.Add(halfLength.ToString());

                        var er = new ErrInfo
                        {
                            ErrRowNo = rowNo,
                            ErrType = PageConst.ImportDataErrorType.DataIsTooLarge,
                            ErrMsg = GetMessage("700004", paras),
                            ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                        };
                        listErrInfo.Add(er);
                        retVal = false;
                    }
                    #endregion

                }
                else if (dr["DataType"].ToString().ToLower().StartsWith("int"))
                {
                    #region Check整数
                    if (!PageHelper.CheckIsInteger(csvDataDR[dr["CName"].ToString()].ToString()))
                    {
                        //不是整数时
                        var paras = new List<string>(1) { dr["CName"].ToString() };

                        var er = new ErrInfo
                        {
                            ErrRowNo = rowNo,
                            ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                            ErrMsg = GetMessage("700005", paras),
                            ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                        };
                        listErrInfo.Add(er);
                        retVal = false;
                    }
                    #endregion
                }
                else if (dr["DataType"].ToString().ToLower().StartsWith("decimal"))
                {
                    //Check高精度浮点数
                    string subStr = dr["DataType"].ToString().ToLower().Substring(dr["DataType"].ToString().ToLower().IndexOf("(") + 1).TrimEnd(')');
                    string[] strs = subStr.Split(',');
                    int decimalPart = Convert.ToInt32(strs[1]);
                    int integerPart = Convert.ToInt32(strs[0]) - decimalPart;

                    #region Check是否为Decimal类型
                    if (!PageHelper.CheckIsDecimal(csvDataDR[dr["CName"].ToString()].ToString()))
                    {
                        //不是高精度浮点数类型
                        var paras = new List<string>(1) { dr["CName"].ToString() };

                        var er = new ErrInfo
                        {
                            ErrRowNo = rowNo,
                            ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                            ErrMsg = GetMessage("700007", paras),
                            ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                        };
                        listErrInfo.Add(er);
                        retVal = false;
                    }
                    #endregion
                    #region Check高精度型的位数是否正确
                    else
                    {
                        //是高精度浮点数，但超出范围
                        if (PageHelper.CheckDecimalIsBigger(csvDataDR[dr["CName"].ToString()].ToString(), integerPart, decimalPart))
                        {
                            //不是高精度浮点数类型
                            var paras = new List<string>(3)
                                            {
                                                dr["CName"].ToString(),
                                                integerPart.ToString(),
                                                decimalPart.ToString()
                                            };

                            var er = new ErrInfo
                            {
                                ErrRowNo = rowNo,
                                ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                ErrMsg = GetMessage("700008", paras),
                                ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                            };
                            listErrInfo.Add(er);
                            retVal = false;
                        }
                    }
                    #endregion
                }
                else if (dr["DataType"].ToString().ToLower().StartsWith("datetime"))
                {
                    #region Check日期格式
                    if (!PageHelper.CheckIsDateTime(csvDataDR[dr["CName"].ToString()].ToString()))
                    {
                        var paras = new List<string>(1) { dr["CName"].ToString() };

                        var er = new ErrInfo
                        {
                            ErrRowNo = rowNo,
                            ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                            ErrMsg = GetMessage("700009", paras),
                            ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                        };
                        listErrInfo.Add(er);
                        retVal = false;
                    }
                    #endregion
                }
                #endregion

                #region 检测是否符合某些验证规范
                if (!String.IsNullOrEmpty(dr["CheckMethod"].ToString()))
                {
                    string[] methods = dr["CheckMethod"].ToString().Split(';');
                    bool hasChecked = false;

                    foreach (string method in methods)
                    {
                        if (hasChecked)
                            break;

                        switch (method)
                        {
                            #region 检测是否不为空
                            case "CheckIsNotEmpty":
                                if (!PageHelper.CheckIsNotEmpty(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700014", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为整数
                            case "CheckIsInteger":
                                if (!PageHelper.CheckIsInteger(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700005", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为数字类型
                            case "CheckIsDecimal":
                                if (!PageHelper.CheckIsDecimal(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700007", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为字符类型
                            case "CheckIsChar":
                                if (!PageHelper.CheckIsChar(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700015", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为数字字母类型
                            case "CheckIsNumChar":
                                if (!PageHelper.CheckIsNumChar(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700016", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为邮件类型
                            case "CheckIsEmail":
                                if (!PageHelper.CheckIsEmail(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700017", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为日期类型
                            case "CheckIsDate":
                                if (!PageHelper.CheckIsDateTime(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700009", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为有效的身份证号码
                            case "CheckIsIdCardNo":
                                if (!PageHelper.CheckIsIdCardNo(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700018", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为正数
                            case "ChechIsSignless":
                                if (!PageHelper.ChechIsSignless(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700019", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为有效的手机号码
                            case "CheckIsMobile":
                                if (!PageHelper.CheckIsMobile(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700020", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为正整数类型
                            case "CheckIsSignlessInteger":
                                if (!PageHelper.CheckIsSignlessInteger(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700021", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为中文类型
                            case "isChinese":
                                if (!PageHelper.IsChinese(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700023", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                            #region 检测是否为非负数
                            case "CheckIsNonnegativeInteger":
                                if (!PageHelper.CheckIsNonnegativeInteger(csvDataDR[dr["CName"].ToString()].ToString()))
                                {
                                    var paras = new List<string>(1) { dr["CName"].ToString() };

                                    var er = new ErrInfo
                                    {
                                        ErrRowNo = rowNo,
                                        ErrType = PageConst.ImportDataErrorType.DataTypeNotMatch,
                                        ErrMsg = GetMessage("700022", paras),
                                        ErrData = csvDataDR[dr["CName"].ToString()].ToString()
                                    };
                                    listErrInfo.Add(er);
                                    retVal = false;
                                    hasChecked = true;
                                }
                                break;
                            #endregion
                        }
                    }
                }
                #endregion
            }
            return retVal;
        }

        /// <summary>
        /// 消息替换参数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static string GetMessage(string code, List<string> paras)
        {
            string sql = string.Format("select CName from ND_Message where ID ={0}", code);
            string msg = DbHelper.GetSingleByExecuteScalar(sql);
            //有参数时，需要进行参数替换
            if (paras.Count > 0)
            {
                string[] para = paras.ToArray();
                if (!string.IsNullOrEmpty(msg))
                {
                    msg = string.Format(msg, para);
                }
            }

            return msg;
        }

        /// <summary>
        /// 将DR行数据整个转换为字符窜
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static string GetImportFileRowData(DataRow dr)
        {
            string retVal = string.Empty;

            for (int i = 0; i < dr.ItemArray.Length; i++)
            {
                retVal += string.Format("{0},", dr[i]);
            }

            return retVal.TrimEnd(',');
        }

        /// <summary>
        /// CSV文件中是否存在该列
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static bool CheckItemExist(string name, DataRow dr)
        {
            DataTable dt = dr.Table;
            bool hasColumn = false;

            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName.ToLower().Trim().Equals(name.ToLower().Trim()))
                {
                    hasColumn = true;
                    break;
                }
            }

            return hasColumn;
        }

    }

    /// <summary>
    /// 错误消息类
    /// </summary>
    public class ErrInfo1
    {
        /// <summary>
        /// 错误数据的行号
        /// </summary>
        public string ErrRowNo { get; set; }

        /// <summary>
        /// 错误类型
        /// </summary>
        public string ErrType { get; set; }

        /// <summary>
        /// 错误提示消息
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 错误数据
        /// </summary>
        public string ErrData { get; set; }
    }
}