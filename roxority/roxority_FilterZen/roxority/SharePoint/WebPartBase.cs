namespace roxority.SharePoint
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.WebPartPages;
    using Microsoft.SharePoint.WebPartPages.Communication;
    using System;
    using System.Reflection;
    using System.Web;
    using System.Web.UI.WebControls.WebParts;

    public abstract class WebPartBase : Microsoft.SharePoint.WebPartPages.WebPart
    {
        private bool ec;
        private bool ex;
        internal bool expiredTitles;
        private int jQuery;
        private ProductPage.LicInfo licInfo;
        protected internal string urlPropertyPrefix = "prop_";
        private bool urlSettings;

        protected WebPartBase()
        {
        }

        public override ConnectionRunAt CanRunAt()
        {
            return ConnectionRunAt.Server;
        }

        protected internal T GetProp<T>(string name, T value)
        {
            return (T) this.GetPropValue(name, value);
        }

        protected internal object GetPropValue(string name, object value)
        {
            string str = this.urlPropertyPrefix + name;
            string s = (this.Context == null) ? null : this.Context.Request.QueryString[str];
            if ((this.UrlSettings && (this.Context != null)) && (Array.IndexOf<string>(this.Context.Request.QueryString.AllKeys, str) >= 0))
            {
                if (value is bool)
                {
                    value = (s == "1") ? true : ((s == "0") ? false : value);
                    return value;
                }
                if (value is int)
                {
                    int num;
                    value = int.TryParse(s, out num) ? num : value;
                    return value;
                }
                if (value is double)
                {
                    double num2;
                    value = double.TryParse(s, out num2) ? num2 : value;
                    return value;
                }
                if (value is long)
                {
                    long num3;
                    value = long.TryParse(s, out num3) ? num3 : value;
                    return value;
                }
                value = s;
            }
            return value;
        }

        internal bool LicEd(int edition)
        {
            if (!this.TwilightZone)
            {
                return ProductPage.LicEdition(ProductPage.GetContext(), this.Lic, edition);
            }
            return true;
        }

        public override bool RequiresWebPartClientScript()
        {
            return true;
        }

        protected internal virtual bool CanRun
        {
            get
            {
                try
                {
                    return (((this.Context != null) && (SPContext.Current != null)) && (SPContext.Current.Site != null));
                }
                catch
                {
                    return false;
                }
            }
        }

        protected override HttpContext Context
        {
            get
            {
                try
                {
                    return base.Context;
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool EffectiveJquery
        {
            get
            {
                if (this.JQuery == 1)
                {
                    return true;
                }
                if (this.JQuery == 0)
                {
                    return !ProductPage.Config<bool>(ProductPage.GetContext(), "_nojquery");
                }
                return false;
            }
        }

        internal virtual bool Exed
        {
            get
            {
                if (!this.ec && !this.TwilightZone)
                {
                    this.ec = true;
                    this.ex = this.Lic.expired;
                }
                return this.ex;
            }
        }

        public string ExpiredMessage
        {
            get
            {
                return ProductPage.GetResource("LicExpiry", new object[0]);
            }
        }

        public virtual bool IsConnected
        {
            get
            {
                return false;
            }
        }

        public bool IsDesign
        {
            get
            {
                return ((base.DesignMode || (base.WebPartManager == null)) || ((base.WebPartManager.DisplayMode != null) && base.WebPartManager.DisplayMode.AllowPageDesign));
            }
        }

        public bool IsFrontPage
        {
            get
            {
                if (this.Context == null)
                {
                    return false;
                }
                return ((!string.IsNullOrEmpty(this.Context.Request.UserAgent) && this.Context.Request.UserAgent.ToLowerInvariant().Contains("msfrontpage")) || this.Context.Request.Url.ToString().ToLowerInvariant().Contains("/_layouts/toolpane.aspx"));
            }
        }

        public bool IsPreview
        {
            get
            {
                return this.Context.Request.Url.ToString().ToLowerInvariant().Contains("/_layouts/wpprevw.aspx?");
            }
        }

        public virtual bool IsViewPage
        {
            get
            {
                return ((this.Page is WebPartPage) && this.Page.GetType().FullName.StartsWith("ASP.VIEWPAGE_ASPX__", StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public string this[string resKey, object[] args]
        {
            get
            {
                return ProductPage.GetProductResource(resKey, args);
            }
        }

        [Personalizable]
        public int JQuery
        {
            get
            {
                return this.GetProp<int>("JQuery", this.jQuery);
            }
            set
            {
                this.jQuery = (value < 0) ? 0 : ((value > 2) ? 2 : value);
            }
        }

        internal ProductPage.LicInfo Lic
        {
            get
            {
                if ((this.licInfo == null) && !this.TwilightZone)
                {
                    this.licInfo = ProductPage.LicInfo.Get(null);
                }
                return this.licInfo;
            }
        }

        public static SPSite OffSite
        {
            get
            {
                return ProductPage.currentSite;
            }
            set
            {
                ProductPage.currentSite = value;
            }
        }

        [Personalizable]
        public override string Title
        {
            get
            {
                if ((this.Exed && this.CanRun) && this.expiredTitles)
                {
                    return this.ExpiredMessage;
                }
                return base.Title;
            }
            set
            {
                base.Title = ((this.Exed && this.CanRun) && this.expiredTitles) ? this.ExpiredMessage : value.Replace(this.ExpiredMessage, string.Empty);
            }
        }

        [Personalizable]
        public override string TitleUrl
        {
            get
            {
                if ((this.Exed && this.CanRun) && this.expiredTitles)
                {
                    return (ProductPage.GetProductResource("_WhiteLabelUrl", new object[0]) + ProductPage.GetTitle().ToLowerInvariant() + "-license/");
                }
                return base.TitleUrl;
            }
            set
            {
                base.TitleUrl = ((this.Exed && this.CanRun) && this.expiredTitles) ? (ProductPage.GetProductResource("_WhiteLabelUrl", new object[0]) + ProductPage.GetTitle().ToLowerInvariant() + "-license/") : value.Replace(ProductPage.GetProductResource("_WhiteLabelUrl", new object[0]) + ProductPage.GetTitle().ToLowerInvariant() + "-license/", "");
            }
        }

        private bool TwilightZone
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return false;
                }
                if (this.Page != null)
                {
                    return (this.Parent == null);
                }
                return true;
            }
        }

        [Personalizable]
        public virtual bool UrlSettings
        {
            get
            {
                return this.urlSettings;
            }
            set
            {
                this.urlSettings = value;
            }
        }
    }
}

