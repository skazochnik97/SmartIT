using SmartIT.Module.Authentification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using SmartIT.Module.BusinessObjects;
using DevExpress.Data.Filtering;

namespace SmartIT.Module.Helpers
{
    /// <summary>
    /// Manager for connections to Active Directory
    /// </summary>
    public class ActiveDirectoryMgmtFabrica
    {
        private static List<ActiveDirectoryController> listADMgmt;
        private static IObjectSpace session;

        public static ActiveDirectoryController GetADMethods(string domainName)
        {
            if (listADMgmt.Exists(x => x.DomainName.Equals(domainName)))
            {
                return listADMgmt.Find(x => x.DomainName.Equals(domainName));
            }
            else
            {
                var listAD = session.GetObjects<ADDomain>(CriteriaOperator.Parse("(Name=?)", domainName)); //new XPCollection<Domain>(session, CriteriaOperator.Parse("(Name=?)", domainName));
                if (listAD != null)
                    if (listAD.Count == 1)
                    {
                        ADDomain adDomain = listAD[0];
                        ActiveDirectoryController newADMgmt = new ActiveDirectoryController(
                         adDomain.Name, adDomain.DomainController, adDomain.DefaultRootOU, adDomain.Username, adDomain.Password);
                        listADMgmt.Add(newADMgmt);
                        return listADMgmt.Find(x => x.DomainName.Equals(domainName));
                    }
                    else
                        throw new Exception(string.Format("Domain {0} not found!", domainName));
                //  throw new Exception("Few Domain has the same name " + domainName);
                else
                    throw new Exception(string.Format("Domain {0} not found!", domainName));

            }
        }
        public static void Initialize(IObjectSpace newSession)
        {

            if (session != newSession)
            {
                session = newSession;
            }

            if (listADMgmt == null)
            {
                listADMgmt = new List<ActiveDirectoryController>();
            }

            var listADDomains = session.GetObjects<ADDomain>();// new XPCollection<Domain>(session. (session);
            if (listADDomains != null)
            {
                foreach (ADDomain adDomain in listADDomains)
                {
                    if (!listADMgmt.Exists(x => x.DomainName.Equals(adDomain.Name)))
                    {
                        ActiveDirectoryController newADMgmt = new ActiveDirectoryController(
                     adDomain.Name,adDomain.DomainController, adDomain.DefaultRootOU, adDomain.Username, adDomain.Password);
                        listADMgmt.Add(newADMgmt);
                    }

                }
            }
        }

    }
}
