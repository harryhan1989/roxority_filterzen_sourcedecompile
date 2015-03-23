namespace roxority_FilterZen
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Utilities;
    using Microsoft.SharePoint.WebPartPages;
    using roxority.SharePoint;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;

    public class FilterToolPart : ToolPart
    {
        internal TextBox acSecFieldsTextBox = new TextBox();
        internal CheckBox ajax14CheckBox = new CheckBox();
        internal CheckBox ajax14FocusCheckBox = new CheckBox();
        internal TextBox ajax14TextBox = new TextBox();
        internal CheckBox autoCheckBox = new CheckBox();
        private string bigAnnoyance;
        internal RadioButton camlNoRadioButton = new RadioButton();
        internal TextBox camlTextBox = new TextBox();
        internal RadioButton camlYesRadioButton = new RadioButton();
        internal CheckBox cascadedCheckBox = new CheckBox();
        internal RadioButton debugOffRadioButton = new RadioButton();
        internal RadioButton debugOnRadioButton = new RadioButton();
        internal CheckBox defaultOrCheckBox = new CheckBox();
        internal DropDownList disableDropDownList = new DropDownList();
        internal DropDownList dynDropDownList = new DropDownList();
        private FilterBase editFilter;
        internal CheckBox editorEnabledCheckBox = new CheckBox();
        internal TextBox editorNameTextBox = new TextBox();
        internal Panel editorPanel = new Panel();
        internal CheckBox embedFiltersCheckBox = new CheckBox();
        internal RadioButton errorOffRadioButton = new RadioButton();
        internal RadioButton errorOnRadioButton = new RadioButton();
        internal DropDownList filterDropDownList = new DropDownList();
        internal HiddenField filterEditorHidden = new HiddenField();
        internal ListBox filterListBox = new ListBox();
        private List<FilterBase> filters;
        internal DropDownList folderDropDownList = new DropDownList();
        public const string FORMAT_INFO_CONTROL = "<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>";
        public const string FORMAT_PREFIX_CONTROL = "<div class=\"rox-usersection\"><table><tr><td><div class='rox-UserSectionTitle'>{0}</div></td></tr></table><div><table width=\"100%\"><tr><td><div class='rox-UserSectionHead'><label for='{2}'>{1}</label></div><div class='rox-UserSectionBody'><div class='rox-UserControlGroup'>";
        public const string FORMAT_SUFFIX_CONTROL = "</div></div><div class='rox-UserDottedLine'> </div></td></tr></table></div></div>";
        public const string FORMAT_TOOL_BUTTON = "<a id=\"{0}\" href=\"#noop\" onclick=\"{1}\" class=\"rox-toolbutton\" style=\"visibility: hidden;\"><img alt=\"{3}\" border=\"0\" src=\"/_layouts/images/{2}\" title=\"{3}\"/></a>";
        internal TextBox groupsTextBox = new TextBox();
        internal TextBox hiddenTextBox = new TextBox();
        internal CheckBox highlightCheckBox = new CheckBox();
        internal DropDownList htmlDropDownList = new DropDownList();
        internal Panel htmlPanel = new Panel();
        internal DropDownList htmlTempDropDownList = new DropDownList();
        internal TextBox htmlTextBox = new TextBox();
        internal DropDownList jqueryDropDownList = new DropDownList();
        internal TextBox jsonTextBox = new TextBox();
        public const string MAPPING_SEPARATOR = "=>";
        internal TextBox maxTextBox = new TextBox();
        internal DropDownList multiDropDownList = new DropDownList();
        internal DropDownList nameDropDownList = new DropDownList();
        internal CheckBox recollapseGroupsCheckBox = new CheckBox();
        internal CheckBox rememberCheckBox = new CheckBox();
        internal CheckBox respectCheckBox = new CheckBox();
        internal CheckBox searchCheckBox = new CheckBox();
        internal CheckBox sendOnChangeCheckBox = new CheckBox();
        internal CheckBox showClearCheckBox = new CheckBox();
        internal CheckBox suppressCheckBox = new CheckBox();
        internal CheckBox toolSpaceCheckBox = new CheckBox();
        internal CheckBox toolStyleCheckBox = new CheckBox();
        internal CheckBox urlParamCheckBox = new CheckBox();
        internal CheckBox urlSettingsCheckBox = new CheckBox();
        private roxority_FilterWebPart webPart;
        private string webUrl;
        internal Button wizardButton = new Button();
        internal DropDownList wizardFieldDropDownList = new DropDownList();
        internal HiddenField wizardHidden = new HiddenField();
        internal HiddenField wizardHidden2 = new HiddenField();
        internal Label wizardLabel = new Label();
        internal DropDownList wizardListDropDownList = new DropDownList();
        internal TextBox wizardTextBox = new TextBox();

        public FilterToolPart(string title)
        {
            this.Title = title;
            this.ChromeState = PartChromeState.Normal;
            this.Visible = true;
        }

        public override void ApplyChanges()
        {
            base.ApplyChanges();
            ProductPage.Check("ApplyChanges", false);
            this.EnsureChildControls();
            if ((this.WebPart != null) && this.WebPart.CanRun)
            {
                int num;
                int num2;
                this.WebPart.ApplyToolbarStylings = this.toolStyleCheckBox.Checked;
                this.WebPart.SuppressSpacing = this.toolSpaceCheckBox.Checked;
                this.WebPart.MultiValueFilterID = this.multiDropDownList.SelectedValue;
                this.WebPart.AutoRepost = this.sendOnChangeCheckBox.Checked;
                this.WebPart.EmbedFilters = this.embedFiltersCheckBox.Checked;
                this.WebPart.DebugMode = this.debugOnRadioButton.Checked;
                this.WebPart.ErrorMode = this.errorOnRadioButton.Checked;
                this.WebPart.FiltersList = this.Filters;
                this.WebPart.HtmlEmbed = this.htmlTextBox.Text;
                this.WebPart.HtmlMode = this.htmlDropDownList.SelectedIndex;
                this.WebPart.JQuery = this.jqueryDropDownList.SelectedIndex;
                if (int.TryParse(this.maxTextBox.Text, out num))
                {
                    this.WebPart.MaxFiltersPerRow = num;
                }
                else
                {
                    this.WebPart.MaxFiltersPerRow = 0;
                    this.maxTextBox.Text = "0";
                }
                this.WebPart.DynamicInteractiveFilters = this.dynDropDownList.SelectedIndex;
                this.WebPart.CamlFilters = this.camlYesRadioButton.Checked;
                this.WebPart.AutoConnect = this.autoCheckBox.Checked;
                this.WebPart.CamlFiltersAndCombined = this.camlTextBox.Text;
                this.WebPart.JsonFilters = this.jsonTextBox.Text;
                this.WebPart.SuppressUnknownFilters = this.suppressCheckBox.Checked;
                this.WebPart.RememberFilterValues = this.rememberCheckBox.Checked;
                this.WebPart.Cascaded = this.cascadedCheckBox.Checked;
                this.WebPart.FolderScope = this.folderDropDownList.SelectedValue;
                this.WebPart.RecollapseGroups = this.recollapseGroupsCheckBox.Checked;
                this.WebPart.DisableFilters = this.disableDropDownList.SelectedIndex < 2;
                this.WebPart.DisableFiltersSome = this.disableDropDownList.SelectedIndex == 1;
                this.WebPart.RespectFilters = this.respectCheckBox.Checked;
                this.WebPart.DefaultToOr = this.defaultOrCheckBox.Checked;
                this.WebPart.UrlSettings = this.urlSettingsCheckBox.Checked;
                this.WebPart.Ajax14hide = this.ajax14CheckBox.Checked;
                this.WebPart.Ajax14focus = this.ajax14FocusCheckBox.Checked;
                this.WebPart.Ajax14Interval = int.TryParse(this.ajax14TextBox.Text, out num2) ? num2 : 0;
                this.WebPart.Groups = this.groupsTextBox.Text;
                this.webPart.forceReload = true;
                this.webPart.UrlParams = this.urlParamCheckBox.Checked;
                this.webPart.SearchBehaviour = this.searchCheckBox.Checked;
                this.webPart.Highlight = this.highlightCheckBox.Checked;
                this.webPart.ShowClearButtons = this.showClearCheckBox.Checked;
                this.webPart.AcSecFields = this.acSecFieldsTextBox.Text;
            }
            base.ApplyChanges();
        }

        public override void CancelChanges()
        {
            if (this.WebPart != null)
            {
                this.WebPart.toolPart = null;
            }
            base.CancelChanges();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if ((this.WebPart != null) && this.WebPart.CanRun)
            {
                this.WebPart.EnsureChildControls2();
                this.wizardHidden.ID = "roxListWizardHidden";
                this.Controls.Add(this.wizardHidden);
                this.wizardHidden2.ID = "roxListWizardHidden2";
                this.Controls.Add(this.wizardHidden2);
                ProductPage.CreateLicControls(this.Controls, "<div class=\"rox-usersection\"><table><tr><td><div class='rox-UserSectionTitle'>{0}</div></td></tr></table><div><table width=\"100%\"><tr><td><div class='rox-UserSectionHead'><label for='{2}'>{1}</label></div><div class='rox-UserSectionBody'><div class='rox-UserControlGroup'>".Replace("UserSectionTitle\"", "UserSectionTitle\" style=\"font-weight: normal;\""), "</div></div><div class='rox-UserDottedLine'> </div></td></tr></table></div></div>");
                this.Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"roxfilteraction\" id=\"roxfilteraction\" value=\"\"/>"));
                this.hiddenTextBox.CssClass = "ms-input";
                this.hiddenTextBox.TextMode = TextBoxMode.MultiLine;
                this.hiddenTextBox.Rows = 4;
                this.hiddenTextBox.Style[HtmlTextWriterStyle.Display] = "none";
                this.hiddenTextBox.Width = new Unit(200.0, UnitType.Pixel);
                this.hiddenTextBox.Wrap = true;
                this.Controls.Add(this.hiddenTextBox);
                this.Controls.Add(new LiteralControl("<span id=\"roxfiltereditor\" style=\"display: none;\">"));
                this.Controls.Add(new LiteralControl(string.Format("<div class=\"rox-usersection\"><table><tr><td><div class='rox-UserSectionTitle'>{0}</div></td></tr></table><div><table width=\"100%\"><tr><td><div class='rox-UserSectionHead'><label for='{2}'>{1}</label></div><div class='rox-UserSectionBody'><div class='rox-UserControlGroup'>", "<span id=\"roxeditortitle\">Filter Editor</span>", this["SectionDesc_FilterEditor", new object[0]], null)));
                this.Controls.Add(this.filterEditorHidden);
                this.Controls.Add(new LiteralControl("<div class=\"rox-infobox\" id=\"roxListWizard\" style=\"display: none;\">" + this["WizardInfo", new object[0]] + "<fieldset><legend>" + this["WizardTitle", new object[0]] + "</legend>"));
                this.wizardTextBox.CssClass = "ms-input";
                this.wizardTextBox.Width = new Unit(85.0, UnitType.Percentage);
                this.Controls.Add(this.wizardTextBox);
                this.wizardButton.ID = "wizardButton";
                this.wizardButton.Style["font-size"] = "xx-small";
                this.wizardButton.Text = this["WizardGo", new object[0]];
                this.wizardButton.UseSubmitBehavior = false;
                this.wizardButton.CausesValidation = false;
                this.wizardButton.Width = new Unit(15.0, UnitType.Percentage);
                this.Controls.Add(this.wizardButton);
                this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> var roxHiddenFieldID = '" + this.wizardHidden.ClientID + "', roxHidden2FieldID = '" + this.wizardHidden2.ClientID + "'; configInitPage('" + this.wizardButton.ClientID + "'); </script>"));
                this.wizardListDropDownList.AutoPostBack = true;
                this.wizardListDropDownList.ID = "wizardListDropDownList";
                this.wizardListDropDownList.Width = new Unit(98.0, UnitType.Percentage);
                this.Controls.Add(this.wizardListDropDownList);
                this.wizardFieldDropDownList.ID = "wizardFieldDropDownList";
                this.wizardFieldDropDownList.Width = new Unit(98.0, UnitType.Percentage);
                this.Controls.Add(this.wizardFieldDropDownList);
                this.wizardLabel.Style["padding"] = "2px 2px 2px 20px";
                this.wizardLabel.Style["display"] = "block";
                this.wizardLabel.Style["background"] = "url('" + this.WebUrl + "/_layouts/images/forward.gif') 2px 2px no-repeat";
                this.wizardLabel.Text = this["WizardLabel", new object[0]];
                this.Controls.Add(this.wizardLabel);
                this.wizardFieldDropDownList.Attributes["onchange"] = "jQuery('#" + this.wizardLabel.ClientID + "').html(this.options[this.selectedIndex].value ? '" + this["WizardName", new object[0]].Substring(0, this["WizardName", new object[0]].IndexOf("<b>")).Replace("'", @"\'") + "<b>' + this.options[this.selectedIndex].value + '</b>" + this["WizardName", new object[0]].Substring(this["WizardName", new object[0]].IndexOf("</b>") + 4).Replace("'", @"\'") + "' : (roxListViewUrl ? roxListViewUrl : '" + this["WizardLabel", new object[0]].Replace("'", @"\'") + "'));";
                this.Controls.Add(new LiteralControl("</fieldset>" + this["WizardInfo2", new object[0]] + "</div>"));
                this.Controls.Add(new LiteralControl("<div class=\"rox-prop rox-proplimited\"><span style=\"display: inline-block; float: left;\">" + this["Edit_T_Name", new object[0]] + ":</span>"));
                this.Controls.Add(new LiteralControl("<a id=\"roxListWizardLink\" onclick=\"toggleListWizard();\" href=\"#noop\">" + this["WizardLink", new object[0]] + "</a></div><div class=\"rox-prop\">"));
                this.editorNameTextBox.CssClass = "ms-input rox-proplimited";
                this.editorNameTextBox.ID = "editorNameTextBox";
                this.editorNameTextBox.Style["clear"] = "both";
                this.editorNameTextBox.Width = new Unit(98.0, UnitType.Percentage);
                this.Controls.Add(this.editorNameTextBox);
                this.nameDropDownList.ID = "nameDropDownList";
                this.nameDropDownList.Width = new Unit(96.0, UnitType.Percentage);
                this.nameDropDownList.Attributes["onblur"] = "jQuery('#roxfilternames').hide();";
                this.nameDropDownList.Attributes["onfocus"] = "setTimeout(\"jQuery('#roxfilternames').show();\", 50);setTimeout(\"document.getElementById('roxfilternames').focus();\", 100);";
                this.Controls.Add(new LiteralControl("<div style=\"display: none;\" id=\"roxfilternames\">"));
                this.Controls.Add(this.nameDropDownList);
                this.Controls.Add(new LiteralControl("</div><div class=\"rox-proplimited\">"));
                this.editorEnabledCheckBox.ID = "editorEnabledCheckBox";
                this.editorEnabledCheckBox.Text = this["Edit_T_Enabled", new object[0]];
                this.Controls.Add(this.editorEnabledCheckBox);
                this.Controls.Add(new LiteralControl("</div>"));
                this.editorEnabledCheckBox.LabelAttributes["id"] = "label_editorEnabledCheckBox";
                this.nameDropDownList.Attributes["onchange"] = "if(this.selectedIndex>0){document.getElementById(nameTextBoxID).value=this.options[this.selectedIndex].value;document.getElementById(nameTextBoxID).focus();setTimeout(\"jQuery('#roxfilternames').hide();\", 50);this.selectedIndex=0;}";
                this.editorNameTextBox.Attributes["onblur"] = "jQuery('#roxfilternames').hide();";
                this.editorNameTextBox.Attributes["onfocus"] = "if(document.getElementById('" + this.nameDropDownList.ClientID + "').options.length>1)jQuery('#roxfilternames').show();";
                this.editorNameTextBox.Attributes["onchange"] = this.editorNameTextBox.Attributes["onkeyup"] = "roxUpdatePreview();";
                this.editorPanel.ID = "editorPanel";
                this.Controls.Add(this.editorPanel);
                this.Controls.Add(new LiteralControl("</div>"));
                this.Controls.Add(new LiteralControl("<div style=\"float: right; white-space: nowrap;\"><button style=\"background-image: url('" + this.WebUrl + "/_layouts/images/saveitem.gif');\" onclick=\"toolFilterAction('editsave');\" class=\"UserButton\" id=\"roxfiltappbtn\">" + this["BtnApply", new object[0]] + "</button><button style=\"background-image: url('" + this.WebUrl + "/_layouts/images/delete.gif');\" onclick=\"toolFilterAction('editstop');\" class=\"UserButton\" id=\"roxfiltdiscbtn\">" + this["BtnDiscard", new object[0]] + "</button></div><br style=\"clear: both;\"/>"));
                this.Controls.Add(new LiteralControl("</div></div><div class='rox-UserDottedLine'> </div></td></tr></table></div></div>"));
                this.Controls.Add(new LiteralControl("</span><span id=\"roxfilterlist\">"));
                this.Controls.Add(new LiteralControl(string.Format("<div class=\"rox-usersection\"><table><tr><td><div class='rox-UserSectionTitle'>{0}</div></td></tr></table><div><table width=\"100%\"><tr><td><div class='rox-UserSectionHead'><label for='{2}'>{1}</label></div><div class='rox-UserSectionBody'><div class='rox-UserControlGroup'>", this["Section_Filters", new object[0]], this["SectionDesc_Filters", new object[0]], null)));
                this.Controls.Add(new LiteralControl("<div class=\"rox-toolbar\">"));
                this.filterDropDownList.Items.Clear();
                this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\">\n"));
                this.Controls.Add(new LiteralControl("roxFilterDescs[''] = '" + this["FilterDesc_None", new object[0]] + "';\n"));
                this.filterDropDownList.Items.Add(new ListItem(this["FilterType_None", new object[0]], string.Empty));
                foreach (System.Type type in FilterBase.FilterTypes)
                {
                    this.filterDropDownList.Items.Add(new ListItem(FilterBase.GetFilterTypeTitle(type), type.AssemblyQualifiedName));
                    this.filterDropDownList.Items[this.filterDropDownList.Items.Count - 1].Attributes["style"] = "font-weight: bold;";
                    this.Controls.Add(new LiteralControl("roxFilterDescs['" + type.AssemblyQualifiedName + "'] = '" + SPEncode.ScriptEncode(FilterBase.GetFilterTypeDesc(type)) + "';\n"));
                }
                this.Controls.Add(new LiteralControl("</script>\n"));
                this.filterDropDownList.Attributes["onchange"] = "document.getElementById('roxFilterDesc').innerText=roxFilterDescs[this.options[this.selectedIndex].value];jQuery('#roxFilterAddButton').css({'visibility':((this.selectedIndex>0)?'visible':'hidden')});";
                this.filterDropDownList.CssClass = "ms-input";
                this.filterDropDownList.Style["width"] = "180px";
                this.filterDropDownList.ID = "filterDropDownList";
                this.Controls.Add(this.filterDropDownList);
                this.Controls.Add(new LiteralControl(string.Format("<a id=\"{0}\" href=\"#noop\" onclick=\"{1}\" class=\"rox-toolbutton\" style=\"visibility: hidden;\"><img alt=\"{3}\" border=\"0\" src=\"/_layouts/images/{2}\" title=\"{3}\"/></a>", new object[] { "roxFilterAddButton", "toolFilterAction('add');", "collapseplus.gif", this["ToolButton_AddFilter", new object[0]] })));
                this.Controls.Add(new LiteralControl(string.Format("<a id=\"{0}\" href=\"#noop\" onclick=\"{1}\" class=\"rox-toolbutton\" style=\"visibility: hidden;\"><img alt=\"{3}\" border=\"0\" src=\"/_layouts/images/{2}\" title=\"{3}\"/></a>", new object[] { "roxFilterUpButton", "toolFilterAction('up');", "arrupi.gif", this["ToolButton_FilterUp", new object[0]] })));
                this.Controls.Add(new LiteralControl(string.Format("<a id=\"{0}\" href=\"#noop\" onclick=\"{1}\" class=\"rox-toolbutton\" style=\"visibility: hidden;\"><img alt=\"{3}\" border=\"0\" src=\"/_layouts/images/{2}\" title=\"{3}\"/></a>", new object[] { "roxFilterDownButton", "toolFilterAction('down');", "arrdowni.gif", this["ToolButton_FilterDown", new object[0]] })));
                this.Controls.Add(new LiteralControl(string.Format("<a id=\"{0}\" href=\"#noop\" onclick=\"{1}\" class=\"rox-toolbutton\" style=\"visibility: hidden;\"><img alt=\"{3}\" border=\"0\" src=\"/_layouts/images/{2}\" title=\"{3}\"/></a>", new object[] { "roxFilterEditButton", "toolFilterAction('edit');", "edit.gif", this["ToolButton_EditFilter", new object[0]] })));
                this.Controls.Add(new LiteralControl(string.Format("<a id=\"{0}\" href=\"#noop\" onclick=\"{1}\" class=\"rox-toolbutton\" style=\"visibility: hidden;\"><img alt=\"{3}\" border=\"0\" src=\"/_layouts/images/{2}\" title=\"{3}\"/></a>", new object[] { "roxFilterRemoveButton", "toolFilterAction('remove', '" + this["ToolPrompt_RemoveFilter", new object[0]] + "');", "filteroff.gif", this["ToolButton_RemoveFilter", new object[0]] })));
                this.Controls.Add(new LiteralControl("</div>"));
                this.filterListBox.CssClass = "ms-input";
                this.filterListBox.Width = new Unit(98.0, UnitType.Percentage);
                this.filterListBox.ID = "filterListBox";
                this.Controls.Add(this.filterListBox);
                this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> wizardFieldDropDownListID = '" + this.wizardFieldDropDownList.ClientID + "'; nameTextBoxID = '" + this.editorNameTextBox.ClientID + "'; filterListBoxID = '" + this.filterListBox.ClientID + "'; </script>"));
                this.Controls.Add(new LiteralControl(string.Format("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", this["FilterDesc_None", new object[0]], "blank.gif", "roxFilterDesc") + "</div></div><div class='rox-UserDottedLine'> </div></td></tr></table></div></div>"));
                this.Controls.Add(new LiteralControl("<span id=\"roxifilters\" style=\"display: none;\">"));
                this.Controls.Add(new LiteralControl(string.Format("<div class=\"rox-usersection\"><table><tr><td><div class='rox-UserSectionTitle'>{0}</div></td></tr></table><div><table width=\"100%\"><tr><td><div class='rox-UserSectionHead'><label for='{2}'>{1}</label></div><div class='rox-UserSectionBody'><div class='rox-UserControlGroup'>", this["Section_Interactive", new object[0]], this["SectionDesc_Interactive", new object[0]], "") + "<div><br/>"));
                this.toolStyleCheckBox.Checked = this.WebPart.ApplyToolbarStylings;
                this.toolStyleCheckBox.Text = this["ApplyToolStylings", new object[0]];
                this.Controls.Add(this.toolStyleCheckBox);
                this.Controls.Add(new LiteralControl("</div><div style=\"display: " + (ProductPage.Is14 ? "none" : "block") + ";\">"));
                this.toolSpaceCheckBox.Checked = this.WebPart.SuppressSpacing;
                this.toolSpaceCheckBox.Text = this["SuppressSpacing", new object[0]];
                this.Controls.Add(this.toolSpaceCheckBox);
                this.Controls.Add(new LiteralControl("</div>" + ProductPage.GetResource("Css", new object[] { ProductPage.AssemblyName }) + "<br/><div>"));
                this.cascadedCheckBox.Checked = this.WebPart.Cascaded;
                this.cascadedCheckBox.Enabled = this.WebPart.LicEd(4);
                this.cascadedCheckBox.Text = this["Cascaded", new object[0]];
                this.Controls.Add(this.cascadedCheckBox);
                this.Controls.Add(new LiteralControl("</div><div>"));
                this.rememberCheckBox.Checked = this.WebPart.RememberFilterValues;
                this.rememberCheckBox.Enabled = this.WebPart.LicEd(2);
                this.rememberCheckBox.Text = this["RememberFilterValues", new object[0]];
                this.Controls.Add(this.rememberCheckBox);
                this.Controls.Add(new LiteralControl("</div><div>"));
                this.sendOnChangeCheckBox.Checked = this.WebPart.AutoRepost;
                this.sendOnChangeCheckBox.Text = this["Prop_RepostOnChange", new object[0]];
                this.Controls.Add(this.sendOnChangeCheckBox);
                if (ProductPage.Is14)
                {
                    this.Controls.Add(new LiteralControl("</div><div>"));
                    this.embedFiltersCheckBox.Checked = this.WebPart.EmbedFilters;
                    this.embedFiltersCheckBox.Text = this["Prop_EmbedFilters", new object[0]];
                    this.Controls.Add(this.embedFiltersCheckBox);
                }
                this.Controls.Add(new LiteralControl("</div><br/><div>" + this["HtmlPre", new object[0]] + "</div>"));
                for (int i = 0; i < 3; i++)
                {
                    this.htmlDropDownList.Items.Add(new ListItem(this["Html" + i, new object[0]], i.ToString()));
                }
                this.htmlDropDownList.CssClass = "ms-input";
                this.htmlDropDownList.ID = "htmlDropDownList";
                this.htmlDropDownList.Width = new Unit(98.0, UnitType.Percentage);
                this.htmlDropDownList.SelectedIndex = this.WebPart.HtmlMode;
                this.Controls.Add(this.htmlDropDownList);
                this.Controls.Add(new LiteralControl(this["Html", new object[0]]));
                this.htmlTextBox.CssClass = "ms-input";
                this.htmlTextBox.ID = "htmlTextBox";
                this.htmlTextBox.TextMode = TextBoxMode.MultiLine;
                this.htmlTextBox.Rows = 6;
                this.htmlTextBox.Width = new Unit(98.0, UnitType.Percentage);
                this.htmlTextBox.Text = this.WebPart.HtmlEmbed;
                this.Controls.Add(this.htmlTextBox);
                this.Controls.Add(new LiteralControl(this["HtmlTemp", new object[0]] + " "));
                this.htmlTempDropDownList.CssClass = "ms-input";
                this.htmlTempDropDownList.Items.Add(new ListItem("", ""));
                for (int j = 0; j < int.Parse(this["HtmlTempCount", new object[0]]); j++)
                {
                    this.htmlTempDropDownList.Items.Add(new ListItem(this["HtmlTitle" + j, new object[0]], this["HtmlTemp" + j, new object[0]]));
                }
                this.Controls.Add(this.htmlTempDropDownList);
                this.Controls.Add(new LiteralControl("<fieldset><legend>" + this["Preview", new object[0]] + "</legend>"));
                this.htmlPanel.CssClass = "ms-WPBody rox-preview";
                this.htmlPanel.ID = "htmlPanel";
                this.htmlPanel.Style[HtmlTextWriterStyle.TextAlign] = (this.htmlDropDownList.SelectedIndex == 2) ? "right" : ((this.htmlDropDownList.SelectedIndex == 1) ? "left" : "center");
                this.Controls.Add(this.htmlPanel);
                this.Controls.Add(new LiteralControl("</fieldset><script type=\"text/javascript\" language=\"JavaScript\"> setInterval(function() { jQuery('#" + this.htmlPanel.ClientID + "').html(jQuery('#" + this.htmlTextBox.ClientID + "').text()); }, 1000); </script>"));
                this.htmlTempDropDownList.Attributes["onchange"] = "document.getElementById('" + this.htmlTextBox.ClientID + "').innerText=this.options[this.selectedIndex].value;jQuery('#" + this.htmlPanel.ClientID + "').html(jQuery('#" + this.htmlTextBox.ClientID + "').text());";
                this.htmlDropDownList.Attributes["onchange"] = "document.getElementById('" + this.htmlPanel.ClientID + "').style.textAlign=((this.selectedIndex==2)?'right':((this.selectedIndex==1)?'left':'center'));";
                this.Controls.Add(new LiteralControl("<div><br/>"));
                this.showClearCheckBox.Text = this["ShowClearButtons", new object[0]];
                this.showClearCheckBox.Checked = (this.showClearCheckBox.Enabled = this.webPart.LicEd(4)) && this.WebPart.showClearButtons;
                this.Controls.Add(this.showClearCheckBox);
                this.Controls.Add(new LiteralControl("</div><div><br/>" + this["MaxFiltersPerRow", new object[0]]));
                this.maxTextBox.CssClass = "ms-input";
                this.maxTextBox.Width = new Unit(98.0, UnitType.Percentage);
                this.maxTextBox.Text = this.WebPart.MaxFiltersPerRow.ToString();
                this.Controls.Add(this.maxTextBox);
                this.Controls.Add(new LiteralControl("</div><br/><div>"));
                this.highlightCheckBox.Text = this["Highlight", new object[0]];
                this.highlightCheckBox.Checked = (this.highlightCheckBox.Enabled = this.webPart.LicEd(4)) && this.WebPart.highlight;
                this.Controls.Add(this.highlightCheckBox);
                this.Controls.Add(new LiteralControl("</div><div>"));
                this.searchCheckBox.Text = this["SearchBehavior", new object[0]];
                this.searchCheckBox.Checked = (this.searchCheckBox.Enabled = this.webPart.LicEd(4)) && this.WebPart.searchBehaviour;
                this.Controls.Add(this.searchCheckBox);
                this.Controls.Add(new LiteralControl("</div><div>"));
                this.urlParamCheckBox.Text = this["UrlParams", new object[0]];
                this.urlParamCheckBox.Checked = (this.urlParamCheckBox.Enabled = this.webPart.LicEd(4)) && this.WebPart.urlParams;
                this.Controls.Add(this.urlParamCheckBox);
                this.Controls.Add(new LiteralControl("</div></div></div><div class='rox-UserDottedLine'> </div></td></tr></table></div></div></span>"));
                this.Controls.Add(new LiteralControl(string.Format("<div class=\"rox-usersection\"><table><tr><td><div class='rox-UserSectionTitle'>{0}</div></td></tr></table><div><table width=\"100%\"><tr><td><div class='rox-UserSectionHead'><label for='{2}'>{1}</label></div><div class='rox-UserSectionBody'><div class='rox-UserControlGroup'>", this["FilteringMode", new object[0]], this["CamlFilters", new object[0]], "")));
                this.Controls.Add(new LiteralControl("<br/><div>"));
                this.camlNoRadioButton.Enabled = this.camlYesRadioButton.Enabled = this.webPart.LicEd(4);
                this.camlNoRadioButton.GroupName = this.camlYesRadioButton.GroupName = "CamlFilters";
                this.camlNoRadioButton.Checked = true;
                if (this.camlYesRadioButton.Checked = this.WebPart.CamlFilters)
                {
                    this.camlNoRadioButton.Checked = false;
                }
                this.camlYesRadioButton.Text = this["CamlFiltersYes", new object[0]];
                this.camlNoRadioButton.Text = this["CamlFiltersNo", new object[0]];
                this.Controls.Add(this.camlNoRadioButton);
                this.Controls.Add(new LiteralControl("</div><br/><div>"));
                this.Controls.Add(this.camlYesRadioButton);
                this.Controls.Add(new LiteralControl("</div><br/><div style=\"padding: 4px; background: InfoBackground; color: InfoText; border: 1px solid #c0c0c0;\">" + this["CamlListView", new object[0]] + "<br/><br/><div>" + this["DisableFilters1", new object[0]] + " "));
                this.disableDropDownList.ID = "disableDropDownList";
                this.Controls.Add(this.disableDropDownList);
                this.Controls.Add(new LiteralControl(" " + this["DisableFilters2", new object[0]] + "</div><div>"));
                this.respectCheckBox.Checked = this.WebPart.respectFilters;
                this.respectCheckBox.Text = this["RespectFilters", new object[0]];
                this.Controls.Add(this.respectCheckBox);
                this.Controls.Add(new LiteralControl("</div><div>"));
                this.recollapseGroupsCheckBox.Checked = this.WebPart.recollapseGroups;
                this.recollapseGroupsCheckBox.Text = this["RecollapseGroups", new object[0]];
                this.Controls.Add(this.recollapseGroupsCheckBox);
                this.Controls.Add(new LiteralControl("</div><div>"));
                this.defaultOrCheckBox.Checked = this.WebPart.defaultToOr;
                this.defaultOrCheckBox.Text = this["DefaultToOr", new object[0]];
                this.Controls.Add(this.defaultOrCheckBox);
                this.Controls.Add(new LiteralControl("</div><div>" + this["NoListFolders", new object[0]] + "<br/>"));
                this.folderDropDownList.Items.Add(new ListItem(this["FolderScope_", new object[0]], string.Empty));
                foreach (object obj2 in Enum.GetValues(typeof(SPViewScope)))
                {
                    this.folderDropDownList.Items.Add(new ListItem(this["FolderScope_" + obj2, new object[0]], obj2 + string.Empty));
                }
                this.folderDropDownList.CssClass = "ms-input";
                this.folderDropDownList.ID = "folderDropDownList";
                this.folderDropDownList.Width = new Unit(98.0, UnitType.Percentage);
                if (this.folderDropDownList.Enabled = this.WebPart.LicEd(4))
                {
                    this.folderDropDownList.SelectedValue = this.WebPart.FolderScope;
                }
                this.Controls.Add(this.folderDropDownList);
                this.Controls.Add(new LiteralControl("</div><br/><div>" + this["CamlAnd", new object[0]]));
                this.camlTextBox.CssClass = "ms-input";
                this.camlTextBox.Enabled = this.WebPart.LicEd(4);
                this.camlTextBox.Text = this.WebPart.CamlFiltersAndCombined;
                this.camlTextBox.TextMode = TextBoxMode.MultiLine;
                this.camlTextBox.Wrap = false;
                this.camlTextBox.Width = new Unit(98.0, UnitType.Percentage);
                this.Controls.Add(this.camlTextBox);
                this.Controls.Add(new LiteralControl("</div><br/><div>" + this["JsonFilters", new object[0]]));
                this.jsonTextBox.CssClass = "ms-input";
                this.jsonTextBox.Enabled = this.WebPart.LicEd(4);
                this.jsonTextBox.Text = this.WebPart.JsonFilters;
                this.jsonTextBox.TextMode = TextBoxMode.MultiLine;
                this.jsonTextBox.Wrap = false;
                this.jsonTextBox.Width = new Unit(98.0, UnitType.Percentage);
                this.Controls.Add(this.jsonTextBox);
                this.Controls.Add(new LiteralControl("<br/></div></div>"));
                this.Controls.Add(new LiteralControl("</div></div><div class='rox-UserDottedLine'> </div></td></tr></table></div></div>"));
                this.Controls.Add(new LiteralControl(string.Format("<div class=\"rox-usersection\"><table><tr><td><div class='rox-UserSectionTitle'>{0}</div></td></tr></table><div><table width=\"100%\"><tr><td><div class='rox-UserSectionHead'><label for='{2}'>{1}</label></div><div class='rox-UserSectionBody'><div class='rox-UserControlGroup'>", this["Advanced", new object[0]], this["AdvancedDesc", new object[0]], "")));
                if (ProductPage.Is14)
                {
                    this.Controls.Add(new LiteralControl("<div><br/>"));
                    this.autoCheckBox.Text = this["AutoConnect", new object[0]];
                    this.autoCheckBox.Checked = this.WebPart.AutoConnect;
                    this.Controls.Add(this.autoCheckBox);
                    this.Controls.Add(new LiteralControl("<br/></div>"));
                }
                this.Controls.Add(new LiteralControl("<div>"));
                this.suppressCheckBox.Checked = this.WebPart.SuppressUnknownFilters;
                this.suppressCheckBox.Enabled = this.WebPart.LicEd(4);
                this.suppressCheckBox.Text = this["SuppressUnknownFilters", new object[0]];
                this.Controls.Add(this.suppressCheckBox);
                this.Controls.Add(new LiteralControl("</div><div style=\"display: none;\"><br/>"));
                if (this["DynamicInteractiveFilters", new object[0]].EndsWith("{0}"))
                {
                    this.Controls.Add(new LiteralControl(this["DynamicInteractiveFilters", new object[0]].Substring(0, this["DynamicInteractiveFilters", new object[0]].Length - 3)));
                }
                for (int k = 0; k < 3; k++)
                {
                    this.dynDropDownList.Items.Add(new ListItem(this["Dyn" + k, new object[0]], k.ToString()));
                }
                this.dynDropDownList.AutoPostBack = true;
                this.dynDropDownList.CssClass = "ms-input";
                this.dynDropDownList.Enabled = this.webPart.LicEd(4);
                this.dynDropDownList.SelectedIndex = this.WebPart.DynamicInteractiveFilters;
                this.Controls.Add(this.dynDropDownList);
                if (this["DynamicInteractiveFilters", new object[0]].StartsWith("{0}"))
                {
                    this.Controls.Add(new LiteralControl(this["DynamicInteractiveFilters", new object[0]].Substring(3)));
                }
                this.Controls.Add(new LiteralControl("<br/></div><div>"));
                this.urlSettingsCheckBox.Checked = this.WebPart.UrlSettings;
                this.urlSettingsCheckBox.Enabled = this.WebPart.LicEd(4);
                this.urlSettingsCheckBox.Text = ProductPage.GetResource("UrlSettings", new object[0]);
                this.Controls.Add(this.urlSettingsCheckBox);
                if (ProductPage.Is14)
                {
                    this.Controls.Add(new LiteralControl("</div><br/><b>" + this["Ajax14", new object[0]] + "</b><ul><li>" + this["Ajax14Interval", new object[0]] + " "));
                    this.ajax14TextBox.Style[HtmlTextWriterStyle.Width] = "40px";
                    this.ajax14TextBox.CssClass = "ms-input";
                    this.ajax14TextBox.Text = this.WebPart.ajax14Interval.ToString();
                    this.Controls.Add(this.ajax14TextBox);
                    this.Controls.Add(new LiteralControl(" " + this["Ajax14Interval2", new object[0]] + "</li><li>"));
                    this.ajax14FocusCheckBox.Text = this["Ajax14Focus", new object[0]];
                    this.ajax14FocusCheckBox.Checked = this.WebPart.ajax14focus;
                    this.Controls.Add(this.ajax14FocusCheckBox);
                    this.Controls.Add(new LiteralControl("</li><li>"));
                    this.ajax14CheckBox.Text = this["Ajax14Hide", new object[0]];
                    this.ajax14CheckBox.Checked = this.WebPart.ajax14hide;
                    this.Controls.Add(this.ajax14CheckBox);
                    this.Controls.Add(new LiteralControl("</li></ul><div>"));
                }
                this.Controls.Add(new LiteralControl("</div><div><br/><span id=\"roxmultilabel\">" + this["MultiValueFilter", new object[0]] + "</span>"));
                this.multiDropDownList.AutoPostBack = false;
                this.multiDropDownList.CssClass = "ms-input";
                this.multiDropDownList.Enabled = this.webPart.LicEd(4);
                this.multiDropDownList.ID = "multiDropDownList";
                this.multiDropDownList.Width = new Unit(98.0, UnitType.Percentage);
                this.Controls.Add(this.multiDropDownList);
                this.filterListBox.Attributes["onchange"] = "jQuery('#roxFilterUpButton').css({'visibility':((this.selectedIndex>0)?'visible':'hidden')});jQuery('#roxFilterDownButton').css({'visibility':((this.selectedIndex<(this.options.length-1))?'visible':'hidden')});jQuery('#roxFilterRemoveButton').css({'visibility':(((this.selectedIndex>=0)&&(!" + (this.WebPart._rowConnected ? "true" : "false") + ")&&(jQuery(document.getElementById('" + this.multiDropDownList.ClientID + "').options[document.getElementById('" + this.multiDropDownList.ClientID + "').selectedIndex]).text()!=jQuery(document.getElementById('" + this.filterListBox.ClientID + "').options[document.getElementById('" + this.filterListBox.ClientID + "').selectedIndex]).text()))?'visible':'hidden')});jQuery('#roxFilterEditButton').css({'visibility':((this.selectedIndex>=0)?'visible':'hidden')});";
                this.Controls.Add(new LiteralControl("</div><br/><div>" + ProductPage.GetProductResource("AcSecFields", new object[0]) + "<br/>"));
                this.acSecFieldsTextBox.CssClass = "ms-input";
                this.acSecFieldsTextBox.TextMode = TextBoxMode.MultiLine;
                this.acSecFieldsTextBox.Rows = 2;
                this.acSecFieldsTextBox.Width = new Unit(98.0, UnitType.Percentage);
                this.acSecFieldsTextBox.Wrap = false;
                this.acSecFieldsTextBox.Text = this.WebPart.AcSecFields;
                this.Controls.Add(this.acSecFieldsTextBox);
                this.Controls.Add(new LiteralControl("</div><br/><div>" + ProductPage.GetResource("Jquery", new object[0]) + "<br/>"));
                this.jqueryDropDownList.AutoPostBack = false;
                this.jqueryDropDownList.CssClass = "ms-input";
                this.jqueryDropDownList.Style["width"] = "99%";
                for (int m = 0; m < 3; m++)
                {
                    string resource = ProductPage.GetResource("Jquery_" + m, new object[0]);
                    this.jqueryDropDownList.Items.Add(new ListItem(resource, m.ToString()));
                }
                this.jqueryDropDownList.SelectedIndex = this.WebPart.JQuery;
                this.Controls.Add(this.jqueryDropDownList);
                this.Controls.Add(new LiteralControl("</div></div></div><div class='rox-UserDottedLine'> </div></td></tr></table></div></div>"));
                this.Controls.Add(new LiteralControl(string.Format("<div class=\"rox-usersection\"><table><tr><td><div class='rox-UserSectionTitle'>{0}</div></td></tr></table><div><table width=\"100%\"><tr><td><div class='rox-UserSectionHead'><label for='{2}'>{1}</label></div><div class='rox-UserSectionBody'><div class='rox-UserControlGroup'>", this["Groups", new object[0]], this["GroupsDesc", new object[0]], "")));
                this.groupsTextBox.CssClass = "ms-input";
                this.groupsTextBox.Enabled = this.webPart.LicEd(4);
                this.groupsTextBox.TextMode = TextBoxMode.MultiLine;
                this.groupsTextBox.Rows = (this.groupsTextBox.Text = this.WebPart.Groups).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length + 2;
                this.groupsTextBox.Wrap = false;
                this.groupsTextBox.Width = new Unit(98.0, UnitType.Percentage);
                this.Controls.Add(this.groupsTextBox);
                this.Controls.Add(new LiteralControl("</div></div><div class='rox-UserDottedLine'> </div></td></tr></table></div></div>"));
                this.Controls.Add(new LiteralControl(string.Format("<div class=\"rox-usersection\"><table><tr><td><div class='rox-UserSectionTitle'>{0}</div></td></tr></table><div><table width=\"100%\"><tr><td><div class='rox-UserSectionHead'><label for='{2}'>{1}</label></div><div class='rox-UserSectionBody'><div class='rox-UserControlGroup'>", this["DebugMode", new object[0]], this["DebugModeDesc", new object[0]], "")));
                this.debugOnRadioButton.Text = this["DebugModeOn", new object[0]];
                this.debugOffRadioButton.Text = this["DebugModeOff", new object[0]];
                this.debugOffRadioButton.GroupName = this.debugOnRadioButton.GroupName = "DebugMode";
                this.debugOffRadioButton.Checked = !(this.debugOnRadioButton.Checked = this.WebPart.DebugMode);
                this.debugOnRadioButton.Enabled = this.debugOffRadioButton.Enabled = this.errorOnRadioButton.Enabled = this.errorOffRadioButton.Enabled = this.filterListBox.Enabled = this.filterDropDownList.Enabled = !this.webPart.Exed;
                this.Controls.Add(this.debugOnRadioButton);
                this.Controls.Add(new LiteralControl("<br/>"));
                this.Controls.Add(this.debugOffRadioButton);
                this.Controls.Add(new LiteralControl("</div></div><div class='rox-UserDottedLine'> </div></td></tr></table></div></div>"));
                this.Controls.Add(new LiteralControl(string.Format("<div class=\"rox-usersection\"><table><tr><td><div class='rox-UserSectionTitle'>{0}</div></td></tr></table><div><table width=\"100%\"><tr><td><div class='rox-UserSectionHead'><label for='{2}'>{1}</label></div><div class='rox-UserSectionBody'><div class='rox-UserControlGroup'>", this["ErrorMode", new object[0]], this["ErrorModeDesc", new object[0]], "")));
                this.errorOnRadioButton.Text = this["ErrorModeOn", new object[0]];
                this.errorOffRadioButton.Text = this["ErrorModeOff", new object[0]];
                this.errorOffRadioButton.GroupName = this.errorOnRadioButton.GroupName = "ErrorMode";
                this.errorOffRadioButton.Checked = !(this.errorOnRadioButton.Checked = this.WebPart.ErrorMode);
                this.Controls.Add(this.errorOnRadioButton);
                this.Controls.Add(new LiteralControl("<br/>"));
                this.Controls.Add(this.errorOffRadioButton);
                this.Controls.Add(new LiteralControl("</div></div><div class='rox-UserDottedLine'> </div></td></tr></table></div></div></span>"));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            SPSecurity.CodeToRunElevated code = null;
            string listVal;
            string fieldVal;
            int num = -1;
            int selIndex = this.filterListBox.SelectedIndex;
            bool flag = false;
            SPList parentLib = null;
            if ((this.WebPart != null) && this.WebPart.CanRun)
            {
                if (this.disableDropDownList.Items.Count == 0)
                {
                    this.disableDropDownList.Items.Add(new ListItem(this["DisableFiltersAll", new object[0]], "0"));
                    this.disableDropDownList.Items.Add(new ListItem(this["DisableFiltersSome", new object[0]], "1"));
                    this.disableDropDownList.Items.Add(new ListItem(this["DisableFiltersNone", new object[0]], "2"));
                    this.disableDropDownList.SelectedIndex = !this.WebPart.DisableFilters ? 2 : (this.WebPart.DisableFiltersSome ? 1 : 0);
                }
                foreach (FilterBase base3 in this.Filters)
                {
                    base3.isEditMode = false;
                }
                if (this.filterDropDownList.SelectedIndex > 0)
                {
                    this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery('#roxFilterAddButton').css({ 'visibility': 'visible' }); </script>"));
                }
                if (!string.IsNullOrEmpty(this.Context.Request["roxfilteraction"]) && (((selIndex >= 0) && (selIndex < this.filterListBox.Items.Count)) || (this.Context.Request["roxfilteraction"] == "add")))
                {
                    if (((this.Context.Request["roxfilteraction"] == "add") && !string.IsNullOrEmpty(this.bigAnnoyance = this.Context.Request[this.filterDropDownList.ClientID.Replace("ctl00_", "ctl00$").Replace("MSO_ContentDiv_", "MSO_ContentDiv$").Replace("MSOTlPn_EditorZone_", "MSOTlPn_EditorZone$").Replace("Zone_Edit", "Zone$Edit").Replace("_filterDropDownList", "$filterDropDownList")])) && (!this.bigAnnoyance.StartsWith("____") && !this.bigAnnoyance.StartsWith("_$$_")))
                    {
                        this.Filters.Add(FilterBase.Create(this.bigAnnoyance));
                        this.Filters[this.Filters.Count - 1].isEditMode = true;
                        this.Filters[this.Filters.Count - 1].parentWebPart = this.WebPart;
                        this.hiddenTextBox.Text = FilterBase.Serialize(this.Filters);
                        selIndex = this.Filters.Count - 1;
                        flag = true;
                        this.filterEditorHidden.Value = "1";
                    }
                    else
                    {
                        FilterBase base2;
                        if (this.Context.Request["roxfilteraction"] == "up")
                        {
                            base2 = this.Filters[selIndex];
                            this.Filters.RemoveAt(selIndex);
                            this.Filters.Insert(--selIndex, base2);
                        }
                        else if (this.Context.Request["roxfilteraction"] == "down")
                        {
                            base2 = this.Filters[selIndex];
                            this.Filters.RemoveAt(selIndex);
                            this.Filters.Insert(++selIndex, base2);
                        }
                        else if (this.Context.Request["roxfilteraction"] == "remove")
                        {
                            this.Filters.RemoveAt(selIndex);
                            this.hiddenTextBox.Text = FilterBase.Serialize(this.Filters);
                        }
                        else if (this.Context.Request["roxfilteraction"] == "edit")
                        {
                            flag = true;
                            this.filterEditorHidden.Value = "1";
                            this.Filters[selIndex].isEditMode = true;
                        }
                        else if ((this.Context.Request["roxfilteraction"] == "editstop") || (this.Context.Request["roxfilteraction"] == "editsave"))
                        {
                            flag = false;
                            this.filterEditorHidden.Value = "";
                            if (this.Context.Request["roxfilteraction"] == "editsave")
                            {
                                this.Filters[selIndex].Enabled = this.editorEnabledCheckBox.Checked;
                                this.Filters[selIndex].Name = this.editorNameTextBox.Text;
                                this.Filters[selIndex].isEditMode = true;
                                this.Filters[selIndex].resolve = false;
                                this.Filters[selIndex].UpdateProperties(this.editorPanel);
                                this.Filters[selIndex].resolve = true;
                                this.Filters[selIndex].isEditMode = false;
                                this.hiddenTextBox.Text = FilterBase.Serialize(this.Filters);
                            }
                        }
                    }
                    this.hiddenTextBox.Text = FilterBase.Serialize(this.Filters);
                    if (Array.IndexOf<string>(new string[] { "up", "down", "remove", "add" }, this.Context.Request["roxfilteraction"]) >= 0)
                    {
                        this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery(document).ready(function() { toolFilterAction(''); }); </script>"));
                    }
                }
                if (flag |= this.filterEditorHidden.Value == "1")
                {
                    if (selIndex < 0)
                    {
                        selIndex = this.Filters.Count - 1;
                    }
                    if ((selIndex >= 0) && (selIndex < this.Filters.Count))
                    {
                        if (this.Filters[selIndex].Enabled)
                        {
                            this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> document.getElementById('" + this.editorEnabledCheckBox.ClientID + "').checked = true; </script>"));
                        }
                        this.editorNameTextBox.Text = this.Filters[selIndex].Name;
                        this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> showFilterEditor('" + ((this.filterListBox.SelectedItem == null) ? "" : SPEncode.ScriptEncode(this.filterListBox.SelectedItem.Text.Replace(this["Disabled", new object[0]], string.Empty))) + "'); </script>"));
                        this.Filters[selIndex].isEditMode = true;
                        foreach (FilterBase base4 in this.WebPart.GetFilters(false, false))
                        {
                            if (base4.ID.Equals(this.Filters[selIndex].ID))
                            {
                                base4.isEditMode = true;
                            }
                        }
                        this.Filters[selIndex].resolve = false;
                        this.Filters[selIndex].UpdatePanel(this.editorPanel);
                        this.editFilter = this.Filters[selIndex];
                        this.Filters[selIndex].resolve = true;
                    }
                }
                this.filterListBox.Items.Clear();
                foreach (FilterBase base5 in this.Filters)
                {
                    int num4 = ++num;
                    this.filterListBox.Items.Add(new ListItem(base5.ToString(), num4.ToString()));
                }
                try
                {
                    this.filterListBox.SelectedIndex = selIndex;
                }
                catch
                {
                }
                if ((this.filterListBox.SelectedIndex >= 0) && (this.filterListBox.SelectedIndex < this.filterListBox.Items.Count))
                {
                    this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery('#roxFilterRemoveButton').css({ 'visibility': ((jQuery(document.getElementById('" + this.multiDropDownList.ClientID + "').options[document.getElementById('" + this.multiDropDownList.ClientID + "').selectedIndex]).text() == '" + this.filterListBox.SelectedItem.Text.Replace("'", @"\'") + "') ? 'hidden' : '" + (this.WebPart._rowConnected ? "hidden" : "visible") + "') }); jQuery('#roxFilterEditButton').css({ 'visibility': 'visible' }); </script>"));
                    if (this.filterListBox.SelectedIndex > 0)
                    {
                        this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery('#roxFilterUpButton').css({ 'visibility': 'visible' }); </script>"));
                    }
                    if (this.filterListBox.SelectedIndex < (this.filterListBox.Items.Count - 1))
                    {
                        this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery('#roxFilterDownButton').css({ 'visibility': 'visible' }); </script>"));
                    }
                }
                if (e != null)
                {
                    base.OnLoad(e);
                }
                this.groupsTextBox.Rows = this.groupsTextBox.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length + 2;
                listVal = (this.multiDropDownList.Items.Count == 0) ? this.WebPart.MultiValueFilterID : this.multiDropDownList.SelectedValue;
                this.multiDropDownList.Items.Clear();
                foreach (FilterBase base6 in this.Filters)
                {
                    if ((base6.Enabled && base6.SupportsMultipleValues) && !(base6 is roxority_FilterZen.FilterBase.Lookup.Multi))
                    {
                        if (this.multiDropDownList.Items.Count == 0)
                        {
                            this.multiDropDownList.Items.Add(new ListItem(this["FilterNone", new object[] { "'" + base6.Name + "'" }], string.Empty));
                        }
                        this.multiDropDownList.Items.Add(new ListItem(base6.ToString(), base6.ID));
                    }
                }
                if (this.multiDropDownList.Items.Count == 0)
                {
                    this.multiDropDownList.Items.Add(new ListItem(this["FilterNone", new object[] { this["FilterNoneNone", new object[0]] }], string.Empty));
                }
                try
                {
                    this.multiDropDownList.SelectedValue = listVal;
                }
                catch
                {
                    this.multiDropDownList.SelectedIndex = 0;
                }
                if (code == null)
                {
                    code = delegate {
                        listVal = this.wizardListDropDownList.SelectedValue;
                        fieldVal = this.wizardFieldDropDownList.SelectedValue;
                        this.wizardListDropDownList.Items.Clear();
                        this.wizardFieldDropDownList.Items.Clear();
                        this.wizardListDropDownList.Items.Add(new ListItem(this["WizardList", new object[0]], ""));
                        this.wizardFieldDropDownList.Items.Add(new ListItem(this["WizardField", new object[0]], ""));
                        this.wizardListDropDownList.SelectedIndex = this.wizardFieldDropDownList.SelectedIndex = 0;
                        if (string.IsNullOrEmpty(this.wizardTextBox.Text.Trim()))
                        {
                            this.wizardTextBox.Text = SPContext.Current.Web.Url;
                        }
                        try
                        {
                            using (SPSite site = new SPSite(this.wizardTextBox.Text.Trim()))
                            {
                                using (SPWeb web = site.OpenWeb())
                                {
                                    foreach (SPList list in ProductPage.TryEach<SPList>(web.Lists))
                                    {
                                        this.wizardListDropDownList.Items.Add(new ListItem(list.Title, list.ID.ToString()));
                                    }
                                    if (!string.IsNullOrEmpty(listVal))
                                    {
                                        try
                                        {
                                            this.wizardListDropDownList.SelectedValue = listVal;
                                            goto Label_02A3;
                                        }
                                        catch
                                        {
                                            this.wizardListDropDownList.SelectedIndex = 0;
                                            goto Label_02A3;
                                        }
                                    }
                                    if (((selIndex >= 0) && (selIndex < this.Filters.Count)) && (this.Filters[selIndex] is FilterBase.Lookup.Multi.PageField))
                                    {
                                        try
                                        {
                                            using (SPWrap<SPList> wrap = ((FilterBase.Interactive) this.Filters[selIndex]).GetList("ListUrl", true))
                                            {
                                                parentLib = wrap.Value;
                                                if (parentLib != null)
                                                {
                                                    this.wizardListDropDownList.SelectedValue = parentLib.ID.ToString();
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                Label_02A3:
                                    if (this.wizardListDropDownList.SelectedIndex > 0)
                                    {
                                        foreach (SPField field in ProductPage.TryEach<SPField>(web.Lists[new Guid(this.wizardListDropDownList.SelectedValue)].Fields))
                                        {
                                            this.wizardFieldDropDownList.Items.Add(new ListItem(field.Title, field.InternalName));
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(fieldVal))
                                    {
                                        try
                                        {
                                            this.wizardFieldDropDownList.SelectedValue = fieldVal;
                                        }
                                        catch
                                        {
                                            this.wizardFieldDropDownList.SelectedIndex = 0;
                                        }
                                    }
                                    this.wizardLabel.Text = string.IsNullOrEmpty(this.wizardFieldDropDownList.SelectedValue) ? (string.IsNullOrEmpty(this.wizardListDropDownList.SelectedValue) ? this["WizardLabel", new object[0]] : this["WizardUrl", new object[] { ProductPage.MergeUrlPaths(web.Url, web.Lists[new Guid(this.wizardListDropDownList.SelectedValue)].DefaultViewUrl) }]) : string.Format(this["WizardName", new object[0]], this.wizardFieldDropDownList.SelectedValue, this.wizardFieldDropDownList.ClientID);
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            this.wizardLabel.Text = exception.Message;
                        }
                    };
                }
                ProductPage.Elevate(code, true);
            }
            else
            {
                base.OnLoad(e);
            }
        }

        protected override void RenderToolPart(HtmlTextWriter output)
        {
            int selectedIndex = this.nameDropDownList.SelectedIndex;
            Dictionary<KeyValuePair<string, string>, FilterBase.Interactive> dictionary = new Dictionary<KeyValuePair<string, string>, FilterBase.Interactive>();
            if (!ProductPage.isEnabled)
            {
                using (SPSite site = ProductPage.GetAdminSite())
                {
                    output.Write("<div class=\"rox-infobox\" id=\"{2}\" style=\"background-image: url('/_layouts/images/{1}');\">{0}</div>", ProductPage.GetResource("NotEnabled", new object[] { ProductPage.MergeUrlPaths(site.Url, "/_layouts/roxority_FilterZen/default.aspx?cfg=enable"), "FilterZen" }), "servicenotinstalled.gif", "noid");
                }
            }
            else
            {
                if (this.WebPart != null)
                {
                    this.WebPart.RenderScripts(output, this.WebUrl);
                    if (!(this.dynDropDownList.Enabled = this.suppressCheckBox.Enabled = this.WebPart.LicEd(4) && (this.WebPart.validFilterNames.Count > 0)))
                    {
                        this.suppressCheckBox.Checked = false;
                        this.dynDropDownList.SelectedIndex = 0;
                    }
                    int num2 = 1;
                    if (num2.Equals(this.filterListBox.Rows = this.filterListBox.Items.Count + 1))
                    {
                        this.filterListBox.Style["display"] = "none";
                    }
                    else
                    {
                        this.filterListBox.Style.Remove("display");
                    }
                    if (this.InteractiveCount != 0)
                    {
                        output.Write("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery(document).ready(function() { document.getElementById('roxifilters').style.display = 'block'; }); </script>");
                    }
                    this.nameDropDownList.Items.Clear();
                    this.nameDropDownList.Items.Add(new ListItem(this["NameSelector", new object[0]], string.Empty));
                    using (List<KeyValuePair<string, string>>.Enumerator enumerator = this.WebPart.validFilterNames.GetEnumerator())
                    {
                        Predicate<FilterBase> match = null;
                        KeyValuePair<string, string> kvp;
                        while (enumerator.MoveNext())
                        {
                            kvp = enumerator.Current;
                            this.nameDropDownList.Items.Add(new ListItem(kvp.Value, kvp.Key));
                            if (match == null)
                            {
                                match = fb => fb.Name == kvp.Key;
                            }
                            if (!this.Filters.Exists(match))
                            {
                                dictionary[kvp] = this.WebPart.CreateDynamicInteractiveFilter(kvp);
                            }
                        }
                    }
                    try
                    {
                        this.nameDropDownList.SelectedIndex = selectedIndex;
                    }
                    catch
                    {
                    }
                    selectedIndex = 0;
                    this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\">\n"));
                    foreach (System.Type type in FilterBase.FilterTypes)
                    {
                        selectedIndex++;
                        foreach (KeyValuePair<KeyValuePair<string, string>, FilterBase.Interactive> pair in dictionary)
                        {
                            if ((pair.Value != null) && (type == pair.Value.GetType()))
                            {
                                selectedIndex++;
                                this.filterDropDownList.Items.Insert(selectedIndex, new ListItem(this["FilterFly" + (pair.Key.Key.Equals(pair.Key.Value) ? "1" : string.Empty), new object[] { pair.Key.Value, pair.Key.Key }], "____" + pair.Key.Key));
                                this.filterDropDownList.Items[selectedIndex].Attributes["style"] = "color: #666;";
                                this.Controls.Add(new LiteralControl("roxFilterDescs['____" + pair.Key.Key + "'] = '" + SPEncode.ScriptEncode(this["FilterFlyDesc", new object[] { FilterBase.GetFilterTypeTitle(type) }]) + "';\n"));
                            }
                            else if ((type == typeof(FilterBase.Lookup)) && (this.webPart.connectedList != null))
                            {
                                selectedIndex++;
                                this.filterDropDownList.Items.Insert(selectedIndex, new ListItem(this["FilterFly" + (pair.Key.Key.Equals(pair.Key.Value) ? "1" : string.Empty), new object[] { pair.Key.Value, pair.Key.Key }], "_$$_" + pair.Key.Key));
                                this.filterDropDownList.Items[selectedIndex].Attributes["style"] = "color: #666;";
                                this.Controls.Add(new LiteralControl("roxFilterDescs['_$$_" + pair.Key.Key + "'] = '" + SPEncode.ScriptEncode(this["FilterFlyDesc", new object[] { FilterBase.GetFilterTypeTitle(type) }]) + "';\n"));
                            }
                        }
                    }
                    this.Controls.Add(new LiteralControl("</script>\n"));
                    if ((this.bigAnnoyance != null) && (this.bigAnnoyance.StartsWith("____") || this.bigAnnoyance.StartsWith("_$$_")))
                    {
                        foreach (FilterBase base2 in this.Filters)
                        {
                            base2.isEditMode = false;
                        }
                        this.Filters.Add(this.WebPart.CreateDynamicInteractiveFilter(this.bigAnnoyance.Substring(4), this.bigAnnoyance.StartsWith("_$$_")));
                        this.Filters[this.Filters.Count - 1].isEditMode = true;
                        this.Filters[this.Filters.Count - 1].parentWebPart = this.WebPart;
                        this.hiddenTextBox.Text = FilterBase.Serialize(this.Filters);
                        this.filterEditorHidden.Value = "1";
                        this.hiddenTextBox.Text = FilterBase.Serialize(this.Filters);
                        int num4 = this.Filters.Count - 1;
                        this.filterListBox.Items.Add(new ListItem(this.Filters[this.Filters.Count - 1].ToString(), num4.ToString()));
                        this.filterListBox.SelectedIndex = this.filterListBox.Items.Count - 1;
                        this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"JavaScript\"> showFilterEditor('" + ((this.filterListBox.Items.Count == 0) ? "" : SPEncode.ScriptEncode(this.filterListBox.Items[this.filterListBox.Items.Count - 1].Text.Replace(this["Disabled", new object[0]], string.Empty))) + "'); </script>"));
                        this.Filters[this.Filters.Count - 1].isEditMode = true;
                        foreach (FilterBase base3 in this.WebPart.GetFilters(false, false))
                        {
                            if (base3.ID.Equals(this.Filters[this.Filters.Count - 1].ID))
                            {
                                base3.isEditMode = true;
                            }
                        }
                        this.Filters[this.Filters.Count - 1].resolve = false;
                        this.Filters[this.Filters.Count - 1].UpdatePanel(this.editorPanel);
                        this.editFilter = null;
                        this.Filters[this.Filters.Count - 1].resolve = true;
                    }
                    if ((this.editFilter != null) && this.editFilter.requirePostLoadRendering)
                    {
                        this.editFilter.isEditMode = true;
                        this.editFilter.parentWebPart = this.WebPart;
                        this.editFilter.resolve = false;
                        this.editorPanel.Controls.Clear();
                        this.editFilter.UpdatePanel(this.editorPanel);
                        this.editFilter.resolve = true;
                        this.editFilter.isEditMode = false;
                    }
                    if (this.WebPart._rowConnected)
                    {
                        output.Write("<style type=\"text/css\"> .rox-proplimited { display: none !important; } </style>");
                        output.Write("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery(document).ready(function() { jQuery('#roxFilterRemoveButton').css({ 'visibility': 'hidden' }); }); </script>");
                    }
                    if (this.WebPart.transform != null)
                    {
                        output.Write("<script type=\"text/javascript\" language=\"JavaScript\"> jQuery(document).ready(function() { document.getElementById('" + this.multiDropDownList.ClientID + "').style.display = 'none'; jQuery('#roxmultilabel').html(jQuery('#roxmultilabel').html() + '&nbsp;<b>" + this.WebPart.transform.Filter.ToString() + "</b>'); }); </script>");
                        if (!this.WebPart._rowConnected)
                        {
                            foreach (FilterBase base4 in this.Filters)
                            {
                                if (base4.isEditMode && base4.ID.Equals(this.WebPart.transform.Filter.ID))
                                {
                                    output.Write("<style type=\"text/css\"> .rox-proplimited { display: none !important; } </style>");
                                }
                                break;
                            }
                        }
                    }
                }
                output.Write("<div class=\"rox-toolpart\">");
                base.RenderToolPart(output);
                output.Write("</div>");
            }
        }

        public List<FilterBase> Filters
        {
            get
            {
                if (this.filters == null)
                {
                    if (!string.IsNullOrEmpty(this.hiddenTextBox.Text))
                    {
                        this.filters = FilterBase.Deserialize(this.WebPart, this.hiddenTextBox.Text);
                    }
                    else if (this.WebPart != null)
                    {
                        this.filters = this.WebPart.GetFilters(false, false);
                        this.hiddenTextBox.Text = FilterBase.Serialize(this.filters);
                    }
                }
                return this.filters;
            }
        }

        public int InteractiveCount
        {
            get
            {
                int num = (this.dynDropDownList.Enabled && (this.dynDropDownList.SelectedIndex > 0)) ? -1 : 0;
                if (num == 0)
                {
                    foreach (FilterBase base2 in string.IsNullOrEmpty(this.hiddenTextBox.Text) ? this.WebPart.GetFilters(false, false) : this.Filters)
                    {
                        FilterBase.Interactive interactive;
                        if ((((interactive = base2 as FilterBase.Interactive) != null) && interactive.Enabled) && interactive.IsInteractive)
                        {
                            num++;
                        }
                    }
                }
                return num;
            }
        }

        public string this[string resKey, object[] args]
        {
            get
            {
                return ProductPage.GetProductResource(resKey, args);
            }
        }

        public int ListPartCount
        {
            get
            {
                int num = 0;
                foreach (Microsoft.SharePoint.WebPartPages.WebPart part in this.WebPart.connectedParts)
                {
                    if (part is ListViewWebPart)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public roxority_FilterWebPart WebPart
        {
            get
            {
                if ((this.webPart == null) && (base.ParentToolPane != null))
                {
                    this.webPart = base.ParentToolPane.SelectedWebPart as roxority_FilterWebPart;
                }
                return this.webPart;
            }
        }

        public string WebUrl
        {
            get
            {
                if (this.webUrl == null)
                {
                    try
                    {
                        this.webUrl = SPContext.Current.Web.Url.TrimEnd(new char[] { '/' });
                    }
                    catch
                    {
                        this.webUrl = string.Empty;
                    }
                }
                return this.webUrl;
            }
        }
    }
}

