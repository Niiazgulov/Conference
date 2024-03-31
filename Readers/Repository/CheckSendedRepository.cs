using Dapper;
using Domain;
using Domain.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Readers.Repository
{
    public class CheckSendedRepository : ICheckSendedRepository
    {
        private readonly IConfiguration _configuration;

        public CheckSendedRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CheckSended(Guid id)
        {
            var query = "SELECT sended FROM applications WHERE id = @id";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var requestedApp = await connection.QuerySingleAsync<AppForSendorDeleteorEdit>(query, new { id });
                if (requestedApp!.Sended == true)
                {
                    return "YES";
                }

                return "NO";
            }
        }

    }
}
