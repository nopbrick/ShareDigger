using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;
using System.Security;

namespace ShareDig
{
    class Digger : Authority
    {
        public static Dictionary<string,string> GetShares(string machineName, ConnectionOptions conn)
        {
            Dictionary<string,string> shares = new Dictionary<string, string>();
            ManagementScope scope = new ManagementScope("\\\\" + machineName + "\\" + "root\\CIMV2", conn);
            scope.Connect();
            ManagementObjectSearcher worker = new ManagementObjectSearcher(scope, new ObjectQuery("select Name,Path from win32_share"));
            foreach (ManagementObject item in worker.Get())
            {
                shares.Add(item["Name"].ToString(), item["Path"].ToString());
            }

            return shares;
        }

        
        public static Dictionary<string, string> GetShares(ConnectionOptions conn)
        {
            Dictionary<string, string> shares = new Dictionary<string, string>();
            ManagementScope scope = new ManagementScope("\\\\" + Environment.MachineName + "\\" + "root\\CIMV2", conn);
            scope.Connect();
            ManagementObjectSearcher worker = new ManagementObjectSearcher(scope, new ObjectQuery("select Name,ProviderName from Win32_MappedLogicalDisk"));
            foreach (ManagementObject item in worker.Get())
            {
                shares.Add(item["Name"].ToString(), item["ProviderName"].ToString());
            }

            return shares;
        }

        public static List<string> Search(Dictionary<string,string> shares, string query)
        {
            List<string> files = new List<string>();
            foreach (var item in shares.Values)
            {
                var results = from file in Directory.EnumerateFiles(item, query, SearchOption.AllDirectories)
                              select new
                              {
                                  file
                              };
                foreach (var f in results)
                {
                    Console.WriteLine($"[+] Found file in: {f.file}");
                    files.Add(f.file);
                }
            }
            return files;
        }
    }
}
