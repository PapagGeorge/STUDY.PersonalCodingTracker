using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMoviesByTitle(string title);
        Movie GetMovieById(int id);

    }
}
