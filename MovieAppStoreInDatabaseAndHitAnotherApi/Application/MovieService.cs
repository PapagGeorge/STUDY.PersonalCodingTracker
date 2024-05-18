using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Application
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMemoryCache _memoryCache;

        public MovieService(IMovieRepository movieRepository, IMemoryCache memoryCache)
        {
            _movieRepository = movieRepository;
            _memoryCache = memoryCache;
        }
        public async Task<Movie> GetMovieByIdAsync(string movieId)
        {
            var result = _memoryCache.Get<Movie>("movie");

            if (result == null)
            {
                result = await _movieRepository.GetMovieByIdAsync(movieId);

                if (result != null)
                {
                    _memoryCache.Set("movie", movieId, TimeSpan.FromMinutes(5));
                }

                return result;
            }
            return result;
        }
    }
}
