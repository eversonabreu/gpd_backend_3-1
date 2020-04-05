using GPD.Backend.Domain.Services.Contracts;
using GPD.Backend.Domain.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace GPD.Backend.Domain.IoC
{
    public static class Services
    {
        public static void LoadRepositories(IServiceCollection services, Assembly assemblyDbContext)
        {
            var assembly = typeof(Services).Assembly;
            var types = assembly.GetTypes().Where(tp => tp.FullName.StartsWith("GPD.Backend.Domain.Repositories.") && tp.FullName.EndsWith("Repository"));
            var interfaces = types.Where(item => item.IsInterface).ToList();
            types = assemblyDbContext.GetTypes().Where(tp => tp.FullName.StartsWith("GPD.Backend.Infrastructure.Database.Repositories.") && tp.FullName.EndsWith("Repository"));
            var classes = types.Where(item => item.IsClass);
            Type interfaceType = null;

            foreach (var item in classes)
            {
                foreach (var subItem in interfaces)
                {
                    if (item.GetInterface(subItem.Name) != null)
                    {
                        interfaceType = subItem;
                        services.AddTransient(subItem, item);
                        break;
                    }
                }

                interfaces.Remove(interfaceType);
            }
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<IIndicadorLancamentosService, IndicadorLancamentosService>();
        }
    }
}
