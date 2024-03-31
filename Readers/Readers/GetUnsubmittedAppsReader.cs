using Dapper;
using Domain;
using Domain.Readers;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Readers.Readers
{
    public class GetUnsubmittedAppsReader : IGetUnsubmittedAppsReader
    {
        private readonly IConfiguration _configuration;

        public GetUnsubmittedAppsReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Applications>> GetUnsubmittedApps(DateTime datetime)
        {
            var query = "SELECT id, author, activity, name, description, outline FROM applications WHERE datetime::timestamp > @datetime";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var apps = await connection.QueryAsync<Applications>(query, new { datetime });
                return apps.ToList();
            }
        }
    }
}
