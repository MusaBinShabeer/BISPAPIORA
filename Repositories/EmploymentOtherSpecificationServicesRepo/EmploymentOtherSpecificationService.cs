using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EmploymentOtherSpecificationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Repositories.EmploymentServicesRepo;

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
        public async Task<ResponseModel<EmploymentOtherSpecificationResponseDTO>> AddEmploymentOtherSpecification(AddEmploymentOtherSpecificationDTO model)
        {
            try
            {
                var employmentOtherSpecification = await db.tbl_employment_other_specifications.Where(x => x.employment_other_specification.ToLower().Equals(model.employmentOtherSpecification.ToLower())).FirstOrDefaultAsync();
                if (employmentOtherSpecification == null)
                {
                    var newEmploymentOtherSpecification = new tbl_employment_other_specification();
                    newEmploymentOtherSpecification = _mapper.Map<tbl_employment_other_specification>(model);
                    db.tbl_employment_other_specifications.Add(newEmploymentOtherSpecification);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Employment Other Specification {model.employmentOtherSpecification} has been added successfully",
                        data = _mapper.Map<EmploymentOtherSpecificationResponseDTO>(newEmploymentOtherSpecification),
                    };
                }
                else
                {
                    var existingEmploymentOtherSpecification = _mapper.Map<UpdateEmploymentOtherSpecificationDTO>(model);
                    existingEmploymentOtherSpecification.employmentOtherSpecificationId = employmentOtherSpecification.employment_other_specification_id.ToString();
                    var response = await UpdateEmploymentOtherSpecification(existingEmploymentOtherSpecification);
                    return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EmploymentOtherSpecificationResponseDTO>> UpdateEmploymentOtherSpecification(UpdateEmploymentOtherSpecificationDTO model)
        {
            try
            {
                var existingEmploymentOtherSpecification = await db.tbl_employment_other_specifications.Where(x => x.employment_other_specification_id == Guid.Parse(model.employmentOtherSpecificationId)).FirstOrDefaultAsync();
                if (existingEmploymentOtherSpecification != null)
                {
                    existingEmploymentOtherSpecification = _mapper.Map(model, existingEmploymentOtherSpecification);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                    {
                        remarks = $"Employment Other Specification has been updated",
                        data = _mapper.Map<EmploymentOtherSpecificationResponseDTO>(existingEmploymentOtherSpecification),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EmploymentOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}