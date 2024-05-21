using BISPAPIORA.Models.DTOS.BankDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.BankServicesRepo
{
    public interface IBankService
    {
        public Task<ResponseModel<BankResponseDTO>> AddBank(AddBankDTO model);
        public Task<ResponseModel<BankResponseDTO>> DeleteBank(string bankId);
        public Task<ResponseModel<List<BankResponseDTO>>> GetBanksList();
        public Task<ResponseModel<List<BankResponseDTO>>> GetActiveBanksList();
        public Task<ResponseModel<BankResponseDTO>> GetBank(string bankId);
        public Task<ResponseModel<BankResponseDTO>> UpdateBank(UpdateBankDTO model);
    }
}
