using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAppAPI.Models
{
    public class WeatherAPIForecastModel
    {
        public List<WeatherAPIForecastdayModel> forecastday { get; set; }
    }
}