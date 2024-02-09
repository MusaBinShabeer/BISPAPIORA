using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;

namespace BISPAPIORA.Repositories.BankOtherSpecificationServicesRepo
{
    public interface IBankOtherSpecificationService
    {
        public Task<ResponseModel<BankOtherSpecificationResponseDTO>> AddBankOtherSpecification(AddBankOtherSpecificationDTO model);
        public Task<ResponseModel<BankOtherSpecificationResponseDTO>> UpdateBankOtherSpecification(UpdateBankOtherSpecificationDTO model);
    }
}