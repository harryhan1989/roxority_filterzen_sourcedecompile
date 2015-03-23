namespace roxority.Shared.Win32
{
    using Microsoft.Win32;
    using roxority.Shared;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    internal sealed class RegistryUtil
    {
        internal const string DEFAULT_EXPORT_PREFACE = "Windows Registry Editor Version 5.00";

        private RegistryUtil()
        {
        }

        internal static object Deserialize(string name, object defaultValue)
        {
            return Deserialize(name, defaultValue, StreamingContextStates.Persistence);
        }

        internal static object Deserialize(RegistryKey key, string name, object defaultValue)
        {
            return Deserialize(key, name, defaultValue, StreamingContextStates.Persistence);
        }

        internal static object Deserialize(string name, object defaultValue, StreamingContextStates streamingContext)
        {
            return Deserialize(Application.UserAppDataRegistry, name, defaultValue, streamingContext);
        }

        internal static object Deserialize(RegistryKey key, string name, object defaultValue, StreamingContextStates streamingContext)
        {
            object obj2;
            BinaryFormatter formatter = new BinaryFormatter(null, new StreamingContext(streamingContext));
            try
            {
                byte[] buffer;
                if (SharedUtil.IsEmpty((ICollection) (buffer = GetBytes(key, name, null))))
                {
                    return defaultValue;
                }
                using (MemoryStream stream = new MemoryStream(buffer, false))
                {
                    object obj3;
                    if (((obj3 = formatter.Deserialize(stream)) != null) && ((defaultValue == null) || defaultValue.GetType().IsAssignableFrom(obj3.GetType())))
                    {
                        return obj3;
                    }
                    obj2 = defaultValue;
                }
            }
            catch
            {
                obj2 = defaultValue;
            }
            return obj2;
        }

        internal static void Export(RegistryKey key, string filePath)
        {
            Export(key, filePath, true, "Windows Registry Editor Version 5.00", Encoding.Unicode);
        }

        internal static void Export(RegistryKey key, string filePath, bool recursive, string preface, Encoding encoding)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (SharedUtil.IsEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            if (SharedUtil.IsEmpty(preface))
            {
                preface = "Windows Registry Editor Version 5.00";
            }
            using (StreamWriter writer = new StreamWriter(filePath, false, encoding))
            {
                writer.WriteLine(preface);
                ExportKey(key, recursive, writer);
            }
        }

        internal static void ExportKey(RegistryKey key, bool recursive, StreamWriter writer)
        {
            string[] strArray;
            string[] strArray2;
            writer.WriteLine("\r\n[{0}]", TrimHex(key.ToString()));
            ExportValue("@", key.GetValue(null), writer, false);
            if (!SharedUtil.IsEmpty((ICollection) (strArray2 = key.GetValueNames())))
            {
                foreach (string str in strArray2)
                {
                    ExportValue(str, key.GetValue(str), writer, false);
                }
            }
            if (recursive && !SharedUtil.IsEmpty((ICollection) (strArray = key.GetSubKeyNames())))
            {
                foreach (string str2 in strArray)
                {
                    using (RegistryKey key2 = key.OpenSubKey(str2, false))
                    {
                        ExportKey(key2, true, writer);
                    }
                }
            }
        }

        internal static void ExportValue(string valueName, int value, StreamWriter writer)
        {
            writer.WriteLine("\"{0}\"=dword:{1}", Normalize(valueName), value.ToString("x"));
        }

        internal static void ExportValue(string valueName, byte[] value, StreamWriter writer)
        {
            StringBuilder builder = new StringBuilder("\"" + Normalize(valueName) + "\"=hex:");
            if (!SharedUtil.IsEmpty((ICollection) value))
            {
                builder.Append(value[0].ToString("x"));
            }
            if (value.Length > 1)
            {
                for (int i = 1; i < value.Length; i++)
                {
                    builder.AppendFormat(",{0}", value[i].ToString("x"));
                }
            }
            writer.WriteLine(builder.ToString());
        }

        internal static void ExportValue(string valueName, string value, StreamWriter writer)
        {
            writer.WriteLine("\"{0}\"=\"{1}\"", Normalize(valueName), Normalize(value));
        }

        internal static void ExportValue(string valueName, object value, StreamWriter writer, bool @throw)
        {
            if ((value == null) && @throw)
            {
                throw new ArgumentNullException("value");
            }
            if (value != null)
            {
                if (value is string)
                {
                    ExportValue(valueName, value as string, writer);
                }
                else if (value is int)
                {
                    ExportValue(valueName, (int) value, writer);
                }
                else if (value is byte[])
                {
                    ExportValue(valueName, value as byte[], writer);
                }
                else if (@throw)
                {
                    throw new ArgumentException(null, "value");
                }
            }
        }

        internal static bool GetBoolean(string name, bool defaultValue)
        {
            return GetBoolean(Application.UserAppDataRegistry, name, defaultValue);
        }

        internal static bool GetBoolean(RegistryKey key, string name, bool defaultValue)
        {
            try
            {
                return bool.Parse(GetString(key, name, defaultValue.ToString()));
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static byte[] GetBytes(string name, params byte[] defaultValue)
        {
            return GetBytes(Application.UserAppDataRegistry, name, defaultValue);
        }

        internal static byte[] GetBytes(RegistryKey key, string name, params byte[] defaultValue)
        {
            try
            {
                return (byte[]) key.GetValue(name, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static Enum GetEnum(string name, Enum defaultValue)
        {
            return GetEnum(Application.UserAppDataRegistry, name, defaultValue);
        }

        internal static Enum GetEnum(RegistryKey key, string name, Enum defaultValue)
        {
            try
            {
                Enum enum2 = (Enum) Enum.Parse(defaultValue.GetType(), GetString(key, name, defaultValue.ToString()), true);
                return ((enum2.GetType() == defaultValue.GetType()) ? enum2 : defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static int GetInt32(string name, int defaultValue)
        {
            return GetInt32(Application.UserAppDataRegistry, name, defaultValue);
        }

        internal static int GetInt32(RegistryKey key, string name, int defaultValue)
        {
            try
            {
                return (int) key.GetValue(name, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static string GetString(string name, string defaultValue)
        {
            return GetString(Application.UserAppDataRegistry, name, defaultValue);
        }

        internal static string GetString(RegistryKey key, string name, string defaultValue)
        {
            try
            {
                return (string) key.GetValue(name, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static string GetString(string name, string defaultValue, string entropy)
        {
            return GetString(Application.UserAppDataRegistry, name, defaultValue, entropy);
        }

        internal static string GetString(RegistryKey key, string name, string defaultValue, string entropy)
        {
            try
            {
                return Encoding.Unicode.GetString(ProtectedData.Unprotect(GetBytes(key, name, Encoding.Unicode.GetBytes(defaultValue)), Encoding.Unicode.GetBytes(entropy), DataProtectionScope.CurrentUser));
            }
            catch
            {
                return null;
            }
        }

        internal static string[] GetStrings(string name, params string[] defaultValue)
        {
            return GetStrings(Application.UserAppDataRegistry, name, defaultValue);
        }

        internal static string[] GetStrings(RegistryKey key, string name, params string[] defaultValue)
        {
            try
            {
                return (string[]) key.GetValue(name, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static object GetValue(string name, object defaultValue)
        {
            return GetValue(Application.UserAppDataRegistry, name, defaultValue);
        }

        internal static object GetValue(RegistryKey key, string name, object defaultValue)
        {
            try
            {
                TypeConverter converter;
                object obj2;
                if (((defaultValue == null) && ((obj2 = Deserialize(key, name, defaultValue)) == null)) || (defaultValue is string))
                {
                    return GetString(key, name, (defaultValue == null) ? null : defaultValue.ToString());
                }
                if (defaultValue is string[])
                {
                    return GetStrings(key, name, (string[]) defaultValue);
                }
                if (defaultValue is bool)
                {
                    return GetBoolean(key, name, (bool) defaultValue);
                }
                if (defaultValue is byte[])
                {
                    return GetBytes(key, name, (byte[]) defaultValue);
                }
                if (defaultValue is Enum)
                {
                    return GetEnum(key, name, (Enum) defaultValue);
                }
                if (defaultValue is int)
                {
                    return GetInt32(key, name, (int) defaultValue);
                }
                if ((((defaultValue != null) && ((converter = TypeDescriptor.GetConverter(defaultValue)) != null)) && (converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(typeof(string)))) && (((obj2 = converter.ConvertFrom(null, CultureInfo.InvariantCulture, GetString(key, name, (string) converter.ConvertTo(null, CultureInfo.InvariantCulture, defaultValue, typeof(string))))) != null) && defaultValue.GetType().IsAssignableFrom(obj2.GetType())))
                {
                    return obj2;
                }
                return Deserialize(key, name, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static string Normalize(string value)
        {
            return value.Replace(@"\", @"\\").Replace("\"", "\\\"");
        }

        internal static void Serialize(string name, object value)
        {
            Serialize(Application.UserAppDataRegistry, name, value);
        }

        internal static void Serialize(RegistryKey key, string name, object value)
        {
            Serialize(key, name, value, StreamingContextStates.Persistence);
        }

        internal static void Serialize(string name, object value, StreamingContextStates streamingContext)
        {
            Serialize(Application.UserAppDataRegistry, name, value, streamingContext);
        }

        internal static void Serialize(RegistryKey key, string name, object value, StreamingContextStates streamingContext)
        {
            BinaryFormatter formatter = new BinaryFormatter(null, new StreamingContext(streamingContext));
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, value);
                SetBytes(key, name, stream.ToArray());
            }
        }

        internal static void SetBoolean(string name, bool value)
        {
            SetBoolean(Application.UserAppDataRegistry, name, value);
        }

        internal static void SetBoolean(RegistryKey key, string name, bool value)
        {
            try
            {
                SetString(key, name, value.ToString());
            }
            catch
            {
            }
        }

        internal static void SetBytes(string name, params byte[] value)
        {
            SetBytes(Application.UserAppDataRegistry, name, value);
        }

        internal static void SetBytes(RegistryKey key, string name, params byte[] value)
        {
            try
            {
                key.SetValue(name, value);
            }
            catch
            {
            }
        }

        internal static void SetEnum(string name, Enum value)
        {
            SetEnum(Application.UserAppDataRegistry, name, value);
        }

        internal static void SetEnum(RegistryKey key, string name, Enum value)
        {
            try
            {
                SetString(key, name, value.ToString());
            }
            catch
            {
            }
        }

        internal static void SetInt32(string name, int value)
        {
            SetInt32(Application.UserAppDataRegistry, name, value);
        }

        internal static void SetInt32(RegistryKey key, string name, int value)
        {
            try
            {
                key.SetValue(name, value);
            }
            catch
            {
            }
        }

        internal static void SetString(string name, string value)
        {
            SetString(Application.UserAppDataRegistry, name, value);
        }

        internal static void SetString(RegistryKey key, string name, string value)
        {
            try
            {
                if ((value == null) && (Array.IndexOf<string>(key.GetValueNames(), name) >= 0))
                {
                    key.DeleteValue(name);
                }
                else if (value != null)
                {
                    key.SetValue(name, value);
                }
            }
            catch
            {
            }
        }

        internal static void SetString(string name, string value, string entropy)
        {
            SetString(Application.UserAppDataRegistry, name, value, entropy);
        }

        internal static void SetString(RegistryKey key, string name, string value, string entropy)
        {
            SetBytes(key, name, ProtectedData.Protect(Encoding.Unicode.GetBytes(value), Encoding.Unicode.GetBytes(entropy), DataProtectionScope.CurrentUser));
        }

        internal static void SetStrings(string name, params string[] value)
        {
            SetStrings(Application.UserAppDataRegistry, name, value);
        }

        internal static void SetStrings(RegistryKey key, string name, params string[] value)
        {
            try
            {
                if (value == null)
                {
                    key.DeleteValue(name, false);
                }
                else
                {
                    key.SetValue(name, value);
                }
            }
            catch
            {
            }
        }

        internal static void SetValue(string name, object value)
        {
            SetValue(Application.UserAppDataRegistry, name, value);
        }

        internal static void SetValue(RegistryKey key, string name, object value)
        {
            TypeConverter converter = (value == null) ? null : TypeDescriptor.GetConverter(value);
            if (value == null)
            {
                key.DeleteValue(name, false);
            }
            else if (value is string)
            {
                SetString(key, name, (string) value);
            }
            else if (value is string[])
            {
                SetStrings(key, name, (string[]) value);
            }
            else if (value is bool)
            {
                SetBoolean(key, name, (bool) value);
            }
            else if (value is byte[])
            {
                SetBytes(key, name, (byte[]) value);
            }
            else if (value is Enum)
            {
                SetEnum(key, name, (Enum) value);
            }
            else if (value is int)
            {
                SetInt32(key, name, (int) value);
            }
            else
            {
                string str;
                if (((converter != null) && converter.CanConvertFrom(typeof(string))) && (converter.CanConvertTo(typeof(string)) && ((str = converter.ConvertTo(null, CultureInfo.InvariantCulture, value, typeof(string)) as string) != null)))
                {
                    SetString(key, name, str);
                }
                else
                {
                    Serialize(key, name, value);
                }
            }
        }

        private static string TrimHex(string keyName)
        {
            int num;
            if (SharedUtil.IsEmpty(keyName))
            {
                throw new ArgumentNullException("keyName");
            }
            if (((keyName.LastIndexOf(']') == (keyName.Length - 1)) && ((num = keyName.LastIndexOf(" [0x")) > 0)) && (num < (keyName.Length - 1)))
            {
                return keyName.Substring(0, num);
            }
            return keyName;
        }
    }
}

