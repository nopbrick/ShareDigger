using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace ShareDig
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "-h") { Help(); }
            Digger dig = new Digger();
            Dictionary<string, string> shares = new Dictionary<string, string>();
            ConnectionOptions conn = args.Contains("-u") ? dig.auth(args[Array.IndexOf(args, "-u") + 1]) : dig.authNoParams(); //if user provided, use his identity

            shares = args.Contains("-m") ? dig.GetShares(args[Array.IndexOf(args, "-u") + 1], conn) : args.Contains("-h") ? null : dig.GetShares(conn);
            if (shares != null) { foreach (var item in shares) { Console.WriteLine($"[+] Found share => {item}"); } }
            if (args.Contains("-q")){ dig.Search(shares, args[Array.IndexOf(args, "-q") + 1]); } else{ Console.WriteLine("Finished!");}
        }
                static void Help()
        {
            Console.WriteLine("Usage: ShareDig.exe [-u USER] [-m MACHINE_NAME] [-q QUERY]");
            Console.WriteLine("All arguments are optional. No arguments -> search mounted shares on local machine using current user. ");
        }
    }
}
