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


        [HttpDelete("{registrationId}")]
        public IActionResult SoftDeleteRegistration(int registrationId)
        {
            try
            {
                _crudService.SoftDelete<Registration>("Registrations", registrationId, "isDeleted");
                return Ok("Registration deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
