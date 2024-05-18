using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IOMDbAccess _oMDbAccess;
        public MoviesController(IMovieService movieService, IOMDbAccess oMDbAccess)
        {
            _movieService = movieService;
            _oMDbAccess = oMDbAccess;
        }

        //}

        [HttpGet("OMBd/{ImdbId}")]
        public async Task<IActionResult> GetMovieByImdbIdFromOMBd(string ImdbId)
        {
            try
            {
                Movie movie = new Movie();
                movie = await _oMDbAccess.GetMovieByImdbIdAsync(ImdbId);

                if(movie == null)
                {
                    return BadRequest("Movie was not found");
                }    

                return Ok(movie);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
