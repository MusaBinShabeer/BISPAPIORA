using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.CitizenComplianceDTO;
using BISPAPIORA.Models.DTOS.CitizenComplianceDTO;

namespace BISPAPIORA.Repositories.CitizenComplianceServicesRepo
{
    public class CitizenComplianceService : ICitizenComplianceService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public CitizenComplianceService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<CitizenComplianceResponseDTO>> AddCitizenCompliance(AddCitizenComplianceDTO model)
        {
            try
            {
                var citizenCompliance = await db.tbl_citizen_compliances.Where(x => x.fk_citizen.Equals(Guid.Parse(model.fkCitizen))).FirstOrDefaultAsync();
                if (citizenCompliance == null)
                {
                    var newCitizenCompliance = new tbl_citizen_compliance();
                    newCitizenCompliance = _mapper.Map<tbl_citizen_compliance>(model);
                    db.tbl_citizen_compliances.Add(newCitizenCompliance);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen Compliance has been added successfully",
                        data = _mapper.Map<CitizenComplianceResponseDTO>(newCitizenCompliance),
                    };
                }
                else
                {
                    var existingCitizenCompliance = _mapper.Map<UpdateCitizenComplianceDTO>(model);
                    existingCitizenCompliance.citizenComplianceId = citizenCompliance.citizen_compliance_id.ToString();
                    var response = await UpdateCitizenCompliance(existingCitizenCompliance);
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenComplianceResponseDTO>> DeleteCitizenCompliance(string citizenComplianceId)
        {
            try
            {
                var existingCitizenCompliance = await db.tbl_citizen_compliances.Where(x => x.citizen_compliance_id == Guid.Parse(citizenComplianceId)).FirstOrDefaultAsync();
                if (existingCitizenCompliance != null)
                {
                    db.tbl_citizen_compliances.Remove(existingCitizenCompliance);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        remarks = "Citizen Compliance Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<CitizenComplianceResponseDTO>>> GetCitizenCompliancesList()
        {
            try
            {
                var citizenCompliances = await db.tbl_citizen_compliances.Include(x => x.tbl_citizen).Include(x => x.tbl_citizen_scheme).ToListAsync();
                if (citizenCompliances.Count() > 0)
                {
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenComplianceResponseDTO>>(citizenCompliances),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenComplianceResponseDTO>> GetCitizenCompliance(string citizenComplianceId)
        {
            try
            {
                var existingCitizenCompliance = await db.tbl_citizen_compliances.Include(x => x.tbl_citizen).Include(x => x.tbl_citizen_scheme).Where(x => x.citizen_compliance_id == Guid.Parse(citizenComplianceId)).FirstOrDefaultAsync();
                if (existingCitizenCompliance != null)
                {
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        data = _mapper.Map<CitizenComplianceResponseDTO>(existingCitizenCompliance),
                        remarks = "Citizen Compliance found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenComplianceResponseDTO>> UpdateCitizenCompliance(UpdateCitizenComplianceDTO model)
        {
            try
            {
                var existingCitizenCompliance = await db.tbl_citizen_compliances.Include(x => x.tbl_citizen).Include(x => x.tbl_citizen_scheme).Where(x => x.citizen_compliance_id == Guid.Parse(model.citizenComplianceId)).FirstOrDefaultAsync();
                if (existingCitizenCompliance != null)
                {
                    existingCitizenCompliance = _mapper.Map(model, existingCitizenCompliance);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        remarks = $"CitizenCompliance has been updated",
                        data = _mapper.Map<CitizenComplianceResponseDTO>(existingCitizenCompliance),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<CitizenComplianceResponseDTO>>> GetCitizenComplianceByCitizenId(string citizenId)
        {
            try
            {
                var existingDistricts = await db.tbl_citizen_compliances.Include(x => x.tbl_citizen).Include(x => x.tbl_citizen_scheme).Where(x => x.fk_citizen == Guid.Parse(citizenId)).ToListAsync();
                if (existingDistricts != null)
                {
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenComplianceResponseDTO>>(existingDistricts),
                        remarks = "Citizen Compliances found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<CitizenComplianceResponseDTO>>> GetCitizenComplianceByCitizenSchemeId(string citizenSchemeId)
        {
            try
            {
                var existingCitizenCompliances = await db.tbl_citizen_compliances.Include(x => x.tbl_citizen).Include(x => x.tbl_citizen_scheme).Where(x => x.fk_citizen_scheme == Guid.Parse(citizenSchemeId)).ToListAsync();
                if (existingCitizenCompliances != null)
                {
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenComplianceResponseDTO>>(existingCitizenCompliances),
                        remarks = "Citizen Compliances found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
