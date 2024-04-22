using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.BankStatementDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.BankStatementServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Controllers
{
    // Controller for managing attachment images of citizens
    // Requires user authentication
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class BankStatementController : ControllerBase
    {
        private readonly IBankStatementService bankStatementService;

        // Constructor to inject the BankStatementService dependency
        public BankStatementController(IBankStatementService bankStatementService)
        {
            this.bankStatementService = bankStatementService;
        }

        // POST api/BankStatement
        // Endpoint for adding a new attachment image
        [HttpPost]
        public async Task<ActionResult<ResponseModel<BankStatementResponseDTO>>> Post(AddBankStatementDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = bankStatementService.AddBankStatement(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankStatementResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // PUT api/BankStatement
        // Endpoint for updating an existing attachment image
        [HttpPut]
        public async Task<ActionResult<ResponseModel<BankStatementResponseDTO>>> Put(UpdateBankStatementDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = bankStatementService.UpdateBankStatement(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankStatementResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // DELETE api/BankStatement
        // Endpoint for deleting an attachment image by ID
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<BankStatementResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = bankStatementService.DeleteBankStatement(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankStatementResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // GET api/BankStatement/GetById
        // Endpoint for getting an attachment image by ID
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<BankStatementResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = bankStatementService.GetBankStatement(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankStatementResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // GET api/BankStatement
        // Endpoint for getting a list of all attachment images
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<BankStatementResponseDTO>>>> Get()
        {
            var response = bankStatementService.GetBankStatementsList();
            return Ok(await response);
        }

        // GET api/BankStatement/GetByCitizenId
        // Endpoint for getting a list of attachment images by Citizen ID
        [HttpGet("GetByCitizenId")]
        public async Task<ActionResult<ResponseModel<List<BankStatementResponseDTO>>>> GetByCitizenId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = bankStatementService.GetBankStatementByCitizenId(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<BankStatementResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
    }

}