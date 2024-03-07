using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EmploymentOtherSpecificationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BISPAPIORA.Repositories.EmploymentOtherSpecificationServicesRepo
{
    public class EmploymentOtherSpecificationService : IEmploymentOtherSpecificationService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public EmploymentOtherSpecificationService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }

        // Adds or updates an employment other specification based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EmploymentOtherSpecificationResponseDTO>> AddEmploymentOtherSpecification(AddEmploymentOtherSpecificationDTO model)
        {
            try
            {
                // Check if an employment other specification already exists for the given citizen
                var employmentOtherSpecification = await db.tbl_employment_other_specifications
                    .Where(x => x.fk_citizen == Guid.Parse(model.fkCitizen.ToLower()))
                    .FirstOrDefaultAsync();

                if (employmentOtherSpecification == null)
                {
                    // If no existing record is found, create a new employment other specification
                    var newEmploymentOtherSpecification = new tbl_employment_other_specification();

                    // Map properties from the provided DTO to the entity using AutoMapper
                    newEmploymentOtherSpecification = _mapper.Map<tbl_employment_other_specification>(model);

                    // Add the new entity to the database
                    db.tbl_employment_other_specifications.Add(newEmploymentOtherSpecification);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the added employment other specification
                    return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Employment Other Specification {model.employmentOtherSpecification} has been added successfully",
                        // Map the entity to the response DTO using AutoMapper
                        data = _mapper.Map<EmploymentOtherSpecificationResponseDTO>(newEmploymentOtherSpecification),
                    };
                }
                else
                {
                    // If an existing record is found, update the employment other specification using the provided model

                    // Map properties from the provided model to an UpdateEmploymentOtherSpecificationDTO using AutoMapper
                    var existingEmploymentOtherSpecification = _mapper.Map<UpdateEmploymentOtherSpecificationDTO>(model);

                    // Set the ID property of the mapped model to the ID of the existing employment other specification
                    existingEmploymentOtherSpecification.employmentOtherSpecificationId = employmentOtherSpecification.employment_other_specification_id.ToString();

                    // Perform the update operation by calling the UpdateEmploymentOtherSpecification method with the mapped model
                    var response = await UpdateEmploymentOtherSpecification(existingEmploymentOtherSpecification);

                    // Return the response from the update operation
                    return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks,
                        // Note: The data field is not explicitly mapped here as it's already mapped during the update operation
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing employment other specification based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EmploymentOtherSpecificationResponseDTO>> UpdateEmploymentOtherSpecification(UpdateEmploymentOtherSpecificationDTO model)
        {
            try
            {
                // Retrieve the existing employment other specification from the database based on the provided ID
                var existingEmploymentOtherSpecification = await db.tbl_employment_other_specifications
                    .Where(x => x.employment_other_specification_id == Guid.Parse(model.employmentOtherSpecificationId))
                    .FirstOrDefaultAsync();

                if (existingEmploymentOtherSpecification != null)
                {
                    // Update the existing employment other specification with the properties from the provided model
                    existingEmploymentOtherSpecification = _mapper.Map(model, existingEmploymentOtherSpecification);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated employment other specification
                    return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                    {
                        remarks = $"Employment Other Specification has been updated",
                        // Map the entity to the response DTO using AutoMapper
                        data = _mapper.Map<EmploymentOtherSpecificationResponseDTO>(existingEmploymentOtherSpecification),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record",
                        // Note: The data field is not explicitly mapped here as there's no data to map for a non-existing record
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}