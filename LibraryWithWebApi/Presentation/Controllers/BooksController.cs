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

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] Book book)
        {
            try
            {
                _application.AddNewBook(book);
                return Ok("Book added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("delete-book")]
        public IActionResult DeleteBook([FromBody] int bookId)
        {
            try
            {
                _application.DeleteBook(bookId);
                return Ok($"Book with Id: {bookId} successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-books")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _application.ShowAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
