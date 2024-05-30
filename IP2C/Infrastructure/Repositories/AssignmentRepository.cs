using Application.Interfaces;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> InsertCountryAsync(Country country)
        {
            

            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();

            return country.Id;
        }

        public async Task<Country?> GetCountryByCodes(WebServiceResponse webServiceResponse)
        {
            if(webServiceResponse == null)
            {
                throw new ArgumentNullException(nameof(webServiceResponse));
            }

            var query = from country in _context.Countries
                        where country.TwoLetterCode == webServiceResponse.TwoLetterCode
                        && country.ThreeLetterCode == webServiceResponse.ThreeLetterCode
                        select country;

            return await query.FirstOrDefaultAsync();
        }

        public async Task InsertIpAddress(int countryId, string ipAddress)
        {
            if(countryId == 0)
            {
                throw new ArgumentNullException(nameof(countryId));
            }

            if(string.IsNullOrWhiteSpace(ipAddress))
            {
                throw new ArgumentNullException(nameof(ipAddress));
            }

            IpAddress addressToAdd = IpAddress.Create(countryId, ipAddress, DateTime.UtcNow, DateTime.UtcNow);
            await _context.IpAddresses.AddAsync(addressToAdd);
            await _context.SaveChangesAsync();
        }
    }
}
