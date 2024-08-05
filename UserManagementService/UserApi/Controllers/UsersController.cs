using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Dtos;
using Application.Interfaces;
using AutoMapper;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, ILogger<UsersController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> AddUserAsync(UserCreateDto user)
        {
            if (user == null)
            {
                _logger.LogWarning("AddUserAsync called with null user.");
                return BadRequest("User cannot be null");
            }
            try
            {
                var newUser = _mapper.Map<User>(user);

                // Add the user to the database
                await _userService.AddUserAsync(newUser);

                // Map the newly created user to a DTO for the response
                var userReadDto = _mapper.Map<UserReadDto>(newUser);

                _logger.LogInformation($"Returning CreatedAtAction with userId: {newUser.UserId}");
                return Ok(userReadDto);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to add user due to unexpected error.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while adding the user.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserReadDto>> GetUserAsync(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                _logger.LogWarning("GetAsync called with empty UserId.");
                return BadRequest("UserId cannot be empty.");
            }
            try
            {
                var user = await _userService.GetUserAsync(userId);
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            catch (Exception)
            {
                _logger.LogError("Failed to get user due to unexpected error.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while retrieving the user.");
            }
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUserAsync(Guid userId, UserUpdateDto user)
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
                var existingUser = await _userService.GetUserAsync(userId);
                if (existingUser == null)
                {
                    _logger.LogWarning($"User with ID {userId} not found.");
                    return NotFound("User not found.");
                }

                var updatedUser = _mapper.Map<User>(user);
                await _userService.UpdateUserAsync(userId, updatedUser);

                // Map the newly created user to a DTO for the response
                var userReadDto = _mapper.Map<UserReadDto>(updatedUser);
                return Ok(userReadDto);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to update user due to unexpected error.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while updating the user.");
            }
        }
    }
}
