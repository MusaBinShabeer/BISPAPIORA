using BISPAPIORA.Models.DTOS.DistrictStatusResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.DistrictStatusResponseServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictStatusResponseController : ControllerBase
    {
        private readonly IDistrictStatusResponseService districtStatusResponseService;
        public DistrictStatusResponseController(IDistrictStatusResponseService districtStatusResponseService)
        {
            this.districtStatusResponseService = districtStatusResponseService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<DistrictStatusResponseDTO>>>> Get()
        {
            var response = districtStatusResponseService.GetDistrictStatusResponses();
            return Ok(await response);
        }
    }
}
