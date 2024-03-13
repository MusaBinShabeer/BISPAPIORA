using BISPAPIORA.Models.DTOS.TehsilDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BISPAPIORA.Repositories.TehsilServicesRepo;
using BISPAPIORA.Extensions.Middleware;

namespace BISPAPIORA.Controllers
{
    // Controller for managing tehsil-related operations
    // Requires user authentication
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class TehsilController : ControllerBase
    {
        private readonly ITehsilService tehsilService;

        // Constructor to inject the tehsilService dependency
        public TehsilController(ITehsilService tehsilService)
        {
            this.tehsilService = tehsilService;
        }

        // POST api/Tehsil
        // Endpoint for adding a new tehsil
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

        // PUT api/Tehsil
        // Endpoint for updating an existing tehsil
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

        // DELETE api/Tehsil
        // Endpoint for deleting a tehsil by ID
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

        // GET api/Tehsil/GetById
        // Endpoint for getting a tehsil by ID
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

        // GET api/Tehsil
        // Endpoint for getting a list of all tehsils
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<TehsilResponseDTO>>>> Get()
        {
            var response = tehsilService.GetTehsilsList();
            return Ok(await response);
        }

        // GET api/Tehsil/GetByDistrictId
        // Endpoint for getting a list of tehsils by district ID
        [HttpGet("GetByDistrictId")]
        public async Task<ActionResult<ResponseModel<List<TehsilResponseDTO>>>> GetByDistrictId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = tehsilService.GetTehsilByDistrictId(id);
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
