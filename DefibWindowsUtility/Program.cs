using System;
using System.Collections.Generic;
using DefibWindowsUtility.EventArgs;
using System.Web;
using System.IO;
using System.Net;

namespace DefibWindowsUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                // Lay down the law
                Console.WriteLine("You failed to specify a command, please use `defib help` for usage instructions.");
                return;
            }

            if (Storage.RequiresPreparation())
            {
                Storage.PrepareRegistry();
            }

            Delegator CommandDelegator = new Delegator();
            CommandDelegator.OnHelpCommand += OnHelpHandler;
            CommandDelegator.OnAddCommand += OnAddHandler;
            CommandDelegator.OnBeatCommand += OnBeatHandler;
            CommandDelegator.OnDeleteCommand += OnDeleteHandler;
            CommandDelegator.OnUpdateCommand += OnUpdateHandler;

            string command = args[0];

            List<string> argumentList = new List<string>(args);
            argumentList.Reverse();
            argumentList.RemoveAt(args.Length - 1);
            argumentList.Reverse();

            string[] arguments = argumentList.ToArray();

            CommandDelegator.Delegate(command, arguments);

            Console.ReadLine();
        }

        private static void OnHelpHandler(object sender, OnHelpCommandEventArgs arguments)
        {
            string[] content = new string[6]
            {
                "The Defib Windows Utility implements the following command:",
                "`defib help` - Displays this information.",
                "`defib add <alias> <key>` - Adds a Defib key to the registry.",
                "`defib update <alias> <key>` - Updates a Defib key to the registry with the new, supplied key.",
                "`defib delete <alias>` - Deletes a Defib key from the registry.",
                "`defib beat <alias>` - Executes a heartbeat with the supplied Defib key alias."
            };

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(content[i]);
            }
        }

        private static void OnAddHandler(object sender, OnAddCommandEventArgs arguments)
        {
            if (!Storage.ExistsAlias(arguments.Alias))
            {
                Storage.CreateAlias(arguments.Alias, arguments.Key);
                Console.WriteLine(string.Format("Your Defib key `{0}` was created for you.", arguments.Alias));
                return;
            }

            Console.WriteLine(string.Format("Your Defib key `{0}` already exists.", arguments.Alias));
        }

        private static void OnBeatHandler(object sender, OnBeatCommandEventArgs arguments)
        {
            if (Storage.ExistsAlias(arguments.Alias))
            {
                string defibKey = Storage.FetchAlias(arguments.Alias);

                WebRequest httpRequest = WebRequest.Create("https://defib.io/heartbeat/receiver/" + defibKey);
                WebResponse httpResponse = httpRequest.GetResponse();

                Stream httpStream = httpResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(httpStream);
                string responseFromDefib = streamReader.ReadToEnd();

                if (responseFromDefib == @"{""status"":""success"",""msg"":""success""}")
                {
                    Console.WriteLine("Heartbeat succesfully sent.");
                }
                else
                {
                    Console.WriteLine("Invalid Defib key, or possible service outage.");
                }

                return;
            }

            Console.WriteLine(string.Format("Your Defib key `{0}` did not exist.", arguments.Alias));
        }

        private static void OnDeleteHandler(object sender, OnDeleteCommandEventArgs arguments)
        {
            if (Storage.ExistsAlias(arguments.Alias))
            {
                Storage.DeleteAlias(arguments.Alias);
                Console.WriteLine(string.Format("Your Defib key `{0}` was successfully deleted.", arguments.Alias));
                return;

            }

            Console.WriteLine(string.Format("Your Defib key `{0}` did not exist.", arguments.Alias));
        }

        private static void OnUpdateHandler(object sender, OnUpdateCommandEventArgs arguments)
        {
            if (Storage.ExistsAlias(arguments.Alias))
            {
                Storage.UpdateAlias(arguments.Alias, arguments.Key);
                Console.WriteLine(string.Format("Your Defib key `{0}` was successfully updated.", arguments.Alias));
                return;
            }

            Storage.CreateAlias(arguments.Alias, arguments.Key);
            Console.WriteLine(string.Format("Your Defib key `{0}` did not exist, and was created for you.", arguments.Alias));
        }
    }
}
