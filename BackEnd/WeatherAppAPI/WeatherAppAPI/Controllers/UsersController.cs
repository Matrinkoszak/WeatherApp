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
using WeatherAppAPI.Models;
using WeatherAppAPI.Services;

namespace WeatherAppAPI.Controllers
{
    public class UsersController : ApiController
    {
        private WeatherApp_dbEntities db = new WeatherApp_dbEntities();

        

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(long id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Password = null;
            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(long id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            using(var serv = new SecurityService(db))
            {
                user.Password = serv.GetHashString(user.Password);
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var serv = new SecurityService(db))
            {
                user.Password = serv.GetHashString(user.Password);
            }

            db.User.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(long id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.User.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(long id)
        {
            return db.User.Count(e => e.Id == id) > 0;
        }

    }
}