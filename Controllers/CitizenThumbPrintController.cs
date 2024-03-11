using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.CitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.CitizenThumbPrintDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.TehsilDTO;
using BISPAPIORA.Repositories.CitizenThumbPrintServicesRepo;
using BISPAPIORA.Repositories.DistrictServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    //[UserAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenThumbPrintController : ControllerBase
    {
        private readonly ICitizenThumbPrintService citizenThumbPrintService;
        public CitizenThumbPrintController(ICitizenThumbPrintService citizenThumbPrintService)
        {
            this.citizenThumbPrintService = citizenThumbPrintService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<CitizenThumbPrintResponseDTO>>> Post(AddCitizenThumbPrintDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenThumbPrintService.AddCitizenThumbPrint(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenThumbPrintResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<CitizenThumbPrintResponseDTO>>> Put(UpdateCitizenThumbPrintDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenThumbPrintService.UpdateCitizenThumbPrint(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenThumbPrintResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<CitizenThumbPrintResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = citizenThumbPrintService.DeleteCitizenThumbPrint(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenThumbPrintResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<CitizenThumbPrintResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = citizenThumbPrintService.GetCitizenThumbPrint(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenThumbPrintResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<CitizenThumbPrintResponseDTO>>>> Get()
        {
            var response = citizenThumbPrintService.GetCitizenThumbPrintsList();
            return Ok(await response);
        }
        [HttpGet("GetByCitizenId")]
        public async Task<ActionResult<ResponseModel<List<CitizenThumbPrintResponseDTO>>>> GetByCitizenId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = citizenThumbPrintService.GetCitizenThumbPrintByCitizenId(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenThumbPrintResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
    }
}