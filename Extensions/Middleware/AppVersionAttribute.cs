using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Extensions.Middleware
{
    public class AppVersionAttribute:ServiceFilterAttribute
    {
        public AppVersionAttribute()
            : base(typeof(UserAuthorizeAttribute))
        {
        }
    }
    
}
