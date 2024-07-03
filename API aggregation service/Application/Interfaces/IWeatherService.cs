using Domain.Models.WeatherBitApi;

namespace Application.Interfaces
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherData>> GetWeatherApiResponseAsync(bool ascending, string sortBy = "temperature");
    }
}
