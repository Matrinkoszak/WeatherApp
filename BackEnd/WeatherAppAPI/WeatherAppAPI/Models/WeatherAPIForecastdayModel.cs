using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAppAPI.Models
{
    public class WeatherAPIForecastdayModel
    {
        public string date { get; set; }
        public WeatherAPIDayModel day { get; set; }
        public WeatherAPIAstroModel astro { get; set; }
        public List<WeatherAPIHourModel> hour { get; set; }
    }
}