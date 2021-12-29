using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security;

namespace ShareDig
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.Query != null)
                    {
                        if (o.User != null)
                        {
                            var password = Helper.ReadPassword();
                            if (o.Machine != null)
                            {
                                var shares = Digger.GetShares(o.Machine, Authority.GetAuthority(o.User, password));
                                Digger.Search(shares, o.Query);
                            }
                        }
                        else
                        {
                            var shares = o.Machine == null ? Digger.GetShares(Authority.GetAuthority()) : Digger.GetShares(o.Machine,Authority.GetAuthority());
                            Digger.Search(shares, o.Query);
                        }
                    }
                });
        }
        static void Help()
        {
            Console.WriteLine("Usage: ShareDig.exe [-u USER] [-m MACHINE_NAME] -q QUERY");
        }

        public class Options
        {
            [Option('s', "server", Required = false, HelpText = "Specify Active Directory server for queries. ")]
            public string Server { get; set; }
            [Option('u', "username", Required = false, HelpText = "User to impersonate. ")]
            public string User { get; set; }
            [Option('m', "machinename", Required = false, HelpText = "Enumerate shares on remote machine. ")]
            public string Machine { get; set; }
            [Option('q', "query", Required = true, HelpText = "String to search for. ")]
            public string Query { get; set; }
        }
    }
}
