using BISPAPIORA.Models.DTOS.TransactionDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.TransactionServicesRepo
{
    public interface ITransactionService
    {
        public Task<ResponseModel<TransactionResponseDTO>> AddTransaction(AddTransactionDTO model);
        public Task<ResponseModel<TransactionResponseDTO>> DeleteTransaction(string bankId);
        public Task<ResponseModel<List<TransactionResponseDTO>>> GetTransactionsList();
        public Task<ResponseModel<TransactionResponseDTO>> GetTransaction(string bankId);
        public Task<ResponseModel<TransactionResponseDTO>> UpdateTransaction(UpdateTransactionDTO model);
        public Task<ResponseModel<List<TransactionResponseDTO>>> GetTransactionByCitizenId(string citizenId);
        public Task<ResponseModel<List<TransactionResponseDTO>>> GetTransactionByCitizenCnic(string citizenCnic);
    }
}
