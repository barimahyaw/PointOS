using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PointOS.Services.Authentication
{
    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBae64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            ExtractRolesFromJwt(claims, keyValuePairs);
            //ExtractUserNameFromJwt(jwt, claims);

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private static void ExtractRolesFromJwt(List<Claim> claims, IDictionary<string, object> keyValuePares)
        {
            keyValuePares.TryGetValue(ClaimTypes.Role, out var roles);

            if (roles == null) return;

            var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');

            if (parsedRoles.Length > 1)
                claims.AddRange(parsedRoles.Select(parsedRole => new Claim(ClaimTypes.Role, parsedRole.Trim('"'))));
            else
                claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));

            keyValuePares.Remove(ClaimTypes.Role);
        }

        private static void ExtractUserNameFromJwt(ICollection<Claim> claims, IDictionary<string, object> keyValuePares)
        {
            keyValuePares.TryGetValue(ClaimTypes.Name, out var name);

            if (name != null) claims.Add(new Claim(ClaimTypes.Name, name.ToString()));

            keyValuePares.Remove(ClaimTypes.Name);
        }

        private static void ExtractUserNameFromJwt(string jwt, ICollection<Claim> claims)
        {
            var authenticationToken = AuthenticationHeaderValue.Parse(jwt).ToString()
                .Substring("Bearer".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authenticationToken);

            var credentials = token.Claims.ToArray();

            var userName = credentials[0].ToString().Split(":")[1].Trim();
            //var password = credentials[1].ToString().Split(":")[1].Trim();

            claims.Add(new Claim(ClaimTypes.Name, userName));
        }

        private static byte[] ParseBae64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
