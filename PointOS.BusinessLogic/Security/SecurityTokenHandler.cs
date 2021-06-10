using Microsoft.IdentityModel.Tokens;
using PointOS.Common.DTO.Request;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PointOS.BusinessLogic.Security
{
    public static class SecurityTokenHandler
    {
        /// <summary>
        /// Generate user Authentication Token
        /// </summary>
        /// <returns>Base64 string</returns>
        private static string GenerateAuthenticationToken(AuthenticationRequest request)
        {
            var authorization = $"{request.UserName}:{request.Password}";

            var conversion = Encoding.UTF8.GetBytes(authorization);

            return Convert.ToBase64String(conversion);
        }

        /// <summary>
        /// Generate User Company (Client) Authorization Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GenerateAuthorizationToken(AuthenticationRequest request)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GenerateAuthenticationToken(request));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserName", request.UserName),
                    new Claim("Password",request.Password)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
