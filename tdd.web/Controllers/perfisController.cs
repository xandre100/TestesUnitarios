using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using tdd.model.entity;

namespace tdd.web.Controllers
{
    public class perfisController : ApiController
    {
        private icatuDBEntities db = new icatuDBEntities();
        private IPerfilRepository repository;
        private IperfisController controller;

        public perfisController()
        {
            if (repository == null)
                repository = new PerfilRepository();
        }

        public perfisController(IperfisController _controller, IPerfilRepository _repository)
        {
            this.controller = _controller;
            this.repository = _repository;
        }

        public perfisController(IPerfilRepository _repository)
        {
            this.repository = _repository;
        }
        
        // GET: api/perfis
        public IQueryable<perfis> Getperfis()
        {
            return this.repository.Getperfis();
        }

        // GET: api/perfis/5
        [ResponseType(typeof(perfis))]
        public IHttpActionResult Getperfis(int id)
        {
            perfis perfis = this.repository.Getperfis(id);
            if (perfis == null)
            {
                return NotFound();
            }

            return Ok(perfis);
        }

        // PUT: api/perfis/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putperfis(int id, perfis perfis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != perfis.id_perfil)
            {
                return BadRequest();
            }
            
            try
            {
                this.repository.Putperfis(id, perfis);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/perfis
        [ResponseType(typeof(perfis))]
        public IHttpActionResult Postperfis(perfis perfis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                this.repository.Postperfis(perfis);
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

            return CreatedAtRoute("DefaultApi", new { id = perfis.id_perfil }, perfis);
        }

        // DELETE: api/perfis/5
        [ResponseType(typeof(perfis))]
        public IHttpActionResult Deleteperfis(int id)
        {
            if(this.repository.Getperfis(id) == null)
                return NotFound();

            perfis perfil = this.repository.Deleteperfis(id);

            if (perfil == null)
                return NotFound();

            return Ok(perfil);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}