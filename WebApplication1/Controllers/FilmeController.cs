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
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FilmeController : ApiController
    {
        private rhuanDBEntities db = new rhuanDBEntities();

        // GET: api/Filme
        public IQueryable<Filme> GetFilme()
        {
            return db.Filme;
        }

        // GET: api/Filme/5
        [ResponseType(typeof(Filme))]
        public IHttpActionResult GetFilme(int id)
        {
            Filme filme = db.Filme.Find(id);
            if (filme == null)
            {
                return NotFound();
            }

            return Ok(filme);
        }

        // PUT: api/Filme/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFilme(int id, Filme filme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filme.IdFilme)
            {
                return BadRequest();
            }

            db.Entry(filme).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmeExists(id))
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

        // POST: api/Filme
        [ResponseType(typeof(Filme))]
        public IHttpActionResult PostFilme(Filme filme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Filme.Add(filme);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = filme.IdFilme }, filme);
        }

        // DELETE: api/Filme/5
        [ResponseType(typeof(Filme))]
        public IHttpActionResult DeleteFilme(int id)
        {
            Filme filme = db.Filme.Find(id);
            if (filme == null)
            {
                return NotFound();
            }

            db.Filme.Remove(filme);
            db.SaveChanges();

            return Ok(filme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilmeExists(int id)
        {
            return db.Filme.Count(e => e.IdFilme == id) > 0;
        }
    }
}