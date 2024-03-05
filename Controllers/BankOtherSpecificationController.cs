using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.BankOtherSpecificationServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    //[UserAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankOtherSpecificationController : ControllerBase
    {
        private readonly IBankOtherSpecificationService BankOtherSpecificationService;
        public BankOtherSpecificationController(IBankOtherSpecificationService BankOtherSpecificationService)
        {
            this.BankOtherSpecificationService = BankOtherSpecificationService;
        }
        [HttpPost("PostEnrolled")]
        public async Task<ActionResult<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>>> PostEnrolled(AddEnrolledBankOtherSpecificationDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = BankOtherSpecificationService.AddEnrolledBankOtherSpecification(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPost("PostRegistered")]
        public async Task<ActionResult<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>>> PostRegistered(AddRegisteredBankOtherSpecificationDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = BankOtherSpecificationService.AddRegisteredBankOtherSpecification(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut("PutEnrolled")]
        public async Task<ActionResult<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>>> PutEnrolled(UpdateEnrolledCitizenBankInfoDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = BankOtherSpecificationService.UpdateEnrolleedBankOtherSpecification(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut("PutRegistered")]
        public async Task<ActionResult<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>>> PutRegistered(UpdateRegisteredBankOtherSpecificationDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = BankOtherSpecificationService.UpdateRegisteredBankOtherSpecification(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
    }
}
