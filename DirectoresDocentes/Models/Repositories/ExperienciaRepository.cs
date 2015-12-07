using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DirectoresDocentes.Context;
using DirectoresDocentes.Models.ApplicationModels;

namespace DirectoresDocentes.Models.Repositories
{
    public class ExperienciaRepository : IExactus<VIEW_capacitacion>
    {
        private ExactusDbContext _db;
        public IEnumerable<VIEW_capacitacion> GetAll()
        {
            _db = new ExactusDbContext();
            return _db.Database.SqlQuery<VIEW_capacitacion>("SELECT * FROM EXACTUS_REPORT.DBO.VIEW_capacitacion");
        }
    }
}