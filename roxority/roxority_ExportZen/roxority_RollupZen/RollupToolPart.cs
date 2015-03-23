namespace roxority_RollupZen
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.WebPartPages;
    using roxority.Data;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;

    internal class RollupToolPart : ToolPart
    {
        internal DropDownList animDropDownList = new DropDownList();
        internal CheckBox curUserCheckBox = new CheckBox();
        internal DropDownList dataSourceDropDownList = new DropDownList();
        internal TextBox dateIntervalTextBox = new TextBox();
        internal CheckBox dateNoDayCheckBox = new CheckBox();
        internal TextBox datePropTextBox = new TextBox();
        internal CheckBox dateThisYearCheckBox = new CheckBox();
        internal DropDownList expDropDownList = new DropDownList();
        internal CheckBox filterLiveCheckBox = new CheckBox();
        internal RadioButton groupAscRadioButton = new RadioButton();
        internal CheckBox groupByCountsCheckBox = new CheckBox();
        internal RadioButton groupDescRadioButton = new RadioButton();
        internal DropDownList groupDropDownList = new DropDownList();
        internal CheckBox groupIntCheckBox = new CheckBox();
        internal CheckBox groupIntDirCheckBox = new CheckBox();
        internal CheckBox groupShowCountsCheckBox = new CheckBox();
        internal TextBox imageHeightTextBox = new TextBox();
        internal DropDownList jqueryDropDownList = new DropDownList();
        private string knownPropID;
        private SortedDictionary<string, string> knownProps;
        internal DropDownList nameDropDownList = new DropDownList();
        internal DropDownList pageDropDownList = new DropDownList();
        internal TextBox pageSizeTextBox = new TextBox();
        internal DropDownList pageSkipDropDownList = new DropDownList();
        internal DropDownList pageStepDropDownList = new DropDownList();
        internal DropDownList pictDropDownList = new DropDownList();
        internal const string PREFIX_CONTROL = "\r\n<div>\r\n<table><tr><td><div class=\"rox-UserSectionTitle\">{0}</div></td></tr></table>\r\n<div>\r\n<table><tr><td>\r\n<div class=\"rox-UserSectionHead\"><label for=\"{2}\">{1}</label></div>\r\n<div class=\"rox-UserSectionBody\"><div class=\"rox-UserControlGroup\">\r\n";
        internal const string PREFIX_CONTROL_HLP = "\r\n<div>\r\n<table><tr><td><div class=\"rox-UserSectionTitle\">{0}<a target=\"_blank\" href=\"/_layouts/{3}/default.aspx?doc={4}\"><img src=\"/_layouts/images/hhelp.gif\"/></a></div></td></tr></table>\r\n<div>\r\n<table><tr><td>\r\n<div class=\"rox-UserSectionHead\"><label for=\"{2}\">{1}</label></div>\r\n<div class=\"rox-UserSectionBody\"><div class=\"rox-UserControlGroup\">\r\n";
        internal CheckBox presenceCheckBox = new CheckBox();
        internal DropDownList printDropDownList = new DropDownList();
        internal DropDownList propDropDownList = new DropDownList();
        internal TextBox propsTextBox = new TextBox();
        internal static readonly Random rnd = new Random();
        internal TextBox rowSizeTextBox = new TextBox();
        internal const string SELEMPTY = "e58b8e83-784f-4fab-943a-8552e4aa7032";
        internal RadioButton sortAscRadioButton = new RadioButton();
        internal CheckBox sortCheckBox = new CheckBox();
        internal RadioButton sortDescRadioButton = new RadioButton();
        internal DropDownList sortDropDownList = new DropDownList();
        internal RadioButton styleClassicRadioButton = new RadioButton();
        internal RadioButton styleListRadioButton = new RadioButton();
        internal const string SUFFIX_CONTROL = "\r\n</div></div>\r\n<div class=\"rox-UserDottedLine\"> </div>\r\n</td></tr></table>\r\n</div>\r\n</div>\r\n";
        internal CheckBox tabCheckBox = new CheckBox();
        internal DropDownList tabDropDownList = new DropDownList();
        internal TextBox tileTextBox = new TextBox();
        internal CheckBox urlSettingsCheckBox = new CheckBox();
        internal CheckBox vcardCheckBox = new CheckBox();
        internal CheckBox viewCheckBox = new CheckBox();

        public RollupToolPart()
        {
            this.Title = ProductPage.GetProductResource("WebPart_DefaultTitle", new object[0]);
            this.ChromeState = PartChromeState.Normal;
            this.Visible = true;
        }

        public override void ApplyChanges()
        {
            roxority_RollupZen.RollupWebPart selectedWebPart = base.ParentToolPane.SelectedWebPart as roxority_RollupZen.RollupWebPart;
            if (selectedWebPart != null)
            {
                int num;
                selectedWebPart.forceReload = true;
                selectedWebPart.textArea.Text = string.Empty;
                if (int.TryParse(this.rowSizeTextBox.Text.Trim(), out num))
                {
                    selectedWebPart.RowSize = num;
                }
                else
                {
                    selectedWebPart.RowSize = 2;
                }
                selectedWebPart.GroupDesc = this.groupDescRadioButton.Checked;
                selectedWebPart.GroupByCounts = this.groupByCountsCheckBox.Checked;
                selectedWebPart.GroupShowCounts = this.groupShowCountsCheckBox.Checked;
                selectedWebPart.GroupInteractive = this.groupIntCheckBox.Checked;
                selectedWebPart.TabInteractive = this.tabCheckBox.Checked;
                selectedWebPart.GroupInteractiveDir = this.groupIntDirCheckBox.Checked;
                selectedWebPart.GroupProp = "e58b8e83-784f-4fab-943a-8552e4aa7032".Equals(this.groupDropDownList.SelectedValue) ? string.Empty : this.groupDropDownList.SelectedValue;
                selectedWebPart.TabProp = "e58b8e83-784f-4fab-943a-8552e4aa7032".Equals(this.tabDropDownList.SelectedValue) ? string.Empty : this.tabDropDownList.SelectedValue;
                selectedWebPart.SortDesc = this.sortDescRadioButton.Checked;
                selectedWebPart.SortProp = "e58b8e83-784f-4fab-943a-8552e4aa7032".Equals(this.sortDropDownList.SelectedValue) ? string.Empty : this.sortDropDownList.SelectedValue;
                selectedWebPart.TileWidth = this.tileTextBox.Text;
                selectedWebPart.message = string.Empty;
                selectedWebPart.JQuery = this.jqueryDropDownList.SelectedIndex;
                selectedWebPart.DateThisYear = this.dateThisYearCheckBox.Checked;
                selectedWebPart.DateIgnoreDay = this.dateNoDayCheckBox.Checked;
                selectedWebPart.filterLive = this.filterLiveCheckBox.Enabled && this.filterLiveCheckBox.Checked;
                if (int.TryParse(this.pageSizeTextBox.Text, out num))
                {
                    selectedWebPart.PageSize = num;
                }
                selectedWebPart.ImageHeight = int.TryParse(this.imageHeightTextBox.Text, out num) ? num : 0;
                selectedWebPart.Properties = this.propsTextBox.Text;
                selectedWebPart.PageMode = this.pageDropDownList.SelectedIndex;
                selectedWebPart.PageSkipMode = this.pageSkipDropDownList.SelectedIndex;
                selectedWebPart.PageStepMode = this.pageStepDropDownList.SelectedIndex;
                selectedWebPart.LoaderAnim = this.animDropDownList.SelectedValue;
                selectedWebPart.NameMode = this.nameDropDownList.SelectedIndex;
                selectedWebPart.PictMode = this.pictDropDownList.SelectedIndex;
                selectedWebPart.ListStyle = this.styleListRadioButton.Checked;
                selectedWebPart.AllowSort = this.sortCheckBox.Checked;
                selectedWebPart.AllowView = this.viewCheckBox.Checked;
                selectedWebPart.UrlSettings = this.urlSettingsCheckBox.Checked;
                selectedWebPart.PrintAction = this.printDropDownList.SelectedValue + string.Empty;
                selectedWebPart.DataSourceID = this.dataSourceDropDownList.SelectedValue + string.Empty;
                selectedWebPart.ExportAction = this.expDropDownList.SelectedValue + string.Empty;
            }
            base.ApplyChanges();
        }

        protected override void CreateChildControls()
        {
            List<string> list = new List<string>();
            roxority_RollupZen.RollupWebPart selectedWebPart = base.ParentToolPane.SelectedWebPart as roxority_RollupZen.RollupWebPart;
            if (selectedWebPart != null)
            {
                foreach (string str2 in selectedWebPart.Properties.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] strArray;
                    if (((strArray = str2.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)) != null) && (strArray.Length >= 2))
                    {
                        list.Add("rox-rollupitem-" + strArray[0].Trim().ToLowerInvariant());
                    }
                }
            }
            base.CreateChildControls();
            ProductPage.CreateLicControls(this.Controls, "\r\n<div>\r\n<table><tr><td><div class=\"rox-UserSectionTitle\">{0}</div></td></tr></table>\r\n<div>\r\n<table><tr><td>\r\n<div class=\"rox-UserSectionHead\"><label for=\"{2}\">{1}</label></div>\r\n<div class=\"rox-UserSectionBody\"><div class=\"rox-UserControlGroup\">\r\n".Replace("UserSectionTitle\"", "UserSectionTitle\" style=\"font-weight: normal;\""), "\r\n</div></div>\r\n<div class=\"rox-UserDottedLine\"> </div>\r\n</td></tr></table>\r\n</div>\r\n</div>\r\n");
            this.Controls.Add(new LiteralControl(string.Format("\r\n<div>\r\n<table><tr><td><div class=\"rox-UserSectionTitle\">{0}</div></td></tr></table>\r\n<div>\r\n<table><tr><td>\r\n<div class=\"rox-UserSectionHead\"><label for=\"{2}\">{1}</label></div>\r\n<div class=\"rox-UserSectionBody\"><div class=\"rox-UserControlGroup\">\r\n", Res("DataSourceMode", new object[0]), Res("DataSourceModeDesc", new object[0]), string.Empty)));
            this.Controls.Add(new LiteralControl("<div>"));
            this.dataSourceDropDownList.AutoPostBack = true;
            this.Controls.Add(this.dataSourceDropDownList);
            string[] strArray3 = new string[8];
            strArray3[0] = "</div><div><a target=\"_blank\" href=\"";
            strArray3[1] = SPContext.Current.Web.Url;
            strArray3[2] = "/_layouts/";
            strArray3[3] = ProductPage.AssemblyName;
            strArray3[4] = "/default.aspx?cfg=tools&tool=Tool_DataSources\">";
            strArray3[5] = Res("Tool_ItemEditor_DefaultDesc2", new object[0]);
            string str = Res("Tool_ItemEditor_DefaultDesc", new object[0]);
            strArray3[6] = string.Format(str.Substring(str.IndexOf('.') + 2), string.Empty, ProductPage.GetTitle(), Res("Tool_DataSources_Title", new object[0]));
            strArray3[7] = "</a></div>";
            this.Controls.Add(new LiteralControl(string.Concat(strArray3)));
            this.Controls.Add(new LiteralControl("\r\n</div></div>\r\n<div class=\"rox-UserDottedLine\"> </div>\r\n</td></tr></table>\r\n</div>\r\n</div>\r\n"));
            this.Controls.Add(new LiteralControl(string.Format("\r\n<div>\r\n<table><tr><td><div class=\"rox-UserSectionTitle\">{0}<a target=\"_blank\" href=\"/_layouts/{3}/default.aspx?doc={4}\"><img src=\"/_layouts/images/hhelp.gif\"/></a></div></td></tr></table>\r\n<div>\r\n<table><tr><td>\r\n<div class=\"rox-UserSectionHead\"><label for=\"{2}\">{1}</label></div>\r\n<div class=\"rox-UserSectionBody\"><div class=\"rox-UserControlGroup\">\r\n", new object[] { Res("Style", new object[0]), Res("StyleDesc", new object[0]), string.Empty, ProductPage.AssemblyName, "user_profiles_web_part_display_style" })));
            this.Controls.Add(new LiteralControl("<div>"));
            this.styleClassicRadioButton.GroupName = "PeopleStyle";
            this.styleClassicRadioButton.ID = "styleClassicRadioButton";
            this.Controls.Add(this.styleClassicRadioButton);
            this.Controls.Add(new LiteralControl(string.Format("<label for=\"{0}\">" + Res("StyleClassic", new object[0]) + "</label></div>", this.styleClassicRadioButton.ClientID)));
            this.Controls.Add(new LiteralControl("<div style=\"padding-left: 28px;\">" + Res("RowSize1", new object[0]) + " "));
            this.rowSizeTextBox.CssClass = "ms-input";
            this.rowSizeTextBox.Attributes["onchange"] = "if(!roxRowInfoShown){roxRowInfoShown=true;jQuery('#roxrowsizeinfo').show();}";
            this.rowSizeTextBox.Attributes["onfocus"] = "jQuery('#roxpeoprowinfo').show();";
            this.rowSizeTextBox.Attributes["onblur"] = "jQuery('#roxpeoprowinfo').hide();";
            this.rowSizeTextBox.Style["width"] = "24px";
            this.rowSizeTextBox.Style["text-align"] = "center";
            this.Controls.Add(this.rowSizeTextBox);
            this.Controls.Add(new LiteralControl(Res("TileWidth1", new object[0]) + " "));
            this.tileTextBox.CssClass = "ms-input";
            this.tileTextBox.Style["width"] = "40px";
            this.tileTextBox.Style["text-align"] = "center";
            this.Controls.Add(this.tileTextBox);
            this.Controls.Add(new LiteralControl(" " + Res("TileWidth2", new object[0]) + "<div id=\"roxpeoprowinfo\" style=\"display: none;\">" + Res("RowSize2", new object[0]) + "</div></div><div style=\"display: none;\" id=\"roxrowsizeinfo\" class=\"rox-error\">" + Res("RowSize3", new object[0]) + "</div>"));
            this.Controls.Add(new LiteralControl("<div>"));
            this.styleListRadioButton.GroupName = "PeopleStyle";
            this.styleListRadioButton.ID = "styleListRadioButton";
            this.Controls.Add(this.styleListRadioButton);
            this.Controls.Add(new LiteralControl(string.Format("<label for=\"{0}\">" + Res("StyleList", new object[0]) + "</label></div>", this.styleListRadioButton.ClientID)));
            this.Controls.Add(new LiteralControl("<div style=\"padding-left: 28px;\">"));
            this.Controls.Add(this.sortCheckBox);
            this.Controls.Add(new LiteralControl(string.Format("<label for=\"{0}\">" + Res("AllowSort", new object[0]) + "</label></div>", this.sortCheckBox.ClientID)));
            this.Controls.Add(new LiteralControl("<div>"));
            this.Controls.Add(this.viewCheckBox);
            this.Controls.Add(new LiteralControl(string.Format("<label for=\"{0}\">" + Res("AllowView", new object[0]) + "</label></div>", this.viewCheckBox.ClientID)));
            this.Controls.Add(new LiteralControl(ProductPage.GetResource("Css", new object[] { ProductPage.AssemblyName })));
            this.Controls.Add(new LiteralControl("<br/>" + ProductPage.GetProductResource("HtmlInfo", new object[0])));
            this.Controls.Add(new LiteralControl("\r\n</div></div>\r\n<div class=\"rox-UserDottedLine\"> </div>\r\n</td></tr></table>\r\n</div>\r\n</div>\r\n"));
            this.Controls.Add(new LiteralControl(string.Format("\r\n<div>\r\n<table><tr><td><div class=\"rox-UserSectionTitle\">{0}<a target=\"_blank\" href=\"/_layouts/{3}/default.aspx?doc={4}\"><img src=\"/_layouts/images/hhelp.gif\"/></a></div></td></tr></table>\r\n<div>\r\n<table><tr><td>\r\n<div class=\"rox-UserSectionHead\"><label for=\"{2}\">{1}</label></div>\r\n<div class=\"rox-UserSectionBody\"><div class=\"rox-UserControlGroup\">\r\n", new object[] { Res("T_Prop11" + (roxority_RollupZen.RollupWebPart.IsRollupZen ? "R" : string.Empty), new object[0]), Res("D_Prop11" + (roxority_RollupZen.RollupWebPart.IsRollupZen ? "R" : string.Empty), new object[] { selectedWebPart.ID }), this.propsTextBox.ClientID, ProductPage.AssemblyName, "web_part_user_profile_properties" })));
            this.propsTextBox.ID = "propsTextBox";
            this.propsTextBox.TextMode = TextBoxMode.MultiLine;
            this.propsTextBox.Rows = 2;
            this.propsTextBox.Wrap = false;
            this.propsTextBox.Width = new Unit(99.0, UnitType.Percentage);
            this.Controls.Add(this.propsTextBox);
            this.Controls.Add(new LiteralControl("<div>" + ProductPage.GetResource("Tool_ItemEditor_DataFields_LabelAdd", new object[0]) + " "));
            this.propDropDownList.Attributes["onchange"] = "if(this.selectedIndex>0){document.getElementById('" + this.propsTextBox.ClientID + @"').innerText+=('\n'+this.options[this.selectedIndex].value+': '+this.options[this.selectedIndex].innerText);roxScrollEnd(document.getElementById('" + this.propsTextBox.ClientID + "'));}this.selectedIndex=0;";
            this.Controls.Add(this.propDropDownList);
            this.Controls.Add(new LiteralControl("</select></div>"));
            this.Controls.Add(new LiteralControl("<div>" + ProductPage.GetProductResource("Old_SortInfo", new object[0]) + "</div>"));
            this.Controls.Add(new LiteralControl("\r\n</div></div>\r\n<div class=\"rox-UserDottedLine\"> </div>\r\n</td></tr></table>\r\n</div>\r\n</div>\r\n"));
            this.Controls.Add(new LiteralControl(string.Format("\r\n<div>\r\n<table><tr><td><div class=\"rox-UserSectionTitle\">{0}<a target=\"_blank\" href=\"/_layouts/{3}/default.aspx?doc={4}\"><img src=\"/_layouts/images/hhelp.gif\"/></a></div></td></tr></table>\r\n<div>\r\n<table><tr><td>\r\n<div class=\"rox-UserSectionHead\"><label for=\"{2}\">{1}</label></div>\r\n<div class=\"rox-UserSectionBody\"><div class=\"rox-UserControlGroup\">\r\n", new object[] { Res("T_Prop5", new object[0]), Res("D_Prop5", new object[0]), string.Empty, ProductPage.AssemblyName, "user_profiles_web_part_filtering_tabbing_search" })));
            this.Controls.Add(new LiteralControl("<br/><div>" + Res("TabBy", new object[0]) + "<br/>"));
            this.Controls.Add(this.tabDropDownList);
            this.Controls.Add(new LiteralControl("</div><div>"));
            this.tabCheckBox.Text = Res("Retab", new object[0]);
            this.Controls.Add(this.tabCheckBox);
            this.Controls.Add(new LiteralControl("</div>" + Res(roxority_RollupZen.RollupWebPart.IsRollupZen ? "PreFilterHint" : "FilterExclude", new object[0]) + "<br/><div>"));
            this.Controls.Add(this.dateThisYearCheckBox);
            this.Controls.Add(new LiteralControl(string.Format("<label for=\"{0}\">" + Res("T_Prop10", new object[0]) + "</label>", this.dateThisYearCheckBox.ClientID)));
            this.Controls.Add(new LiteralControl("</div><div>"));
            this.Controls.Add(this.dateNoDayCheckBox);
            this.Controls.Add(new LiteralControl(string.Format("<label for=\"{0}\">" + Res("IgnoreDay", new object[0]) + "</label>", this.dateNoDayCheckBox.ClientID)));
            this.Controls.Add(new LiteralControl("</div><div>"));
            this.Controls.Add(this.filterLiveCheckBox);
            this.Controls.Add(new LiteralControl(string.Format("<label for=\"{0}\">" + Res("FilterLive", new object[0]) + "</label></div>", this.filterLiveCheckBox.ClientID)));
            this.Controls.Add(new LiteralControl("\r\n</div></div>\r\n<div class=\"rox-UserDottedLine\"> </div>\r\n</td></tr></table>\r\n</div>\r\n</div>\r\n"));
            this.Controls.Add(new LiteralControl(string.Format("\r\n<div>\r\n<table><tr><td><div class=\"rox-UserSectionTitle\">{0}<a target=\"_blank\" href=\"/_layouts/{3}/default.aspx?doc={4}\"><img src=\"/_layouts/images/hhelp.gif\"/></a></div></td></tr></table>\r\n<div>\r\n<table><tr><td>\r\n<div class=\"rox-UserSectionHead\"><label for=\"{2}\">{1}</label></div>\r\n<div class=\"rox-UserSectionBody\"><div class=\"rox-UserControlGroup\">\r\n", new object[] { Res("GroupSort", new object[0]), Res("GroupSortInfo" + (roxority_RollupZen.RollupWebPart.IsRollupZen ? "R" : string.Empty), new object[0]), string.Empty, ProductPage.AssemblyName, "user_profiles_web_part_sorting_grouping" })));
            this.Controls.Add(new LiteralControl("<br/><div>" + Res("Sort", new object[0]) + "<br/>"));
            this.Controls.Add(this.sortDropDownList);
            this.Controls.Add(new LiteralControl("</div><div>"));
            this.sortAscRadioButton.Text = Res("GroupSortAsc", new object[0]);
            this.sortAscRadioButton.GroupName = "sortDir";
            this.Controls.Add(this.sortAscRadioButton);
            this.sortDescRadioButton.Text = Res("GroupSortDesc", new object[0]);
            this.sortDescRadioButton.GroupName = "sortDir";
            this.Controls.Add(this.sortDescRadioButton);
            this.Controls.Add(new LiteralControl("</div><br/><div>" + Res("GroupBy", new object[0]) + "<br/>"));
            this.Controls.Add(this.groupDropDownList);
            this.Controls.Add(new LiteralControl("</div><div>"));
            this.groupAscRadioButton.Text = Res("GroupSortAsc", new object[0]);
            this.groupAscRadioButton.GroupName = "groupDir";
            this.Controls.Add(this.groupAscRadioButton);
            this.groupDescRadioButton.Text = Res("GroupSortDesc", new object[0]);
            this.groupDescRadioButton.GroupName = "groupDir";
            this.Controls.Add(this.groupDescRadioButton);
            this.Controls.Add(new LiteralControl("</div><div>"));
            this.groupByCountsCheckBox.Text = Res("GroupByCounts", new object[0]);
            this.Controls.Add(this.groupByCountsCheckBox);
            this.Controls.Add(new LiteralControl("</div><div>"));
            this.groupShowCountsCheckBox.Text = Res("GroupShowCounts", new object[0]);
            this.Controls.Add(this.groupShowCountsCheckBox);
            this.Controls.Add(new LiteralControl("</div><div>"));
            this.groupIntCheckBox.Text = Res("GroupInteractive", new object[0]);
            this.Controls.Add(this.groupIntCheckBox);
            this.Controls.Add(new LiteralControl("</div><div>"));
            this.groupIntDirCheckBox.Text = Res("GroupInteractiveDir", new object[0]);
            this.Controls.Add(this.groupIntDirCheckBox);
            this.Controls.Add(new LiteralControl("</div>"));
            this.Controls.Add(new LiteralControl("\r\n</div></div>\r\n<div class=\"rox-UserDottedLine\"> </div>\r\n</td></tr></table>\r\n</div>\r\n</div>\r\n"));
            this.Controls.Add(new LiteralControl(string.Format("\r\n<div>\r\n<table><tr><td><div class=\"rox-UserSectionTitle\">{0}<a target=\"_blank\" href=\"/_layouts/{3}/default.aspx?doc={4}\"><img src=\"/_layouts/images/hhelp.gif\"/></a></div></td></tr></table>\r\n<div>\r\n<table><tr><td>\r\n<div class=\"rox-UserSectionHead\"><label for=\"{2}\">{1}</label></div>\r\n<div class=\"rox-UserSectionBody\"><div class=\"rox-UserControlGroup\">\r\n", new object[] { Res("T_Prop4", new object[0]), Res("D_Prop4", new object[0]), this.pageSizeTextBox.ClientID, ProductPage.AssemblyName, "user_profiles_web_part_paging" })));
            this.Controls.Add(this.pageSizeTextBox);
            this.Controls.Add(new LiteralControl("<br/><br/><div>" + Res("PageMode", new object[0]) + "</div>"));
            for (int i = 0; i < 4; i++)
            {
                string text = Res("PageMode_" + i, new object[0]);
                this.pageDropDownList.Items.Add(new ListItem(text, i.ToString()));
            }
            this.Controls.Add(this.pageDropDownList);
            this.Controls.Add(new LiteralControl("<br/><br/><div>" + Res("StepMode", new object[0]) + "</div>"));
            for (int j = 0; j < 3; j++)
            {
                string introduced57 = Res("StepMode_" + j, new object[0]);
                this.pageStepDropDownList.Items.Add(new ListItem(introduced57, j.ToString()));
            }
            this.Controls.Add(this.pageStepDropDownList);
            this.Controls.Add(new LiteralControl("<br/><br/><div>" + Res("SkipMode", new object[0]) + "</div>"));
            for (int k = 0; k < 3; k++)
            {
                string introduced58 = Res("StepMode_" + k, new object[0]);
                this.pageSkipDropDownList.Items.Add(new ListItem(introduced58, k.ToString()));
            }
            this.Controls.Add(this.pageSkipDropDownList);
            this.Controls.Add(new LiteralControl("\r\n</div></div>\r\n<div class=\"rox-UserDottedLine\"> </div>\r\n</td></tr></table>\r\n</div>\r\n</div>\r\n"));
            this.Controls.Add(new LiteralControl(string.Format("\r\n<div>\r\n<table><tr><td><div class=\"rox-UserSectionTitle\">{0}<a target=\"_blank\" href=\"/_layouts/{3}/default.aspx?doc={4}\"><img src=\"/_layouts/images/hhelp.gif\"/></a></div></td></tr></table>\r\n<div>\r\n<table><tr><td>\r\n<div class=\"rox-UserSectionHead\"><label for=\"{2}\">{1}</label></div>\r\n<div class=\"rox-UserSectionBody\"><div class=\"rox-UserControlGroup\">\r\n", new object[] { Res("Misc", new object[0]), Res("ImageHeight" + (roxority_RollupZen.RollupWebPart.IsRollupZen ? "R" : string.Empty), new object[0]), string.Empty, ProductPage.AssemblyName, "user_profiles_web_part_misc" })));
            this.Controls.Add(this.imageHeightTextBox);
            this.Controls.Add(new LiteralControl("<br/><br/><div>" + Res("PrintAction", new object[0]) + "<br/>"));
            this.printDropDownList.AutoPostBack = false;
            this.printDropDownList.CssClass = "ms-input";
            this.Controls.Add(this.printDropDownList);
            this.Controls.Add(new LiteralControl("<br/><br/><div>" + Res("ExportAction", new object[0]) + "<br/>"));
            this.expDropDownList.AutoPostBack = false;
            this.expDropDownList.CssClass = "ms-input";
            this.Controls.Add(this.expDropDownList);
            this.Controls.Add(new LiteralControl("</div><br/><div>" + Res("Anim", new object[0]) + "<br/>"));
            this.animDropDownList.AutoPostBack = false;
            this.animDropDownList.CssClass = "ms-input";
            foreach (string str3 in new string[] { "b", "k", "l" })
            {
                this.animDropDownList.Items.Add(new ListItem(Res("Anim_" + str3, new object[0]), str3));
            }
            this.Controls.Add(this.animDropDownList);
            this.Controls.Add(new LiteralControl("</div><br/><div>" + ProductPage.GetResource("Old_ShowPictures", new object[0]) + "<br/>"));
            this.pictDropDownList.AutoPostBack = false;
            this.pictDropDownList.CssClass = "ms-input";
            for (int m = 0; m < 3; m++)
            {
                string introduced59 = Res("NameMode_" + m, new object[0]);
                this.pictDropDownList.Items.Add(new ListItem(introduced59, m.ToString()));
            }
            this.Controls.Add(this.pictDropDownList);
            this.Controls.Add(new LiteralControl("</div><br/><div>" + Res("NameMode", new object[0]) + "<br/>"));
            this.nameDropDownList.AutoPostBack = false;
            this.nameDropDownList.CssClass = "ms-input";
            for (int n = 0; n < 3; n++)
            {
                string introduced60 = Res("NameMode_" + n, new object[0]);
                this.nameDropDownList.Items.Add(new ListItem(introduced60, n.ToString()));
            }
            this.Controls.Add(this.nameDropDownList);
            this.Controls.Add(new LiteralControl("</div><br/><div>"));
            this.urlSettingsCheckBox.Text = ProductPage.GetResource("UrlSettings", new object[0]);
            this.Controls.Add(this.urlSettingsCheckBox);
            this.Controls.Add(new LiteralControl("</div><br/><div>" + ProductPage.GetResource("Jquery", new object[0]) + "<br/>"));
            this.jqueryDropDownList.AutoPostBack = false;
            this.jqueryDropDownList.CssClass = "ms-input";
            for (int num6 = 0; num6 < 3; num6++)
            {
                string resource = ProductPage.GetResource("Jquery_" + num6, new object[0]);
                this.jqueryDropDownList.Items.Add(new ListItem(resource, num6.ToString()));
            }
            this.Controls.Add(this.jqueryDropDownList);
            this.Controls.Add(new LiteralControl("</div>"));
            this.Controls.Add(new LiteralControl("\r\n</div></div>\r\n<div class=\"rox-UserDottedLine\"> </div>\r\n</td></tr></table>\r\n</div>\r\n</div>\r\n"));
            if (this.propsTextBox.Enabled = selectedWebPart != null)
            {
                this.jqueryDropDownList.Enabled = this.animDropDownList.Enabled = this.pageSizeTextBox.Enabled = this.propsTextBox.Enabled = this.rowSizeTextBox.Enabled = this.tileTextBox.Enabled = selectedWebPart.LicEd(0);
                this.groupByCountsCheckBox.Enabled = this.groupShowCountsCheckBox.Enabled = this.groupIntCheckBox.Enabled = this.tabCheckBox.Enabled = this.groupIntDirCheckBox.Enabled = selectedWebPart.LicEd(4);
                this.groupAscRadioButton.Enabled = this.groupDescRadioButton.Enabled = selectedWebPart.LicEd(2);
                this.groupIntCheckBox.Checked = selectedWebPart.groupInteractive;
                this.tabCheckBox.Checked = selectedWebPart.tabInteractive;
                this.groupIntDirCheckBox.Checked = selectedWebPart.groupInteractiveDir;
                this.groupShowCountsCheckBox.Checked = selectedWebPart.groupShowCounts;
                this.groupByCountsCheckBox.Checked = selectedWebPart.groupByCounts;
                this.groupAscRadioButton.Checked = !(this.groupDescRadioButton.Checked = selectedWebPart.groupDesc && this.groupDescRadioButton.Enabled) && this.groupAscRadioButton.Enabled;
                this.sortAscRadioButton.Enabled = this.sortDescRadioButton.Enabled = selectedWebPart.LicEd(2);
                this.sortAscRadioButton.Checked = !(this.sortDescRadioButton.Checked = selectedWebPart.sortDesc && this.sortDescRadioButton.Enabled) && this.sortAscRadioButton.Enabled;
                this.imageHeightTextBox.Text = selectedWebPart.imageHeight.ToString();
                this.jqueryDropDownList.SelectedIndex = selectedWebPart.JQuery;
                this.animDropDownList.SelectedValue = selectedWebPart.loaderAnim;
                this.pageSizeTextBox.Enabled = this.nameDropDownList.Enabled = this.pictDropDownList.Enabled = selectedWebPart.LicEd(2);
                this.pageSkipDropDownList.Enabled = this.pageStepDropDownList.Enabled = this.pageDropDownList.Enabled = selectedWebPart.LicEd(4);
                this.nameDropDownList.SelectedIndex = selectedWebPart.LicEd(2) ? selectedWebPart.nameMode : selectedWebPart.NameMode;
                this.pictDropDownList.SelectedIndex = selectedWebPart.LicEd(2) ? selectedWebPart.pictMode : selectedWebPart.PictMode;
                this.urlSettingsCheckBox.Checked = (this.urlSettingsCheckBox.Enabled = selectedWebPart.LicEd(4)) && selectedWebPart.UrlSettings;
                this.rowSizeTextBox.Text = selectedWebPart.rowSize.ToString();
                this.tileTextBox.Text = selectedWebPart.TileWidth;
                this.sortCheckBox.Checked = (this.sortCheckBox.Enabled = selectedWebPart.LicEd(2)) && selectedWebPart.allowSort;
                this.viewCheckBox.Checked = (this.viewCheckBox.Enabled = selectedWebPart.LicEd(4)) && selectedWebPart.allowView;
                this.dateThisYearCheckBox.Checked = (this.dateThisYearCheckBox.Enabled = selectedWebPart.LicEd(2)) && selectedWebPart.dateThisYear;
                this.dateNoDayCheckBox.Checked = (this.dateNoDayCheckBox.Enabled = selectedWebPart.LicEd(2)) && selectedWebPart.dateIgnoreDay;
                this.filterLiveCheckBox.Checked = (this.filterLiveCheckBox.Enabled = selectedWebPart.LicEd(2)) && selectedWebPart.filterLive;
                this.styleClassicRadioButton.Checked = !(this.styleListRadioButton.Checked = (this.styleListRadioButton.Enabled = selectedWebPart.LicEd(2)) && selectedWebPart.listStyle);
                this.pageSizeTextBox.Text = selectedWebPart.LicEd(2) ? selectedWebPart.pageSize.ToString() : "4";
                this.propsTextBox.Text = selectedWebPart.Properties;
                this.pageDropDownList.SelectedIndex = selectedWebPart.LicEd(4) ? selectedWebPart.pageMode : selectedWebPart.PageMode;
                this.pageSkipDropDownList.SelectedIndex = selectedWebPart.LicEd(4) ? selectedWebPart.pageSkipMode : selectedWebPart.PageSkipMode;
                this.pageStepDropDownList.SelectedIndex = selectedWebPart.LicEd(4) ? selectedWebPart.pageStepMode : selectedWebPart.PageStepMode;
            }
        }

        internal bool NeedsRefresh(DropDownList propList, string extraProp)
        {
            int num = this.KnownProps.Count + (string.IsNullOrEmpty(extraProp) ? 0 : 1);
            if ((propList.Items.Count == 0) || (propList.Items.Count != (num + 1)))
            {
                return true;
            }
            for (int i = 1; i < propList.Items.Count; i++)
            {
                if (!this.KnownProps.ContainsKey(propList.Items[i].Value) && (string.IsNullOrEmpty(extraProp) || (extraProp != propList.Items[i].Value)))
                {
                    return true;
                }
            }
            foreach (string str in this.KnownProps.Keys)
            {
                if (propList.Items.FindByValue(str) == null)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnLoad(EventArgs e)
        {
            roxority_RollupZen.RollupWebPart selectedWebPart = base.ParentToolPane.SelectedWebPart as roxority_RollupZen.RollupWebPart;
            this.EnsureChildControls();
            base.OnLoad(e);
            this.expDropDownList.Enabled = this.printDropDownList.Enabled = (selectedWebPart == null) || selectedWebPart.LicEd(2);
            if (((this.dataSourceDropDownList.Items.Count == 0) && (selectedWebPart != null)) && !string.IsNullOrEmpty(selectedWebPart.DataSourcePath))
            {
                foreach (IDictionary dictionary in JsonSchemaManager.GetInstances(selectedWebPart.DataSourcePath, "DataSources", "roxority_Shared"))
                {
                    this.dataSourceDropDownList.Items.Add(new ListItem(JsonSchemaManager.GetDisplayName(dictionary, "DataSources", false) + " -- " + ProductPage.GetResource("PC_DataSources_t_" + dictionary["t"], new object[0]), dictionary["id"] + string.Empty));
                }
                if (!string.IsNullOrEmpty(selectedWebPart.DataSourceID))
                {
                    try
                    {
                        this.dataSourceDropDownList.SelectedValue = selectedWebPart.DataSourceID;
                    }
                    catch
                    {
                    }
                }
            }
            this.RefreshFieldDropDown(this.groupDropDownList, 2, selectedWebPart, true, selectedWebPart.groupProp, null, null);
            this.RefreshFieldDropDown(this.tabDropDownList, 2, selectedWebPart, true, selectedWebPart.tabProp, null, null);
            this.RefreshFieldDropDown(this.sortDropDownList, 2, selectedWebPart, true, selectedWebPart.sortProp, "___roxRandomizedSort", ProductPage.GetResource("Disp___roxRandomizedSort", new object[0]));
            this.RefreshFieldDropDown(this.propDropDownList, 0, selectedWebPart, false, string.Empty, null, null);
            if (this.printDropDownList.Items.Count == 0)
            {
                this.printDropDownList.Items.Add(new ListItem(Res("Anim_b", new object[0]), string.Empty));
                if (((selectedWebPart != null) && this.printDropDownList.Enabled) && !string.IsNullOrEmpty(selectedWebPart.PzPath))
                {
                    foreach (IDictionary dictionary2 in JsonSchemaManager.GetInstances(selectedWebPart.PzPath, "PrintActions", "roxority_PrintZen"))
                    {
                        if (!"n".Equals(dictionary2["mpz"]))
                        {
                            this.printDropDownList.Items.Add(new ListItem(JsonSchemaManager.GetDisplayName(dictionary2, "PrintActions", false), dictionary2["id"] + string.Empty));
                        }
                    }
                    if (!string.IsNullOrEmpty(selectedWebPart.PrintAction))
                    {
                        try
                        {
                            this.printDropDownList.SelectedValue = selectedWebPart.PrintAction;
                        }
                        catch
                        {
                            this.printDropDownList.SelectedIndex = 0;
                        }
                    }
                }
            }
            if (this.expDropDownList.Items.Count == 0)
            {
                this.expDropDownList.Items.Add(new ListItem(Res("Anim_b", new object[0]), string.Empty));
                if (((selectedWebPart != null) && this.expDropDownList.Enabled) && !string.IsNullOrEmpty(selectedWebPart.EzPath))
                {
                    foreach (IDictionary dictionary3 in JsonSchemaManager.GetInstances(selectedWebPart.EzPath, "ExportActions", "roxority_ExportZen"))
                    {
                        this.expDropDownList.Items.Add(new ListItem(JsonSchemaManager.GetDisplayName(dictionary3, "ExportActions", false), dictionary3["id"] + string.Empty));
                    }
                    if (!string.IsNullOrEmpty(selectedWebPart.ExportAction))
                    {
                        try
                        {
                            this.expDropDownList.SelectedValue = selectedWebPart.ExportAction;
                        }
                        catch
                        {
                            this.expDropDownList.SelectedIndex = 0;
                        }
                    }
                }
            }
            if ((selectedWebPart != null) && ((this.propsTextBox.Rows = selectedWebPart.Properties.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length + 1) < 3))
            {
                this.propsTextBox.Rows = 3;
            }
            this.dataSourceDropDownList.Attributes["onchange"] = "alert('" + Res("DataSourceChange", new object[0]) + "');";
        }

        internal void RefreshFieldDropDown(DropDownList dropDown, int le, roxority_RollupZen.RollupWebPart webPart, bool none, string propVal, string extraPropName, string extraPropTitle)
        {
            if (this.NeedsRefresh(dropDown, extraPropName))
            {
                dropDown.Items.Clear();
                dropDown.Items.Add(new ListItem(none ? ProductPage.GetResource("None", new object[0]) : string.Empty, none ? string.Empty : string.Empty));
                if (!string.IsNullOrEmpty(extraPropName))
                {
                    dropDown.Items.Add(new ListItem(extraPropTitle, extraPropName));
                }
                foreach (KeyValuePair<string, string> pair in this.KnownProps)
                {
                    dropDown.Items.Add(new ListItem(pair.Value, pair.Key));
                }
                if (string.IsNullOrEmpty(dropDown.SelectedValue))
                {
                    if (dropDown.Enabled = (le <= 0) || webPart.LicEd(le))
                    {
                        try
                        {
                            dropDown.SelectedValue = propVal;
                            return;
                        }
                        catch
                        {
                            dropDown.SelectedIndex = 0;
                            return;
                        }
                    }
                    dropDown.SelectedIndex = 0;
                }
            }
        }

        protected override void RenderToolPart(HtmlTextWriter output)
        {
            if (!ProductPage.isEnabled)
            {
                using (SPSite site = ProductPage.GetAdminSite())
                {
                    output.Write("<div class=\"rox-error\">" + ProductPage.GetResource("NotEnabled", new object[] { ProductPage.MergeUrlPaths(site.Url, string.Concat(new object[] { "/_layouts/", ProductPage.AssemblyName, "/default.aspx?cfg=enable&r=", rnd.Next() })), ProductPage.GetTitle() }) + "</div>");
                }
            }
            else
            {
                output.Write("<div class=\"rox-toolpart\">");
                base.RenderToolPart(output);
                output.Write("</div>");
            }
        }

        internal static string Res(string name, params object[] args)
        {
            return ProductPage.GetProductResource(name, args);
        }

        internal SortedDictionary<string, string> KnownProps
        {
            get
            {
                if (this.knownProps == null)
                {
                    this.knownProps = new SortedDictionary<string, string>();
                }
                try
                {
                    DataSource source;
                    if (((this.knownProps.Count == 0) || (this.dataSourceDropDownList.SelectedValue != this.knownPropID)) && ((source = DataSource.FromID(this.dataSourceDropDownList.SelectedValue, true, true, null)) != null))
                    {
                        this.knownProps.Clear();
                        this.knownPropID = this.dataSourceDropDownList.SelectedValue;
                        foreach (RecordProperty property in source.Properties)
                        {
                            this.knownProps[property.Name] = property.DisplayName;
                        }
                    }
                }
                catch
                {
                }
                return this.knownProps;
            }
        }
    }
}

