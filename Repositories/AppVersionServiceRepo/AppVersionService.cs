using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.AppVersionDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.AppVersionDTO;

namespace BISPAPIORA.Repositories.AppVersionServicesRepo
{
    public class AppVersionService : IAppVersionService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public AppVersionService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }

        // Adds a new App Version record to the database based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<AppVersionResponseDTO>> AddAppVersion(AddAppVersionDTO model)
        {
            try
            {
                // Check if a AppVersion with the same name already exists in the database
                var appVersion = await db.tbl_app_versions.Where(x => x.app_version.ToLower().Equals(model.appVersion.ToLower())).FirstOrDefaultAsync();

                if (appVersion == null)
                {
                    // If no existing record is found, create a new AppVersion record
                    var newAppVersion = new tbl_app_version();

                    // Map properties from the provided DTO to the entity using AutoMapper
                    newAppVersion = _mapper.Map<tbl_app_version>(model);
                    db.tbl_app_versions.Add(newAppVersion);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the added AppVersion record
                    return new ResponseModel<AppVersionResponseDTO>()
                    {
                        success = true,
                        remarks = $"App Version {model.appVersion} has been added successfully",
                        data = _mapper.Map<AppVersionResponseDTO>(newAppVersion),
                    };
                }
                else
                {
                    // If a AppVersion with the same name already exists, return a failure response
                    return new ResponseModel<AppVersionResponseDTO>()
                    {
                        success = false,
                        remarks = $"App Version with name {model.appVersion} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<AppVersionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes an existing App Version record from the database based on the provided AppVersion ID
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<AppVersionResponseDTO>> DeleteAppVersion(string appVersionId)
        {
            try
            {
                // Retrieve the existing AppVersion record from the database based on the provided ID
                var existingAppVersion = await db.tbl_app_versions.Where(x => x.app_version_id == Guid.Parse(appVersionId)).FirstOrDefaultAsync();

                if (existingAppVersion != null)
                {
                    // If the AppVersion record is found, remove it from the database
                    db.tbl_app_versions.Remove(existingAppVersion);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating the deletion
                    return new ResponseModel<AppVersionResponseDTO>()
                    {
                        remarks = "App Version Deleted",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<AppVersionResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<AppVersionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all App Version records from the database
        // Returns a response model containing the list of AppVersions or indicating the absence of records
        public async Task<ResponseModel<List<AppVersionResponseDTO>>> GetAppVersionsList()
        {
            try
            {
                // Retrieve all AppVersion records from the database
                var appVersions = await db.tbl_app_versions.ToListAsync();

                if (appVersions.Count() > 0)
                {
                    // If there are records, return a success response model with the list of AppVersions
                    return new ResponseModel<List<AppVersionResponseDTO>>()
                    {
                        data = _mapper.Map<List<AppVersionResponseDTO>>(appVersions),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // If no records are found, return a failure response
                    return new ResponseModel<List<AppVersionResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<AppVersionResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a specific App Version record from the database based on the provided App Version ID
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<AppVersionResponseDTO>> GetAppVersion(string appVersionId)
        {
            try
            {
                // Retrieve the existing AppVersion record from the database based on the provided ID
                var existingAppVersion = await db.tbl_app_versions.Where(x => x.app_version_id == Guid.Parse(appVersionId)).FirstOrDefaultAsync();

                if (existingAppVersion != null)
                {
                    // If the AppVersion record is found, return a success response model with details
                    return new ResponseModel<AppVersionResponseDTO>()
                    {
                        data = _mapper.Map<AppVersionResponseDTO>(existingAppVersion),
                        remarks = "App Version found successfully",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<AppVersionResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<AppVersionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing App Version record based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<AppVersionResponseDTO>> UpdateAppVersion(UpdateAppVersionDTO model)
        {
            try
            {
                // Retrieve the existing AppVersion record from the database based on the provided ID
                var existingAppVersion = await db.tbl_app_versions.Where(x => x.app_version_id == Guid.Parse(model.appVersionId)).FirstOrDefaultAsync();

                if (existingAppVersion != null)
                {
                    // If the AppVersion record is found, update it with the properties from the provided model
                    existingAppVersion = _mapper.Map(model, existingAppVersion);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated AppVersion record
                    return new ResponseModel<AppVersionResponseDTO>()
                    {
                        remarks = $"AppVersion: {model.appVersion} has been updated",
                        data = _mapper.Map<AppVersionResponseDTO>(existingAppVersion),
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<AppVersionResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<AppVersionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}