using BISPAPIORA.Models.DTOS.EmploymentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Repositories.EmploymentServicesRepo
{
    public interface IEmploymentService
    {
        public Task<ResponseModel<EmploymentResponseDTO>> AddEmployment(AddEmploymentDTO model);
        public Task<ResponseModel<EmploymentResponseDTO>> DeleteEmployment(string EmploymentId);
        public Task<ResponseModel<List<EmploymentResponseDTO>>> GetEmploymentsList();
        public Task<ResponseModel<List<EmploymentResponseDTO>>> GetActiveEmploymentsList();
        public Task<ResponseModel<EmploymentResponseDTO>> GetEmployment(string EmploymentId);
        public Task<ResponseModel<EmploymentResponseDTO>> UpdateEmployment(UpdateEmploymentDTO model);
    }
}
