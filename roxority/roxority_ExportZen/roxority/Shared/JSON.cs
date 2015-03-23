namespace roxority.Shared
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;

    public class JSON
    {
        protected static JSON instance = new JSON();
        protected string lastDecode = "";
        protected int lastErrorIndex = -1;
        protected bool ordered;
        public const int TOKEN_COLON = 5;
        public const int TOKEN_COMMA = 6;
        public const int TOKEN_CURLY_CLOSE = 2;
        public const int TOKEN_CURLY_OPEN = 1;
        public const int TOKEN_FALSE = 10;
        public const int TOKEN_NONE = 0;
        public const int TOKEN_NULL = 11;
        public const int TOKEN_NUMBER = 8;
        public const int TOKEN_SQUARED_CLOSE = 4;
        public const int TOKEN_SQUARED_OPEN = 3;
        public const int TOKEN_STRING = 7;
        public const int TOKEN_TRUE = 9;

        public static object ConvertToGeneric(object v)
        {
            IList list = v as IList;
            IDictionary dictionary = v as IDictionary;
            Type type = null;
            Type type2 = null;
            if (dictionary != null)
            {
                foreach (DictionaryEntry entry in dictionary)
                {
                    if (entry.Key != null)
                    {
                        if (type == null)
                        {
                            type = entry.Key.GetType();
                        }
                        else if (type != entry.Key.GetType())
                        {
                            type = typeof(object);
                        }
                    }
                    if (entry.Value != null)
                    {
                        if (type2 == null)
                        {
                            type2 = entry.Value.GetType();
                        }
                        else if (type2 != entry.Value.GetType())
                        {
                            type2 = typeof(object);
                        }
                    }
                }
                if ((type == null) || (type2 == null))
                {
                    return v;
                }
                IDictionary dictionary2 = Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(new Type[] { type, type2 })) as IDictionary;
                foreach (DictionaryEntry entry2 in dictionary)
                {
                    object introduced20 = ConvertToGeneric(entry2.Key);
                    dictionary2[introduced20] = ConvertToGeneric(entry2.Value);
                }
                return dictionary2;
            }
            if (list == null)
            {
                return v;
            }
            foreach (object obj2 in list)
            {
                if (obj2 != null)
                {
                    if (type2 == null)
                    {
                        type2 = obj2.GetType();
                    }
                    else if (type2 != obj2.GetType())
                    {
                        type2 = typeof(object);
                    }
                }
            }
            if (type2 == null)
            {
                return v;
            }
            IList list2 = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { type2 })) as IList;
            foreach (object obj3 in list)
            {
                list2.Add(ConvertToGeneric(obj3));
            }
            return list2;
        }

        protected void EatWhitespace(char[] json, ref int index)
        {
            while (index < json.Length)
            {
                if (" \t\n\r".IndexOf(json[index]) == -1)
                {
                    return;
                }
                index++;
            }
        }

        public static int GetLastErrorIndex()
        {
            return instance.lastErrorIndex;
        }

        public static string GetLastErrorSnippet()
        {
            if (instance.lastErrorIndex == -1)
            {
                return "";
            }
            int startIndex = instance.lastErrorIndex - 5;
            int num2 = instance.lastErrorIndex + 15;
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            if (num2 >= instance.lastDecode.Length)
            {
                num2 = instance.lastDecode.Length - 1;
            }
            return instance.lastDecode.Substring(startIndex, (num2 - startIndex) + 1);
        }

        protected int GetLastIndexOfNumber(char[] json, int index)
        {
            int num = index;
            while (num < json.Length)
            {
                if ("0123456789+-.eE".IndexOf(json[num]) == -1)
                {
                    break;
                }
                num++;
            }
            return (num - 1);
        }

        protected bool IsNumeric(object o)
        {
            decimal num;
            if (o == null)
            {
                return false;
            }
            if (!decimal.TryParse(o.ToString(), out num))
            {
                double num2;
                return double.TryParse(o.ToString(), out num2);
            }
            return true;
        }

        public static T JsonDecode<T>(string json) where T: class, ISerializable
        {
            return (JsonDecode(json, typeof(T)) as T);
        }

        public static object JsonDecode(string json)
        {
            return JsonDecode(json, null);
        }

        public static object JsonDecode(string json, Type returnType)
        {
            ConstructorInfo info;
            IDictionary dictionary;
            instance.lastDecode = json;
            if (json == null)
            {
                return null;
            }
            char[] chArray = json.ToCharArray();
            int index = 0;
            bool success = true;
            bool ordered = instance.ordered;
            if (instance.ordered = returnType == typeof(OrderedDictionary))
            {
                returnType = null;
            }
            object obj3 = instance.ParseValue(chArray, ref index, ref success);
            instance.ordered = ordered;
            if (success)
            {
                instance.lastErrorIndex = -1;
            }
            else
            {
                instance.lastErrorIndex = index;
            }
            if (((returnType == null) || ((dictionary = obj3 as IDictionary) == null)) || ((info = returnType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(SerializationInfo), typeof(StreamingContext) }, null)) == null))
            {
                return obj3;
            }
            SerializationInfo info2 = new SerializationInfo(returnType, new FormatterConverter());
            foreach (DictionaryEntry entry in dictionary)
            {
                object obj2;
                if (((entry.Key != null) && (entry.Value != null)) && ((obj2 = ConvertToGeneric(entry.Value)) != null))
                {
                    info2.AddValue(entry.Key.ToString(), obj2, obj2.GetType());
                }
            }
            return info.Invoke(new object[] { info2, new StreamingContext(StreamingContextStates.Persistence) });
        }

        public static string JsonEncode(object json)
        {
            StringBuilder builder = new StringBuilder();
            if (!instance.SerializeValue(json, builder))
            {
                return null;
            }
            return builder.ToString();
        }

        public static bool LastDecodeSuccessful()
        {
            return (instance.lastErrorIndex == -1);
        }

        protected int LookAhead(char[] json, int index)
        {
            int num = index;
            return this.NextToken(json, ref num);
        }

        protected int NextToken(char[] json, ref int index)
        {
            this.EatWhitespace(json, ref index);
            if (index != json.Length)
            {
                char ch = json[index];
                index++;
                switch (ch)
                {
                    case '"':
                        return 7;

                    case ',':
                        return 6;

                    case '-':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        return 8;

                    case ':':
                        return 5;

                    case '[':
                        return 3;

                    case ']':
                        return 4;

                    case '{':
                        return 1;

                    case '}':
                        return 2;
                }
                index--;
                int num = json.Length - index;
                if ((((num >= 5) && (json[index] == 'f')) && ((json[index + 1] == 'a') && (json[index + 2] == 'l'))) && ((json[index + 3] == 's') && (json[index + 4] == 'e')))
                {
                    index += 5;
                    return 10;
                }
                if ((((num >= 4) && (json[index] == 't')) && ((json[index + 1] == 'r') && (json[index + 2] == 'u'))) && (json[index + 3] == 'e'))
                {
                    index += 4;
                    return 9;
                }
                if ((((num >= 4) && (json[index] == 'n')) && ((json[index + 1] == 'u') && (json[index + 2] == 'l'))) && (json[index + 3] == 'l'))
                {
                    index += 4;
                    return 11;
                }
            }
            return 0;
        }

        protected ArrayList ParseArray(char[] json, ref int index)
        {
            ArrayList list = new ArrayList();
            this.NextToken(json, ref index);
            bool flag = false;
            while (!flag)
            {
                int num = this.LookAhead(json, index);
                if (num == 0)
                {
                    return null;
                }
                if (num == 6)
                {
                    this.NextToken(json, ref index);
                }
                else
                {
                    if (num == 4)
                    {
                        this.NextToken(json, ref index);
                        return list;
                    }
                    bool success = true;
                    object obj2 = this.ParseValue(json, ref index, ref success);
                    if (!success)
                    {
                        return null;
                    }
                    list.Add(obj2);
                }
            }
            return list;
        }

        protected object ParseNumber(char[] json, ref int index)
        {
            string str;
            int num;
            double num2;
            this.EatWhitespace(json, ref index);
            int lastIndexOfNumber = this.GetLastIndexOfNumber(json, index);
            int length = (lastIndexOfNumber - index) + 1;
            char[] destinationArray = new char[length];
            Array.Copy(json, index, destinationArray, 0, length);
            index = lastIndexOfNumber + 1;
            if (int.TryParse(str = new string(destinationArray), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                return num;
            }
            if (double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out num2))
            {
                return num2;
            }
            return decimal.Parse(str, CultureInfo.InvariantCulture);
        }

        protected IDictionary ParseObject(char[] json, ref int index)
        {
            IDictionary dictionary = this.ordered ? ((IDictionary) new OrderedDictionary()) : ((IDictionary) new Hashtable());
            this.NextToken(json, ref index);
            bool flag = false;
            while (!flag)
            {
                switch (this.LookAhead(json, index))
                {
                    case 0:
                        return null;

                    case 6:
                    {
                        this.NextToken(json, ref index);
                        continue;
                    }
                    case 2:
                        this.NextToken(json, ref index);
                        return dictionary;
                }
                string str = this.ParseString(json, ref index);
                if (str == null)
                {
                    return null;
                }
                if (this.NextToken(json, ref index) != 5)
                {
                    return null;
                }
                bool success = true;
                object obj2 = this.ParseValue(json, ref index, ref success);
                if (!success)
                {
                    return null;
                }
                dictionary[str] = obj2;
            }
            return dictionary;
        }

        protected string ParseString(char[] json, ref int index)
        {
            string str = "";
            this.EatWhitespace(json, ref index);
            char ch = json[index++];
            bool flag = false;
            while (!flag)
            {
                if (index == json.Length)
                {
                    break;
                }
                ch = json[index++];
                if (ch == '"')
                {
                    flag = true;
                    break;
                }
                if (ch == '\\')
                {
                    if (index == json.Length)
                    {
                        break;
                    }
                    ch = json[index++];
                    if (ch == '"')
                    {
                        str = str + '"';
                    }
                    else
                    {
                        if (ch == '\\')
                        {
                            str = str + '\\';
                            continue;
                        }
                        if (ch == '/')
                        {
                            str = str + '/';
                            continue;
                        }
                        if (ch == 'b')
                        {
                            str = str + '\b';
                            continue;
                        }
                        if (ch == 'f')
                        {
                            str = str + '\f';
                            continue;
                        }
                        if (ch == 'n')
                        {
                            str = str + '\n';
                            continue;
                        }
                        if (ch == 'r')
                        {
                            str = str + '\r';
                            continue;
                        }
                        if (ch == 't')
                        {
                            str = str + '\t';
                        }
                        else if (ch == 'u')
                        {
                            int num = json.Length - index;
                            if (num < 4)
                            {
                                break;
                            }
                            char[] destinationArray = new char[4];
                            Array.Copy(json, index, destinationArray, 0, 4);
                            uint num2 = uint.Parse(new string(destinationArray), NumberStyles.HexNumber);
                            str = str + char.ConvertFromUtf32((int) num2);
                            index += 4;
                        }
                    }
                }
                else
                {
                    str = str + ch;
                }
            }
            if (!flag)
            {
                return null;
            }
            return str;
        }

        protected object ParseValue(char[] json, ref int index, ref bool success)
        {
            switch (this.LookAhead(json, index))
            {
                case 1:
                    return this.ParseObject(json, ref index);

                case 3:
                    return this.ParseArray(json, ref index);

                case 7:
                    return this.ParseString(json, ref index);

                case 8:
                    return this.ParseNumber(json, ref index);

                case 9:
                    this.NextToken(json, ref index);
                    return bool.Parse("TRUE");

                case 10:
                    this.NextToken(json, ref index);
                    return bool.Parse("FALSE");

                case 11:
                    this.NextToken(json, ref index);
                    return null;
            }
            success = false;
            return null;
        }

        protected bool SerializeArray(IList anArray, StringBuilder builder)
        {
            builder.Append("[");
            bool flag = true;
            for (int i = 0; i < anArray.Count; i++)
            {
                object obj2 = anArray[i];
                if (!flag)
                {
                    builder.Append(",");
                }
                if (!this.SerializeValue(obj2, builder))
                {
                    return false;
                }
                flag = false;
            }
            builder.Append("]");
            return true;
        }

        protected void SerializeNumber(double number, StringBuilder builder)
        {
            builder.Append(Convert.ToString(number, CultureInfo.InvariantCulture));
        }

        protected bool SerializeObject(IDictionary anObject, StringBuilder builder)
        {
            builder.Append("{");
            IDictionaryEnumerator enumerator = anObject.GetEnumerator();
            for (bool flag = true; enumerator.MoveNext(); flag = false)
            {
                string aString = enumerator.Key.ToString();
                object obj2 = enumerator.Value;
                if (!flag)
                {
                    builder.Append(",");
                }
                this.SerializeString(aString, builder);
                builder.Append(":");
                if (!this.SerializeValue(obj2, builder))
                {
                    return false;
                }
            }
            builder.Append("}");
            return true;
        }

        protected bool SerializeObjectOrArray(object objectOrArray, StringBuilder builder)
        {
            if (objectOrArray is IDictionary)
            {
                return this.SerializeObject((IDictionary) objectOrArray, builder);
            }
            if (objectOrArray is IList)
            {
                return this.SerializeArray((IList) objectOrArray, builder);
            }
            return ((objectOrArray is ISerializable) && this.SerializeSerializable((ISerializable) objectOrArray, builder));
        }

        protected bool SerializePair(object kvp, StringBuilder builder)
        {
            Hashtable anObject = new Hashtable(2);
            anObject["k"] = kvp.GetType().GetProperty("Key").GetValue(kvp, null);
            anObject["v"] = kvp.GetType().GetProperty("Value").GetValue(kvp, null);
            return this.SerializeObject(anObject, builder);
        }

        protected bool SerializeSerializable(ISerializable obj, StringBuilder builder)
        {
            IDictionary anObject = this.ordered ? ((IDictionary) new OrderedDictionary()) : ((IDictionary) new Hashtable());
            SerializationInfo info = new SerializationInfo(obj.GetType(), new FormatterConverter());
            obj.GetObjectData(info, new StreamingContext(StreamingContextStates.Persistence));
            SerializationInfoEnumerator enumerator = info.GetEnumerator();
            while (enumerator.MoveNext())
            {
                SerializationEntry current = enumerator.Current;
                anObject[current.Name] = current.Value;
            }
            return this.SerializeObject(anObject, builder);
        }

        protected void SerializeString(string aString, StringBuilder builder)
        {
            builder.Append("\"");
            foreach (char ch in aString.ToCharArray())
            {
                switch (ch)
                {
                    case '"':
                        builder.Append("\\\"");
                        break;

                    case '\\':
                        builder.Append(@"\\");
                        break;

                    case '\b':
                        builder.Append(@"\b");
                        break;

                    case '\f':
                        builder.Append(@"\f");
                        break;

                    case '\n':
                        builder.Append(@"\n");
                        break;

                    case '\r':
                        builder.Append(@"\r");
                        break;

                    case '\t':
                        builder.Append(@"\t");
                        break;

                    default:
                    {
                        int num2 = Convert.ToInt32(ch);
                        if ((num2 >= 0x20) && (num2 <= 0x7e))
                        {
                            builder.Append(ch);
                        }
                        else
                        {
                            builder.Append(@"\u" + Convert.ToString(num2, 0x10).PadLeft(4, '0'));
                        }
                        break;
                    }
                }
            }
            builder.Append("\"");
        }

        protected bool SerializeValue(object value, StringBuilder builder)
        {
            if (value is string)
            {
                this.SerializeString((string) value, builder);
            }
            else if (value is IDictionary)
            {
                this.SerializeObject((IDictionary) value, builder);
            }
            else if (value is IList)
            {
                this.SerializeArray((IList) value, builder);
            }
            else if (value is ISerializable)
            {
                this.SerializeSerializable((ISerializable) value, builder);
            }
            else if (this.IsNumeric(value))
            {
                this.SerializeNumber(Convert.ToDouble(value), builder);
            }
            else if ((value is bool) && ((bool) value))
            {
                builder.Append("true");
            }
            else if ((value is bool) && !((bool) value))
            {
                builder.Append("false");
            }
            else if (value is Enum)
            {
                this.SerializeString(value.ToString(), builder);
            }
            else if (value == null)
            {
                builder.Append("null");
            }
            else if (value.GetType().FullName.StartsWith("System.Collections.Generic.KeyValuePair`2[["))
            {
                this.SerializePair(value, builder);
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}

