using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System.Collections;
using WebApplication.DTO_s;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICrudService _crudService;

        public UsersController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                IEnumerable users = _crudService.GetAll<User>("dbo.Users");
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var searchedUser = _crudService.GetById<Event>(userId, "Users", "UserId");
                return Ok(searchedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertUser(UserDTO userRequest)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User newUser = new User()
                {
                    FullName = userRequest.FullName,
                    Email = userRequest.Email,
                    MobilePhone = userRequest.MobilePhone,
                    isDeleted = false
                };

                _crudService.Insert<User>(newUser);

                return CreatedAtAction(nameof(GetUserById), new { userId = newUser.UserId }, newUser);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult SoftDeleteUser(int userId)
        {
            try
            {
                _crudService.SoftDelete<Event>("Users", userId);
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
