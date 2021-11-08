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
    public class DoctorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of doctors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DoctorGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllDoctors()
        {
            var listOfDoctors = await _unitOfWork.Doctor.GetAllAsync();

            var doctorDto = new List<DoctorGetDTO>();

            foreach (var doctor in listOfDoctors)
            {
                doctorDto.Add(_mapper.Map<DoctorGetDTO>(doctor));
            }

            return Ok(doctorDto);
        }
        
        /// <summary>
        /// Get individual doctors
        /// </summary>
        /// <param name="doctorId"> The Id of the doctor</param>
        /// <returns></returns>
        [HttpGet("{doctorId:int}", Name = "GetDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DoctorGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDoctor(int doctorId)
        {
            var doctor = await _unitOfWork.Doctor.GetFirstOrDefaultAsync(p => p.Id == doctorId, includeProperties: "Patients");

            if (doctor == null)
            {
                return NotFound();
            }

            var doctorDto = _mapper.Map<DoctorGetDTO>(doctor);

            return Ok(doctorDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DoctorCreateDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorCreateDTO doctorCreateDto)
        {
            if (!ModelState.IsValid) return StatusCode(500, ModelState);
            if (await _unitOfWork.Doctor.GetFirstOrDefaultAsync(p => 
                p.FirstName ==  doctorCreateDto.FirstName && 
                p.LastName ==  doctorCreateDto.LastName) != null)
            {
                ModelState.AddModelError("", "Doctor Exists!");
                return StatusCode(404, ModelState);
            }
                
            var doctor = _mapper.Map<Doctor>(doctorCreateDto);

            if (!await _unitOfWork.Doctor.AddAsync(doctor))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {doctor.FirstName} {doctor.LastName}");
                return StatusCode(500, ModelState);
            }

            //return Ok();

            //This returns the object that was created
            return CreatedAtRoute("GetDoctor", new { /*version = HttpContext.GetRequestedApiVersion().ToString(),*/ doctorId = doctor.Id }, doctor);

        }
        
        [HttpPatch("{doctorId:int}", Name = "UpdateDoctor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDoctor(int doctorId, [FromBody] DoctorUpdateDTO doctorUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (doctorUpdateDto == null || doctorId != doctorUpdateDto.Id)
            {
                return BadRequest(ModelState);
            }

            var doctor = _mapper.Map<Doctor>(doctorUpdateDto);

            if (!await _unitOfWork.Doctor.UpdateDoctor(doctor))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {doctor.FirstName} {doctor.LastName}");
                return StatusCode(404, ModelState);
            }

            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{doctorId:int}", Name = "DeleteDoctor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDoctor(int doctorId)
        {
            var doctor = await _unitOfWork.Doctor.GetFirstOrDefaultAsync(h => h.Id == doctorId, includeProperties: "Address,Bill");

            if (doctor == null)
            {
                return NotFound();
            }

            if (!_unitOfWork.Doctor.RemoveAsync(doctor))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {doctor.FirstName} {doctor.LastName}");
                return StatusCode(500, ModelState);
            }

            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
