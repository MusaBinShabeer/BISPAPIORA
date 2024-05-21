using BISPAPIORA.Models.DTOS.EducationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.EducationServicesRepo
{
    public interface IEducationService
    {
        public Task<ResponseModel<EducationResponseDTO>> AddEducation(AddEducationDTO model);
        public Task<ResponseModel<EducationResponseDTO>> DeleteEducation(string educationId);
        public Task<ResponseModel<List<EducationResponseDTO>>> GetEducationsList();
        public Task<ResponseModel<List<EducationResponseDTO>>> GetActiveEducationsList();
        public Task<ResponseModel<EducationResponseDTO>> GetEducation(string educationId);
        public Task<ResponseModel<EducationResponseDTO>> UpdateEducation(UpdateEducationDTO model);
    }
}
