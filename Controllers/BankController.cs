using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.BankDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.BankServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [UserAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService bankService;
        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
        }
        // Add New Bank Method
        [HttpPost]
        public async Task<ActionResult<ResponseModel<BankResponseDTO>>> Post(AddBankDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = bankService.AddBank(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Update Bank Method
        [HttpPut]
        public async Task<ActionResult<ResponseModel<BankResponseDTO>>> Put(UpdateBankDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = bankService.UpdateBank(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Delete Bank Method
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<BankResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = bankService.DeleteBank(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //To get Bank By id Method
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<BankResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = bankService.GetBank(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //To get all Banks Method
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<BankResponseDTO>>>> Get()
        {
            var response = bankService.GetBanksList();
            return Ok(await response);
        }
    }
}
