using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenServicesRepo;

namespace BISPAPIORA.Repositories.EnrollmentServicesRepo
{
    public interface IEnrollmentService
    {
        public Task<ResponseModel<EnrollmentResponseDTO>> AddEnrolledCitizen(AddEnrollmentDTO model);
        public Task<ResponseModel<EnrollmentResponseDTO>> DeleteEnrollment(string registrationId);
        public Task<ResponseModel<EnrollmentResponseDTO>> GetEnrollment(string registrationId);
    }
}
