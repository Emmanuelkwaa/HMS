using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HMS.Models;
using HMS.Models.DTOs.Create;
using HMS.Models.DTOs.Get;
using HMS.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Areas.Reception.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of addresss
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AddressGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAddresss()
        {
            var listOfAddresss = await _unitOfWork.Address.GetAllAsync();

            var addressDto = new List<AddressGetDTO>();

            foreach (var address in listOfAddresss)
            {
                addressDto.Add(_mapper.Map<AddressGetDTO>(address));
            }

            return Ok(addressDto);
        }
        
        /// <summary>
        /// Get individual addresss
        /// </summary>
        /// <param name="addressId"> The Id of the address</param>
        /// <returns></returns>
        [HttpGet("{addressId:int}", Name = "GetAddress")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AddressGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddress(int addressId)
        {
            var address = await _unitOfWork.Address.GetFirstOrDefaultAsync(p => p.Id == addressId);

            if (address == null)
            {
                return NotFound();
            }

            var addressDto = _mapper.Map<AddressGetDTO>(address);

            return Ok(addressDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddressCreateDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateDTO addressCreateDto)
        {
            if (ModelState.IsValid)
            {
                if (await _unitOfWork.Address.GetFirstOrDefaultAsync(a => 
                    a.Street ==  addressCreateDto.Street && 
                    a.ApartmentNumber ==  addressCreateDto.ApartmentNumber &&
                    a.City == addressCreateDto.City &&
                    a.State == addressCreateDto.State) != null)
                {
                    ModelState.AddModelError("", "Address Exists!");
                    return StatusCode(404, ModelState);
                }
            }
            
            var address = _mapper.Map<Address>(addressCreateDto);

            if (!await _unitOfWork.Address.AddAsync(address))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record this address");
                return StatusCode(500, ModelState);
            }
            
            await _unitOfWork.Save();

            //return Ok();

            //This returns the object that was created
            return CreatedAtRoute(nameof(GetAddress), new { /*version = HttpContext.GetRequestedApiVersion().ToString(),*/ addressId = address.Id }, address);
        }
        
        [HttpPatch("{addressId:int}", Name = "UpdateAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAddress(int addressId, [FromBody] AddressUpdateDTO addressUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (addressUpdateDto == null || addressId != addressUpdateDto.Id)
            {
                return BadRequest(ModelState);
            }

            var address = _mapper.Map<Address>(addressUpdateDto);

            if (!_unitOfWork.Address.UpdateAddress(address))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record address");
                return StatusCode(404, ModelState);
            }

            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{addressId:int}", Name = "DeleteAddress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            var address = await _unitOfWork.Address.GetFirstOrDefaultAsync(h => h.Id == addressId);

            if (address == null)
            {
                return NotFound();
            }

            if (!_unitOfWork.Address.RemoveAsync(address))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record address");
                return StatusCode(500, ModelState);
            }

            await _unitOfWork.Save();

            return NoContent();
        }
    }
}