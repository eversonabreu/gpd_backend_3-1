using GPD.Backend.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using GPD.Backend.Domain.IoC;
using GPD.Backend.Domain.Services.Implementations;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GPD.Backend.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup()
        {
            const string nameVariableEnviroment = "GPD_ENVIRONMENT";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            string environment = Environment.GetEnvironmentVariable(nameVariableEnviroment)!;
            if (string.IsNullOrWhiteSpace(environment))
            {
                environment = "Local";
            }

            builder.AddUserSecrets<Startup>();
            builder = builder.AddJsonFile($"appsettings.{environment}.json", optional: true);
            configuration = builder.Build();

            Console.Out.WriteLine($"Connection in '{environment}'.");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(DatabaseContext.GetDatabaseStringConnection(configuration)));
            services.AddScoped<DatabaseContext>();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                        item => item
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddSwaggerGen(swg =>
            {
                swg.IncludeXmlComments(Path.ChangeExtension(Assembly.GetAssembly(typeof(Startup))!.Location, "xml"));
                swg.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "GPD",
                    Version = "1.0",
                    Description = "Gerenciamento pelas diretrizes"
                });

                var scheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Insira um token para autenticar as requisições na api. Exemplo: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                };

                swg.AddSecurityDefinition("Bearer", scheme);
                var requirement = new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    { scheme, new List<string>() }
                };
                swg.AddSecurityRequirement(requirement);
            });

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            Services.LoadRepositories(services, typeof(DatabaseContext).Assembly);
            services.AddSingleton(new EnvironmentService(configuration));
            services.AddBusinessServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext databaseContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(cors =>
            {
                cors.AllowAnyHeader();
                cors.AllowAnyMethod();
                cors.AllowAnyOrigin();
            });

            app.UseRouting();
            app.UseMiddleware(typeof(HandlingMiddleware));
            app.UseStaticFiles();
            app.UseMvc();
            app.UseStatusCodePages();
            app.UseSwagger();
            app.UseSwaggerUI(swg => { swg.SwaggerEndpoint("/swagger/v1/swagger.json", "GPD - Gerenciamento pelas diretrizes"); });

            databaseContext.Database.Migrate();
        }
    }
}
