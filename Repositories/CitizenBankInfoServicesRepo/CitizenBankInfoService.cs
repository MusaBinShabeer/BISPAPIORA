using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Repositories.CitizenBankInfoServicesRepo
{
    public class CitizenBankInfoService : ICitizenBankInfoService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public CitizenBankInfoService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        #region Registered Citizen Bank Info 
        public async Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> AddRegisteredCitizenBankInfo(AddRegisteredCitizenBankInfoDTO model)
        {
            try
            {
                var citizenBankInfo = await db.tbl_citizen_bank_infos.Where(x => x.fk_citizen.Equals(Guid.Parse(model.fkCitizen)) && x.fk_bank.Equals(Guid.Parse(model.fkBank))).FirstOrDefaultAsync();
                if (citizenBankInfo == null)
                {
                    var newCitizenBankInfo = new tbl_citizen_bank_info();
                    newCitizenBankInfo = _mapper.Map<tbl_citizen_bank_info>(model);
                    db.tbl_citizen_bank_infos.Add(newCitizenBankInfo);
                    await db.SaveChangesAsync();
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        success = true,
                        remarks = $"CitizenBankInfo has been added successfully",
                        data = _mapper.Map<RegisteredCitizenBankInfoResponseDTO>(newCitizenBankInfo),
                    };
                }
                else
                {
                    citizenBankInfo=_mapper.Map(model, citizenBankInfo);
                    await db.SaveChangesAsync();
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        remarks = $"CitizenBankInfo has been added successfully",
                        data = _mapper.Map<RegisteredCitizenBankInfoResponseDTO>(citizenBankInfo),
                        success = true,
                    };                   
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> DeleteRegisteredCitizenBankInfo(string CitizenBankInfoId)
        {
            try
            {
                var existingCitizenBankInfo = await db.tbl_citizen_bank_infos.Where(x => x.citizen_bank_info_id == Guid.Parse(CitizenBankInfoId)).FirstOrDefaultAsync();
                if (existingCitizenBankInfo != null)
                {
                    db.tbl_citizen_bank_infos.Remove(existingCitizenBankInfo);
                    await db.SaveChangesAsync();
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        remarks = "CitizenBankInfo Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<RegisteredCitizenBankInfoResponseDTO>>> GetRegisteredCitizenBankInfosList()
        {
            try
            {
                var citizenBankInfos = await db.tbl_citizen_bank_infos.Include(x => x.tbl_citizen).Include(x => x.tbl_bank).ToListAsync();
                if (citizenBankInfos.Count() > 0)
                {
                    return new ResponseModel<List<RegisteredCitizenBankInfoResponseDTO>>()
                    {
                        data = _mapper.Map<List<RegisteredCitizenBankInfoResponseDTO>>(citizenBankInfos),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<RegisteredCitizenBankInfoResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<RegisteredCitizenBankInfoResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> GetRegisteredCitizenBankInfo(string citizenBankInfoId)
        {
            try
            {
                var existingCitizenBankInfo = await db.tbl_citizen_bank_infos.Include(x => x.tbl_citizen).Include(x => x.tbl_bank).Where(x => x.citizen_bank_info_id == Guid.Parse(citizenBankInfoId)).FirstOrDefaultAsync();
                if (existingCitizenBankInfo != null)
                {
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        data = _mapper.Map<RegisteredCitizenBankInfoResponseDTO>(existingCitizenBankInfo),
                        remarks = "CitizenBankInfo found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> UpdateRegisteredCitizenBankInfo(UpdateRegisteredCitizenBankInfoDTO model)
        {
            try
            {
                var existingCitizenBankInfo = await db.tbl_citizen_bank_infos.Where(x => x.citizen_bank_info_id == Guid.Parse(model.CitizenBankInfoId)).FirstOrDefaultAsync();
                if (existingCitizenBankInfo != null)
                {
                    existingCitizenBankInfo = _mapper.Map(model, existingCitizenBankInfo);
                    await db.SaveChangesAsync();
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        remarks = $"CitizenBankInfo has been added successfully",
                        data = _mapper.Map<RegisteredCitizenBankInfoResponseDTO>(existingCitizenBankInfo),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        #endregion
        #region Enrolled Citizen Bank Info
        public async Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> AddEnrolledCitizenBankInfo(AddEnrolledCitizenBankInfoDTO model)
        {
            try
            {
                var citizenBankInfo = await db.tbl_citizen_bank_infos.Where(x => x.fk_citizen.Equals(Guid.Parse(model.fkCitizen)) && x.fk_bank.Equals(Guid.Parse(model.fkBank))).FirstOrDefaultAsync();
                if (citizenBankInfo == null)
                {
                    var newCitizenBankInfo = new tbl_citizen_bank_info();
                    newCitizenBankInfo = _mapper.Map<tbl_citizen_bank_info>(model);
                    db.tbl_citizen_bank_infos.Add(newCitizenBankInfo);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        success = true,
                        remarks = $"CitizenBankInfo has been added successfully",
                        data = _mapper.Map<EnrolledCitizenBankInfoResponseDTO>(newCitizenBankInfo),
                    };
                }
                else
                {
                    var updateModel= _mapper.Map<UpdateEnrolledCitizenBankInfoDTO>(model);
                    updateModel.CitizenBankInfoId = citizenBankInfo.citizen_bank_info_id.ToString();
                    var response = await UpdateEnrolledCitizenBankInfo(updateModel);
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> DeleteEnrolledCitizenBankInfo(string CitizenBankInfoId)
        {
            try
            {
                var existingCitizenBankInfo = await db.tbl_citizen_bank_infos.Where(x => x.citizen_bank_info_id == Guid.Parse(CitizenBankInfoId)).FirstOrDefaultAsync();
                if (existingCitizenBankInfo != null)
                {
                    db.tbl_citizen_bank_infos.Remove(existingCitizenBankInfo);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        remarks = "CitizenBankInfo Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<EnrolledCitizenBankInfoResponseDTO>>> GetEnrolledCitizenBankInfosList()
        {
            try
            {
                var citizenBankInfos = await db.tbl_citizen_bank_infos.Include(x => x.tbl_citizen).Include(x => x.tbl_bank).ToListAsync();
                if (citizenBankInfos.Count() > 0)
                {
                    return new ResponseModel<List<EnrolledCitizenBankInfoResponseDTO>>()
                    {
                        data = _mapper.Map<List<EnrolledCitizenBankInfoResponseDTO>>(citizenBankInfos),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<EnrolledCitizenBankInfoResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<EnrolledCitizenBankInfoResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> GetEnrolledCitizenBankInfo(string citizenBankInfoId)
        {
            try
            {
                var existingCitizenBankInfo = await db.tbl_citizen_bank_infos.Include(x => x.tbl_citizen).Include(x => x.tbl_bank).Where(x => x.citizen_bank_info_id == Guid.Parse(citizenBankInfoId)).FirstOrDefaultAsync();
                if (existingCitizenBankInfo != null)
                {
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        data = _mapper.Map<EnrolledCitizenBankInfoResponseDTO>(existingCitizenBankInfo),
                        remarks = "CitizenBankInfo found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> UpdateEnrolledCitizenBankInfo(UpdateEnrolledCitizenBankInfoDTO model)
        {
            try
            {
                var existingCitizenBankInfo = await db.tbl_citizen_bank_infos.Where(x => x.citizen_bank_info_id == Guid.Parse(model.CitizenBankInfoId)).FirstOrDefaultAsync();
                if (existingCitizenBankInfo != null)
                {
                    existingCitizenBankInfo = _mapper.Map(model, existingCitizenBankInfo);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        remarks = $"CitizenBankInfo has been added successfully",
                        data = _mapper.Map<EnrolledCitizenBankInfoResponseDTO>(existingCitizenBankInfo),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        #endregion
    }
}
