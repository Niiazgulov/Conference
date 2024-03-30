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

        public async Task<IEnumerable<Applications>> GetSubmittedApps(DateTime datetime)
        {
            Console.WriteLine(datetime.ToString());
            var query = "SELECT id, author, activity, name, description, outline FROM applications WHERE datetime::timestamp > @datetime";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var apps = await connection.QueryAsync<Applications>(query, new { datetime } );
                return apps.ToList();

            }

        }
        public Applications GetUnsubmittedOlderApps(string date)
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
        public Applications GetAppsById(Guid id)
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
