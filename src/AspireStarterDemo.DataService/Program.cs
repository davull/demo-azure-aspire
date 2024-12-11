using AspireStarterDemo.DataService;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

builder.AddNpgsqlDataSource(connectionName: "postgresdb");

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/weatherforecast", async (NpgsqlDataSource dataSource) =>
    {
        var summary = await Repository.GetSummary(dataSource);

        var forecast = new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(Random.Shared.Next(0, 10))),
            Random.Shared.Next(-20, 55),
            summary);
        return Results.Json(forecast);
    })
    .WithName("GetWeatherForecast");

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary);