using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.AppVersionValidatingServicesRepo
{
    public interface IAppVersionValidatingServices
    {
        public bool IsValid(string apiVersion);
    }
}