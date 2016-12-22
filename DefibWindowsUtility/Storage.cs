using System;
using System.Linq;
using Microsoft.Win32;

namespace DefibWindowsUtility
{
    static class Storage
    {
        public static bool RequiresPreparation()
        {
            if (!Registry.CurrentUser.OpenSubKey("SOFTWARE").GetSubKeyNames().Contains("Defib"))
            {
                return true;
            }

            return false;
        }

        public static void PrepareRegistry()
        {
            RegistryKey DefibKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Defib", true);
            DefibKey.SetValue("Installed", true);
        }

        public static bool ExistsAlias(string alias)
        {
            if (!Registry.CurrentUser.OpenSubKey("SOFTWARE").GetSubKeyNames().Contains("Defib"))
            {
                return false;
            }

            try
            {
                RegistryKey DefibKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Defib", true);

                if (DefibKey.GetValueNames().Contains(alias) == false)
                {
                    return false;
                }
            }
            catch (NullReferenceException exception)
            {
                Console.WriteLine("Unexpected exception: NullReferenceException (Does the `Defib` subkey exist in HKEY_LOCAL_MACHINE?)");
                return false;
            }

            return true;
        }

        public static void CreateAlias(string alias, string key)
        {
            RegistryKey DefibKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Defib", true);

            DefibKey.SetValue(alias, key);
        }

        public static void UpdateAlias(string alias, string key)
        {
            RegistryKey DefibKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Defib", true);

            DefibKey.SetValue(alias, key);
        }

        public static string FetchAlias(string alias)
        {
            RegistryKey DefibKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Defib", true);

            return DefibKey.GetValue(alias).ToString();
        }

        public static void DeleteAlias(string alias)
        {
            RegistryKey DefibKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Defib", true);

            DefibKey.DeleteValue(alias);
        }
    }
}
