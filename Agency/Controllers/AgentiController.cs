using Agency.Interfaces;
using Agency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Agency.Controllers
{
    public class AgentiController : ApiController
    {
        IAgentiRepository _repository { get; set; }

        public AgentiController(IAgentiRepository repository)
        {
            _repository = repository;
        }

        //GET api/agenti
        public IQueryable<Agent> GetAll()
        {
            return _repository.GetAll();
        }

        //GET api/agenti/{id}
        //[Authorize]
        [ResponseType(typeof(Agent))]
        public IHttpActionResult GetById(int id)
        {
            var agent = _repository.GetById(id);
            if (agent == null)
            {
                return NotFound();
            }
            return Ok(agent);
        }

        //[Authorize]
        [Route("api/ekstremi")]
        public IQueryable<Agent> GetEkstrem()
        {
            return _repository.GetEkstrem();
        }

        //[Authorize]
        [Route("api/najmladji")]
        public IQueryable<Agent> GetNajmladji()
        {
            return _repository.GetNajmladji();
        }

    }
}
