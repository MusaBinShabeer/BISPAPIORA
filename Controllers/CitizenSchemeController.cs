using BISPAPIORA.Models.DTOS.CitizenSchemeDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenSchemeServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenSchemeController : ControllerBase
    {

        private readonly ICitizenSchemeService CitizenSchemeService;
        public CitizenSchemeController(ICitizenSchemeService CitizenSchemeService)
        {
            this.CitizenSchemeService = CitizenSchemeService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<CitizenSchemeResponseDTO>>> Post(AddCitizenSchemeDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = CitizenSchemeService.AddCitizenScheme(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Update Citizen Compliance Method
        [HttpPut]
        public async Task<ActionResult<ResponseModel<CitizenSchemeResponseDTO>>> Put(UpdateCitizenSchemeDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = CitizenSchemeService.UpdateCitizenScheme(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Delete Citizen Compliance Method
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<CitizenSchemeResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = CitizenSchemeService.DeleteCitizenScheme(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get Citizen Compliance By Id Method
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<CitizenSchemeResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = CitizenSchemeService.GetCitizenScheme(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        //Get All Citizen Compliance Method
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<CitizenSchemeResponseDTO>>>> Get()
        {
            var response = CitizenSchemeService.GetCitizenSchemesList();
            return Ok(await response);
        }       
        //Get Citizen By Id Method
        [HttpGet("GetByCitizenId")]
        public async Task<ActionResult<ResponseModel<List<CitizenSchemeResponseDTO>>>> GetByCitizenId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = CitizenSchemeService.GetCitizenSchemeByCitizenId(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
    }
}
