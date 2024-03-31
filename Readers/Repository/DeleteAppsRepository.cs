using Dapper;
using Domain.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Readers.Repository
{
    public class DeleteAppsRepository : IDeleteAppsRepository
    {
        private readonly IConfiguration _configuration;

        public DeleteAppsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteApps(Guid id)
        {
            var query = "DELETE FROM applications WHERE id = @id";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
