using BISPAPIORA.Models.DTOS.ProvinceStatusResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.ProvinceStatusResponseServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceStatusResponseController : ControllerBase
    {
        private readonly IProvinceStatusResponseService provinceStatusResponseService;
        public ProvinceStatusResponseController(IProvinceStatusResponseService provinceStatusResponseService)
        {
            this.provinceStatusResponseService = provinceStatusResponseService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ProvinceStatusResponseDTO>>>> Get()
        {
            var response = provinceStatusResponseService.GetProvinceStatusResponses();
            return Ok(await response);
        }
    }
}
