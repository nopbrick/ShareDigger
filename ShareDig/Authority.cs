using System.Management;

namespace ShareDig
{
    class Authority
    {
        public static ConnectionOptions GetAuthority(string userString)
        {
            ConnectionOptions conn = new ConnectionOptions();
            conn.EnablePrivileges = true;
            conn.Impersonation = ImpersonationLevel.Impersonate;
            conn.Username = userString.Substring(0, userString.IndexOf(':'));
            conn.Password = userString.Substring(userString.IndexOf(':') + 1);
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
