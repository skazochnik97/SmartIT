using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.Module.Helpers
{
   
    // строка подключения к серверу SmartIT

    public class ConnectionSettings
    {
       // private const string DefaultServer = "localhost";
        private const string ServerUri = "ServerURI";
      //  private const int DefaultPort = 1451;
      //  private const string SmartITServerRegistryKeyName = "SmartITServerName";
       // private const string SmartITPortRegistryKeyName = "SmartITServerPort";
       // private const string UserRoleRegistryKeyName = "userRoleName";
        private const string UserNameRegistryKeyName = "UserName";
        private const string AutoConnectRegistryKeyName = "AutoConnect";
        private const string PasswordRegistryKeyName = "Password";

        private string server;
        public string Server
        {
            get
            {
                return this.server;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new System.ArgumentNullException("Server cannot be null or empty.");
                }
                this.server = value;
            }
        }
     /*   public int Port
        {
            get;
            set;
        }
       */ public Uri ServerURI
        {
            get;
            set;
        }
        /*public string UserRole
        {
            get;
            set;
        }*/
        public bool UseCurrentCredentials
        {
            get;
            set;
        }
        public bool AutoConnect
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        /*
        public long? MaxBufferPoolSizeBytes
        {
            get;
            set;
        }*/
        private string RegistryKey
        {
            get
            {
                return "Software\\SmartIT\\Settings";
            }
        }
        public void Save()
        {
         //   RegistryUtils.SetUserSpecificRegistryValue(this.RegistryKey, "defaultVMMServerName", this.Server);
          //  RegistryUtils.SetUserSpecificRegistryValue(this.RegistryKey, "vmmServerPort", this.Port);
          //  RegistryUtils.SetUserSpecificRegistryValue(this.RegistryKey, "userRoleName", this.UserRole);
            RegistryUtils.SetUserSpecificRegistryValue(this.RegistryKey, UserNameRegistryKeyName, this.UseCurrentCredentials ? string.Empty : this.UserName);
            RegistryUtils.SetUserSpecificRegistryValue(this.RegistryKey, AutoConnectRegistryKeyName, this.AutoConnect);
            RegistryUtils.SetUserSpecificRegistryValue(this.RegistryKey, ServerUri, this.ServerURI);
            RegistryUtils.SetUserSpecificRegistryValue(this.RegistryKey, PasswordRegistryKeyName, this.Password);
        }
        public bool Load()
        {
            bool result = false;
            string value = RegistryUtils.ReadUserSpecificRegistryValue<string>(this.RegistryKey, ServerUri, null);
            if (!string.IsNullOrEmpty(value))
            {
                result = true;
                this.ServerURI = new Uri(value);
              //  this.Port = RegistryUtils.ReadUserSpecificRegistryValue<int>(this.RegistryKey, "vmmServerPort", 8100);
              //  this.UserRole = RegistryUtils.ReadUserSpecificRegistryValue<string>(this.RegistryKey, "userRoleName", string.Empty);
                this.UserName = RegistryUtils.ReadUserSpecificRegistryValue<string>(this.RegistryKey, UserNameRegistryKeyName, string.Empty);
                this.UseCurrentCredentials = string.IsNullOrEmpty(this.UserName);
                string value2 = RegistryUtils.ReadUserSpecificRegistryValue<string>(this.RegistryKey, AutoConnectRegistryKeyName, "False");
                this.AutoConnect = bool.Parse(value2);
                this.Password = RegistryUtils.ReadUserSpecificRegistryValue<string>(this.RegistryKey, PasswordRegistryKeyName, string.Empty);

                //  this.MaxBufferPoolSizeBytes = RegistryUtils.ReadUserSpecificRegistryValue<long?>(this.RegistryKey, "MaxBufferPoolSizeBytes", null);
            }
            return result;
        }

        public ConnectionSettings()
        {
            this.SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            //  this.Server = "localhost";
            //this.Port = 1451;
            this.UserName = "Administrator";// = string.Empty;
            this.Password = "Administrator";
            this.UseCurrentCredentials = false;
            this.AutoConnect = false;
        //    this.MaxBufferPoolSizeBytes = null;
            this.ServerURI = new Uri("http://localhost:8000/RemoteService");
        }
    }
    

}
