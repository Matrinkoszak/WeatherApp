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
    public class forecastdaysController : ApiController
    {
        private WeatherApp_dbEntities db = new WeatherApp_dbEntities();



        // GET: api/forecastdays/5
        [Route("api/forcastdays/{locationName}/{day}")]
        [ResponseType(typeof(forecastday))]
        public IHttpActionResult Getforecastday(string locationName, string day)
        {
            DateTime dayTime = DateTime.Parse(day);
            if(dayTime != null)
            {
                forecastday forecastday = db.forecastday.Where(x => (x.location.name.Equals(locationName)) && (x.date.Date.Equals(dayTime.Date))).FirstOrDefault();
                if (forecastday == null)
                {
                    return NotFound();
                }

                return Ok(forecastday);
            }
            else
            {
                return BadRequest();
            }
        }
        // GET: api/forecastdays/5
        [Route("api/forcastdays/{locationName}/{startDate}/{endDate}")]
        [ResponseType(typeof(List<forecastday>))]
        public IHttpActionResult Getforecastday(string locationName, string startDate, string endDate)
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);
            if (start != null && end != null)
            {
                List<forecastday> forecastdays = db.forecastday.Where(x => (x.location.name.Equals(locationName)) && (x.date.Date.CompareTo(start) >= 0) && (x.date.Date.CompareTo(end) <= 0)).ToList();
                if (forecastdays == null || forecastdays.Count <= 0)
                {
                    return NotFound();
                }

                return Ok(forecastdays);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/forecastdays/5
        [Route("api/forcastdays/updateDays/{location}/{startDate}/{endDate}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putforecastdays(string location, string startDate, string endDate)
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);
            if (start != null && end != null)
            {
                while(start.Date.CompareTo(end.Date) <= 0)
                {
                    forecastday forecastDay = db.forecastday.Where(x => (x.location.name.Equals(location) && (x.date.Equals(start)))).FirstOrDefault();
                    if(forecastDay == null)
                    {
                        location loc = db.location.Where(x => x.name.Equals(location)).FirstOrDefault();
                        if(loc == null)
                        {
                            return BadRequest();
                        }
                        forecastDay = new forecastday() {location = loc, date = start };
                        db.forecastday.Add(forecastDay);
                    }
                    using(WeatherAPIService serv = new WeatherAPIService())
                    {

                    }
                    db.SaveChanges();
                }
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool forecastdayExists(long id)
        {
            return db.forecastday.Count(e => e.Id == id) > 0;
        }
    }
}