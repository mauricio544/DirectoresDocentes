using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using DirectoresDocentes.Models.BachilleratoModels;
using DirectoresDocentes.Models.Repositories;

namespace DirectoresDocentes.Controllers
{
    public class PersonaController : ApiController
    {
        private static readonly IRepository<PERSONAS> Repository = new PersonaRepository();
        // GET api/<controller>
        public IEnumerable<PERSONAS> Get()
        {
            return Repository.GetAll();
        }

        // GET api/<controller>/5
        public PERSONAS Get(int id)
        {
            return Repository.Get(id);  
        }

        // POST api/<controller>
        [ResponseType(typeof (PERSONAS))]
        public IHttpActionResult Post(PERSONAS person)
        {
            person = Repository.Add(person);
            return CreatedAtRoute("DefaultApi", new {id = person.CODIGO}, person);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, PERSONAS person)
        {
            var response = Repository.Update(id, person);
            return Ok(response);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}