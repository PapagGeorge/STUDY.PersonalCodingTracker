using Domain.Models.AggregateModel;

namespace Application.Interfaces
{
    public interface IAggregateService
    {
        Task<AggregateModel> GetAggregateData(string newsKeyword, string countryCode, string cityName, string startDate = null,
            string endDate = null, string sortBy = "date", bool ascending = true);
    }
}
