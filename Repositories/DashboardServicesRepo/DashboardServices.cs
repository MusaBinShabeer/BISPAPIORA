using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.DashboardServicesRepo
{
    public class DashboardServices
    {
        private readonly Dbcontext db;
        public DashboardServices(Dbcontext db)
        {
            this.db = db;
        }
        public async Task<ResponseModel<DashboardUserPerformanceResponseDTO>> GetUserPerformanceStats(string userId) 
        {
            try
            {

            }
            catch (Exception ex)
            {
                return new ResponseModel<DashboardUserPerformanceResponseDTO>()
                {
                    remarks = $"There was a fatal error {ex.ToString()}",
                    success = false,
                };
            }
        } 
    }
}
