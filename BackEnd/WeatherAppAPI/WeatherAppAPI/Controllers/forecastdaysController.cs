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
        [Route("api/forcastdays/{token}/{locationName}/{day}")]
        [ResponseType(typeof(forecastday))]
        public IHttpActionResult Getforecastday(string token,string locationName, string day)
        {
            using(var serv = new SecurityService(db))
            {
                if (serv.IsTokenValid(token))
                {
                    DateTime dayTime = DateTime.Parse(day);
                    if (dayTime != null)
                    {
                        forecastday forecastday = db.forecastday.Where(x => (x.location.name.Equals(locationName)) && (x.date.Equals(dayTime))).FirstOrDefault();
                        if (forecastday == null)
                        {
                            return NotFound();
                        }
                        //self-referencing is properly handled, but I decided returning all forecastdays for the location is too much data for this request, therefore instead the return of that field is null
                        forecastday.location.forecastday = null;
                        return Ok(forecastday);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
        }
        // GET: api/forecastdays/5
        [Route("api/forcastdays/{token}/{locationName}/{startDate}/{endDate}")]
        [ResponseType(typeof(List<forecastday>))]
        public IHttpActionResult Getforecastday(string token, string locationName, string startDate, string endDate)
        {
            using (var serv = new SecurityService(db))
            {
                if (serv.IsTokenValid(token))
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
                else
                {
                    return Unauthorized();
                }
            }
        }

        // PUT: api/forecastdays/5
        [Route("api/forcastdays/updateDays/{token}/{location}/{startDate}/{endDate}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putforecastdays(string token,string location, string startDate, string endDate)
        {
            using (var serv = new SecurityService(db))
            {
                if (serv.IsTokenValid(token) && serv.IsTokenAdmin(token))
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
                            using(WeatherAPIService apiServ = new WeatherAPIService())
                            {
                                forecastday downloadedDay = apiServ.GetForecastDayData(forecastDay.location.latitude, forecastDay.location.longitude, start.Date);
                                forecastDay.avg_humidity = downloadedDay.avg_humidity;
                                forecastDay.avg_temp = downloadedDay.avg_temp;
                                forecastDay.avg_visibility = downloadedDay.avg_visibility;
                                forecastDay.condition = downloadedDay.condition;
                                forecastDay.max_temp = downloadedDay.max_temp;
                                forecastDay.max_wind = downloadedDay.max_wind;
                                forecastDay.min_temp = downloadedDay.min_temp;
                                forecastDay.moonphase = downloadedDay.moonphase;
                                forecastDay.moonrise = downloadedDay.moonrise;
                                forecastDay.moonset = downloadedDay.moonset;
                                forecastDay.moon_illumination = downloadedDay.moon_illumination;
                                forecastDay.sunrise = downloadedDay.sunrise;
                                forecastDay.sunset = downloadedDay.sunset;
                                foreach(var downloadedHour in downloadedDay.forecasthour)
                                {
                                    forecasthour hour = forecastDay.forecasthour.Where(x => x.time.CompareTo(downloadedHour.time) == 0).FirstOrDefault();
                                    if(hour == null)
                                    {
                                        hour = new forecasthour();
                                        hour.forecastday = forecastDay;
                                        hour.time = downloadedHour.time;
                                        db.forecasthour.Add(hour);
                                    }
                                    hour.cloud_coverage = downloadedHour.cloud_coverage;
                                    hour.condition = downloadedHour.condition;
                                    hour.humidity = downloadedHour.humidity;
                                    hour.pressure = downloadedHour.pressure;
                                    hour.rain = downloadedHour.rain;
                                    hour.snow = downloadedHour.snow;
                                    hour.temp = downloadedHour.temp;
                                    hour.visibility = downloadedHour.visibility;
                                    hour.wind_direction = downloadedHour.wind_direction;
                                    hour.wind_speed = downloadedHour.wind_speed;
                                }
                            }
                            db.SaveChanges();
                            start = start.AddDays(1);
                        }
                        return Ok();
                    }
                    return BadRequest();
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

        private bool forecastdayExists(long id)
        {
            return db.forecastday.Count(e => e.Id == id) > 0;
        }

        private bool dateEqualityHelper(DateTime main, DateTime comparing)
        {
            return main.Date.CompareTo(comparing) == 0;
        }
    }
}