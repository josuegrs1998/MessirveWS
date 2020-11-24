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
using MessirveWS.Models;

namespace MessirveWS.Controllers
{
    [Authorize]
    public class SubCategoriaController : ApiController
    {
        private MessirveWSEntities db = new MessirveWSEntities();

        // GET: api/SubCategoria
        public IQueryable<SubCategoria> GetSubCategorias()
        {
            return db.SubCategorias;
        }

        // GET: api/SubCategoria/5
        [ResponseType(typeof(SubCategoria))]
        public IHttpActionResult GetSubCategoria(int id)
        {
            SubCategoria subCategoria = db.SubCategorias.Find(id);
            if (subCategoria == null)
            {
                return NotFound();
            }

            return Ok(subCategoria);
        }

        // PUT: api/SubCategoria/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubCategoria(int id, SubCategoria subCategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subCategoria.IdSubCategoria)
            {
                return BadRequest();
            }

            db.Entry(subCategoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoriaExists(id))
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

        // POST: api/SubCategoria
        [ResponseType(typeof(SubCategoria))]
        public IHttpActionResult PostSubCategoria(SubCategoria subCategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubCategorias.Add(subCategoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subCategoria.IdSubCategoria }, subCategoria);
        }

        // DELETE: api/SubCategoria/5
        [ResponseType(typeof(SubCategoria))]
        public IHttpActionResult DeleteSubCategoria(int id)
        {
            SubCategoria subCategoria = db.SubCategorias.Find(id);
            if (subCategoria == null)
            {
                return NotFound();
            }

            db.SubCategorias.Remove(subCategoria);
            db.SaveChanges();

            return Ok(subCategoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubCategoriaExists(int id)
        {
            return db.SubCategorias.Count(e => e.IdSubCategoria == id) > 0;
        }
    }
}