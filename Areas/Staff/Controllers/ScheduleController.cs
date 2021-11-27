using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HMS.Models;
using HMS.Models.DTOs.Create;
using HMS.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Areas.Staff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of Schedules
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ScheduleGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSchedules()
        {
            var listOfSchedules = await _unitOfWork.Schedule.GetAllAsync(includeProperties:"Doctor");

            var scheduleDto = new List<ScheduleGetDTO>();

            foreach (var schedule in listOfSchedules)
            {
                scheduleDto.Add(_mapper.Map<ScheduleGetDTO>(schedule));
            }

            return Ok(scheduleDto);
        }
        
        /// <summary>
        /// Get individual schedules
        /// </summary>
        /// <param name="scheduleId"> The Id of the schedule</param>
        /// <returns></returns>
        [HttpGet("{scheduleId:int}", Name = "GetSchedule")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ScheduleGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSchedule(int scheduleId)
        {
            var schedule = await _unitOfWork.Schedule.GetFirstOrDefaultAsync(p => p.Id == scheduleId, includeProperties:"Doctor");

            if (schedule == null)
            {
                return NotFound();
            }

            var scheduleDto = _mapper.Map<ScheduleGetDTO>(schedule);
            
            return Ok(scheduleDto);
        }

        [HttpPost(template:"{doctorId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Schedule))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSchedule(int doctorId, [FromBody] ScheduleCreateDTO scheduleToCreate)
        {
            if(!ModelState.IsValid) return StatusCode(404, ModelState);

            var doctor = await _unitOfWork.Doctor.GetFirstOrDefaultAsync(d => d.Id == doctorId);

            var schedule = _mapper.Map<Schedule>(scheduleToCreate);
            schedule.ScheduleDay = schedule.StartTime.ToString("dddd");
            schedule.Doctor = doctor;
            schedule.ShiftLength = schedule.StartTime > schedule.EndTime
                ? schedule.StartTime - schedule.EndTime
                : schedule.EndTime - schedule.StartTime;

            if (!await _unitOfWork.Schedule.AddAsync(schedule))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record this schedule");
                return StatusCode(500, ModelState);
            }
            
            await _unitOfWork.Save();
        
            //This returns the object that was created
            return CreatedAtRoute(nameof(GetSchedule), new { /*version = HttpContext.GetRequestedApiVersion().ToString(),*/ scheduleId = schedule.Id }, schedule);
        }
        
        [HttpPatch("{scheduleId:int},{doctorId:int}", Name = "UpdateSchedule")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSchedule(int scheduleId, int doctorId, [FromBody] ScheduleCreateDTO scheduleToUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (scheduleToUpdate == null || scheduleId != scheduleToUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            
            var doctor = await _unitOfWork.Doctor.GetFirstOrDefaultAsync(d => d.Id == doctorId);

            var schedule = _mapper.Map<Schedule>(scheduleToUpdate);
            schedule.ScheduleDay = schedule.StartTime.ToString("dddd");
            schedule.Doctor = doctor;
        
            if (!_unitOfWork.Schedule.UpdateSchedule(schedule))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record schedule");
                return StatusCode(404, ModelState);
            }
        
            await _unitOfWork.Save();
        
            return NoContent();
        }
        
        [HttpDelete("{scheduleId:int}", Name = "DeleteSchedule")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSchedule(int scheduleId)
        {
            var schedule = await _unitOfWork.Schedule.GetFirstOrDefaultAsync(h => h.Id == scheduleId);
        
            if (schedule == null)
            {
                return NotFound();
            }
        
            if (!_unitOfWork.Schedule.RemoveAsync(schedule))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record schedule");
                return StatusCode(500, ModelState);
            }
        
            await _unitOfWork.Save();
        
            return NoContent();
        }
    }
}