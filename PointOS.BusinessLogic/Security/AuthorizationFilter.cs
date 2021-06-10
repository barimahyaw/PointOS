using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;

namespace PointOS.BusinessLogic.Security
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IUserAccountBusiness _userAccountBusiness;

        public AuthorizationFilter(IUserAccountBusiness userAccountBusiness)
        {
            _userAccountBusiness = userAccountBusiness;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authenticationToken = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrWhiteSpace(authenticationToken))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                authenticationToken = authenticationToken.Substring("Bearer".Length).Trim();

                var jwt = authenticationToken;
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                var userNamePasswordArray = token.Claims.ToArray();

                //var decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                //var userNamePasswordArray = decodedAuthenticationToken.Split(":");

                var userName = userNamePasswordArray[0].ToString();
                var password = userNamePasswordArray[1].ToString();

                var authenticationRequest = new AuthenticationRequest
                {
                    UserName = userName,
                    Password = password
                };

                if (_userAccountBusiness.AuthenticationAsync(authenticationRequest).Result.Success)
                {
                    Thread.CurrentPrincipal = new ClaimsPrincipal(new GenericIdentity(userName));
                }
                else
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            }
        }
    }
}
