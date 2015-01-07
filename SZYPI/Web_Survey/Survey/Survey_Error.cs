using LoginClass;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Survey.Survey
{
    public class Survey_Error : Page, IRequiresSessionState
    {
        protected Label Label1;

        protected string getErrorMessage(int intEC, string[] _L)
        {
            _L[0] = "非法输入";
            _L[0x11] = "未找到问卷";
            _L[6] = "此问卷需要会员登录后才能答卷，请登录后答卷！";
            if ((intEC != 0x2d) || ((1 != 0) && (0 != 0)))
            {
                try
                {
                    return _L[intEC];
                }
                catch
                {
                    return "非法的输入";
                }
            }
            return "会员积分分值不足以回答问卷";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            long num;
            languageClass class2 = null; //赋初值
            int num2 = 0; //赋初值
            string[] strArray = null; //赋初值
            int num4;
            goto Label_0023;
        Label_0002:
            switch (num4)
            {
                case 0:
                    goto Label_0035;

                case 1:
                    this.Label1.Text = "非法的输入";
                    return;

                case 2:
                        short intEC = Convert.ToInt16(base.Request.QueryString["EC"]);
                        this.Label1.Text = this.getErrorMessage(intEC, strArray);
                        return;
                case 3:
                    num2 = class2.getLan(num);
                    num4 = 0;
                    goto Label_0002;

                case 4:
                    if (num <= 0)
                    {
                        goto Label_0035;
                    }
                    num4 = 3;
                    goto Label_0002;

                case 5:
                    try
                    {
                        num = Convert.ToInt64(base.Request.QueryString["SID"]);
                    }
                    catch
                    {
                    }
                    class2 = new languageClass();
                    class2.getLanguage();
                    num2 = 1;
                    num4 = 4;
                    goto Label_0002;
            }
        Label_0023:
            num = 0;
            num4 = 5;
            goto Label_0002;
        Label_0035:
            strArray = class2._arrLanguage[num2, 1].Split(new char[] { '|' });
            num4 = 2;
            goto Label_0002;
        }

        protected HttpApplication ApplicationInstance
        {
            get
            {
                return this.Context.ApplicationInstance;
            }
        }

        protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile)this.Context.Profile;
            }
        }
    }
}

