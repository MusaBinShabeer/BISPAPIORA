using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BISPAPIORA.Repositories.AppVersionValidatingServicesRepo;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.UserDTOs;

namespace BISPAPIORA.Extensions.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserAuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        private readonly IConfiguration configuration;
        private readonly IAppVersionValidatingServices appVersionValidatingServices;
        private readonly string AppVersionHeaderName;
        private readonly Dbcontext db;
        public UserAuthorizeAttribute(IAppVersionValidatingServices appVersionValidatingServices, IConfiguration configuration, Dbcontext db)
        {
            this.appVersionValidatingServices = appVersionValidatingServices;
            this.configuration = configuration;
            AppVersionHeaderName = configuration.GetSection("App-Version:Header_key").Value ?? "";
            this.db = db;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string appVersion = context.HttpContext.Request.Headers[AppVersionHeaderName];
            if (appVersion != null)
            {
                if (appVersionValidatingServices.IsValid(appVersion))
                {
                    var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
                    if (allowAnonymous)
                        return;
                    var account = context.HttpContext.Items["User"] as UserResponseDTO;
                    if (account == null)
                    {
                        context.Result = new UnauthorizedObjectResult(StatusCodes.Status401Unauthorized);
                    }
                }
                else
                {
                    var url = db.tbl_app_versions.FirstOrDefault().app_update_url;
                    context.Result = Result(HttpStatusCode.Unauthorized, $"{url}");
                }
            }
            else 
            {
                var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
                if (allowAnonymous)
                    return;
                var account = context.HttpContext.Items["User"] as UserResponseDTO;
                if (account == null)
                {
                    context.Result = new UnauthorizedObjectResult(StatusCodes.Status401Unauthorized);
                }
            }
        }
        private static ActionResult Result(HttpStatusCode statusCode, string reason) => new ContentResult
        {
            StatusCode = (int)statusCode,
            Content = $"Status Code: {(int)statusCode}; {statusCode}; {reason}",
            ContentType = "text/plain",
        };
    }
}
