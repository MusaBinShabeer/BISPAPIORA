using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.PaymentDTO;
using BISPAPIORA.Models.DTOS.ProvinceDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.PaymentServicesRepo;
using BISPAPIORA.Repositories.ProvinceServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        // Constructor to inject the provinceService dependency
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        // POST api/Province
        // Endpoint for adding a new province
        [HttpPost]
        public async Task<ActionResult<ResponseModel<PaymentResponseDTO>>> Post(AddPaymentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = paymentService.AddPayment(model/*.citizenCnic, model.quarterCode,model.paidAmount*/);
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
    }
}
