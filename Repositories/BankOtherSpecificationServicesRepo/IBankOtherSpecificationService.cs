using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;

namespace BISPAPIORA.Repositories.BankOtherSpecificationServicesRepo
{
    public interface IBankOtherSpecificationService
    {
        public  Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> AddRegisteredBankOtherSpecification(AddRegisteredBankOtherSpecificationDTO model);
        public  Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> UpdateRegisteredBankOtherSpecification(UpdateRegisteredBankOtherSpecificationDTO model);
        public Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> AddEnrolledBankOtherSpecification(AddEnrolledBankOtherSpecificationDTO model);
        public Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> UpdateEnrolledBankOtherSpecification(UpdateEnrolledCitizenBankInfoDTO model);
    }
}