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
    public class locationsController : ApiController
    {
        private WeatherApp_dbEntities db = new WeatherApp_dbEntities();

        [Route("api/locations/{token}")]
        [ResponseType(typeof(IQueryable<location>))]
        public IHttpActionResult Getlocation(string token)
        {
            using (var serv = new SecurityService(db))
            {
                if (serv.IsTokenValid(token))
                {
                    return Ok(db.location);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        [Route("api/locations/{token}/{name}")]
        [ResponseType(typeof(location))]
        public IHttpActionResult Getlocation(string token, string name)
        {
            using (var serv = new SecurityService(db))
            {
                if (serv.IsTokenValid(token))
                {
                    location location = db.location.Where(x => x.name.Equals(name)).FirstOrDefault();
                    if (location == null)
                    {
                        return NotFound();
                    }

                    return Ok(location);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        [Route("api/locations/update/{token}/{name}")]
        [ResponseType(typeof(location))]
        public IHttpActionResult Putlocation(string token, string name)
        {
            using (var serv = new SecurityService(db))
            {
                if (serv.IsTokenValid(token) && serv.IsTokenAdmin(token))
                {
                    using(var apiServ = new WeatherAPIService())
                    {
                        var tempLocation = apiServ.GetLocationData(name);
                        if(tempLocation != null)
                        {
                            var location = db.location.Where(x => x.name.Equals(name)).FirstOrDefault();
                            if(location == null)
                            {
                                location = new location();
                                location.name = name;
                                db.location.Add(location);
                            }
                            location.latitude = tempLocation.latitude;
                            location.longitude = tempLocation.longitude;
                            db.SaveChanges();
                            return Ok(location);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
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

        private bool locationExists(long id)
        {
            return db.location.Count(e => e.Id == id) > 0;
        }
    }
}