using BISPAPIORA.Models.DTOS.ImageCitizenThumbPrintDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.ImageCitizenThumbPrintServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCitizenThumbPrintController : ControllerBase
    {
        private readonly IImageCitizenThumbPrintService imageCitizenThumbPrintService;
        public ImageCitizenThumbPrintController(IImageCitizenThumbPrintService ImageCitizenThumbPrintService)
        {
            this.imageCitizenThumbPrintService = ImageCitizenThumbPrintService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<ImageCitizenThumbPrintResponseDTO>>> Post(AddImageCitizenThumbPrintDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = imageCitizenThumbPrintService.AddImageCitizenThumbPrint(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<ImageCitizenThumbPrintResponseDTO>>> Put(UpdateImageCitizenThumbPrintDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = imageCitizenThumbPrintService.UpdateImageCitizenThumbPrint(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<ImageCitizenThumbPrintResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = imageCitizenThumbPrintService.DeleteImageCitizenThumbPrint(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<ImageCitizenThumbPrintResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = imageCitizenThumbPrintService.GetImageCitizenThumbPrint(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>>> Get()
        {
            var response = imageCitizenThumbPrintService.GetImageCitizenThumbPrintsList();
            return Ok(await response);
        }
        [HttpGet("GetByCitizenId")]
        public async Task<ActionResult<ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>>> GetByCitizenId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = imageCitizenThumbPrintService.GetImageCitizenThumbPrintByCitizenId(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
    }
}