using BISPAPIORA.Models.DTOS.ProvinceDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.ProvinceServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService provinceService;
        public ProvinceController(IProvinceService provinceService)
        {
            this.provinceService = provinceService;
        }
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
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ProvinceResponseDTO>>>> Get()
        {
            var response = provinceService.GetProvincesList();
            return Ok(await response);
        }
    }
}
