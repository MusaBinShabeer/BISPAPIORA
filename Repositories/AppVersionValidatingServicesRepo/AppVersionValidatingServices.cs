using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Repositories.AppVersionValidatingServicesRepo
{
    public class AppVersionValidatingServices : IAppVersionValidatingServices
    {
        private readonly IConfiguration configuration;
        public AppVersionValidatingServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool IsValid(string apiVersion)
        {
            var key = configuration.GetSection("App-Version:Version").Value ?? "";
            if (apiVersion == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
