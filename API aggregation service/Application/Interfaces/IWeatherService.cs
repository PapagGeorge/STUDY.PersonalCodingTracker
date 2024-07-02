using Domain.Models.WeatherBitApi;

namespace Application.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherApiResponseAsync(string countryCode, string cityName);
    }
}
