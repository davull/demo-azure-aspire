using System.Text.Json;
using AspireStarterDemo.ApiService;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Redis client
// https://learn.microsoft.com/en-us/dotnet/aspire/caching/stackexchange-redis-integration#client-integration
builder.AddRedisClient(connectionName: "cache");

builder.Services.AddHttpClient<DataServiceApiClient>(client =>
{
    client.BaseAddress = new("https+http://dataservice");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/weatherforecast", async (IConnectionMultiplexer redis, DataServiceApiClient client) =>
    {
        const int count = 10;
        var forecast = new List<WeatherForecast>();
        for (var i = 0; i < count; i++)
        {
            forecast.Add(await client.GetWeatherAsync());
        }

        var db = redis.GetDatabase();

        var key = $"{Random.Shared.Next(0, 10)}";
        var value = JsonSerializer.Serialize(forecast);
        await db.StringGetSetAsync(key, value);

        return value;
    })
    .WithName("GetWeatherForecast");

app.MapDefaultEndpoints();

app.Run();