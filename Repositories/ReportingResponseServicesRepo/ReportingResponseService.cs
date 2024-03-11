using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ReportingResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BISPAPIORA.Repositories.ReportingResponseServicesRepo
{
    public class ReportingResponseService : IReportingResponseService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public ReportingResponseService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<ReportingResponseDTO>> GetReportingResponse()
        {
            try
            {
                var totalCitizens = await db.tbl_citizens.ToListAsync();
                var totalCitizensCount = totalCitizens.Count();
                if (totalCitizensCount > 0)
                {
                    var registeredCitizens = await db.tbl_registrations.ToListAsync();
                    var registeredCitizensCount = registeredCitizens.Count();

                    var enrolledCitizens = await db.tbl_enrollments.ToListAsync();
                    var enrolledCitizensCount = enrolledCitizens.Count();

                    //var percentageCitizenEnrolled = (double)enrolledCitizensCount / totalCitizensCount * 100;
                    //var percentageCitizenRegistered = (double)registeredCitizensCount / totalCitizensCount * 100;

                    var reportResponseDto = new ReportingResponseDTO() 
                    {
                        totalCount = totalCitizensCount,
                        enrolledCount = enrolledCitizensCount,
                        registeredCount = registeredCitizensCount
                        //, percentageEnrolled = percentageCitizenEnrolled,
                        //percentageRegistered = percentageCitizenRegistered
                    };
                    return new ResponseModel<ReportingResponseDTO>()
                    {
                        success = true,
                        remarks = "Enrolled and registed Citizens found.",
                        data = reportResponseDto
                    };

                }
                else
                {
                    return new ResponseModel<ReportingResponseDTO>()
                    {
                        success = false,
                        remarks = "There is no citizen registered or enrolled."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ReportingResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}