using Microsoft.AspNetCore.Authorization;

namespace PointOS.BusinessLogic.Security
{
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        public CustomAuthorizationAttribute()
        {
            Policy = "CustomAuthentication";
        }
    }
}
