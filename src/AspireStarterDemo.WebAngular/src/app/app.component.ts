import { CommonModule } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Component, Injectable } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { WeatherForecasts } from "../types/weatherForecast";

@Injectable()
@Component({
  selector: "app-root",
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: "./app.component.html",
  styleUrl: "./app.component.css",
})
export class AppComponent {
  title = "weather";
  forecasts: WeatherForecasts = [];

  constructor(private http: HttpClient) {
    http.get<WeatherForecasts>("api/weatherforecast").subscribe({
      next: (result) => (this.forecasts = result),
      error: console.error,
    });
  }
}
