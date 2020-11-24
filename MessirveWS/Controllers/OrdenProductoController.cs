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
    public class OrdenProductoController : ApiController
    {
        private MessirveWSEntities db = new MessirveWSEntities();

        // GET: api/OrdenProducto
        public IQueryable<OrdenProducto> GetOrdenProductoes()
        {
            return db.OrdenProductoes;
        }

        // GET: api/OrdenProducto/5
        [ResponseType(typeof(OrdenProducto))]
        public IHttpActionResult GetOrdenProducto(int id)
        {
            OrdenProducto ordenProducto = db.OrdenProductoes.Find(id);
            if (ordenProducto == null)
            {
                return NotFound();
            }

            return Ok(ordenProducto);
        }

        // PUT: api/OrdenProducto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrdenProducto(int id, OrdenProducto ordenProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ordenProducto.IdOrdenProducto)
            {
                return BadRequest();
            }

            db.Entry(ordenProducto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenProductoExists(id))
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

        // POST: api/OrdenProducto
        [ResponseType(typeof(OrdenProducto))]
        public IHttpActionResult PostOrdenProducto(OrdenProducto ordenProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrdenProductoes.Add(ordenProducto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ordenProducto.IdOrdenProducto }, ordenProducto);
        }

        // DELETE: api/OrdenProducto/5
        [ResponseType(typeof(OrdenProducto))]
        public IHttpActionResult DeleteOrdenProducto(int id)
        {
            OrdenProducto ordenProducto = db.OrdenProductoes.Find(id);
            if (ordenProducto == null)
            {
                return NotFound();
            }

            db.OrdenProductoes.Remove(ordenProducto);
            db.SaveChanges();

            return Ok(ordenProducto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdenProductoExists(int id)
        {
            return db.OrdenProductoes.Count(e => e.IdOrdenProducto == id) > 0;
        }
    }
}