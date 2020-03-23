﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace GPD.Backend.Infrastructure.Database
{
    public class DatabaseContext : DbContext, IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

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
                builderContext.UseNpgsql(connectionStringDatabase);
            }
            else
            {
                builderContext.UseNpgsql(args.First());
            }

            return new DatabaseContext(builderContext.Options);
        }

        public static string GetDatabaseStringConnection(IConfiguration configuration)
        {
            string connectionDatabaseStringBase = configuration["DatabaseConnection"];
            string userId = configuration["DatabaseConnectionSettings:UserId"];
            string password = configuration["DatabaseConnectionSettings:Password"];
            string host = configuration["DatabaseConnectionSettings:Host"];
            string port = configuration["DatabaseConnectionSettings:Port"];
            string database = configuration["DatabaseConnectionSettings:Database"];
            string pooling = configuration["DatabaseConnectionSettings:Pooling"];
            string minPoolSize = configuration["DatabaseConnectionSettings:MinPoolSize"];
            string maxPoolSize = configuration["DatabaseConnectionSettings:MaxPoolSize"];
            string connectionLifetime = configuration["DatabaseConnectionSettings:ConnectionLifetime"];

            string connectionStringDatabase = string.Format($"{connectionDatabaseStringBase}", userId,
                                                            password, host, port, database, pooling, minPoolSize, maxPoolSize, connectionLifetime);
            return connectionStringDatabase;
        }
    }
}