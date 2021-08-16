using Agency.Interfaces;
using Agency.Models;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Agency.Controllers
{
    public class NekretnineController : ApiController
    {
        INekretnineRepository _repository { get; set; }

        public NekretnineController(INekretnineRepository repository)
        {
            _repository = repository;
        }

        //GET api/nekretnine
        public IQueryable<NekretninaDTO> GetAll()
        {
            return _repository.GetAll().ProjectTo<NekretninaDTO>();
        }

        //GET api/nekretnine/id
        //[Authorize]
        [ResponseType(typeof(Nekretnina))]
        public IHttpActionResult GetById(int id)
        {
            var nekretnina = _repository.GetById(id);
            if (nekretnina == null)
            {
                return NotFound();
            }
            return Ok(nekretnina);
        }

        //POST api/nekretnine/
        //[Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(Nekretnina nekretnina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Add(nekretnina);
            return CreatedAtRoute("DefaultApi", new { id = nekretnina.Id }, nekretnina);
        }

        //PUT api/nekretnine/id
        //[Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Nekretnina nekretnina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != nekretnina.Id)
            {
                return BadRequest();
            }
            try
            {
                _repository.Update(nekretnina);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(nekretnina);
        }

        //DELETE api/nekretnine/id
        //[Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            var nekretnina = _repository.GetById(id);
            if (nekretnina == null)
            {
                return NotFound();
            }
            _repository.Delete(nekretnina);
            return Ok();
        }

        //GET api/nekretnine?napravljeno={vrednost}
        //[Authorize]
        public IQueryable<NekretninaDTO> GetByGodine(int napravljeno)
        {
            return _repository.GetByGodine(napravljeno).ProjectTo<NekretninaDTO>();
        }

        [Route("api/pretraga")]
        //[Authorize]
        public IQueryable<NekretninaDTO> PostPretraga(int mini, int maksi)
        {
            return _repository.Pretraga(mini, maksi).ProjectTo<NekretninaDTO>();
        }

    }
}
