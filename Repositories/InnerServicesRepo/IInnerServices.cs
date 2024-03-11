using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.VerificationResponseDTO;

namespace BISPAPIORA.Repositories.InnerServicesRepo
{
    public interface IInnerServices
    {
        public Task<ResponseModel<SurvayResponseDTO>> VerifyCitzen(string cnic);
        public Task<ResponseModel> SendEmail(string to, string subject, string body);
    }
}