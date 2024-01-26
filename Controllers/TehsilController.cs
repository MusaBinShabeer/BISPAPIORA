using BISPAPIORA.Models.DTOS.TehsilDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BISPAPIORA.Repositories.TehsilServicesRepo;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TehsilController : ControllerBase
    {
        private readonly ITehsilService tehsilService;
        public TehsilController(ITehsilService tehsilService)
        {
            this.tehsilService = tehsilService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<TehsilResponseDTO>>> Post(AddTehsilDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = tehsilService.AddTehsil(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<TehsilResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<TehsilResponseDTO>>> Put(UpdateTehsilDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = tehsilService.UpdateTehsil(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<TehsilResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<TehsilResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = tehsilService.DeleteTehsil(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<TehsilResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<TehsilResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = tehsilService.GetTehsil(id);
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
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<TehsilResponseDTO>>>> Get()
        {
            var response = tehsilService.GetTehsilsList();
            return Ok(await response);
        }
    }
}
