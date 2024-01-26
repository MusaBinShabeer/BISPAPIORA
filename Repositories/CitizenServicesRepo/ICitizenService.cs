using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.CitizenServicesRepo
{
    public interface ICitizenService
    {
        #region RegisteredCitizen
        public Task<ResponseModel<RegistrationResponseDTO>> AddRegisteredCitizen(AddRegistrationDTO model);
        public Task<ResponseModel<RegistrationResponseDTO>> UpdateRegisteredCitizen(UpdateRegistrationDTO model);
        public Task<ResponseModel<RegistrationResponseDTO>> AddRegisteredDBFCitizen(AddRegistrationDTO model);
        #endregion
        #region EnrolledCitizen
        public Task<ResponseModel<EnrollmentResponseDTO>> AddEnrolledCitizen(AddEnrollmentDTO model);
        public Task<ResponseModel<EnrollmentResponseDTO>> UpdateEnrolledCitizen(UpdateEnrollmentDTO model);
        #endregion
        public Task<ResponseModel<RegistrationResponseDTO>> DeleteCitizen(string CitizenId);
        public Task<ResponseModel<List<RegistrationResponseDTO>>> GetRegisteredCitizensList();
        public Task<ResponseModel<RegistrationResponseDTO>> GetCitizen(string CitizenId);
    }
}