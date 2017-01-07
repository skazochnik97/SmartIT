using SmartIT.Module.Cmdlets;
using SmartIT.Module.Model;
using SmartIT.Module.PowerShellAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
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
               
            }
            else
            {
                string error = "Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase;
               
            }



        }
    }
}
