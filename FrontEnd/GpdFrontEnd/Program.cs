using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using GpdFrontEnd.Infraestructure;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GpdFrontEnd
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddScoped<Session>();
            await builder.Build().RunAsync();
        }
    }
}
