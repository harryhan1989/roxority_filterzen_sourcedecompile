namespace roxority.Shared
{
    using roxority.Shared.Collections;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    internal static class SharedUtil
    {
        internal const char CHAR_ELLIPSIS = '…';
        private const string DEFAULT_RESOURCES_BASENAME = "Properties.Resources";
        internal static readonly System.Random Random = new System.Random();
        private static HashTree resourceManagers = null;
        internal const string STRING_ELLIPSIS = "…";

        internal static bool AnyFlag(Enum value, params Enum[] flags)
        {
            foreach (Enum enum2 in flags)
            {
                if (((Convert.ToInt32(value) & (Convert.ToInt32(enum2))) == (Convert.ToInt32(enum2))))
                {
                    return true;
                }
            }
            return false;
        }

        internal static long ApproximateRemainingProgress(long progress, int percent)
        {
            if ((percent != 0) && (percent < 100))
            {
                return (((long) ((100.0 / ((double) percent)) * progress)) - progress);
            }
            return 0L;
        }

        internal static void Assort(Array array, out Array assorted, out Array remainders, AssortHandler handler, params object[] args)
        {
            ThrowIfEmpty(handler, "handler");
            if (IsEmpty((ICollection) array))
            {
                assorted = null;
                remainders = null;
            }
            else
            {
                ArrayList list = new ArrayList(array.Length);
                ArrayList list2 = new ArrayList(array.Length);
                System.Type elementType = array.GetType().GetElementType();
                for (int i = 0; i < array.Length; i++)
                {
                    object obj2;
                    if (handler(obj2 = array.GetValue(i), args))
                    {
                        list.Add(obj2);
                    }
                    else
                    {
                        list2.Add(obj2);
                    }
                }
                assorted = list.ToArray(elementType);
                remainders = list2.ToArray(elementType);
            }
        }

        internal static U Batch<T, U>(Operation<T, U> operation, U determinant, U defaultValue, params T[] args)
        {
            foreach (T local in args)
            {
                U local2;
                U local3 = local2 = operation(local);
                if (local3.Equals(determinant))
                {
                    return local2;
                }
            }
            return defaultValue;
        }

        internal static T Clone<T>(T value) where T: class, ICloneable
        {
            if (value != null)
            {
                return (value.Clone() as T);
            }
            return default(T);
        }

        internal static List<T> Combine<T>(params ICollection[] collections)
        {
            List<T> list = new List<T>();
            foreach (ICollection is2 in collections)
            {
                if (!IsEmpty(is2))
                {
                    T[] array = new T[is2.Count];
                    is2.CopyTo(array, 0);
                    list.AddRange(array);
                }
            }
            return list;
        }

        internal static ComparisonResult Compare<T>(T one, T two, Predicate<T> match)
        {
            bool flag = match(one);
            bool flag2 = match(two);
            if (flag && flag2)
            {
                return ComparisonResult.Both;
            }
            if (flag)
            {
                return ComparisonResult.One;
            }
            if (!flag2)
            {
                return ComparisonResult.None;
            }
            return ComparisonResult.Two;
        }

        internal static bool Contains<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Predicate<TKey> keyMatch, Predicate<TValue> valueMatch)
        {
            return Contains<TKey, TValue>(dictionary, keyMatch, valueMatch, delegate (Duo<bool> value) {
                if (value.Value1)
                {
                    return value.Value2;
                }
                return false;
            });
        }

        internal static bool Contains<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Predicate<TKey> keyMatch, Predicate<TValue> valueMatch, Predicate<Duo<bool>> combineMatch)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                bool flag = (keyMatch == null) || keyMatch(pair.Key);
                bool flag2 = (valueMatch == null) || valueMatch(pair.Value);
                if (combineMatch(new Duo<bool>(flag, flag2)))
                {
                    return true;
                }
            }
            return false;
        }

        internal static void CopyHashtableTo(Hashtable source, Hashtable destination)
        {
            foreach (DictionaryEntry entry in source)
            {
                destination[entry.Key] = entry.Value;
            }
        }

        internal static T[] CreateArray<T>(IEnumerable<T> values, Predicate<T> match)
        {
            List<T> list = new List<T>();
            if (values != null)
            {
                if (match == null)
                {
                    list.AddRange(values);
                }
                else
                {
                    foreach (T local in values)
                    {
                        if (match(local))
                        {
                            list.Add(local);
                        }
                    }
                }
            }
            return list.ToArray();
        }

        internal static TArray[] CreateArray<TArray, TSource>(IEnumerable<TSource> values, Operation<TSource, TArray> operation)
        {
            return CreateArray<TArray, TSource>(values, operation, false);
        }

        internal static TArray[] CreateArray<TArray, TSource>(IEnumerable<TSource> values, Operation<TSource, TArray> operation, bool nullIfEmpty)
        {
            List<TArray> list = new List<TArray>();
            if (values != null)
            {
                foreach (TSource local in values)
                {
                    list.Add(operation(local));
                }
            }
            else if (nullIfEmpty)
            {
                return null;
            }
            if (nullIfEmpty && (list.Count == 0))
            {
                return null;
            }
            return list.ToArray();
        }

        internal static Dictionary<TKey, TValue> CreateDictionary<TKey, TValue>(params Duo<TKey, TValue>[] items)
        {
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            foreach (Duo<TKey, TValue> duo in items)
            {
                dictionary[duo.Value1] = duo.Value2;
            }
            return dictionary;
        }

        internal static Dictionary<T, T> CreateDictionary<T>(params T[] keysValues)
        {
            List<Duo<T, T>> list = new List<Duo<T, T>>();
            for (int i = 0; i < keysValues.Length; i += 2)
            {
                list.Add(new Duo<T, T>(keysValues[i], keysValues[i + 1]));
            }
            return CreateDictionary<T, T>(list.ToArray());
        }

        internal static Dictionary<TKey, TValue> CreateDictionary<TKey, TValue>(IEnumerable<TKey> keys, IEnumerable<TValue> values)
        {
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            foreach (Duo<TKey, TValue> duo in Enumerate<TKey, TValue>(keys, values))
            {
                dictionary[duo.Value1] = duo.Value2;
            }
            return dictionary;
        }

        internal static Hashtable CreateHashtable(IDataRecord record)
        {
            if (record == null)
            {
                return null;
            }
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < record.FieldCount; i++)
            {
                hashtable[record.GetName(i)] = record[i];
            }
            return hashtable;
        }

        internal static object CreateInstance(System.Type type, params object[] args)
        {
            if (type.IsArray)
            {
                return Array.CreateInstance(type.GetElementType(), (int) args[0]);
            }
            ConstructorInfo constructor = type.GetConstructor(GetTypes(args));
            if (constructor == null)
            {
                return Activator.CreateInstance(type, args);
            }
            return constructor.Invoke(args);
        }

        internal static LinkArea CreateLinkArea(LinkLabel linkLabel, SeekOrigin seekOrigin, int length)
        {
            switch (seekOrigin)
            {
                case SeekOrigin.Begin:
                    return new LinkArea(0, length);

                case SeekOrigin.End:
                    return new LinkArea(linkLabel.Text.Length - length, length);
            }
            throw new ArgumentException(null, "seekOrigin");
        }

        internal static List<T> CreateList<T>(IEnumerable<T> source, bool nullIfEmpty)
        {
            List<T> list = (source == null) ? new List<T>() : new List<T>(source);
            if (nullIfEmpty && IsEmpty((ICollection) list))
            {
                return null;
            }
            return list;
        }

        internal static List<T> CreateList<T>(IEnumerable<T> source, Predicate<T> match)
        {
            List<T> list = new List<T>();
            if (source != null)
            {
                foreach (T local in source)
                {
                    if ((local != null) && ((match == null) || match(local)))
                    {
                        list.Add(local);
                    }
                }
            }
            return list;
        }

        internal static string DefaultJoinOperation<T>(T value) where T: class
        {
            if (value != null)
            {
                return value.ToString();
            }
            return null;
        }

        internal static string DefaultJoinStructOperation<T>(T value) where T: struct
        {
            return value.ToString();
        }

        internal static T DescendentControl<T>(Control control) where T: Control
        {
            if ((control == null) || (control.Parent == null))
            {
                return default(T);
            }
            if (control.Parent is T)
            {
                return (control.Parent as T);
            }
            return DescendentControl<T>(control.Parent);
        }

        internal static T Deserialize<T>(SerializationInfo info, string name, T defaultValue)
        {
            try
            {
                return (T) info.GetValue(name, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static object DeserializeBinary(byte[] value)
        {
            if (value != null)
            {
                using (MemoryStream stream = new MemoryStream(value, false))
                {
                    return new BinaryFormatter().Deserialize(stream);
                }
            }
            return null;
        }

        internal static object DeserializeBinary(IDataObject value)
        {
            if ((value != null) && value.GetDataPresent(typeof(byte[])))
            {
                return DeserializeBinary(value.GetData(typeof(byte[])) as byte[]);
            }
            return null;
        }

        internal static T Do<T>(T value, params Operation<T, T>[] actions)
        {
            foreach (Operation<T, T> operation in actions)
            {
                value = operation(value);
            }
            return value;
        }

        internal static IEnumerable<Duo<T, U>> Enumerate<T, U>(params object[] values)
        {
            throw new NotSupportedException();
        }

        internal static IEnumerable<T> Enumerate<T>(params object[] values)
        {
            throw new NotSupportedException();
        }

        internal static IEnumerable<T> Enumerate<T>(IEnumerable source)
        {
            throw new NotSupportedException();
        }

        internal static IEnumerable<Duo<T, U>> Enumerate<T, U>(IEnumerable<T> t, IEnumerable<U> u)
        {
            throw new NotSupportedException();
        }

        internal static IEnumerable<TValue> Enumerate<TTagged, TValue>(IEnumerable taggedObjects, Operation<TTagged, TValue> convertTag)
        {
            throw new NotSupportedException();
        }

        internal static bool Equals<T>(T[] one, T[] two)
        {
            if ((one != null) || (two != null))
            {
                if ((one == null) || (two == null))
                {
                    return false;
                }
                if (!object.ReferenceEquals(one, two))
                {
                    if (one.Length != two.Length)
                    {
                        return false;
                    }
                    for (int i = 0; i < one.Length; i++)
                    {
                        if (!Equals(one[i], two[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        internal static bool Equals(object one, object two)
        {
            if ((one != null) || (two != null))
            {
                if ((one == null) || (two == null))
                {
                    return false;
                }
                if (!object.ReferenceEquals(one, two))
                {
                    return one.Equals(two);
                }
            }
            return true;
        }

        internal static IEnumerable<TReturn> FetchUnique<TReturn, TList>(IEnumerable<TList> values, Operation<TList, TReturn> getter)
        {
            List<TReturn> list = new List<TReturn>();
            foreach (TList local in values)
            {
                TReturn local2;
                if (((local2 = getter(local)) != null) && !list.Contains(local2))
                {
                    list.Add(local2);
                }
            }
            return list;
        }

        internal static Duo<TKey, TValue> Find<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Predicate<TValue> match)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                if (match(pair.Value))
                {
                    return new Duo<TKey, TValue>(pair.Key, pair.Value);
                }
            }
            return null;
        }

        internal static object FromBase64String(string value)
        {
            if (IsEmpty(value))
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(value), false))
            {
                return new BinaryFormatter().Deserialize(stream);
            }
        }

        internal static bool GetConfig(string configName, bool defaultValue)
        {
            try
            {
                return bool.Parse(GetConfig(configName, defaultValue.ToString()));
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static IsolationLevel GetConfig(string configName, IsolationLevel defaultValue)
        {
            try
            {
                return (IsolationLevel) Enum.Parse(typeof(IsolationLevel), GetConfig(configName, defaultValue.ToString()), true);
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static string GetConfig(string configName, string defaultValue)
        {
            try
            {
                string str;
                return (IsEmpty(str = ConfigurationManager.AppSettings[configName]) ? defaultValue : str);
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static string GetExceptionMessages(Exception ex)
        {
            StringBuilder buffer = new StringBuilder();
            GetExceptionMessages(ex, buffer, 0);
            return buffer.ToString();
        }

        internal static void GetExceptionMessages(Exception ex, StringBuilder buffer, int level)
        {
            if (level > 0)
            {
                buffer.Append('\t', level);
            }
            buffer.Append(ex.Message);
            if (ex.InnerException != null)
            {
                buffer.AppendLine();
                GetExceptionMessages(ex.InnerException, buffer, level + 1);
            }
        }

        internal static int GetHashCode(Array values)
        {
            object obj2;
            if (IsEmpty((ICollection) values))
            {
                return 0;
            }
            int num = ((obj2 = values.GetValue(0)) == null) ? 0 : obj2.GetHashCode();
            if (values.Length > 1)
            {
                for (int i = 1; i < values.Length; i++)
                {
                    obj2 = values.GetValue(i);
                    if (obj2 is Array)
                    {
                        num ^= GetHashCode(obj2 as Array);
                    }
                    else if (obj2 != null)
                    {
                        num ^= obj2.GetHashCode();
                    }
                }
            }
            return num;
        }

        internal static int GetHashCode(params object[] values)
        {
            return GetHashCode((Array) values);
        }

        internal static int GetIndex(string text, int line, int column, bool zeroBased)
        {
            int num = 0;
            if (!zeroBased)
            {
                return GetIndex(text, line - 1, column - 1, true);
            }
            for (int i = 0; i < line; i++)
            {
                int num3;
                if ((((num3 = text.IndexOf("\r\n", (int) (num + 2))) < num) && ((num3 = text.IndexOf("\n", (int) (num + 1))) < num)) && ((num3 = text.IndexOf("\r", (int) (num + 1))) < num))
                {
                    break;
                }
                num = num3;
            }
            return (num + column);
        }

        internal static ResourceManager GetResourceManager(Assembly assembly)
        {
            return GetResourceManager(assembly, null);
        }

        internal static ResourceManager GetResourceManager(Assembly assembly, string baseName)
        {
            ResourceManager manager = null;
            bool flag = false;
            ThrowIfEmpty(assembly, "assembly");
            if (IsEmpty(baseName, true))
            {
                ResourceBaseNameAttribute customAttribute = Attribute.GetCustomAttribute(assembly, typeof(ResourceBaseNameAttribute)) as ResourceBaseNameAttribute;
                if (customAttribute == null)
                {
                    baseName = assembly.GetName().Name + ".Properties.Resources";
                }
                else
                {
                    baseName = customAttribute.ResourceBaseName;
                }
            }
            foreach (string str in assembly.GetManifestResourceNames())
            {
                if (flag = str == (baseName + ".resources"))
                {
                    break;
                }
            }
            if (flag && ((manager = ResourceManagers[new object[] { assembly, baseName }] as ResourceManager) == null))
            {
                lock (resourceManagers)
                {
                    resourceManagers[new object[] { assembly, baseName }] = manager = new ResourceManager(baseName, assembly);
                }
            }
            return manager;
        }

        internal static string GetSafeString(string value)
        {
            return GetSafeString(new StringBuilder(value)).ToString();
        }

        internal static StringBuilder GetSafeString(StringBuilder value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!IsCharSafe(value[i]))
                {
                    value[i] = '_';
                }
            }
            return value;
        }

        internal static string GetString(string name, params object[] args)
        {
            string str = GetString(GetResourceManager(Assembly.GetCallingAssembly()), name, args);
            if (!IsEmpty(str))
            {
                return str;
            }
            return GetString(GetResourceManager(typeof(SharedUtil).Assembly), name, args);
        }

        internal static string GetString(ResourceManager resources, string name, params object[] args)
        {
            return GetString(resources, name, CultureInfo.CurrentUICulture, args);
        }

        internal static string GetString(string name, CultureInfo culture, params object[] args)
        {
            string str = GetString(GetResourceManager(Assembly.GetCallingAssembly()), name, culture, args);
            if (!IsEmpty(str))
            {
                return str;
            }
            return GetString(GetResourceManager(typeof(SharedUtil).Assembly), name, culture, args);
        }

        internal static string GetString(ResourceManager resources, string name, CultureInfo culture, params object[] args)
        {
            try
            {
                if (resources == null)
                {
                    resources = GetResourceManager(typeof(SharedUtil).Assembly);
                }
                string format = resources.GetString(name, culture);
                if (format == null)
                {
                    format = string.Empty;
                }
                format = format.Replace(@"\r", "\r").Replace(@"\n", "\n").Replace(@"\t", "\t");
                if ((args == null) || (args.Length == 0))
                {
                    return format;
                }
                return string.Format(format, args);
            }
            catch
            {
                return string.Empty;
            }
        }

        internal static string[] GetStrings(params string[] values)
        {
            return values;
        }

        internal static TReturn GetTag<TParam, TReturn>(TParam value) where TParam: Control where TReturn: class
        {
            if (value != null)
            {
                return (value.Tag as TReturn);
            }
            return default(TReturn);
        }

        internal static Predicate<TPredicate> GetTypeCheckPredicate<TCheck, TPredicate>()
        {
            return value => (value is TCheck);
        }

        internal static string GetTypeDescription(System.Type type)
        {
            string typeDescription = GetTypeDescription(Assembly.GetCallingAssembly(), type);
            if (IsEmpty(typeDescription))
            {
                try
                {
                    typeDescription = GetTypeDescription(type.Assembly, type);
                }
                catch
                {
                }
            }
            if (IsEmpty(typeDescription))
            {
                typeDescription = GetTypeDescription((ResourceManager) null, type);
            }
            return typeDescription;
        }

        internal static string GetTypeDescription(Assembly assembly, System.Type type)
        {
            return GetTypeDescription((assembly == null) ? null : GetResourceManager(assembly), type);
        }

        internal static string GetTypeDescription(ResourceManager resources, System.Type type)
        {
            string str;
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (resources == null)
            {
                resources = GetResourceManager(typeof(SharedUtil).Assembly);
            }
            if (IsEmpty(str = GetString(resources, "T_Desc_" + type.Name, new object[0])) && IsEmpty(str = GetString(resources, type.FullName + ".Description", new object[0])))
            {
                str = GetString(resources, type.Name + ".Description", new object[0]);
            }
            if (!IsEmpty(str))
            {
                return str;
            }
            if (type.BaseType != null)
            {
                return GetTypeDescription(resources, type.BaseType);
            }
            return string.Empty;
        }

        internal static Image GetTypeImage(System.Type type)
        {
            return GetTypeImage(GetResourceManager(type.Assembly), type);
        }

        internal static Image GetTypeImage(ResourceManager resources, System.Type type)
        {
            Image image = null;
            while (((image == null) && (type != null)) && (type != typeof(object)))
            {
                image = resources.GetObject("Image_" + type.Name) as Image;
                type = type.BaseType;
            }
            return image;
        }

        internal static System.Type[] GetTypes(params object[] args)
        {
            System.Type[] typeArray = new System.Type[(args == null) ? 0 : args.Length];
            for (int i = 0; i < typeArray.Length; i++)
            {
                typeArray[i] = (args[i] == null) ? null : args[i].GetType();
            }
            return typeArray;
        }

        internal static System.Type[] GetTypes(System.Type type, int length)
        {
            System.Type[] typeArray = new System.Type[length];
            for (int i = 0; i < length; i++)
            {
                typeArray[i] = type;
            }
            return typeArray;
        }

        internal static string GetTypeTitle(System.Type type)
        {
            return GetTypeTitle(type, null);
        }

        internal static string GetTypeTitle(Assembly assembly, System.Type type)
        {
            return GetTypeTitle(assembly, type, null);
        }

        internal static string GetTypeTitle(ResourceManager resources, System.Type type)
        {
            return GetTypeTitle(resources, type, null);
        }

        internal static string GetTypeTitle(System.Type type, CultureInfo culture)
        {
            string str = string.Empty;
            try
            {
                str = GetTypeTitle(Assembly.GetCallingAssembly(), type, culture);
            }
            catch
            {
            }
            if (IsEmpty(str))
            {
                try
                {
                    str = GetTypeTitle(type.Assembly, type, culture);
                }
                catch
                {
                }
            }
            if (IsEmpty(str))
            {
                str = GetTypeTitle((ResourceManager) null, type, culture);
            }
            if (str != null)
            {
                return str;
            }
            return string.Empty;
        }

        internal static string GetTypeTitle(Assembly assembly, System.Type type, CultureInfo culture)
        {
            return GetTypeTitle((assembly == null) ? null : GetResourceManager(assembly), type, culture);
        }

        internal static string GetTypeTitle(ResourceManager resources, System.Type type, CultureInfo culture)
        {
            string str;
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (resources == null)
            {
                resources = GetResourceManager(typeof(SharedUtil).Assembly);
            }
            if (culture == null)
            {
                if (IsEmpty(str = GetString(resources, "T_Title_" + type.Name, new object[0])) && IsEmpty(str = GetString(resources, type.Name + ".Title", new object[0])))
                {
                    str = GetString(resources, type.FullName + ".Title", new object[0]);
                }
            }
            else if (IsEmpty(str = GetString(resources, "T_Title_" + type.Name, culture, new object[0])) && IsEmpty(str = GetString(resources, type.Name + ".Title", culture, new object[0])))
            {
                str = GetString(resources, type.FullName + ".Title", culture, new object[0]);
            }
            if (!IsEmpty(str))
            {
                return str;
            }
            if (type.BaseType != null)
            {
                return GetTypeTitle(resources, type.BaseType, culture);
            }
            return string.Empty;
        }

        internal static string Hash(string value, bool bothMd5AndSha)
        {
            string str;
            string str2 = string.Empty;
            using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
            {
                str = Hash(value, provider, true, false);
            }
            return (str + str2);
        }

        internal static string Hash(string value, HashAlgorithm provider, bool replace, bool base64)
        {
            if (IsEmpty(value))
            {
                return string.Empty;
            }
            value = base64 ? Convert.ToBase64String(provider.ComputeHash(Encoding.UTF8.GetBytes(value)), Base64FormattingOptions.None) : Encoding.UTF8.GetString(provider.ComputeHash(Encoding.UTF8.GetBytes(value)));
            if (replace)
            {
                value = ReplaceCharacters(value, oldChar => !char.IsLetterOrDigit(oldChar), '_');
            }
            return value;
        }

        internal static bool In<T>(T value, IEnumerable<T> values)
        {
            if (values != null)
            {
                foreach (T local in values)
                {
                    if (Equals(value, local))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static bool In<T>(T value, params T[] values)
        {
            return (Array.IndexOf<T>(values, value) >= 0);
        }

        internal static int IndexOfOrLength(object value, char c)
        {
            return IndexOfOrLength((value == null) ? string.Empty : value.ToString(), c);
        }

        internal static int IndexOfOrLength(string value, char c)
        {
            int index = value.IndexOf(c);
            if (index > 0)
            {
                return index;
            }
            return value.Length;
        }

        internal static string InsertSpaces(string value)
        {
            StringBuilder builder = new StringBuilder();
            if ((value == null) || (value.Length == 0))
            {
                return string.Empty;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (((char.IsUpper(value, i) && (i > 0)) && ((i == (value.Length - 1)) || char.IsLower(value, i + 1))) && !char.IsWhiteSpace(value, i - 1))
                {
                    builder.Append(new char[] { ' ', value[i] });
                }
                else if (((char.IsNumber(value, i) && (i > 0)) && ((i == (value.Length - 1)) || char.IsLetter(value, i + 1))) && !char.IsWhiteSpace(value, i - 1))
                {
                    builder.Append(new char[] { ' ', value[i] });
                }
                else
                {
                    builder.Append(value, i, 1);
                }
            }
            return builder.ToString();
        }

        internal static bool InvokeIfRequired<TReturn>(Control control, Action action, ref TReturn result)
        {
            if ((control != null) && control.InvokeRequired)
            {
                result = (TReturn) control.Invoke(action);
                return true;
            }
            return false;
        }

        internal static bool InvokeIfRequired<TReturn, TParam>(Control control, Action<TParam> action, TParam param, ref TReturn result)
        {
            if ((control != null) && control.InvokeRequired)
            {
                result = (TReturn) control.Invoke(action, new object[] { param });
                return true;
            }
            return false;
        }

        internal static bool InvokeIfRequired<TReturn, TParam1, TParam2>(Control control, Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2, ref TReturn result)
        {
            if ((control != null) && control.InvokeRequired)
            {
                result = (TReturn) control.Invoke(action, new object[] { param1, param2 });
                return true;
            }
            return false;
        }

        internal static bool InvokeIfRequired<TReturn, TParam1, TParam2, TParam3>(Control control, Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3, ref TReturn result)
        {
            if ((control != null) && control.InvokeRequired)
            {
                result = (TReturn) control.Invoke(action, new object[] { param1, param2, param3 });
                return true;
            }
            return false;
        }

        internal static bool InvokeIfRequiredVoid(Control control, Action action)
        {
            if ((control != null) && control.InvokeRequired)
            {
                control.Invoke(action);
                return true;
            }
            return false;
        }

        internal static bool InvokeIfRequiredVoid<TParam>(Control control, Action<TParam> action, TParam param)
        {
            if ((control != null) && control.InvokeRequired)
            {
                control.Invoke(action, new object[] { param });
                return true;
            }
            return false;
        }

        internal static bool InvokeIfRequiredVoid<TParam1, TParam2>(Control control, Action<TParam1, TParam2> action, TParam1 param1, TParam2 param2)
        {
            if ((control != null) && control.InvokeRequired)
            {
                control.Invoke(action, new object[] { param1, param2 });
                return true;
            }
            return false;
        }

        internal static bool InvokeIfRequiredVoid<TParam1, TParam2, TParam3>(Control control, Action<TParam1, TParam2, TParam3> action, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            if ((control != null) && control.InvokeRequired)
            {
                control.Invoke(action, new object[] { param1, param2, param3 });
                return true;
            }
            return false;
        }

        internal static bool IsCharSafe(char value)
        {
            return ((char.IsNumber(value) || ((value >= 'A') && (value <= 'Z'))) || ((value >= 'a') && (value <= 'z')));
        }

        internal static bool IsEmpty<T, U>(Duo<T, U> tuple)
        {
            return ((tuple == null) || (IsEmpty(tuple.Value1) && IsEmpty(tuple.Value2)));
        }

        internal static bool IsEmpty(ICollection value)
        {
            if (value != null)
            {
                return (value.Count == 0);
            }
            return true;
        }

        internal static bool IsEmpty(object value)
        {
            if (value is ICollection)
            {
                return IsEmpty((ICollection) value);
            }
            if (value is string)
            {
                return IsEmpty((string) value);
            }
            return (value == null);
        }

        internal static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        internal static bool IsEmpty(Uri uri)
        {
            if (uri != null)
            {
                return IsEmpty(uri.ToString());
            }
            return true;
        }

        internal static bool IsEmpty(ref string value)
        {
            string str;
            value = str = Trim(value);
            return (str.Length == 0);
        }

        internal static bool IsEmpty(XmlNodeList list)
        {
            if (list != null)
            {
                return (list.Count == 0);
            }
            return true;
        }

        internal static bool IsEmpty(ICollection value, bool deep)
        {
            bool flag = (value == null) || (value.Count == 0);
            if (deep && !flag)
            {
                foreach (object obj2 in value)
                {
                    if (flag |= IsEmpty(obj2))
                    {
                        return flag;
                    }
                }
            }
            return flag;
        }

        internal static bool IsEmpty(string value, bool trim)
        {
            if (trim)
            {
                if (value != null)
                {
                    return (value.Trim().Length == 0);
                }
                return true;
            }
            if (value != null)
            {
                return (value.Length == 0);
            }
            return true;
        }

        internal static bool IsEmptyEnumerable<T>(IEnumerable<T> values)
        {
            if (values != null)
            {
                return IsEmpty((ICollection) new List<T>(values));
            }
            return true;
        }

        internal static bool IsOperator(MethodInfo method)
        {
            return (((method != null) && method.IsSpecialName) && method.Name.StartsWith("op_"));
        }

        internal static string Join(string separator, params object[] values)
        {
            string[] strArray = new string[IsEmpty((ICollection) values) ? 0 : values.Length];
            if (strArray.Length > 0)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    strArray[i] = (values[i] == null) ? string.Empty : values[i].ToString();
                }
            }
            return string.Join(separator, strArray);
        }

        internal static string Join<T>(string separator, IEnumerable<T> values, Operation<T, string> operation) where T: class
        {
            if (operation == null)
            {
                operation = new Operation<T, string>(SharedUtil.DefaultJoinOperation<T>);
            }
            return string.Join(separator, CreateArray<string, T>(values, operation));
        }

        internal static string JoinStruct<T>(string separator, IEnumerable<T> values, Operation<T, string> operation) where T: struct
        {
            if (operation == null)
            {
                operation = new Operation<T, string>(SharedUtil.DefaultJoinStructOperation<T>);
            }
            return string.Join(separator, CreateArray<string, T>(values, operation));
        }

        internal static T Last<T>(List<T> list)
        {
            return list[list.Count - 1];
        }

        internal static bool MatchAll<T>(List<T> one, List<T> two, Predicate<Duo<T>> match)
        {
            if (one.Count != two.Count)
            {
                return false;
            }
            for (int i = 0; i < one.Count; i++)
            {
                if (!match(new Duo<T>(one[i], two[i])))
                {
                    return false;
                }
            }
            return true;
        }

        internal static int ParseInt(object value, int defaultValue)
        {
            int num;
            if (!int.TryParse(value.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }

        internal static double Percent(double value, double hundred)
        {
            return ((value * 100.0) / hundred);
        }

        internal static int Percent(int value, int hundred)
        {
            return (int) Percent((double) value, (double) hundred);
        }

        internal static int Percent(long value, long hundred)
        {
            return (int) Percent((double) value, (double) hundred);
        }

        internal static double PercentOf(double percent, double of)
        {
            return ((of / 100.0) * percent);
        }

        internal static int PercentOf(int percent, int of)
        {
            return (int) PercentOf((double) percent, (double) of);
        }

        internal static long PercentOf(long percent, long of)
        {
            return (long) PercentOf((double) percent, (double) of);
        }

        internal static void RemoveDuplicates<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int num2;
                while ((num2 = list.IndexOf(list[i], i + 1)) > i)
                {
                    list.RemoveAt(num2);
                }
            }
        }

        internal static string RemoveSentences(string value, string sentenceClosingIndicator, int count, SeekOrigin seekOrigin)
        {
            if ((IsEmpty(value) || IsEmpty(sentenceClosingIndicator)) || ((count <= 0) || (seekOrigin == SeekOrigin.Current)))
            {
                return value;
            }
            List<string> list = new List<string>(value.Split(new string[] { sentenceClosingIndicator }, StringSplitOptions.RemoveEmptyEntries));
            if (count == list.Count)
            {
                list.Clear();
            }
            else if (list.Count > count)
            {
                for (int i = 0; i < count; i++)
                {
                    list.RemoveAt((seekOrigin == SeekOrigin.Begin) ? 0 : (list.Count - 1));
                }
            }
            if (list.Count != 1)
            {
                return string.Join(sentenceClosingIndicator, list.ToArray());
            }
            return (list[0] + sentenceClosingIndicator);
        }

        internal static string ReplaceCharacters(string value, Predicate<char> match, char newChar)
        {
            StringBuilder builder = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                builder.Append(match(value[i]) ? newChar : value[i]);
            }
            return builder.ToString();
        }

        internal static string ReplaceCharacters(string value, string characters, string newValue)
        {
            foreach (char ch in characters)
            {
                value = value.Replace(ch.ToString(), newValue);
            }
            return value;
        }

        internal static string ReplaceRepeatedly(string value, params string[] replaceValues)
        {
            if ((replaceValues != null) && (replaceValues.Length > 1))
            {
                for (int i = 0; i < (replaceValues.Length - 1); i++)
                {
                    while (value.IndexOf(replaceValues[i]) >= 0)
                    {
                        value = value.Replace(replaceValues[i], replaceValues[replaceValues.Length - 1]);
                    }
                }
            }
            return value;
        }

        internal static string Reverse(string value)
        {
            char[] array = value.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        internal static bool RunThread(Action action, Predicate doCancel)
        {
            bool aborted = false;
            Thread thread = new Thread(delegate() {
                try
                {
                    action();
                }
                catch (ThreadAbortException)
                {
                    aborted = true;
                }
            });
            try
            {
                thread.Start();
                while (thread.ThreadState != ThreadState.Stopped)
                {
                    if (aborted = doCancel())
                    {
                        thread.Abort();
                        goto Label_005D;
                    }
                }
            }
            catch (ThreadAbortException)
            {
                aborted = true;
            }
        Label_005D:
            return !aborted;
        }

        internal static void Serialize(SerializationInfo info, params object[] namesValues)
        {
            for (int i = 1; i < namesValues.Length; i += 2)
            {
                info.AddValue(namesValues[i - 1].ToString(), namesValues[i]);
            }
        }

        internal static byte[] SerializeBinary(object value)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, value);
                return stream.ToArray();
            }
        }

        internal static T[] SubArray<T>(T[] source, int index, int length)
        {
            T[] destinationArray = new T[length];
            Array.Copy(source, index, destinationArray, 0, (length > source.Length) ? source.Length : length);
            return destinationArray;
        }

        internal static string Substring(string value, int startIndex, int length)
        {
            if ((IsEmpty(value) || (startIndex < 0)) || ((length < 0) || (startIndex >= value.Length)))
            {
                return string.Empty;
            }
            if ((startIndex + length) < value.Length)
            {
                return value.Substring(startIndex, length);
            }
            return value.Substring(startIndex);
        }

        internal static void ThrowIfEmpty(ICollection value, string paramName)
        {
            ThrowIfEmpty(value, false, paramName);
        }

        internal static void ThrowIfEmpty(object value, string paramName)
        {
            if (IsEmpty(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        internal static void ThrowIfEmpty(string value, string paramName)
        {
            if (IsEmpty(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        internal static void ThrowIfEmpty(ICollection value, bool deep, string paramName)
        {
            if (IsEmpty(value, deep))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        internal static void ThrowIfNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        internal static void ThrowIfNull(ref string value, string paramName)
        {
            if (IsEmpty(ref value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        internal static string ToBase64String(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, value);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        internal static Hashtable ToDictionary(params object[] values)
        {
            Hashtable dictionary = new Hashtable((values == null) ? 0 : (values.Length / 2));
            ToDictionary(dictionary, values);
            return dictionary;
        }

        internal static void ToDictionary(IDictionary dictionary, params object[] values)
        {
            if (!IsEmpty((ICollection) values) && ((values.Length % 2) == 0))
            {
                for (int i = 1; i < values.Length; i += 2)
                {
                    dictionary[values[i - 1]] = values[i];
                }
            }
        }

        internal static string ToString(object value, string ifNull, TypeConverter converter)
        {
            if ((converter != null) && converter.CanConvertTo(typeof(string)))
            {
                return (converter.ConvertTo(value, typeof(string)) as string);
            }
            if (value != null)
            {
                return value.ToString();
            }
            return ifNull;
        }

        internal static string Trim(string value)
        {
            if (value != null)
            {
                return value.Trim();
            }
            return string.Empty;
        }

        internal static string TrimLength(string value, int maxLength)
        {
            return TrimLength(value, maxLength, true);
        }

        internal static string TrimLength(string value, int maxLength, bool ellipsis)
        {
            if (value == null)
            {
                return null;
            }
            if (value.Length > maxLength)
            {
                return (value.Substring(0, maxLength) + (ellipsis ? "…" : string.Empty));
            }
            return value;
        }

        internal static bool TrueForAll<T>(IEnumerable<T> values, Predicate<T> match)
        {
            foreach (T local in values)
            {
                if (!match(local))
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool TrueForAll<T>(IEnumerable values, Predicate<T> match) where T: class
        {
            foreach (object obj2 in values)
            {
                T local;
                if (((local = obj2 as T) != null) && !match(local))
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool TrueForAll<T>(Predicate<T> match, params T[] values)
        {
            return TrueForAll<T>(values, match);
        }

        internal static bool TrueForAny<T>(IEnumerable<T> values, Predicate<T> match)
        {
            foreach (T local in values)
            {
                if (match(local))
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool TrueForAny<T>(IEnumerable values, Predicate<T> match) where T: class
        {
            foreach (object obj2 in values)
            {
                T local;
                if (((local = obj2 as T) != null) && match(local))
                {
                    return true;
                }
            }
            return false;
        }

        internal static void Wait(TimeSpan duration, bool doEvents, params Action[] actions)
        {
            DateTime now = DateTime.Now;
            while ((DateTime.Now.Ticks - now.Ticks) < duration.Ticks)
            {
                if (doEvents)
                {
                    Application.DoEvents();
                }
            }
            if (actions != null)
            {
                foreach (Action action in actions)
                {
                    if (action != null)
                    {
                        action();
                    }
                }
            }
        }

        internal static void Walk<T>(IEnumerable values, Action<T> action) where T: class
        {
            if (values != null)
            {
                foreach (object obj2 in values)
                {
                    T local = obj2 as T;
                    if (local != null)
                    {
                        action(local);
                    }
                }
            }
        }

        private static HashTree ResourceManagers
        {
            get
            {
                if (resourceManagers == null)
                {
                    resourceManagers = new HashTree();
                }
                return resourceManagers;
            }
        }
    }
}

