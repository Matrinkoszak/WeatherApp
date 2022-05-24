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

namespace WeatherAppAPI.Controllers
{
    public class locationsController : ApiController
    {
        private WeatherApp_dbEntities db = new WeatherApp_dbEntities();

        // GET: api/locations
        public IQueryable<location> Getlocation()
        {
            return db.location;
        }

        // GET: api/locations/5
        [ResponseType(typeof(location))]
        public IHttpActionResult Getlocation(long id)
        {
            location location = db.location.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlocation(long id, location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest();
            }

            db.Entry(location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!locationExists(id))
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

        // POST: api/locations
        [ResponseType(typeof(location))]
        public IHttpActionResult Postlocation(location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.location.Add(location);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = location.Id }, location);
        }

        // DELETE: api/locations/5
        [ResponseType(typeof(location))]
        public IHttpActionResult Deletelocation(long id)
        {
            location location = db.location.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            db.location.Remove(location);
            db.SaveChanges();

            return Ok(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool locationExists(long id)
        {
            return db.location.Count(e => e.Id == id) > 0;
        }
    }
}