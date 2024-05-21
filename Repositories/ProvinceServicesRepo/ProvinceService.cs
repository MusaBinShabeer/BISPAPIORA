using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ProvinceDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Repositories.ProvinceServicesRepo
{
    public class ProvinceService : IProvinceService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public ProvinceService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        // Adds a new province based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<ProvinceResponseDTO>> AddProvince(AddProvinceDTO model)
        {
            try
            {
                // Check if a province with the same name already exists
                var province = await db.tbl_provinces.Where(x => x.province_name.ToLower().Equals(model.provinceName.ToLower())).FirstOrDefaultAsync();

                if (province == null)
                {
                    // Create a new province entity and map properties from the provided model
                    var newProvince = new tbl_province();
                    newProvince = _mapper.Map<tbl_province>(model);

                    // Add the new province to the database and save changes
                    db.tbl_provinces.Add(newProvince);
                    await db.SaveChangesAsync();

                    // Return a success response model with the added province details
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        success = true,
                        remarks = $"Province {model.provinceName} has been added successfully",
                        data = _mapper.Map<ProvinceResponseDTO>(newProvince),
                    };
                }
                else
                {
                    // Return a failure response model if a province with the same name already exists
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        success = false,
                        remarks = $"Province with name {model.provinceName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<ProvinceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes a province based on the provided provinceId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<ProvinceResponseDTO>> DeleteProvince(string provinceId)
        {
            try
            {
                // Retrieve the existing province from the database based on the provinceId
                var existingProvince = await db.tbl_provinces.Where(x => x.province_id == Guid.Parse(provinceId)).FirstOrDefaultAsync();

                if (existingProvince != null)
                {
                    // Remove the existing province and save changes to the database
                    existingProvince.is_active= false;
                    await db.SaveChangesAsync();
                    // Return a success response model indicating that the province has been deleted
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        remarks = "Province Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<ProvinceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all provinces
        // Returns a response model containing the list of provinces or an error message
        public async Task<ResponseModel<List<ProvinceResponseDTO>>> GetProvincesList()
        {
            try
            {
                // Retrieve all provinces from the database
                var provinces = await db.tbl_provinces.ToListAsync();

                if (provinces.Count() > 0)
                {
                    // Return a success response model with the list of provinces
                    return new ResponseModel<List<ProvinceResponseDTO>>()
                    {
                        data = _mapper.Map<List<ProvinceResponseDTO>>(provinces),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no provinces are found
                    return new ResponseModel<List<ProvinceResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<ProvinceResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        // Retrieves a list of all provinces
        // Returns a response model containing the list of provinces or an error message
        public async Task<ResponseModel<List<ProvinceResponseDTO>>> GetActiveProvincesList()
        {
            try
            {
                var isActive = true;
                // Retrieve all provinces from the database
                var activeProvinces = await db.tbl_provinces.Where(x=>x.is_active== isActive).ToListAsync();
                if (activeProvinces.Count() > 0)
                {
                    // Return a success response model with the list of provinces
                    return new ResponseModel<List<ProvinceResponseDTO>>()
                    {
                        data = _mapper.Map<List<ProvinceResponseDTO>>(activeProvinces),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no provinces are found
                    return new ResponseModel<List<ProvinceResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<ProvinceResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a province based on the provided provinceId
        // Returns a response model containing the province details or an error message
        public async Task<ResponseModel<ProvinceResponseDTO>> GetProvince(string provinceId)
        {
            try
            {
                // Retrieve the existing province from the database based on the provinceId
                var existingProvince = await db.tbl_provinces.Where(x => x.province_id == Guid.Parse(provinceId)).FirstOrDefaultAsync();

                if (existingProvince != null)
                {
                    // Return a success response model with the details of the found province
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        data = _mapper.Map<ProvinceResponseDTO>(existingProvince),
                        remarks = "Province found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<ProvinceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates a province based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<ProvinceResponseDTO>> UpdateProvince(UpdateProvinceDTO model)
        {
            try
            {
                // Retrieve the existing province from the database based on the model's provinceId
                var existingProvince = await db.tbl_provinces.Where(x => x.province_id == Guid.Parse(model.provinceId)).FirstOrDefaultAsync();

                if (existingProvince != null)
                {
                    // Map properties from the provided model and update the existing province
                    existingProvince = _mapper.Map(model, existingProvince);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model indicating that the province has been updated
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        remarks = $"Province: {model.provinceName} has been updated",
                        data = _mapper.Map<ProvinceResponseDTO>(existingProvince),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<ProvinceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}
