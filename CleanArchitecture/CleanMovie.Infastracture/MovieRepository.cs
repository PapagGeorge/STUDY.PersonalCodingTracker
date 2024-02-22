using Clean.MovieDomain;
using CleanMovie.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMovie.Infrastructure
{
    public class MovieRepository : IMovieRepository
    {
        public static List<Movie> movies = new List<Movie>()
        {
            new Movie{Id = 1, Name = "Passion of Christ", Cost = 2},
            new Movie{Id = 2, Name = "Home Alone", Cost= 3}
        };
        public List<Movie> GetAllMovies()
        {
            return movies;
        }
    }
}
