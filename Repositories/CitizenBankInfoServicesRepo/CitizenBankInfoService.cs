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
        // Adds or updates a registered citizen's bank information based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> AddRegisteredCitizenBankInfo(AddRegisteredCitizenBankInfoDTO model)
        {
            try
            {
                // Check if there is already bank information for the given citizen and bank
                var citizenBankInfo = await db.tbl_citizen_family_bank_infos
                    .Where(x => x.fk_citizen.Equals(Guid.Parse(model.fkCitizen)) && x.fk_bank.Equals(Guid.Parse(model.fkBank)))
                    .FirstOrDefaultAsync();

                if (citizenBankInfo == null)
                {
                    // If no existing record is found, create a new citizen bank info
                    var newCitizenBankInfo = new tbl_citizen_family_bank_info();

                    // Map properties from the provided DTO to the entity using AutoMapper
                    newCitizenBankInfo = _mapper.Map<tbl_citizen_family_bank_info>(model);
                    db.tbl_citizen_family_bank_infos.Add(newCitizenBankInfo);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the added citizen bank info
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        success = true,
                        remarks = "CitizenBankInfo has been added successfully",
                        data = _mapper.Map<RegisteredCitizenBankInfoResponseDTO>(newCitizenBankInfo),
                    };
                }
                else
                {
                    // If an existing record is found, update the citizen bank info using the provided model
                    citizenBankInfo = _mapper.Map(model, citizenBankInfo);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated citizen bank info
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        remarks = "CitizenBankInfo has been added successfully",
                        data = _mapper.Map<RegisteredCitizenBankInfoResponseDTO>(citizenBankInfo),
                        success = true,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes a registered citizen's bank information based on the provided CitizenBankInfoId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> DeleteRegisteredCitizenBankInfo(string CitizenBankInfoId)
        {
            try
            {
                // Retrieve the existing citizen bank info from the database based on the provided ID
                var existingCitizenBankInfo = await db.tbl_citizen_family_bank_infos
                    .Where(x => x.citizen_bank_info_id == Guid.Parse(CitizenBankInfoId))
                    .FirstOrDefaultAsync();

                if (existingCitizenBankInfo != null)
                {
                    // If the record is found, remove it from the database and save changes
                    db.tbl_citizen_family_bank_infos.Remove(existingCitizenBankInfo);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating the successful deletion
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        remarks = "CitizenBankInfo Deleted",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of registered citizen bank information
        // Returns a response model containing the list or indicating no records
        public async Task<ResponseModel<List<RegisteredCitizenBankInfoResponseDTO>>> GetRegisteredCitizenBankInfosList()
        {
            try
            {
                // Retrieve all citizen bank information records from the database, including related entities
                var citizenBankInfos = await db.tbl_citizen_family_bank_infos.Include(x => x.tbl_citizen).Include(x => x.tbl_bank).ToListAsync();

                if (citizenBankInfos.Count() > 0)
                {
                    // If records are found, return a success response model with the mapped list
                    return new ResponseModel<List<RegisteredCitizenBankInfoResponseDTO>>()
                    {
                        data = _mapper.Map<List<RegisteredCitizenBankInfoResponseDTO>>(citizenBankInfos),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // If no records are found, return a failure response
                    return new ResponseModel<List<RegisteredCitizenBankInfoResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<RegisteredCitizenBankInfoResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves registered citizen bank information based on the provided citizenBankInfoId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> GetRegisteredCitizenBankInfo(string citizenBankInfoId)
        {
            try
            {
                // Retrieve the existing registered citizen bank info from the database based on the provided ID
                var existingCitizenBankInfo = await db.tbl_citizen_family_bank_infos
                    .Include(x => x.tbl_citizen)
                    .Include(x => x.tbl_bank)
                    .Where(x => x.citizen_bank_info_id == Guid.Parse(citizenBankInfoId))
                    .FirstOrDefaultAsync();

                if (existingCitizenBankInfo != null)
                {
                    // If the record is found, return a success response model with the mapped DTO
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        data = _mapper.Map<RegisteredCitizenBankInfoResponseDTO>(existingCitizenBankInfo),
                        remarks = "CitizenBankInfo found successfully",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing registered citizen bank information based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<RegisteredCitizenBankInfoResponseDTO>> UpdateRegisteredCitizenBankInfo(UpdateRegisteredCitizenBankInfoDTO model)
        {
            try
            {
                // Retrieve the existing registered citizen bank info from the database based on the provided ID
                var existingCitizenBankInfo = await db.tbl_citizen_family_bank_infos
                    .Where(x => x.citizen_bank_info_id == Guid.Parse(model.CitizenBankInfoId))
                    .FirstOrDefaultAsync();

                if (existingCitizenBankInfo != null)
                {
                    // Update the existing registered citizen bank info with the properties from the provided model
                    existingCitizenBankInfo = _mapper.Map(model, existingCitizenBankInfo);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated citizen bank info
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        remarks = "CitizenBankInfo has been updated successfully",
                        data = _mapper.Map<RegisteredCitizenBankInfoResponseDTO>(existingCitizenBankInfo),
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<RegisteredCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        #endregion
        #region Enrolled Citizen Bank Info
        // Adds or updates an enrolled citizen's bank information based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> AddEnrolledCitizenBankInfo(AddEnrolledCitizenBankInfoDTO model)
        {
            try
            {
                // Check if there is already bank information for the given citizen
                var citizenBankInfo = await db.tbl_citizen_bank_infos
                    .Where(x => x.fk_citizen.Equals(Guid.Parse(model.fkCitizen)))
                    .FirstOrDefaultAsync();

                if (citizenBankInfo == null)
                {
                    // If no existing record is found, create a new citizen bank info
                    var newCitizenBankInfo = new tbl_citizen_bank_info();

                    // Map properties from the provided DTO to the entity using AutoMapper
                    newCitizenBankInfo = _mapper.Map<tbl_citizen_bank_info>(model);

                    // Add the new citizen bank info to the database
                    await db.tbl_citizen_bank_infos.AddAsync(newCitizenBankInfo);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Retrieve the newly added citizen bank info from the database
                    newCitizenBankInfo = await db.tbl_citizen_bank_infos
                        .Where(x => x.fk_citizen == newCitizenBankInfo.fk_citizen)
                        .FirstOrDefaultAsync();

                    // Return a success response model with details of the added citizen bank info
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        success = true,
                        remarks = "CitizenBankInfo has been added successfully",
                        data = _mapper.Map<EnrolledCitizenBankInfoResponseDTO>(newCitizenBankInfo),
                    };
                }
                else
                {
                    // If an existing record is found, update the citizen bank info using the provided model
                    var updateModel = _mapper.Map<UpdateEnrolledCitizenBankInfoDTO>(model);
                    updateModel.CitizenBankInfoId = citizenBankInfo.citizen_bank_info_id.ToString();
                    var response = await UpdateEnrolledCitizenBankInfo(updateModel);

                    // Return the response from the update operation
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes an enrolled citizen's bank information based on the provided CitizenBankInfoId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> DeleteEnrolledCitizenBankInfo(string CitizenBankInfoId)
        {
            try
            {
                // Retrieve the existing citizen bank info from the database based on the provided ID
                var existingCitizenBankInfo = await db.tbl_citizen_bank_infos
                    .Where(x => x.citizen_bank_info_id == Guid.Parse(CitizenBankInfoId))
                    .FirstOrDefaultAsync();

                if (existingCitizenBankInfo != null)
                {
                    // If the record is found, remove it from the database and save changes
                    db.tbl_citizen_bank_infos.Remove(existingCitizenBankInfo);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating the successful deletion
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        remarks = "CitizenBankInfo Deleted",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of enrolled citizen bank information
        // Returns a response model containing the list or indicating no records
        public async Task<ResponseModel<List<EnrolledCitizenBankInfoResponseDTO>>> GetEnrolledCitizenBankInfosList()
        {
            try
            {
                // Retrieve all citizen bank information records from the database, including related entities
                var citizenBankInfos = await db.tbl_citizen_bank_infos.Include(x => x.tbl_citizen).Include(x => x.tbl_bank).ToListAsync();

                if (citizenBankInfos.Count() > 0)
                {
                    // If records are found, return a success response model with the mapped list
                    return new ResponseModel<List<EnrolledCitizenBankInfoResponseDTO>>()
                    {
                        data = _mapper.Map<List<EnrolledCitizenBankInfoResponseDTO>>(citizenBankInfos),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // If no records are found, return a failure response
                    return new ResponseModel<List<EnrolledCitizenBankInfoResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<EnrolledCitizenBankInfoResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves enrolled citizen bank information based on the provided citizenBankInfoId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> GetEnrolledCitizenBankInfo(string citizenBankInfoId)
        {
            try
            {
                // Retrieve the existing enrolled citizen bank info from the database based on the provided ID
                var existingCitizenBankInfo = await db.tbl_citizen_bank_infos
                    .Include(x => x.tbl_citizen)
                    .Include(x => x.tbl_bank)
                    .Where(x => x.citizen_bank_info_id == Guid.Parse(citizenBankInfoId))
                    .FirstOrDefaultAsync();

                if (existingCitizenBankInfo != null)
                {
                    // If the record is found, return a success response model with the mapped DTO
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        data = _mapper.Map<EnrolledCitizenBankInfoResponseDTO>(existingCitizenBankInfo),
                        remarks = "CitizenBankInfo found successfully",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing enrolled citizen bank information based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EnrolledCitizenBankInfoResponseDTO>> UpdateEnrolledCitizenBankInfo(UpdateEnrolledCitizenBankInfoDTO model)
        {
            try
            {
                // Retrieve the existing enrolled citizen bank info from the database based on the provided ID
                var existingCitizenBankInfo = await db.tbl_citizen_bank_infos
                    .Where(x => x.citizen_bank_info_id == Guid.Parse(model.CitizenBankInfoId))
                    .FirstOrDefaultAsync();

                if (existingCitizenBankInfo != null)
                {
                    // Update the existing enrolled citizen bank info with the properties from the provided model
                    existingCitizenBankInfo = _mapper.Map(model, existingCitizenBankInfo);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated citizen bank info
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        remarks = "CitizenBankInfo has been updated successfully",
                        data = _mapper.Map<EnrolledCitizenBankInfoResponseDTO>(existingCitizenBankInfo),
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<EnrolledCitizenBankInfoResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
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
