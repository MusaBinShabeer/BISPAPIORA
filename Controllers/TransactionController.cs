using BISPAPIORA.Models.DTOS.TransactionDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.TehsilDTO;
using BISPAPIORA.Repositories.TransactionServicesRepo;
using BISPAPIORA.Repositories.DistrictServicesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BISPAPIORA.Extensions.Middleware;

namespace BISPAPIORA.Controllers
{
    // Controller for managing transaction-related operations
    // Requires user authentication
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        // Constructor to inject the transactionService dependency
        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        // POST api/Transaction
        // Endpoint for adding a new transaction
        [HttpPost]
        public async Task<ActionResult<ResponseModel<TransactionResponseDTO>>> Post(AddTransactionDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = transactionService.AddTransaction(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<TransactionResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // PUT api/Transaction
        // Endpoint for updating an existing transaction
        [HttpPut]
        public async Task<ActionResult<ResponseModel<TransactionResponseDTO>>> Put(UpdateTransactionDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = transactionService.UpdateTransaction(model);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<TransactionResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // DELETE api/Transaction
        // Endpoint for deleting a transaction by ID
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<TransactionResponseDTO>>> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var response = transactionService.DeleteTransaction(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<TransactionResponseDTO>()
                {
                    remarks = "Model Not Verified",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // GET api/Transaction/GetById
        // Endpoint for getting a transaction by ID
        [HttpGet("GetById")]
        public async Task<ActionResult<ResponseModel<TransactionResponseDTO>>> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = transactionService.GetTransaction(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<TransactionResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }

        // GET api/Transaction
        // Endpoint for getting a list of all transactions
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<TransactionResponseDTO>>>> Get()
        {
            var response = transactionService.GetTransactionsList();
            return Ok(await response);
        }

        // GET api/Transaction/GetByCitizenId
        // Endpoint for getting a list of transactions by citizen ID
        [HttpGet("GetByCitizenId")]
        public async Task<ActionResult<ResponseModel<List<TransactionResponseDTO>>>> GetByCitizenId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = transactionService.GetTransactionByCitizenId(id);
                return Ok(await response);
            }
            else
            {
                var response = new ResponseModel<TransactionResponseDTO>()
                {
                    remarks = "Parameter missing",
                    success = false
                };
                return BadRequest(response);
            }
        }
    }

}