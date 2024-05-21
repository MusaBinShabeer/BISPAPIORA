using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EmploymentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.EmploymentServicesRepo
{
    public class EmploymentService : IEmploymentService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public EmploymentService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        // Adds a new employment based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EmploymentResponseDTO>> AddEmployment(AddEmploymentDTO model)
        {
            try
            {
                // Check if an employment with the same name already exists
                var employment = await db.tbl_employments.Where(x => x.employment_name.ToLower().Equals(model.employmentName.ToLower())).FirstOrDefaultAsync();

                if (employment == null)
                {
                    // Create a new employment entity and map properties from the provided model
                    var newEmployment = new tbl_employment();
                    newEmployment = _mapper.Map<tbl_employment>(model);

                    // Add the new employment to the database and save changes
                    db.tbl_employments.Add(newEmployment);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the added employment
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        success = true,
                        remarks = $"Employment {model.employmentName} has been added successfully",
                        data = _mapper.Map<EmploymentResponseDTO>(newEmployment),
                    };
                }
                else
                {
                    // Return a failure response model if an employment with the same name already exists
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        success = false,
                        remarks = $"Employment with name {model.employmentName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EmploymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes an existing employment based on the provided employmentId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EmploymentResponseDTO>> DeleteEmployment(string employmentId)
        {
            try
            {
                // Retrieve the existing employment from the database based on the employmentId
                var existingEmployment = await db.tbl_employments.Where(x => x.employment_id == Guid.Parse(employmentId)).FirstOrDefaultAsync();

                if (existingEmployment != null)
                {
                    // Remove the existing employment from the database and save changes
                    db.tbl_employments.Remove(existingEmployment);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating that the employment has been deleted
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        remarks = "Employment Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EmploymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of employments from the database
        // Returns a response model containing the list of employments or an error message
        public async Task<ResponseModel<List<EmploymentResponseDTO>>> GetEmploymentsList()
        {
            try
            {
                // Retrieve all employments from the database
                var employments = await db.tbl_employments.ToListAsync();

                if (employments.Count() > 0)
                {
                    // Return a success response model with the list of employments
                    return new ResponseModel<List<EmploymentResponseDTO>>()
                    {
                        data = _mapper.Map<List<EmploymentResponseDTO>>(employments),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no employments are found
                    return new ResponseModel<List<EmploymentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<EmploymentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of active employments from the database
        // Returns a response model containing the list of employments or an error message
        public async Task<ResponseModel<List<EmploymentResponseDTO>>> GetActiveEmploymentsList()
        {
            try
            {
                var isActive= true;
                // Retrieve all employments from the database
                var activeEmployments = await db.tbl_employments
                    .Where(x=>x.is_active== isActive)
                    .ToListAsync();

                if (activeEmployments.Count() > 0)
                {
                    // Return a success response model with the list of employments
                    return new ResponseModel<List<EmploymentResponseDTO>>()
                    {
                        data = _mapper.Map<List<EmploymentResponseDTO>>(activeEmployments),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no employments are found
                    return new ResponseModel<List<EmploymentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<EmploymentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves an employment based on the provided employmentId
        // Returns a response model containing the employment details or an error message
        public async Task<ResponseModel<EmploymentResponseDTO>> GetEmployment(string EmploymentId)
        {
            try
            {
                // Retrieve the existing employment from the database based on the employmentId
                var existingEmployment = await db.tbl_employments.Where(x => x.employment_id == Guid.Parse(EmploymentId)).FirstOrDefaultAsync();

                if (existingEmployment != null)
                {
                    // Return a success response model with the details of the found employment
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        data = _mapper.Map<EmploymentResponseDTO>(existingEmployment),
                        remarks = "Employment found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EmploymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing employment based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EmploymentResponseDTO>> UpdateEmployment(UpdateEmploymentDTO model)
        {
            try
            {
                // Retrieve the existing employment from the database based on the employmentId
                var existingEmployment = await db.tbl_employments.Where(x => x.employment_id == Guid.Parse(model.employmentId)).FirstOrDefaultAsync();

                if (existingEmployment != null)
                {
                    // Map properties from the provided model to the existing employment
                    existingEmployment = _mapper.Map(model, existingEmployment);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated employment
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        remarks = $"Employment: {model.employmentName} has been updated",
                        data = _mapper.Map<EmploymentResponseDTO>(existingEmployment),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EmploymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}
