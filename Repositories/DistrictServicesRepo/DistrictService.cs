using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.DistrictDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Repositories.DistrictServicesRepo
{
    public class DistrictService : IDistrictService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public DistrictService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        // Adds a new district based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<DistrictResponseDTO>> AddDistrict(AddDistrictDTO model)
        {
            try
            {
                // Check if a district with the same name already exists
                var district = await db.tbl_districts.Where(x => x.district_name.ToLower().Equals(model.districtName.ToLower())).FirstOrDefaultAsync();

                if (district == null)
                {
                    // Create a new district entity and map properties from the provided model
                    var newDistrict = new tbl_district();
                    newDistrict = _mapper.Map<tbl_district>(model);

                    // Add the new district to the database and save changes
                    db.tbl_districts.Add(newDistrict);
                    await db.SaveChangesAsync();

                    // Return a success response model with the added district details
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        success = true,
                        remarks = $"District {model.districtName} has been added successfully",
                        data = _mapper.Map<DistrictResponseDTO>(newDistrict),
                    };
                }
                else
                {
                    // Return a failure response model if a district with the same name already exists
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        success = false,
                        remarks = $"District with name {model.districtName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<DistrictResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes a district based on the provided districtId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<DistrictResponseDTO>> DeleteDistrict(string districtId)
        {
            try
            {
                // Retrieve the existing district from the database based on the districtId
                var existingDistrict = await db.tbl_districts.Where(x => x.district_id == Guid.Parse(districtId)).FirstOrDefaultAsync();

                if (existingDistrict != null)
                {
                    // Remove the existing district and save changes to the database
                    db.tbl_districts.Remove(existingDistrict);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating that the district has been deleted
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        remarks = "District Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<DistrictResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all districts along with associated province details
        // Returns a response model containing the list of districts or an error message
        public async Task<ResponseModel<List<DistrictResponseDTO>>> GetDistrictsList()
        {
            try
            {
                // Retrieve all districts from the database, including associated province details
                var districts = await db.tbl_districts.Include(x => x.tbl_province).ToListAsync();

                // Check if there are districts in the list
                if (districts.Count() > 0)
                {
                    // Return a success response model with the list of districts
                    return new ResponseModel<List<DistrictResponseDTO>>()
                    {
                        data = _mapper.Map<List<DistrictResponseDTO>>(districts),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no districts are found
                    return new ResponseModel<List<DistrictResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<DistrictResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a district based on the provided districtId, including associated province details
        // Returns a response model containing the district or an error message
        public async Task<ResponseModel<DistrictResponseDTO>> GetDistrict(string districtId)
        {
            try
            {
                // Retrieve the existing district from the database, including associated province details
                var existingDistrict = await db.tbl_districts.Include(x => x.tbl_province).Where(x => x.district_id == Guid.Parse(districtId)).FirstOrDefaultAsync();

                if (existingDistrict != null)
                {
                    // Return a success response model with the retrieved district details
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        data = _mapper.Map<DistrictResponseDTO>(existingDistrict),
                        remarks = "District found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<DistrictResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates a district based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<DistrictResponseDTO>> UpdateDistrict(UpdateDistrictDTO model)
        {
            try
            {
                // Retrieve the existing district from the database based on the districtId
                var existingDistrict = await db.tbl_districts.Where(x => x.district_id == Guid.Parse(model.districtId)).FirstOrDefaultAsync();

                if (existingDistrict != null)
                {
                    // Map properties from the provided model and save changes to the database
                    existingDistrict = _mapper.Map(model, existingDistrict);
                    await db.SaveChangesAsync();

                    // Return a success response model with the updated district details
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        remarks = $"District: {model.districtName} has been updated",
                        data = _mapper.Map<DistrictResponseDTO>(existingDistrict),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<DistrictResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of districts based on the provided provinceId, including associated province details
        // Returns a response model containing the list of districts or an error message
        public async Task<ResponseModel<List<DistrictResponseDTO>>> GetDistrictByProviceId(string provinceId)
        {
            try
            {
                // Retrieve all districts from the database that belong to the specified province, including associated province details
                var existingDistricts = await db.tbl_districts.Include(x => x.tbl_province).Where(x => x.fk_province == Guid.Parse(provinceId)).ToListAsync();

                if (existingDistricts.Count() > 0)
                {
                    // Return a success response model with the list of districts
                    return new ResponseModel<List<DistrictResponseDTO>>()
                    {
                        data = _mapper.Map<List<DistrictResponseDTO>>(existingDistricts),
                        remarks = "Districts found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no districts are found
                    return new ResponseModel<List<DistrictResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<DistrictResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
