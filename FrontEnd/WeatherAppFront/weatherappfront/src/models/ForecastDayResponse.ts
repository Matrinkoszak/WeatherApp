import { ForecastHourResponse } from "./ForecastHourResponse";
import { Location } from "./LocationModel";

export interface ForecastDayResponse {
    Id: number;
    avg_humidity: number;
    avg_temp: number;
    avg_visibility: number;
    condition: string;
    date: string;
    forecasthour: ForecastHourResponse[];
    location: Location;
    location_id: number;
    max_temp: number;
    max_wind: number;
    min_temp: number;
    moon_illumination: number;
    moonphase: string;
    moonrise: string;
    moonset: string;
    sunrise: string;
    sunset: string;
}