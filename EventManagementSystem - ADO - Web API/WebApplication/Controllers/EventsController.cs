using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using WebApplication.DTO_s;
using Microsoft.Extensions.Logging;

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

        [HttpPost]
        public IActionResult AddNewEvent([FromBody] EventDTO newEvent)
        {
            try
            {

                
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not all information for the new event were provided properly");
                }

                Event eventToAdd = new Event()
                {
                    Description = newEvent.Description,
                    StartDate = newEvent.StartDate,
                    EndDate = newEvent.EndDate,
                    Location = newEvent.Location,
                    OrganizerId = newEvent.OrganizerId,
                    Capacity = newEvent.Capacity
                };

                _crudService.AddNewEvent(eventToAdd);

                return CreatedAtAction(nameof(GetEventById), new { eventId = eventToAdd.EventId }, eventToAdd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }
}
