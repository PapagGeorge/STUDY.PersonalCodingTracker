using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IApplication _application;

        public RentController(IApplication application)
        {
            _application = application;
        }

        [HttpPost("rent-book")]
        public IActionResult RentBook([FromBody]RentBookRequest rentBookRequest)
        {
            try
            {
                _application.RentBook(rentBookRequest);
                return Ok($"Book with Id: {rentBookRequest.BookId} was rented successfully " +
                    $"to member with Id: {rentBookRequest.MemberId}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("return-book")]
        public IActionResult ReturnBook([FromBody]ReturnBookRequest returnBookRequest)
        {
            try
            {
                _application.ReturnBook(returnBookRequest);
                return Ok($"Book with Id: {returnBookRequest.BookId} was returned successfully " +
                    $"from member with Id: {returnBookRequest.MemberId}");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
