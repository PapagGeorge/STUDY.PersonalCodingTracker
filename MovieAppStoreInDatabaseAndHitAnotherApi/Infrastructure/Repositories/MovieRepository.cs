using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public Movie GetMovieById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetMoviesByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
