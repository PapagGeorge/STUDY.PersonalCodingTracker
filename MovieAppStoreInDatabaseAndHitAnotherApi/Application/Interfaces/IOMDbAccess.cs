using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOMDbAccess
    {
        Task<Movie> GetMovieByImdbIdAsync(string movieId);
    }
}
