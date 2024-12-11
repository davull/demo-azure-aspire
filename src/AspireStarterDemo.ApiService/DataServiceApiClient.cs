namespace AspireStarterDemo.ApiService;

internal class DataServiceApiClient(HttpClient httpClient)
{
    public async Task<WeatherForecast> GetWeatherAsync(CancellationToken cancellationToken = default)
    {
        var result = await httpClient.GetAsync("/weatherforecast", cancellationToken);
        var weather = await result.Content.ReadFromJsonAsync<WeatherForecast>(cancellationToken: cancellationToken);
        return weather ?? throw new InvalidOperationException("No weather forecast was returned.");
    }
}