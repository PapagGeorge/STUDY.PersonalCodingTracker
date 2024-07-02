using Domain.Models.NewsApiModels;
using Domain.Models.WeatherBitApi;
using Domain.Models.AstronomyPictureModel;

namespace Domain.Models.AggregateModel
{
    public class AggregateModel
    {
        public NewsApiResponse NewsApiResponse { get; set; }
        public WeatherData WeatherData { get; set; }
        public IEnumerable<AstronomyPicture> AstronomyPicture { get; set; }
    }
}
