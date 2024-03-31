using Dapper;
using Domain;
using Domain.Readers;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Readers.Readers
{
    public class GetSubmittedAppsReader : IGetSubmittedAppsReader
    {
        private readonly IConfiguration _configuration;

        public GetSubmittedAppsReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Applications>> GetSubmittedApps(DateTime datetime)
        {
            var query = "SELECT id, author, activity, name, description, outline FROM sendedapps WHERE datetime::timestamp > @datetime";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var apps = await connection.QueryAsync<Applications>(query, new { datetime });
                return apps.ToList();
            }
        }
    }
}
