using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> GetMovieByIdAsync(string movieId);
    }
}
