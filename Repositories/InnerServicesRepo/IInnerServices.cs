using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using BISPAPIORA.Models.DTOS.InnerServicesDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.VerificationResponseDTO;

namespace BISPAPIORA.Repositories.InnerServicesRepo
{
    public interface IInnerServices
    {
        public Task<ResponseModel<SurvayResponseDTO>> VerifyCitzen(string cnic);
        public Task<ResponseModel> SendEmail(string to, string subject, string body);
        public List<int> GetQuarterCodesBetween(int startingQuarterCode, int currentQuarterCode);
        public Task<double> GetTotalExpectedSavingAmount(List<int> quarterCodes, Guid fk_citizen, double expectedSavingAmountPerQuarter);
        public List<QuarterCodesReponseDTO> GetAllQuarterCodes(int startingQuarterCode);
        public Task<Boolean> CheckCompliance(List<int> quarterCodes,Guid citizenId);
        public Task<int> CheckCompliance(List<DashboardCitizenLocationModel> citizens);
        public Task<List<DashboardCitizenLocationModel>> GetComplaintCitizen(List<DashboardCitizenLocationModel> citizens);


    }
}