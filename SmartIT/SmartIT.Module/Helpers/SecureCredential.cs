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
    public class SecureCredential : ICloneable, IDisposable
    {
        public static SecureCredential Empty = new SecureCredential(string.Empty, new SecureString());
        private string user;
        private string domain;
        private SecureString password;
        private bool encrypted;

        public string UserName
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
            }
        }

        public string Domain
        {
            get
            {
                return this.domain;
            }
            set
            {
                this.domain = value;
            }
        }

        public bool Encrypted
        {
            get
            {
                return this.encrypted;
            }
            set
            {
                this.encrypted = value;
            }
        }

        public SecureString Password
        {
            get
            {
                return this.password;
            }
        }

     /*   public SecurePassword SecurePassword
        {
            get
            {
                return new SecurePassword(this.Password);
            }
        }
        */
        public string UnencryptedPassword
        {
            get
            {
                if (this.password == null)
                    return (string)null;
                return CredentialHelper.SecureStringToString(this.password);
            }
        }

        public string Account
        {
            get
            {
                return this.FormatAccount();
            }
        }

        private SecureCredential()
        {
        }

        public SecureCredential(string account, char[] password)
        {
            this.ParseAccount(account);
            if (password == null)
                return;
            this.password = new SecureString();
            foreach (char c in password)
                this.password.AppendChar(c);
            this.password.MakeReadOnly();
        }

        public SecureCredential(string account, SecureString password)
        {
            this.ParseAccount(account);
            if (password == null)
                return;
            this.password = password.Copy();
            this.password.MakeReadOnly();
        }

        public SecureCredential(string user, string domain, char[] password)
        {
            this.user = user;
            this.domain = domain;
            if (password == null)
                return;
            this.password = new SecureString();
            foreach (char c in password)
                this.password.AppendChar(c);
            this.password.MakeReadOnly();
        }

        public SecureCredential(string user, string domain, SecureString password)
        {
            this.user = user;
            this.domain = domain;
            if (password == null)
                return;
            this.password = password.Copy();
            this.password.MakeReadOnly();
        }

        public object Clone()
        {
            SecureCredential secureCredential = new SecureCredential();
            secureCredential.domain = this.domain;
            secureCredential.user = this.user;
            secureCredential.encrypted = this.encrypted;
            if (this.password != null)
            {
                secureCredential.password = this.password.Copy();
                secureCredential.password.MakeReadOnly();
            }
            return (object)secureCredential;
        }

        public SecureCredential Copy()
        {
            return (SecureCredential)this.Clone();
        }

        public void Dispose()
        {
            if (this.password == null)
                return;
            this.password.Dispose();
            this.password = (SecureString)null;
        }

        public string FormatAccount()
        {
            return CredentialHelper.CombineUserName(this.Domain, this.UserName);
        }

        public void ParseAccount(string account)
        {
            CredentialHelper.SplitAccount(account, out this.domain, out this.user);
        }

        public void EnsureDoubleSlashAfterDomain()
        {
            CredentialHelper.EnsureDoubleSlashAfterDomain(this.Account, ref this.domain);
        }

        //public void CanonicalizeAccount()
        //{
        //    try
        //    {
        //        this.ParseAccount(new NTAccount(this.Domain, this.UserName).Translate(typeof(SecurityIdentifier)).Translate(typeof(NTAccount)).Value);
        //    }
        //    catch (IdentityNotMappedException ex)
        //    {
        //        ScopeTracer.OnCatch<IdentityNotMappedException>(CallSite.New("CanonicalizeAccount", "SecureCredential.cs", 235), ex);
        //        throw new CarmineException(new ErrorInfo(ErrorCode.InvalidGenericAccount, "UserName", this.Account, new string[0]), (Exception)ex);
        //    }
        //}

        public void SetPassword(SecureString password)
        {
            this.password = password.Copy();
            this.password.MakeReadOnly();
        }

        public static SecureCredential FromAccount(string account)
        {
            SecureCredential secureCredential = new SecureCredential();
            CredentialHelper.SplitAccount(account, out secureCredential.domain, out secureCredential.user);
            return secureCredential;
        }

        public string GetHashString()
        {
            if (this.password == null)
                return this.Account;
            IntPtr num = IntPtr.Zero;
            byte[] numArray = (byte[])null;
            byte[] inArray = (byte[])null;
            try
            {
                numArray = new byte[2 * this.password.Length];
                num = Marshal.SecureStringToBSTR(this.password);
                Marshal.Copy(num, numArray, 0, numArray.Length);
                //if (AppSettings.IsFIPSComplianceRequired())
               // {
                    SHA1 shA1 = (SHA1)new SHA1CryptoServiceProvider();
                    inArray = shA1.ComputeHash(numArray);
                    if (shA1 != null)
                        shA1.Clear();
                //}
               /* else
                {
                    SHA256Managed shA256Managed = new SHA256Managed();
                    inArray = shA256Managed.ComputeHash(numArray);
                    if (shA256Managed != null)
                        shA256Managed.Clear();
                }*/
            }
            finally
            {
                if (num != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(num);
                if (numArray != null)
                    Array.Clear((Array)numArray, 0, numArray.Length);
            }
            return this.Account + Convert.ToBase64String(inArray);
        }

        public bool IsEqualTo(SecureCredential arg)
        {
            return arg != null && CredentialHelper.DoPasswordsMatch(this.Password, arg.Password) && this.FormatAccount().Equals(arg.FormatAccount(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
