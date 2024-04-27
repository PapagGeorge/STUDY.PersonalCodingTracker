using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IApplication _application;

        public TicketsController(IApplication application)
        {
            _application = application;
        }

        [HttpPost("add-ticket")]
        public IActionResult AddTicket([FromBody] CreateTicketWithBetsRequest ticketBetsRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    throw new Exception("The request for adding a new ticket does not contail all required information");
                }
                var userId = ticketBetsRequest.UserId;
                var betsData = ticketBetsRequest.BetsData.Select(bet => (bet.MatchId, bet.BettingMarket, bet.Stake)).ToList();
                _application.CreateTicketWithBets(userId, betsData);

                return Ok("Ticket created Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the ticket: {ex.Message}");
            }
        }
    }
}
