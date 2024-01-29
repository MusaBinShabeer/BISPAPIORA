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
        public async Task<ResponseModel<EmploymentResponseDTO>> AddEmployment(AddEmploymentDTO model)
        {
            try
            {
                var employment = await db.tbl_employments.Where(x => x.employment_name.ToLower().Equals(model.employmentName.ToLower())).FirstOrDefaultAsync();
                if (employment == null)
                {
                    var newEmployment = new tbl_employment();
                    newEmployment = _mapper.Map<tbl_employment>(model);
                    db.tbl_employments.Add(newEmployment);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        success = true,
                        remarks = $"Employment {model.employmentName} has been added successfully",
                        data = _mapper.Map<EmploymentResponseDTO>(newEmployment),
                    };
                }
                else
                {
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        success = false,
                        remarks = $"Employment with name {model.employmentName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EmploymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EmploymentResponseDTO>> DeleteEmployment(string employmentId)
        {
            try
            {
                var existingEmployment = await db.tbl_employments.Where(x => x.employment_id == Guid.Parse(employmentId)).FirstOrDefaultAsync();
                if (existingEmployment != null)
                {
                    db.tbl_employments.Remove(existingEmployment);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        remarks = "Employment Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EmploymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<EmploymentResponseDTO>>> GetEmploymentsList()
        {
            try
            {
                var employments = await db.tbl_employments.ToListAsync();
                if (employments.Count() > 0)
                {
                    return new ResponseModel<List<EmploymentResponseDTO>>()
                    {
                        data = _mapper.Map<List<EmploymentResponseDTO>>(employments),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<EmploymentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<EmploymentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EmploymentResponseDTO>> GetEmployment(string EmploymentId)
        {
            try
            {
                var existingEmployment = await db.tbl_employments.Where(x => x.employment_id == Guid.Parse(EmploymentId)).FirstOrDefaultAsync();
                if (existingEmployment != null)
                {
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        data = _mapper.Map<EmploymentResponseDTO>(existingEmployment),
                        remarks = "Employment found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EmploymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EmploymentResponseDTO>> UpdateEmployment(UpdateEmploymentDTO model)
        {
            try
            {
                var existingEmployment = await db.tbl_employments.Where(x => x.employment_id == Guid.Parse(model.employmentId)).FirstOrDefaultAsync();
                if (existingEmployment != null)
                {
                    existingEmployment = _mapper.Map(model, existingEmployment);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        remarks = $"Employment: {model.employmentName} has been updated",
                        data = _mapper.Map<EmploymentResponseDTO>(existingEmployment),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EmploymentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EmploymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
