using Application.Interfaces;
using Domain.Models.AggregateModel;

namespace Application.Services
{ 

    public class AggregateService : IAggregateService
    {
        private readonly IWeatherService _weatherService;
        private readonly INewsService _newsService;

        public AggregateService(IWeatherService weatherService, INewsService newsService)
        {
            _newsService = newsService;
            _weatherService = weatherService;
        }
        public async Task<AggregateModel> GetAggregateData(string newsKeyword, string countryCode, string cityName)
        {
            var newsApiResponse = await _newsService.GetNewsApiResponseAsync(newsKeyword);
            var weatherApiResponse = await _weatherService.GetWeatherApiResponseAsync(countryCode, cityName);

            var aggregateModel = new AggregateModel
            {
                NewsApiResponse = newsApiResponse,
                WeatherData = weatherApiResponse
            };

            return aggregateModel;
        }
    }
}
