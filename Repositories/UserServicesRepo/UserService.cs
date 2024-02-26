using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.UserDTOs;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Extensions;

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
        public async Task<ResponseModel<UserResponseDTO>> AddUser(AddUserDTO model)
        {
            try
            {
                var user = await db.tbl_users.Where(x => x.user_email.ToLower().Equals(model.userEmail.ToLower())).FirstOrDefaultAsync();
                if (user == null)
                {
                    var newUser = new tbl_user();
                    newUser = _mapper.Map<tbl_user>(model);
                    db.tbl_users.Add(newUser);
                    await db.SaveChangesAsync();
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = true,
                        remarks = $"User {model.userName} has been added successfully",
                        data = _mapper.Map<UserResponseDTO>(newUser),
                    };
                }
                else
                {
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false,
                        remarks = $"User with name {model.userName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<UserResponseDTO>> DeleteUser(string userId)
        {
            try
            {
                var existingUser = await db.tbl_users.Where(x => x.user_id == Guid.Parse(userId)).FirstOrDefaultAsync();
                if (existingUser != null)
                {
                    db.tbl_users.Remove(existingUser);
                    await db.SaveChangesAsync();
                    return new ResponseModel<UserResponseDTO>()
                    {
                        remarks = "User Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<UserResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<UserResponseDTO>>> GetUsersList()
        {
            try
            {
                var users = await db.tbl_users.Include(x=>x.tbl_user_type).ToListAsync();
                if (users.Count() > 0)
                {
                    return new ResponseModel<List<UserResponseDTO>>()
                    {
                        data = _mapper.Map<List<UserResponseDTO>>(users),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<UserResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<UserResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<UserResponseDTO>> GetUser(string userId)
        {
            try
            {
                var existingUser = await db.tbl_users.Include(x => x.tbl_user_type).Where(x => x.user_id == Guid.Parse(userId)).FirstOrDefaultAsync();
                if (existingUser != null)
                {
                    return new ResponseModel<UserResponseDTO>()
                    {
                        data = _mapper.Map<UserResponseDTO>(existingUser),
                        remarks = "User found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<UserResponseDTO>> UpdateUser(UpdateUserDTO model)
        {
            try
            {
                var existingUser = await db.tbl_users.Include(x => x.tbl_user_type).Where(x => x.user_id == Guid.Parse(model.userId)).FirstOrDefaultAsync();
                if (existingUser != null)
                {
                    existingUser = _mapper.Map(model, existingUser);
                    await db.SaveChangesAsync();
                    return new ResponseModel<UserResponseDTO>()
                    {
                        remarks = $"User: {model.userName} has been updated",
                        data = _mapper.Map<UserResponseDTO>(existingUser),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<UserResponseDTO>> UpdateFTP(UpdateUserFtpDTO model)
        {
            try
            {
                var existingUser = await db.tbl_users.Where(x => x.user_id == Guid.Parse(model.userId)).FirstOrDefaultAsync();
                if (existingUser != null)
                {
                    if (existingUser.is_ftp_set == false)
                    {
                        if (existingUser.user_password == otherServices.encodePassword(model.currentPassword))
                        {
                            existingUser.user_password = otherServices.encodePassword(model.userPassword);
                            existingUser.is_ftp_set = true;
                            await db.SaveChangesAsync();
                            return new ResponseModel<UserResponseDTO>()
                            {
                                remarks = $"User: {model.userName} password has been updated",
                                data = _mapper.Map<UserResponseDTO>(existingUser),
                                success = true,
                            };
                        }
                        else
                        {
                            return new ResponseModel<UserResponseDTO>() { remarks = "Password Incorrect", success = false };
                        }
                    }
                    else
                    {
                        return new ResponseModel<UserResponseDTO>() { remarks = "Password Already Changed For the First Time", success = false };
                    }
                }
                else
                {
                    return new ResponseModel<UserResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<UserResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}