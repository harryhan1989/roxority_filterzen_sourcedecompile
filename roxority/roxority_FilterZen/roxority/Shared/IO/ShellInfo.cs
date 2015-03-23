namespace roxority.Shared.IO
{
    using Microsoft.Win32;
    using roxority.Shared;
    using roxority.Shared.Drawing;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class ShellInfo : IDisposable
    {
        private string contentType;
        internal readonly string DirectoryName;
        internal readonly string DotLessExtension;
        internal readonly string Extension;
        internal readonly string FileName;
        internal readonly string FilePath;
        private System.Drawing.Icon icon;
        private System.Drawing.Image image;
        internal readonly string Name;
        private Verb primaryVerb;
        private List<Verb> secondaryVerbs;
        private System.Drawing.Icon smallIcon;
        private System.Drawing.Image smallImage;

        internal ShellInfo(string filePath)
        {
            if (SharedUtil.IsEmpty(this.FilePath = SharedUtil.Trim(filePath)))
            {
                throw new ArgumentNullException("filePath");
            }
            this.DirectoryName = Path.GetDirectoryName(this.FilePath);
            this.Extension = Path.GetExtension(this.FilePath).ToLower();
            this.DotLessExtension = this.Extension.Replace(".", string.Empty);
            this.FileName = Path.GetFileName(this.FilePath);
            this.Name = Path.GetFileNameWithoutExtension(this.FilePath);
        }

        private static string AppDesc(ref string value)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\ShellNoRoam\MUICache", false);
                if (key != null)
                {
                    using (key)
                    {
                        string[] strArray;
                        if (!SharedUtil.IsEmpty((ICollection) (strArray = key.GetValueNames())))
                        {
                            foreach (string str in strArray)
                            {
                                if (value.ToLower().IndexOf(str.ToLower()) >= 0)
                                {
                                    return SharedUtil.Trim(key.GetValue(str, value, RegistryValueOptions.None) as string);
                                }
                            }
                            foreach (string str2 in strArray)
                            {
                                if (str2.ToLower().IndexOf(value.ToLower()) >= 0)
                                {
                                    value = str2;
                                    return SharedUtil.Trim(key.GetValue(str2, value, RegistryValueOptions.None) as string);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            try
            {
                return ("(" + Path.GetFileNameWithoutExtension(Extract(value)) + ")");
            }
            catch
            {
                return ("(" + value + ")");
            }
        }

        public void Dispose()
        {
            if (this.icon != null)
            {
                this.icon.Dispose();
                this.icon = null;
            }
            if (this.image != null)
            {
                this.image.Dispose();
                this.image = null;
            }
            if (this.smallIcon != null)
            {
                this.smallIcon.Dispose();
                this.smallIcon = null;
            }
            if (this.smallImage != null)
            {
                this.smallImage.Dispose();
                this.smallImage = null;
            }
        }

        internal void ExecuteVerb(Verb verb, IntPtr errorDialogParentHandle, bool useShellExecute)
        {
            ExecuteVerb(verb, errorDialogParentHandle, useShellExecute, this.FilePath);
        }

        internal static void ExecuteVerb(Verb verb, IntPtr errorDialogParentHandle, bool useShellExecute, string filePath)
        {
            string args = null;
            int num;
            string newValue = Guid.NewGuid().ToString("N");
            string str3 = filePath.Replace(".exe", newValue);
            bool flag = true;
            if (verb == null)
            {
                Open(filePath, null, errorDialogParentHandle, useShellExecute);
                return;
            }
            string cmd = verb.Command.Replace("\"%1\"", "\"" + str3 + "\"").Replace("\"%l\"", "\"" + str3 + "\"");
            if (cmd.Contains("rundll32"))
            {
                cmd = cmd.Replace("%1", str3 ?? "").Replace("%l", str3 ?? "");
            }
            else
            {
                cmd = cmd.Replace("%1", "\"" + str3 + "\"").Replace("%l", "\"" + str3 + "\"");
            }
            if (!cmd.Contains(str3))
            {
                cmd = cmd + " \"" + str3 + "\"";
            }
        Label_0107:
            if (((num = cmd.ToLower().IndexOf(".exe\"")) > 0) && ((num + 5) < cmd.Length))
            {
                args = cmd.Substring(num + 5).Trim();
                cmd = cmd.Substring(0, num + 5);
            }
            else if (((num = cmd.ToLower().IndexOf(".exe")) > 0) && ((num + 4) < cmd.Length))
            {
                args = cmd.Substring(num + 4).Trim();
                cmd = cmd.Substring(0, num + 4);
            }
            cmd = cmd.Replace(newValue, ".exe");
            if (args != null)
            {
                args = args.Replace(newValue, ".exe");
            }
            else if (flag)
            {
                flag = false;
                goto Label_0107;
            }
            if (args == "%*")
            {
                args = null;
            }
            Open(cmd, args, errorDialogParentHandle, useShellExecute);
        }

        private static string Extract(string value)
        {
            if (!SharedUtil.IsEmpty(value))
            {
                for (int i = 0; i < 20; i++)
                {
                    value = value.Replace("%" + i, string.Empty);
                }
                value = value.Replace("\"\"", string.Empty).Trim();
                int index = value.ToLower().IndexOf(".exe");
                if (index > 0)
                {
                    value = value.Substring(0, index + 4);
                }
                while (value.StartsWith("\"") || value.StartsWith("@"))
                {
                    value = value.Substring(1);
                }
                while (value.EndsWith("\""))
                {
                    value = value.Substring(0, value.Length - 1);
                }
            }
            return SharedUtil.Trim(value);
        }

        internal System.Drawing.Image GetDisabledImage(System.Drawing.Color backColor, bool large)
        {
            return DrawingUtil.GetDisabledImage(backColor, large ? this.Image : this.SmallImage);
        }

        internal void Open(IntPtr errorDialogParentHandle, bool useShellExecute)
        {
            Open(this.FilePath, null, errorDialogParentHandle, useShellExecute);
        }

        private static void Open(string cmd, string args, IntPtr errorDialogParentHandle, bool useShellExecute)
        {
            ProcessStartInfo startInfo = null;
            Process process = null;
            try
            {
                if ((startInfo = SharedUtil.IsEmpty(args) ? new ProcessStartInfo(cmd) : new ProcessStartInfo(cmd, args)).ErrorDialog = !IntPtr.Zero.Equals(errorDialogParentHandle))
                {
                    startInfo.ErrorDialogParentHandle = errorDialogParentHandle;
                }
                startInfo.UseShellExecute = useShellExecute;
                process = Process.Start(startInfo);
            }
            finally
            {
                if (process != null)
                {
                    process.Dispose();
                }
            }
        }

        [DllImport("shell32.dll", SetLastError=true)]
        private static extern bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);
        internal static bool ShowOpenWith(string filePath)
        {
            ShellExecuteInfo info=new ShellExecuteInfo();
            info = new ShellExecuteInfo {
                Size = Marshal.SizeOf(info),
                Verb = "openas",
                File = filePath,
                Show = 1
            };
            try
            {
                return ShellExecuteEx(ref info);
            }
            catch
            {
                return false;
            }
        }

        internal static bool ShowProperties(string filePath)
        {
            ShellExecuteInfo info=new ShellExecuteInfo();
            info = new ShellExecuteInfo {
                Size = Marshal.SizeOf(info),
                Verb = "properties",
                File = filePath,
                Show = 5,
                Mask = 12
            };
            try
            {
                return ShellExecuteEx(ref info);
            }
            catch
            {
                return false;
            }
        }

        internal string ContentType
        {
            get
            {
                if (this.contentType == null)
                {
                    try
                    {
                        RegistryKey key = Registry.ClassesRoot.OpenSubKey(this.Extension, false);
                        if (key != null)
                        {
                            using (key)
                            {
                                this.contentType = key.GetValue("Content Type", string.Empty, RegistryValueOptions.None) as string;
                            }
                        }
                    }
                    catch
                    {
                        this.contentType = string.Empty;
                    }
                }
                return this.contentType;
            }
        }

        internal bool Exists
        {
            get
            {
                return File.Exists(this.FilePath);
            }
        }

        internal string FileType
        {
            get
            {
                string dotLessExtension = this.DotLessExtension;
                try
                {
                    RegistryKey key = Registry.ClassesRoot.OpenSubKey(this.Extension, false);
                    if (key != null)
                    {
                        using (key)
                        {
                            dotLessExtension = key.GetValue(null, dotLessExtension, RegistryValueOptions.None) as string;
                        }
                    }
                    key = Registry.ClassesRoot.OpenSubKey(dotLessExtension, false);
                    if (key != null)
                    {
                        using (key)
                        {
                            dotLessExtension = key.GetValue(null, dotLessExtension, RegistryValueOptions.None) as string;
                        }
                    }
                }
                catch
                {
                }
                if (SharedUtil.IsEmpty(dotLessExtension))
                {
                    dotLessExtension = this.DotLessExtension;
                }
                return SharedUtil.Trim(dotLessExtension);
            }
        }

        internal System.Drawing.Icon Icon
        {
            get
            {
                if (this.icon != null)
                {
                    return this.icon;
                }
                return (this.icon = DrawingUtil.GetFileIcon(this.FilePath, true));
            }
        }

        internal System.Drawing.Image Image
        {
            get
            {
                if (this.image != null)
                {
                    return this.image;
                }
                return (this.image = DrawingUtil.GetFileImage(this.FilePath, true));
            }
        }

        internal Verb PrimaryVerb
        {
            get
            {
                RegistryKey key = null;
                RegistryKey key2 = null;
                RegistryKey key3 = null;
                string title = string.Empty;
                string str2 = string.Empty;
                System.Drawing.Image fileImage = null;
                if (this.primaryVerb == null)
                {
                    try
                    {
                        key = Registry.ClassesRoot.OpenSubKey(this.Extension, false);
                        if (key != null)
                        {
                            string str3;
                            if (!SharedUtil.IsEmpty(str3 = key.GetValue(null, string.Empty) as string) && (((key2 = Registry.ClassesRoot.OpenSubKey(str3, false)) == null) || ((key3 = key2.OpenSubKey(@"shell\open\command", false)) == null)))
                            {
                                key3 = key.OpenSubKey(@"shell\open\command", false);
                            }
                            if (((key3 == null) && (key2 != null)) && (((key3 = key2.OpenSubKey("shell", false)) != null) && (key3.SubKeyCount > 0)))
                            {
                                key3 = key3.OpenSubKey(key3.GetSubKeyNames()[0] + @"\command", false);
                            }
                            if ((key3 != null) && !SharedUtil.IsEmpty(str3 = key3.GetValue(null, null, RegistryValueOptions.None) as string))
                            {
                                str2 = str3;
                                str3 = Extract((this.Extension == ".exe") ? this.FilePath : str3);
                                title = AppDesc(ref str3);
                                string str4 = AppDesc(ref str2);
                                if (str4.Contains(" ") || str4.Contains("-"))
                                {
                                    title = str4;
                                }
                                if (this.Extension != ".exe")
                                {
                                    fileImage = DrawingUtil.GetFileImage(Extract(str3), false);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (key3 != null)
                        {
                            key3.Close();
                        }
                        if (key2 != null)
                        {
                            key2.Close();
                        }
                        if (key != null)
                        {
                            key.Close();
                        }
                        this.primaryVerb = new Verb(title, str2, fileImage);
                    }
                }
                return this.primaryVerb;
            }
        }

        internal IEnumerable<Verb> SecondaryVerbs
        {
            get
            {
                RegistryKey key = null;
                RegistryKey key2 = null;
                RegistryKey key3 = null;
                string filePath = string.Empty;
                if (this.secondaryVerbs == null)
                {
                    try
                    {
                        this.secondaryVerbs = new List<Verb>();
                        List<string> list = new List<string>();
                        key = Registry.ClassesRoot.OpenSubKey(this.Extension, false);
                        if (key != null)
                        {
                            if (((key2 = Registry.ClassesRoot.OpenSubKey(key.GetValue(null, this.Extension, RegistryValueOptions.None) as string, false)) == null) || ((key3 = key2.OpenSubKey("OpenWithList", false)) == null))
                            {
                                key3 = key.OpenSubKey("OpenWithList", false);
                            }
                            if (key3 != null)
                            {
                                list.AddRange(key3.GetSubKeyNames());
                                key3.Close();
                            }
                            key.Close();
                            key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths", false);
                            if (key3 != null)
                            {
                                foreach (string str2 in list)
                                {
                                    string str3;
                                    key = key3.OpenSubKey(str2, false);
                                    if (key != null)
                                    {
                                        str3 = SharedUtil.Trim(key.GetValue(null, null, RegistryValueOptions.None) as string);
                                        this.secondaryVerbs.Add(new Verb(AppDesc(ref str3), str3, (this.Extension == ".exe") ? null : DrawingUtil.GetFileImage(Extract(str3), false)));
                                        key.Close();
                                    }
                                    else if ((this.Extension == ".exe") || (this.Extension == ".com"))
                                    {
                                        str3 = str2;
                                        filePath = this.FilePath;
                                        this.secondaryVerbs.Add(new Verb(AppDesc(ref filePath), str3, null));
                                    }
                                    else
                                    {
                                        str3 = str2;
                                        this.secondaryVerbs.Add(new Verb(AppDesc(ref str3), str3, DrawingUtil.GetFileImage(Extract(str3), false)));
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        this.secondaryVerbs = null;
                    }
                    finally
                    {
                        if (key3 != null)
                        {
                            key3.Close();
                        }
                        if (key2 != null)
                        {
                            key2.Close();
                        }
                        if (key != null)
                        {
                            key.Close();
                        }
                    }
                }
                return this.secondaryVerbs;
            }
        }

        internal System.Drawing.Icon SmallIcon
        {
            get
            {
                if (this.smallIcon != null)
                {
                    return this.smallIcon;
                }
                return (this.smallIcon = DrawingUtil.GetFileIcon(this.FilePath, false));
            }
        }

        internal System.Drawing.Image SmallImage
        {
            get
            {
                if (this.smallImage != null)
                {
                    return this.smallImage;
                }
                return (this.smallImage = DrawingUtil.GetFileImage(this.FilePath, false));
            }
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct ShellExecuteInfo
        {
            internal int Size;
            internal uint Mask;
            internal IntPtr hwnd;
            internal string Verb;
            internal string File;
            internal string Parameters;
            internal string Directory;
            internal uint Show;
            internal IntPtr InstApp;
            internal IntPtr IDList;
            internal string Class;
            internal IntPtr hkeyClass;
            internal uint HotKey;
            internal IntPtr Icon;
            internal IntPtr Monitor;
        }

        internal class Verb : Trio<string, string, System.Drawing.Image>
        {
            internal Verb(string title, string command, System.Drawing.Image image) : base(title, command, image)
            {
            }

            internal string Command
            {
                get
                {
                    return base.Value2;
                }
            }

            internal System.Drawing.Image Image
            {
                get
                {
                    return base.Value3;
                }
            }

            internal string Title
            {
                get
                {
                    return base.Value1;
                }
            }
        }
    }
}

