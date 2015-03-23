using System;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using System.ServiceModel.Web;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;


namespace roxority.SharePoint
{
    public class ConvertHelper
    {
        #region 类型转换
        public static Boolean ConvertBoolean(string value)
        {
            if (value == null || string.IsNullOrEmpty(value.Trim()) || Equals(value.Trim().ToLower(), "system.object"))
                return false;
            if (Equals(value.Trim(), "1"))
                return true;
            if (Equals(value.Trim().ToLower(), "true"))
                return true;
            return false;
        }
        public static Boolean ConvertBoolean(int value)
        {
            if (value == 1)
                return true;
            return false;
        }

        public static Boolean ConvertBoolean(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString().Trim()))
                return false;
            if (Equals(value.ToString().Trim(), "1"))
                return true;
            if (Equals(value.ToString().Trim().ToLower(), "true"))
                return true;
            return false;
        }

        public static string ConvertString(object obj)
        {
            if (obj == null || Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return string.Empty;
            return obj.ToString().Trim();
        }

        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ConvertInt(object obj)
        {
            if (obj == null)
                return 0;
            if (Equals(obj, DBNull.Value))
                return 0;
            if (Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return 0;
            if (Equals(obj.ToString().Trim(), string.Empty))
                return 0;
            int i;
            if (Int32.TryParse(obj.ToString(), out i))
            {
                return i;
            }
            return 0;
        }
        public static int ConvertInt(string obj)
        {
            if (string.IsNullOrEmpty(obj) || Equals(obj.Trim(), string.Empty))
                return 0;
            if (Equals(obj, DBNull.Value))
                return 0;
            if (string.IsNullOrEmpty(obj.Trim()))
                return 0;
            if (Equals(obj.ToLower().Trim(), "system.object"))
                return 0;
            int i;
            if (Int32.TryParse(obj.Trim(), out i))
            {
                return i;
            }
            return 0;
        }
        public static int ConvertInt(float obj)
        {
            return Convert.ToInt32(obj);
        }

        public static int ConvertInt(double obj)
        {
            return Convert.ToInt32(obj);
        }

        public static int ConvertInt(UInt32 obj)
        {
            return Convert.ToInt32(obj);
        }
        public static int ConvertInt(long obj)
        {
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ConvertLong(object obj)
        {
            if (obj == null)
                return 0;
            if (Equals(obj.ToString(), string.Empty))
                return 0;
            if (Equals(obj, DBNull.Value))
                return 0;
            if (Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return 0;
            long i;
            if (Int64.TryParse(obj.ToString(), out i))
            {
                return i;
            }
            return 0;
        }
        public static long ConvertLong(string obj)
        {
            if (obj == null)
                return 0;
            if (Equals(obj.Trim(), string.Empty))
                return 0;
            if (Equals(obj, DBNull.Value))
                return 0;
            if (Equals(obj.ToLower().Trim(), "system.object"))
                return 0;
            long i;
            if (Int64.TryParse(obj, out i))
            {
                return i;
            }
            return 0;
        }
        public static long ConvertLong(int obj)
        {
            return Convert.ToInt64(obj);
        }

        public static long ConvertLong(float obj)
        {
            return Convert.ToInt64(obj);
        }
        public static long ConvertLong(double obj)
        {
            return Convert.ToInt64(obj);
        }
        public static long ConvertLong(UInt32 obj)
        {
            return Convert.ToInt64(obj);
        }
        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float ConvertFloat(object obj)
        {
            if (obj == null)
                return 0;
            if (Equals(obj.ToString(), string.Empty))
                return 0;
            if (Equals(obj, DBNull.Value))
                return 0;
            if (Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return 0;
            float i;
            if (float.TryParse(obj.ToString(), out i))
            {
                return i;
            }
            return 0;
        }
        public static float ConvertFloat(string obj)
        {
            if (obj == null)
                return 0;
            if (Equals(obj.Trim(), string.Empty))
                return 0;
            if (Equals(obj, DBNull.Value))
                return 0;
            if (Equals(obj.ToLower().Trim(), "system.object"))
                return 0;
            float i;
            if (float.TryParse(obj, out i))
            {
                return i;
            }
            return 0;
        }
        public static float ConvertFloat(int obj)
        {
            return Convert.ToInt64(obj);
        }

        public static float ConvertFloat(double obj)
        {
            return Convert.ToInt64(obj);
        }
        public static float ConvertFloat(UInt32 obj)
        {
            return Convert.ToInt64(obj);
        }
        public static long ConvertFloat(long obj)
        {
            return Convert.ToInt64(obj);
        }
        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ConvertDecimal(object obj)
        {
            if (obj == null)
                return 0;
            if (obj == DBNull.Value)
                return 0;
            if (obj.ToString().ToLower().Trim() == "system.object")
                return 0;
            if (string.IsNullOrEmpty(obj.ToString().Trim()))
                return 0;
            decimal i;
            if (decimal.TryParse(obj.ToString(), out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        /// 判断是否是正数字字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ConvertDouble(object obj)
        {
            if (obj == null)
                return 0;
            if (obj == DBNull.Value)
                return 0;
            if (obj.ToString().ToLower().Trim() == "system.object")
                return 0;
            if (string.IsNullOrEmpty(obj.ToString().Trim()))
                return 0;
            double i;
            if (double.TryParse(obj.ToString(), out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        /// 判断是否是正数字字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ConvertDouble(string obj)
        {
            if (obj == null)
                return 0;
            if (string.IsNullOrEmpty(obj))
                return 0;
            if (obj.ToLower().Trim() == "system.object")
                return 0;
            if (string.IsNullOrEmpty(obj.Trim()))
                return 0;
            double i;
            if (double.TryParse(obj, out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ConvertDecimal(string obj)
        {
            if (obj == null)
                return 0;
            if (string.IsNullOrEmpty(obj.Trim()))
                return 0;
            if (obj.ToLower().Trim() == "system.object")
                return 0;
            decimal i;
            if (decimal.TryParse(obj, out i))
            {
                return i;
            }
            return 0;
        }
        /// <summary>
        /// 转换为日期型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>转换失败是返回1753/01/01 00:00:00（数据库最小时间）</returns>
        public static DateTime ConvertDateTime(object obj)
        {
            if (obj == null)
                return SqlDateTime.MinValue.Value;
            if (obj == DBNull.Value)
                return SqlDateTime.MinValue.Value;
            if (obj.ToString().ToLower().Trim() == "system.object")
                return SqlDateTime.MinValue.Value;
            if (string.IsNullOrEmpty(obj.ToString().Trim()))
                return SqlDateTime.MinValue.Value;
            DateTime i;
            if (DateTime.TryParse(obj.ToString(), out i))
            {
                return i;
            }
            return SqlDateTime.MinValue.Value;
        }

        public static decimal ConvertDecimal(int obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ConvertDecimal(float obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ConvertDecimal(double obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ConvertDecimal(UInt32 obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ConvertDecimal(long obj)
        {
            return Convert.ToDecimal(obj);
        }

        public static double ConvertDouble(int obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ConvertDouble(float obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ConvertDouble(UInt32 obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ConvertDouble(long obj)
        {
            return Convert.ToDouble(obj);
        }
        /// <summary>
        /// 转换为日期型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ConvertDateTime(string obj)
        {
            if (obj == null)
                return SqlDateTime.MinValue.Value;
            if (string.IsNullOrEmpty(obj.Trim()))
                return SqlDateTime.MinValue.Value;
            if (obj.ToLower().Trim() == "system.object")
                return SqlDateTime.MinValue.Value;
            DateTime i;
            if (DateTime.TryParse(obj, out i))
            {
                return i;
            }
            return SqlDateTime.MinValue.Value;
        }
        #endregion

        #region  数据转换成xml
        /**/
        /// <summary>
        /// 将DataTable对象转换成XML字符串
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <returns>XML字符串</returns>
        public static string CDataToXml(DataTable dt)
        {
            if (dt != null)
            {
                try
                {
                    byte[] temp;
                    using (var ms = new MemoryStream())
                    {
                        using (var xmlWt = new XmlTextWriter(ms, Encoding.Unicode))
                        {
                            dt.WriteXml(xmlWt);
                        }
                        var count = (int)ms.Length;
                        temp = new byte[count];
                        ms.Seek(0, SeekOrigin.Begin);
                        ms.Read(temp, 0, count);
                    }
                    //返回Unicode编码的文本
                    var ucode = new UnicodeEncoding();
                    string returnValue = ucode.GetString(temp).Trim();
                    return returnValue;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return string.Empty;
        }

        /**/
        /// <summary>
        /// 将DataSet对象中指定的Table转换成XML字符串
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <param name="tableIndex">DataSet对象中的Table索引</param>
        /// <returns>XML字符串</returns>
        public static string CDataToXml(DataSet ds, int tableIndex)
        {
            if (ds != null && ds.Tables != null)
            {
                if (tableIndex != -1 && ds.Tables.Count >= tableIndex)
                {
                    return CDataToXml(ds.Tables[tableIndex]);
                }
                if (ds.Tables.Count > 0)
                {
                    return CDataToXml(ds.Tables[0]);
                }
            }
            return string.Empty;
        }

        /**/
        /// <summary>
        /// 将DataSet对象转换成XML字符串
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <returns>XML字符串</returns>
        public static string CDataToXml(DataSet ds)
        {
            return CDataToXml(ds, -1);
        }
        /**/
        /// <summary>
        /// 将DataView对象转换成XML字符串
        /// </summary>
        /// <param name="dv">DataView对象</param>
        /// <returns>XML字符串</returns>
        public static string CDataToXml(DataView dv)
        {
            return CDataToXml(dv.Table);
        }
        /**/
        /// <summary>
        /// 将DataSet对象数据保存为XML文件
        /// </summary>
        /// <param name="dt">DataSet</param>
        /// <param name="xmlFilePath">XML文件路径(相对路径)</param>
        /// <returns>bool值</returns>
        public static bool CDataToXmlFile(DataTable dt, string xmlFilePath)
        {
            if ((dt != null) && (!string.IsNullOrEmpty(xmlFilePath)))
            {
                string path = HttpContext.Current.Server.MapPath(xmlFilePath);
                try
                {
                    byte[] temp;
                    using (var ms = new MemoryStream())
                    {
                        using (var xmlWt = new XmlTextWriter(ms, Encoding.Unicode))
                        {
                            dt.WriteXml(xmlWt);
                        }
                        var count = (int)ms.Length;
                        temp = new byte[count];
                        ms.Seek(0, SeekOrigin.Begin);
                        ms.Read(temp, 0, count);
                    }
                    //返回Unicode编码的文本
                    var ucode = new UnicodeEncoding();
                    //写文件
                    using (var sw = new StreamWriter(path))
                    {
                        sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        sw.WriteLine(ucode.GetString(temp).Trim());
                        sw.Close();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            return false;
        }

        /**/
        /// <summary>
        /// 将DataSet对象中指定的Table转换成XML文件
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <param name="tableIndex">DataSet对象中的Table索引</param>
        /// <param name="xmlFilePath">xml文件路径</param>
        /// <returns>bool]值</returns>
        public static bool CDataToXmlFile(DataSet ds, int tableIndex, string xmlFilePath)
        {
            if (tableIndex != -1)
            {
                return CDataToXmlFile(ds.Tables[tableIndex], xmlFilePath);
            }
            return CDataToXmlFile(ds.Tables[0], xmlFilePath);
        }

        /**/
        /// <summary>
        /// 将DataSet对象转换成XML文件
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <param name="xmlFilePath">xml文件路径</param>
        /// <returns>bool]值</returns>
        public static bool CDataToXmlFile(DataSet ds, string xmlFilePath)
        {
            return CDataToXmlFile(ds, -1, xmlFilePath);
        }
        /**/
        /// <summary>
        /// 将DataView对象转换成XML文件
        /// </summary>
        /// <param name="dv">DataView对象</param>
        /// <param name="xmlFilePath">xml文件路径</param>
        /// <returns>bool]值</returns>
        public static bool CDataToXmlFile(DataView dv, string xmlFilePath)
        {
            return CDataToXmlFile(dv.Table, xmlFilePath);
        }


        #endregion

        #region  XML形式的字符串、XML文江转换成DataSet、DataTable格式
        /**/
        /// <summary>
        /// 将Xml内容字符串转换成DataSet对象
        /// </summary>
        /// <param name="xmlStr">Xml内容字符串</param>
        /// <returns>DataSet对象</returns>
        public static DataSet CXmlToDataSet(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                try
                {
                    var ds = new DataSet();
                    //读取字符串中的信息
                    using (var strStream = new StringReader(xmlStr))
                    {
                        using (var xmlrdr = new XmlTextReader(strStream))
                        {
                            ds.ReadXml(xmlrdr);
                        }
                    }
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            return null;
        }

        /**/
        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param name="xmlStr">Xml字符串</param>
        /// <param name="tableIndex">Table表索引</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDatatTable(string xmlStr, int tableIndex)
        {
            return CXmlToDataSet(xmlStr).Tables[tableIndex];
        }
        /**/
        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param name="xmlStr">Xml字符串</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDatatTable(string xmlStr)
        {
            return CXmlToDataSet(xmlStr).Tables[0];
        }
        /**/
        /// <summary>
        /// 读取Xml文件信息,并转换成DataSet对象
        /// </summary>
        /// <remarks>
        /// DataSet ds = new DataSet();
        /// ds = CXmlFileToDataSet("/XML/upload.xml");
        /// </remarks>
        /// <param name="xmlFilePath">Xml文件地址</param>
        /// <returns>DataSet对象</returns>
        public static DataSet CXmlFileToDataSet(string xmlFilePath)
        {
            if (!string.IsNullOrEmpty(xmlFilePath))
            {
                string path = HttpContext.Current.Server.MapPath(xmlFilePath);
                try
                {
                    var xmldoc = new XmlDocument();
                    //根据地址加载Xml文件
                    xmldoc.Load(path);

                    var ds = new DataSet();
                    //读取文件中的字符流
                    using (var strStream = new StringReader(xmldoc.InnerXml))
                    {
                        using (var xmlrdr = new XmlTextReader(strStream))
                        {
                            ds.ReadXml(xmlrdr);
                        }
                    }
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return null;
        }

        /**/
        /// <summary>
        /// 读取Xml文件信息,并转换成DataTable对象
        /// </summary>
        /// <param name="xmlFilePath">xml文江路径</param>
        /// <param name="tableIndex">Table索引</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDataTable(string xmlFilePath, int tableIndex)
        {
            return CXmlFileToDataSet(xmlFilePath).Tables[tableIndex];
        }
        /**/
        /// <summary>
        /// 读取Xml文件信息,并转换成DataTable对象
        /// </summary>
        /// <param name="xmlFilePath">xml文江路径</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDataTable(string xmlFilePath)
        {
            return CXmlFileToDataSet(xmlFilePath).Tables[0];
        }



        #endregion

        #region DataTable、DataRow、XML、实体、实体集合互转

        /**/
        /// <summary>
        /// 数据转换DataRow为List对象
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="tableRow">DataRow</param>
        /// <returns>实体类对象</returns>
        public static T ConvertToEntity<T>(DataRow tableRow) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();

            foreach (DataColumn col in tableRow.Table.Columns)
            {
                string colName = col.ColumnName;

                // Look for the object's property with the columns name, ignore case
                PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // did we find the property ?
                if (pInfo != null)
                {
                    object val = tableRow[colName];

                    // is this a Nullable<> type
                    bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                    if (IsNullable)
                    {
                        if (val is DBNull)
                        {
                            val = null;
                        }
                        else
                        {
                            // Convert the db type into the T we have in our Nullable<T> type
                            val = Convert.ChangeType
                    (val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                        }
                    }
                    else
                    {
                        // Convert the db type into the type of the property in our entity
                        val = Convert.ChangeType(val, pInfo.PropertyType);
                    }
                    // Set the value of the property with the value from the db
                    pInfo.SetValue(returnObject, val, null);
                }
            }

            // return the entity object with values
            return returnObject;
        }

        /**/
        /// <summary>
        /// 数据转换DataTable为List对象
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>实体类集合</returns>
        public static List<T> ConvertToList<T>(DataTable table) where T : new()
        {
            // Return the finished list
            return (from DataRow dr in table.Rows select ConvertToEntity<T>(dr)).ToList();
        }

        /**/
        /// <summary> 
        ///  实体类序列化成xml  
        /// </summary>  
        /// <param name="enitities">实体.</param>  
        /// <param name="headtag">节点名称</param>  
        /// <returns></returns>  
        public static string ObjListToXml<T>(List<T> enitities, string headtag)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propinfos = null;
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf - 8\"?>");
            sb.AppendLine("<" + headtag + ">");
            foreach (T obj in enitities)
            {
                //初始化propertyinfo  
                if (propinfos == null)
                {
                    Type objtype = obj.GetType();
                    propinfos = objtype.GetProperties();
                }

                sb.AppendLine("<item>");
                foreach (PropertyInfo propinfo in propinfos)
                {
                    sb.Append("<");
                    sb.Append(propinfo.Name);
                    sb.Append(">");
                    sb.Append(propinfo.GetValue(obj, null));
                    sb.Append("</");
                    sb.Append(propinfo.Name);
                    sb.AppendLine(">");
                }
                sb.AppendLine("</item>");
            }
            sb.AppendLine("</" + headtag + ">");
            return sb.ToString();
        }

        /**/
        /// <summary>  
        /// xml文件转化为实体类列表  
        /// </summary>  
        /// <typeparam name="T">实体名称</typeparam>  
        /// <param name="xml">您的xml文件</param>  
        /// <param name="headtag">xml头文件</param>  
        /// <returns>实体列表</returns>  
        public static List<T> LinqXmlToObjList<T>(string xml, string headtag) where T : new()
        {

            List<T> list = new List<T>();
            XDocument xDocument = XDocument.Parse(xml);
            PropertyInfo[] propinfos = null;
            //XmlNodeList nodelist = doc.SelectNodes(headtag);  
            var nodes = from f in xDocument.Elements(headtag).Elements("item") select f;
            foreach (var item in nodes)
            {
                T entity = new T();
                //初始化propertyinfo  
                if (propinfos == null)
                {
                    Type objtype = entity.GetType();
                    propinfos = objtype.GetProperties();
                }
                //填充entity类的属性  
                foreach (PropertyInfo propinfo in propinfos)
                {
                    //实体类字段首字母变成小写的  
                    string name = propinfo.Name.Substring(0, 1) + propinfo.Name.Substring(1, propinfo.Name.Length - 1);
                    var cnode = item.Element(name);
                    if (cnode != null)
                    {
                        string value = cnode.Value;
                        propinfo.SetValue(entity, Convert.ChangeType(value, propinfo.PropertyType, null), null);
                    }
                }
                list.Add(entity);

            }
            return list;

        }

        #endregion

        #region Dictinary与String互转

        public static string DictionaryToString(Dictionary<string, string> d)
        {
            // Build up each line one-by-one and them trim the end
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in d)
            {
                builder.Append(pair.Key).Append(":").Append(pair.Value).Append(',');
            }
            string result = builder.ToString();
            // Remove the final delimiter
            result = result.TrimEnd(',');
            return result;
        }

        public static Dictionary<string, string> GetDict(string f)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            // Divide all pairs (remove empty strings)
            string[] tokens = f.Split(new[] { ':', ',' },
                StringSplitOptions.RemoveEmptyEntries);
            // Walk through each item
            for (int i = 0; i < tokens.Length; i += 2)
            {
                string name = tokens[i];
                string freq = tokens[i + 1];

                // Fill the value in the sorted dictionary
                if (d.ContainsKey(name))
                {
                    d[name] += freq;
                }
                else
                {
                    d.Add(name, freq);
                }
            }
            return d;
        }

        #endregion

        #region Xml序列化反序列化
        /**/
        ///*****************************************
        /// <summary>
        /// 序列化一个对象
        /// </summary>
        /// <param name="o">将要序列化的对象</param>
        /// <returns>返回byte[]</returns>
        ///*****************************************
        public static byte[] Serialize(object o)
        {
            if (o == null) return null;
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, o);
            ms.Position = 0;
            byte[] b = new byte[ms.Length];
            ms.Read(b, 0, b.Length);
            ms.Close();
            return b;
        }

        /**/
        ///*****************************************
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="b">返回一个对象</param>
        ///*****************************************
        public static object Deserialize(byte[] b)
        {
            if (b.Length == 0) return null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                ms.Write(b, 0, b.Length);
                ms.Position = 0;
                object n = (object)bf.Deserialize(ms);
                ms.Close();
                return n;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return null;
            }
        }

        #endregion

        #region Json序列化与反序列化

        /// <summary> 
        /// JSON序列化 
        /// </summary> 
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            //替换Json的Date字符串 
            string p = @"\/Date((d+)+d+)\/";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }
        /// <summary> 
        /// JSON反序列化 
        /// </summary> 
        public static T JsonDeserialize<T>(string jsonString)
        {
            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"/Date(1294499956278+0800)/"格式 
            string p = @"d{4}-d{2}-d{2}sd{2}:d{2}:d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
        /// <summary> 
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串 
        /// </summary> 
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }
        /// <summary> 
        /// 将时间字符串转为Json时间 
        /// </summary> 
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format(@"\/Date({0}+0800)\/", ts.TotalMilliseconds);
            return result;
        }


        #endregion
    }
}