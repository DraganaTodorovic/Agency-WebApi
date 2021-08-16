using Agency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Interfaces
{
    public interface INekretnineRepository
    {
        IQueryable<Nekretnina> GetAll();
        Nekretnina GetById(int id);
        void Add(Nekretnina nekretnina);
        void Update(Nekretnina nekretnina);
        void Delete(Nekretnina nekretnina);
        IQueryable<Nekretnina> GetByGodine(int napravljeno);
        IQueryable<Nekretnina> Pretraga(int mini, int maksi);
    }
}
