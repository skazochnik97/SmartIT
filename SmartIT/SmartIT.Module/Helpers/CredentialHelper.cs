using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.Module.Helpers
{
    public static class CredentialHelper
    {
        public static bool ContainsDomain(string username)
        {
            return username.IndexOf('\\') >= 0;
        }

        public static string CombineUserName(string domain, string name)
        {
            if (string.IsNullOrEmpty(domain))
                return name;
            return string.Format("{0}\\{1}", (object)domain, (object)name);
        }

        public static void SplitAccount(string account, out string domain, out string user)
        {
            if (string.IsNullOrEmpty(account))
            {
                domain = (string)null;
                user = (string)null;
            }
            else if (!account.Contains("\\"))
            {
                domain = (string)null;
                user = account;
            }
            else
            {
                string[] strArray = account.Split('\\');
                domain = strArray[0];
                user = strArray[1];
            }
        }

        public static void EnsureDoubleSlashAfterDomain(string account, ref string domain)
        {
            string str = "\\";
            if (string.IsNullOrEmpty(account) || account.Split(str.ToCharArray()).Length != 2)
                return;
            domain += str;
        }

        public static char[] GenerateString(int nChars, bool alphaOnly)
        {
            if (alphaOnly)
                return CredentialHelper.GenerateAlphaString(nChars);
            return CredentialHelper.GenerateAlphaNumericString(nChars);
        }

        public static char[] GenerateAlphaString(int nChars)
        {
            string alphabet = "qwertyuiopasdfghjklzxcvbnm";
            return CredentialHelper.GenerateString(nChars, alphabet);
        }

        public static char[] GenerateAlphaNumericString(int nChars)
        {
            string alphabet = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            return CredentialHelper.GenerateString(nChars, alphabet);
        }

        public static char[] GenerateString(int nChars)
        {
            string alphabet = "!@#$%^&*()_+-=`~<>?/[]\\{}|;:,.qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            return CredentialHelper.GenerateString(nChars, alphabet);
        }

        public static char[] GenerateString(int nChars, string alphabet)
        {
            byte[] data = new byte[nChars];
            char[] chArray = new char[nChars];
            new RNGCryptoServiceProvider().GetBytes(data);
            for (int index = 0; index < nChars; ++index)
                chArray[index] = alphabet[Convert.ToInt32(data[index]) % alphabet.Length];
            return chArray;
        }

        public static bool DoPasswordsMatch(SecureString password, SecureString confirmPassword)
        {
            IntPtr num1 = IntPtr.Zero;
            IntPtr num2 = IntPtr.Zero;
            try
            {
                num1 = Marshal.SecureStringToBSTR(password);
                num2 = Marshal.SecureStringToBSTR(confirmPassword);
                return Marshal.PtrToStringBSTR(num1).Equals(Marshal.PtrToStringBSTR(num2));
            }
            finally
            {
                if (num1 != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(num1);
                if (num2 != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(num2);
            }
        }

        public static string SecureStringToString(SecureString secureString)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(secureString);
            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }
    }
}
