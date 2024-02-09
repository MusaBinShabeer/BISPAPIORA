using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.CitizenSchemeDTO;

namespace BISPAPIORA.Repositories.CitizenSchemeServicesRepo
{
    public class CitizenSchemeService : ICitizenSchemeService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public CitizenSchemeService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<CitizenSchemeResponseDTO>> AddCitizenScheme(AddCitizenSchemeDTO model)
        {
            try
            {
                var citizenScheme = await db.tbl_citizen_schemes.Where(x => x.fk_citizen.Equals(Guid.Parse(model.fkCitizen))).FirstOrDefaultAsync();
                if (citizenScheme == null)
                {
                    var newCitizenScheme = new tbl_citizen_scheme();
                    newCitizenScheme = _mapper.Map<tbl_citizen_scheme>(model);
                    db.tbl_citizen_schemes.Add(newCitizenScheme);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen Scheme has been added successfully",
                        data = _mapper.Map<CitizenSchemeResponseDTO>(newCitizenScheme),
                    };
                }
                else
                {
                    var existingCitizenScheme = _mapper.Map<UpdateCitizenSchemeDTO>(model);
                    existingCitizenScheme.citizenSchemeId = citizenScheme.citizen_scheme_id.ToString();
                    var response = await UpdateCitizenScheme(existingCitizenScheme);
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenSchemeResponseDTO>> DeleteCitizenScheme(string citizenSchemeId)
        {
            try
            {
                var existingCitizenScheme = await db.tbl_citizen_schemes.Where(x => x.citizen_scheme_id == Guid.Parse(citizenSchemeId)).FirstOrDefaultAsync();
                if (existingCitizenScheme != null)
                {
                    db.tbl_citizen_schemes.Remove(existingCitizenScheme);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        remarks = "Citizen Scheme Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<CitizenSchemeResponseDTO>>> GetCitizenSchemesList()
        {
            try
            {
                var citizenSchemes = await db.tbl_citizen_schemes.ToListAsync();
                if (citizenSchemes.Count() > 0)
                {
                    return new ResponseModel<List<CitizenSchemeResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenSchemeResponseDTO>>(citizenSchemes),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<CitizenSchemeResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<CitizenSchemeResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenSchemeResponseDTO>> GetCitizenScheme(string citizenSchemeId)
        {
            try
            {
                var existingCitizenScheme = await db.tbl_citizen_schemes.Where(x => x.citizen_scheme_id == Guid.Parse(citizenSchemeId)).FirstOrDefaultAsync();
                if (existingCitizenScheme != null)
                {
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        data = _mapper.Map<CitizenSchemeResponseDTO>(existingCitizenScheme),
                        remarks = "Citizen Scheme found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenSchemeResponseDTO>> UpdateCitizenScheme(UpdateCitizenSchemeDTO model)
        {
            try
            {
                var existingCitizenScheme = await db.tbl_citizen_schemes.Where(x => x.citizen_scheme_id == Guid.Parse(model.citizenSchemeId)).FirstOrDefaultAsync();
                if (existingCitizenScheme != null)
                {
                    existingCitizenScheme = _mapper.Map(model, existingCitizenScheme);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        remarks = $"Citizen Scheme has been updated",
                        data = _mapper.Map<CitizenSchemeResponseDTO>(existingCitizenScheme),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
