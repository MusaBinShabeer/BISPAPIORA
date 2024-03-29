﻿using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;
//using BISPAPIORA.Repositories.CitizenAttachmentServicesRepo;
//using BISPAPIORA.Repositories.CitizenThumbPrintServicesRepo;
//using BISPAPIORA.Models.DTOS.CitizenAttachmentDTO;
//using BISPAPIORA.Models.DTOS.CitizenThumbPrintDTO;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Repositories.CitizenBankInfoServicesRepo;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;
using BISPAPIORA.Repositories.ImageCitizenAttachmentServicesRepo;
using BISPAPIORA.Models.DTOS.ImageCitizenFingerPrintDTO;
using BISPAPIORA.Repositories.ImageCitizenFingePrintServicesRepo;
using BISPAPIORA.Repositories.InnerServicesRepo;
using BISPAPIORA.Models.DTOS.VerificationResponseDTO;
using BISPAPIORA.Models.DTOS.CitizenDTO;

namespace BISPAPIORA.Repositories.CitizenServicesRepo
{
    public class CitizenService : ICitizenService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        private readonly IImageCitizenAttachmentService attachmentService;
        private readonly ICitizenBankInfoService citizenBankInfoService;
        private readonly IImageCitizenFingerPrintService thumbprintService;
        private readonly IInnerServices innerServices;
        public CitizenService(IInnerServices innerServices, IMapper mapper, Dbcontext db,ICitizenBankInfoService citizenBankInfoService, IImageCitizenFingerPrintService thumbPrintService, IImageCitizenAttachmentService citizenAttachmentService)
        {
            _mapper = mapper;
            this.db = db;
            this.attachmentService = citizenAttachmentService;
            this.thumbprintService = thumbPrintService;
            this.citizenBankInfoService = citizenBankInfoService;
            this.innerServices = innerServices;
        }
        #region Registered Citizen

