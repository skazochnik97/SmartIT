using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartIT.Backend.Controllers
{
    public class CompanyController : ApiController
    {
        // GET: api/Comapny
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Comapny/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Comapny
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Comapny/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Comapny/5
        public void Delete(int id)
        {
        }
    }
}
