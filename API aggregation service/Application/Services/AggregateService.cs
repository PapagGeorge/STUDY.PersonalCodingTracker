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
            string endDate = null, string sortByAstronomyPictures = "date", bool ascendingAstronomyPictures = true,
            string sortByNews = "date", bool ascendingNews = true)
        {
            var newsApiResponse = await _newsService.GetNewsApiResponseAsync(newsKeyword, sortByNews, ascendingNews);
            var weatherApiResponse = await _weatherService.GetWeatherApiResponseAsync(countryCode, cityName);
            var astronomyPictureResponse = await _astronomyPictureService.GetAstronomyPictures(startDate, endDate, sortByAstronomyPictures, ascendingAstronomyPictures);

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
