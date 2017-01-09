using SmartIT.Module.Model;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SmartIT.Module.Cmdlets
{
    [Cmdlet("Set", "SITCompany"), OutputType(typeof(Company))]
    public class SetSITCompanyCmdlet : SmartIT.Module.PowerShellAbstractionLayer.ConnectionCmdlet
    {
        public const string CmdletName = "Set-SITCompany";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public Company Company;

        [ValidateLength(3, 255)]
        public string Name;
        [Parameter]
        public string ShortName;
        [Parameter]
        public DateTime DateCreate;
        [Parameter]
        public DateTime DateCancel;


        public SetSITCompanyCmdlet() : base()
        {
        }

        protected override void Process()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:7056/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(Name))
                Company.Name = Name;
            if (!string.IsNullOrEmpty(ShortName))
                Company.ShortName = ShortName;
            if (DateTime.MinValue != DateCancel)
                Company.DateCancel = DateCancel;
            if (DateTime.MinValue != DateCreate)
                Company.DateCreate = DateCreate;

            var response = client.PutAsJsonAsync($"api/products/{Company.Id}", Company);
            if (response.Result.IsSuccessStatusCode)
            {
                base.WriteObject(Company);
            }
            else
            {
                base.WriteObject(response.Result);
            }
        }
    }

}

