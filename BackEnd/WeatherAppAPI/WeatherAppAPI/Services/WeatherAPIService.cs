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
        private const string URL = "http://api.weatherapi.com/v1/history.json";
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
                string urlParameters = keyParameter + "&q=" + latitude.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "," + longitude.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "&dt=" + date.ToString("yyyy-MM-dd");
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var dataObjects = response.Content.ReadAsAsync<WeatherAPIResponseModel>().Result;
                    return TranslateAPIModelToDBModel(dataObjects, date);
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

        private forecastday TranslateAPIModelToDBModel(WeatherAPIResponseModel model, DateTime date)
        {
            try
            {
                forecastday result = new forecastday();
                WeatherAPIForecastdayModel dayModel = model.forecast.forecastday.Where(x => DateTime.Parse(x.date).CompareTo(date) == 0).FirstOrDefault();
                if(dayModel != null)
                {
                    result.date = DateTime.Parse(dayModel.date);
                    result.max_wind = dayModel.day.maxwind_kph;
                    result.avg_humidity = dayModel.day.avghumidity;
                    result.avg_visibility = dayModel.day.avgvis_km;
                    result.avg_temp = dayModel.day.avgtemp_c;
                    result.condition = dayModel.day.condition.text;
                    result.max_temp = dayModel.day.maxtemp_c;
                    result.min_temp = dayModel.day.mintemp_c;
                    result.moonphase = dayModel.astro.moon_phase;
                    result.moonrise = DateTime.Parse(dayModel.astro.moonrise);
                    result.moonset = DateTime.Parse(dayModel.astro.moonset);
                    result.moon_illumination = Int32.Parse(dayModel.astro.moon_illumination);
                    result.sunrise = DateTime.Parse(dayModel.astro.sunrise);
                    result.sunset = DateTime.Parse(dayModel.astro.sunset);

                    result.location = new location();
                    result.location.name = model.location.name;
                    result.location.latitude = model.location.lat;
                    result.location.longitude = model.location.lon;

                    foreach(var hour in dayModel.hour)
                    {
                        forecasthour forecasthour = new forecasthour();
                        forecasthour.cloud_coverage = hour.cloud;
                        forecasthour.condition = hour.condition.text;
                        forecasthour.humidity = hour.humidity;
                        forecasthour.pressure = hour.pressure_mb;
                        forecasthour.rain = hour.will_it_rain == 1;
                        forecasthour.snow = hour.will_it_snow == 1;
                        forecasthour.temp = hour.temp_c;
                        forecasthour.time = DateTime.Parse(hour.time);
                        forecasthour.visibility = hour.vis_km;
                        forecasthour.wind_direction = hour.wind_dir;
                        forecasthour.wind_speed = hour.wind_kph;
                        result.forecasthour.Add(forecasthour);
                    }

                    return result;

                }
                else
                {
                    throw new Exception("Forecast wasn't properly downloaded from 3rd party API");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}