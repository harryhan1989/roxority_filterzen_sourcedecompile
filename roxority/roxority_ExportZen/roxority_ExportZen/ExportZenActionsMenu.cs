namespace roxority_ExportZen
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.WebControls;
    using roxority.SharePoint;
    using System;
    using System.IO;
    using System.Web;

    public class ExportZenActionsMenu : ActionsMenu
    {
        public static void RemoveLegacyTemplate()
        {
            try
            {
                ProductPage.Elevate(delegate {
                    string str = null;
                    SPSecurity.CatchAccessDeniedException = false;
                    ProductPage.GetSite(ProductPage.GetContext());
                    try
                    {
                        str = HttpContext.Current.Server.MapPath("/_controltemplates/roxority_ExportZen_MenuItem.ascx");
                    }
                    catch
                    {
                    }
                    if (string.IsNullOrEmpty(str))
                    {
                        str = Path.Combine(ProductPage.HivePath, @"TEMPLATE\CONTROLTEMPLATES\roxority_ExportZen_MenuItem.ascx");
                    }
                    if (File.Exists(str))
                    {
                        File.Delete(str);
                    }
                }, true, true);
            }
            catch
            {
            }
        }
    }
}

