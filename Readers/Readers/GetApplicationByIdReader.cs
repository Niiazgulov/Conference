using Dapper;
using Domain;
using Domain.Readers;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Readers.Readers
{
    public class GetApplicationByIdReader : IGetApplicationByIdReader
    {
        private readonly IConfiguration _configuration;

        public GetApplicationByIdReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Applications?> GetAppsById(Guid id)
        {
            var query = "SELECT id, author, activity, name, description, outline FROM applications WHERE id = @id";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var app = await connection.QuerySingleOrDefaultAsync<Applications>(query, new { id });
                return app;
            }
        }

    }
}
