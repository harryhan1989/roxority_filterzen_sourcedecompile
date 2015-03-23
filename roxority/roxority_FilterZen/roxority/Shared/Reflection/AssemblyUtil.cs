namespace roxority.Shared.Reflection
{
    using roxority.Shared;
    using roxority.Shared.IO;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal static class AssemblyUtil
    {
        private static readonly List<Assembly> allAssemblies = new List<Assembly>();
        private static Hashtable loadedAssemblies = null;
        private static Hashtable loadedTypes = null;

        internal static void CopyResource(Assembly assembly, string resourceName, Stream targetStream)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                IOUtil.CopyStream(stream, targetStream);
            }
        }

        internal static void CopyResource(Assembly assembly, string resourceName, string targetFilePath)
        {
            using (FileStream stream = File.Create(targetFilePath))
            {
                CopyResource(assembly, resourceName, stream);
            }
        }

        internal static List<Assembly> GetAllAssemblies()
        {
            return GetAllAssemblies(null);
        }

        internal static List<Assembly> GetAllAssemblies(ProgressBar progressBar)
        {
            if (allAssemblies.Count == 0)
            {
                Assembly[] assemblyArray;
                GetAllAssemblies(allAssemblies, Assembly.GetEntryAssembly());
                if (!SharedUtil.IsEmpty((ICollection) (assemblyArray = AppDomain.CurrentDomain.GetAssemblies())))
                {
                    if (progressBar != null)
                    {
                        progressBar.Value = 0;
                        progressBar.Maximum = assemblyArray.Length;
                        Application.DoEvents();
                    }
                    foreach (Assembly assembly in assemblyArray)
                    {
                        if (progressBar != null)
                        {
                            progressBar.Value++;
                            Application.DoEvents();
                        }
                        GetAllAssemblies(allAssemblies, assembly);
                    }
                }
            }
            return allAssemblies;
        }

        private static void GetAllAssemblies(List<Assembly> list, Assembly asm)
        {
            if (((list != null) && (asm != null)) && !list.Contains(asm))
            {
                list.Add(asm);
                foreach (AssemblyName name in asm.GetReferencedAssemblies())
                {
                    try
                    {
                        GetAllAssemblies(list, Assembly.Load(name));
                    }
                    catch
                    {
                    }
                }
            }
        }

        internal static MethodInfo GetBaseDefinition(MethodInfo methodInfo)
        {
            System.Type type;
            PropertyInfo info;
            return GetBaseDefinition(methodInfo, false, out type, out info);
        }

        internal static MethodInfo GetBaseDefinition(MethodInfo methodInfo, bool findInterfaceProperty, out System.Type interfaceType, out PropertyInfo interfaceProperty)
        {
            MethodInfo info;
            System.Type[] typeArray;
            SharedUtil.ThrowIfNull(methodInfo, "methodInfo");
            interfaceType = null;
            interfaceProperty = null;
            if (((info = methodInfo.GetBaseDefinition()) == methodInfo) && !SharedUtil.IsEmpty((ICollection) (typeArray = methodInfo.DeclaringType.GetInterfaces())))
            {
                foreach (System.Type type in typeArray)
                {
                    InterfaceMapping interfaceMap = methodInfo.DeclaringType.GetInterfaceMap(type);
                    for (int i = 0; i < interfaceMap.InterfaceMethods.Length; i++)
                    {
                        if (interfaceMap.TargetMethods[i] != methodInfo)
                        {
                            continue;
                        }
                        if (findInterfaceProperty)
                        {
                            PropertyInfo[] infoArray;
                            System.Type type2;
                            interfaceType = type2 = type;
                            if (!SharedUtil.IsEmpty((ICollection) (infoArray = type2.GetProperties())))
                            {
                                foreach (PropertyInfo info2 in infoArray)
                                {
                                    if ((info2.GetGetMethod() == interfaceMap.InterfaceMethods[i]) || (info2.GetSetMethod() == interfaceMap.InterfaceMethods[i]))
                                    {
                                        interfaceProperty = info2;
                                        break;
                                    }
                                }
                            }
                        }
                        return interfaceMap.InterfaceMethods[i];
                    }
                }
            }
            return info;
        }

        internal static string GetCompany()
        {
            return GetCompany(Assembly.GetCallingAssembly());
        }

        internal static string GetCompany(Assembly assembly)
        {
            AssemblyCompanyAttribute attribute = null;
            attribute = Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute), true) as AssemblyCompanyAttribute;
            if (attribute == null)
            {
                return string.Empty;
            }
            return attribute.Company;
        }

        internal static string GetCopyright(Assembly assembly)
        {
            AssemblyCopyrightAttribute attribute = null;
            attribute = Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute), true) as AssemblyCopyrightAttribute;
            if (attribute == null)
            {
                return string.Empty;
            }
            return attribute.Copyright;
        }

        internal static string GetDescription(Assembly assembly)
        {
            AssemblyDescriptionAttribute attribute = null;
            attribute = Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute), true) as AssemblyDescriptionAttribute;
            if (attribute == null)
            {
                return string.Empty;
            }
            return attribute.Description;
        }

        internal static System.Type[] GetLoadedTypes(System.Type baseType, bool @abstract, ProgressBar progressBar)
        {
            return GetLoadedTypes(baseType, false, @abstract, progressBar);
        }

        internal static System.Type[] GetLoadedTypes(System.Type baseType, bool includeBaseType, bool @abstract, ProgressBar progressBar)
        {
            List<Assembly> allAssemblies = GetAllAssemblies(progressBar);
            ArrayList list2 = new ArrayList();
            if (baseType == null)
            {
                baseType = typeof(object);
            }
            if (progressBar != null)
            {
                progressBar.Value = 0;
                progressBar.Maximum = allAssemblies.Count;
                Application.DoEvents();
            }
            foreach (Assembly assembly in allAssemblies)
            {
                System.Type[] typeArray;
                if (progressBar != null)
                {
                    progressBar.Value++;
                    Application.DoEvents();
                }
                if ((assembly != null) && !SharedUtil.IsEmpty((ICollection) (typeArray = assembly.GetTypes())))
                {
                    foreach (System.Type type in typeArray)
                    {
                        if ((((type != null) && baseType.IsAssignableFrom(type)) && (includeBaseType || (type != baseType))) && (@abstract || (!type.IsAbstract && !type.IsInterface)))
                        {
                            list2.Add(type);
                        }
                    }
                }
            }
            return (list2.ToArray(typeof(System.Type)) as System.Type[]);
        }

        internal static string GetProduct()
        {
            return GetProduct(Assembly.GetCallingAssembly());
        }

        internal static string GetProduct(Assembly assembly)
        {
            AssemblyProductAttribute attribute = null;
            attribute = Attribute.GetCustomAttribute(assembly, typeof(AssemblyProductAttribute), true) as AssemblyProductAttribute;
            if (attribute == null)
            {
                return string.Empty;
            }
            return attribute.Product;
        }

        internal static string GetTitle(Assembly assembly)
        {
            AssemblyTitleAttribute attribute = null;
            attribute = Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute), true) as AssemblyTitleAttribute;
            if (attribute == null)
            {
                return string.Empty;
            }
            return attribute.Title;
        }

        internal static string GetTrademark(Assembly assembly)
        {
            AssemblyTrademarkAttribute attribute = null;
            attribute = Attribute.GetCustomAttribute(assembly, typeof(AssemblyTrademarkAttribute), true) as AssemblyTrademarkAttribute;
            if (attribute == null)
            {
                return string.Empty;
            }
            return attribute.Trademark;
        }

        internal static System.Type GetType(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Object:
                    return typeof(object);

                case TypeCode.Boolean:
                    return typeof(bool);

                case TypeCode.Char:
                    return typeof(char);

                case TypeCode.SByte:
                    return typeof(sbyte);

                case TypeCode.Byte:
                    return typeof(byte);

                case TypeCode.Int16:
                    return typeof(short);

                case TypeCode.UInt16:
                    return typeof(ushort);

                case TypeCode.Int32:
                    return typeof(int);

                case TypeCode.UInt32:
                    return typeof(uint);

                case TypeCode.Int64:
                    return typeof(long);

                case TypeCode.UInt64:
                    return typeof(ulong);

                case TypeCode.Single:
                    return typeof(float);

                case TypeCode.Double:
                    return typeof(double);

                case TypeCode.Decimal:
                    return typeof(decimal);

                case TypeCode.DateTime:
                    return typeof(DateTime);

                case TypeCode.String:
                    return typeof(string);
            }
            return null;
        }

        internal static string GetTypeName(System.Type type)
        {
            return string.Format("{0},{1}", type.FullName, type.Assembly.GetName().Name);
        }

        internal static System.Type[] GetTypes(Assembly assembly)
        {
            System.Type[] typeArray;
            lock (LoadedTypes.SyncRoot)
            {
                typeArray = LoadedTypes[assembly] as System.Type[];
                if (typeArray == null)
                {
                    LoadedTypes[assembly] = typeArray = assembly.GetTypes();
                }
            }
            return typeArray;
        }

        internal static Version GetVersion()
        {
            return GetVersion(Assembly.GetCallingAssembly());
        }

        internal static Version GetVersion(Assembly assembly)
        {
            return assembly.GetName().Version;
        }

        internal static Assembly LoadAssembly(string filePath)
        {
            return LoadAssembly(filePath, false);
        }

        internal static Assembly LoadAssembly(string filePath, bool forceReload)
        {
            Assembly[] assemblyArray;
            if (!forceReload && !SharedUtil.IsEmpty((ICollection) (assemblyArray = AppDomain.CurrentDomain.GetAssemblies())))
            {
                foreach (Assembly assembly in assemblyArray)
                {
                    if (((assembly != null) && !(assembly is AssemblyBuilder)) && (!SharedUtil.IsEmpty(assembly.CodeBase) && IOUtil.PathEquals(assembly.CodeBase, filePath)))
                    {
                        return assembly;
                    }
                }
            }
            lock (LoadedAssemblies.SyncRoot)
            {
                if (forceReload || !LoadedAssemblies.ContainsKey(filePath = IOUtil.NormalizePath(filePath)))
                {
                    byte[] buffer;
                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        buffer = new byte[stream.Length];
                        using (MemoryStream stream2 = new MemoryStream(buffer, true))
                        {
                            IOUtil.CopyStream(stream, stream2);
                        }
                    }
                    LoadedAssemblies[filePath] = Assembly.Load(buffer);
                }
                return (LoadedAssemblies[filePath] as Assembly);
            }
        }

        private static Hashtable LoadedAssemblies
        {
            get
            {
                if (loadedAssemblies == null)
                {
                    loadedAssemblies = Hashtable.Synchronized(new Hashtable());
                }
                return loadedAssemblies;
            }
        }

        private static Hashtable LoadedTypes
        {
            get
            {
                if (loadedTypes == null)
                {
                    loadedTypes = Hashtable.Synchronized(new Hashtable());
                }
                return loadedTypes;
            }
        }
    }
}

