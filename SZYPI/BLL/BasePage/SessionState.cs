using System;
using System.Data;
using System.Web;
using Nandasoft;
using Nandasoft.BaseModule;
using BLL;

namespace WebUI
{
	/// <summary>
	/// 目的：用于存放系统中声明的所有Session对象.
	/// 日期：2008-9-2
	/// </summary>
	public sealed class SessionState
	{
        /// <summary>
        /// 用户ID
        /// </summary>
        public static long UserID
        {
            get
            {
                return NDConvert.ToInt64(HttpContext.Current.Session["UserID"]);
            }
            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public static long EmployeeID
        {
            get
            {
                return NDConvert.ToInt64(HttpContext.Current.Session["EmployeeID"]);
            }
            set
            {
                HttpContext.Current.Session["EmployeeID"] = value;
            }
        }
      
        /// <summary>
        /// 用户帐号
        /// </summary>
        public static string Account
        {
            get
            {
                if (HttpContext.Current.Session["Account"] != null)
                {
                    return NDConvert.ToString(HttpContext.Current.Session["Account"]);
                }
                return "";
            }
            set
            {
                HttpContext.Current.Session["Account"] = value;
            }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["EmployeeName"] != null)
                {
                    return NDConvert.ToString(HttpContext.Current.Session["EmployeeName"]);
                }
                return "";
            }
            set
            {
                HttpContext.Current.Session["EmployeeName"] = value;
            }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public static CommonEnum.UserType UserType
        {
            get
            {
                return (CommonEnum.UserType)HttpContext.Current.Session["UserType"];
            }
            set
            {
                HttpContext.Current.Session["UserType"] = value;
            }
        }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public static bool IsAdmin
        {
            get
            {
                if (HttpContext.Current.Session["IsAdmin"] != null)
                {
                    return NDConvert.ToBoolean(HttpContext.Current.Session["IsAdmin"]);
                }
                return false;
            }
            set
            {
                HttpContext.Current.Session["IsAdmin"] = value;
            }
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public static long OUID
        {
            get
            {
                if (HttpContext.Current.Session["OUID"] != null)
                {
                    return NDConvert.ToInt64(HttpContext.Current.Session["OUID"]);
                }
                return -1;
            }
            set
            {
                HttpContext.Current.Session["OUID"] = value;
            }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public static string OUName
        {
            get
            {
                if (HttpContext.Current.Session["OUName"] != null)
                {
                    return HttpContext.Current.Session["OUName"].ToString();
                }
                return "";
            }
            set
            {
                HttpContext.Current.Session["OUName"] = value;
            }
        }

        /// <summary>
        /// 部门类型
        /// </summary>
        public static int OUType
        {
            get
            {
                if (HttpContext.Current.Session["OUType"] != null)
                {
                    return NDConvert.ToInt32(HttpContext.Current.Session["OUType"].ToString());
                }
                return -1;
            }
            set
            {
                HttpContext.Current.Session["OUType"] = value;
            }
        }        

        /// <summary>
        /// 验证用户是否登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogon()
        {
            if (HttpContext.Current.Session["UserType"] != null && HttpContext.Current.Session["UserID"] != null && HttpContext.Current.Session["Account"] != null &&  HttpContext.Current.Session["OUID"] != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public static void OutLogon()
        {
            if (HttpContext.Current.Session["UserID"] != null)
            {
                //HttpContext.Current.Session.Contents.Remove("UserID");
                HttpContext.Current.Session.Clear();
            }
        }

        
	}
}
