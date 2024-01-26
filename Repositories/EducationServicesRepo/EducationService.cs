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
        private readonly OraDbContext db;
        public EducationService(IMapper mapper, OraDbContext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<EducationResponseDTO>> AddEducation(AddEducationDTO model)
        {
            try
            {
                var education = await db.tbl_educations.Where(x => x.education_name.ToLower().Equals(model.educationName.ToLower())).FirstOrDefaultAsync();
                if (education == null)
                {
                    var newEducation = new tbl_education();
                    newEducation = _mapper.Map<tbl_education>(model);
                    db.tbl_educations.Add(newEducation);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Education {model.educationName} has been added successfully",
                        data = _mapper.Map<EducationResponseDTO>(newEducation),
                    };
                }
                else
                {
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        success = false,
                        remarks = $"Education with name {model.educationName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EducationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EducationResponseDTO>> DeleteEducation(string educationId)
        {
            try
            {
                var existingEducation = await db.tbl_educations.Where(x => x.education_id == Guid.Parse(educationId)).FirstOrDefaultAsync();
                if (existingEducation != null)
                {
                    db.tbl_educations.Remove(existingEducation);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        remarks = "Education Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EducationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<EducationResponseDTO>>> GetEducationsList()
        {
            try
            {
                var educations = await db.tbl_educations.ToListAsync();
                if (educations.Count() > 0)
                {
                    return new ResponseModel<List<EducationResponseDTO>>()
                    {
                        data = _mapper.Map<List<EducationResponseDTO>>(educations),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<EducationResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<EducationResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EducationResponseDTO>> GetEducation(string educationId)
        {
            try
            {
                var existingEducation = await db.tbl_educations.Where(x => x.education_id == Guid.Parse(educationId)).FirstOrDefaultAsync();
                if (existingEducation != null)
                {
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        data = _mapper.Map<EducationResponseDTO>(existingEducation),
                        remarks = "Education found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EducationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EducationResponseDTO>> UpdateEducation(UpdateEducationDTO model)
        {
            try
            {
                var existingEducation = await db.tbl_educations.Where(x => x.education_id == Guid.Parse(model.educationId)).FirstOrDefaultAsync();
                if (existingEducation != null)
                {
                    existingEducation = _mapper.Map(model, existingEducation);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        remarks = $"Education: {model.educationName} has been updated",
                        data = _mapper.Map<EducationResponseDTO>(existingEducation),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EducationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EducationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
