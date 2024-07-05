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
        public async Task<AggregateModel> GetAggregateData(string newsKeyword, bool weatherTempAscending, string sortByTemp = "temperature",
            string startDate = null,
            string endDate = null,
            string sortByAstronomyPictures = "date",
            bool ascendingAstronomyPictures = true,
            string sortByNews = "author",
            bool ascendingNews = true)
        {
            // Start all tasks concurrently
            var newsTask = _newsService.GetNewsApiResponseAsync(newsKeyword, sortByNews, ascendingNews);
            var weatherTask = _weatherService.GetWeatherApiResponseAsync(weatherTempAscending, sortByTemp);
            var astronomyPictureTask = _astronomyPictureService.GetAstronomyPictures(startDate, endDate, sortByAstronomyPictures, ascendingAstronomyPictures);

            // Wait for all tasks to complete
            await Task.WhenAll(newsTask, weatherTask, astronomyPictureTask);

            // Retrieve the results
            var newsApiResponse = await newsTask;
            var weatherApiResponse = await weatherTask;
            var astronomyPictureResponse = await astronomyPictureTask;

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
