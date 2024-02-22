using BISPAPIORA.Models.DTOS.ReportingResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.TehsilStatusResponseServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportingResponseController : ControllerBase
    {
        private readonly IReportingResponseService reportingResponseService;
        public ReportingResponseController(IReportingResponseService reportingResponseService)
        {
            this.reportingResponseService = reportingResponseService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<ReportingResponseDTO>>> Get()
        {
            var response = reportingResponseService.GetReportingResponse();
            return Ok(await response);
        }
    }
}
