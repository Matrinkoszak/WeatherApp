using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAppAPI.Models
{
    public class WeatherAPIHourModel
    {
        public string time { get; set; }
        public decimal temp_c { get; set; }
        public int is_day { get; set; }
        public WeatherAPIConditionModel condition { get; set; }
        public decimal wind_kph { get; set; }
        public decimal wind_dir { get; set; }
        public decimal pressure_mb { get; set; }
        public int humidity { get; set; }
        public int cloud { get; set; }
        public decimal vis_km { get; set; }
        public int will_it_rain { get; set; }
        public int will_it_snow { get; set; }
    }
}