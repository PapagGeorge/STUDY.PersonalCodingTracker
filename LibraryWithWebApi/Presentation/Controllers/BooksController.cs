using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IApplication _application;

        public BooksController(IApplication application)
        {
            _application = application;
        }


        [HttpGet("get-books")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _application.ShowAllBooks();
                return Ok(books);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }   
        }

        [HttpPost("add-book")]
        public IActionResult AddNewBook([FromBody]Book book)
        {
            try
            {
                _application.AddNewBook(book);
                return Ok("Book added sucessfully");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("detete-book")]
        public IActionResult DeleteBook([FromBody]int bookId)
        {
            try
            {
                _application.DeleteBook(bookId);
                return Ok($"Book with Id: {bookId} deleted successfully.");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
