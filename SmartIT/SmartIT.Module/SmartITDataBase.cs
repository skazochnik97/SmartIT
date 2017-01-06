
using LinqToDB;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.SqlServer;
using SmartIT.Module.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SmartIT.Module
{
    public class SmartITDataBase : LinqToDB.Data.DataConnection
    {
        public SmartITDataBase() : base(GetDataProvider(), GetConnection()) { }

        public ITable<Company> Company { get { return GetTable<Company>(); } }

        private static IDataProvider GetDataProvider()
        {
            return new SqlServerDataProvider("", SqlServerVersion.v2012);
        }

        private static IDbConnection GetConnection()
        {
            LinqToDB.Common.Configuration.AvoidSpecificDataProviderAPI = true;

            string connectionString = "";
#if DEBUG
           
            if (ConfigurationManager.ConnectionStrings["SmartITDataBase_debug"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["SmartITDataBase_debug"].ConnectionString;
            }
#endif


            var dbConnection = new SqlConnection(connectionString);
            return dbConnection;
        }

    }
}

