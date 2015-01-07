using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Net;
using LoginClass;
using System.Data.SqlClient;
using BusinessLayer.Survey;
using Business.Helper;

namespace Web_Survey.Survey
{
    [System.Reflection.ObfuscationAttribute(Exclude = true)]
    public partial class Survey_SurveyCode : System.Web.UI.Page
    {
        public long SID1 = 0;
        public string sClientJs = "";
        public string dStartTime;
        
        protected Label SIDhf;
        protected Label StartTimehf;
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] arrSurveyPar = new string[5];
            string sIPCheckResult = "";
            SID1=Convert.ToInt64(base.Request.QueryString["SID"]);
            SIDhf = Page.FindControl("SID") as Label;
            SID1 = Convert.ToInt64(SIDhf.Text);  //注释掉，为找到源文件
            string sURL = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["URL"];
            sClientJs += "intTargetSID=" + SID1.ToString() + ";\nblnAnswerPSW=false;\n";
            SqlDataReader dr = null;
            arrSurveyPar = getSurveyPar(SID1, dr);
            sClientJs += getSurveyExpand(SID1, dr);


            if (arrSurveyPar[3] == "")
            {
                Response.Redirect("../../Error.aspx?EC=0&SID=" + SID1.ToString());//非法输入，无此问卷
            }

            if (arrSurveyPar[3] == "0")
            {
                Response.Redirect("../../Error.aspx?EC=4&SID=" + SID1.ToString());//问卷未启用
            }

            if (arrSurveyPar[1] != "")   //如果非空　即已设置
            {
                if (DateTime.Now > Convert.ToDateTime(arrSurveyPar[1]))
                {
                    Response.Redirect("../../Error.aspx?EC=9&SID=" + SID1.ToString());//问卷过期
                }
            }

            if ((arrSurveyPar[4].IndexOf("|MemberLogin:1|") >= 0) || (arrSurveyPar[4].IndexOf("|GUIDAndDep:1|") >= 0))
            {

                HttpCookie hc = new HttpCookie("SurveyURL", HttpContext.Current.Request.Url.ToString());
                Response.Cookies.Add(hc);
                int GUID = 0;
                //LoginClass.loginClass lc = new LoginClass.loginClass();
                //lc.checkGULogin(out GUID, "../../../gu/tologin.htm");
                if (ConvertHelper.ConvertString(Session["UserIDClient"]) != null && ConvertHelper.ConvertString(Session["UserIDClient"]) != "")
                {
                }
                else
                {
                    Response.Redirect("../../Error.aspx?ec=6&sid=" + SID1.ToString());//不在允许的答卷用户及部门中
                }
                Response.Cookies["SurveyURL"].Expires = DateTime.Now;

              /*        处理指定答卷用户或部门  */
                if (arrSurveyPar[4].IndexOf("|GUIDAndDep:1|") >= 0)
                {
                    if (!checkSurveyExpand_5( SID1, dr, GUID.ToString()))
                    {
                        Response.Redirect("../../Error.aspx?ec=37&sid=" + SID1.ToString());//不在允许的答卷用户及部门中
                    }
                }


                sClientJs += "GUID='" + GUID.ToString() + "';\n";

            }


            if (arrSurveyPar[4].IndexOf("|PSW:1|") >= 0)
            {
                string sPSW = Convert.ToString(Request.Form["PSW"]);

                if (sPSW == null)
                {
                    Response.Redirect("../../EnterPSW.aspx?url=" + sURL + "&e=问卷需要密码");
                }

                if (sPSW != arrSurveyPar[0])
                {
                    Response.Redirect("../../EnterPSW.aspx?url=" + sURL + "&e=密码错误");
                }
            }


            if (arrSurveyPar[4].IndexOf("|CheckCode:1|") >= 0)
            {
                sClientJs += "blnCheckCode=true;\n";
            }

            if (arrSurveyPar[4].IndexOf("|AnswerPSW:1|") >= 0)
            {
                sClientJs += "blnAnswerPSW=true;\n";
            }

