using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplication _application;

        public UsersController(IApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _application.GetAllUsers().ToList();

                if (users.Count == 0)
                {
                    return NotFound("No users were found");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _application.GetUserById(id);

                if(user == null)
                {
                    return NotFound($"User with Id: {id} not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] UserDto userRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("User object doesn't provide all necessary information");
                }

                User user = new User()
                {
                    FullName = userRequest.FullName,
                    Mobile = userRequest.Mobile,
                    Email = userRequest.Email
                };

                if (user == null)
                {
                    return BadRequest("User object is null.");
                }

                

                _application.CreateUser(user);

                return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
