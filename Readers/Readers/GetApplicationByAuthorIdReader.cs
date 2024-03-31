using Dapper;
using Domain;
using Domain.Readers;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Readers.Readers
{
    public class GetApplicationByAuthorIdReader : IGetApplicationByAuthorIdReader
    {

        private readonly IConfiguration _configuration;

        public GetApplicationByAuthorIdReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Applications?> GetAppByAuthorId(Guid author)
        {
            var query = "SELECT id, author, activity, name, description, outline FROM applications WHERE author = @author";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var app = await connection.QuerySingleOrDefaultAsync<Applications>(query, new { author });
                return app;
            }
        }
    }
}
