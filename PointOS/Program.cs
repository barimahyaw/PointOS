using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PointOS.Common.Helpers;
using PointOS.Common.Helpers.IHelpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using MudBlazor.Services;

namespace PointOS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMudServices();

            builder.Services.AddSingleton<IRestUtility, RestUtility>();

            await builder.Build().RunAsync();
        }
    }
}
