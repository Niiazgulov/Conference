using Npgsql;
using System.Data;

namespace ConfApp.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection"));

        public IDbConnection CreateMasterConnection()
            => new NpgsqlConnection(_configuration.GetConnectionString("MasterConnection"));
    }
}