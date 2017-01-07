using SmartIT.Module.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
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
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:7056/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Company").Result;
            List<Company> unfilteredResults = new List<Company>();
            if (response.IsSuccessStatusCode)
            {
                unfilteredResults = response.Content.ReadAsAsync<IEnumerable<Company>>().Result.ToList<Company>();
                base.WriteObject(unfilteredResults, true);
            }
            else
            {
               string error = "Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase;
                base.WriteObject(error, true);
            }
            

           /* List<Company> unfilteredResults = this.ProcessBase<Company>();

            if (!string.IsNullOrEmpty(this.Name))
                unfilteredResults = unfilteredResults.FindAll((Predicate<Company>)(x => string.Equals(x.Name, Name, StringComparison.Ordinal)));
                */
           

        }
        /*
        private async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:7056/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Company company = await GetObjectAsync("api/Company/");
        }
        protected virtual async Task<Company> GetObjectAsync(string path)
        {
            Company item = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<Company>();
            }
            return item;
        }
        */
    }
}
