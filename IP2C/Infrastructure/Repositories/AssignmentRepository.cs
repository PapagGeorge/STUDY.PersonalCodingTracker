using Application.Interfaces;
using Domain.Models;
using Infrastructure.DatabaseContext;
namespace Infrastructure.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Country?> GetCountryByIpAsync(string ip)
        {
            var query = from country in _context.Countries
                        join ipAddress in _context.IpAddresses
                        on country.Id equals ipAddress.CountryId
                        where ipAddress.Ip == ip
                        orderby ipAddress.UpdatedAt
                        select country;

            return query.FirstOrDefault();
                        
        }
    }
}
