using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAppAPI.Models
{
    public class WeatherAPIDayModel
    {
        public decimal maxtemp_c { get; set; }
        public decimal mintemp_c { get; set; }
        public decimal avgtemp_c { get; set; }
        public decimal maxwind_kph { get; set; }
        public decimal avgvis_km { get; set; }
        public decimal avghumidity { get; set; }
        public WeatherAPIConditionModel condition { get; set; }
    }
}