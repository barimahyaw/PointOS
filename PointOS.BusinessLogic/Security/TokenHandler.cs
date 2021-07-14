using eViSeM.Common.DTO.Response;
using Microsoft.IdentityModel.Tokens;
using PointOS.Common.DTO.Request;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PointOS.BusinessLogic.Security
{
    public static class TokenHandler
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

        /// <summary>
        /// Generate authentication token with user authentication details and roles
        /// </summary>
        /// <param name="request"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static dynamic GenerateAuthToken(AuthenticationRequest request, IList<UserRoleResponse> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.SerialNumber, request.Password),
                new Claim(ClaimTypes.NameIdentifier, request.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.Name));
            //}

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GenerateAuthenticationToken(request))), SecurityAlgorithms.HmacSha256)
                ),
                new JwtPayload(claims)
            );

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                request.UserName
            };

            return output.Access_Token;
        }
    }
}
