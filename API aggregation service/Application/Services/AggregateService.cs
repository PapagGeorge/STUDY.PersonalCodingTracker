using Application.Interfaces;
using Domain.Models.AggregateModel;

namespace Application.Services
{ 

    public class AggregateService : IAggregateService
    {
        private readonly IWeatherService _weatherService;
        private readonly INewsService _newsService;
        private readonly IAstronomyPictureService _astronomyPictureService;

        public AggregateService(IWeatherService weatherService, INewsService newsService, IAstronomyPictureService astronomyPictureService)
        {
            _newsService = newsService;
            _weatherService = weatherService;
            _astronomyPictureService = astronomyPictureService;
        }
        public async Task<AggregateModel> GetAggregateData(string newsKeyword, string countryCode, string cityName, string startDate = null,
            string endDate = null, string sortBy = "date", bool ascending = true)
        {
            var newsApiResponse = await _newsService.GetNewsApiResponseAsync(newsKeyword);
            var weatherApiResponse = await _weatherService.GetWeatherApiResponseAsync(countryCode, cityName);
            var astronomyPictureResponse = await _astronomyPictureService.GetAstronomyPictures(startDate, endDate, sortBy, ascending);

            var aggregateModel = new AggregateModel
            {
                NewsApiResponse = newsApiResponse,
                WeatherData = weatherApiResponse,
                AstronomyPicture = astronomyPictureResponse
            };

            return aggregateModel;
        }
    }
}
