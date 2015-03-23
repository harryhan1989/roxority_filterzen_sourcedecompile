namespace roxority.Shared
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Web;

    internal static class WebUtil
    {
        internal static readonly Hashtable entityMapping = new Hashtable();

        static WebUtil()
        {
            entityMapping["<"] = "&lt;";
            entityMapping[">"] = "&gt;";
            entityMapping["\""] = "&quot;";
            entityMapping["Œ"] = "&OElig;";
            entityMapping["œ"] = "&oelig;";
            entityMapping["Š"] = "&Scaron;";
            entityMapping["š"] = "&scaron;";
            entityMapping["Ÿ"] = "&Yuml;";
            entityMapping["ˆ"] = "&circ;";
            entityMapping["~"] = "&tilde;";
            entityMapping["–"] = "&ndash;";
            entityMapping["—"] = "&mdash;";
            entityMapping["‘"] = "&lsquo;";
            entityMapping["’"] = "&rsquo;";
            entityMapping["‚"] = "&sbquo;";
            entityMapping["“"] = "&ldquo;";
            entityMapping["”"] = "&rdquo;";
            entityMapping["„"] = "&bdquo;";
            entityMapping["†"] = "&dagger;";
            entityMapping["‡"] = "&Dagger;";
            entityMapping["‰"] = "&permil;";
            entityMapping["‹"] = "&lsaquo;";
            entityMapping["›"] = "&rsaquo;";
            entityMapping["ƒ"] = "&fnof;";
            entityMapping["Α"] = "&Alpha;";
            entityMapping["Β"] = "&Beta;";
            entityMapping["Γ"] = "&Gamma;";
            entityMapping["Δ"] = "&Delta;";
            entityMapping["Ε"] = "&Epsilon;";
            entityMapping["Ζ"] = "&Zeta;";
            entityMapping["Η"] = "&Eta;";
            entityMapping["Θ"] = "&Theta;";
            entityMapping["Ι"] = "&Iota;";
            entityMapping["Κ"] = "&Kappa;";
            entityMapping["Λ"] = "&Lambda;";
            entityMapping["Μ"] = "&Mu;";
            entityMapping["Ν"] = "&Nu;";
            entityMapping["Ξ"] = "&Xi;";
            entityMapping["Ο"] = "&Omicron;";
            entityMapping["Π"] = "&Pi;";
            entityMapping["Ρ"] = "&Rho;";
            entityMapping["Σ"] = "&Sigma;";
            entityMapping["Τ"] = "&Tau;";
            entityMapping["Υ"] = "&Upsilon;";
            entityMapping["Φ"] = "&Phi;";
            entityMapping["Χ"] = "&Chi;";
            entityMapping["Ψ"] = "&Psi;";
            entityMapping["Ω"] = "&Omega;";
            entityMapping["α"] = "&alpha;";
            entityMapping["β"] = "&beta;";
            entityMapping["γ"] = "&gamma;";
            entityMapping["δ"] = "&delta;";
            entityMapping["ε"] = "&epsilon;";
            entityMapping["ζ"] = "&zeta;";
            entityMapping["η"] = "&eta;";
            entityMapping["θ"] = "&theta;";
            entityMapping["ι"] = "&iota;";
            entityMapping["κ"] = "&kappa;";
            entityMapping["λ"] = "&lambda;";
            entityMapping["μ"] = "&mu;";
            entityMapping["ν"] = "&nu;";
            entityMapping["ξ"] = "&xi;";
            entityMapping["ο"] = "&omicron;";
            entityMapping["π"] = "&pi;";
            entityMapping["ρ"] = "&rho;";
            entityMapping["ς"] = "&sigmaf;";
            entityMapping["σ"] = "&sigma;";
            entityMapping["τ"] = "&tau;";
            entityMapping["υ"] = "&upsilon;";
            entityMapping["φ"] = "&phi;";
            entityMapping["χ"] = "&chi;";
            entityMapping["ψ"] = "&psi;";
            entityMapping["ω"] = "&omega;";
            entityMapping["ϑ"] = "&thetasym;";
            entityMapping["ϒ"] = "&upsih;";
            entityMapping["ϖ"] = "&piv;";
            entityMapping["•"] = "&bull;";
            entityMapping["…"] = "&hellip;";
            entityMapping["′"] = "&prime;";
            entityMapping["″"] = "&Prime;";
            entityMapping["‾"] = "&oline;";
            entityMapping["⁄"] = "&frasl;";
            entityMapping["℘"] = "&weierp;";
            entityMapping["ℑ"] = "&image;";
            entityMapping["ℜ"] = "&real;";
            entityMapping["™"] = "&trade;";
            entityMapping["ℵ"] = "&alefsym;";
            entityMapping["←"] = "&larr;";
            entityMapping["↑"] = "&uarr;";
            entityMapping["→"] = "&rarr;";
            entityMapping["↓"] = "&darr;";
            entityMapping["↔"] = "&harr;";
            entityMapping["↵"] = "&crarr;";
            entityMapping["⇐"] = "&lArr;";
            entityMapping["⇑"] = "&uArr;";
            entityMapping["⇒"] = "&rArr;";
            entityMapping["⇓"] = "&dArr;";
            entityMapping["⇔"] = "&hArr;";
            entityMapping["∀"] = "&forall;";
            entityMapping["∂"] = "&part;";
            entityMapping["∃"] = "&exist;";
            entityMapping["∅"] = "&empty;";
            entityMapping["∇"] = "&nabla;";
            entityMapping["∈"] = "&isin;";
            entityMapping["∉"] = "&notin;";
            entityMapping["∋"] = "&ni;";
            entityMapping["∏"] = "&prod;";
            entityMapping["−"] = "&sum;";
            entityMapping["−"] = "&minus;";
            entityMapping["∗"] = "&lowast;";
            entityMapping["√"] = "&radic;";
            entityMapping["∝"] = "&prop;";
            entityMapping["∞"] = "&infin;";
            entityMapping["∠"] = "&ang;";
            entityMapping["⊥"] = "&and;";
            entityMapping["⊦"] = "&or;";
            entityMapping["∩"] = "&cap;";
            entityMapping["∪"] = "&cup;";
            entityMapping["∫"] = "&int;";
            entityMapping["∴"] = "&there4;";
            entityMapping["∼"] = "&sim;";
            entityMapping["≅"] = "&cong;";
            entityMapping["≅"] = "&asymp;";
            entityMapping["≠"] = "&ne;";
            entityMapping["≡"] = "&equiv;";
            entityMapping["≤"] = "&le;";
            entityMapping["≥"] = "&ge;";
            entityMapping["⊂"] = "&sub;";
            entityMapping["⊃"] = "&sup;";
            entityMapping["⊄"] = "&nsub;";
            entityMapping["⊆"] = "&sube;";
            entityMapping["⊇"] = "&supe;";
            entityMapping["⊕"] = "&oplus;";
            entityMapping["⊗"] = "&otimes;";
            entityMapping["⊥"] = "&perp;";
            entityMapping["⋅"] = "&sdot;";
            entityMapping["⌈"] = "&lceil;";
            entityMapping["⌉"] = "&rceil;";
            entityMapping["⌊"] = "&lfloor;";
            entityMapping["⌋"] = "&rfloor;";
            entityMapping["〈"] = "&lang;";
            entityMapping["〉"] = "&rang;";
            entityMapping["◊"] = "&loz;";
            entityMapping["♠"] = "&spades;";
            entityMapping["♣"] = "&clubs;";
            entityMapping["♥"] = "&hearts;";
            entityMapping["♦"] = "&diams;";
            entityMapping["\x00a3"] = "&pound;";
            entityMapping["\x00a1"] = "&iexcl;";
            entityMapping["\x00a2"] = "&cent;";
            entityMapping["€"] = "&euro;";
            entityMapping["\x00a4"] = "&curren;";
            entityMapping["\x00a5"] = "&yen;";
            entityMapping["\x00a6"] = "&brvbar;";
            entityMapping["\x00a7"] = "&sect;";
            entityMapping["\x00a8"] = "&uml;";
            entityMapping["\x00a9"] = "&copy;";
            entityMapping["\x00aa"] = "&ordf;";
            entityMapping["\x00ab"] = "&laquo;";
            entityMapping["\x00ac"] = "&not;";
            entityMapping["\x00ae"] = "&reg;";
            entityMapping["\x00af"] = "&macr;";
            entityMapping["\x00b0"] = "&deg;";
            entityMapping["\x00b1"] = "&plusmn;";
            entityMapping["\x00b2"] = "&sup2;";
            entityMapping["\x00b3"] = "&sup3;";
            entityMapping["\x00b4"] = "&acute;";
            entityMapping["\x00b5"] = "&micro;";
            entityMapping["\x00b6"] = "&para;";
            entityMapping["\x00b7"] = "&middot;";
            entityMapping["\x00b8"] = "&cedil;";
            entityMapping["\x00b9"] = "&sup1;";
            entityMapping["\x00ba"] = "&ordm;";
            entityMapping["\x00bb"] = "&raquo;";
            entityMapping["\x00bc"] = "&frac14;";
            entityMapping["\x00bd"] = "&frac12;";
            entityMapping["\x00be"] = "&frac34;";
            entityMapping["\x00bf"] = "&iquest;";
            entityMapping["\x00c0"] = "&Agrave;";
            entityMapping["\x00c1"] = "&Aacute;";
            entityMapping["\x00c2"] = "&Acirc;";
            entityMapping["\x00c3"] = "&Atilde;";
            entityMapping["\x00c4"] = "&Auml;";
            entityMapping["\x00c5"] = "&Aring;";
            entityMapping["\x00c6"] = "&AElig;";
            entityMapping["\x00c7"] = "&Ccedil;";
            entityMapping["\x00c8"] = "&Egrave;";
            entityMapping["\x00c9"] = "&Eacute;";
            entityMapping["\x00ca"] = "&Ecirc;";
            entityMapping["\x00cb"] = "&Euml;";
            entityMapping["\x00cc"] = "&Igrave;";
            entityMapping["\x00cd"] = "&Iacute;";
            entityMapping["\x00ce"] = "&Icirc;";
            entityMapping["\x00cf"] = "&Iuml;";
            entityMapping["\x00d0"] = "&ETH;";
            entityMapping["\x00d1"] = "&Ntilde;";
            entityMapping["\x00d2"] = "&Ograve;";
            entityMapping["\x00d3"] = "&Oacute;";
            entityMapping["\x00d4"] = "&Ocirc;";
            entityMapping["\x00d5"] = "&Otilde;";
            entityMapping["\x00d6"] = "&Ouml;";
            entityMapping["\x00d7"] = "&times;";
            entityMapping["\x00d8"] = "&Oslash;";
            entityMapping["\x00d9"] = "&Ugrave;";
            entityMapping["\x00da"] = "&Uacute;";
            entityMapping["\x00db"] = "&Ucirc;";
            entityMapping["\x00dc"] = "&Uuml;";
            entityMapping["\x00dd"] = "&Yacute;";
            entityMapping["\x00de"] = "&THORN;";
            entityMapping["\x00df"] = "&szlig;";
            entityMapping["\x00e0"] = "&agrave;";
            entityMapping["\x00e1"] = "&aacute;";
            entityMapping["\x00e2"] = "&acirc;";
            entityMapping["\x00e3"] = "&atilde;";
            entityMapping["\x00e4"] = "&auml;";
            entityMapping["\x00e5"] = "&aring;";
            entityMapping["\x00e6"] = "&aelig;";
            entityMapping["\x00e7"] = "&ccedil;";
            entityMapping["\x00e8"] = "&egrave;";
            entityMapping["\x00e9"] = "&eacute;";
            entityMapping["\x00ea"] = "&ecirc;";
            entityMapping["\x00eb"] = "&euml;";
            entityMapping["\x00ec"] = "&igrave;";
            entityMapping["\x00ed"] = "&iacute;";
            entityMapping["\x00ee"] = "&icirc;";
            entityMapping["\x00ef"] = "&iuml;";
            entityMapping["\x00f0"] = "&eth;";
            entityMapping["\x00f1"] = "&ntilde;";
            entityMapping["\x00f2"] = "&ograve;";
            entityMapping["\x00f3"] = "&oacute;";
            entityMapping["\x00f4"] = "&ocirc;";
            entityMapping["\x00f5"] = "&otilde;";
            entityMapping["\x00f6"] = "&ouml;";
            entityMapping["\x00f7"] = "&divide;";
            entityMapping["\x00f8"] = "&oslash;";
            entityMapping["\x00f9"] = "&ugrave;";
            entityMapping["\x00fa"] = "&uacute;";
            entityMapping["\x00fb"] = "&ucirc;";
            entityMapping["\x00fc"] = "&uuml;";
            entityMapping["\x00fd"] = "&yacute;";
            entityMapping["\x00fe"] = "&thorn;";
            entityMapping["\x00ff"] = "&yuml;";
        }

        internal static string CompileMht(string title, params string[] files)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("From: <Saved by Windows Internet Explorer 7>\r\nSubject: {0}\r\nDate: {1} +0200\r\nMIME-Version: 1.0\r\nContent-Type: multipart/related;\r\n\ttype=\"text/html\";\r\n\tboundary=\"----=_NextPart_000_0000_01CA2B1E.5D5D76A0\"\r\nX-MimeOLE: Produced By Microsoft MimeOLE V6.00.3790.3959\r\nX-roxority: not really, this is auto-generated but pretends to be IE-generated\r\n\r\nThis is a multi-part message in MIME format.\r\n\r\n", title, DateTime.Now.ToUniversalTime().ToString("r").Substring(0, DateTime.Now.ToUniversalTime().ToString("r").LastIndexOf(' ')));
            return builder.ToString();
        }

        internal static string[] GetQueryParameters(string queryString)
        {
            string[] strArray;
            ArrayList list = new ArrayList();
            while (queryString.StartsWith("?"))
            {
                queryString = queryString.Substring(1);
            }
            if (!SharedUtil.IsEmpty((ICollection) (strArray = queryString.Split(new char[] { '&' }))))
            {
                foreach (string str in strArray)
                {
                    string[] strArray2;
                    if (!SharedUtil.IsEmpty((ICollection) (strArray2 = str.Split(new char[] { '=' }))))
                    {
                        foreach (string str2 in strArray2)
                        {
                            list.Add(str2);
                        }
                    }
                }
            }
            return (list.ToArray(typeof(string)) as string[]);
        }

        internal static string[] GetUserLanguages(params string[] supportedLanguages)
        {
            string customStr = "";
            return GetUserLanguages(ref customStr, supportedLanguages);
        }

        internal static string[] GetUserLanguages(ref string languagePreference, params string[] supportedLanguages)
        {
            ArrayList list = null;
            int index;
            string str;
            bool flag = true;
            string str2 = languagePreference = SharedUtil.Trim(languagePreference).ToLower();
            if (!SharedUtil.IsEmpty((string) languagePreference) && ((index = languagePreference.IndexOf('-')) > 0))
            {
                str2 = languagePreference.Substring(0, index);
            }
            if (((HttpContext.Current == null) || (HttpContext.Current.Request == null)) || SharedUtil.IsEmpty((ICollection) HttpContext.Current.Request.UserLanguages))
            {
                if (!SharedUtil.IsEmpty((ICollection) supportedLanguages))
                {
                    list = new ArrayList(supportedLanguages);
                }
                else
                {
                    list = new ArrayList();
                }
            }
            else
            {
                list = new ArrayList(HttpContext.Current.Request.UserLanguages.Clone() as ICollection);
                while (flag && (list.Count > 0))
                {
                    flag = false;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (flag = (list[i] == null) || SharedUtil.IsEmpty(list[i] as string))
                        {
                            list.Remove(i);
                            continue;
                        }
                        if (flag = (index = ((string) list[i]).IndexOf(';')) >= 0)
                        {
                            list[i] = ((string) list[i]).Substring(0, index);
                            continue;
                        }
                        if (flag = (index = ((string) list[i]).IndexOf('-')) >= 0)
                        {
                            list[i] = ((string) list[i]).Substring(0, index);
                            continue;
                        }
                        list[i] = SharedUtil.Trim((string) list[i]).ToLower();
                    }
                }
                if (!SharedUtil.IsEmpty((ICollection) supportedLanguages))
                {
                    flag = true;
                    while (flag)
                    {
                        flag = false;
                        foreach (string str3 in list)
                        {
                            if (Array.IndexOf<string>(supportedLanguages, str3) < 0)
                            {
                                flag = true;
                                list.Remove(str3);
                                continue;
                            }
                        }
                    }
                }
            }
            if (SharedUtil.IsEmpty((ICollection) list))
            {
                list.AddRange(supportedLanguages);
            }
            if ((Array.IndexOf<string>(supportedLanguages, languagePreference) < 0) && (Array.IndexOf<string>(supportedLanguages, str2) >= 0))
            {
                languagePreference = str2;
            }
            languagePreference = str = SharedUtil.Trim(languagePreference).ToLower();
            if (!SharedUtil.IsEmpty(str) && (Array.IndexOf<string>(supportedLanguages, languagePreference) >= 0))
            {
                index = list.IndexOf(languagePreference);
                if (index >= 0)
                {
                    list.RemoveAt(index);
                }
                list.Insert(0, languagePreference);
            }
            else if ((list.Count > 0) && ((languagePreference = list[0] as string) == null))
            {
                languagePreference = string.Empty;
            }
            return (list.ToArray(typeof(string)) as string[]);
        }

        internal static string HtmlEscape(string value)
        {
            StringBuilder builder = new StringBuilder(value);
            builder.Replace("&", "&amp;");
            foreach (DictionaryEntry entry in entityMapping)
            {
                builder.Replace((string) entry.Key, (string) entry.Value);
            }
            return builder.ToString();
        }

        internal static string HtmlUnescape(string value)
        {
            StringBuilder builder = new StringBuilder(value);
            foreach (DictionaryEntry entry in entityMapping)
            {
                builder.Replace((string) entry.Value, (string) entry.Key);
            }
            return builder.ToString();
        }

        internal static string ModifyUrlParameter(string uri, string paramName, string paramValue)
        {
            return ModifyUrlParameter(new Uri(uri), paramName, paramValue).ToString();
        }

        internal static Uri ModifyUrlParameter(Uri uri, string paramName, string paramValue)
        {
            int num;
            string query = uri.Query;
            if ((query.Length > 1) && ((num = query.IndexOf(paramName + "=")) >= 1))
            {
                int index = query.IndexOf('&', num + 1);
                string str2 = query.Substring(0, num);
                while (str2.EndsWith("&"))
                {
                    str2 = str2.Substring(0, str2.Length - 1);
                }
                if (index > num)
                {
                    str2 = str2 + query.Substring(index);
                }
                query = str2 = str2 + string.Format("&{0}={1}", paramName, paramValue);
            }
            else if (query.Length == 1)
            {
                query = query + string.Format("{0}={1}", paramName, paramValue);
            }
            else if (query.Length == 0)
            {
                query = query + string.Format("?{0}={1}", paramName, paramValue);
            }
            else
            {
                query = query + string.Format("&{0}={1}", paramName, paramValue);
            }
            return new Uri(uri.GetLeftPart(UriPartial.Path) + query);
        }

        internal static string ModifyUrlParameters(string uri, params string[] paramNamesValues)
        {
            return ModifyUrlParameters(new Uri(uri), paramNamesValues).ToString();
        }

        internal static Uri ModifyUrlParameters(Uri uri, params string[] paramNamesValues)
        {
            for (int i = 0; i < paramNamesValues.Length; i += 2)
            {
                uri = ModifyUrlParameter(uri, paramNamesValues[i], paramNamesValues[i + 1]);
            }
            return uri;
        }

        internal static string NamedToNumericEntities(string html)
        {
            StringBuilder builder = new StringBuilder(html);
            foreach (DictionaryEntry entry in entityMapping)
            {
                builder.Replace((string) entry.Value, HttpUtility.HtmlEncode((string) entry.Key));
            }
            return builder.Replace("&nbsp;", "&#160;").ToString();
        }

        internal sealed class RequestContext
        {
            internal readonly string ContextID;
            private object value;

            private RequestContext() : this(null)
            {
            }

            private RequestContext(string contextID)
            {
                if (SharedUtil.IsEmpty(this.ContextID = contextID))
                {
                    this.ContextID = contextID = Guid.NewGuid().ToString();
                }
                try
                {
                    if ((Items != null) && Items.Contains(this.ContextID))
                    {
                        throw new ArgumentException(null, "contextID");
                    }
                }
                catch
                {
                }
            }

            internal static WebUtil.RequestContext Add(object value)
            {
                return Add(null, value);
            }

            internal static WebUtil.RequestContext Add(string contextID, object value)
            {
                return new WebUtil.RequestContext(contextID) { Value = value };
            }

            internal void Remove()
            {
                try
                {
                    if (Items != null)
                    {
                        Items.Remove(this.ContextID);
                    }
                }
                catch
                {
                }
            }

            public override string ToString()
            {
                object obj2 = this.Value;
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return null;
            }

            internal static IDictionary Items
            {
                get
                {
                    if (HttpContext.Current != null)
                    {
                        return HttpContext.Current.Items;
                    }
                    return null;
                }
            }

            internal object Value
            {
                get
                {
                    if (Items != null)
                    {
                        return Items[this.ContextID];
                    }
                    return this.value;
                }
                set
                {
                    this.value = value;
                    try
                    {
                        if (Items != null)
                        {
                            Items[this.ContextID] = value;
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}

