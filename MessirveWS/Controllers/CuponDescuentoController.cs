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
    public class CuponDescuentoController : ApiController
    {
        private MessirveWSEntities db = new MessirveWSEntities();

        // GET: api/CuponDescuento
        public IQueryable<CuponDescuento> GetCuponDescuentoes()
        {
            return db.CuponDescuentoes;
        }

        // GET: api/CuponDescuento/5
        [ResponseType(typeof(CuponDescuento))]
        public IHttpActionResult GetCuponDescuento(int id)
        {
            CuponDescuento cuponDescuento = db.CuponDescuentoes.Find(id);
            if (cuponDescuento == null)
            {
                return NotFound();
            }

            return Ok(cuponDescuento);
        }

        // PUT: api/CuponDescuento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCuponDescuento(int id, CuponDescuento cuponDescuento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuponDescuento.IdCupon)
            {
                return BadRequest();
            }

            db.Entry(cuponDescuento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuponDescuentoExists(id))
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

        // POST: api/CuponDescuento
        [ResponseType(typeof(CuponDescuento))]
        public IHttpActionResult PostCuponDescuento(CuponDescuento cuponDescuento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CuponDescuentoes.Add(cuponDescuento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cuponDescuento.IdCupon }, cuponDescuento);
        }

        // DELETE: api/CuponDescuento/5
        [ResponseType(typeof(CuponDescuento))]
        public IHttpActionResult DeleteCuponDescuento(int id)
        {
            CuponDescuento cuponDescuento = db.CuponDescuentoes.Find(id);
            if (cuponDescuento == null)
            {
                return NotFound();
            }

            db.CuponDescuentoes.Remove(cuponDescuento);
            db.SaveChanges();

            return Ok(cuponDescuento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuponDescuentoExists(int id)
        {
            return db.CuponDescuentoes.Count(e => e.IdCupon == id) > 0;
        }
    }
}