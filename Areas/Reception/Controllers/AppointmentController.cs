using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HMS.Models;
using HMS.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Areas.Reception.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of Appointments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AppointmentGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAppointments()
        {
            var listOfAppointments = await _unitOfWork.Appointment.GetAllAsync(includeProperties:"Doctor,Patient");

            var appointmentDto = new List<AppointmentGetDTO>();

            foreach (var appointment in listOfAppointments)
            {
                appointmentDto.Add(_mapper.Map<AppointmentGetDTO>(appointment));
            }

            return Ok(appointmentDto);
        }
        
        /// <summary>
        /// Get individual appointments
        /// </summary>
        /// <param name="appointmentId"> The Id of the appointment</param>
        /// <returns></returns>
        [HttpGet("{appointmentId:int}", Name = "GetAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AppointmentGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointment(int appointmentId)
        {
            var appointment = await _unitOfWork.Appointment.GetFirstOrDefaultAsync(p => p.Id == appointmentId, includeProperties:"Patient,Doctor");

            if (appointment == null)
            {
                return NotFound();
            }

            var appointmentDto = _mapper.Map<AppointmentGetDTO>(appointment);

            return Ok(appointmentDto);
        }

        [HttpPost(template:"{patientId:int},{doctorId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppointmentCreateDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAppointment(int doctorId, int patientId, [FromBody] AppointmentCreateDTO appointmentCreateDto)
        {
            if(!ModelState.IsValid) return StatusCode(400, ModelState);

            var patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == patientId, includeProperties:"Doctor");

            var appointment = _mapper.Map<Appointment>(appointmentCreateDto);
            
            appointment.Patient = patient;
            appointment.Doctor = patient.Doctor;
            appointment.DaysUntilAppointment = appointment.DateOfAppointment - DateTime.Now;
        
            if (!await _unitOfWork.Appointment.AddAsync(appointment))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record this appointment");
                return StatusCode(500, ModelState);
            }
            
            await _unitOfWork.Save();
        
            //This returns the object that was created
            return CreatedAtRoute(nameof(GetAppointment), new { /*version = HttpContext.GetRequestedApiVersion().ToString(),*/ appointmentId = appointment.Id }, appointment);
        }
        
        [HttpPatch("{appointmentId:int},{patientId:int}", Name = "UpdateAppointment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAppointment(int appointmentId, int patientId, [FromBody] AppointmentCreateDTO appointmentCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (appointmentCreateDto == null || appointmentId != appointmentCreateDto.Id)
            {
                return BadRequest(ModelState);
            }

            var patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == patientId, includeProperties:"Doctor");
            var appointment = _mapper.Map<Appointment>(appointmentCreateDto);
            appointment.Patient = patient;
            appointment.Doctor = patient.Doctor;
            appointment.DaysUntilAppointment = appointment.DateOfAppointment - DateTime.Now;
        
            if (!_unitOfWork.Appointment.UpdateAppointment(appointment))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record appointment");
                return StatusCode(404, ModelState);
            }
        
            await _unitOfWork.Save();
        
            return NoContent();
        }
        
        [HttpDelete("{appointmentId:int}", Name = "DeleteAppointment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            var appointment = await _unitOfWork.Appointment.GetFirstOrDefaultAsync(a => a.Id == appointmentId);
        
            if (appointment == null)
            {
                return NotFound();
            }
        
            if (!_unitOfWork.Appointment.RemoveAsync(appointment))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record appointment");
                return StatusCode(500, ModelState);
            }
        
            await _unitOfWork.Save();
        
            return NoContent();
        }
    }
}