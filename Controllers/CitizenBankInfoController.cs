//using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
//using BISPAPIORA.Models.DTOS.ResponseDTO;
//using BISPAPIORA.Repositories.CitizenBankInfoServicesRepo;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace BISPAPIORA.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CitizenBankInfoController : ControllerBase
//    {
//        private readonly ICitizenBankInfoService citizenBankInfoService;
//        public CitizenBankInfoController(ICitizenBankInfoService citizenBankInfoService)
//        {
//            this.citizenBankInfoService = citizenBankInfoService;
//        }
//        [HttpPost]
//        public async Task<ActionResult<ResponseModel<CitizenBankInfoResponseDTO>>> Post(AddCitizenBankInfoDTO model)
//        {
//            if (ModelState.IsValid)
//            {
//                var response = citizenBankInfoService.AddCitizenBankInfo(model);
//                return Ok(await response);
//            }
//            else
//            {
//                var response = new ResponseModel<CitizenBankInfoResponseDTO>()
//                {
//                    remarks = "Model Not Verified",
//                    success = false
//                };
//                return BadRequest(response);
//            }
//        }
//        [HttpPut]
//        public async Task<ActionResult<ResponseModel<CitizenBankInfoResponseDTO>>> Put(UpdateCitizenBankInfoDTO model)
//        {
//            if (ModelState.IsValid)
//            {
//                var response = citizenBankInfoService.UpdateCitizenBankInfo(model);
//                return Ok(await response);
//            }
//            else
//            {
//                var response = new ResponseModel<CitizenBankInfoResponseDTO>()
//                {
//                    remarks = "Model Not Verified",
//                    success = false
//                };
//                return BadRequest(response);
//            }
//        }
//        [HttpDelete]
//        public async Task<ActionResult<ResponseModel<CitizenBankInfoResponseDTO>>> Delete(string id)
//        {
//            if (ModelState.IsValid)
//            {
//                var response = citizenBankInfoService.DeleteCitizenBankInfo(id);
//                return Ok(await response);
//            }
//            else
//            {
//                var response = new ResponseModel<CitizenBankInfoResponseDTO>()
//                {
//                    remarks = "Model Not Verified",
//                    success = false
//                };
//                return BadRequest(response);
//            }
//        }
//        [HttpGet("GetById")]
//        public async Task<ActionResult<ResponseModel<CitizenBankInfoResponseDTO>>> GetById(string id)
//        {
//            if (!string.IsNullOrEmpty(id))
//            {
//                var response = citizenBankInfoService.GetCitizenBankInfo(id);
//                return Ok(await response);
//            }
//            else
//            {
//                var response = new ResponseModel<CitizenBankInfoResponseDTO>()
//                {
//                    remarks = "Parameter missing",
//                    success = false
//                };
//                return BadRequest(response);
//            }
//        }
//        [HttpGet]
//        public async Task<ActionResult<ResponseModel<List<CitizenBankInfoResponseDTO>>>> Get()
//        {
//            var response = citizenBankInfoService.GetCitizenBankInfosList();
//            return Ok(await response);
//        }
//    }
//}