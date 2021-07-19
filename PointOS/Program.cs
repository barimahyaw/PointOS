using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using PointOS.Common.Helpers;
using PointOS.Common.Helpers.IHelpers;
using PointOS.Services;
using PointOS.Services.Authentication;
using PointOS.Services.Notifications;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PointOS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IApiEndpointCallService, ApiEndpointCallService>();

            builder.Services.AddBlazoredSessionStorage();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMudServices();

            builder.Services.AddSingleton<IRestUtility, RestUtility>();

            await builder.Build().RunAsync();
        }
    }
}
