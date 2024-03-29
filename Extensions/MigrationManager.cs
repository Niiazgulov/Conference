using ConfApp.Migrations;
using FluentMigrator.Runner;

namespace ConfApp.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                try
                {
                    databaseService.CreateDatabase("ConfApps");

                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                }
                catch
                {
                    throw;
                }
            }

            return host;
        }
    }
}
