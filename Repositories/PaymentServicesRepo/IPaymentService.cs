using BISPAPIORA.Models.DTOS.PaymentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.PaymentServicesRepo
{
    public interface IPaymentService
    {
        public Task<ResponseModel<PaymentResponseDTO>> AddPayment(AddPaymentDTO model);
        public Task<ResponseModel<PaymentResponseDTO>> DeletePayment(string bankId);
        public Task<ResponseModel<List<PaymentResponseDTO>>> GetPaymentsList();
        public Task<ResponseModel<PaymentResponseDTO>> GetPayment(string bankId);
        public Task<ResponseModel<PaymentResponseDTO>> UpdatePayment(UpdatePaymentDTO model);
        public Task<ResponseModel<List<PaymentResponseDTO>>> GetPaymentByCitizenId(string citizenId);
        public Task<ResponseModel<List<PaymentResponseDTO>>> GetPaymentByCitizenCnic(string citizenCnic);
        public Task<ResponseModel<List<PaymentResponseDTO>>> GetPaymentByCitizenComplianceId(string citizenComplainceId);
    }
}
