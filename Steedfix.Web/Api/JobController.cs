using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Steedfix.Data.Interfaces;
using Steedfix.Data.Repositories;
using Steedfix.Domain;

namespace Steedfix.Web.Api
{
    public class JobController : ApiController
    {
        // GET: api/Job
        public IEnumerable<Job> Get()
        {
            IJobRepository repository = new JobRepository();
            return repository.All;
        }

        // GET: api/Job/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Job
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Job/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Job/5
        public void Delete(int id)
        {
        }
    }
}
