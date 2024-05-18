using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class OMDbToDatabase : IOMDbToDatabase
    {
        private readonly IOMDbAccess _oMDbAccess;
        private readonly IMovieRepository _movieRepository;

        public OMDbToDatabase(IOMDbAccess oMDbAccess, IMovieRepository movieRepository)
        {
            _oMDbAccess = oMDbAccess;
            _movieRepository = movieRepository; 
        }
        public async Task AddMovieFromIOMDbToDatabase(string imdbId)
        {
            var movie = await _oMDbAccess.GetMovieByImdbIdAsync(imdbId);
            await _movieRepository.AddNewMovieAsync(movie);
        }
    }
}
