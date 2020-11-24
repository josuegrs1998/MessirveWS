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
    public class OrdenController : ApiController
    {
        private MessirveWSEntities db = new MessirveWSEntities();

        // GET: api/Orden
        public IQueryable<Orden> GetOrdens()
        {
            return db.Ordens;
        }

        // GET: api/Orden/5
        [ResponseType(typeof(Orden))]
        public IHttpActionResult GetOrden(int id)
        {
            Orden orden = db.Ordens.Find(id);
            if (orden == null)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        // PUT: api/Orden/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrden(int id, Orden orden)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orden.IdOrden)
            {
                return BadRequest();
            }

            db.Entry(orden).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenExists(id))
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

        // POST: api/Orden
        [ResponseType(typeof(Orden))]
        public IHttpActionResult PostOrden(Orden orden)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ordens.Add(orden);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orden.IdOrden }, orden);
        }

        // DELETE: api/Orden/5
        [ResponseType(typeof(Orden))]
        public IHttpActionResult DeleteOrden(int id)
        {
            Orden orden = db.Ordens.Find(id);
            if (orden == null)
            {
                return NotFound();
            }

            db.Ordens.Remove(orden);
            db.SaveChanges();

            return Ok(orden);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdenExists(int id)
        {
            return db.Ordens.Count(e => e.IdOrden == id) > 0;
        }
    }
}