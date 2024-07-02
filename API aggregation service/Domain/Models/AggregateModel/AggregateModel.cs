using Domain.Models.NewsApiModels;
using Domain.Models.WeatherBitApi;

namespace Domain.Models.AggregateModel
{
    public class AggregateModel
    {
        public NewsApiResponse NewsApiResponse { get; set; }
        public WeatherData WeatherData { get; set; }
    }
}
