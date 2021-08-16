using Agency.Interfaces;
using Agency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agency.Repository
{
    public class AgentiRepository : IAgentiRepository, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IQueryable<Agent> GetAll()
        {
            return db.Agenti;
        }

        public Agent GetById(int id)
        {
            return db.Agenti.Find(id);
        }

        public IQueryable<Agent> GetEkstrem()
        {
            int maximum = db.Agenti.Select(x => x.BrojProdatihNekretnina).Max();
            int minimum = db.Agenti.Select(x => x.BrojProdatihNekretnina).Min();
            Agent max = db.Agenti.Where(x => x.BrojProdatihNekretnina == maximum).SingleOrDefault();
            Agent min = db.Agenti.Where(x => x.BrojProdatihNekretnina == minimum).SingleOrDefault();

            List<Agent> agenti = new List<Agent>();
            agenti.Add(max);
            agenti.Add(min);
            return agenti.AsQueryable();
        }

        public IQueryable<Agent> GetNajmladji()
        {
            return db.Agenti.OrderByDescending(x => x.GodinaRodjenja);
        }
    }
}