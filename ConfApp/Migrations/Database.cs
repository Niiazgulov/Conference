using ConfApp.Context;
using Dapper;

namespace ConfApp.Migrations
{
    public class Database
    {
        private readonly DapperContext _context;
        public Database(DapperContext context)
        {
            _context = context;
        }
        public void CreateDatabase(string dbName)
        {
            using var connection = _context.CreateMasterConnection();
            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{dbName}';";
            var dbCount = connection.ExecuteScalar<int>(sqlDbCount);
            if (dbCount == 0)
            {
                var sql = $"CREATE DATABASE \"{dbName}\"";
                connection.ExecuteAsync(sql);
            }
        }
    }
}
