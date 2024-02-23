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
        public async Task<ResponseModel<UserTypeResponseDTO>> AddUserType(AddUserTypeDTO model)
        {
            try
            {
                var userType = await db.tbl_user_types.Where(x => x.user_type_name.ToLower().Equals(model.userTypeName.ToLower())).FirstOrDefaultAsync();
                if (userType == null)
                {
                    var newUserType = new tbl_user_type();
                    newUserType = _mapper.Map<tbl_user_type>(model);
                    db.tbl_user_types.Add(newUserType);
                    await db.SaveChangesAsync();
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        success = true,
                        remarks = $"User Type {model.userTypeName} has been added successfully",
                        data = _mapper.Map<UserTypeResponseDTO>(newUserType),
                    };
                }
                else
                {
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        success = false,
                        remarks = $"User Type with name {model.userTypeName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<UserTypeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<UserTypeResponseDTO>> DeleteUserType(string userTypeId)
        {
            try
            {
                var existingUserType = await db.tbl_user_types.Where(x => x.user_type_id == Guid.Parse(userTypeId)).FirstOrDefaultAsync();
                if (existingUserType != null)
                {
                    db.tbl_user_types.Remove(existingUserType);
                    await db.SaveChangesAsync();
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        remarks = "User Type Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<UserTypeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<UserTypeResponseDTO>>> GetUserTypesList()
        {
            try
            {
                var userTypes = await db.tbl_user_types.ToListAsync();
                if (userTypes.Count() > 0)
                {
                    return new ResponseModel<List<UserTypeResponseDTO>>()
                    {
                        data = _mapper.Map<List<UserTypeResponseDTO>>(userTypes),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<UserTypeResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<UserTypeResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<UserTypeResponseDTO>> GetUserType(string userTypeId)
        {
            try
            {
                var existingUserType = await db.tbl_user_types.Where(x => x.user_type_id == Guid.Parse(userTypeId)).FirstOrDefaultAsync();
                if (existingUserType != null)
                {
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        data = _mapper.Map<UserTypeResponseDTO>(existingUserType),
                        remarks = "User Type found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<UserTypeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<UserTypeResponseDTO>> UpdateUserType(UpdateUserTypeDTO model)
        {
            try
            {
                var existingUserType = await db.tbl_user_types.Where(x => x.user_type_id == Guid.Parse(model.userTypeId)).FirstOrDefaultAsync();
                if (existingUserType != null)
                {
                    existingUserType = _mapper.Map(model, existingUserType);
                    await db.SaveChangesAsync();
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        remarks = $"User Type: {model.userTypeName} has been updated",
                        data = _mapper.Map<UserTypeResponseDTO>(existingUserType),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<UserTypeResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<UserTypeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}