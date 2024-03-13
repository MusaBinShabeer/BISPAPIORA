using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenBankInfoServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenBankInfoController : ControllerBase
    {
        private readonly ICitizenBankInfoService citizenBankInfoService;
        public CitizenBankInfoController(ICitizenBankInfoService citizenBankInfoService)
        {
            this.citizenBankInfoService = citizenBankInfoService;
        }
        #region Citizen Family Bank Info
        //Add Citizen Family Bank Info
        [HttpPost("PostCitizenFamilyBankInfo")]
        public async Task<ActionResult<ResponseModel<EnrolledCitizenBankInfoResponseDTO>>> PostCitizenFamilyBankInfo(AddEnrolledCitizenBankInfoDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenBankInfoService.AddEnrolledCitizenBankInfo(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Update Citizen Family Bank Info
        [HttpPut("PutCitizenFamilyBankInfo")]
        public async Task<ActionResult<ResponseModel<EnrolledCitizenBankInfoResponseDTO>>> PutCitizenFamilyBankInfo(UpdateEnrolledCitizenBankInfoDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenBankInfoService.UpdateEnrolledCitizenBankInfo(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Delete Citizen Family Bank Info
        [HttpDelete("DeleteCitizenFamilyBankInfo")]
        public async Task<ActionResult<ResponseModel<EnrolledCitizenBankInfoResponseDTO>>> DeleteCitizenFamilyBankInfo(string id)
        {
            if (ModelState.IsValid)
            {
                var response = citizenBankInfoService.DeleteEnrolledCitizenBankInfo(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get Citizen Family Bank Info By Id
        [HttpGet("GetByCitizenFamilyBankInfoId")]
        public async Task<ActionResult<ResponseModel<EnrolledCitizenBankInfoResponseDTO>>> GetByCitizenFamilyBankInfoId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = citizenBankInfoService.GetEnrolledCitizenBankInfo(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get All Citizen Family Bank Info
        [HttpGet("GetCitizenFamilyBankInfo")]
        public async Task<ActionResult<ResponseModel<List<EnrolledCitizenBankInfoResponseDTO>>>> GetCitizenFamilyBankInfo()
        {
            var response = citizenBankInfoService.GetEnrolledCitizenBankInfosList();
            return Ok(await response);
        }
        #endregion
        #region Citizen Bank Info
        //Add Citizen Bank Info
        [HttpPost("PostCitizenBankInfo")]
        public async Task<ActionResult<ResponseModel<EnrolledCitizenBankInfoResponseDTO>>> PostCitizenBankInfo(AddEnrolledCitizenBankInfoDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenBankInfoService.AddEnrolledCitizenBankInfo(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Update Citizen Bank Info
        [HttpPut("PutCitizenBankInfo")]
        public async Task<ActionResult<ResponseModel<RegisteredCitizenBankInfoResponseDTO>>> PutCitizenBankInfo(UpdateRegisteredCitizenBankInfoDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenBankInfoService.UpdateRegisteredCitizenBankInfo(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Delete Citizen Bank Info
        [HttpDelete("DeleteCitizenBankInfo")]
        public async Task<ActionResult<ResponseModel<RegisteredCitizenBankInfoResponseDTO>>> DeleteCitizenBankInfo(string id)
        {
            if (ModelState.IsValid)
            {
                var response = citizenBankInfoService.DeleteRegisteredCitizenBankInfo(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get Citizen Bank Info
        [HttpGet("GetByCitizenBankInfoId")]
        public async Task<ActionResult<ResponseModel<RegisteredCitizenBankInfoResponseDTO>>> GetByCitizenBankInfoId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = citizenBankInfoService.GetEnrolledCitizenBankInfo(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get All Citizen Bank Info
        [HttpGet("GetCitizenBankInfo")]
        public async Task<ActionResult<ResponseModel<List<RegisteredCitizenBankInfoResponseDTO>>>> GetCitizenBankInfo()
        {
            var response = citizenBankInfoService.GetEnrolledCitizenBankInfosList();
            return Ok(await response);
        }
        #endregion
    }
}