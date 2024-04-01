using Dapper;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace Readers.Repository
{
    public class CheckUserByIdRepository : ICheckUserByIdRepository
    {
        private readonly IConfiguration _configuration;

        public CheckUserByIdRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CheckUserById(Guid author)
        {
            var query = "SELECT EXISTS (SELECT * FROM applications WHERE author = @author)";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var requestedApp = await connection.QuerySingleOrDefaultAsync<bool>(query, new { author });
                if (requestedApp == true)
                {
                    return true;
                }

                return false;
            }

        }

    }
}