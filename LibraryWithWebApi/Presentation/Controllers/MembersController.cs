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
        public IActionResult GetMembers()
        {
            try
            {
                var members = _application.ShowAllMembers();
                return Ok(members);
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
                return Ok("Member created successfully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("delete-member")]
        public IActionResult DeleteMember([FromBody] int memberId)
        {
            try
            {
                _application.DeleteMember(memberId);
                return Ok($"Member with Id: {memberId} was successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
