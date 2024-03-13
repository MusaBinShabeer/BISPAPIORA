using BISPAPIORA.Models.DTOS.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BISPAPIORA.Repositories.AppVersionServicesRepo;
using System.Net;

namespace BISPAPIORA.Extensions.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserAuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        private readonly IConfiguration configuration;
        private readonly IAppVersionServices appVersionServices;
        private readonly string AppVersionHeaderName;
        public UserAuthorizeAttribute( IAppVersionServices appVersionServices, IConfiguration configuration)
        {
            this.appVersionServices = appVersionServices;
            this.configuration = configuration;
            AppVersionHeaderName = configuration.GetSection("App-Version:Header_key").Value ?? "";
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string appVersion = context.HttpContext.Request.Headers[AppVersionHeaderName];
            if (!appVersionServices.IsValid(appVersion))
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
                context.Result = Result(HttpStatusCode.Unauthorized, "Follow This Url to Update App");
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
