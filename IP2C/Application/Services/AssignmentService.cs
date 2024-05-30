using Application.Extensions;
using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Application.Converters;
using Application.Extension;

namespace Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        public const string baseUrl = "https://ip2c.org/";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;

        public AssignmentService(IUnitOfWork unitOfWork, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
        }

        public async Task<WebServiceResponse> GetIpInformationAsync(string ipAddress)
        {
            var cacheResponse = await _distributedCache.GetRecordAsync<WebServiceResponse>(ipAddress, GetJsonSerializerOptions());
            if(cacheResponse != null)
            {
                return cacheResponse;
            }

            var country = await _unitOfWork.AssignmentRepository.GetCountryByIpAsync(ipAddress);
            if(country != null)
            {
                var countryDbResponse = country.TransformCountryToResponse();
                await _distributedCache.SetRecordAsync(ipAddress, countryDbResponse);
                return countryDbResponse;
            }

            WebServiceResponse webServiceResponse = await GetHttpClientResponse(ipAddress);
            await InsertCountryAndIp(webServiceResponse, ipAddress);
            await _distributedCache.SetRecordAsync(ipAddress, webServiceResponse);
            return webServiceResponse;
            
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                Converters = { new WebServiceResponseConverter() }
            };
        }

        public static async Task<WebServiceResponse> GetHttpClientResponse(string ipAddress)
        {
            var apiUrl = $"{baseUrl}{ipAddress}";

            var httpClientResponse = await apiUrl.GetHttpClientAsync();

            var webServiceResponse = WebServiceResponse.Create(name: httpClientResponse[3],
                twoLetterCode: httpClientResponse[1],
                threeLetterCode: httpClientResponse[2]);

            return webServiceResponse;
        }

        private async Task InsertCountryAndIp(WebServiceResponse webServiceResponse, string ipAddress)
        {
            var country = await _unitOfWork.AssignmentRepository.GetCountryByCodes(webServiceResponse);

            if (country == null)
            {
                var countryId = await _unitOfWork.AssignmentRepository.InsertCountryAsync(
                    Country.Create(webServiceResponse.Name,
                    webServiceResponse.TwoLetterCode,
                    webServiceResponse.ThreeLetterCode,
                    DateTime.UtcNow));

                await _unitOfWork.AssignmentRepository.InsertIpAddress(countryId, ipAddress);
            }
            else
            {
                await _unitOfWork.AssignmentRepository.InsertIpAddress(country.Id, ipAddress);
            }
        }
    }
}
