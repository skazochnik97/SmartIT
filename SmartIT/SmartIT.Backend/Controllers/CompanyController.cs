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
        public Company Post(Company item)
        {
            item = repository.Add(item);
            return item;
        }

        // PUT: api/Comapny/5
        public void Put(int id, Company item)
        {
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // DELETE: api/Comapny/5
        public void Delete(int id)
        {
            Company item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }

    }
}
