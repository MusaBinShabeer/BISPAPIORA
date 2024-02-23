using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ProvinceStatusResponseDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BISPAPIORA.Repositories.ProvinceStatusResponseServicesRepo
{
    public class ProvinceStatusResponseService : IProvinceStatusResponseService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public ProvinceStatusResponseService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<List<ProvinceStatusResponseDTO>>> GetProvinceStatusResponses()
        {
            try
            {
                var totalCitizens = await db.tbl_citizens.Where(x => x.tbl_citizen_registration != null).ToListAsync();
                var totalCitizensCount = totalCitizens.Count();
                if (totalCitizensCount > 0)
                {
                    var provinces = await db.tbl_provinces.ToListAsync();
                    var provinceStatusResponseDTOs = provinces.Select(province => new ProvinceStatusResponseDTO
                    {
                        provinceName = province.province_name,
                        applicantCount = totalCitizens.Count(citizen => citizen.tbl_citizen_tehsil.tbl_district.fk_province == province.province_id)
                    }).ToList();

                    return new ResponseModel<List<ProvinceStatusResponseDTO>>()
                    {
                        success = true,
                        remarks = "Citizen counts based on Province retrieved successfully.",
                        data = provinceStatusResponseDTOs
                    };

                }
                else
                {
                    return new ResponseModel<List<ProvinceStatusResponseDTO>>()
                    {
                        success = false,
                        remarks = "There is no citizen."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ProvinceStatusResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}