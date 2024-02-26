using BISPAPIORA.Models.DTOS.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Extensions.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration configuration;
        private string ApiKeyHeaderName = "";
        public UserAuthorizeAttribute( IConfiguration configuration)
        {
            this.configuration = configuration;
            ApiKeyHeaderName = configuration.GetSection("Api-Key:Header_key").Value ?? "";
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];
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
}
