using Domain.Models.NewsApiModels;
using Domain.Models.WeatherBitApi;

namespace Application.Interfaces
{
    public interface IWeatherApiClient
    {
        Task<WeatherData> GetWeatherAsync(string CountryCode, string CityName);
    }
}
