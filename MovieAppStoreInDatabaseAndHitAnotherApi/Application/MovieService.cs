using Application.Interfaces;
using Domain.Entities;

namespace Application
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<Movie> GetMovieByIdAsync(string movieId)
        {
            return await _movieRepository.GetMovieByIdAsync(movieId);
        }
    }
}
