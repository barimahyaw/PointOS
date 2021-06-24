using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace PointOS.Services.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        //private readonly IRestUtility _restUtility;
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationState _anonymous;

        public AuthStateProvider(/*IRestUtility restUtility,*/ ILocalStorageService localStorage, HttpClient httpClient)
        {
            //_restUtility = restUtility;
            _localStorage = localStorage;
            _httpClient = httpClient;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return _anonymous;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParser.ParseClaimFromJwt(token), "jwtAuthType")));
        }

        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParser.ParseClaimFromJwt(token), "jwtAuthType"));

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }


        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    var response = await _restUtility.ApiServiceAsync(BaseUrl.PointOs, "Account/Authenticate", null, null,
        //        null, Verb.Post);

        //    var user = JsonConvert.DeserializeObject<UserResponse>(response.ToString());

        //    if (user != null && user.UserName == null)
        //        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        //    var claim = new Claim(ClaimTypes.Name, user.UserName);

        //    var claimIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");

        //    var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

        //    return new AuthenticationState(claimsPrincipal);

        //}
    }
}
