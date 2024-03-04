using BISPAPIORA.Repositories.JWTServicesRepo;
using Microsoft.AspNetCore.Authorization;

namespace BISPAPIORA.Extensions.Middleware
{
    public class JWTMiddleWare
    {
        private readonly RequestDelegate _next;
        public JWTMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
            {
                await _next(context);
                return;
            }
            else
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (token != null)
                {
                    var response = await jwtUtils.ValidateToken(token);
                    if (response.success == true)
                    {
                        context.Items["User"] = response.data;
                    }
                }
                await _next(context);
            }

        }
    }
}
