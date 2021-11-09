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
    public class BillController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BillController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of bills
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BillGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBills()
        {
            var listOfBills = await _unitOfWork.Bill.GetAllAsync(includeProperties: "Medicine");

            var billDTO = new List<BillGetDTO>();

            foreach (var bill in listOfBills)
            {
                billDTO.Add(_mapper.Map<BillGetDTO>(bill));
            }

            return Ok(billDTO);
        }
        
        /// <summary>
        /// Get individual bills
        /// </summary>
        /// <param name="billId"> The Id of the bill</param>
        /// <returns></returns>
        [HttpGet("{billId:int}", Name = "GetBill")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BillGetDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBill(int billId)
        {
            var bill = await _unitOfWork.Bill.GetFirstOrDefaultAsync(p => p.Id == billId, includeProperties: "Medicine");

            if (bill == null)
            {
                return NotFound();
            }

            var billDTO = _mapper.Map<BillGetDTO>(bill);

            return Ok(billDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BillCreateDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBill([FromBody] BillCreateDTO billCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // if (await _unitOfWork.Bill.GetFirstOrDefaultAsync(b => b.PatientId == billCreateDTO.PatientId) != null)
            // {
            //     ModelState.AddModelError("", "Bill Exists!");
            //     return StatusCode(404, ModelState);
            // }
            
            var bill = _mapper.Map<Bill>(billCreateDTO);

            if (!await _unitOfWork.Bill.AddAsync(bill))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record this bill");
                return StatusCode(500, ModelState);
            }
            
            await _unitOfWork.Save();

            //return Ok();

            //This returns the object that was created
            return CreatedAtRoute("GetBill", new { /*version = HttpContext.GetRequestedApiVersion().ToString(),*/ billId = bill.Id }, bill);
        }
        
        [HttpPatch("{billId:int}", Name = "UpdateBill")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBill(int billId, [FromBody] BillUpdateDTO billUpdateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (billUpdateDTO == null || billId != billUpdateDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var bill = _mapper.Map<Bill>(billUpdateDTO);

            if (!_unitOfWork.Bill.UpdateBill(bill))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record BILL");
                return StatusCode(404, ModelState);
            }

            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{billId:int}", Name = "DeleteBill")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBill(int billId)
        {
            var bill = await _unitOfWork.Bill.GetFirstOrDefaultAsync(h => h.Id == billId, includeProperties: "Address,Bill");

            if (bill == null)
            {
                return NotFound();
            }

            if (!_unitOfWork.Bill.RemoveAsync(bill))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record BILL");
                return StatusCode(500, ModelState);
            }

            await _unitOfWork.Save();

            return NoContent();
        }
    }
}