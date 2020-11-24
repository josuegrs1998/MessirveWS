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
    public class LoginController : ApiController
    {
        private MessirveWSEntities db = new MessirveWSEntities();

        // GET: api/Login
        public IQueryable<Login> GetLogins()
        {
            return db.Logins;
        }

        // GET: api/Login/5
        [ResponseType(typeof(Login))]
        public IHttpActionResult GetLogin(int id)
        {
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }

        //Get LoginByName
        [ResponseType(typeof(Login))]
        public IHttpActionResult GetLoginName(string username)
        {
            MessirveWSEntities db = new MessirveWSEntities();
            var result = db.Logins.Where(x => x.Username.Equals(username));
          
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Login/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLogin(int id, Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != login.IdLogin)
            {
                return BadRequest();
            }

            db.Entry(login).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
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

        // POST: api/Login
        [ResponseType(typeof(Login))]
        public IHttpActionResult PostLogin(Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            /*EncriptarClave*/
            string encriptar = login.Password;
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(encriptar);
            result = Convert.ToBase64String(encryted);
            login.Password = result;
            db.Logins.Add(login);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = login.IdLogin }, login);
        }

        // DELETE: api/Login/5
        [ResponseType(typeof(Login))]
        public IHttpActionResult DeleteLogin(int id)
        {
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return NotFound();
            }

            db.Logins.Remove(login);
            db.SaveChanges();

            return Ok(login);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoginExists(int id)
        {
            return db.Logins.Count(e => e.IdLogin == id) > 0;
        }
    }
}