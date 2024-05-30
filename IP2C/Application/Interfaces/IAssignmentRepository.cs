using Domain.Models;

namespace Application.Interfaces
{
    public interface IAssignmentRepository
    {
        Task<Country?> GetCountryByIpAsync(string ipAddress);
        Task <int> InsertCountryAsync(Country country);
        Task<Country?> GetCountryByCodes(WebServiceResponse webServiceResponse);
        Task InsertIpAddress(int countryId, string ipAddress);
        
    }
}
