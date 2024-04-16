using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IApplication _application;

        public MembersController(IApplication application)
        {
            _application = application;
        }

        [HttpGet("get-members")]
        public IActionResult ShowAllMembers()
        {
            try
            {
                var allMembers = _application.ShowAllMembers();
                return Ok(allMembers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-member")]
        public IActionResult AddNewMember([FromBody]Member member)
        {
            try
            {
                _application.CreateNewMember(member);
                return Ok("Member added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        public IActionResult DeleteMember([FromBody] int memberId)
        {
            try
            {
                _application.DeleteMember(memberId);
                return Ok($"Member with Id: {memberId} deleted sucessfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
