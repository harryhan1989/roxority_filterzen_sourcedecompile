namespace roxority.SharePoint
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Administration;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml;

    public class FeatureReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPSite site = null;
            SPList list = null;
            List<string> list2 = new List<string>();
            bool flag = false;
            bool flag2 = true;
            try
            {
                foreach (SPElementDefinition definition in (IEnumerable) properties.Definition.GetElementDefinitions(CultureInfo.InvariantCulture))
                {
                    if (definition.XmlDefinition.HasChildNodes)
                    {
                        foreach (XmlNode node in definition.XmlDefinition.ChildNodes)
                        {
                            foreach (XmlAttribute attribute in node.Attributes)
                            {
                                if (attribute.LocalName == "Url")
                                {
                                    string str;
                                    if (!list2.Contains(str = attribute.Value.ToLowerInvariant()))
                                    {
                                        list2.Add(str);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            try
            {
                if (((list2.Count > 0) && ((site = properties.Feature.Parent as SPSite) != null)) && ((list = site.GetCatalog(SPListTemplateType.WebPartCatalog)) != null))
                {
                    while (flag2)
                    {
                        flag2 = false;
                        foreach (SPListItem item in ProductPage.TryEach<SPListItem>(list.Items))
                        {
                            if (list2.Contains(item.Name.ToLowerInvariant()))
                            {
                                item.Delete();
                                flag2 = flag = true;
                                continue;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            try
            {
                if (flag)
                {
                    site.AllowUnsafeUpdates = true;
                    list.Update();
                }
            }
            catch
            {
            }
        }

        public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        {
        }

        public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        {
        }
    }
}

