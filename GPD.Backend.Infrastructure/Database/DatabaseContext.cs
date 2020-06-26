using GPD.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GPD.Backend.Infrastructure.Database
{
    public class DatabaseContext : DbContext, IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        public static DatabaseContext CreateContext(DatabaseFacade databaseFacade) => new DatabaseContext().CreateDbContext(new string[] { databaseFacade.GetDbConnection().ConnectionString });

        public DatabaseContext CreateDbContext(string[] args)
        {
            var builderContext = new DbContextOptionsBuilder<DatabaseContext>();

            if (args is null || args.Length == 0)
            {
                var directory = new DirectoryInfo(typeof(DatabaseContext).Assembly.Location);
                while (!directory.FullName.ToUpper().EndsWith("BACKEND_3-1")) { directory = directory.Parent; }
                string pathBaseServer = Path.Combine(directory.FullName, "GPD.Backend.Api");
                var builder = new ConfigurationBuilder()
                    .SetBasePath(pathBaseServer)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
                var configuration = builder.Build();
                string connectionStringDatabase = GetDatabaseStringConnection(configuration);
                builderContext.UseSqlServer(connectionStringDatabase);
            }
            else
            {
                builderContext.UseSqlServer(args.First());
            }

            return new DatabaseContext(builderContext.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Assembly currentAssembly = typeof(DatabaseContext).Assembly;

            IEnumerable<Type> efMappingTypes = currentAssembly.GetTypes().Where(tp =>
                tp.FullName.StartsWith("GPD.Backend.Infrastructure.Database.Mappings.") &&
                tp.FullName.EndsWith("Mapping"));

            foreach (var map in efMappingTypes.Select(Activator.CreateInstance))
            {
                modelBuilder.ApplyConfiguration((dynamic)map);
            }
        }

        public static string GetDatabaseStringConnection(IConfiguration configuration)
        {
            string connectionDatabaseStringBase = configuration["DatabaseConnection"];
            string userId = configuration["DatabaseConnectionSettings:UserId"];
            string password = configuration["DatabaseConnectionSettings:Password"];
            string host = configuration["DatabaseConnectionSettings:Host"];
            string database = configuration["DatabaseConnectionSettings:Database"];
            string connectionStringDatabase = string.Format($"{connectionDatabaseStringBase}", host, database, userId, password);
            return connectionStringDatabase;
        }

        public virtual DbSet<Auditoria> Auditorias { get; set; }

        public virtual DbSet<IndicadorLancamento> IndicadorLancamentos { get; set; }
    }
}
