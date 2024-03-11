using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.ImageCitizenFingerPrintDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.ImageCitizenFingePrintServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    // Controller for managing fingerprint images of citizens
    // Requires user authentication
    [UserAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCitizenFingerPrintController : ControllerBase
    {
        private readonly IImageCitizenFingerPrintService imageCitizenFingerPrintService;

        // Constructor to inject the imageCitizenFingerPrintService dependency
        public ImageCitizenFingerPrintController(IImageCitizenFingerPrintService ImageCitizenFingerPrintService)
        {
            this.imageCitizenFingerPrintService = ImageCitizenFingerPrintService;
        }

        // POST api/ImageCitizenFingerPrint
        // Endpoint for adding a new fingerprint image
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

        // PUT api/ImageCitizenFingerPrint
        // Endpoint for updating an existing fingerprint image
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

        // DELETE api/ImageCitizenFingerPrint
        // Endpoint for deleting a fingerprint image by ID
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

        // GET api/ImageCitizenFingerPrint/GetById
        // Endpoint for getting a fingerprint image by ID
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

        // GET api/ImageCitizenFingerPrint
        // Endpoint for getting a list of all fingerprint images
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>>> Get()
        {
            var response = imageCitizenFingerPrintService.GetImageCitizenFingerPrintsList();
            return Ok(await response);
        }

        // GET api/ImageCitizenFingerPrint/GetByCitizenId
        // Endpoint for getting a list of fingerprint images by Citizen ID
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