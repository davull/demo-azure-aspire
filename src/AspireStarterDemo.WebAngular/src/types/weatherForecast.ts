export interface WeatherForecast {
  Date: string;
  TemperatureC: number;
  TemperatureF: number;
  Summary: string;
}

export type WeatherForecasts = WeatherForecast[];
