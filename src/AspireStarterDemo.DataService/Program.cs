var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

// TODO: Add postgres client

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/weatherforecast", () =>
    {
        string[] summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        var forecast = new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(Random.Shared.Next(0, 10))),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]);
        return Results.Json(forecast);
    })
    .WithName("GetWeatherForecast");

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary);