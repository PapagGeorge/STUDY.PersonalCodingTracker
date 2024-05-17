using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet("{ImdbId}")]
        public IActionResult GetMovieByImdbId(string ImdbId)
        {

        }


    }
}
