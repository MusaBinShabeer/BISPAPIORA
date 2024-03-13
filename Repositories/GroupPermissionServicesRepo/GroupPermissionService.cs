using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.GroupPermissionDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BISPAPIORA.Repositories.GroupPermissionServicesRepo
{
    public class GroupPermissionService : IGroupPermissionService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public GroupPermissionService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }

        // Adds a new GroupPermission record to the database based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<GroupPermissionResponseDTO>> AddGroupPermission(AddGroupPermissionDTO model)
        {
            try
            {
                // Check if a GroupPermission with the same name already exists in the database
                var groupPermission = await db.tbl_group_permissions.Where(x => x.fk_user_type.Equals(Guid.Parse(model.fkUserType)) && x.fk_functionality.Equals(Guid.Parse(model.fkFunctionality))).FirstOrDefaultAsync();

                if (groupPermission == null)
                {
                    // If no existing record is found, create a new GroupPermission record
                    var newGroupPermission = new tbl_group_permission();
                    
                    // Map properties from the provided DTO to the entity using AutoMapper
                    newGroupPermission = _mapper.Map<tbl_group_permission>(model);
                    db.tbl_group_permissions.Add(newGroupPermission);
                    await db.SaveChangesAsync();
                    // Return a success response model with details of the added GroupPermission record
                    return new ResponseModel<GroupPermissionResponseDTO>()
                    {
                        success = true,
                        remarks = $"Group Permission has been added successfully",
                        data = _mapper.Map<GroupPermissionResponseDTO>(newGroupPermission),
                    };
                }
                else
                {
                    // If a GroupPermission with the same name already exists, return a failure response
                    return new ResponseModel<GroupPermissionResponseDTO>()
                    {
                        success = false,
                        remarks = $"Group Permission of User type: {groupPermission.tbl_user_type.user_type_name} with Functionality {groupPermission.tbl_functionality.functionality_name} has been added successfully"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<GroupPermissionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes an existing GroupPermission record from the database based on the provided GroupPermission ID
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<GroupPermissionResponseDTO>> DeleteGroupPermission(string GroupPermissionId)
        {
            try
            {
                // Retrieve the existing GroupPermission record from the database based on the provided ID
                var existingGroupPermission = await db.tbl_group_permissions.Where(x => x.group_permission_id == Guid.Parse(GroupPermissionId)).FirstOrDefaultAsync();

                if (existingGroupPermission != null)
                {
                    // If the GroupPermission record is found, remove it from the database
                    db.tbl_group_permissions.Remove(existingGroupPermission);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating the deletion
                    return new ResponseModel<GroupPermissionResponseDTO>()
                    {
                        remarks = "Group Permission Deleted",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<GroupPermissionResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<GroupPermissionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all GroupPermission records from the database
        // Returns a response model containing the list of GroupPermissions or indicating the absence of records
        public async Task<ResponseModel<List<GroupPermissionResponseDTO>>> GetGroupPermissionsList()
        {
            try
            {
                // Retrieve all GroupPermission records from the database
                var grouppermissions = await db.tbl_group_permissions.ToListAsync();

                if (grouppermissions.Count() > 0)
                {
                    // If there are records, return a success response model with the list of GroupPermissions
                    return new ResponseModel<List<GroupPermissionResponseDTO>>()
                    {
                        data = _mapper.Map<List<GroupPermissionResponseDTO>>(grouppermissions),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // If no records are found, return a failure response
                    return new ResponseModel<List<GroupPermissionResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<GroupPermissionResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a specific GroupPermission record from the database based on the provided GroupPermission ID
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<GroupPermissionResponseDTO>> GetGroupPermission(string GroupPermissionId)
        {
            try
            {
                // Retrieve the existing GroupPermission record from the database based on the provided ID
                var existingGroupPermission = await db.tbl_group_permissions.Where(x => x.group_permission_id == Guid.Parse(GroupPermissionId)).FirstOrDefaultAsync();

                if (existingGroupPermission != null)
                {
                    // If the GroupPermission record is found, return a success response model with details
                    return new ResponseModel<GroupPermissionResponseDTO>()
                    {
                        data = _mapper.Map<GroupPermissionResponseDTO>(existingGroupPermission),
                        remarks = "Group Permission found successfully",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<GroupPermissionResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<GroupPermissionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing GroupPermission record based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<GroupPermissionResponseDTO>> UpdateGroupPermission(UpdateGroupPermissionDTO model)
        {
            try
            {
                // Retrieve the existing GroupPermission record from the database based on the provided ID
                var existingGroupPermission = await db.tbl_group_permissions.Where(x => x.group_permission_id == Guid.Parse(model.groupPermissionId)).FirstOrDefaultAsync();

                if (existingGroupPermission != null)
                {
                    // If the GroupPermission record is found, update it with the properties from the provided model
                    existingGroupPermission = _mapper.Map(model, existingGroupPermission);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated GroupPermission record
                    return new ResponseModel<GroupPermissionResponseDTO>()
                    {
                        remarks = $"GroupPermission: {model.groupPermissionId} of User Type: {existingGroupPermission.tbl_user_type.user_type_name} for Functionality {existingGroupPermission.tbl_functionality.functionality_name} has been updated",
                        data = _mapper.Map<GroupPermissionResponseDTO>(existingGroupPermission),
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<GroupPermissionResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<GroupPermissionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}