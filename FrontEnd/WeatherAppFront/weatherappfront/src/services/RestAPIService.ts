import axios from 'axios';
import { ForecastDay } from '../models/ForecastDayModel';
import { ForecastDayResponse } from '../models/ForecastDayResponse';
import { ForecastHour } from '../models/ForecastHourModel';
import { ForecastHourResponse } from '../models/ForecastHourResponse';

export class RestApiService {
    public getToken = (login: string, password: string) => {
        const url = "https://localhost:44372/api/authToken/" + login + "/" + password;
        return axios({
            method: 'get',
            url: url
        }).then((response) => {
            //console.log(response);
            return response.data.code;
        })
    }

    public getForecastDays = (token: string, location: string, startDate: Date, endDate: Date) => {
        const url = "https://localhost:44372/api/forcastdays/" + token + "/" + location + "/" + startDate.toDateString() + "/" + endDate.toDateString();
        //console.log(url);
        return axios({
            method: 'get',
            url: url
        }).then((response) => {
            //console.log(response);
            var result: ForecastDay[] = [];
            response.data.map((element: ForecastDayResponse) => {
                //console.log(element);
                var hours: ForecastHour[] = [];
                element.forecasthour.map((innerHour: ForecastHourResponse) => {
                    const hour: ForecastHour = {
                        Id: innerHour.Id,
                        cloud_coverage: innerHour.cloud_coverage,
                        condition: innerHour.condition,
                        humidity: innerHour.humidity,
                        pressure: innerHour.pressure,
                        rain: innerHour.rain,
                        snow: innerHour.snow,
                        temp: innerHour.temp,
                        time: new Date(innerHour.time),
                        visibility: innerHour.visibility,
                        wind_direction: innerHour.wind_direction,
                        wind_speed: innerHour.wind_speed
                    }
                    hours.push(hour);
                })

                const day: ForecastDay = {
                    Id: element.Id,
                    avg_humidity: element.avg_humidity,
                    avg_temp: element.avg_temp,
                    avg_visibility: element.avg_visibility,
                    condition: element.condition,
                    date: new Date(element.date),
                    location_id: element.location_id,
                    max_temp: element.max_temp,
                    max_wind: element.max_wind,
                    min_temp: element.min_temp,
                    moonphase: element.moonphase,
                    moonrise: new Date(element.moonrise),
                    moonset: new Date(element.moonset),
                    moon_illumination: element.moon_illumination,
                    sunrise: new Date(element.sunrise),
                    sunset: new Date(element.sunset),
                    forecasthour: hours,
                    location: element.location
                };
                //console.log(day);
                result.push(day);
            })
            return result;
        })
    }
}