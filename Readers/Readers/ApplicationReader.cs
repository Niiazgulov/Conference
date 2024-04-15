using Dapper;
using Domain.Models;
using Domain.ReadersContracts;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Readers
{
    public class ApplicationReader : IApplicationReader
    {

        private readonly IConfiguration _configuration;

        public ApplicationReader(IConfiguration configuration)
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

        public async Task<Applications?> GetAppByAuthorId(Guid author)
        {
            var query = "SELECT id, author, activity, name, description, outline FROM applications WHERE author = @author AND sended = false";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var returnedData = await connection.QueryFirstOrDefaultAsync<Applications>(query, new { author });
                
                return returnedData;
            }
        }

        public async Task<IEnumerable<Applications>> GetUnsubmittedApps(DateTime? datetime, bool sended)
        {
            var query = "SELECT id, author, activity, name, description, outline FROM applications WHERE datetime::timestamp < @datetime AND sended = @sended";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var apps = await connection.QueryAsync<Applications>(query, new { datetime, sended });
                return apps.ToList();
            }
        }

        public async Task<IEnumerable<Applications>> GetSubmittedApps(DateTime? datetime, bool sended)
        {
            var query = "SELECT id, author, activity, name, description, outline FROM applications WHERE datetime::timestamp > @datetime AND sended = @sended";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var apps = await connection.QueryAsync<Applications>(query, new { datetime, sended });
                return apps.ToList();
            }
        }
    }
}
