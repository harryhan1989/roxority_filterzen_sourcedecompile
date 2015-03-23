namespace roxority.Shared.Win32
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class IniFile
    {
        internal string Path;

        internal IniFile(string path)
        {
            this.Path = path;
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        internal string this[string section, string key]
        {
            get
            {
                StringBuilder retVal = new StringBuilder(0xff);
                GetPrivateProfileString(section, key, "", retVal, 0xff, this.Path);
                return retVal.ToString();
            }
            set
            {
                WritePrivateProfileString(section, key, value, this.Path);
            }
        }
    }
}

