using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTO_s;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {

        private readonly ICrudService _crudService;

        public AttendanceController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        
        [HttpGet]
        public IActionResult GetAllAtendanceRecords()
        {
            try
            {
                var attendanceRecords = _crudService.GetAll<Attendance>("Attendance");
                return Ok(attendanceRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{attendanceId}")]
        public IActionResult GetAttendanceRecordById(int attendanceId)
        {
            try
            {
                var attendanceRecord = _crudService.GetById<Attendance>(attendanceId, "Events", "EventId");
                return Ok(attendanceRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertAttendanceRecord(AttendanceDTO attendanceRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Attendance attendance = new Attendance()
                {
                    EventId = attendanceRequest.EventId,
                    UserId = attendanceRequest.UserId,
                    AttendanceDateTime = DateTime.Now,
                    isDeleted = false
                };

                _crudService.Insert<Attendance>(attendance);
                return CreatedAtAction(nameof(GetAttendanceRecordById), new {id = attendance.AttendanceId }, attendance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{attendanceId}")]
        public IActionResult SoftDeleteAttendanceRecord(int attendanceId)
        {
            try
            {
                _crudService.SoftDelete<Attendance>("Attendance", attendanceId);
                return Ok("Attendance record deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
