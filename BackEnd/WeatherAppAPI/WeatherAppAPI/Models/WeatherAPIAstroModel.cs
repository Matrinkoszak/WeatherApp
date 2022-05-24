using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAppAPI.Models
{
    public class WeatherAPIAstroModel
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string moonrise { get; set; }
        public string moonset { get; set; }
        public string moon_phase { get; set; }
        public string moon_illumination { get; set; }
    }
}