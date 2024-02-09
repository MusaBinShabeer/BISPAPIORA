using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.EmploymentOtherSpecificationDTO;

namespace BISPAPIORA.Repositories.EmploymentServicesRepo
{
    public interface IEmploymentOtherSpecificationService
    {
        public Task<ResponseModel<EmploymentOtherSpecificationResponseDTO>> AddEmploymentOtherSpecification(AddEmploymentOtherSpecificationDTO model);
        public Task<ResponseModel<EmploymentOtherSpecificationResponseDTO>> UpdateEmploymentOtherSpecification(UpdateEmploymentOtherSpecificationDTO model);
    }
}