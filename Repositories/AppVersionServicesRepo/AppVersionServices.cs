namespace BISPAPIORA.Repositories.AppVersionServicesRepo
{
    public class AppVersionServices : IAppVersionServices
    {
        private readonly IConfiguration configuration;
        public AppVersionServices(IConfiguration configuration)
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
