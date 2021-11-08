using AutoMapper;
using HMS.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.Models;
using HMS.Models.DTOs.Create;
using HMS.Models.DTOs.Get;
using Microsoft.AspNetCore.Http;

namespace HMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of patients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PatientGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPatients()
        {
            var listOfPatients = await _unitOfWork.Patient.GetAllAsync(includeProperties:"Address,Room,Doctor,Nurse");

            var patientDto = new List<PatientGetDTO>();

            foreach (var patient in listOfPatients)
            {
                patientDto.Add(_mapper.Map<PatientGetDTO>(patient));
            }

            return Ok(patientDto);
        }
        
        /// <summary>
        /// Get individual patients
        /// </summary>
        /// <param name="patientId"> The Id of the patient</param>
        /// <returns></returns>
        [HttpGet("{patientId:int}", Name = "GetPatient")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PatientGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPatient(int patientId)
        {
            var patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => p.Id == patientId);

            if (patient == null)
            {
                return NotFound();
            }

            var patientDto = _mapper.Map<PatientGetDTO>(patient);

            return Ok(patientDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientCreateDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePatient([FromBody] PatientCreateDTO patientCreateDto)
        {
            if (!ModelState.IsValid) return StatusCode(500, ModelState);

            // var allPatients = await _unitOfWork.Patient.GetAllAsync(includeProperties: "Room");
            //
            // foreach (var patients in allPatients)
            // {
            //     if (patients.Room.RoomNumber == patientCreateDto.Room.RoomNumber)
            //     {
            //         ModelState.AddModelError("", "Room is taken!");
            //         return StatusCode(404, ModelState);
            //     }
            // }
            
            if (await _unitOfWork.Patient.GetFirstOrDefaultAsync(p => 
                p.FirstName ==  patientCreateDto.FirstName && 
                p.LastName ==  patientCreateDto.LastName) != null)
            {
                ModelState.AddModelError("", "Patient Exists!");
                return StatusCode(404, ModelState);
            }

            var patient = _mapper.Map<Patient>(patientCreateDto);

            if (!await _unitOfWork.Patient.AddAsync(patient))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {patient.FirstName} {patient.LastName}");
                return StatusCode(500, ModelState);
            }

            await _unitOfWork.Save();

            //return Ok();

            //This returns the object that was created
            return CreatedAtRoute("GetPatient", new { /*version = HttpContext.GetRequestedApiVersion().ToString(),*/ patientId = patient.Id }, patient);

        }
        
        [HttpPatch("{patientId:int}", Name = "UpdatePatient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePatient(int patientId, [FromBody] PatientUpdateDTO patientUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (patientUpdateDto == null || patientId != patientUpdateDto.Id)
            {
                return BadRequest(ModelState);
            }

            var patient = _mapper.Map<Patient>(patientUpdateDto);

            if (!await _unitOfWork.Patient.UpdatePatient(patient))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {patient.FirstName} {patient.LastName}");
                return StatusCode(404, ModelState);
            }

            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{patientId:int}", Name = "DeletePatient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePatient(int patientId)
        {
            var patient = await _unitOfWork.Patient.GetFirstOrDefaultAsync(h => h.Id == patientId);

            if (patient == null)
            {
                return NotFound();
            }

            if (!_unitOfWork.Patient.RemoveAsync(patient))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {patient.FirstName} {patient.LastName}");
                return StatusCode(500, ModelState);
            }

            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
