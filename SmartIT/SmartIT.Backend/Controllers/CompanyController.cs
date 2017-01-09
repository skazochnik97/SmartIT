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
        public IHttpActionResult Post(Company item)
        {
            if (item == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            item = repository.Add(item);

            return Ok<Company>(item);
        }

        // PUT: api/Comapny/5
        public IHttpActionResult Put(int id, Company item)
        {
            if (item == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (repository.Update(item))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/Comapny/5
        public IHttpActionResult Delete(int id)
        {
            Company item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            repository.Remove(id);

            return Ok();
        }

    }
}
