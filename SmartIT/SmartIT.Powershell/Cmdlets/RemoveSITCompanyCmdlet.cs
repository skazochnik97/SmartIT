using SmartIT.Module.Model;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SmartIT.Module.Cmdlets
{
    [Cmdlet("Remove", "SITCompany"), OutputType(typeof(Company))]
    public class RemoveSITCompanyCmdlet : SmartIT.Module.PowerShellAbstractionLayer.ConnectionCmdlet
    {
        public const string CmdletName = "Remove-SITCompany";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public Company Company;

        public RemoveSITCompanyCmdlet() : base()
        {
        }

        protected override void Process()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:7056/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            var response = client.DeleteAsync($"api/Company{Company.Id}");
            if (response.Result.IsSuccessStatusCode)
            {
                base.WriteObject(response.Result);
            }
            else
            {
                base.WriteObject(response.Result);
            }
        }

    }


}


