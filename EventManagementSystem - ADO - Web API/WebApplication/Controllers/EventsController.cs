using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using WebApplication.DTO_s;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ICrudService _crudService;

        public EventsController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            try
            {
                var events = _crudService.GetAll<Event>("Events");
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{eventId}")]
        public IActionResult GetEventById(int eventId)
        {
            try
            {
                var searchedEvent = _crudService.GetById<Event>(eventId, "Events", "EventId");
                return Ok(searchedEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{eventId}")]
        public IActionResult SoftDeleteEvent(int eventId)
        {
            try
            {
                _crudService.SoftDelete<Event>("Events", eventId, "isDeleted");
                return Ok("Event deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