            //StartTimehf = Page.FindControl("StartTime") as Label;
            //if (StartTimehf != null)
            //{
            //    StartTimehf.Text = ConvertHelper.ConvertString(DateTime.Now);   //记录答卷的开始时间
            //}
            dStartTime = ConvertHelper.ConvertString(DateTime.Now);
        }

        public string getIP10(string sInputID)
        {
            string[] Ip_List = sInputID.Split(".".ToCharArray());
            string X_Ip = "";
            foreach (string ip in Ip_List)
            {
                string tmp = Convert.ToInt16(ip).ToString("X");
                X_Ip += tmp;
            }
            return Convert.ToString(long.Parse(X_Ip, System.Globalization.NumberStyles.HexNumber));
        }

        public string checkIPArea(SqlDataReader dr, string SID, string sSubmitIP)
        {
            string sResult = "";
            string sArea = "";
            //objComm = new OleDbCommand("SELECT TOP 1 Content FROM CondtionTable WHERE SID=" + SID, objConn);
            dr = new Survey_SurveyCode_Layer().GetCondtionTable(SID);
            if (dr.Read())
            {
                sArea = dr[0].ToString();
            }
            dr.Close();
            if (sArea != "")
            {

                if (sSubmitIP == "")
                {
                    sResult = "25";
                }
                sSubmitIP = getIP10(sSubmitIP);
                //objComm.CommandText = "SELECT TOP 1 Province,City FROM IP WHERE " + sSubmitIP + ">=StartIP AND " + sSubmitIP + "<=EndIP";
                dr = new Survey_SurveyCode_Layer().GetIP(sSubmitIP);
                if (dr.Read())
                {
                    string sCity = "," + dr["City"].ToString() + ",";
                    string sProvince = "," + dr["Province"].ToString() + ",";
                    string[] arrArea = sArea.Split('|');

                    if (arrArea[2].IndexOf(sProvince) < 0)//如果所在省不在允许内
                    {

                        if (sCity == ",,")
                        {
                            sResult = "29";//检查不到城市来源

                        }
                        else if (arrArea[1].IndexOf(sCity) < 0)
                        {
                            sResult = "26";//IP来源城市不在允许的范围
                        }

                    }
                }
                else
                {
                    sResult = "27";//检查不到来源
                }
                dr.Close();
            }
            else
            {
                sResult = "28";
            }

            return sResult;
        }

        protected string getSurveyExpand(long SID, SqlDataReader dr)
        {
            string sResult = "";
            //objComm.CommandText = "SELECT ExpandContent,ExpandType FROM SurveyExpand WHERE SID=" + SID + " AND ExpandType IN(0,1,8)";
            dr = new Survey_SurveyCode_Layer().GetSurveyExpand(SID.ToString());
            while (dr.Read())
            {
                if (dr[1].ToString() == "0")
                {
                    sResult += "sHiddenItem = '" + dr[0].ToString() + "';\n";
                }
                else if (dr[1].ToString() == "1")
                {
                    sResult += "sURLVar = '" + dr[0].ToString() + "';\n";
                }
                else if (dr[1].ToString() == "8")
                {
                    sResult += "sProgressiveAsk = '" + dr[0].ToString() + "';\n";
                }
            }
            dr.Close();
            return sResult;
        }

        protected bool checkSurveyExpand_5(long SID, SqlDataReader dr, string sGUID)
        {
            bool blnResult = true;
            string sTemp = "";
            string sDep = "";
            //objComm.CommandText = "SELECT ExpandContent FROM SurveyExpand WHERE SID=" + SID + " AND ExpandType =5";
            dr = new Survey_SurveyCode_Layer().GetSurveyExpand1(SID.ToString());
            if (dr.Read())
            {
                sTemp = dr[0].ToString();
            }
            dr.Close();
            if (sTemp == "")
            {
                return true;
            }
            string[] arrTemp = sTemp.Split('|');
            //objComm.CommandText = "SELECT TOP 1 Dep FROM GUTable WHERE GUID=" + sGUID;
            sGUID = "," + sGUID + ",";


            if (arrTemp[1] != "")
            {
                if (arrTemp[0] == "W")
                {
                    if (("," + arrTemp[1] + ",").IndexOf(sGUID) < 0)//设置了白名单，而不在白名单中
                    {
                        //如果这样，那只要不处于白名单中，不管是否处于部门黑白名单，均得到同一结果，即部门设置无用
                        //在个人检查中未通过，放到下面部门检查是否通过
                        blnResult = false;
                        //return false;
                    }
                    else
                    {
                        return true;//设置了白名单，在白名单中
                    }
                }
                else
                {
                    if (("," + arrTemp[1] + ",").IndexOf(sGUID) > 0)//设置了黑名单，并且在黑名单中
                    {
                        return false;
                    }
                }
            }

            if (arrTemp[3] != "")
            {
                //dr = objComm.ExecuteReader();
                if (dr.Read())
                {
                    sDep = "," + dr[0].ToString() + ",";
                }
                dr.Close();



                if (arrTemp[2] == "W")
                {
                    if (("," + arrTemp[3] + ",").IndexOf(sDep) < 0)//如果不在部门白名单
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (("," + arrTemp[3] + ",").IndexOf(sDep) > 0)
                    {
                        return false;
                    }

                }
            }

            return blnResult;
        }

        protected string[] getSurveyPar(long SID, SqlDataReader dr)
        {
            string[] arrResult = new string[5];
            //objComm.CommandText = "SELECT SurveyPSW,State,Active,Par,EndDate FROM SurveyTable WHERE SID=" + SID.ToString();
            dr = new Survey_SurveyCode_Layer().GetSurveyTable(SID.ToString());
            while (dr.Read())
            {
                arrResult[0] = dr["SurveyPSW"].ToString();// sSurveyPSW = Convert.ToString(dr["SurveyPSW"]);
                if (!Convert.IsDBNull(dr["EndDate"]))
                {
                    arrResult[1] = dr["EndDate"].ToString();// sEndDate = dr["EndDate"].ToString();                
                }
                else
                {
                    arrResult[1] = "";
                }
                arrResult[2] = dr["State"].ToString();// sState = dr["State"].ToString();
                arrResult[3] = dr["Active"].ToString();// sActive = dr["Active"].ToString();
                arrResult[4] = "|" + Convert.ToString(dr["Par"]) + "|"; //sPar = "|" + Convert.ToString(dr["Par"]) + "|";    

            }
            dr.Close();

            return arrResult;

        }
    }
}