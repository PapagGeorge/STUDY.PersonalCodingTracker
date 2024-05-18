using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMoviesByTitle(string title);
        Task<Movie> GetMovieByIdAsync(string movieId);
        Task AddNewMovieAsync(Movie movie);

    }
}
