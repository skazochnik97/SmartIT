using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartIT.Module.BusinessObjects;

namespace SmartIT.Module.Helpers
{
    public class SchedulerManager
    {
        private static SchedulerManager schedulerManagerSingeton;
        private Session session;

        public SchedulerManager(Session session)
        {
            this.session = session;
        }
        public static SchedulerManager GetInstance(Session newSession)
        {

            if (schedulerManagerSingeton == null)
                schedulerManagerSingeton = new SchedulerManager(newSession);
            else
            {
                if (schedulerManagerSingeton.session != newSession)
                {
                    schedulerManagerSingeton.session = newSession;
                }
            }

            return schedulerManagerSingeton;
        }

        /// <summary>
        /// count of jobs in status progress
        /// </summary>
        /// <returns></returns>
        public int GetActiveJobCount()
        {
            return 0;
        }


        public bool AddActionToScheduler(BusinessObjects.Action action)
        {
            bool status = true;
            SingleExecutionJob singleExecJob = new SingleExecutionJob(session);
            SITStepBase baseTask = new SITStepBase(session);
            baseTask.Action = action;
            singleExecJob.Name = action.Name;
            singleExecJob.Steps.Add(baseTask);
            singleExecJob.Save();
            session.CommitTransaction();
            return status;
        }

        public bool AddActionToScheduler( BusinessObjects.Action[] actions, string jobName)
        {
            bool status = true;
            SingleExecutionJob singleExecJob = new SingleExecutionJob(session);
            if (jobName != "") singleExecJob.Name = jobName;
            int idx = 0;
            foreach (BusinessObjects.Action act in actions)
            {
                SITStepBase baseTask = new SITStepBase(session);
                baseTask.Action = act;
                baseTask.Index = idx;
                idx++;
                //     adduserToGroupTask.Group = (string)parameterts[0];
                //   adduserToGroupTask.Login = (string)parameterts[1];

                singleExecJob.Steps.Add(baseTask);
            }
           
            singleExecJob.Save();
            session.CommitTransaction();
            return status;
        }


    }
}
