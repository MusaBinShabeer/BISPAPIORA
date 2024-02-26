using BISPAPIORA.Models.DTOS.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Extensions.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public UserAuthorizeAttribute( )
        {
           
        }
        public void OnAuthorization(AuthorizationFilterContext context)
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
}
