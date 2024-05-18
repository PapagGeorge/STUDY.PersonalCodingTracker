using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application;
using Application.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet("{ImdbId}")]
        public async Task<IActionResult> GetMovieByImdbId(string ImdbId)
        {
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(ImdbId);

                if (movie == null)
                {
                    return NotFound();
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
