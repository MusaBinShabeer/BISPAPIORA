using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenServicesRepo;

namespace BISPAPIORA.Repositories.RegistrationServicesRepo
{
    public interface IRegistrationService
    {
        public Task<ResponseModel<RegistrationResponseDTO>> AddRegisteredCitizen(AddRegistrationDTO model);
        public Task<ResponseModel<RegistrationResponseDTO>> DeleteRegistration(string registrationId);
        public Task<ResponseModel<RegistrationResponseDTO>> GetRegistration(string registrationId);
    }
}
