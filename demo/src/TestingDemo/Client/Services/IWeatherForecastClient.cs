using TestingDemo.Shared;

namespace TestingDemo.Client.Services;

public interface IWeatherForecastClient
{
    Task<WeatherForecast[]?> GetWeatherForecastsAsync();
}
