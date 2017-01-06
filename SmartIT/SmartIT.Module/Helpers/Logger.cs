using DevExpress.ExpressApp;
using DevExpress.Xpo;
using SmartIT.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.Module.Helpers
{
    public class Logger
    {
        private static Logger logger;
        private Session session;

        public Logger(Session session)
        {
            this.session = session;
        }
        public static Logger GetInstance(Session newSession)
        {

            if (logger == null)
                logger = new Logger(newSession);
            else
            {
                if (logger.session != newSession)
                {
                    logger.session = newSession;
                }
            }
            return logger;
        }

        public void AddEvent(string descr, LogAction action)
        {

            LogRecord logRecord = new LogRecord(session);
            if (SecuritySystem.CurrentUserName != "")
                logRecord.UserName = SecuritySystem.CurrentUserName;
            else
                logRecord.UserName = "System";
            logRecord.ModifiedOn = DateTime.Now;
            logRecord.Description = descr;
            logRecord.Action = action;
            logRecord.Save();
            session.CommitTransaction();
        }


        public void AddEvent(string descr, LogAction action, string sysname)
        {
            LogRecord logRecord = new LogRecord(session);
            if (SecuritySystem.CurrentUserName != "")
                logRecord.UserName = SecuritySystem.CurrentUserName;
            else
                logRecord.UserName = "System";
            logRecord.ModifiedOn = DateTime.Now;
            logRecord.Description = descr;
            logRecord.Action = action;
            logRecord.SysName = sysname;
            logRecord.Save();
            session.CommitTransaction();
        }

    }
}
