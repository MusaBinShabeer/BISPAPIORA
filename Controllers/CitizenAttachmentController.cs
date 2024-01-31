using BISPAPIORA.Models.DTOS.CitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.CitizenThumbPrintDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.TehsilDTO;
using BISPAPIORA.Repositories.CitizenAttachmentServicesRepo;
using BISPAPIORA.Repositories.CitizenThumbPrintServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenAttachmentController : ControllerBase
    {
        private readonly ICitizenAttachmentService citizenAttachmentService;
        public CitizenAttachmentController(ICitizenAttachmentService citizenAttachmentService)
        {
            this.citizenAttachmentService = citizenAttachmentService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<CitizenAttachmentResponseDTO>>> Post(AddCitizenAttachmentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenAttachmentService.AddCitizenAttachment(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenAttachmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<CitizenAttachmentResponseDTO>>> Put(UpdateCitizenAttachmentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = citizenAttachmentService.UpdateCitizenAttachment(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenAttachmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<CitizenAttachmentResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = citizenAttachmentService.DeleteCitizenAttachment(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenAttachmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<CitizenAttachmentResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = citizenAttachmentService.GetCitizenAttachment(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenAttachmentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<CitizenAttachmentResponseDTO>>>> Get()
        {
            var response = citizenAttachmentService.GetCitizenAttachmentsList();
            return Ok(await response);
        }
        [HttpGet("GetByCitizenId")]
        public async Task<ActionResult<ResponseModel<List<CitizenAttachmentResponseDTO>>>> GetByCitizenId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = citizenAttachmentService.GetCitizenAttachmentByCitizenId(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenAttachmentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
    }
}