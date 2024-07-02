using Domain.Models.AggregateModel;

namespace Application.Interfaces
{
    public interface IAggregateService
    {
        Task<AggregateModel> GetAggregateData(string newsKeyword, string countryCode, string cityName);
    }
}
