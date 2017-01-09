using SmartIT.Module.Model;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SmartIT.Module.Cmdlets
{
    [Cmdlet("New", "SITCompany"), OutputType(new System.Type[]
     {
        typeof(Company)
     })]
    public class NewCompanyCmdlet : SmartIT.Module.PowerShellAbstractionLayer.ConnectionCmdlet
    {
        public const string CmdletName = "New-SITCompany";

        [Parameter(Mandatory = true, Position = 0)]
        [ValidateLength(3, 255)]
        public string Name;
        [Parameter]
        public string ShortName;
        [Parameter]
        public DateTime DateCreate;
        [Parameter]
        public DateTime DateCancel;



        public NewCompanyCmdlet() : base()
        {
        }

        protected override void Process()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:7056/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Company company = new Company();
            company.Name = Name;
            if (!string.IsNullOrEmpty(ShortName))
                company.ShortName = ShortName;
            if (DateTime.MinValue != DateCancel)
                company.DateCancel = DateCancel;
            if (DateTime.MinValue != DateCreate)
                company.DateCreate = DateCreate;

            var response = client.PostAsJsonAsync("api/Company", company);
            if (response.Result.IsSuccessStatusCode)
            {
                base.WriteObject(company);
            }
            else
            {
                base.WriteObject(response.Result);
            }
                
        }

    }
}


