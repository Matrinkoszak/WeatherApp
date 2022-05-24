using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using WeatherAppAPI.Models;
using System.Text.Json;

namespace WeatherAppAPI.Services
{
    public class WeatherAPIService : IDisposable
    {
        private HttpClient client { get; set; }
        private const string URL = "http://api.weatherapi.com/v1/current.json";
        private string keyParameter = "?key=c1675cf7add745c4bf9210718222305";

        public WeatherAPIService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public forecastday GetForecastDayData(decimal latitude, decimal longitude, DateTime date)
        {
            try
            {
                string urlParameters = keyParameter + "&q=" + latitude.ToString() + "," + longitude.ToString() + "&dt=" + date.ToString("yyyy-MM-dd");
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var dataObjects = response.Content.ReadAsStringAsync().Result;// ReadAsAsync<IEnumerable<forecastday>>().Result;
                    return  Jso dataObjects
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }

        }

        public void Dispose()
        {
            client.Dispose();
        }


    }
}