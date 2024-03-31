//using Microsoft.Data.SqlClient;
using Npgsql;
//using Microsoft.Extensions.Configuration;
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

        /**private void SqlConnectionReader()
        {
            NpgsqlConnection sqlConnection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection"));
            sqlConnection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = sqlConnection;
            command.CommandType = CommandType.Text;
            sqlConnection.Close();
        }
        **/
 
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection"));


        public IDbConnection CreateMasterConnection()
            => new NpgsqlConnection(_configuration.GetConnectionString("MasterConnection"));
        
    }
}
