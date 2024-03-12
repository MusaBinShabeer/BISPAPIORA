using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.FunctionalityDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BISPAPIORA.Repositories.FunctionalityServicesRepo
{
    public class FunctionalityService : IFunctionalityService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public FunctionalityService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }

        // Adds a new Functionality record to the database based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<FunctionalityResponseDTO>> AddFunctionality(AddFunctionalityDTO model)
        {
            try
            {
                // Check if a Functionality with the same name already exists in the database
                var functionality = await db.tbl_functionalitys.Where(x => x.functionality_name.ToLower().Equals(model.functionalityName.ToLower())).FirstOrDefaultAsync();

                if (functionality == null)
                {
                    // If no existing record is found, create a new Functionality record
                    var newFunctionality = new tbl_functionality();

                    // Map properties from the provided DTO to the entity using AutoMapper
                    newFunctionality = _mapper.Map<tbl_functionality>(model);
                    db.tbl_functionalitys.Add(newFunctionality);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the added Functionality record
                    return new ResponseModel<FunctionalityResponseDTO>()
                    {
                        success = true,
                        remarks = $"Functionality {model.functionalityName} has been added successfully",
                        data = _mapper.Map<FunctionalityResponseDTO>(newFunctionality),
                    };
                }
                else
                {
                    // If a Functionality with the same name already exists, return a failure response
                    return new ResponseModel<FunctionalityResponseDTO>()
                    {
                        success = false,
                        remarks = $"Functionality with name {model.functionalityName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<FunctionalityResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes an existing Functionality record from the database based on the provided Functionality ID
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<FunctionalityResponseDTO>> DeleteFunctionality(string FunctionalityId)
        {
            try
            {
                // Retrieve the existing Functionality record from the database based on the provided ID
                var existingFunctionality = await db.tbl_functionalitys.Where(x => x.functionality_id == Guid.Parse(FunctionalityId)).FirstOrDefaultAsync();

                if (existingFunctionality != null)
                {
                    // If the Functionality record is found, remove it from the database
                    db.tbl_functionalitys.Remove(existingFunctionality);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating the deletion
                    return new ResponseModel<FunctionalityResponseDTO>()
                    {
                        remarks = "Functionality Deleted",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<FunctionalityResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<FunctionalityResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all Functionality records from the database
        // Returns a response model containing the list of Functionalitys or indicating the absence of records
        public async Task<ResponseModel<List<FunctionalityResponseDTO>>> GetFunctionalitysList()
        {
            try
            {
                // Retrieve all Functionality records from the database
                var functionalitys = await db.tbl_functionalitys.ToListAsync();

                if (functionalitys.Count() > 0)
                {
                    // If there are records, return a success response model with the list of Functionalitys
                    return new ResponseModel<List<FunctionalityResponseDTO>>()
                    {
                        data = _mapper.Map<List<FunctionalityResponseDTO>>(functionalitys),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // If no records are found, return a failure response
                    return new ResponseModel<List<FunctionalityResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<FunctionalityResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a specific Functionality record from the database based on the provided Functionality ID
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<FunctionalityResponseDTO>> GetFunctionality(string FunctionalityId)
        {
            try
            {
                // Retrieve the existing Functionality record from the database based on the provided ID
                var existingFunctionality = await db.tbl_functionalitys.Where(x => x.functionality_id == Guid.Parse(FunctionalityId)).FirstOrDefaultAsync();

                if (existingFunctionality != null)
                {
                    // If the Functionality record is found, return a success response model with details
                    return new ResponseModel<FunctionalityResponseDTO>()
                    {
                        data = _mapper.Map<FunctionalityResponseDTO>(existingFunctionality),
                        remarks = "Functionality found successfully",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<FunctionalityResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<FunctionalityResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing Functionality record based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<FunctionalityResponseDTO>> UpdateFunctionality(UpdateFunctionalityDTO model)
        {
            try
            {
                // Retrieve the existing Functionality record from the database based on the provided ID
                var existingFunctionality = await db.tbl_functionalitys.Where(x => x.functionality_id == Guid.Parse(model.functionalityId)).FirstOrDefaultAsync();

                if (existingFunctionality != null)
                {
                    // If the Functionality record is found, update it with the properties from the provided model
                    existingFunctionality = _mapper.Map(model, existingFunctionality);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated Functionality record
                    return new ResponseModel<FunctionalityResponseDTO>()
                    {
                        remarks = $"Functionality: {model.functionalityName} has been updated",
                        data = _mapper.Map<FunctionalityResponseDTO>(existingFunctionality),
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<FunctionalityResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<FunctionalityResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}