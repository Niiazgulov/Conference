using Dapper;
using Domain;
using Domain.Readers;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;

namespace Readers
{
    public class ConferenceAppsReader : IConferenceAppsReader
    {
        private readonly IConfiguration _configuration;

        public ConferenceAppsReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Applications> GetAppsById(Guid id)
        {
            var query = "SELECT id, author, activity, name, description, outline FROM applications WHERE id = @id";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var app = await connection.QuerySingleOrDefaultAsync<Applications>(query, new { id });
                return app;
            }
        }

        public async Task<Applications> GetAppByAuthorId(Guid author)
        {
            var query = "SELECT id, author, activity, name, description, outline FROM applications WHERE author = @author";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var app = await connection.QuerySingleOrDefaultAsync<Applications>(query, new { author });
                return app;
            }
        }

        public async Task<IEnumerable<Applications>> GetUnsubmittedApps(DateTime datetime)
        {
            var query = "SELECT id, author, activity, name, description, outline FROM applications WHERE datetime::timestamp > @datetime";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var apps = await connection.QueryAsync<Applications>(query, new { datetime } );
                return apps.ToList();
            }

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


        public Applications GetCurrentApps(Guid author)
        {
            var connectionString = _configuration.GetConnectionString("NpgConnection");
            Applications app = new Applications()
            {
                Author = new Guid(),
                Name = "Vasya",
                Outline = "sdsdscsdsddc"
            };
            return app;
        }
    }
}
