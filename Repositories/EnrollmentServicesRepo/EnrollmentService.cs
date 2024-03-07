using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenServicesRepo;
using Microsoft.EntityFrameworkCore;
using BISPAPIORA.Repositories.CitizenBankInfoServicesRepo;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Repositories.CitizenSchemeServicesRepo;
using BISPAPIORA.Models.DTOS.CitizenSchemeDTO;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;
using BISPAPIORA.Repositories.BankOtherSpecificationServicesRepo;
using BISPAPIORA.Models.DTOS.EmploymentOtherSpecificationDTO;
using BISPAPIORA.Repositories.EmploymentOtherSpecificationServicesRepo;

namespace BISPAPIORA.Repositories.EnrollmentServicesRepo
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IMapper _mapper;
        private readonly ICitizenService citizenService;
        private readonly Dbcontext db;
        private readonly ICitizenBankInfoService citizenBankInfoService;
        private readonly ICitizenSchemeService citizenSchemeService;
        private readonly IBankOtherSpecificationService bankOtherSpecificationService;
        private readonly IEmploymentOtherSpecificationService employmentOtherSpecificationService;

        public EnrollmentService(IMapper mapper,IEmploymentOtherSpecificationService employmentOtherSpecificationService, IBankOtherSpecificationService bankOtherSpecificationService, Dbcontext db, ICitizenService citizenService,ICitizenBankInfoService citizenBankInfoService,ICitizenSchemeService citizenSchemeService)
        {
            _mapper = mapper;
            this.db = db;
            this.citizenService = citizenService;
            this.citizenBankInfoService= citizenBankInfoService;
            this.citizenSchemeService = citizenSchemeService;
            this.bankOtherSpecificationService= bankOtherSpecificationService;
        }
        public async Task<ResponseModel<EnrollmentResponseDTO>> AddEnrolledCitizen(AddEnrollmentDTO model)
        {
            try
            {
                var citizen = await db.tbl_citizens.Where(x => x.citizen_cnic.ToLower().Equals(model.citizenCnic.ToLower())).Include(x=>x.tbl_enrollment).FirstOrDefaultAsync();
                if (citizen == null)
                {
                    var newCitizen = await citizenService.AddEnrolledCitizen(model);
                    if (newCitizen.success)
                    {
                        if (newCitizen.data != null)
                        {
                            model.fkCitizen = newCitizen.data.citizenId;                          
                            var newEnrollment = new tbl_enrollment();
                            newEnrollment = _mapper.Map<tbl_enrollment>((model,newCitizen.data.citizenCode));
                            await db.tbl_enrollments.AddAsync(newEnrollment);
                            await db.SaveChangesAsync();
                            var newRequest = new AddEnrolledCitizenBankInfoDTO();
                            newRequest= _mapper.Map<AddEnrolledCitizenBankInfoDTO>(model);
                            var resposne= citizenBankInfoService.AddEnrolledCitizenBankInfo(newRequest);
                            if (!string.IsNullOrEmpty(model.citizenBankOtherSpecification))
                            {
                                var addBankOtherSpecification = new AddEnrolledBankOtherSpecificationDTO();
                                addBankOtherSpecification = _mapper.Map<AddEnrolledBankOtherSpecificationDTO>((model, resposne));
                                var responseBankOtherSpecification = await bankOtherSpecificationService.AddEnrolledBankOtherSpecification(addBankOtherSpecification);
                            }
                            if (!string.IsNullOrEmpty(model.citizenEmploymentOtherSpecification))
                            {
                                var addEmploymentOtherSpecification = new AddEmploymentOtherSpecificationDTO();
                                addEmploymentOtherSpecification = _mapper.Map<AddEmploymentOtherSpecificationDTO>(model);
                                var responseEmploymentOtherSpecification = await employmentOtherSpecificationService.AddEmploymentOtherSpecification(addEmploymentOtherSpecification);
                            }
                            var newSchemeReq= new AddCitizenSchemeDTO();
                            newSchemeReq = _mapper.Map<AddCitizenSchemeDTO>(model);
                            var schemeResp = citizenSchemeService.AddCitizenScheme(newSchemeReq);
                            var response = _mapper.Map<EnrollmentResponseDTO>(newCitizen.data);
                            response.enrollmentId = newEnrollment.enrollment_id.ToString();
                            return new ResponseModel<EnrollmentResponseDTO>()
                            {
                                success = true,
                                remarks = $"Citizen {model.citizenName} has been enrolled successfully",
                                data = response,
                            };
                        }
                        else
                        {
                            return new ResponseModel<EnrollmentResponseDTO>()
                            {
                                success = true,
                                remarks = $"Citizen {model.citizenName} has been not been enrolled successfully",
                            };
                        }
                    }
                    else
                    {
                        return new ResponseModel<EnrollmentResponseDTO>()
                        {
                            success = true,
                            remarks = newCitizen.remarks
                        };
                    }
                }
                else
                {
                    if (citizen.tbl_enrollment == null)
                    {
                        model.fkCitizen = citizen.citizen_id.ToString();
                        var updateModel = _mapper.Map<UpdateEnrollmentDTO>(model);
                        var newEnrollment = new tbl_enrollment(); 
                        newEnrollment = _mapper.Map<tbl_enrollment>((model, citizen.id));
                        await db.tbl_enrollments.AddAsync(newEnrollment);
                        await db.SaveChangesAsync();
                        updateModel.enrollmentId = newEnrollment.enrollment_id.ToString();
                        var newCitizen = await citizenService.UpdateEnrolledCitizen(updateModel);
                        if (newCitizen.success)
                        {
                            if (newCitizen.data != null)
                            {
                                model.fkCitizen = newCitizen.data.citizenId;                                
                                var newRequest = new AddEnrolledCitizenBankInfoDTO();
                                newRequest = _mapper.Map<AddEnrolledCitizenBankInfoDTO>(model);
                                var bankResposne = await citizenBankInfoService.AddEnrolledCitizenBankInfo(newRequest);
                                var newSchemeReq = new AddCitizenSchemeDTO();
                                newSchemeReq = _mapper.Map<AddCitizenSchemeDTO>(model);
                                var schemeResp = citizenSchemeService.AddCitizenScheme(newSchemeReq);
                                var response = _mapper.Map<EnrollmentResponseDTO>(newCitizen.data);
                                response.enrollmentId = newEnrollment.enrollment_id.ToString();
                                if (!string.IsNullOrEmpty(model.citizenBankOtherSpecification))
                                {
                                    var addBankOtherSpecification = new AddEnrolledBankOtherSpecificationDTO();
                                    addBankOtherSpecification = _mapper.Map<AddEnrolledBankOtherSpecificationDTO>((model, bankResposne.data));
                                    var responseBankOtherSpecification = await bankOtherSpecificationService.AddEnrolledBankOtherSpecification(addBankOtherSpecification);
                                }
                                if (!string.IsNullOrEmpty(model.citizenEmploymentOtherSpecification))
                                {
                                    var addEmploymentOtherSpecification = new AddEmploymentOtherSpecificationDTO();
                                    addEmploymentOtherSpecification = _mapper.Map<AddEmploymentOtherSpecificationDTO>(model);
                                    var responseEmploymentOtherSpecification = await employmentOtherSpecificationService.AddEmploymentOtherSpecification(addEmploymentOtherSpecification);
                                }
                                return new ResponseModel<EnrollmentResponseDTO>()
                                {
                                    success = true,
                                    remarks = $"Citizen {model.citizenName} has been enrolled successfully",
                                    data = response,
                                };
                            }
                            else
                            {
                                return new ResponseModel<EnrollmentResponseDTO>()
                                {
                                    success = false,
                                    remarks = $"Citizen {model.citizenName} has been not been enrolled successfully",
                                };
                            }
                        }
                        else
                        {
                            return new ResponseModel<EnrollmentResponseDTO>()
                            {
                                success = false,
                                remarks = newCitizen.remarks
                            };
                        }
                    }
                    else
                    {
                        return new ResponseModel<EnrollmentResponseDTO>()
                        {
                            success = false,
                            remarks = "Already Enrolled"
                        };
                    }
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
        public async Task<ResponseModel<EnrollmentResponseDTO>> DeleteEnrollment(string enrollmentId)
        {
            try
            {
                var registration = await db.tbl_enrollments.Where(x => x.enrollment_id == Guid.Parse(enrollmentId)).FirstOrDefaultAsync();
                if (registration != null)
                {
                    db.tbl_enrollments.Remove(registration);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        remarks = "Enrollment Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
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
        public async Task<ResponseModel<EnrollmentResponseDTO>> GetEnrollment(string enrollmentId)
        {
            try
            {
                var existingCitizen = await db.tbl_enrollments.Where(x => x.enrollment_id == Guid.Parse(enrollmentId)).Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province).Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_employment).Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_education).FirstOrDefaultAsync();
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
