using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;

namespace Web_Survey.Survey
{
    public class Survey_FileManage : Page, IRequiresSessionState
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

