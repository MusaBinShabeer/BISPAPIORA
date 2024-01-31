using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenThumbPrintDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.DistrictDTO;

namespace BISPAPIORA.Repositories.CitizenThumbPrintServicesRepo
{
    public class CitizenThumbPrintService : ICitizenThumbPrintService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public CitizenThumbPrintService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<CitizenThumbPrintResponseDTO>> AddCitizenThumbPrint(AddCitizenThumbPrintDTO model)
        {
            try
            {
                var bank = await db.tbl_citizen_thumb_prints.Where(x => x.citizen_thumb_print_name.ToLower().Equals(model.citizenThumbPrintName.ToLower())).FirstOrDefaultAsync();
                if (bank == null)
                {
                    var newCitizenThumbPrint = new tbl_citizen_thumb_print();
                    newCitizenThumbPrint = _mapper.Map<tbl_citizen_thumb_print>(model);
                    db.tbl_citizen_thumb_prints.Add(newCitizenThumbPrint);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenThumbPrintResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen Thumb Print {model.citizenThumbPrintName} has been added successfully",
                        data = _mapper.Map<CitizenThumbPrintResponseDTO>(newCitizenThumbPrint),
                    };
                }
                else
                {
                    return new ResponseModel<CitizenThumbPrintResponseDTO>()
                    {
                        success = false,
                        remarks = $"Citizen Thumb Print with name {model.citizenThumbPrintName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenThumbPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenThumbPrintResponseDTO>> DeleteCitizenThumbPrint(string bankId)
        {
            try
            {
                var existingCitizenThumbPrint = await db.tbl_citizen_thumb_prints.Where(x => x.citizen_thumb_print_id == Guid.Parse(bankId)).FirstOrDefaultAsync();
                if (existingCitizenThumbPrint != null)
                {
                    db.tbl_citizen_thumb_prints.Remove(existingCitizenThumbPrint);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenThumbPrintResponseDTO>()
                    {
                        remarks = "Citizen Thumb Print Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenThumbPrintResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenThumbPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<CitizenThumbPrintResponseDTO>>> GetCitizenThumbPrintsList()
        {
            try
            {
                var citizenThumbPrints = await db.tbl_citizen_thumb_prints.Include(x => x.tbl_citizen).ToListAsync();
                if (citizenThumbPrints.Count() > 0)
                {
                    return new ResponseModel<List<CitizenThumbPrintResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenThumbPrintResponseDTO>>(citizenThumbPrints),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<CitizenThumbPrintResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<CitizenThumbPrintResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenThumbPrintResponseDTO>> GetCitizenThumbPrint(string citizenThumbPrintId)
        {
            try
            {
                var existingCitizenThumbPrint = await db.tbl_citizen_thumb_prints.Include(x => x.tbl_citizen).Where(x => x.citizen_thumb_print_id == Guid.Parse(citizenThumbPrintId)).FirstOrDefaultAsync();
                if (existingCitizenThumbPrint != null)
                {
                    return new ResponseModel<CitizenThumbPrintResponseDTO>()
                    {
                        data = _mapper.Map<CitizenThumbPrintResponseDTO>(existingCitizenThumbPrint),
                        remarks = "Citizen Thumb Print found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenThumbPrintResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenThumbPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenThumbPrintResponseDTO>> UpdateCitizenThumbPrint(UpdateCitizenThumbPrintDTO model)
        {
            try
            {
                var existingCitizenThumbPrint = await db.tbl_citizen_thumb_prints.Include(x => x.tbl_citizen).Where(x => x.citizen_thumb_print_id == Guid.Parse(model.citizenThumbPrintId)).FirstOrDefaultAsync();
                if (existingCitizenThumbPrint != null)
                {
                    existingCitizenThumbPrint = _mapper.Map(model, existingCitizenThumbPrint);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenThumbPrintResponseDTO>()
                    {
                        remarks = $"Citizen Thumb Print: {model.citizenThumbPrintName} has been updated",
                        data = _mapper.Map<CitizenThumbPrintResponseDTO>(existingCitizenThumbPrint),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenThumbPrintResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenThumbPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<CitizenThumbPrintResponseDTO>>> GetCitizenThumbPrintByCitizenId(string citizenId)
        {
            try
            {
                var existingDistricts = await db.tbl_citizen_thumb_prints.Include(x => x.tbl_citizen).Where(x => x.fk_citizen == Guid.Parse(citizenId)).ToListAsync();
                if (existingDistricts != null)
                {
                    return new ResponseModel<List<CitizenThumbPrintResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenThumbPrintResponseDTO>>(existingDistricts),
                        remarks = "Citizen Thumb Print found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<CitizenThumbPrintResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<CitizenThumbPrintResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
