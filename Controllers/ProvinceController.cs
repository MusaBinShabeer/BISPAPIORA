using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.ProvinceDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.ProvinceServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    // Controller for managing province-related operations
    // Requires user authentication
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService provinceService;

        // Constructor to inject the provinceService dependency
        public ProvinceController(IProvinceService provinceService)
        {
            this.provinceService = provinceService;
        }

        // POST api/Province
        // Endpoint for adding a new province
        [HttpPost]
        public async Task<ActionResult<ResponseModel<ProvinceResponseDTO>>> Post(AddProvinceDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = provinceService.AddProvince(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ProvinceResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // PUT api/Province
        // Endpoint for updating an existing province
        [HttpPut]
        public async Task<ActionResult<ResponseModel<ProvinceResponseDTO>>> Put(UpdateProvinceDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = provinceService.UpdateProvince(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ProvinceResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // DELETE api/Province
        // Endpoint for deleting a province by ID
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<ProvinceResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = provinceService.DeleteProvince(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ProvinceResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // GET api/Province/GetById
        // Endpoint for getting a province by ID
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<ProvinceResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = provinceService.GetProvince(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<ProvinceResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // GET api/Province
        // Endpoint for getting a list of all provinces
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ProvinceResponseDTO>>>> Get()
        {
            var response = provinceService.GetProvincesList();
            return Ok(await response);
        }
    }

}
