using Agency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Interfaces
{
    public interface IAgentiRepository
    {
        IQueryable<Agent> GetAll();
        Agent GetById(int id);
        IQueryable<Agent> GetEkstrem();
        IQueryable<Agent> GetNajmladji();
    }
}
