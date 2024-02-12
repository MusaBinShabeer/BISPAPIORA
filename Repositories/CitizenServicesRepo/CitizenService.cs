﻿using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;
using BISPAPIORA.Repositories.CitizenAttachmentServicesRepo;
using BISPAPIORA.Repositories.CitizenThumbPrintServicesRepo;
using BISPAPIORA.Models.DTOS.CitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.CitizenThumbPrintDTO;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Repositories.CitizenBankInfoServicesRepo;

namespace BISPAPIORA.Repositories.CitizenServicesRepo
{
    public class CitizenService : ICitizenService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        private readonly ICitizenAttachmentService attachmentService;
        private readonly ICitizenBankInfoService citizenBankInfoService;
        private readonly ICitizenThumbPrintService thumbprintService;
        public CitizenService(IMapper mapper, Dbcontext db,ICitizenBankInfoService citizenBankInfoService,ICitizenThumbPrintService thumbPrintService, ICitizenAttachmentService citizenAttachmentService)
        {
            _mapper = mapper;
            this.db = db;
            this.attachmentService = citizenAttachmentService;
            this.thumbprintService = thumbPrintService;
            this.citizenBankInfoService = citizenBankInfoService;
        }
        #region Registered Citizen
        public async Task<ResponseModel<RegistrationResponseDTO>> AddRegisteredCitizen(AddRegistrationDTO model)
        {
            try
            {
                var Citizen = await db.tbl_citizens.Where(x => x.citizen_cnic.ToLower().Equals(model.citizenCnic.ToLower())).FirstOrDefaultAsync();
                if (Citizen == null)
                {
                    var newCitizen = new tbl_citizen();
                    newCitizen = _mapper.Map<tbl_citizen>(model);
                    db.tbl_citizens.Add(newCitizen);
                    await db.SaveChangesAsync();
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen {model.citizenName} has been added successfully",
                        data = _mapper.Map<RegistrationResponseDTO>(newCitizen),
                    };
                }
                else
                {
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = false,
                        remarks = $"Citizen with name {model.citizenName} already exists",
                        data = _mapper.Map<RegistrationResponseDTO>(Citizen),
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<RegistrationResponseDTO>> AddRegisteredDBFCitizen(AddRegistrationDTO model)
        {
            try
            {
                var Citizen = await db.HiberProtectionAccounts.Where(x => x.Cnic.ToString().Equals(model.citizenCnic.ToLower())).FirstOrDefaultAsync();
                if (Citizen == null)
                {
                    var newCitizen = new HiberProtectionAccount();
                    newCitizen = _mapper.Map<HiberProtectionAccount>(model);
                    db.HiberProtectionAccounts.Add(newCitizen);
                    await db.SaveChangesAsync();
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen {model.citizenName} has been added successfully",
                        data = _mapper.Map<RegistrationResponseDTO>(newCitizen),
                    };
                }
                else
                {
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = false,
                        remarks = $"Citizen with name {model.citizenName} already exists",
                        data = _mapper.Map<RegistrationResponseDTO>(Citizen),
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<RegistrationResponseDTO>> UpdateRegisteredCitizen(UpdateRegistrationDTO model)
        {
            try
            {
                var existingCitizen = await db.tbl_citizens.Where(x => x.citizen_id == Guid.Parse(model.fkCitizen)).FirstOrDefaultAsync();
                if (existingCitizen != null)
                {
                    existingCitizen = _mapper.Map(model, existingCitizen);
                    await db.SaveChangesAsync();
                    var AddBankInfoRequest = new AddRegisteredCitizenBankInfoDTO();
                    AddBankInfoRequest= _mapper.Map<AddRegisteredCitizenBankInfoDTO>(model);
                    var bankInfo = citizenBankInfoService.AddRegisteredCitizenBankInfo(AddBankInfoRequest);
                    model.citizenCnic = existingCitizen.citizen_cnic; 
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = $"Citizen: {model.citizenName} has been updated",
                        data = _mapper.Map<RegistrationResponseDTO>(existingCitizen),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<RegistrationResponseDTO>> UpdateRegisteredDBFCitizen(UpdateRegistrationDTO model)
        {
            try
            {
                var existingCitizen = await db.HiberProtectionAccounts.Where(x => x.Cnic == decimal.Parse(model.citizenCnic)).FirstOrDefaultAsync();
                if (existingCitizen != null)
                {
                    existingCitizen = _mapper.Map(model, existingCitizen);
                    await db.SaveChangesAsync();
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = $"Citizen: {model.citizenName} has been updated",
                        data = _mapper.Map<RegistrationResponseDTO>(existingCitizen),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<RegistrationResponseDTO>>> GetRegisteredCitizensList()
        {
            try
            {
                var registerdCitizens = await db.tbl_registrations.Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province).Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_employment).Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_education).Include(x=>x.tbl_citizen.tbl_citizen_bank_info).ThenInclude(x=>x.tbl_bank).Include(x=>x.tbl_citizen.tbl_bank_other_specification).Include(x=>x.tbl_citizen.tbl_citizen_registration).Select(x => x.tbl_citizen).ToListAsync();
                if (registerdCitizens.Count() > 0)
                {
                    return new ResponseModel<List<RegistrationResponseDTO>>()
                    {
                        data = _mapper.Map<List<RegistrationResponseDTO>>(registerdCitizens),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<RegistrationResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<RegistrationResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
       
        #region Enrolled Citizen
        public async Task<ResponseModel<EnrollmentResponseDTO>> AddEnrolledCitizen(AddEnrollmentDTO model)
        {
            try
            {
                var Citizen = await db.tbl_citizens.Where(x => x.citizen_cnic.ToLower().Equals(model.citizenCnic.ToLower())).FirstOrDefaultAsync();
                if (Citizen == null)
                {
                    var newCitizen = new tbl_citizen();
                    newCitizen = _mapper.Map<tbl_citizen>(model);
                    await db.tbl_citizens.AddAsync(newCitizen);
                    await db.SaveChangesAsync();
                    var newAttachmentDto = new AddCitizenAttachmentDTO()
                    {
                        citizenAttachmentName = model.fileName,
                        citizenAttachmentPath = model.filePath,
                        fkCitizen = newCitizen.citizen_id.ToString()
                    };
                    var resposneOfattachment = await attachmentService.AddCitizenAttachment(newAttachmentDto);

                    var newthumbPrintDto = new AddCitizenThumbPrintDTO()
                    {
                        citizenThumbPrintName = model.fileName,
                        citizenThumbPrintPath = model.filePath,
                        fkCitizen = newCitizen.citizen_id.ToString()
                    };
                    var responseOfThumbPrint = await thumbprintService.AddCitizenThumbPrint(newthumbPrintDto);
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen {model.citizenName} has been added successfully",
                        data = _mapper.Map<EnrollmentResponseDTO>(newCitizen),
                    };
                }
                else
                {
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen with cnic {model.citizenCnic} already exists",
                        data = _mapper.Map<EnrollmentResponseDTO>(Citizen),
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrollmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EnrollmentResponseDTO>> UpdateEnrolledCitizen(UpdateEnrollmentDTO model)
        {
            try
            {
                var existingCitizen = await db.tbl_citizens.Where(x => x.citizen_id == Guid.Parse(model.fkCitizen)).FirstOrDefaultAsync();
                if (existingCitizen != null)
                {
                    existingCitizen = _mapper.Map(model, existingCitizen);
                    await db.SaveChangesAsync();
                    //model.citizenCnic = existingCitizen.citizen_cnic;
                    //await UpdateEnrolledDBFCitizen(model);
                    var newAttachmentDto = new AddCitizenAttachmentDTO()
                    {
                        citizenAttachmentName = model.fileName,
                        citizenAttachmentPath = model.filePath,
                        fkCitizen = existingCitizen.citizen_id.ToString()
                    };
                    var resposneOfattachment = await attachmentService.AddCitizenAttachment(newAttachmentDto);

                    var newthumbPrintDto = new AddCitizenThumbPrintDTO()
                    {
                        citizenThumbPrintName = model.fileName,
                        citizenThumbPrintPath = model.filePath,
                        fkCitizen = existingCitizen.citizen_id.ToString()
                    };
                    var responseOfThumbPrint = await thumbprintService.AddCitizenThumbPrint(newthumbPrintDto);
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        remarks = $"Citizen: {model.citizenName} has been updated",
                        data = _mapper.Map<EnrollmentResponseDTO>(existingCitizen),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrollmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EnrollmentResponseDTO>> UpdateEnrolledDBFCitizen(UpdateEnrollmentDTO model)
        {
            try
            {
                var existingCitizen = await db.HiberProtectionAccounts.Where(x => x.Cnic == decimal.Parse(model.citizenCnic)).FirstOrDefaultAsync();
                if (existingCitizen != null)
                {
                    existingCitizen = _mapper.Map(model, existingCitizen);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        remarks = $"Citizen: {model.citizenName} has been updated",
                        data = _mapper.Map<EnrollmentResponseDTO>(existingCitizen),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrollmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<EnrollmentResponseDTO>>> GetEnrolledCitizensList()
        {
            try
            {
                var enrolledCitizens = await db.tbl_enrollments
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_education)
                    .Include(x=>x.tbl_citizen).ThenInclude(x=>x.tbl_citizen_scheme)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_bank_info).ThenInclude(x=>x.tbl_bank).ToListAsync();
                if (enrolledCitizens.Count() > 0)
                {
                    var resposne = _mapper.Map<List<EnrollmentResponseDTO>>(enrolledCitizens);
                    return new ResponseModel<List<EnrollmentResponseDTO>>()
                    {
                        data = resposne,
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<EnrollmentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<EnrollmentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        #endregion
        #endregion
        public async Task<ResponseModel<RegistrationResponseDTO>> DeleteCitizen(string CitizenId)
        {
            try
            {
                var existingCitizen = await db.tbl_citizens.Where(x => x.citizen_id == Guid.Parse(CitizenId)).FirstOrDefaultAsync();
                if (existingCitizen != null)
                {
                    db.tbl_citizens.Remove(existingCitizen);
                    await db.SaveChangesAsync();
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = "Citizen Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<RegistrationResponseDTO>> GetCitizen(string CitizenId)
        {
            try
            {
                var existingCitizen = await db.tbl_citizens.Where(x => x.citizen_id == Guid.Parse(CitizenId)).FirstOrDefaultAsync();
                if (existingCitizen != null)
                {
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        data = _mapper.Map<RegistrationResponseDTO>(existingCitizen),
                        remarks = "Citizen found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<RegistrationResponseDTO>> VerifyCitizenRegistrationWithCNIC(string citizenCnic)
        {
            try
            {
                var existingCitizen = await db.tbl_citizens.Where(x => x.citizen_cnic == citizenCnic)
                .Include(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                .Include(x => x.tbl_citizen_employment)
                .Include(x => x.tbl_citizen_education)
                .Include(x => x.tbl_citizen_scheme)
                .Include(x=>x.tbl_citizen_registration)
                .Include(x=>x.tbl_enrollment)
                .Include(x => x.tbl_citizen_bank_info).ThenInclude(x => x.tbl_bank).FirstOrDefaultAsync();
                if (existingCitizen != null)
                {
                    var response = _mapper.Map<RegistrationResponseDTO>(existingCitizen);
                    if (existingCitizen.tbl_enrollment != null)
                    {
                        response.isEnrolled = true;
                    }
                    if (existingCitizen.tbl_citizen_registration != null)
                    {
                        response.isRegisteered = true;
                    }
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        data = response,
                        remarks = "Citizen found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                            
                        remarks = "No Record",
                        success = true,
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<RegistrationResponseDTO>> GetRegisteredCitizenByCnic(string citizenCnic)
        {
            try
            {
                var existingCitizen = await db.tbl_citizens.Where(x => x.citizen_cnic == citizenCnic)
                .Include(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                .Include(x => x.tbl_citizen_employment)
                .Include(x => x.tbl_citizen_education)
                .Include(x => x.tbl_citizen_scheme)
                .Include(x => x.tbl_citizen_registration)
                .Include(x => x.tbl_enrollment)
                .Include(x => x.tbl_citizen_bank_info).ThenInclude(x => x.tbl_bank).FirstOrDefaultAsync();
                if (existingCitizen != null)
                {
                    var response = _mapper.Map<RegistrationResponseDTO>(existingCitizen);
                    if (existingCitizen.tbl_enrollment != null)
                    {
                        response.isEnrolled = true;
                    }
                    if (existingCitizen.tbl_citizen_registration != null)
                    {
                        response.isRegisteered = true;
                    }
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        data = response,
                        remarks = "Citizen found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<RegistrationResponseDTO>()
                    {

                        remarks = "Applicant Not Registered",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EnrollmentResponseDTO>> GetEnrolledCitizenByCnic(string citizenCnic)
        {
            try
            {
                var existingRPFCitizen = await db.HiberProtectionAccounts.Where(x => x.Cnic == Decimal.Parse(citizenCnic)).FirstOrDefaultAsync();
                if (existingRPFCitizen != null)
                {
                    var existingCitizen = await db.tbl_citizens.Where(x => x.citizen_cnic == citizenCnic).FirstOrDefaultAsync();
                    if (existingCitizen != null)
                    {
                        return new ResponseModel<EnrollmentResponseDTO>()
                        {
                            data = _mapper.Map<EnrollmentResponseDTO>(existingCitizen),
                            remarks = "Citizen found successfully",
                            success = true,
                        };
                    }
                    else
                    {
                        return new ResponseModel<EnrollmentResponseDTO>()
                        {
                            data = _mapper.Map<EnrollmentResponseDTO>(existingRPFCitizen),
                            remarks = "Citizen found successfully",
                            success = true,
                        };
                    }
                }
                else
                {
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrollmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
