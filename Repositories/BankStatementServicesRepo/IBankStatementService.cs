using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankStatementDTO;

namespace BISPAPIORA.Repositories.BankStatementServicesRepo
{
    public interface IBankStatementService
    {
        public Task<ResponseModel<BankStatementResponseDTO>> AddBankStatement(AddBankStatementDTO model);
        public Task<ResponseModel<BankStatementResponseDTO>> AddFkCitizenComplianceToAttachment(AddBankStatementDTO model);
        public Task<ResponseModel<BankStatementResponseDTO>> DeleteBankStatement(string bankStatementId);
        public Task<ResponseModel<List<BankStatementResponseDTO>>> GetBankStatementsList();
        public Task<ResponseModel<BankStatementResponseDTO>> GetBankStatement(string bankStatementId);
        public Task<ResponseModel<BankStatementResponseDTO>> GetBankStatementByCitizenCnic(string citizenCnic);
        public Task<ResponseModel<BankStatementResponseDTO>> UpdateBankStatement(UpdateBankStatementDTO model);
        public Task<ResponseModel<List<BankStatementResponseDTO>>> GetBankStatementByCitizenId(string citizenId);
    }
}
