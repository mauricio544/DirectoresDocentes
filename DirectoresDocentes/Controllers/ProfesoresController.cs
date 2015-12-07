using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Bachillerato.Controllers;
using DirectoresDocentes.Context;
using DirectoresDocentes.Models.ApplicationModels;
using DirectoresDocentes.Models.BachilleratoModels;
using DirectoresDocentes.Models.Repositories;
using Syncfusion.JavaScript;

namespace DirectoresDocentes.Controllers
{
    public class ProfesoresController : ApiController
    {
        static readonly IRepository<PROFESORES_FICHAS> Repository = new ProfesoresRepository();
        private Common funciones;
        private AppDbContext _db;
        private ExactusDbContext _edb;
        // GET api/<controller>
        public IEnumerable<PROFESORES_FICHAS> Get()
        {
            return Repository.GetAll();
        }

        // GET api/<controller>/5
        public PROFESORES_FICHAS Get(int id)
        {
            return Repository.Get(id);
        }

        // POST api/<controller>
        [ResponseType(typeof(PROFESORES_FICHAS))]
        public IHttpActionResult Post(PROFESORES_FICHAS profesor)
        {
            profesor = Repository.Add(profesor);
            return CreatedAtRoute("DefaultApi", new {id = profesor.CODIGO}, profesor);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, PROFESORES_FICHAS profesor)
        {
            var response = Repository.Update(id, profesor);
            return Ok(response);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            Repository.Remove(id);
        }

        [Route("api/Profesores/DataSource")]
        public IHttpActionResult DataSource(DataManager dm)
        {
            funciones = new Common();
            _db = new AppDbContext();
            var currentPeriodo = funciones.PeriodoActual();
            var profesores = _db.Profesores_Fichas.Include("PERSONA_ROLES").Include("PERSONA_ROLES.PERSONAS")                  
                .Where(p => p.PERIODOS_CODIGO == currentPeriodo);
            return Ok(new {result = profesores.OrderBy(c => c.CODIGO).Skip(dm.Skip).Take(dm.Take), count = profesores.Count()});
        }

        [Route("api/Profesores/CapacitacionByDoc")]
        public IHttpActionResult CapacitacionByDoc(string documento, DataManager dm)
        {
            _edb = new ExactusDbContext();
            var titulos =
                _edb.Database.SqlQuery<VIEW_capacitacion>(
                    "SELECT * FROM EXACTUS_REPORT.DBO.VIEW_capacitacion WHERE identificacion = '" + documento + "'");
            return Ok(new { result = titulos, count = titulos.Count() });
        }
    }
}