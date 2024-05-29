using Domain.Models;

namespace Application.Interfaces
{
    public interface IAssignmentRepository
    {
        Task<Country?> GetCountryByIpAsync(string ipAddress);
        
    }
}
