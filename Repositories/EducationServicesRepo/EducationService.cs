using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EducationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Repositories.EducationServicesRepo
{
    public class EducationService : IEducationService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public EducationService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }

        // Adds a new education record to the database
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EducationResponseDTO>> AddEducation(AddEducationDTO model)
        {
            try
            {
                // Check if an education record with the same name already exists
                var education = await db.tbl_educations.Where(x => x.education_name.ToLower().Equals(model.educationName.ToLower())).FirstOrDefaultAsync();

                if (education == null)
                {
                    // If no existing record is found, create a new education record
                    var newEducation = new tbl_education();

                    // Map properties from the provided DTO to the entity using AutoMapper
                    newEducation = _mapper.Map<tbl_education>(model);
                    db.tbl_educations.Add(newEducation);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the added education record
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Education {model.educationName} has been added successfully",
                        data = _mapper.Map<EducationResponseDTO>(newEducation),
                    };
                }
                else
                {
                    // If an existing record is found, return a failure response indicating duplication
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        success = false,
                        remarks = $"Education with name {model.educationName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EducationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes an education record from the database based on the provided ID
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EducationResponseDTO>> DeleteEducation(string educationId)
        {
            try
            {
                // Retrieve the existing education record from the database based on the provided ID
                var existingEducation = await db.tbl_educations.Where(x => x.education_id == Guid.Parse(educationId)).FirstOrDefaultAsync();

                if (existingEducation != null)
                {
                    // Remove the existing education record from the database
                    existingEducation.is_active=false;
                    await db.SaveChangesAsync();

                    // Return a success response model indicating that the education record has been deleted
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        remarks = "Education Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EducationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all education records from the database
        // Returns a response model containing the list of education records
        public async Task<ResponseModel<List<EducationResponseDTO>>> GetEducationsList()
        {
            try
            {
                // Retrieve all education records from the database
                var educations = await db.tbl_educations.ToListAsync();

                if (educations.Count() > 0)
                {
                    // Return a success response model with the list of education records
                    return new ResponseModel<List<EducationResponseDTO>>()
                    {
                        data = _mapper.Map<List<EducationResponseDTO>>(educations),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no records are found
                    return new ResponseModel<List<EducationResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<EducationResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        // Retrieves a list of all active education records from the database
        // Returns a response model containing the list of education records
        public async Task<ResponseModel<List<EducationResponseDTO>>> GetActiveEducationsList()
        {
            try
            {
                var isActive= true;
                // Retrieve all education records from the database
                var activeEducations = await db.tbl_educations
                    .Where(x=>x.is_active==isActive)
                    .ToListAsync();

                if (activeEducations.Count() > 0)
                {
                    // Return a success response model with the list of education records
                    return new ResponseModel<List<EducationResponseDTO>>()
                    {
                        data = _mapper.Map<List<EducationResponseDTO>>(activeEducations),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no records are found
                    return new ResponseModel<List<EducationResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<EducationResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves an education record from the database based on the provided ID
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EducationResponseDTO>> GetEducation(string educationId)
        {
            try
            {
                // Retrieve the existing education record from the database based on the provided ID
                var existingEducation = await db.tbl_educations.Where(x => x.education_id == Guid.Parse(educationId)).FirstOrDefaultAsync();

                if (existingEducation != null)
                {
                    // If the education record is found, return a success response with details
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        data = _mapper.Map<EducationResponseDTO>(existingEducation),
                        remarks = "Education found successfully",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EducationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing education record based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<EducationResponseDTO>> UpdateEducation(UpdateEducationDTO model)
        {
            try
            {
                // Retrieve the existing education record from the database based on the provided ID
                var existingEducation = await db.tbl_educations.Where(x => x.education_id == Guid.Parse(model.educationId)).FirstOrDefaultAsync();

                if (existingEducation != null)
                {
                    // If the education record is found, update it with the properties from the provided model
                    existingEducation = _mapper.Map(model, existingEducation);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated education record
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        remarks = $"Education: {model.educationName} has been updated",
                        data = _mapper.Map<EducationResponseDTO>(existingEducation),
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<EducationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}
