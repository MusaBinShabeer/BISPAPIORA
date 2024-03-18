using AutoMapper;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.TehsilStatusResponseDTO;
using BISPAPIORA.Repositories.TehsilStatusResponseServicesRepo;

namespace BISPAPIORA.Repositories.DashboardServicesRepo
{
    public class DashboardServices : IDashboardServices
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public TehsilStatusResponseService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<List<TehsilStatusResponseDTO>>> GetTehsilStatusResponses()
        {
            try
            {
                var totalCitizens = await db.tbl_citizens.Where(x => x.tbl_citizen_registration != null).ToListAsync();
                var totalCitizensCount = totalCitizens.Count();
                if (totalCitizensCount > 0)
                {
                    var tehsils = await db.tbl_tehsils.ToListAsync();
                    var tehsilStatusResponseDTOs = tehsils.Select(tehsil => new TehsilStatusResponseDTO
                    {
                        tehsilName = tehsil.tehsil_name,
                        applicantCount = totalCitizens.Count(citizen => citizen.fk_tehsil == tehsil.tehsil_id)
                    }).ToList();

                    return new ResponseModel<List<TehsilStatusResponseDTO>>()
                    {
                        success = true,
                        remarks = "Citizen counts based on tehsil retrieved successfully.",
                        data = tehsilStatusResponseDTOs
                    };

                }
                else
                {
                    return new ResponseModel<List<TehsilStatusResponseDTO>>()
                    {
                        success = false,
                        remarks = "There is no citizen."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<TehsilStatusResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
