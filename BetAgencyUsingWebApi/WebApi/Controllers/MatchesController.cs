using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using WebApi.DTO;

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
                    return NotFound($"Match with Id: {id} was not found");
                }

                return Ok(match);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-match")]
        public IActionResult AddNewMatch([FromBody] MatchDto matchRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Match object doesn't provide all necessary information");
                }

                Match match = new Match()
                {
                    MatchDateTime = matchRequest.MatchDateTime,
                    HomeTeam = matchRequest.HomeTeam,
                    AwayTeam = matchRequest.AwayTeam,
                    Status = matchRequest.Status,
                    HomeTeamWinsOdds = matchRequest.HomeTeamWinsOdds,
                    AwayTeamWinsOdds = matchRequest.AwayTeamWinsOdds,
                    DrawOdds = matchRequest.DrawOdds,
                    OverOdds = matchRequest.OverOdds,
                    UnderOdds = matchRequest.UnderOdds
                };

                if(match == null)
                {
                    throw new Exception("Match object is null");
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
