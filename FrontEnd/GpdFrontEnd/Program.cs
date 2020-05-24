using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using GpdFrontEnd.Infraestructure;
using GpdFrontEnd.Services.System;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GpdFrontEnd
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // var dir = Directory.GetCurrentDirectory();
            // var builder2 = new ConfigurationBuilder()
            //     .SetBasePath(Path.Combine("", "wwwroot"))
            //     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            //     .Build();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000/") });

            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddSingleton(new EnvironmentService("Development", "http://localhost:5000/"));
            builder.Services.AddScoped<Session>();
            await builder.Build().RunAsync();
        }
    }
}
