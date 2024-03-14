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
        private readonly Dbcontext db;
        public AppVersionValidatingServices(IConfiguration configuration, Dbcontext db)
        {
            this.configuration = configuration;
            this.db = db;
        }
        public bool IsValid(string apiVersion)
        {
            var key = db.tbl_app_versions.FirstOrDefault();
            if (apiVersion == key.app_version)
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
