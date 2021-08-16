using Agency.Interfaces;
using Agency.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Agency.Repository
{
    public class NekretnineRepository : INekretnineRepository, IDisposable
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

        public IQueryable<Nekretnina> GetAll()
        {
            return db.Nekretnine.OrderByDescending(x => x.Cena);
        }

        public Nekretnina GetById(int id)
        {
            return db.Nekretnine.Find(id);
        }

        public void Add(Nekretnina nekretnina)
        {
            db.Nekretnine.Add(nekretnina);
            db.SaveChanges();
        }

        public void Update(Nekretnina nekretnina)
        {
            db.Entry(nekretnina).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Nekretnina nekretnina)
        {
            db.Nekretnine.Remove(nekretnina);
            db.SaveChanges();
        }

        public IQueryable<Nekretnina> GetByGodine(int napravljeno)
        {
            return db.Nekretnine.Where(x => x.GodinaIzgradnje > napravljeno).OrderBy(x => x.GodinaIzgradnje);
        }

        public IQueryable<Nekretnina> Pretraga(int mini, int maksi)
        {
            return db.Nekretnine.Where(x => x.Kvadratura >= mini && x.Kvadratura < maksi).OrderBy(x => x.Kvadratura);
        }
    }
}