using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.ImageCitizenAttachmentServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [UserAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCitizenAttachmentController : ControllerBase
    {
        private readonly IImageCitizenAttachmentService imageCitizenAttachmentService;
        public ImageCitizenAttachmentController(IImageCitizenAttachmentService ImageCitizenAttachmentService)
        {
            this.imageCitizenAttachmentService = ImageCitizenAttachmentService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<ImageCitizenAttachmentResponseDTO>>> Post(AddImageCitizenAttachmentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = imageCitizenAttachmentService.AddImageCitizenAttachment(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<ImageCitizenAttachmentResponseDTO>>> Put(UpdateImageCitizenAttachmentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = imageCitizenAttachmentService.UpdateImageCitizenAttachment(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<ImageCitizenAttachmentResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = imageCitizenAttachmentService.DeleteImageCitizenAttachment(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<ImageCitizenAttachmentResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = imageCitizenAttachmentService.GetImageCitizenAttachment(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ImageCitizenAttachmentResponseDTO>>>> Get()
        {
            var response = imageCitizenAttachmentService.GetImageCitizenAttachmentsList();
            return Ok(await response);
        }
        [HttpGet("GetByCitizenId")]
        public async Task<ActionResult<ResponseModel<List<ImageCitizenAttachmentResponseDTO>>>> GetByCitizenId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = imageCitizenAttachmentService.GetImageCitizenAttachmentByCitizenId(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
    }
}