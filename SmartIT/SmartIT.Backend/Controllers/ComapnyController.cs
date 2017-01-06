using SmartIT.Module;
using SmartIT.Module.Model;
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
        static readonly ICompanyRepository repository = new CompanyRepository();

        // GET: api/Comapny
        public IEnumerable<Company> Get()
        {
            return repository.GetAll();
        }

        // GET: api/Comapny/5
        public Company Get(int id)
        {
            return repository.Get(id);
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
            repository.Remove(id);
        }
    }
}
