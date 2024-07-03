using Domain.Models.AggregateModel;

namespace Application.Interfaces
{
    public interface IAggregateService
    {
        Task<AggregateModel> GetAggregateData(string newsKeyword, bool weatherTempAscending, string sortByTemp = "temperature",
            string startDate = null,
            string endDate = null,
            string sortByAstronomyPictures = "date",
            bool ascendingAstronomyPictures = true,
            string sortByNews = "author",
            bool ascendingNews = true);
    }
}
