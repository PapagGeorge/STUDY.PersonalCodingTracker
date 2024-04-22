using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IApplication _application;

        public MatchesController(IApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public IActionResult GetAllMatchesByDateRange(DateTime startingDate, DateTime endingDate)
        {
            try
            {
                var matches = _application.GetAllMatchesByDateRange(startingDate, endingDate).ToList();

                if(matches.Count == 0)
                {
                    return NotFound("No matches found.");
                }

                return Ok(matches);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMatchById(int id)
        {
            try
            {
                var match = _application.GetMatchById(id);

                if(match == null)
                {
                    return NotFound($"Match with Id: {id} not found");
                }

                return Ok(match);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-match")]
        public IActionResult AddNewMatch([FromBody] Match match)
        {
            try
            {
                if(match == null)
                {
                    throw new Exception("Match object is null");
                }

                if(!ModelState.IsValid)
                {
                    throw new Exception("Match object doesn't provide all necessary information");
                }

                _application.CreateMatch(match);
                return CreatedAtAction(nameof(GetMatchById), new { id = match.MatchId }, match);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
