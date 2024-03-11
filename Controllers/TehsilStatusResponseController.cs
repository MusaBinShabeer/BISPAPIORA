using BISPAPIORA.Models.DTOS.TehsilStatusResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.TehsilStatusResponseServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TehsilStatusResponseController : ControllerBase
    {
        private readonly ITehsilStatusResponseService tehsilStatusResponseService;
        public TehsilStatusResponseController(ITehsilStatusResponseService tehsilStatusResponseService)
        {
            this.tehsilStatusResponseService = tehsilStatusResponseService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<TehsilStatusResponseDTO>>>> Get()
        {
            var response = tehsilStatusResponseService.GetTehsilStatusResponses();
            return Ok(await response);
        }
    }
}
