using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DirectoresDocentes.Context;
using DirectoresDocentes.Models.BachilleratoModels;

namespace DirectoresDocentes.Models.Repositories
{
    public class ProfesoresRepository : IRepository<PROFESORES_FICHAS>
    {
        private AppDbContext _db;

        public IEnumerable<PROFESORES_FICHAS> GetAll()
        {
            _db = new AppDbContext();
            return _db.Profesores_Fichas
                .Include(r => r.PERSONA_ROLES)
                .Include(p => p.PERSONA_ROLES.PERSONAS);
        }

        public PROFESORES_FICHAS Get(int id)
        {
            _db = new AppDbContext();
            return _db.Profesores_Fichas.FirstOrDefault(c => c.CODIGO == id);
        }

        public PROFESORES_FICHAS Add(PROFESORES_FICHAS profesor)
        {
            _db = new AppDbContext();
            _db.Profesores_Fichas.Add(profesor);
            _db.SaveChanges();
            return profesor;
        }

        public bool Update(int id, PROFESORES_FICHAS profesor)
        {
            _db = new AppDbContext();
            PROFESORES_FICHAS dettachedProfesor = _db.Profesores_Fichas.Find(id);
            if(dettachedProfesor != null)
                _db.Entry(dettachedProfesor).State = EntityState.Detached;
            try
            {
                _db.Entry(profesor).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Remove(int id)
        {
            _db = new AppDbContext();
            var objects = _db.Profesores_Fichas.Find(id);
            _db.Profesores_Fichas.Remove(objects);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}