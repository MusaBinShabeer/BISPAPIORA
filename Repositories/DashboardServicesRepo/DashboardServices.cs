using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;

namespace BISPAPIORA.Repositories.DashboardServicesRepo
{
    public class DashboardServices
    {
        private readonly Dbcontext db;
        private readonly IMapper mapper;
        public DashboardServices(Dbcontext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<ResponseModel<DashboardUserPerformanceResponseDTO>> GetUserPerformanceStats(string userId, string dateStart, string dateEnd) 
        {
            try
            {
                var registeredCitizenQuery =  db.tbl_citizens
                    .Include(x=>x.tbl_citizen_registration).ThenInclude(x=>x.registerd_by)
                    .Where(x=>x.tbl_citizen_registration!=null).AsQueryable();
                var registeredBaseCitizen= mapper.Map<IQueryable<DashboardCitizenBaseModel>>(registeredCitizenQuery);
                var enrolledCitizenQuery =  db.tbl_citizens
                   .Include(x => x.tbl_enrollment).ThenInclude(x => x.enrolled_by)
                   .Where(x => x.tbl_enrollment != null).AsQueryable();
                var enrolledBaseCitizen = mapper.Map< IQueryable<DashboardCitizenBaseModel>>(enrolledCitizenQuery);
                var predicateRegistered = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                predicateRegistered= predicateRegistered.And(x=>x.registered_by==Guid.Parse(userId));               
                var predicateEnrolled = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                predicateEnrolled= predicateEnrolled.And(x=>x.enrolled_by==Guid.Parse(userId));
                if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
                {
                    predicateRegistered = predicateRegistered.And(x=>x.registered_date>= DateTime.Parse(dateStart) && x.registered_date<= DateTime.Parse(dateEnd));
                    predicateEnrolled = predicateEnrolled.And(x=>x.enrolled_date>= DateTime.Parse(dateStart) && x.enrolled_date<= DateTime.Parse(dateEnd));
                }
                var registeredCitizen= registeredBaseCitizen.Where(predicateRegistered).ToList();
                var enrolledCitizen= enrolledBaseCitizen.Where(predicateEnrolled).ToList();
                return new ResponseModel<DashboardUserPerformanceResponseDTO>()
                {
                    data = mapper.Map<DashboardUserPerformanceResponseDTO>((registeredCitizen, enrolledCitizen)),
                    remarks = "Success",
                    success = true
                };
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
