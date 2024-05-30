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
            this.employmentOtherSpecificationService= employmentOtherSpecificationService;
        }
        //Add Enrolled Citizen Method
        public async Task<ResponseModel<EnrollmentResponseDTO>> AddEnrolledCitizen(AddEnrollmentDTO model)
        {
            try
            {
                //Getting Registered Citizen
                var citizen = await db.tbl_citizens.Where(x => x.citizen_cnic.ToLower().Equals(model.citizenCnic.ToLower())).Include(x=>x.tbl_enrollment).FirstOrDefaultAsync();
                //Check Citizen is Registered or Not
                if (citizen == null)
                {
                    #region Add New Citizen
                    //Add New Citizen
                    var newCitizen = await citizenService.AddEnrolledCitizen(model);
                    #endregion
                    //Check Adding new Citizen Whether Success or Not
                    if (newCitizen.success)
                    {
                        if (newCitizen.data != null)
                        {
                            //Mapping Citizen Id to Request DTO
                            model.fkCitizen = newCitizen.data.citizenId;
                            #region Add New Enrollment
                            var newEnrollment = new tbl_enrollment();
                            //Mapping Required Data To Add Enrollment DTO
                            newEnrollment = _mapper.Map<tbl_enrollment>((model,newCitizen.data.citizenCode));
                            //Add New Enrollment
                            await db.tbl_enrollments.AddAsync(newEnrollment);
                            await db.SaveChangesAsync();
                            #endregion
                            #region Add new Citizen Bank Info
                            //Mapping Required Data To Citizen Bank Info DTO
                            var newRequest = new AddEnrolledCitizenBankInfoDTO();
                            newRequest= _mapper.Map<AddEnrolledCitizenBankInfoDTO>(model);
                            //Add New Bank Info
                            var resposne= await citizenBankInfoService.AddEnrolledCitizenBankInfo(newRequest);
                            #endregion
                            #region Add Bank Other Specification
                            if (!string.IsNullOrEmpty(model.citizenBankOtherSpecification))
                            {
                                //Mapping Required Data to Bank Other Specification DTO
                                var addBankOtherSpecification = new AddEnrolledBankOtherSpecificationDTO();
                                addBankOtherSpecification = _mapper.Map<AddEnrolledBankOtherSpecificationDTO>((model, resposne.data));
                                //Add Bank Other Specification
                                var responseBankOtherSpecification = await bankOtherSpecificationService.AddEnrolledBankOtherSpecification(addBankOtherSpecification);
                            }
                            #endregion
                            #region Add Employment Other Specification
                            if (!string.IsNullOrEmpty(model.citizenEmploymentOtherSpecification))
                            {
                                //Mapping Required Data to Employment Other Specfication DTO
                                var addEmploymentOtherSpecification = new AddEmploymentOtherSpecificationDTO();
                                addEmploymentOtherSpecification = _mapper.Map<AddEmploymentOtherSpecificationDTO>(model);
                                //Add Employment Other Specification
                                var responseEmploymentOtherSpecification = await employmentOtherSpecificationService.AddEmploymentOtherSpecification(addEmploymentOtherSpecification);
                            }
                            #endregion
                            //#region Add Citizen Scheme
                            ////Mapping Required data to Citizen Scheme DTO
                            //var newSchemeReq = new AddCitizenSchemeDTO();
                            //newSchemeReq = _mapper.Map<AddCitizenSchemeDTO>(model);
                            ////Add Citizen Scheme
                            //var schemeResp = citizenSchemeService.AddCitizenScheme(newSchemeReq);
                            //#endregion
                            #region Preparing Response and Returning it
                            //Mapping Citizen To Response DTO
                            var response = _mapper.Map<EnrollmentResponseDTO>(newCitizen.data);
                            response.enrollmentId = newEnrollment.enrollment_id.ToString();                            
                            //Returning Response
                            return new ResponseModel<EnrollmentResponseDTO>()
                            {
                                success = true,
                                remarks = $"Citizen {model.citizenName} has been enrolled successfully",
                                data = response,
                            };
                            #endregion
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
                    //Check Citizen Is Already Enrolled Or Not
                    if (citizen.tbl_enrollment == null)
                    {
                        #region Assigning Or Adjusting Request DTOs
                        //Assigning citizen Id to request model
                        model.fkCitizen = citizen.citizen_id.ToString();
                        //Mapping Request DTO to Update Enrollment DTO
                        var updateModel = _mapper.Map<UpdateEnrollmentDTO>(model);
                        //Mapping Request DTO to Enrollemt Tbl Model
                        var newEnrollment = new tbl_enrollment(); 
                        newEnrollment = _mapper.Map<tbl_enrollment>((model, citizen.id));
                        #endregion
                        #region Add New Enrollment
                        //Add New Enrollment
                        await db.tbl_enrollments.AddAsync(newEnrollment);
                        await db.SaveChangesAsync();
                        #endregion
                        #region Update Citizen
                        //Add Enrollement Id To Update Enrollment DTO
                        updateModel.enrollmentId = newEnrollment.enrollment_id.ToString();
                        //Update the Citizen
                        var newCitizen = await citizenService.UpdateEnrolledCitizen(updateModel);
                        //Check Update Is Success
                        if (newCitizen.success)
                        {
                            if (newCitizen.data != null)
                            {
                                //Mapping Citizen Id to Request DTO
                                model.fkCitizen = newCitizen.data.citizenId;
                                #region Add Citizen Bank Info
                                //Mapping Required Data to Add Bank Info DTO
                                var newRequest = new AddEnrolledCitizenBankInfoDTO();
                                newRequest = _mapper.Map<AddEnrolledCitizenBankInfoDTO>(model);
                                // Add Bank Info
                                var bankResposne = await citizenBankInfoService.AddEnrolledCitizenBankInfo(newRequest);
                                #endregion
                                #region Add Citizen Scheme
                                //Mapping Required Data To Add Citizen Scheme DTO
                                var newSchemeReq = new AddCitizenSchemeDTO();
                                newSchemeReq = _mapper.Map<AddCitizenSchemeDTO>(model);
                                //Add Citizen Scheme
                                var schemeResp = citizenSchemeService.AddCitizenScheme(newSchemeReq);
                                #endregion
                                #region Add Bank Other Specification
                                if (!string.IsNullOrEmpty(model.citizenBankOtherSpecification))
                                {
                                    //Mapping Required Data to Add Bank Other Specification DTO
                                    var addBankOtherSpecification = new AddEnrolledBankOtherSpecificationDTO();
                                    addBankOtherSpecification = _mapper.Map<AddEnrolledBankOtherSpecificationDTO>((model, bankResposne.data));
                                    //Add Bank Other Specification
                                    var responseBankOtherSpecification = await bankOtherSpecificationService.AddEnrolledBankOtherSpecification(addBankOtherSpecification);
                                }
                                #endregion
                                #region Add Employment Other Specification
                                if (!string.IsNullOrEmpty(model.citizenEmploymentOtherSpecification))
                                {
                                    //Mapping Required Data to Employment Other Specification
                                    var addEmploymentOtherSpecification = new AddEmploymentOtherSpecificationDTO();
                                    addEmploymentOtherSpecification = _mapper.Map<AddEmploymentOtherSpecificationDTO>(model);
                                    //Add Employment Other Specification
                                    var responseEmploymentOtherSpecification = await employmentOtherSpecificationService.AddEmploymentOtherSpecification(addEmploymentOtherSpecification);
                                }
                                #endregion
                                #region Send Response
                                //Mapping Citizen Data To Response DTO
                                var response = _mapper.Map<EnrollmentResponseDTO>(newCitizen.data);
                                response.enrollmentId = newEnrollment.enrollment_id.ToString();
                                return new ResponseModel<EnrollmentResponseDTO>()
                                {
                                    success = true,
                                    remarks = $"Citizen {model.citizenName} has been enrolled successfully",
                                    data = response,
                                };
                                #endregion
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
                        #endregion
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
        //Delete Enrollment Method
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
        //Get Enrollment Method By Id
        public async Task<ResponseModel<EnrollmentResponseDTO>> GetEnrollment(string enrollmentId)
        {
            try
            {
                var existingCitizen = await db.tbl_enrollments
                    //where Condition
                    .Where(x => x.enrollment_id == Guid.Parse(enrollmentId))
                    //Joins
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_education)
                    .FirstOrDefaultAsync();
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