        // Adds a new registered citizen to the database
        public async Task<ResponseModel<RegistrationResponseDTO>> AddRegisteredCitizen(AddRegistrationDTO model)
        {
            try
            {
                // Check if a citizen with the same CNIC already exists
                var Citizen = await db.tbl_citizens
                    .Where(x => x.citizen_cnic.ToLower().Equals(model.citizenCnic.ToLower()))
                    .FirstOrDefaultAsync();

                if (Citizen == null)
                {
                    // If the citizen does not exist, create a new citizen and save to the database
                    var newCitizen = new tbl_citizen();
                    newCitizen = _mapper.Map<tbl_citizen>(model);
                    db.tbl_citizens.Add(newCitizen);
                    await db.SaveChangesAsync();

                    // Return a success response model with the newly added citizen details
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen {model.citizenName} has been added successfully",
                        data = _mapper.Map<RegistrationResponseDTO>(newCitizen),
                    };
                }
                else
                {
                    // Return a failure response model if a citizen with the same CNIC already exists
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
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates the details of an existing registered citizen in the database
        public async Task<ResponseModel<RegistrationResponseDTO>> UpdateRegisteredCitizen(UpdateRegistrationDTO model)
        {
            try
            {
                // Find the existing citizen in the database based on the provided citizen ID
                var existingCitizen = await db.tbl_citizens
                    .Where(x => x.citizen_id == Guid.Parse(model.fkCitizen))
                    .FirstOrDefaultAsync();

                if (existingCitizen != null)
                {
                    // Update the existing citizen details
                    existingCitizen = _mapper.Map(model, existingCitizen);
                    await db.SaveChangesAsync();

                    // Create a request DTO for adding or updating the bank information of the citizen
                    var AddBankInfoRequest = new AddRegisteredCitizenBankInfoDTO();
                    AddBankInfoRequest = _mapper.Map<AddRegisteredCitizenBankInfoDTO>(model);

                    // Call the service to add or update the bank information
                    var bankInfo = citizenBankInfoService.AddRegisteredCitizenBankInfo(AddBankInfoRequest);

                    // Update the citizen's CNIC in the model
                    model.citizenCnic = existingCitizen.citizen_cnic;

                    // Return a success response model with the updated citizen details
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = $"Citizen: {model.citizenName} has been updated",
                        data = _mapper.Map<RegistrationResponseDTO>(existingCitizen),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided citizen ID
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of registered citizens with additional related information
        public async Task<ResponseModel<List<RegistrationResponseDTO>>> GetRegisteredCitizensList()
        {
            try
            {
                // Retrieve registered citizens with related information from the database
                var registerdCitizens = await db.tbl_registrations
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_education)
                    .Include(x => x.tbl_citizen.tbl_citizen_family_bank_info).ThenInclude(x => x.tbl_bank)
                    .Include(x => x.tbl_citizen.tbl_citizen_family_bank_info.tbl_bank_other_specification)
                    .Include(x => x.tbl_citizen.tbl_citizen_registration)
                    .Select(x => x.tbl_citizen)
                    .ToListAsync();

                if (registerdCitizens.Count() > 0)
                {
                    // Return a success response model with the list of registered citizens
                    return new ResponseModel<List<RegistrationResponseDTO>>()
                    {
                        data = _mapper.Map<List<RegistrationResponseDTO>>(registerdCitizens),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no records are found
                    return new ResponseModel<List<RegistrationResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<List<RegistrationResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        #endregion
        #region Enrolled Citizen

        // Adds a new enrolled citizen to the database
        public async Task<ResponseModel<EnrollmentResponseDTO>> AddEnrolledCitizen(AddEnrollmentDTO model)
        {
            try
            {
                // Check if a citizen with the same CNIC already exists
                var Citizen = await db.tbl_citizens
                    .Where(x => x.citizen_cnic.ToLower().Equals(model.citizenCnic.ToLower()))
                    .FirstOrDefaultAsync();

                if (Citizen == null)
                {
                    // If the citizen does not exist, create a new citizen and save it to the database
                    var newCitizen = new tbl_citizen();
                    newCitizen = _mapper.Map<tbl_citizen>(model);
                    await db.tbl_citizens.AddAsync(newCitizen);
                    await db.SaveChangesAsync();

                    // Call the attachment and thumbprint services to associate images with the new citizen
                    var newAttachmentDto = new AddImageCitizenAttachmentDTO()
                    {
                        imageCitizenAttachmentCnic = newCitizen.citizen_cnic,
                        fkCitizen = newCitizen.citizen_id.ToString()
                    };
                    var resposneOfattachment = await attachmentService.AddFkCitizenToAttachment(newAttachmentDto);

                    var newthumbPrintDto = new AddImageCitizenFingerPrintDTO()
                    {
                        imageCitizenFingerPrintCnic = newCitizen.citizen_cnic,
                        fkCitizen = newCitizen.citizen_id.ToString()
                    };
                    var responseOfThumbPrint = await thumbprintService.AddFkCitizentoImage(newthumbPrintDto);

                    // Return a success response model with the newly added citizen details
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen {model.citizenName} has been added successfully",
                        data = _mapper.Map<EnrollmentResponseDTO>(newCitizen),
                    };
                }
                else
                {
                    // Return a success response model if a citizen with the same CNIC already exists
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen with CNIC {model.citizenCnic} already exists",
                        data = _mapper.Map<EnrollmentResponseDTO>(Citizen),
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<EnrollmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates the details of an existing enrolled citizen in the database
        public async Task<ResponseModel<EnrollmentResponseDTO>> UpdateEnrolledCitizen(UpdateEnrollmentDTO model)
        {
            try
            {
                // Find the existing citizen in the database based on the provided citizen ID
                var existingCitizen = await db.tbl_citizens
                    .Where(x => x.citizen_id == Guid.Parse(model.fkCitizen))
                    .FirstOrDefaultAsync();

                if (existingCitizen != null)
                {
                    // Update the existing citizen details
                    existingCitizen = _mapper.Map(model, existingCitizen);
                    await db.SaveChangesAsync();

                    // Create DTOs for adding or updating the citizen's attachment and thumbprint
                    var newAttachmentDto = new AddImageCitizenAttachmentDTO()
                    {
                        fkCitizen = existingCitizen.citizen_id.ToString(),
                        imageCitizenAttachmentCnic = existingCitizen.citizen_cnic
                    };
                    var resposneOfattachment = await attachmentService.AddFkCitizenToAttachment(newAttachmentDto);

                    var newthumbPrintDto = new AddImageCitizenFingerPrintDTO()
                    {
                        imageCitizenFingerPrintCnic = existingCitizen.citizen_cnic,
                        fkCitizen = existingCitizen.citizen_id.ToString()
                    };
                    var responseOfThumbPrint = await thumbprintService.AddFkCitizentoImage(newthumbPrintDto);

                    // Return a success response model with the updated citizen details
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        remarks = $"Citizen: {model.citizenName} has been updated",
                        data = _mapper.Map<EnrollmentResponseDTO>(existingCitizen),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided citizen ID
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<EnrollmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of enrolled citizens with additional related information
        public async Task<ResponseModel<List<EnrollmentResponseDTO>>> GetEnrolledCitizensList()
        {
            try
            {
                // Retrieve enrolled citizens with related information from the database
                var enrolledCitizens = await db.tbl_enrollments
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_education)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_scheme)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_bank_info).ThenInclude(x => x.tbl_bank)
                    .Include(x => x.tbl_citizen.tbl_citizen_bank_info.tbl_bank_other_specification)
                    .ToListAsync();

                if (enrolledCitizens.Count() > 0)
                {
                    // Return a success response model with the list of enrolled citizens
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
                    // Return a failure response model if no records are found
                    return new ResponseModel<List<EnrollmentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<List<EnrollmentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        #endregion

        // Deletes a citizen from the database based on the provided CitizenId
        public async Task<ResponseModel<RegistrationResponseDTO>> DeleteCitizen(string CitizenId)
        {
            try
            {
                // Find the existing citizen in the database based on the provided citizen ID
                var existingCitizen = await db.tbl_citizens
                    .Where(x => x.citizen_id == Guid.Parse(CitizenId))
                    .FirstOrDefaultAsync();

                if (existingCitizen != null)
                {
                    // Remove the citizen from the database and save changes
                    db.tbl_citizens.Remove(existingCitizen);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating the citizen has been deleted
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = "Citizen Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided citizen ID
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves details of a citizen from the database based on the provided CitizenId
        public async Task<ResponseModel<RegistrationResponseDTO>> GetCitizen(string CitizenId)
        {
            try
            {
                // Find the existing citizen in the database based on the provided citizen ID
                var existingCitizen = await db.tbl_citizens
                    .Where(x => x.citizen_id == Guid.Parse(CitizenId))
                    .FirstOrDefaultAsync();

                if (existingCitizen != null)
                {
                    // Return a success response model with the details of the found citizen
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        data = _mapper.Map<RegistrationResponseDTO>(existingCitizen),
                        remarks = "Citizen found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided citizen ID
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        public async Task<ResponseModel<CitizenResponseDTO>> GetCitizenByCnicForApp(string CitizenCnic)
        {
            try
            {
                // Find the existing citizen in the database based on the provided citizen ID
                var existingCitizen = await db.tbl_citizens
                    .Where(x => x.citizen_cnic == CitizenCnic)
                    .Include(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen_education)
                    .Include(x => x.tbl_citizen_scheme)
                    .Include(x => x.tbl_citizen_registration)
                    .Include(x => x.tbl_enrollment)
                    .Include(x => x.tbl_citizen_bank_info).ThenInclude(x => x.tbl_bank)
                    .Include(x => x.tbl_citizen_family_bank_info).ThenInclude(x => x.tbl_bank_other_specification)
                    .Include(x => x.tbl_image_citizen_attachment)
                    .Include(x => x.tbl_image_citizen_finger_print)
                    .FirstOrDefaultAsync();

                if (existingCitizen != null)
                {
                    // Return a success response model with the details of the found citizen
                    return new ResponseModel<CitizenResponseDTO>()
                    {
                        data = _mapper.Map<CitizenResponseDTO>(existingCitizen),
                        remarks = "Citizen found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided citizen ID
                    return new ResponseModel<CitizenResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<CitizenResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Verifies the registration status of a citizen with the provided CNIC
        public async Task<ResponseModel<RegistrationResponseDTO>> VerifyCitizenRegistrationWithCNIC(string citizenCnic)
        {
            try
            {
                // Find the existing citizen in the database based on the provided CNIC
                var existingCitizen = await db.tbl_citizens
                    .Where(x => x.citizen_cnic == citizenCnic)
                    .Include(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen_education)
                    .Include(x => x.tbl_citizen_scheme)
                    .Include(x => x.tbl_citizen_registration)
                    .Include(x => x.tbl_enrollment)
                    .Include(x => x.tbl_citizen_bank_info).ThenInclude(x => x.tbl_bank)
                    .Include(x => x.tbl_citizen_family_bank_info).ThenInclude(x => x.tbl_bank_other_specification)
                    .FirstOrDefaultAsync();

                if (existingCitizen != null)
                {
                    // Check if the citizen is already registered
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = "Already Registered",
                        success = false,
                    };
                }
                else
                {
                    // Verify the citizen using an inner service
                    var verifyCitizen = await innerServices.VerifyCitzen(citizenCnic);
                    var response = new ResponseModel<RegistrationResponseDTO>();

                    // Map the verification result to the response model
                    if (verifyCitizen.success)
                    {
                        response.data = _mapper.Map<RegistrationResponseDTO>((verifyCitizen.data, true));
                    }
                    else
                    {
                        response.data = verifyCitizen.data != null ? _mapper.Map<RegistrationResponseDTO>((verifyCitizen.data, false)) : null;
                    }

                    response.remarks = "Applicant Not Registered";
                    response.success = true;

                    return response;
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<RegistrationResponseDTO>> VerifyCitizenEnrollmentWithCNIC(string citizenCnic)
        {
            try
            {
                // Find the existing citizen in the database based on the provided CNIC
                var existingCitizen = await db.tbl_citizens
                    .Where(x => x.citizen_cnic == citizenCnic)
                    .Include(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen_education)
                    .Include(x => x.tbl_citizen_scheme)
                    .Include(x => x.tbl_citizen_registration)
                    .Include(x => x.tbl_enrollment)
                    .Include(x => x.tbl_citizen_bank_info).ThenInclude(x => x.tbl_bank)
                    .Include(x => x.tbl_citizen_family_bank_info).ThenInclude(x => x.tbl_bank_other_specification)
                    .FirstOrDefaultAsync();

                if (existingCitizen != null)
                {
                    if (existingCitizen.tbl_enrollment == null)
                    {
                        var verifyCitizen = await innerServices.VerifyCitzen(citizenCnic);
                        var response = new ResponseModel<RegistrationResponseDTO>();

                        // Map the verification result to the response model
                        if (verifyCitizen.success)
                        {
                            response.data = _mapper.Map<RegistrationResponseDTO>((verifyCitizen.data, true, existingCitizen));
                        }
                        else
                        {
                            response.data = verifyCitizen.data != null ? _mapper.Map<RegistrationResponseDTO>((verifyCitizen.data, false, existingCitizen)) : null;
                        }
                        // Check if the citizen is already registered
                        return new ResponseModel<RegistrationResponseDTO>()
                        {
                            data = response.data,
                            remarks = "Already Registered",
                            success = true,
                        };
                    }
                    else
                    {
                        return new ResponseModel<RegistrationResponseDTO>()
                        {
                            remarks = "Already Enrolled",
                            success = false,
                        };
                    }
                }
                else
                {
                    // Verify the citizen using an inner service
                    var verifyCitizen = await innerServices.VerifyCitzen(citizenCnic);
                    var response = new ResponseModel<RegistrationResponseDTO>();

                    // Map the verification result to the response model
                    if (verifyCitizen.success)
                    {
                        response.data = _mapper.Map<RegistrationResponseDTO>((verifyCitizen.data, true));
                    }
                    else
                    {
                        response.data = verifyCitizen.data != null ? _mapper.Map<RegistrationResponseDTO>((verifyCitizen.data, false)) : null;
                    }

                    response.remarks = "Applicant Not Registered";
                    response.success = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves details of a registered citizen based on the provided CNIC
        public async Task<ResponseModel<RegistrationResponseDTO>> GetRegisteredCitizenByCnic(string citizenCnic)
        {
            try
            {
                // Find the existing registered citizen in the database based on the provided CNIC
                var existingCitizen = await db.tbl_citizens
                    .Where(x => x.citizen_cnic == citizenCnic)
                    .Include(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen_education)
                    .Include(x => x.tbl_citizen_scheme)
                    .Include(x => x.tbl_citizen_registration)
                    .Include(x => x.tbl_enrollment)
                    .Include(x => x.tbl_citizen_family_bank_info).ThenInclude(x => x.tbl_bank)
                    .Include(x => x.tbl_citizen_family_bank_info).ThenInclude(x => x.tbl_bank_other_specification)
                    .FirstOrDefaultAsync();

                if (existingCitizen != null)
                {
                    // Create a response model with details of the registered citizen
                    var response = _mapper.Map<RegistrationResponseDTO>(existingCitizen);

                    // Update enrollment and registration status flags in the response model
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
                    // Return a failure response model if no record is found for the provided CNIC
                    return new ResponseModel<RegistrationResponseDTO>()
                    {
                        remarks = "Applicant Not Registered",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<RegistrationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves details of an enrolled citizen based on the provided CNIC
        public async Task<ResponseModel<EnrollmentResponseDTO>> GetEnrolledCitizenByCnic(string citizenCnic)
        {
            try
            {
                // Find the existing enrolled citizen in the database based on the provided CNIC
                var existingCitizen = await db.tbl_enrollments
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_education)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_scheme)
                    .Include(x => x.tbl_citizen).ThenInclude(x => x.tbl_citizen_bank_info).ThenInclude(x => x.tbl_bank)
                    .Include(x => x.tbl_citizen.tbl_citizen_bank_info).ThenInclude(x => x.tbl_bank_other_specification)
                    .Where(x => x.tbl_citizen.citizen_cnic == citizenCnic)
                    .FirstOrDefaultAsync();

                if (existingCitizen != null)
                {
                    // Create a response model with details of the enrolled citizen
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        data = _mapper.Map<EnrollmentResponseDTO>(existingCitizen),
                        remarks = "Citizen found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided CNIC
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        remarks = "Not Found",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<EnrollmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}
