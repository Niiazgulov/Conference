using Dapper;
using Domain;
using Domain.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Readers.Repository
{
    public class EditAppsRepository : IEditAppsRepository
    {
        private readonly IConfiguration _configuration;

        public EditAppsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Applications?> EditApps(Guid id, EditedAppDTO app)
        {
            var query = "UPDATE applications SET activity = COALESCE(@activity, activity), name = COALESCE(@name, name), description = COALESCE(@description, description), outline = COALESCE(@outline, outline), datetime = @datetime WHERE id = @id";
            var query2 = "SELECT id, author, activity, name, description, outline FROM applications WHERE id = @id";

            DateTime localDate = DateTime.Now;
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Guid);
            parameters.Add("activity", app.Activity, DbType.String);
            parameters.Add("name", app.Name, DbType.String);
            parameters.Add("description", app.Description, DbType.String);
            parameters.Add("outline", app.Outline, DbType.String);
            parameters.Add("datetime", localDate, DbType.DateTime2);

            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                await connection.ExecuteAsync(query, parameters);
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var editedApp = await connection.QuerySingleOrDefaultAsync<Applications>(query2, new { id });
                return editedApp;
            }
        }
    }
}
