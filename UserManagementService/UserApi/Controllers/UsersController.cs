using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application.Interfaces;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult> AddUserAsync(User user)
        {
            if (user == null)
            {
                _logger.LogWarning("AddUserAsync called with null user.");
                return BadRequest("User cannot be null");
            }
            try
            {
                await _userService.AddUserAsync(user);
                return Ok("User created");
            }
            catch (Exception)
            {
                _logger.LogError("Failed to add user due to unexpected error.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while adding the user.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserAsync(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                _logger.LogWarning("GetAsync called with empty UserId.");
                return BadRequest("UserId cannot be empty.");
            }
            try
            {
                var user = await _userService.GetUserAsync(userId);
                return Ok(user);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to get user due to unexpected error.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while retrieving the user.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync(Guid userId, User user)
        {
            if(user == null)
            {
                _logger.LogWarning("UpdateUserAsync called with null user.");
                return BadRequest("User cannot be null.");
            }
            if(userId == Guid.Empty)
            {
                _logger.LogWarning("UpdateUserAsync called with empty userId.");
                return BadRequest("UserId cannot be empty.");
            }
            try
            {
                await _userService.UpdateUserAsync(userId, user);
                return Ok("User updated successfully.");
            }
            catch (Exception)
            {
                _logger.LogError("Failed to update user due to unexpected error.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while updating the user.");
            }
        }
    }
}
