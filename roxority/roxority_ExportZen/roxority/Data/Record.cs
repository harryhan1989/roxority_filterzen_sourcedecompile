namespace roxority.Data
{
    using Microsoft.SharePoint;
    using roxority.SharePoint;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Collections.Specialized;

    public class Record
    {
        public readonly roxority.Data.DataSource DataSource;
        public const string HTML_PREFIX = "<roxhtml/>";
        public readonly Guid ID = Guid.Empty;
        public readonly object MainItem;
        private Dictionary<string, RecordPropertyValueCollection> propVals = new Dictionary<string, RecordPropertyValueCollection>();
        public readonly long RecordID = -1L;
        public readonly object RelItem;
        public readonly IDictionary Tags = new OrderedDictionary();

        public Record(roxority.Data.DataSource dataSource, object item, object relItem, Guid guid, long recID)
        {
            this.MainItem = item;
            this.RelItem = relItem;
            this.DataSource = dataSource;
            this.RecordID = recID;
            if (Guid.Empty.Equals(this.ID = guid))
            {
                string str;
                this.ID = ProductPage.GetGuid(new string('0', 0x20 - (str = this.RecordID.ToString()).Length) + str, true);
            }
        }

        public static string GetSpecialFieldValue(roxority.Data.DataSource ds, string name, Converter<string, string> getSourceValue)
        {
            string str;
            string[] strArray;
            if (!string.IsNullOrEmpty(str = ds[name.Substring("rox___".Length), string.Empty]) && ((strArray = str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)).Length > 0))
            {
                foreach (string str3 in strArray)
                {
                    int num2;
                    int num = -1;
                    str = str3;
                    List<string> list = new List<string>();
                    while ((num < (num = str3.IndexOf('[', num + 1))) && ((num2 = str3.IndexOf(']', num + 1)) > num))
                    {
                        list.Add(str3.Substring(num + 1, (num2 - num) - 1));
                    }
                    foreach (string str4 in list)
                    {
                        str = str.Replace("[" + str4 + "]", getSourceValue(str4));
                    }
                    if (!string.IsNullOrEmpty(str = str.Trim()))
                    {
                        return str;
                    }
                }
            }
            return null;
        }

        public RecordPropertyValueCollection this[string name]
        {
            get
            {
                RecordPropertyValueCollection values;
                Converter<string, string> getSourceValue = null;
                string loginName = string.Empty;
                bool flag = false;
                IDictionary dictionary = null;
                UserDataSource dataSource = this.DataSource as UserDataSource;
                if (!this.propVals.TryGetValue(name, out values))
                {
                    string str3;
                    string[] strArray;
                    int num;
                    int num2;
                    IEnumerable<IDictionary> enumerable;
                    if ("roxVcardExport".Equals(name) && (this.DataSource is UserDataSource))
                    {
                        this.propVals[name] = values = new RecordPropertyValueCollection(this.DataSource, this, null, new object[] { "<span class=\"rox-vcard\">" + string.Format(ProductPage.Config(ProductPage.GetContext(), "VcardPropFormat"), UserDataSource.GetVcardExport(this), '{', '}') + "</span>" }, null, null, null);
                        return values;
                    }
                    if (((!name.StartsWith("{", StringComparison.InvariantCultureIgnoreCase) || !name.EndsWith("}", StringComparison.InvariantCultureIgnoreCase)) || ((this.DataSource == null) || (this.DataSource.JsonSchema == null))) || (((this.DataSource.JsonSchema.Owner == null) || ((strArray = name.Substring(1, name.Length - 2).Trim().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)) == null)) || ((strArray.Length < 1) || ((enumerable = JsonSchemaManager.GetInstances(this.DataSource.JsonSchema.Owner.ProdPage, null, "DataFieldFormats", null, null, null, true, true, false, "roxority_Shared")) == null))))
                    {
                        string str;
                        if (!name.StartsWith("rox___"))
                        {
                            RecordProperty propertyByName = this.DataSource.Properties.GetPropertyByName(name);
                            if (propertyByName == null)
                            {
                                throw new Exception(ProductPage.GetResource("Tool_DataSources_UnField", new object[] { name, JsonSchemaManager.GetDisplayName(this.DataSource.JsonInstance, "DataSources", false) }));
                            }
                            this.propVals[name] = values = this.DataSource.GetPropertyValues(this, propertyByName);
                            return values;
                        }
                        if (getSourceValue == null)
                        {
                            getSourceValue = n => this[n, string.Empty];
                        }
                        this.propVals[name] = values = new RecordPropertyValueCollection(this.DataSource, this, null, ((str = GetSpecialFieldValue(this.DataSource, name, getSourceValue)) == null) ? new object[0] : new object[] { str }, null, null, null);
                        return values;
                    }
                    foreach (IDictionary dictionary2 in enumerable)
                    {
                        if ((dictionary2 != null) && (strArray[0].Equals(dictionary2["name"]) || strArray[0].Equals(dictionary2["id"])))
                        {
                            dictionary = dictionary2;
                            break;
                        }
                    }
                    if ((dictionary == null) || string.IsNullOrEmpty(loginName = dictionary["t"] + string.Empty))
                    {
                        long num3;
                        if (strArray[0].Contains("_"))
                        {
                            for (int i = 1; i < strArray.Length; i++)
                            {
                                foreach (string str4 in RecordProperty.ExtractNames(strArray[i], true, this.DataSource))
                                {
                                    strArray[i] = strArray[i].Replace("[" + str4 + "]", this[str4].Value + string.Empty);
                                }
                                while (((num = strArray[i].IndexOf('{')) >= 0) && ((num2 = strArray[i].IndexOf('}', num + 1)) > num))
                                {
                                    strArray[i] = strArray[i].Replace(str3 = strArray[i].Substring(num, (num2 - num) + 1), this[str3].Value + string.Empty);
                                }
                            }
                        }
                        if ((!flag && (flag = strArray[0] == "DateTime_FromBinary")) && ((strArray.Length > 1) && long.TryParse(strArray[1], out num3)))
                        {
                            loginName = DateTime.FromBinary(num3).ToShortDateString();
                        }
                        else if ((!flag && (flag = strArray[0] == "DateTime_FromFileTime")) && ((strArray.Length > 1) && long.TryParse(strArray[1], out num3)))
                        {
                            loginName = DateTime.FromFileTime(num3).ToShortDateString();
                        }
                        else if ((!flag && (flag = strArray[0] == "DateTime_FromFileTimeUtc")) && ((strArray.Length > 1) && long.TryParse(strArray[1], out num3)))
                        {
                            loginName = DateTime.FromFileTimeUtc(num3).ToShortDateString();
                        }
                        else if ((!flag && (flag = strArray[0] == "DateTime_FromTicks")) && ((strArray.Length > 1) && long.TryParse(strArray[1], out num3)))
                        {
                            loginName = new DateTime(num3).ToShortDateString();
                        }
                        else if ((!flag && (flag = strArray[0] == "UserProfiles_PropertyValue")) && ((strArray.Length > 3) && (dataSource != null)))
                        {
                            loginName = dataSource.GetRecordVal(strArray[1], strArray[2], strArray[3]);
                        }
                        else if ((!flag && (flag = strArray[0] == "UserProfiles_CurrentUser")) && (dataSource != null))
                        {
                            loginName = SPContext.Current.Web.CurrentUser.LoginName;
                        }
                        if (!flag)
                        {
                            loginName = ProductPage.GetResource("Tool_DataSources_UnFormat", new object[] { strArray[0] });
                        }
                    }
                    else
                    {
                        for (int j = 1; j < strArray.Length; j++)
                        {
                            loginName = loginName.Replace("[" + j + "]", this[strArray[j]].Value + string.Empty);
                        }
                        foreach (string str5 in RecordProperty.ExtractNames(loginName, true, this.DataSource))
                        {
                            loginName = loginName.Replace("[" + str5 + "]", this[str5].Value + string.Empty);
                        }
                        while (((num = loginName.IndexOf('{')) >= 0) && ((num2 = loginName.IndexOf('}', num + 1)) > num))
                        {
                            loginName = loginName.Replace(str3 = loginName.Substring(num, (num2 - num) + 1), this[str3].Value + string.Empty);
                        }
                        if ("h".Equals(dictionary["m"]))
                        {
                            loginName = "<roxhtml/>" + loginName;
                        }
                    }
                    this.propVals[name] = values = new RecordPropertyValueCollection(this.DataSource, this, null, new object[] { loginName }, null, null, null);
                }
                return values;
            }
        }

        public string this[string name, string defVal]
        {
            get
            {
                RecordPropertyValueCollection values = this[name];
                if ((values != null) && (values.Value != null))
                {
                    return values.Value.ToString();
                }
                return defVal;
            }
        }
    }
}

