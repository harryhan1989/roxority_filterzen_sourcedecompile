namespace roxority.SharePoint
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class Reflector
    {
        public readonly System.Reflection.Assembly Assembly;
        private static Reflector current;
        private Dictionary<System.Type, Dictionary<string, MemberInfo>> memberInfos = new Dictionary<System.Type, Dictionary<string, MemberInfo>>();
        private Dictionary<string, System.Type> types = new Dictionary<string, System.Type>();

        public Reflector(System.Reflection.Assembly assembly)
        {
            this.Assembly = assembly;
        }

        public object Call(System.Type type, string name, params object[] args)
        {
            MemberInfo info;
            Dictionary<string, MemberInfo> dictionary;
            if (!this.MemberInfos.TryGetValue(type, out dictionary) || (dictionary == null))
            {
                this.MemberInfos[type] = dictionary = new Dictionary<string, MemberInfo>();
            }
            if (!dictionary.TryGetValue(name, out info))
            {
                dictionary[name] = info = type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            }
            MethodInfo info2 = info as MethodInfo;
            if (info2 != null)
            {
                return info2.Invoke(null, args);
            }
            return null;
        }

        public object Call(object obj, string name, System.Type[] argTypes, object[] args)
        {
            System.Type[] types = (argTypes != null) ? argTypes : new System.Type[(args == null) ? 0 : args.Length];
            if (obj != null)
            {
                System.Type type;
                MemberInfo info;
                Dictionary<string, MemberInfo> dictionary;
                if (argTypes == null)
                {
                    if ((args == null) || (args.Length == 0))
                    {
                        argTypes = System.Type.EmptyTypes;
                    }
                    else
                    {
                        for (int i = 0; i < args.Length; i++)
                        {
                            types[i] = (args[i] == null) ? typeof(object) : args[i].GetType();
                        }
                    }
                }
                if (!this.MemberInfos.TryGetValue(type = obj.GetType(), out dictionary) || (dictionary == null))
                {
                    this.MemberInfos[type] = dictionary = new Dictionary<string, MemberInfo>();
                }
                if (!dictionary.TryGetValue(name, out info))
                {
                    dictionary[name] = info = type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, types, null);
                }
                MethodInfo info2 = info as MethodInfo;
                if (info2 != null)
                {
                    return info2.Invoke(obj, args);
                }
            }
            return null;
        }

        public object Get(object obj, string name, params object[] args)
        {
            if (obj != null)
            {
                System.Type type;
                MemberInfo[] infoArray;
                MemberInfo info;
                Dictionary<string, MemberInfo> dictionary;
                if (!this.MemberInfos.TryGetValue(type = obj.GetType(), out dictionary) || (dictionary == null))
                {
                    this.MemberInfos[type] = dictionary = new Dictionary<string, MemberInfo>();
                }
                if (!dictionary.TryGetValue(name, out info) && ((infoArray = type.GetMember(name, MemberTypes.Property | MemberTypes.Field, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)) != null))
                {
                    MemberInfo[] infoArray2 = infoArray;
                    int index = 0;
                    while (index < infoArray2.Length)
                    {
                        MemberInfo info4 = infoArray2[index];
                        dictionary[name] = info = info4;
                        break;
                    }
                }
                FieldInfo info3 = info as FieldInfo;
                if (info3 != null)
                {
                    return info3.GetValue(obj);
                }
                PropertyInfo info2 = info as PropertyInfo;
                if (info2 != null)
                {
                    return info2.GetValue(obj, ((args != null) && (args.Length == 0)) ? null : args);
                }
            }
            return null;
        }

        public System.Type GetType(string typeName)
        {
            System.Type type;
            if (!this.types.TryGetValue(typeName.ToLowerInvariant(), out type))
            {
                this.types[typeName.ToLowerInvariant()] = type = this.Assembly.GetType(typeName, false, true);
            }
            return type;
        }

        public object New(string typeName, params object[] args)
        {
            System.Type type = this.GetType(typeName);
            if (type != null)
            {
                return Activator.CreateInstance(type, args);
            }
            return null;
        }

        public void Set(object obj, string name, object value, params object[] args)
        {
            if (obj != null)
            {
                System.Type type;
                MemberInfo[] infoArray;
                MemberInfo info;
                Dictionary<string, MemberInfo> dictionary;
                if (!this.MemberInfos.TryGetValue(type = obj.GetType(), out dictionary) || (dictionary == null))
                {
                    this.MemberInfos[type] = dictionary = new Dictionary<string, MemberInfo>();
                }
                if (!dictionary.TryGetValue(name, out info) && ((infoArray = type.GetMember(name, MemberTypes.Property | MemberTypes.Field, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)) != null))
                {
                    MemberInfo[] infoArray2 = infoArray;
                    int index = 0;
                    while (index < infoArray2.Length)
                    {
                        MemberInfo info4 = infoArray2[index];
                        dictionary[name] = info = info4;
                        break;
                    }
                }
                FieldInfo info3 = info as FieldInfo;
                if (info3 != null)
                {
                    info3.SetValue(obj, value);
                }
                else
                {
                    PropertyInfo info2 = info as PropertyInfo;
                    if (info2 != null)
                    {
                        info2.SetValue(obj, value, ((args != null) && (args.Length == 0)) ? null : args);
                    }
                }
            }
        }

        public static Reflector Current
        {
            get
            {
                if (current == null)
                {
                    current = new Reflector(ProductPage.Assembly);
                }
                return current;
            }
        }

        internal Dictionary<System.Type, Dictionary<string, MemberInfo>> MemberInfos
        {
            get
            {
                if (this.memberInfos == null)
                {
                    this.memberInfos = new Dictionary<System.Type, Dictionary<string, MemberInfo>>();
                }
                return this.memberInfos;
            }
        }
    }
}

