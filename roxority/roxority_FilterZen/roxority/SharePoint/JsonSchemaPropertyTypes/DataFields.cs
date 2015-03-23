namespace roxority.SharePoint.JsonSchemaPropertyTypes
{
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Web;

    public class DataFields : JsonSchemaManager.Property.Type.String
    {
        public const string OPTION_REFRESH = "1b150b00-f653-4504-a47d-e7b59a4a12f6";

        internal static string RenderFieldDropDown(string label, bool labelInSelect, string ctlID, string buttonLabel, string onChange, bool divEnclose, bool namesOnly, bool enabled)
        {
            bool flag = !string.IsNullOrEmpty(buttonLabel);
            string str = divEnclose ? "<div>" : string.Empty;
            string str2 = ctlID.Substring(0, ctlID.IndexOf('_'));
            string str3 = str;
            string[] strArray = new string[20];
            strArray[0] = str3;
            strArray[1] = labelInSelect ? string.Empty : ("<label>" + HttpUtility.HtmlEncode(label) + "</label> ");
            strArray[2] = "<select ";
            strArray[3] = enabled ? string.Empty : "disabled=\"disabled\"";
            strArray[4] = " class=\"rox-iteminst-fieldsel-";
            strArray[5] = str2;
            strArray[6] = namesOnly ? " rox-iteminst-fieldsel-small" : string.Empty;
            strArray[7] = "\" id=\"";
            strArray[8] = ctlID;
            strArray[9] = "\" onchange=\"if(this.options[this.selectedIndex].value=='1b150b00-f653-4504-a47d-e7b59a4a12f6')roxRefreshFieldList('";
            strArray[10] = ctlID;
            strArray[11] = "');else if(";
            bool flag2 = !flag;
            strArray[12] = flag2.ToString().ToLowerInvariant();
            strArray[13] = "){";
            strArray[14] = onChange;
            strArray[15] = "}\"><option>";
            strArray[0x10] = labelInSelect ? HttpUtility.HtmlEncode(label) : ProductPage.GetResource("Tool_ItemEditor_DataFields_OptionSel", new object[0]);
            strArray[0x11] = "</option><option value=\"1b150b00-f653-4504-a47d-e7b59a4a12f6\">";
            strArray[0x12] = ProductPage.GetResource("Tool_ItemEditor_DataFields_OptionRef", new object[0]);
            strArray[0x13] = "</option></select>";
            str = string.Concat(strArray);
            if (flag)
            {
                string str4 = str;
                str = str4 + "<button " + (enabled ? string.Empty : "disabled=\"disabled\"") + " id=\"" + ctlID + "_btn\" class=\"rox-iteminst-fieldsel-" + str2 + "\" onclick=\"{" + HttpUtility.HtmlAttributeEncode(onChange) + "}\" type=\"button\">" + HttpUtility.HtmlEncode(buttonLabel) + "</button>";
            }
            return (str + (divEnclose ? "</div>" : string.Empty));
        }

        public override string RenderValueForDisplay(JsonSchemaManager.Property prop, object val)
        {
            prop.RawSchema["lines"] = 3;
            return base.RenderValueForDisplay(prop, val);
        }

        public override string RenderValueForEdit(JsonSchemaManager.Property prop, IDictionary instance, bool disabled, bool readOnly)
        {
            bool flag = prop.Name.EndsWith("_pd");
            string str2 = instance["id"] + "_" + prop.Name;
            prop.RawSchema["lines"] = 3;
            string str = base.RenderValueForEdit(prop, instance, disabled, readOnly);
            if (JsonSchemaManager.Bool(prop.RawSchema["allow_fieldsel"], true))
            {
                str = str + RenderFieldDropDown(base["Tool_ItemEditor_DataFields_LabelAdd", new object[0]], false, str2 + "_fields", string.Empty, "var tb=jQuery('#" + str2 + "'),sel=jQuery('#" + str2 + @"_fields')[0];if((sel.selectedIndex>0)&&(sel.selectedIndex<(sel.options.length-1))){tb.val(tb.val()+(tb.val() ?'\n':'')+'" + (flag ? string.Empty : "[") + "'+sel.options[sel.selectedIndex].value+'" + (flag ? string.Empty : "]") + "');roxScrollEnd(tb[0]);sel.selectedIndex=0;}", true, false, prop.Editable);
            }
            return str;
        }

        public override string CssClass
        {
            get
            {
                return ("rox-iteminst-edit-" + typeof(JsonSchemaManager.Property.Type.String).Name);
            }
        }
    }
}

