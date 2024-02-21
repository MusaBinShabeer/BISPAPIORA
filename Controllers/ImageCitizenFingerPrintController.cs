using BISPAPIORA.Models.DTOS.ImageCitizenFingerPrintDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.ImageCitizenFingePrintServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCitizenFingerPrintController : ControllerBase
    {
        private readonly IImageCitizenFingerPrintService imageCitizenFingerPrintService;
        public ImageCitizenFingerPrintController(IImageCitizenFingerPrintService ImageCitizenFingerPrintService)
        {
            this.imageCitizenFingerPrintService = ImageCitizenFingerPrintService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<ImageCitizenFingerPrintResponseDTO>>> Post(AddImageCitizenFingerPrintDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = imageCitizenFingerPrintService.AddImageCitizenFingerPrint(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<ImageCitizenFingerPrintResponseDTO>>> Put(UpdateImageCitizenFingerPrintDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = imageCitizenFingerPrintService.UpdateImageCitizenFingerPrint(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<ImageCitizenFingerPrintResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = imageCitizenFingerPrintService.DeleteImageCitizenFingerPrint(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<ImageCitizenFingerPrintResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = imageCitizenFingerPrintService.GetImageCitizenFingerPrint(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>>> Get()
        {
            var response = imageCitizenFingerPrintService.GetImageCitizenFingerPrintsList();
            return Ok(await response);
        }
        [HttpGet("GetByCitizenId")]
        public async Task<ActionResult<ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>>> GetByCitizenId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = imageCitizenFingerPrintService.GetImageCitizenFingerPrintByCitizenId(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
    }
}