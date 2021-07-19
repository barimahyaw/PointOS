using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic.Security
{
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserAccountBusiness _userAccountBusiness;
        public CustomAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserAccountBusiness userAccountBusiness) : base(options, logger, encoder, clock)
        {
            _userAccountBusiness = userAccountBusiness;
        }


        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
         {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization Header is not found");

            try
            {
                var authenticationToken = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).ToString()
                    .Substring("Bearer".Length).Trim();

                var jwt = authenticationToken;
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);


                #region Basic Authentication Logics
                //var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                //var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);

                //var credentials = Encoding.UTF8.GetString(bytes);

                #endregion

                var credentials = token.Claims.ToArray();

                var userName  = credentials[0].ToString().Split(":")[2].Trim();
                var password = credentials[1].ToString().Split(":")[2].Trim();
                var serialNumber = credentials[2].ToString().Split(":")[2].Trim();

                var authenticationRequest = new AuthenticationRequest
                {
                    UserName = userName,
                    Password = password,
                    Id = serialNumber
                };

                if ((await _userAccountBusiness.AuthenticationAsync(authenticationRequest)).Success)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, authenticationRequest.UserName),
                        new Claim(ClaimTypes.NameIdentifier, authenticationRequest.Id)
                    };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
            }
            catch (Exception e)
            {
                return AuthenticateResult.Fail($"An Error Occured. Try again later. {e}");
            }
            return AuthenticateResult.Fail("Unauthorized Access");
        }
    }
}
