import { ForecastHour } from "./ForecastHourModel";
import { Location } from "./LocationModel";

export interface ForecastDay {
    Id: number;
    avg_humidity: number;
    avg_temp: number;
    avg_visibility: number;
    condition: string;
    date: Date;
    forecasthour: ForecastHour[];
    location: Location;
    location_id: number;
    max_temp: number;
    max_wind: number;
    min_temp: number;
    moon_illumination: number;
    moonphase: string;
    moonrise: Date;
    moonset: Date;
    sunrise: Date;
    sunset: Date;
}