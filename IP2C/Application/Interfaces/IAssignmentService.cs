using Domain.Models;

namespace Application.Interfaces
{
    public interface IAssignmentService
    {
        Task<WebServiceResponse> GetIpInformationAsync(string ipAddress);
    }
}
