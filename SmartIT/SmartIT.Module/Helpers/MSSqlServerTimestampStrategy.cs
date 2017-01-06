using System;
using DevExpress.Xpo;
using DevExpress.Persistent.AuditTrail;
namespace SmartIT.Module.Helpers
{
    public class MSSqlServerTimestampStrategy : IAuditTimestampStrategy
    {
        DateTime cachedTimestamp;
        #region IAuditTimestampStrategy Members
        public DateTime GetTimestamp(AuditDataItem auditDataItem)
        {
            return cachedTimestamp;
        }
        public void OnBeginSaveTransaction(Session session)
        {
            cachedTimestamp = (DateTime)session.ExecuteScalar("select getdate()");
        }
        #endregion
    }
}