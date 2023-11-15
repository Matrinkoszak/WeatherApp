export interface ForecastHourResponse {
    Id: number;
    cloud_coverage: number;
    condition: string;
    humidity: number;
    pressure: number;
    rain: boolean;
    snow: boolean;
    temp: number;
    time: string;
    visibility: number;
    wind_direction: string;
    wind_speed: number;
}