using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.CitizenComplianceDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.TehsilDTO;
using BISPAPIORA.Repositories.CitizenComplianceServicesRepo;
using BISPAPIORA.Repositories.DistrictServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenComplianceController : ControllerBase
    {
        private readonly ICitizenComplianceService citizenComplianceService;
        public CitizenComplianceController(ICitizenComplianceService citizenComplianceService)
        {
            this.citizenComplianceService = citizenComplianceService;
        }
        //Add Citizen Compliance Method
        [HttpPost]
        public async Task<ActionResult<ResponseModel<CitizenComplianceResponseDTO>>> Post(AddCitizenComplianceDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenComplianceService.AddCitizenCompliance(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Update Citizen Compliance Method
        [HttpPut]
        public async Task<ActionResult<ResponseModel<CitizenComplianceResponseDTO>>> Put(UpdateCitizenComplianceDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenComplianceService.UpdateCitizenCompliance(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Delete Citizen Compliance Method
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<CitizenComplianceResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = citizenComplianceService.DeleteCitizenCompliance(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get Citizen Compliance By Id Method
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<CitizenComplianceResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = citizenComplianceService.GetCitizenCompliance(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get All Citizen Compliance Method
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<CitizenComplianceResponseDTO>>>> Get()
        {
            var response = citizenComplianceService.GetCitizenCompliancesList();
            return Ok(await response);
        }
        //Get Citizen By Id Method
        [HttpGet("GetByCitizenId")]
        public async Task<ActionResult<ResponseModel<List<CitizenComplianceResponseDTO>>>> GetByCitizenId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = citizenComplianceService.GetCitizenComplianceByCitizenId(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }     
    }
}