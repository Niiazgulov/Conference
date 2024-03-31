using Dapper;
using Domain;
using Domain.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Readers.Repository
{
    public class AddAppsToReviewRepository : IAddAppsToReviewRepository
    {
        private readonly IConfiguration _configuration;

        public AddAppsToReviewRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> AddAppsToReview(Guid id)
        {
            var query = "SELECT id, author, activity, name, description, outline, datetime FROM applications WHERE id = @id";
            var query2 = "INSERT INTO sendedapps (id, author, activity, name, description, outline, datetime) VALUES (@id, @author, @activity, @name, @description, @outline, @datetime)";
            var query3 = "UPDATE applications SET sended = @sended WHERE id = @id";

            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var requestedApp = await connection.QuerySingleOrDefaultAsync<AppForSendorDeleteorEdit>(query, new { id });

                if (IsValidAppForReview(requestedApp))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("author", requestedApp!.Author, DbType.Guid);
                    parameters.Add("id", requestedApp.Id, DbType.Guid);
                    parameters.Add("activity", requestedApp.Activity, DbType.String);
                    parameters.Add("name", requestedApp.Name, DbType.String);
                    parameters.Add("description", requestedApp.Description, DbType.String);
                    parameters.Add("outline", requestedApp.Outline, DbType.String);
                    parameters.Add("datetime", requestedApp.DateTime, DbType.DateTime2);

                    var parameters2 = new DynamicParameters();
                    parameters2.Add("sended", true, DbType.Boolean);
                    parameters2.Add("id", id, DbType.Guid);

                    await connection.ExecuteAsync(query2, parameters);
                    await connection.ExecuteAsync(query3, parameters2);

                    return "Success";
                }

                return "Fail";
            }

        }

        private bool IsValidAppForReview(AppForSendorDeleteorEdit? app)
        {
            if (app!.Activity == null ||
                app.Name == null ||
                app.Outline == null)
            {
                return false;
            }

            return true;
        }
    }
}
