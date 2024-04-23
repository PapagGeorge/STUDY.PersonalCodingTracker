using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private readonly IApplication _application;

        public BetsController(IApplication application)
        {
            _application = application;
        }

        [HttpGet("{id}")]
        public IActionResult GetBetById(int id)
        {
            try
            {
                var bet = _application.GetBetById(id);

                if(bet == null)
                {
                    return NotFound("Bet object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("The bet object does not contain all necessary information");
                }

                return Ok(bet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-bet")]
        IActionResult CreateBet([FromBody] AddBetRequest newBetRequest)
        {
            try
            {
                var bet = _application.CreateBet(newBetRequest.UserId, newBetRequest.MatchId, newBetRequest.BettingMarket, newBetRequest.Stake);

                if (bet == null)
                {
                    return BadRequest("Bet object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("The bet object does not contain all necessary information");
                }

                return CreatedAtAction(nameof(GetBetById), new { id = bet.BetId }, bet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
