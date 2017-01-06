using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using NLog;
using System.Configuration;

[assembly: OwinStartup(typeof(SmartIT.Backend.Startup))]

namespace SmartIT.Backend
{
    public class Startup
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Configuration(IAppBuilder app)
        {
            logger.Info("Initialization JobProcessor");
            string connectionString = "";
#if DEBUG
            if (ConfigurationManager.ConnectionStrings["JobProcessorDataBase_debug"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["JobProcessorDataBase_debug"].ConnectionString;
            }
#endif
            try
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            app.UseHangfireDashboard();

            var client = new BackgroundJobClient();

            client.Enqueue(() => Console.WriteLine($"Starting backend - webapi {DateTime.Now.ToString()}!"));
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
