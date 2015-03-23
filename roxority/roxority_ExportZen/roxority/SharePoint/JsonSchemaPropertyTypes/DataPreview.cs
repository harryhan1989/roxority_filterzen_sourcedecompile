namespace roxority.SharePoint.JsonSchemaPropertyTypes
{
    using roxority.SharePoint;
    using roxority_RollupZen;
    using System;
    using System.Collections;
    using System.Text;

    public class DataPreview : JsonSchemaManager.Property.Type
    {
        public override string RenderValueForEdit(JsonSchemaManager.Property prop, IDictionary instance, bool disabled, bool readOnly)
        {
            int pageSize = new Random().Next(0x7fffff9b, 0x7fffffff);
            int pictMode = new Random().Next(0x7fffff37, 0x7fffff9b);
            string properties = ProductPage.GuidLower(Guid.NewGuid(), false);
            string tabPropName = ProductPage.GuidLower(Guid.NewGuid(), false);
            string groupPropName = ProductPage.GuidLower(Guid.NewGuid(), false);
            string dynInst = ProductPage.GuidLower(Guid.NewGuid(), false);
            string dsid = instance["id"] + string.Empty;
            string id = dsid + "_" + prop.Name;
            string introduced16 = roxority_RollupZen.RollupWebPart.GetReloadScript("roxReloadRollup", id + "_tb", id, pageSize, 0, 1, 1, 1, false, false, false, properties, true, true, true, string.Empty, false, tabPropName, string.Empty, string.Empty, groupPropName, false, false, true, false, true, 2, "180px", 1, pictMode, true, true, 0, "k", false, null, dsid, dynInst);
            string str7 = introduced16.Replace(pageSize.ToString(), "parseInt(roxGetCtlVal('" + dsid + "', 'pps'))").Replace(pictMode.ToString(), "(roxGetCtlVal('" + dsid + "',roxDataTypePrefixes[roxGetCtlVal('" + dsid + "', 't')]+'_pp')?1:0)").Replace("\"" + tabPropName + "\"", "roxGetFieldSel('" + id + "_tabby')").Replace("\"" + groupPropName + "\"", "roxGetFieldSel('" + id + "_groupby')").Replace("\"" + dynInst + "\"", "roxGetDynInst('" + dsid + "')").Replace("\"" + properties + "\"", "roxSlimEncode(roxGetCtlVal('" + dsid + "',roxDataTypePrefixes[roxGetCtlVal('" + dsid + "', 't')]+'_pd'))");
            StringBuilder builder = new StringBuilder();
            builder.Append("<div class=\"rox-dataprev-tools\">");
            builder.Append(DataFields.RenderFieldDropDown(base["Tool_ItemEditor_DataPreview_GroupBy", new object[0]], true, id + "_groupby", string.Empty, string.Empty, false, true, prop.IsLe(2)));
            builder.Append(DataFields.RenderFieldDropDown(base["Tool_ItemEditor_DataPreview_TabBy", new object[0]], true, id + "_tabby", string.Empty, string.Empty, false, true, prop.IsLe(4)));
            builder.Append("<label for=\"" + dsid + "_pps\">" + base["Tool_ItemEditor_DataPreview_PageSize", new object[0]] + " </label><input " + (prop.IsLe(2) ? string.Empty : "disabled=\"disabled\"") + " style=\"width: 20px;\" id=\"" + dsid + "_pps\" onchange=\"this.value=roxValidateNumeric('" + dsid + "', 'pps', '4');\" value=\"4\" />");
            builder.Append("\n<script type=\"text/javascript\" language=\"JavaScript\">\nfunction rel" + tabPropName + "(){" + str7 + "}\n</script>\n");
            builder.Append("<input onclick=\"rel" + tabPropName + "();\" type=\"button\" value=\"" + base["Tool_ItemEditor_DataPreview_Refresh", new object[0]] + "\"/></div>");
            builder.Append("<textarea style=\"display: none; width: 98%;\" id=\"" + id + "_tb\"></textarea><div class=\"rox-dataprev\" id=\"" + id + "\"><div id=\"rox_rollup_" + id + "\"><div class=\"rox-dataprev-hint rox-rollup-paging\" id=\"rox_pager_" + id + "\">" + base["Tool_ItemEditor_DataPreview_RefreshHint", new object[0]] + "</div>");
            builder.Append("</div></div>");
            return builder.ToString();
        }

        public override bool ShowInSummary
        {
            get
            {
                return false;
            }
        }
    }
}

