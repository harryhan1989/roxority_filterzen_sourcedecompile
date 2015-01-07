using System;
using System.Data;
using System.Web;
using Nandasoft;
using Nandasoft.BaseModule;
using BLL;

namespace WebUI
{
	/// <summary>
	/// Ŀ�ģ����ڴ��ϵͳ������������Session����.
	/// ���ڣ�2008-9-2
	/// </summary>
	public sealed class SessionState
	{
        /// <summary>
        /// �û�ID
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
        /// �û�ID
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
        /// �û��ʺ�
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
        /// �û�����
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
        /// �û�����
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
        /// �Ƿ��ǹ���Ա
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
        /// ����ID
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
        /// ��������
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
        /// ��������
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
        /// ��֤�û��Ƿ��¼
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
        /// �˳���¼
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
