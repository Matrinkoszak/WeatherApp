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
        [Route("api/users/{token}/{id}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string token, long id)
        {
            using (var serv = new SecurityService(db))
            {
                if (serv.IsTokenValid(token))
                {
                    User user = db.User.Find(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    user.Password = null;
                    return Ok(user);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        // PUT: api/Users/5
        [Route("api/users/{token}/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string token,long id, User user)
        {
            using (var serv = new SecurityService(db))
            {
                if (serv.IsTokenValid(token))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    if (id != user.Id)
                    {
                        return BadRequest();
                    }

                      user.Password = serv.GetHashString(user.Password);

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
                else
                {
                    return Unauthorized();
                }
            }
        }

        // POST: api/Users
        [Route("api/users/{token}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(string token, User user)
        {
            using (var serv = new SecurityService(db))
            {
                if (serv.IsTokenValid(token))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    user.Password = serv.GetHashString(user.Password);

                    db.User.Add(user);
                    db.SaveChanges();

                    return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        // DELETE: api/Users/5
        [Route("api/users/{token}/{id}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string token, long id)
        {
            using (var serv = new SecurityService(db))
            {
                if (serv.IsTokenValid(token))
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
                else
                {
                    return Unauthorized();
                }
            }
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