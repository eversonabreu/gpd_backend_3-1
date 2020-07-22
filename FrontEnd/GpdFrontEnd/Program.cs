using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using GpdFrontEnd.Infraestructure;
using GpdFrontEnd.Services.System;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using Blazored.Modal;

namespace GpdFrontEnd
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("http://192.168.185.151:5020/") });
            //builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("http://gpd-amostra-backend.azurewebsites.net/") });
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddScoped<Session>();
            builder.Services.AddScoped<HttpService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddBlazoredModal();
            AddServices(builder.Services);
            await builder.Build().RunAsync();
        }

        private static void AddServices(IServiceCollection services)
        {
        }
    }
}
