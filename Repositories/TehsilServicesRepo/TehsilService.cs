using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.TehsilDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;
using BISPAPIORA.Models.DTOS.DistrictDTO;

namespace BISPAPIORA.Repositories.TehsilServicesRepo
{
    public class TehsilService : ITehsilService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public TehsilService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        // Adds a new tehsil based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<TehsilResponseDTO>> AddTehsil(AddTehsilDTO model)
        {
            try
            {
                // Check if the tehsil with the provided name already exists
                var tehsil = await db.tbl_tehsils.Where(x => x.tehsil_name.ToLower().Equals(model.tehsilName.ToLower())).FirstOrDefaultAsync();

                if (tehsil == null)
                {
                    // Create a new tehsil entity and map properties from the provided model
                    var newTehsil = new tbl_tehsil();
                    newTehsil = _mapper.Map<tbl_tehsil>(model);

                    // Add the new tehsil to the database and save changes
                    db.tbl_tehsils.Add(newTehsil);
                    await db.SaveChangesAsync();

                    // Return a success response model with the added tehsil details
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        success = true,
                        remarks = $"Tehsil {model.tehsilName} has been added successfully",
                        data = _mapper.Map<TehsilResponseDTO>(newTehsil),
                    };
                }
                else
                {
                    // Return a failure response model if tehsil with the same name already exists
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        success = false,
                        remarks = $"Tehsil with name {model.tehsilName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<TehsilResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes a tehsil based on the provided tehsilId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<TehsilResponseDTO>> DeleteTehsil(string tehsilId)
        {
            try
            {
                // Retrieve the existing tehsil from the database based on the tehsilId
                var existingTehsil = await db.tbl_tehsils
                    .Where(x => x.tehsil_id == Guid.Parse(tehsilId))
                    .FirstOrDefaultAsync();

                if (existingTehsil != null)
                {
                    // Remove the existing tehsil and save changes to the database
                    existingTehsil.is_active = false;
                    await db.SaveChangesAsync();

                    // Return a success response model indicating that the tehsil has been deleted
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        remarks = "Tehsil Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<TehsilResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all tehsils along with associated district and province details
        // Returns a response model containing the list of tehsils or an error message
        public async Task<ResponseModel<List<TehsilResponseDTO>>> GetTehsilsList()
        {
            try
            {
                // Retrieve all tehsils from the database, including associated district and province details
                var tehsils = await db.tbl_tehsils.Include(x => x.tbl_district).Include(x => x.tbl_district.tbl_province).ToListAsync();

                if (tehsils.Count() > 0)
                {
                    // Return a success response model with the list of tehsils
                    return new ResponseModel<List<TehsilResponseDTO>>()
                    {
                        data = _mapper.Map<List<TehsilResponseDTO>>(tehsils),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no tehsils are found
                    return new ResponseModel<List<TehsilResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<TehsilResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of active tehsils along with associated district and province details
        // Returns a response model containing the list of tehsils or an error message
        public async Task<ResponseModel<List<TehsilResponseDTO>>> GetActiveTehsilsList()
        {
            try
            {
                var isActive= true;
                // Retrieve all tehsils from the database, including associated district and province details
                var activeTehsils = await db.tbl_tehsils
                    .Include(x => x.tbl_district)
                    .Include(x => x.tbl_district.tbl_province)
                    .Where(x=>x.is_active== isActive)
                    .ToListAsync();

                if (activeTehsils.Count() > 0)
                {
                    // Return a success response model with the list of tehsils
                    return new ResponseModel<List<TehsilResponseDTO>>()
                    {
                        data = _mapper.Map<List<TehsilResponseDTO>>(activeTehsils),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no tehsils are found
                    return new ResponseModel<List<TehsilResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<TehsilResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        // Retrieves a specific tehsil based on the provided tehsilId
        // Returns a response model containing the tehsil details or an error message
        public async Task<ResponseModel<TehsilResponseDTO>> GetTehsil(string tehsilId)
        {
            try
            {
                // Retrieve the existing tehsil from the database, including associated district and province details
                var existingTehsil = await db.tbl_tehsils.Include(x => x.tbl_district).Include(x => x.tbl_district.tbl_province).Where(x => x.tehsil_id == Guid.Parse(tehsilId)).FirstOrDefaultAsync();

                if (existingTehsil != null)
                {
                    // Return a success response model with the details of the retrieved tehsil
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        data = _mapper.Map<TehsilResponseDTO>(existingTehsil),
                        remarks = "Tehsil found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<TehsilResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing tehsil based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<TehsilResponseDTO>> UpdateTehsil(UpdateTehsilDTO model)
        {
            try
            {
                // Retrieve the existing tehsil from the database based on the tehsilId
                var existingTehsil = await db.tbl_tehsils.Where(x => x.tehsil_id == Guid.Parse(model.tehsilId)).FirstOrDefaultAsync();

                if (existingTehsil != null)
                {
                    // Map properties from the provided model to the existing tehsil and save changes
                    existingTehsil = _mapper.Map(model, existingTehsil);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating that the tehsil has been updated
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        remarks = $"Tehsil: {model.tehsilName} has been updated",
                        data = _mapper.Map<TehsilResponseDTO>(existingTehsil),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<TehsilResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of tehsils based on the provided districtId
        // Returns a response model containing the list of tehsils or an error message
        public async Task<ResponseModel<List<TehsilResponseDTO>>> GetTehsilByDistrictId(string districtId)
        {
            try
            {
                // Retrieve all tehsils from the database associated with the provided districtId
                var existingTehsils = await db.tbl_tehsils.Include(x => x.tbl_district).Where(x => x.fk_district == Guid.Parse(districtId)).ToListAsync();

                if (existingTehsils.Count() > 0)
                {
                    // Return a success response model with the list of tehsils
                    return new ResponseModel<List<TehsilResponseDTO>>()
                    {
                        data = _mapper.Map<List<TehsilResponseDTO>>(existingTehsils),
                        remarks = "Tehsils found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no tehsils are found
                    return new ResponseModel<List<TehsilResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<TehsilResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}
