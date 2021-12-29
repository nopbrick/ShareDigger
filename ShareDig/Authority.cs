using System.Management;
using System.Security;

namespace ShareDig
{
    class Authority
    {
        public static ConnectionOptions GetAuthority(string userName, SecureString password)
        {
            ConnectionOptions conn = new ConnectionOptions();
            conn.EnablePrivileges = true;
            conn.Impersonation = ImpersonationLevel.Impersonate;
            conn.Username = userName;
            conn.SecurePassword = password;
            return conn;          
        }

        public static ConnectionOptions GetAuthority()
        {
            ConnectionOptions conn = new ConnectionOptions();
            conn.EnablePrivileges = true;
            conn.Impersonation = ImpersonationLevel.Impersonate;
            return conn;
        }
    }
}
