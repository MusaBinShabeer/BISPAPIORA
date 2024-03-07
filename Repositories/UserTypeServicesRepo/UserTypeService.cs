using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.UserTypeDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BISPAPIORA.Repositories.UserTypeServicesRepo
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public UserTypeService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }

        // Adds a new user type based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<UserTypeResponseDTO>> AddUserType(AddUserTypeDTO model)
        {
            try
            {
                // Check if a user type with the provided name already exists
                var userType = await db.tbl_user_types.Where(x => x.user_type_name.ToLower().Equals(model.userTypeName.ToLower())).FirstOrDefaultAsync();

                // If the user type does not exist, add a new user type
                if (userType == null)
                {
                    var newUserType = new tbl_user_type();
                    newUserType = _mapper.Map<tbl_user_type>(model);
                    db.tbl_user_types.Add(newUserType);
                    await db.SaveChangesAsync();

                    // Return a success response model with the added user type details
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        success = true,
                        remarks = $"User Type {model.userTypeName} has been added successfully",
                        data = _mapper.Map<UserTypeResponseDTO>(newUserType),
                    };
                }
                else
                {
                    // Return a failure response model if a user type with the provided name already exists
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        success = false,
                        remarks = $"User Type with name {model.userTypeName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserTypeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Deletes a user type based on the provided userTypeId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<UserTypeResponseDTO>> DeleteUserType(string userTypeId)
        {
            try
            {
                // Retrieve the existing user type from the database based on the userTypeId
                var existingUserType = await db.tbl_user_types.Where(x => x.user_type_id == Guid.Parse(userTypeId)).FirstOrDefaultAsync();

                // If the user type exists, remove it and save changes to the database
                if (existingUserType != null)
                {
                    db.tbl_user_types.Remove(existingUserType);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating that the user type has been deleted
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        remarks = "User Type Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserTypeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all user types
        // Returns a response model containing the list of user types or an error message
        public async Task<ResponseModel<List<UserTypeResponseDTO>>> GetUserTypesList()
        {
            try
            {
                // Retrieve all user types from the database
                var userTypes = await db.tbl_user_types.ToListAsync();

                // Check if there are user types in the list
                if (userTypes.Count() > 0)
                {
                    // Return a success response model with the list of user types
                    return new ResponseModel<List<UserTypeResponseDTO>>()
                    {
                        data = _mapper.Map<List<UserTypeResponseDTO>>(userTypes),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no user types are found
                    return new ResponseModel<List<UserTypeResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<UserTypeResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a user type based on the provided userTypeId
        // Returns a response model containing the user type details or an error message
        public async Task<ResponseModel<UserTypeResponseDTO>> GetUserType(string userTypeId)
        {
            try
            {
                // Retrieve the existing user type from the database based on the userTypeId
                var existingUserType = await db.tbl_user_types.Where(x => x.user_type_id == Guid.Parse(userTypeId)).FirstOrDefaultAsync();

                // Check if the user type exists
                if (existingUserType != null)
                {
                    // Return a success response model with the user type details
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        data = _mapper.Map<UserTypeResponseDTO>(existingUserType),
                        remarks = "User Type found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserTypeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Updates a user type based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<UserTypeResponseDTO>> UpdateUserType(UpdateUserTypeDTO model)
        {
            try
            {
                // Retrieve the existing user type from the database based on the provided userTypeId
                var existingUserType = await db.tbl_user_types.Where(x => x.user_type_id == Guid.Parse(model.userTypeId)).FirstOrDefaultAsync();

                // Check if the user type exists
                if (existingUserType != null)
                {
                    // Update the user type with the provided model and save changes to the database
                    existingUserType = _mapper.Map(model, existingUserType);
                    await db.SaveChangesAsync();

                    // Return a success response model with the updated user type details
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        remarks = $"User Type: {model.userTypeName} has been updated",
                        data = _mapper.Map<UserTypeResponseDTO>(existingUserType),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserTypeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

    }
}