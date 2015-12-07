using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DirectoresDocentes.Context;
using DirectoresDocentes.Models.BachilleratoModels;

namespace DirectoresDocentes.Models.Repositories
{
    public class PersonaRepository : IRepository<PERSONAS>
    {
        private AppDbContext db;

        public IEnumerable<PERSONAS> GetAll()
        {
            db = new AppDbContext();
            return db.Personas;
        }

        public PERSONAS Get(int id)
        {
            db = new AppDbContext();
            return db.Personas.First(c => c.CODIGO == id);
        }

        public PERSONAS Add(PERSONAS person)
        {
            db = new AppDbContext();
            db.Personas.Add(person);
            db.SaveChanges();
            return person;
        }

        public bool Update(int id, PERSONAS person)
        {
            db = new AppDbContext();
            var dettachedPersonas = db.Personas.Find(id);
            if (dettachedPersonas != null)
                db.Entry(dettachedPersonas).State = EntityState.Detached;
            try
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Remove(int id)
        {
            db = new AppDbContext();
            var person = db.Personas.Find(id);
            db.Personas.Remove(person);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}