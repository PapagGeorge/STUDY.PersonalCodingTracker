using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models;

namespace Assignment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet("{ipAddress}")]
        public async Task <ActionResult<WebServiceResponse>> GetIpInformation(string ipAddress)
        {
            var response = await _assignmentService.GetIpInformationAsync(ipAddress);
            return Ok(response);
        }
    }
}

