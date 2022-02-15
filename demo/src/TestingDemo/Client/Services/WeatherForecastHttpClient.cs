using System.Net.Http.Json;
using TestingDemo.Shared;

namespace TestingDemo.Client.Services;

public class WeatherForecastHttpClient : IWeatherForecastClient
{
    public WeatherForecastHttpClient(HttpClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public HttpClient Client { get; }

    public Task<WeatherForecast[]?> GetWeatherForecastsAsync()
    {
        return Client.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
    }
}
