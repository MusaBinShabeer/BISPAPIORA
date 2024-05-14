using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DTOS.UserDTOs;

namespace BISPAPIORA.Repositories.UserServicesRepo
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        private readonly OtherServices otherServices= new OtherServices();
        public UserService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        // Adds a new user to the system based on the provided user information
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<UserResponseDTO>> AddUser(AddUserDTO model)
        {
            try
            {
                // Check if a user with the same email already exists
                var user = await db.tbl_users.Where(x => x.user_email.ToLower().Equals(model.userEmail.ToLower())).FirstOrDefaultAsync();

                // If user does not exist, proceed to add the new user
                if (user == null)
                {
                    // Create a new user entity and map the data from the DTO using AutoMapper
                    var newUser = _mapper.Map<tbl_user>(model);

                    // Add the new user to the database
                    db.tbl_users.Add(newUser);
                    await db.SaveChangesAsync();

                    // Return a success response model with the details of the added user
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = true,
                        remarks = $"User {model.userName} has been added successfully",
                        data = _mapper.Map<UserResponseDTO>(newUser),
                    };
                }
                else
                {
                    // Return a failure response model if a user with the same email already exists
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false,
                        remarks = $"User with name {model.userName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Deletes a user from the system based on the provided userId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<UserResponseDTO>> DeleteUser(string userId)
        {
            try
            {
                // Retrieve the existing user from the database based on the userId
                var existingUser = await db.tbl_users.Where(x => x.user_id == Guid.Parse(userId)).FirstOrDefaultAsync();

                // Check if the user record exists
                if (existingUser != null)
                {
                    // Remove the user record from the database
                    db.tbl_users.Remove(existingUser);
                    await db.SaveChangesAsync();

                    // Return a success response model
                    return new ResponseModel<UserResponseDTO>()
                    {
                        remarks = "User Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<UserResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of users from the system
        // Returns a response model containing the list of users or an error message
        public async Task<ResponseModel<List<UserResponseDTO>>> GetUsersList()
        {
            try
            {
                // Retrieve the list of users from the database, including user type information
                var users = await db.tbl_users.Include(x => x.tbl_user_type).ToListAsync();

                // Check if there are any users in the list
                if (users.Count() > 0)
                {
                    // Map the list of user entities to a list of user response DTOs using AutoMapper
                    var mappedUsersList = _mapper.Map<List<UserResponseDTO>>(users);

                    // Return a success response model with the list of users
                    return new ResponseModel<List<UserResponseDTO>>()
                    {
                        data = mappedUsersList,
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no users are found
                    return new ResponseModel<List<UserResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<UserResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a user's details based on the provided userId
        // Returns a response model containing the user details or an error message
        public async Task<ResponseModel<UserResponseDTO>> GetUser(string userId)
        {
            try
            {
                // Retrieve the existing user from the database based on the userId, including user type information
                var existingUser = await db.tbl_users.Include(x => x.tbl_user_type).Where(x => x.user_id == Guid.Parse(userId)).FirstOrDefaultAsync();

                // Check if the user record exists
                if (existingUser != null)
                {
                    // Map the user entity to a user response DTO using AutoMapper
                    var mappedUser = _mapper.Map<UserResponseDTO>(existingUser);

                    // Return a success response model with the user details
                    return new ResponseModel<UserResponseDTO>()
                    {
                        data = mappedUser,
                        remarks = "User found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing user's information based on the provided update model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<UserResponseDTO>> UpdateUser(UpdateUserDTO model)
        {
            try
            {
                // Retrieve the existing user from the database based on the userId, including user type information
                var existingUser = await db.tbl_users.Include(x => x.tbl_user_type).Where(x => x.user_id == Guid.Parse(model.userId)).FirstOrDefaultAsync();

                // Check if the user record exists
                if (existingUser != null)
                {
                    // Map the update model data to the existing user entity using AutoMapper
                    existingUser = _mapper.Map(model, existingUser);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model with the updated user details
                    return new ResponseModel<UserResponseDTO>()
                    {
                        remarks = $"User: {model.userName} has been updated",
                        data = _mapper.Map<UserResponseDTO>(existingUser),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Updates the OTP (One-Time Password) for a user based on the provided email address and OTP
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<UserResponseDTO>> UpdateUserOtp(string to, string otp)
        {
            try
            {
                // Retrieve the existing user from the database based on the provided email address
                var existingUser = await db.tbl_users.Include(x => x.tbl_user_type).Where(x => x.user_email == to).FirstOrDefaultAsync();

                // Check if the user record exists
                if (existingUser != null)
                {
                    // Update the user's OTP with the provided OTP and save changes to the database
                    existingUser.user_otp = decimal.Parse(otp);
                    await db.SaveChangesAsync();

                    // Return a success response model with the updated user details
                    return new ResponseModel<UserResponseDTO>()
                    {
                        remarks = $"User: {existingUser.user_name} has been updated",
                        data = _mapper.Map<UserResponseDTO>(existingUser),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Verifies a user's OTP (One-Time Password) based on the provided email address and OTP
        // Returns a response model indicating the success or failure of the verification
        public async Task<ResponseModel<UserResponseDTO>> VerifyUserOtp(string to, string otp)
        {
            try
            {
                // Retrieve the existing user from the database based on the provided email address
                var existingUser = await db.tbl_users.Include(x => x.tbl_user_type).Where(x => x.user_email == to).FirstOrDefaultAsync();

                // Check if the user record exists
                if (existingUser != null)
                {
                    // Compare the provided OTP with the stored OTP for the user
                    if (existingUser.user_otp == decimal.Parse(otp))
                    {
                        // Return a success response model if the OTP is verified
                        return new ResponseModel<UserResponseDTO>()
                        {
                            remarks = "Verified",
                            data = _mapper.Map<UserResponseDTO>(existingUser),
                            success = true,
                        };
                    }
                    else
                    {
                        // Return a failure response model if the OTP is not verified
                        return new ResponseModel<UserResponseDTO>()
                        {
                            remarks = "Not Verified",
                            success = false,
                        };
                    }
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Updates the FTP (File Time Paswword) information for a user based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<UserResponseDTO>> UpdateFTP(UpdateUserFtpDTO model)
        {
            try
            {
                // Retrieve the existing user from the database based on the userId
                var existingUser = await db.tbl_users.Where(x => x.user_id == Guid.Parse(model.userId)).FirstOrDefaultAsync();

                // Check if the user record exists
                if (existingUser != null)
                {
                    // Check if FTP is not set for the user
                    if (existingUser.is_ftp_set == false)
                    {
                        // Check if the provided current password matches the stored password for the user
                        if (existingUser.user_password == otherServices.encodePassword(model.currentPassword))
                        {
                            // Update the user's password, set FTP to true, and save changes to the database
                            existingUser.user_password = otherServices.encodePassword(model.userPassword);
                            existingUser.is_ftp_set = true;
                            await db.SaveChangesAsync();

                            // Return a success response model with the updated user details
                            return new ResponseModel<UserResponseDTO>()
                            {
                                remarks = $"User: {model.userName} password has been updated",
                                data = _mapper.Map<UserResponseDTO>(existingUser),
                                success = true,
                            };
                        }
                        else
                        {
                            // Return a failure response model if the provided current password is incorrect
                            return new ResponseModel<UserResponseDTO>() { remarks = "Password Incorrect", success = false };
                        }
                    }
                    else
                    {
                        // Return a failure response model if the FTP is already set for the user
                        return new ResponseModel<UserResponseDTO>() { remarks = "Password Already Changed For the First Time", success = false };
                    }
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

    }
}