namespace roxority.SharePoint
{
    using Microsoft.SharePoint;
    using System;

    internal class SPWrap<T> : IDisposable where T: class
    {
        private readonly bool dispose;
        public readonly SPSite Site;
        public readonly T Value;
        public readonly SPWeb Web;

        public SPWrap(SPSite site, SPWeb web, Converter<SPWeb, T> converter)
        {
            this.Value = default(T);
            this.dispose = true;
            this.Site = site;
            this.Web = web;
            if (converter != null)
            {
                this.Value = converter(this.Web);
            }
        }

        public SPWrap(SPSite site, SPWeb web, T value, bool dispose)
        {
            this.Value = default(T);
            this.dispose = true;
            this.Site = site;
            this.Web = web;
            this.Value = value;
            this.dispose = dispose;
        }

        public static SPWrap<T> Create(string fullUrl, Converter<SPWeb, T> converter)
        {
            SPSite site;
            if (string.IsNullOrEmpty(fullUrl))
            {
                throw new ArgumentNullException("fullUrl");
            }
            if (!fullUrl.StartsWith("http://") && !fullUrl.StartsWith("https://"))
            {
                fullUrl = ProductPage.MergeUrlPaths(fullUrl.StartsWith("/") ? SPContext.Current.Site.Url : SPContext.Current.Web.Url, fullUrl);
            }
            return new SPWrap<T>(site = new SPSite(fullUrl), site.OpenWeb(), converter);
        }

        void IDisposable.Dispose()
        {
            if (this.dispose)
            {
                if (this.Web != null)
                {
                    this.Web.Dispose();
                }
                if (this.Site != null)
                {
                    this.Site.Dispose();
                }
            }
        }
    }
}

