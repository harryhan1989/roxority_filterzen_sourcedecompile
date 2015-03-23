namespace roxority.SharePoint
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Principal;

    internal class SPElevator : IDisposable
    {
        private WindowsImpersonationContext context;
        private IntPtr handle = IntPtr.Zero;
        private WindowsIdentity identity;
        private const int LOGON32_LOGON_NETWORK = 3;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        public SPElevator(string domain, string username, string password)
        {
            int index = username.IndexOf('\\');
            if (string.IsNullOrEmpty(domain))
            {
                if (index > 0)
                {
                    domain = username.Substring(0, index);
                    username = username.Substring(index + 1);
                }
                else
                {
                    domain = Environment.UserDomainName;
                }
            }
            if (!LogonUser(username, domain, password, 3, 0, ref this.handle))
            {
                throw new UnauthorizedAccessException(ProductPage.GetResource("SPElevator_Error", new object[] { domain + @"\" + username, Marshal.GetLastWin32Error() }));
            }
            this.context = (this.identity = new WindowsIdentity(this.handle)).Impersonate();
        }

        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        internal static extern bool CloseHandle(IntPtr handle);
        public void Dispose()
        {
            try
            {
                this.context.Undo();
            }
            catch
            {
            }
            try
            {
                this.identity.Dispose();
            }
            catch
            {
            }
            try
            {
                this.context.Dispose();
            }
            catch
            {
            }
            try
            {
                if (this.handle != IntPtr.Zero)
                {
                    CloseHandle(this.handle);
                }
            }
            catch
            {
            }
        }

        [DllImport("advapi32.dll", SetLastError=true)]
        internal static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
    }
}

