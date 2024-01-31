using BISPAPIORA.Models.DTOS.DistrictDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.TehsilDTO;
using BISPAPIORA.Repositories.DistrictServicesRepo;
using BISPAPIORA.Repositories.TehsilServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService districtService;
        public DistrictController(IDistrictService districtService)
        {
            this.districtService = districtService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<DistrictResponseDTO>>> Post(AddDistrictDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = districtService.AddDistrict(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<DistrictResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<DistrictResponseDTO>>> Put(UpdateDistrictDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = districtService.UpdateDistrict(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<DistrictResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<DistrictResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = districtService.DeleteDistrict(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<DistrictResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<DistrictResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = districtService.GetDistrict(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<DistrictResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<DistrictResponseDTO>>>> Get()
        {
            var response = districtService.GetDistrictsList();
            return Ok(await response);
        }
        [HttpGet("GetByProvinceId")]
        public async Task<ActionResult<ResponseModel<List<TehsilResponseDTO>>>> GetByProvinceId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = districtService.GetDistrictByProviceId(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<TehsilResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
    }
}
