using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenComplianceDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.CitizenComplianceServicesRepo
{
    public interface ICitizenComplianceService
    {
        public Task<ResponseModel<CitizenComplianceResponseDTO>> AddCitizenCompliance(AddCitizenComplianceDTO model);
        public Task<ResponseModel<CitizenComplianceResponseDTO>> DeleteCitizenCompliance(string bankId);
        public Task<ResponseModel<List<CitizenComplianceResponseDTO>>> GetCitizenCompliancesList();
        public Task<ResponseModel<CitizenComplianceResponseDTO>> GetCitizenCompliance(string bankId);
        public Task<ResponseModel<CitizenComplianceResponseDTO>> UpdateCitizenCompliance(UpdateCitizenComplianceDTO model);
        public Task<ResponseModel<List<CitizenComplianceResponseDTO>>> GetCitizenComplianceByCitizenId(string citizenId);
    }
}
