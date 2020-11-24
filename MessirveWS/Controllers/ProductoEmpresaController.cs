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
    public class ProductoEmpresaController : ApiController
    {
        private MessirveWSEntities db = new MessirveWSEntities();

        // GET: api/ProductoEmpresa
        public IQueryable<ProductoEmpresa> GetProductoEmpresas()
        {
            return db.ProductoEmpresas;
        }

        // GET: api/ProductoEmpresa/5
        [ResponseType(typeof(ProductoEmpresa))]
        public IHttpActionResult GetProductoEmpresa(int id)
        {
            ProductoEmpresa productoEmpresa = db.ProductoEmpresas.Find(id);
            if (productoEmpresa == null)
            {
                return NotFound();
            }

            return Ok(productoEmpresa);
        }

        // PUT: api/ProductoEmpresa/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductoEmpresa(int id, ProductoEmpresa productoEmpresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productoEmpresa.IdProductoEmpresa)
            {
                return BadRequest();
            }

            db.Entry(productoEmpresa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoEmpresaExists(id))
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

        // POST: api/ProductoEmpresa
        [ResponseType(typeof(ProductoEmpresa))]
        public IHttpActionResult PostProductoEmpresa(ProductoEmpresa productoEmpresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductoEmpresas.Add(productoEmpresa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productoEmpresa.IdProductoEmpresa }, productoEmpresa);
        }

        // DELETE: api/ProductoEmpresa/5
        [ResponseType(typeof(ProductoEmpresa))]
        public IHttpActionResult DeleteProductoEmpresa(int id)
        {
            ProductoEmpresa productoEmpresa = db.ProductoEmpresas.Find(id);
            if (productoEmpresa == null)
            {
                return NotFound();
            }

            db.ProductoEmpresas.Remove(productoEmpresa);
            db.SaveChanges();

            return Ok(productoEmpresa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoEmpresaExists(int id)
        {
            return db.ProductoEmpresas.Count(e => e.IdProductoEmpresa == id) > 0;
        }
    }
}