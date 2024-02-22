using BISPAPIORA.Models.DTOS.ReportingResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.ReportingResponseServicesRepo
{
    public interface IReportingResponseService
    {
        public Task<ResponseModel<ReportingResponseDTO>> GetReportingResponse();
    }
}
