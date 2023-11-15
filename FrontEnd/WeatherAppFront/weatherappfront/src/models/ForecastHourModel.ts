export interface ForecastHour {
    Id: number;
    cloud_coverage: number;
    condition: string;
    humidity: number;
    pressure: number;
    rain: boolean;
    snow: boolean;
    temp: number;
    time: Date;
    visibility: number;
    wind_direction: string;
    wind_speed: number;
}