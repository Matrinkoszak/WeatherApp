using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAppAPI.Models
{
    public class WeatherAPIResponseModel
    {
        public WeatherAPILocationModel location { get; set; }
        public WeatherAPIForecastModel forecast { get; set; }
    }
}