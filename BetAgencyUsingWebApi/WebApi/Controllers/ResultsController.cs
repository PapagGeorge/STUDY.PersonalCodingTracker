using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Interfaces;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {

        private readonly IApplication _applicataion;

        public ResultsController(IApplication applicataion)
        {
            _applicataion = applicataion;
        }

        [HttpPost("add-result")]
        public IActionResult ApplyResult([FromBody] ApplyResultRequest resultRequest)
        {
            try
            {
                if(resultRequest == null)
                {
                    return BadRequest("Result object is null");
                }

                if (resultRequest.MatchId == null || resultRequest.HomeTeamScore == null || resultRequest.AwayTeamScore == null)
                {
                    return BadRequest("Result object does not contain all necessary information");
                }

                _applicataion.ApplyResult(resultRequest.MatchId, resultRequest.HomeTeamScore, resultRequest.AwayTeamScore);

                return Ok(resultRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
