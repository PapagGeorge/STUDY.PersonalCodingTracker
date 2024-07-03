using Application.Interfaces;
using Domain.Models.AstronomyPictureModel;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Services
{
    public class AstronomyPictureService : IAstronomyPictureService
    {
        private readonly IAstronomyPictureClient _astronomyPictureClient;
        private readonly IDistributedCache _distributedCache;

        public AstronomyPictureService(IAstronomyPictureClient astronomyPictureClient, IDistributedCache distributedCache)
        {
            _astronomyPictureClient = astronomyPictureClient;
            _distributedCache = distributedCache;
        }
        public async Task<IEnumerable<AstronomyPicture>> GetAstronomyPictures(string startDate = null, string endDate = null, string sortBy = "date", bool ascending = true)
        {
            var content = await _distributedCache.GetRecordAsync <IEnumerable<AstronomyPicture>>(GetCacheKey(startDate, endDate, sortBy, ascending), GetJsonSerializerOptions());

            if(content == null)
            {
                content = await _astronomyPictureClient.GetAstronomyPicturesAsync(startDate, endDate);
                await _distributedCache.SetRecordAsync(GetCacheKey(startDate, endDate, sortBy, ascending), content);
            }
            
            if(sortBy == "date")
            {
                content = ascending ? content.OrderBy(item => item.Date) : content.OrderByDescending(item => item.Date);
            }

            return content;
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        private static string GetCacheKey(string startDate, string endDate, string sortBy, bool ascending)
        {
            var keyParts = new List<string>();

            if (!string.IsNullOrEmpty(startDate))
            {
                keyParts.Add($"start_{startDate}");
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                keyParts.Add($"end_{endDate}");
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                keyParts.Add($"sort_{sortBy}");
            }

            keyParts.Add($"asc_{ascending}");

            if (keyParts.Count == 0)
            {
                return "default_astronomy_cache_key";
            }

            var cacheKey = string.Join("_", keyParts);

            return cacheKey;
        }

    }
}
