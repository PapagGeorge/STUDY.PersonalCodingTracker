using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using WebApplication.DTO_s;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly ICrudService _crudService;

        public RegistrationsController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public IActionResult GetAllRegistrations()
        {
            try
            {
                var registrations = _crudService.GetAll<Registration>("Registrations");
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{registrationId}")]
        public IActionResult GetRegistrationById(int registrationId)
        {
            try
            {
                var registration = _crudService.GetById<Registration>(registrationId, "Registrations", "RegistrationId");
                return Ok(registration);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertRegistration(RegistrationDTO registrationRequest)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Registration newRegistration = new Registration()
                {
                    EventId = registrationRequest.EventId,
                    UserId = registrationRequest.UserId,
                    RegistrationDateTime = DateTime.Now,
                    Status = "Pending",
                    isDeleted = false
                };

                _crudService.Insert<Registration>(newRegistration);

                return CreatedAtAction(nameof(GetRegistrationById), new { registrationId = newRegistration.RegistrationId }, newRegistration);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{registrationId}")]
        public IActionResult SoftDeleteRegistration(int registrationId)
        {
            try
            {
                _crudService.SoftDelete<Registration>("Registrations", registrationId);
                return Ok("Registration deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
