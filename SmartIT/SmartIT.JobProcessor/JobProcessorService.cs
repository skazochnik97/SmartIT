using Hangfire;
using LinqToDB;
using NLog;
using SmartIT.Module;
using SmartIT.Module.Model;
using System;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;

namespace SmartIT.JobProcessor
{
    public partial class JobProcessorService : ServiceBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private BackgroundJobServer jobProcessorServer;

        public JobProcessorService()
        {
            InitializeComponent();

         
        }

        protected override void OnStart(string[] args)
        {
            logger.Info("Start application server");


            logger.Info("Initialize database");
            try
            {
                using (var db = new SmartITDataBase())
                {
                    logger.Info("Check scheme database");

                    var sp = db.DataProvider.GetSchemaProvider();
                    var dbSchema = sp.GetSchema(db);

                    if (!dbSchema.Tables.Any(t => t.TableName == "Company"))
                    {
                        logger.Info("Create table Company");
                        db.CreateTable<Company>();
                    }

                    logger.Info("Data import");
                    CompanyRepository companyRep = new CompanyRepository(db);
                    companyRep.Import();

                    logger.Info("Test get companies");
                    foreach (Company c in companyRep.GetAll())
                    {
                        logger.Info(c.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }




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

            logger.Info("Starting JobProcessor");
            try
            {
                jobProcessorServer = new BackgroundJobServer();
                logger.Info("JobProcessor Started!");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }


        }

        protected override void OnStop()
        {
            logger.Info("Stoped application server");
            jobProcessorServer.Dispose();

        }
    }
}
