using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.CitizenBankInfoServicesRepo
{
    public interface ICitizenBankInfoService
    {
        #region Registered Citizen Bank Info DTO
        public Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> AddRegisteredCitizenBankInfo(AddRegisteredCitizenBankInfoDTO model);
        public Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> DeleteRegisteredCitizenBankInfo(string CitizenBankInfoId);
        public Task<ResponseModel<List<RegisteredCitizenBankInfoResponseDTO>>> GetRegisteredCitizenBankInfosList();
        public Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> GetRegisteredCitizenBankInfo(string citizenBankInfoId);
        public Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> UpdateRegisteredCitizenBankInfo(UpdateRegisteredCitizenBankInfoDTO model);
        #endregion
        #region Enrolled Citizen Bank Info
        public Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> AddEnrolledCitizenBankInfo(AddEnrolledCitizenBankInfoDTO model);
        public Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> DeleteEnrolledCitizenBankInfo(string CitizenBankInfoId);
        public Task<ResponseModel<List<EnrolledCitizenBankInfoResponseDTO>>> GetEnrolledCitizenBankInfosList();
        public Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> GetEnrolledCitizenBankInfo(string citizenBankInfoId);
        public Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> UpdateEnrolledCitizenBankInfo(UpdateEnrolledCitizenBankInfoDTO model);
        #endregion
    }
}