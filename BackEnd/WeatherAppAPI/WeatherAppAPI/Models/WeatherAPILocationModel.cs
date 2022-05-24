using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAppAPI.Models
{
    public class WeatherAPILocationModel
    {
        public string name { get; set; }
        public decimal lat { get; set; }
        public decimal lon { get; set; }


    }
}