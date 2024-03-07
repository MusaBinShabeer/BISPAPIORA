using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenServicesRepo;
using Microsoft.EntityFrameworkCore;
using BISPAPIORA.Repositories.BankServicesRepo;
using BISPAPIORA.Repositories.CitizenBankInfoServicesRepo;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;
using BISPAPIORA.Repositories.BankOtherSpecificationServicesRepo;
using BISPAPIORA.Models.DTOS.EmploymentOtherSpecificationDTO;
using BISPAPIORA.Repositories.EmploymentOtherSpecificationServicesRepo;

namespace BISPAPIORA.Repositories.RegistrationServicesRepo
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IMapper _mapper;
        private readonly ICitizenService citizenService;
        private readonly Dbcontext db;
        private readonly ICitizenBankInfoService citizenBankService;
        private readonly IBankOtherSpecificationService bankOtherSpecificationService;
        private readonly IEmploymentOtherSpecificationService employmentOtherSpecificationService;
        public RegistrationService(IMapper mapper, Dbcontext db, ICitizenService citizenService, Dbcontext dbf, ICitizenBankInfoService citizenBankService, IBankOtherSpecificationService bankOtherSpecificationService, IEmploymentOtherSpecificationService employmentOtherSpecificationService)
        {
            _mapper = mapper;
            this.db = db;
            this.citizenService = citizenService;
            this.citizenBankService = citizenBankService;
            this.bankOtherSpecificationService = bankOtherSpecificationService;
            this.employmentOtherSpecificationService = employmentOtherSpecificationService;
        }

        //Add Registeration Service
        public async Task<ResponseModel<RegistrationResponseDTO>> AddRegisteredCitizen(AddRegistrationDTO model)
        {
            try
            {
                //Get If Citizen Exists
                var citizen = await db.tbl_citizens.Where(x => x.citizen_cnic.ToLower().Equals(model.citizenCnic.ToLower())).FirstOrDefaultAsync();    
                //Check For Already registered
                if (citizen == null)
                {
                    //Add New Citizen
                    var newCitizen = await citizenService.AddRegisteredCitizen(model);
                    if (newCitizen.success)
                    {
                        if (newCitizen.data != null)
                        {
                            //Mapping Citizen id to Request DTO
                            model.fkCitizen = newCitizen.data.citizenId;
                            #region Add New Registration
                            //Mapping Required Data To Registration DTO
                            var newRegistration = new tbl_registration();
                            newRegistration = _mapper.Map<tbl_registration>((model, newCitizen.data.citizenCode));
                            //Add New Registration
                            await db.tbl_registrations.AddAsync(newRegistration);
                            await db.SaveChangesAsync();
                            #endregion
                            #region Add Bank Info
                            //Mapping Required Data to Bank Info DTO
                            AddRegisteredCitizenBankInfoDTO newBankInfoRequest = new AddRegisteredCitizenBankInfoDTO();
                            newBankInfoRequest = _mapper.Map<AddRegisteredCitizenBankInfoDTO>(model);
                            //Add Bank Info
                            var newRegisteredBankInfo = await citizenBankService.AddRegisteredCitizenBankInfo(newBankInfoRequest);
                            #endregion
                            #region Add Citizen Bank Other Specification
                            if (!string.IsNullOrEmpty(model.citizenBankOtherSpecification))
                            {
                                //Mapping Required Data to Bank Other Specification
                                var addBankOtherSpecification = new AddRegisteredBankOtherSpecificationDTO();
                                addBankOtherSpecification = _mapper.Map<AddRegisteredBankOtherSpecificationDTO>((model,newRegisteredBankInfo.data));
                                //Add New Bank Other Specification
                                var responseBankOtherSpecification= await bankOtherSpecificationService.AddRegisteredBankOtherSpecification(addBankOtherSpecification);
                            }
                            #endregion
                            #region Add Citizen Employment Other Specifcation
                            if (!string.IsNullOrEmpty(model.citizenEmploymentOtherSpecification)) 
                            {
                                //Mapping Employment Other Specification
                                var addEmploymentOtherSpecification = new AddEmploymentOtherSpecificationDTO();
                                addEmploymentOtherSpecification = _mapper.Map<AddEmploymentOtherSpecificationDTO>(model);
                                //Add Employment Other Specification
                                var responseEmploymentOtherSpecification = await employmentOtherSpecificationService.AddEmploymentOtherSpecification(addEmploymentOtherSpecification);
                            }
                            #endregion
                            #region Preparing Response and Returning It
                            //Mapping Citizen to Response DTO
                            var response = _mapper.Map<RegistrationResponseDTO>(newCitizen.data);
                            response.registrationId = newRegistration.registration_id.ToString();
                            return new ResponseModel<RegistrationResponseDTO>()
                            {
                                success = true,
                                remarks = $"Citizen {model.citizenName} has been registered successfully",
                                data = response,
                            };
                            #endregion
                        }
                        else
                        {
                            return new ResponseModel<RegistrationResponseDTO>()
                            {
                                success = true,
                                remarks = $"Citizen {model.citizenName} has been not been registered successfully",
                            };
                        }
                    }
                    else
                    {
                        return new ResponseModel<RegistrationResponseDTO>()
                        {
                            success = true,
                            remarks = newCitizen.remarks
                        };
                    }
                }
                else
                {
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = false,
                        remarks = $"Citizen with cnic {model.citizenCnic} already exists",
                        data = _mapper.Map<RegistrationResponseDTO>(citizen),
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

        // Deletes a registration record based on the provided registrationId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<RegistrationResponseDTO>> DeleteRegistration(string registrationId)
        {
            try
            {
                // Retrieve the registration record from the database based on the registrationId
                var registration = await db.tbl_registrations.Where(x => x.registration_id == Guid.Parse(registrationId)).FirstOrDefaultAsync();

                // Check if the registration record exists
                if (registration != null)
                {
                    // Remove the registration record from the database
                    db.tbl_registrations.Remove(registration);
                    await db.SaveChangesAsync();

                    // Return a success response model
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = "Registration Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves registration details based on the provided registrationId
        // Returns a response model containing the registration details or an error message
        public async Task<ResponseModel<RegistrationResponseDTO>> GetRegistration(string registrationId)
        {
            try
            {
                // Retrieve the registration record from the database, including related entities for a complete citizen profile
                var existingCitizen = await db.tbl_registrations
                    .Where(x => x.registration_id == Guid.Parse(registrationId))
                    .Include(x => x.tbl_citizen)
                        .ThenInclude(x => x.tbl_citizen_tehsil)
                            .ThenInclude(x => x.tbl_district)
                                .ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen)
                        .ThenInclude(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen)
                        .ThenInclude(x => x.tbl_citizen_education)
                    .FirstOrDefaultAsync();

                // Check if the registration record exists
                if (existingCitizen != null)
                {
                    // Map the database entity to the response DTO using AutoMapper
                    var mappedResponse = _mapper.Map<RegistrationResponseDTO>(existingCitizen);

                    // Return a success response model with the mapped registration details
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        data = mappedResponse,
                        remarks = "Citizen found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

    }
}