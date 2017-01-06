using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.Module.Helpers
{
    public class RegistryUtils
    {
        public static object ReadRegistryValue(string key, string valueName)
        {
            return SmartIT.Module.Helpers.RegistryUtils.ReadRegistryValue(key, valueName, null, true);
        }
        public static object ReadRegistryValue(string key, string valueName, object defaultValue)
        {
            return SmartIT.Module.Helpers.RegistryUtils.ReadRegistryValue(key, valueName, defaultValue, false);
        }
        public static T ReadRegistryValue<T>(string key, string valueName, object defaultValue)
        {
            return SmartIT.Module.Helpers.RegistryUtils.ReadRegistryValue<T>(key, valueName, defaultValue, RegistryHive.LocalMachine);
        }
        public static object ReadRegistryValue(string key, string valueName, object defaultValue, bool throwOnError)
        {
            return SmartIT.Module.Helpers.RegistryUtils.ReadRegistryValue(key, valueName, defaultValue, throwOnError, RegistryHive.LocalMachine);
        }
        public static T ReadUserSpecificRegistryValue<T>(string key, string valueName, object defaultValue)
        {
            return SmartIT.Module.Helpers.RegistryUtils.ReadRegistryValue<T>(key, valueName, defaultValue, RegistryHive.CurrentUser);
        }
        public static object ReadUserSpecificRegistryValue(string key, string valueName, object defaultValue, bool throwOnError)
        {
            return SmartIT.Module.Helpers.RegistryUtils.ReadRegistryValue(key, valueName, defaultValue, throwOnError, RegistryHive.CurrentUser);
        }
        public static void DeleteRegistrySubKey(string key, string subKey)
        {
            RegistryHive registryHive = RegistryHive.LocalMachine;
            RegistryKey hiveKeyForProcess = SmartIT.Module.Helpers.RegistryUtils.GetHiveKeyForProcess(registryHive);
            RegistryKey registryKey = hiveKeyForProcess.OpenSubKey(string.Format("{0}\\{1}", key, subKey), true);
            try
            {
                if (registryKey == null)
                {
                    throw new Exception("RegistryKey "+ key);
                }
                registryKey.Close();
                RegistryKey registryKey2 = hiveKeyForProcess.OpenSubKey(key, true);
                registryKey2.DeleteSubKey(subKey);
            }
            finally
            {
                if (registryKey != null)
                {
                    registryKey.Dispose();
                }
                hiveKeyForProcess.Dispose();
            }
        }
        private static T ReadRegistryValue<T>(string key, string valueName, object defaultValue, RegistryHive rootHive)
        {
            object obj = SmartIT.Module.Helpers.RegistryUtils.ReadRegistryValue(key, valueName, null, false, rootHive);
            if (obj == null)
            {
                obj = (T)((object)defaultValue);
            }
            else
            {
                if (!(obj is T))
                {
                    // throw new Exception("RegistryKeyValueName " + key);
                   
                }
            }
            return (T)((object)obj);
        }
        private static object ReadRegistryValue(string key, string valueName, object defaultValue, bool throwOnError, RegistryHive registryHive)
        {
            RegistryKey hiveKeyForProcess = SmartIT.Module.Helpers.RegistryUtils.GetHiveKeyForProcess(registryHive);
            RegistryKey registryKey = hiveKeyForProcess.OpenSubKey(key, false);
            try
            {
                if (registryKey == null)
                {
                    // throw new Exception("RegistryKeyValueName " + key);
                }
                else
                {
                    object value = registryKey.GetValue(valueName);
                    if (value != null)
                    {
                        object result = value;
                        return result;
                    }
                    // throw new Exception("RegistryKeyValueName " + key);
                }
            }
            finally
            {
                if (registryKey != null)
                {
                    registryKey.Dispose();
                }
                hiveKeyForProcess.Dispose();
            }
            if (throwOnError)
            {
                throw new Exception("");
            }
            return defaultValue;
        }
        internal static RegistryKey GetHiveKeyForProcess(RegistryHive registryHive)
        {
            RegistryView view = RegistryView.Default;
            if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess)
            {
                view = RegistryView.Registry64;
            }
            return RegistryKey.OpenBaseKey(registryHive, view);
        }
        public static void SetRegistryValue(string key, string valueName, object value)
        {
            SmartIT.Module.Helpers.RegistryUtils.SetRegistryValue(key, valueName, value, RegistryHive.LocalMachine);
        }
        public static void SetRegistryValue(string key, string valueName, object value, bool bCreateRegValue)
        {
            SmartIT.Module.Helpers.RegistryUtils.SetRegistryValue(key, valueName, value, Registry.LocalMachine, bCreateRegValue);
        }
        public static void SetUserSpecificRegistryValue(string key, string valueName, object value)
        {
            SmartIT.Module.Helpers.RegistryUtils.SetRegistryValue(key, valueName, value, RegistryHive.CurrentUser);
        }
        private static void SetRegistryValue(string key, string valueName, object value, RegistryHive registryHive)
        {
            RegistryKey hiveKeyForProcess = SmartIT.Module.Helpers.RegistryUtils.GetHiveKeyForProcess(registryHive);
            RegistryKey registryKey = hiveKeyForProcess.OpenSubKey(key, true);
            try
            {
                if (registryKey == null)
                {
                    using (RegistryKey registryKey2 = hiveKeyForProcess.CreateSubKey(key))
                    {
                        if (registryKey2 == null)
                        {
                            // throw new Exception("RegistryKeyValueName " + key);
                        }
                        else
                        {
                            registryKey2.SetValue(valueName, value);
                        }
                        goto IL_58;
                    }
                }
                registryKey.SetValue(valueName, value);
                IL_58:;
            }
            finally
            {
                if (registryKey != null)
                {
                    registryKey.Dispose();
                }
                hiveKeyForProcess.Dispose();
            }
        }
        private static bool IsRegValueExists(RegistryKey regKey, string valueName)
        {
            bool result = false;
            string[] valueNames = regKey.GetValueNames();
            string[] array = valueNames;
            for (int i = 0; i < array.Length; i++)
            {
                string a = array[i];
                if (string.Equals(a, valueName, StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                }
            }
            return result;
        }
        private static void SetRegistryValue(string key, string valueName, object value, RegistryKey rootHive, bool bCreateRegValue)
        {
            using (RegistryKey registryKey = rootHive.OpenSubKey(key, true))
            {
                if (registryKey == null)
                {
                    if (!bCreateRegValue)
                    {
                        // throw new Exception("RegistryKeyValueName " + key);
                    }
                    using (RegistryKey registryKey2 = rootHive.CreateSubKey(key))
                    {
                        if (registryKey2 == null)
                        {
                            // throw new Exception("RegistryKeyValueName " + key);
                        }
                        registryKey2.SetValue(valueName, value);
                    //    goto IL_98;
                    }
                }
                if (!bCreateRegValue && !SmartIT.Module.Helpers.RegistryUtils.IsRegValueExists(registryKey, valueName))
                {
                    // throw new Exception("RegistryKeyValueName " + key);
                }
                registryKey.SetValue(valueName, value);
            
            }
        }
    }
}
