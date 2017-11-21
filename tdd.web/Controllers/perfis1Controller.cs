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
    public class perfis1Controller : ApiController
    {
        private icatuDBEntities db = new icatuDBEntities();
        private IperfisController controller;

        public perfis1Controller()
        {

        }

        public perfis1Controller(IperfisController _controller)
        {
            this.controller = _controller;
        }


        // GET: api/perfis1
        public IQueryable<perfis> Getperfis()
        {
            return db.perfis;
        }

        // GET: api/perfis1/5
        [ResponseType(typeof(perfis))]
        public IHttpActionResult Getperfis(int id)
        {
            perfis perfis = db.perfis.Find(id);
            if (perfis == null)
            {
                return NotFound();
            }

            return Ok(perfis);
        }

        // PUT: api/perfis1/5
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

            db.Entry(perfis).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!perfisExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/perfis1
        [ResponseType(typeof(perfis))]
        public IHttpActionResult Postperfis(perfis perfis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.perfis.Add(perfis);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (perfisExists(perfis.id_perfil))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = perfis.id_perfil }, perfis);
        }

        // DELETE: api/perfis1/5
        [ResponseType(typeof(perfis))]
        public IHttpActionResult Deleteperfis(int id)
        {
            perfis perfis = db.perfis.Find(id);
            if (perfis == null)
            {
                return NotFound();
            }

            db.perfis.Remove(perfis);
            db.SaveChanges();

            return Ok(perfis);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool perfisExists(int id)
        {
            return db.perfis.Count(e => e.id_perfil == id) > 0;
        }
    }
}