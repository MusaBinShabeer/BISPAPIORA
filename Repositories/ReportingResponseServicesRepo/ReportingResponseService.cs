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


        // Retrieves reporting response data including total citizen count, registered citizen count, and enrolled citizen count.
        public async Task<ResponseModel<ReportingResponseDTO>> GetReportingResponse()
        {
            try
            {
                // Retrieve total citizens
                var totalCitizens = await db.tbl_citizens.ToListAsync();
                var totalCitizensCount = totalCitizens.Count();

                // Check if there are any citizens
                if (totalCitizensCount > 0)
                {
                    // Retrieve registered citizens and count
                    var registeredCitizens = await db.tbl_registrations.ToListAsync();
                    var registeredCitizensCount = registeredCitizens.Count();

                    // Retrieve enrolled citizens and count
                    var enrolledCitizens = await db.tbl_enrollments.ToListAsync();
                    var enrolledCitizensCount = enrolledCitizens.Count();

                    // Create ReportingResponseDTO object with retrieved counts
                    var reportResponseDto = new ReportingResponseDTO()
                    {
                        totalCount = totalCitizensCount,
                        enrolledCount = enrolledCitizensCount,
                        registeredCount = registeredCitizensCount
                    };

                    // Return response model with success status and retrieved data
                    return new ResponseModel<ReportingResponseDTO>()
                    {
                        success = true,
                        remarks = "Enrolled and registered citizens found.",
                        data = reportResponseDto
                    };
                }
                else
                {
                    // Return response model indicating no citizens are registered or enrolled
                    return new ResponseModel<ReportingResponseDTO>()
                    {
                        success = false,
                        remarks = "There are no registered or enrolled citizens."
                    };
                }
            }
            catch (Exception ex)
            {
                // Return response model indicating a fatal error occurred
                return new ResponseModel<ReportingResponseDTO>()
                {
                    success = false,
                    remarks = $"A fatal error occurred: {ex.Message}"
                };
            }
        }

    }
}