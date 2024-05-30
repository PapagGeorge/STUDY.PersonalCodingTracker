using Domain.Models;

namespace Application.Extensions
{
    public static class CountryToResponse
    {
        public static WebServiceResponse TransformCountryToResponse(this Country country)
        {
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            return WebServiceResponse.Create(country.Name, country.TwoLetterCode, country.ThreeLetterCode);

            
        }
    }
}
