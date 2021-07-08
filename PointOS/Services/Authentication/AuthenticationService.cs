﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using PointOS.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PointOS.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IRestUtility _restUtility;

        public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage, IRestUtility restUtility)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _restUtility = restUtility;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> Login(AuthenticationRequest request)
        {
            var response = await _restUtility.ApiServiceAsync(BaseUrl.PointOs, "Account/Authenticate", null, request,
                null, Verb.Post);

            if (response == null)
            {
                return new ResponseHeader
                {
                    Message = "Opss!!! Something went wrong. Try again later or contact System Administrator for assistance"
                };
            }

            var result = JsonConvert.DeserializeObject<ResponseHeader>(response.ToString());

            if (!result.Success) return result;

            await _localStorage.SetItemAsync("authToken", result.Data.Token);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Data.Token);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userForAuthentication"></param>
        /// <returns></returns>
        public async Task<AuthenticationUserModel> LoginV2(AuthenticationRequest userForAuthentication)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type","password"),
                new KeyValuePair<string, string>("username",userForAuthentication.UserName),
                new KeyValuePair<string, string>("grant_type",userForAuthentication.Password)
            });

            var authResult = await _httpClient.PostAsync("", data);

            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<AuthenticationUserModel>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await _localStorage.SetItemAsync("authToken", result.AccessToken);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.AccessToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        /// <summary>
        /// User Registration Button Submit Handler
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseHeader> Register(UserRegistrationRequest request)
        {
            var response = await _restUtility.ApiServiceAsync(BaseUrl.PointOs, "Account/Authenticate", null, request,
                null, Verb.Post);

            if (response == null)
            {
                return new ResponseHeader
                {
                    Message = "Opss!!! Something went wrong. Try again later or contact System Administrator for assistance"
                };
            }

            var result = JsonConvert.DeserializeObject<ResponseHeader>(response.ToString());

            return result;
        }
    }
}
