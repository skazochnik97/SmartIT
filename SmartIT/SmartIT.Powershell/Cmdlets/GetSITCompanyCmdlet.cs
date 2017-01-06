using SmartIT.Module.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.Module.Cmdlets
{
    [Cmdlet("Get", "SITCompany", DefaultParameterSetName = "All"), OutputType(typeof(Company))]
    public class GetSITCompanyCmdlet : SmartIT.Module.PowerShellAbstractionLayer.GetCmdletBase<Company>
    {
        public const string CmdletName = "Get-SITCompany";

        [Parameter(Mandatory = false, ParameterSetName = "Name", ValueFromPipeline = false)]
        public string Name;


        public GetSITCompanyCmdlet() : base()
        {
        }

        protected override void Process()
        {

            List<Company> unfilteredResults = this.ProcessBase<Company>();

            if (!string.IsNullOrEmpty(this.Name))
                unfilteredResults = unfilteredResults.FindAll((Predicate<Company>)(x => string.Equals(x.Name, Name, StringComparison.Ordinal)));

            base.WriteObject(unfilteredResults, true);

        }
       
    }
}
