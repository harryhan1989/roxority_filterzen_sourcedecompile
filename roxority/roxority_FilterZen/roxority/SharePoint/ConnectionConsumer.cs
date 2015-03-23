namespace roxority.SharePoint
{
    using System;
    using System.Reflection;
    using System.Web.UI.WebControls.WebParts;

    public class ConnectionConsumer : ConsumerConnectionPoint
    {
        public ConnectionConsumer(MethodInfo callbackMethod, System.Type interfaceType, System.Type controlType, string displayName, string id, bool allowsMultipleConnections) : base(callbackMethod, interfaceType, controlType, ProductPage.GetProductResource(displayName, new object[0]), id, allowsMultipleConnections)
        {
        }
    }
}

